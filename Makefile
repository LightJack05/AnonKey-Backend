buildDirectory := ./bin/
installDirectory := /opt/AnonKey/
publishDirectory := ./publish/
defaultOptions := 
compiler := dotnet
projectName := AnonKey-Backend
SHELL := /bin/bash



.PHONY: docs

.SILENT: clean-silent

# 
# Build the project to $(buildDirectory) in default (usually Debug) mode.
# 
build:
	@echo "[BUILD - INFO] 💬 Attempting to build $(projectName) to $(buildDirectory)..."
	$(compiler) build $(defaultOptions) -o $(buildDirectory)

#
# Clean up any auxiliary build files residing in $(buildDirectory) or in $(publishDirectory)
# 
clean:
	@echo "[CLEAN - INFO] 💬 Deleting the contents of $(publishDirectory), ./obj/ and $(buildDirectory)."
	rm -rf $(buildDirectory)
	rm -rf $(publishDirectory)
	rm -rf ./obj/

clean-silent:
	@echo "[CLEAN - INFO] 🔄 Deleting the contents of $(publishDirectory), ./obj/ and $(buildDirectory)."
	@rm -rf $(buildDirectory)
	@rm -rf $(publishDirectory)
	@rm -rf ./obj/

#
# Execute `make clean` and, in addition to that, also delete the current database.
#
wipe: clean
	@echo "[WIPE - INFO] 💬 Deleting database.db."
	if [[ -f database.db ]]; then rm database.db; fi

#
# Run the dotnet application on port 5000, and build it if necessary.
#
run: build 
	@echo "[RUN - INFO] 💬 Starting the application on port 5000..."
	ASPNETCORE_HTTP_PORTS=5000 $(compiler) run	

#
# Run the dotnet appliation on alternate port 5100, and build it if necessary.
#
run_alt: build
	@echo "[RUN_ALT - INFO] 💬 Starting the application on port 5100..."
	ASPNETCORE_HTTP_PORTS=5100 $(compiler) run	

#
# Publish the dotnet application to $(publishDirecotry).
#
publish:
	@echo "[PUBLISH - INFO] 💬 Attempting to publish $(projectName) to $(buildDirectory)..."
	$(compiler) publish -o $(publishDirectory)


