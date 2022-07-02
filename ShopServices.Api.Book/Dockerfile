FROM mcr.microsoft.com/dotnet/core/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["ShopServices.Api.Book/ShopServices.Api.Book.csproj", "ShopServices.Api.Book/"]
RUN dotnet restore "ShopServices.Api.Book/ShopServices.Api.Book.csproj"
COPY . .
WORKDIR "/src/ShopServices.Api.Book"
RUN dotnet build "ShopServices.Api.Book/ShopServices.Api.Book.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ShopServices.Api.Book.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ShopServices.Api.Book.dll"]
