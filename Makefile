buildDirectory := ./bin/
installDirectory := /opt/AnonKey/
publishDirectory := ./publish/
defaultOptions := 
compiler := dotnet
SHELL := /bin/bash

# 
# Build the project to $(buildDirectory) in default (usually Debug) mode.
# 
default:
	$(compiler) build $(defaultOptions) -o $(buildDirectory)

#
# Clean up any auxiliary build files residing in $(buildDirectory) or in $(publishDirectory)
# 
clean:
	rm -rf $(buildDirectory)
	rm -rf $(publishDirectory)

#
# Execute `make clean` and, in addition to that, also delete the current database.
#
wipe: clean
	if [[ -f database.db ]]; then rm database.db; fi

#
# Run the dotnet application on port 5000, and build it if necessary.
#
run: 
	ASPNETCORE_HTTP_PORTS=5000 $(compiler) run	

#
# Run the dotnet appliation on alternate port 5100, and build it if necessary.
#
run_alt:
	ASPNETCORE_HTTP_PORTS=5100 $(compiler) run	

#
# Publish the dotnet application to $(publishDirecotry).
#
publish:
	$(compiler) publish -o $(publishDirectory)


#
# Install the application to $(installDirectory). 
# WARNING: This will run `make wipe`, `make uninstall` and `make publish` accordingly!
#
install: wipe uninstall publish
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
	if [[ -f /etc/systemd/system/AnonKey.service ]]; then systemctl is-active AnonKey.Service && systemctl stop AnonKey.service; systemctl disable AnonKey.service; rm /etc/systemd/system/AnonKey.service; fi
	if [[ -f /etc/systemd/system/AnonKey-Debug.service ]]; then systemctl is-active AnonKey.Service && systemctl stop AnonKey-Debug.service; systemctl disable AnonKey-Debug.service; rm /etc/systemd/system/AnonKey-Debug.service; fi
	rm -rf $(installDirectory)

#
# Build the documentation using doxygen to ./docs/
#
.PHONY: docs
docs:
	doxygen doxygen.conf
