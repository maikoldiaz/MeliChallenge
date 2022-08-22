FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

# Install last version of bash (5.1)
RUN echo "deb http://deb.debian.org/debian bullseye main" | tee -a /etc/apt/sources.list
RUN apt update
RUN apt-get install bash -y
RUN bash --version 

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Develop/Meli.Host.Api/Meli.Host.Api.csproj", "Meli.Host.Api/"]
COPY ["Develop/Meli.Core/Meli.Core.csproj", "Meli.Core/"]
COPY ["Develop/Meli.DataAccess/Meli.DataAccess.csproj", "Meli.DataAccess/"]
COPY ["Develop/Meli.Entities/Meli.Entities.csproj", "Meli.Entities/"]
COPY ["Develop/Meli.Entities/Meli.Entities.csproj", "Meli.Entities/"]
COPY ["Develop/Meli.Proxies/Meli.Proxies.csproj", "Meli.Proxies/"]
COPY ["Develop/Meli.Processor/Meli.Processor.csproj", "Meli.Processor/"]
RUN dotnet restore "Meli.Host.Api/Meli.Host.Api.csproj"
COPY . .
WORKDIR "/src/Develop/Meli.Host.Api"
RUN dotnet build "Meli.Host.Api.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Meli.Host.Api.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Meli.Host.Api.dll"]