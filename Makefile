buildDirectory := ./bin/
installDirectory := /opt/AnonKey/
publishDirectory := ./publish/
defaultOptions := 
compiler := dotnet
projectName := AnonKey-Backend
SHELL := /bin/bash

# 
# Build the project to $(buildDirectory) in default (usually Debug) mode.
# 
build:
	@echo "[BUILD - INFO] ✅ Attempting to build $(projectName) to $(buildDirectory)..."
	$(compiler) build $(defaultOptions) -o $(buildDirectory)

#
# Clean up any auxiliary build files residing in $(buildDirectory) or in $(publishDirectory)
# 
clean:
	@echo "[CLEAN - INFO] ✅ Deleting the contents of $(publishDirectory) and $(buildDirectory)."
	rm -rf $(buildDirectory)
	rm -rf $(publishDirectory)

#
# Execute `make clean` and, in addition to that, also delete the current database.
#
wipe: clean
	@echo "[WIPE - INFO] ✅ Deleting database.db."
	if [[ -f database.db ]]; then rm database.db; fi

#
# Run the dotnet application on port 5000, and build it if necessary.
#
run: build 
	@echo "[RUN - INFO] ✅ Starting the application on port 5000..."
	ASPNETCORE_HTTP_PORTS=5000 $(compiler) run	

#
# Run the dotnet appliation on alternate port 5100, and build it if necessary.
#
run_alt: build
	@echo "[RUN_ALT - INFO] ✅ Starting the application on port 5100..."
	ASPNETCORE_HTTP_PORTS=5100 $(compiler) run	

#
# Publish the dotnet application to $(publishDirecotry).
#
publish:
	@echo "[PUBLISH - INFO] ✅ Attempting to publish $(projectName) to $(buildDirectory)..."
	$(compiler) publish -o $(publishDirectory)


#
# Install the application to $(installDirectory). 
# WARNING: This will run `make wipe`, `make uninstall` and `make publish` accordingly!
#
install: wipe uninstall publish
	@echo "[INSTALL - INFO] ✅ Attempting to install $(projectName) to $(installDirectory) ..."
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
	@echo "[INSTALL-DEBUG - INFO] ✅ Attempting to install debug version of $(projectName) to $(installDirectory) ..."
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
	@echo "[UNINSTALL - INFO] ✅ Attempting to uninstall $(projectName) from $(installDirectory) ..."
	if [[ -f /etc/systemd/system/AnonKey.service ]]; then systemctl is-active AnonKey.Service && systemctl stop AnonKey.service; systemctl disable AnonKey.service; rm /etc/systemd/system/AnonKey.service; fi
	if [[ -f /etc/systemd/system/AnonKey-Debug.service ]]; then systemctl is-active AnonKey.Service && systemctl stop AnonKey-Debug.service; systemctl disable AnonKey-Debug.service; rm /etc/systemd/system/AnonKey-Debug.service; fi
	rm -rf $(installDirectory)

#
# Build the documentation using doxygen to ./docs/
#
.PHONY: docs
docs:
	@echo "[DOCS - INFO] ✅ Attempting to build $(projectName) documentation..."
	doxygen doxygen.conf

### The following profiles are project-specific preflight checks used to ensure consistent code.

preflight-pull:
	@echo "[PREFLIGHT-PULL - INFO] ✅ Running Preflight checks for a Pull Request..."
	@echo "[PREFLIGHT-PULL - INFO] 💬 Checking for uncommited changes..."

