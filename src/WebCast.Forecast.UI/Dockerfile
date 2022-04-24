#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/WebCast.Forecast.UI/WebCast.Forecast.UI.csproj", "src/WebCast.Forecast.UI/"]
COPY ["src/WebCast.Forecast.Application/WebCast.Forecast.Application.csproj", "src/WebCast.Forecast.Application/"]
COPY ["src/WebCast.Forecast.Domain/WebCast.Forecast.Domain.csproj", "src/WebCast.Forecast.Domain/"]
COPY ["src/WebCast.Forecast.Infra/WebCast.Forecast.Infra.csproj", "src/WebCast.Forecast.Infra/"]
RUN dotnet restore "src/WebCast.Forecast.UI/WebCast.Forecast.UI.csproj"
COPY . .
WORKDIR "/src/src/WebCast.Forecast.UI"
RUN dotnet build "WebCast.Forecast.UI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WebCast.Forecast.UI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebCast.Forecast.UI.dll"]