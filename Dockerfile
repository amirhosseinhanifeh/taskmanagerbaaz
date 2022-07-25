#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["nuget.config", "."]
COPY ["MS.Services.TaskCatalog.Api/MS.Services.TaskCatalog.Api.csproj", "MS.Services.TaskCatalog.Api/"]
COPY ["MS.Services.TaskCatalog.Rest/MS.Services.TaskCatalog.Rest.csproj", "MS.Services.TaskCatalog.Rest/"]
COPY ["MS.Services.TaskCatalog.Contract/MS.Services.TaskCatalog.Contract.csproj", "MS.Services.TaskCatalog.Contract/"]
COPY ["MS.Services.TaskCatalog.Domain/MS.Services.TaskCatalog.Domain.csproj", "MS.Services.TaskCatalog.Domain/"]
COPY ["MS.Services.TaskCatalog.Infrastructure/MS.Services.TaskCatalog.Infrastructure.csproj", "MS.Services.TaskCatalog.Infrastructure/"]
COPY ["MS.Services.TaskCatalog.Application/MS.Services.TaskCatalog.Application.csproj", "MS.Services.TaskCatalog.Application/"]
RUN dotnet restore "MS.Services.TaskCatalog.Api/MS.Services.TaskCatalog.Api.csproj"
COPY . .
WORKDIR "/src/MS.Services.TaskCatalog.Api"
RUN dotnet build "MS.Services.TaskCatalog.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MS.Services.TaskCatalog.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MS.Services.TaskCatalog.Api.dll"]
