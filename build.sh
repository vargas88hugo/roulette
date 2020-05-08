#!/usr/bin/env bash
cd ./RouletteAPI && dotnet publish -c Release
cd .. & docker-compose up --build