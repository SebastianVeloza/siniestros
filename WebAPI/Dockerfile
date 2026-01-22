# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copiar csproj y restaurar
COPY *.sln .
COPY src/Application/*.csproj src/Application/
COPY src/Domain/*.csproj src/Domain/
COPY src/Infrastructure/*.csproj src/Infrastructure/
COPY src/WebAPI/*.csproj src/WebAPI/

RUN dotnet restore

# Copiar todo el código
COPY . .

# Build
RUN dotnet publish src/WebAPI/WebAPI.csproj \
    -c Release \
    -o /app/publish \
    /p:UseAppHost=false


FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

COPY --from=build /app/publish .

EXPOSE 8080

ENTRYPOINT ["dotnet", "WebAPI.dll"]
