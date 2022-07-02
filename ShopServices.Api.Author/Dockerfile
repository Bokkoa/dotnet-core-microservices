FROM mcr.microsoft.com/dotnet/core/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80


FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["ShopServices.Api.Author/ShopServices.Api.Author.csproj", "ShopServices.Api.Author/"]
RUN dotnet restore "ShopServices.Api.Author/ShopServices.Api.Author.csproj"
COPY . .
WORKDIR "/src/ShopServices.Api.Author"
RUN dotnet build "ShopServices.Api.Author/ShopServices.Api.Author.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ShopServices.Api.Author.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ShopServices.Api.Author.dll"]
