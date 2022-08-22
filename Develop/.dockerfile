ARG REPO=mcr.microsoft.com/dotnet/aspnet
FROM $REPO:6.0.8-bullseye-slim-amd64 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

COPY ["/Meli.Host.Api/Meli.Host.Api.csproj", "Meli.Host.Api/"]
COPY ["/Meli.Core/Meli.Core.csproj", "Meli.Core/"]
COPY ["/Meli.DataAccess/Meli.DataAccess.csproj", "Meli.DataAccess/"]
COPY ["/Meli.Entities/Meli.Entities.csproj", "Meli.Entities/"]
COPY ["/Meli.Proxies/Meli.Proxies.csproj", "Meli.Proxies/"]
COPY ["/Meli.Processor/Meli.Processor.csproj", "Meli.Processor/"]
COPY ["/Meli.Repository/Meli.Repository.csproj", "Meli.Repository/"]
RUN dotnet restore "Meli.Host.Api/Meli.Host.Api.csproj"
COPY . .
WORKDIR "/src/Meli.Host.Api"
RUN dotnet build "Meli.Host.Api.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Meli.Host.Api.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Meli.Host.Api.dll"]