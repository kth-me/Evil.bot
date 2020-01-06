@echo off
@title .NET Build & Run Tool: Evil.bot

:BeginProcess
timeout /t 3

cd /d "..\src"
dotnet restore
dotnet build --configuration Release
cd /d "..\src\Evil.bot.ConsoleApp"
dotnet run --configuration Release

goto BeginProcess