#
# Install the application to $(installDirectory). 
# WARNING: This will run `make wipe`, `make uninstall` and `make publish` accordingly!
#
install: wipe uninstall publish
	@echo "[INSTALL - INFO] 💬 Attempting to install $(projectName) to $(installDirectory) ..."
	if [[ ! -d $(installDirectory) ]]; then mkdir $(installDirectory); fi
	cp -r publish/* $(installDirectory) 
	cp AnonKey.service /etc/systemd/system/
	systemctl enable AnonKey.service
	@echo "Installed AnonKey to /opt/AnonKey. Start Systemd Unit AnonKey.service to run the application!"
	@echo "The application uses TCP port 5000."

#
# Install the application in debug mode to $(installDirectory). 
# WARNING: This will run `make wipe`, `make uninstall` and `make publish` accordingly!
#
install-debug: wipe uninstall publish 
	@echo "[INSTALL-DEBUG - INFO] 💬 Attempting to install debug version of $(projectName) to $(installDirectory) ..."
	if [[ ! -d $(installDirectory) ]]; then mkdir $(installDirectory); fi
	cp -r publish/* $(installDirectory) 
	cp AnonKey-Debug.service /etc/systemd/system/
	systemctl enable AnonKey-Debug.service
	@echo "Installed debug version of AnonKey to /opt/AnonKey. Start Systemd Unit AnonKey-Debug.service to run the application!"
	@echo "The application uses TCP port 5000."

#
# This will uninstall the application from $(installDirectory) and stop all related services.
# WARNING: This will also delete the database!
#
uninstall:
	@echo "[UNINSTALL - INFO] 💬 Attempting to uninstall $(projectName) from $(installDirectory) ..."
	if [[ -f /etc/systemd/system/AnonKey.service ]]; then systemctl is-active AnonKey.Service && systemctl stop AnonKey.service; systemctl disable AnonKey.service; rm /etc/systemd/system/AnonKey.service; fi
	if [[ -f /etc/systemd/system/AnonKey-Debug.service ]]; then systemctl is-active AnonKey.Service && systemctl stop AnonKey-Debug.service; systemctl disable AnonKey-Debug.service; rm /etc/systemd/system/AnonKey-Debug.service; fi
	rm -rf $(installDirectory)

#
# Build the documentation using doxygen to ./docs/
#
docs:
	@echo "[DOCS - INFO] 💬 Attempting to build $(projectName) documentation..."
	doxygen doxygen.conf

#
# Push local commits to the remote repository.
#
push: preflight-push
	@echo "[PUSH - INFO] Pushing local commits to the remote..."
	git push


### The following profiles are project-specific preflight checks used to ensure consistent code.

preflight-push:
	@echo "[PREFLIGHT-PUSH - INFO] ✅ Running preflight-checks for a push..."
	@make -s preflight-not-main
	@echo "[PREFLIGHT-PUSH - PASS] ✅ All preflight-checks for a push have passed!"

preflight-pull:
	@echo "[PREFLIGHT-PULL - INFO] ✅ Running preflight-checks for a Pull Request..."
	@make -s preflight-not-main
	@make -s preflight-clean
	@make -s preflight-format
	@make -s preflight-compilation
	@make -s preflight-warnings
	@echo "[PREFLIGHT-PULL - PASS] ✅ All preflight-checks for a Pull Request have passed!"

preflight-not-main:
	@echo "[PREFLIGHT-NOT-MAIN - INFO] 💬 Making sure the current branch is not main..."
	@if ! git symbolic-ref -q HEAD | grep -q "/main$$"; then echo "[PREFLIGHT-NOT-MAIN - PASS] ✅ Your current branch is not main."; else echo "[PREFLIGHT-NOT-MAIN - FAIL] ❌ You are working on the main branch!"; exit 1; fi 
preflight-clean:
	@echo "[PREFLIGHT-CLEAN - INFO] 💬 Checking for uncommited changes..."
	@if [[ -z "$$(git status --porcelain=v1)" ]]; then echo "[PREFLIGHT-CLEAN - PASS] ✅ Working tree appears clean!"; else echo "[PREFLIGHT-CLEAN - FAIL] ❌ The working tree is not clean. Please commit your changes!"; exit 1; fi
preflight-format: preflight-clean 
	@echo "[PREFLIGHT-FORMAT - INFO] 💬 Checking project formatting..."
	@dotnet format; if [[ -z "$$(git status --porcelain=v1)" ]]; then echo "[PREFLIGHT-FORMAT - PASS] ✅ Project appears to be formatted correctly!"; else echo "[PREFLIGHT-FORMAT - FAIL] ❌ The project is not properly formatted! Please review the automatic formatting, and then commit the changes."; exit 1; fi
preflight-compilation: clean-silent
	@echo "[PREFLIGHT-COMPILATION - INFO] 💬 Checking if the project compiles..."
	@if dotnet build | grep -q "Build succeeded."; then echo "[PREFLIGHT-COMPILATION - PASS] ✅ Project compiles successfully."; else echo "[PREFLIGHT-COMPILATION - FAIL] ❌ The project did not compile successfully! Please try building it again and make sure your changes are working!"; exit 1; fi 
preflight-warnings: clean-silent
	@echo "[PREFLIGHT-WARNINGS - INFO] 💬 Checking if there are any Warnings during compile-time..."
	@if dotnet build /WarnAsError | grep -q "Build succeeded."; then echo "[PREFLIGHT-WARNINGS - PASS] ✅ Project does not produce any warnings."; else echo "[PREFLIGHT-WARNINGS - WARN] ❓ The project produces compiler warnings. Please review the warnings using 'make clean' and 'make build' before opening a pull request!"; fi 

