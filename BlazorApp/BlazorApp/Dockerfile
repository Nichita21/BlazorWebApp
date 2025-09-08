# Etapa de bază pentru runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Etapa de build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copierea proiectelor relative la contextul root
COPY ["BlazorApp/BlazorApp/BlazorApp.csproj", "BlazorApp/BlazorApp/"]
COPY ["BlazorApp/BlazorApp.Client/BlazorApp.Client.csproj", "BlazorApp/BlazorApp.Client/"]

# Restaurare pachete
RUN dotnet restore "BlazorApp/BlazorApp/BlazorApp.csproj"

# Copierea întregului cod sursă
COPY . .

WORKDIR "/src/BlazorApp/BlazorApp"

# Build proiect
RUN dotnet build "BlazorApp.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publish proiect
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "BlazorApp.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Etapa finală
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BlazorApp.dll"]
