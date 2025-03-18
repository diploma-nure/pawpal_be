FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["PawPal/Web/Web.csproj", "PawPal/Web/"]
COPY ["PawPal/Application/*.csproj", "PawPal/Application/"]
COPY ["PawPal/Domain/*.csproj", "PawPal/Domain/"]
COPY ["PawPal/Infrastructure/*.csproj", "PawPal/Infrastructure/"]
RUN dotnet restore "PawPal/Web/Web.csproj"
COPY . .
WORKDIR "/src/PawPal/Web"
RUN dotnet build "Web.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Web.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Web.dll"]