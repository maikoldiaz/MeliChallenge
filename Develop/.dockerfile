FROM mcr.microsoft.com/dotnet/aspnet:6.0 as base
WORKDIR /app
EXPOSE 80
ENV ASPNETCORE_ENVIRONMENT=Development
    
# Copy csproj and restore as distinct layers
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
##--self-contained --runtime linux-x64
# RUN dotnet publish /Meli.Host.Api/Meli.Host.Api.csproj -c Release -r linux-64 -o /app
# RUN dotnet publish /Meli.Host.Api/Meli.Host.Api.csproj -c Release -o out
    
# Build runtime image
FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Meli.Host.Api.dll"]