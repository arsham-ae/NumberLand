FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY *.sln ./
COPY ["NumberLand/NumberLand.csproj","NumberLand/"]
COPY ["NumberLand.DataAccess/NumberLand.DataAccess.csproj","NumberLand.DataAccess/"]
COPY ["NumberLand.Models/NumberLand.Models.csproj","NumberLand.Models/"]
COPY ["NumberLand.Utility/NumberLand.Utility.csproj","NumberLand.Utility/"]
RUN dotnet restore "NumberLand/NumberLand.csproj"

COPY . .
WORKDIR "/app/NumberLand"
RUN dotnet build -c Release -o /app/build

RUN dotnet publish -c Release -o /app/publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

COPY --from=build /app/publish .

EXPOSE 8080
EXPOSE 443

ENTRYPOINT [ "dotnet","NumberLand.dll" ]