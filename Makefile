buildDirectory := bin
defaultOptions := 
compiler := dotnet
SHELL := /bin/bash

default:
	$(compiler) build $(defaultOptions) -o $(buildDirectory)

clean:
	rm -rf $(buildDirectory)/*

run: 
	$(compiler) run	
