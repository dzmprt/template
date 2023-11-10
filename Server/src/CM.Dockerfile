FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base

RUN sed -i'.bak' 's/$/ contrib/' /etc/apt/sources.list
RUN apt-get update; apt-get install -y ttf-mscorefonts-installer fontconfig

WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

COPY ["CM/CM.Api/CM.Api.csproj", "CM/CM.Api/"]

RUN dotnet restore "CM/CM.Api/CM.Api.csproj"

COPY . .
WORKDIR "/src/CM/CM.Api/"
RUN dotnet build "CM.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CM.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CM.Api.dll"]