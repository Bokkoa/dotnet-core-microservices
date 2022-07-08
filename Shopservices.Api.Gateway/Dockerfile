FROM mcr.microsoft.com/dotnet/core/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["ShopServices.Api.Gateway/ShopServices.Api.Gateway.csproj", "ShopServices.Api.Gateway/"]
RUN dotnet restore "ShopServices.Api.Gateway/ShopServices.Api.Gateway.csproj"
COPY . .
WORKDIR "/src/ShopServices.Api.Gateway"
RUN dotnet build "ShopServices.Api.Gateway/ShopServices.Api.Gateway.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ShopServices.Api.Gateway.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ShopServices.Api.Gateway.dll"]
