FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/CrackHash.Worker/CrackHash.Worker.csproj", "CrackHash.Worker/"]
RUN dotnet restore "CrackHash.Worker/CrackHash.Worker.csproj"
COPY . .
WORKDIR "/src/CrackHash.Worker"
RUN dotnet build "CrackHash.Worker.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CrackHash.Worker.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CrackHash.Worker.dll"]