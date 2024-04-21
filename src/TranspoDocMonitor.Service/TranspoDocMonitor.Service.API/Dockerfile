#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["TranspoDocMonitor.Service.API/TranspoDocMonitor.Service.API.csproj", "TranspoDocMonitor.Service.API/"]
COPY ["TranspoDocMonitor.Service.Contracts.Exceptions/TranspoDocMonitor.Service.Contracts.Exceptions.csproj", "TranspoDocMonitor.Service.Contracts.Exceptions/"]
COPY ["TranspoDocMonitor.Service.Core.Exception/TranspoDocMonitor.Service.Core.Exception.csproj", "TranspoDocMonitor.Service.Core.Exception/"]
COPY ["TranspoDocMonitor.Service.Contracts.Shared/TranspoDocMonitor.Service.Contracts.Shared.csproj", "TranspoDocMonitor.Service.Contracts.Shared/"]
COPY ["TranspoDocMonitor.Service.Domain/TranspoDocMonitor.Service.Domain.csproj", "TranspoDocMonitor.Service.Domain/"]
COPY ["TranspoDocMonitor.Service.Contracts/TranspoDocMonitor.Service.Contracts.csproj", "TranspoDocMonitor.Service.Contracts/"]
COPY ["TranspoDocMonitor.Service.Core.BackgroundJob/TranspoDocMonitor.Service.Core.BackgroundJob.csproj", "TranspoDocMonitor.Service.Core.BackgroundJob/"]
COPY ["TranspoDocMonitor.Service.Core.Notification/TranspoDocMonitor.Service.Core.Notification.csproj", "TranspoDocMonitor.Service.Core.Notification/"]
COPY ["TranspoDocMonitor.Service.DataContext.DataAccess/TranspoDocMonitor.Service.DataContext.DataAccess.csproj", "TranspoDocMonitor.Service.DataContext.DataAccess/"]
COPY ["TranspoDocMonitor.Service.DataContext/TranspoDocMonitor.Service.DataContext.csproj", "TranspoDocMonitor.Service.DataContext/"]
COPY ["TranspoDocMonitor.Service.Core.Authorization/TranspoDocMonitor.Service.Core.Authorization.csproj", "TranspoDocMonitor.Service.Core.Authorization/"]
COPY ["TranspoDocMonitor.Service.Core.HTTP/TranspoDocMonitor.Service.Core.HTTP.csproj", "TranspoDocMonitor.Service.Core.HTTP/"]
COPY ["TranspoDocMonitor.Service.Core.Swagger/TranspoDocMonitor.Service.Core.Swagger.csproj", "TranspoDocMonitor.Service.Core.Swagger/"]
COPY ["TranspoDocMonitor.Service.HTTP.Handlers/TranspoDocMonitor.Service.HTTP.Handlers.csproj", "TranspoDocMonitor.Service.HTTP.Handlers/"]
COPY ["TranspoDocMonitor.Service.Middlewares/TranspoDocMonitor.Service.Middlewares.csproj", "TranspoDocMonitor.Service.Middlewares/"]
RUN dotnet restore "./TranspoDocMonitor.Service.API/./TranspoDocMonitor.Service.API.csproj"
COPY . .
WORKDIR "/src/TranspoDocMonitor.Service.API"
RUN dotnet build "./TranspoDocMonitor.Service.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./TranspoDocMonitor.Service.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TranspoDocMonitor.Service.API.dll"]