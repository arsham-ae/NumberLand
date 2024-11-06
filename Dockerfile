FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["NumberLand/NumberLand.csproj","NumberLand.Api/"]
COPY ["NumberLand.DataAccess/NumberLand.DataAccess.csproj","NumberLand.DataAccess/"]
COPY ["NumberLand.Models/NumberLand.Models.csproj","NumberLand.Models/"]
COPY ["NumberLand.Utility/NumberLand.Utility.csproj","NumberLand.Utility/"]
RUN dotnet restore "NumberLand.Api/NumberLand.csproj"
COPY . .
WORKDIR "/src/NumberLand.Api"
RUN dotnet build "NumberLand.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "NumberLand.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT [ "dotnet","NumberLand.Api.dll" ]