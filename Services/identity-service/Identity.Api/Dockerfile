FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Services/Identity.Services/Identity.Api/Identity.Api.csproj", "Services/Identity.Services/Identity.Api/"]
COPY ["Services/Identity.Services/Identity.Applications/Identity.Applications.csproj", "Services/Identity.Services/Identity.Applications/"]
COPY ["Services/Identity.Services/Identity.Domain/Identity.Domain.csproj", "Services/Identity.Services/Identity.Domain/"]
COPY ["Share-kernel/SharedKernel.Domain/SharedKernel.Domain.csproj", "Share-kernel/SharedKernel.Domain/"]
COPY ["Services/Identity.Services/Identity.Infrastucture/Identity.Infrastucture.csproj", "Services/Identity.Services/Identity.Infrastucture/"]
RUN dotnet restore "Services/Identity.Services/Identity.Api/Identity.Api.csproj"
COPY . .
WORKDIR "/src/Services/Identity.Services/Identity.Api"
RUN dotnet build "./Identity.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Identity.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Identity.Api.dll"]
