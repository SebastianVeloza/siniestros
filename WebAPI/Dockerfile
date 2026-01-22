
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copiar TODO el repositorio
COPY . .

# Restaurar usando la solución
RUN dotnet restore siniestros_viales.sln

# Publicar WebAPI
RUN dotnet publish WebAPI/WebAPI.csproj \
    -c Release \
    -o /app/publish \
    /p:UseAppHost=false


FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

COPY --from=build /app/publish .

EXPOSE 8080

ENTRYPOINT ["dotnet", "WebAPI.dll"]
