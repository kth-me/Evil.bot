FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /app/

COPY src/Evilbot.ConsoleApp/*.csproj ./
RUN dotnet restore

COPY src/Evilbot.ConsoleApp/. ./
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/core/runtime:3.1 AS runtime
WORKDIR /app
COPY --from=build /app/out ./
VOLUME [ "/app/Data" ]
ENTRYPOINT ["dotnet", "Evilbot.ConsoleApp.dll"]