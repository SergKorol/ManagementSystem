FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/ShopManagementSystem/ShopManagementSystem.Dashboard/ShopManagementSystem.Dashboard.csproj", "src/ShopManagementSystem/ShopManagementSystem.Dashboard/"]
COPY ["src/ShopManagementSystem/ShopManagementSystem.Application/ShopManagementSystem.Application.csproj", "src/ShopManagementSystem/ShopManagementSystem.Application/"]
COPY ["src/ShopManagementSystem/ShopManagementSystem.Data/ShopManagementSystem.Data.csproj", "src/ShopManagementSystem/ShopManagementSystem.Data/"]
COPY ["src/ShopManagementSystem/ShopManagementSystem.Services/ShopManagementSystem.Services.csproj", "src/ShopManagementSystem/ShopManagementSystem.Services/"]
COPY ["src/ShopManagementSystem/ShopManagementSystem.UnitTests/ShopManagementSystem.UnitTests.csproj", "src/ShopManagementSystem/ShopManagementSystem.UnitTests/"]
RUN dotnet restore "src/ShopManagementSystem/ShopManagementSystem.Dashboard/ShopManagementSystem.Dashboard.csproj"
COPY ["src/ShopManagementSystem/ShopManagementSystem.sln", "src/ShopManagementSystem/ShopManagementSystem.sln"]
RUN dotnet restore "src/ShopManagementSystem/ShopManagementSystem.sln"
COPY . .
WORKDIR "src/ShopManagementSystem/ShopManagementSystem.Dashboard"
RUN dotnet build "ShopManagementSystem.Dashboard.csproj" -c Release -o /app/build

FROM build AS application
WORKDIR "src/ShopManagementSystem/ShopManagementSystem.Application"

FROM build AS data
WORKDIR "src/ShopManagementSystem/ShopManagementSystem.Data"

FROM build AS services
WORKDIR "src/ShopManagementSystem/ShopManagementSystem.Services"

FROM build AS application
WORKDIR "src/ShopManagementSystem/ShopManagementSystem.UnitTests"

FROM build AS publish
RUN dotnet publish "ShopManagementSystem.Dashboard.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY . .
ENTRYPOINT ["dotnet", "ShopManagementSystem.Dashboard.dll"]
