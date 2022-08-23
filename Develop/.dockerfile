FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /app
    
# Copy csproj and restore as distinct layers
COPY ["/Meli.Host.Api/Meli.Host.Api.csproj", "Meli.Host.Api/"]
COPY ["/Meli.Core/Meli.Core.csproj", "Meli.Core/"]
COPY ["/Meli.DataAccess/Meli.DataAccess.csproj", "Meli.DataAccess/"]
COPY ["/Meli.Entities/Meli.Entities.csproj", "Meli.Entities/"]
COPY ["/Meli.Proxies/Meli.Proxies.csproj", "Meli.Proxies/"]
COPY ["/Meli.Processor/Meli.Processor.csproj", "Meli.Processor/"]
COPY ["/Meli.Repository/Meli.Repository.csproj", "Meli.Repository/"]
RUN dotnet restore "Meli.Host.Api/Meli.Host.Api.csproj"
    
# Copy everything else and build
# COPY ../engine/examples ./
# RUN dotnet publish -c Release -o out
RUN dotnet publish Meli.Host.Api/Meli.Host.Api.csproj -c Release -o /app -r arch-x64
# RUN dotnet publish /Meli.Host.Api/Meli.Host.Api.csproj -c Release -r linux-64 -o /app
# RUN dotnet publish /Meli.Host.Api/Meli.Host.Api.csproj -c Release -o out
    
# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "Meli.Host.Api.dll"]