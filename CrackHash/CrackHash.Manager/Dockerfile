FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["CrackHash.Manager/CrackHash.Manager.csproj", "CrackHash.Manager/"]
RUN dotnet restore "CrackHash.Manager/CrackHash.Manager.csproj"
COPY . .
WORKDIR "/src/CrackHash.Manager"
RUN dotnet build "CrackHash.Manager.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "CrackHash.Manager.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CrackHash.Manager.dll"]
