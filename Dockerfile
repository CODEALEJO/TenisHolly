# Usa la imagen oficial de .NET 8
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["TuProyecto.csproj", "./"]
RUN dotnet restore "./TuProyecto.csproj"

COPY . .
WORKDIR "/src"
RUN dotnet publish "./TuProyecto.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "TuProyecto.dll"]
