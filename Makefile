buildDirectory := bin
installDirectory := /opt/AnonKey/
publishDirectory := publish
defaultOptions := 
compiler := dotnet
SHELL := /bin/bash

default:
	$(compiler) build $(defaultOptions) -o $(buildDirectory)

clean:
	rm -rf $(buildDirectory)
	rm -rf $(publishDirectory)

run: 
	$(compiler) run	

publish:
	$(compiler) publish -o $(publishDirectory)

install: publish
	rm -rf $(installDirectory)
	if [[ ! -d $(installDirectory) ]]; then mkdir $(installDirectory); fi
	cp -r publish/* $(installDirectory) 
	cp AnonKey.service /etc/systemd/system/
	systemctl enable AnonKey.service
	echo "Installed AnonKey to /opt/AnonKey. Start Systemd Unit AnonKey.service to run the application!"
	echo "The application uses TCP port 5000."

install-debug: publish 
	rm -rf $(installDirectory)
	if [[ ! -d $(installDirectory) ]]; then mkdir $(installDirectory); fi
	cp -r $(buildDirectory)/* $(installDirectory) 
	cp AnonKey-Debug.service /etc/systemd/system/
	systemctl enable AnonKey-Debug.service
	echo "Installed debug version of AnonKey to /opt/AnonKey. Start Systemd Unit AnonKey.service to run the application!"
	echo "The application uses TCP port 5000."

uninstall:
	systemctl stop AnonKey.service
	systemctl disable AnonKey.service
	rm /etc/systemd/system/AnonKey.service
	systemctl stop AnonKey-Debug.service
	systemctl disable AnonKey-Debug.service
	rm /etc/systemd/system/AnonKey-Debug.service
	rm -rf $(installDirectory)
