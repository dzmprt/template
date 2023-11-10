FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

COPY ["UM/UM.Api/UM.Api.csproj", "UM/UM.Api/"]

RUN dotnet restore "UM/UM.Api/UM.Api.csproj"

COPY . .
WORKDIR "/src/UM/UM.Api/"
RUN dotnet build "UM.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "UM.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "UM.Api.dll"]