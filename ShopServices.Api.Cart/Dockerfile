FROM mcr.microsoft.com/dotnet/core/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80


FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["ShopServices.Api.Cart/ShopServices.Api.Cart.csproj", "ShopServices.Api.Cart/"]
RUN dotnet restore "ShopServices.Api.Cart/ShopServices.Api.Cart.csproj"
COPY . .
WORKDIR "/src/ShopServices.Api.Cart"
RUN dotnet build "ShopServices.Api.Cart/ShopServices.Api.Cart.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ShopServices.Api.Cart.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ShopServices.Api.Cart.dll"]
