# Container image for Smoke.Web. Build from the repo root:
#   docker build -t smoke-web .
FROM mcr.microsoft.com/dotnet/sdk:11.0-preview AS build
WORKDIR /src
COPY global.json Directory.Build.props Directory.Build.targets Directory.Packages.props nuget.config ./
COPY src/Smoke.Library/ src/Smoke.Library/
COPY src/Smoke.Web/ src/Smoke.Web/
RUN dotnet publish src/Smoke.Web -c Release -o /app

FROM mcr.microsoft.com/dotnet/aspnet:11.0-preview
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "Smoke.Web.dll"]
