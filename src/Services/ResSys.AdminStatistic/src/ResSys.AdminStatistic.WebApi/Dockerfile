#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["./ResSys.AdminStatistic.WebApi.csproj", "ResSys.AdminStatistic.WebApi/"]
RUN dotnet restore "ResSys.AdminStatistic.WebApi/ResSys.AdminStatistic.WebApi.csproj"
COPY . .
WORKDIR "/src/ResSys.AdminStatistic.WebApi"
RUN dotnet build "ResSys.AdminStatistic.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ResSys.AdminStatistic.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ResSys.AdminStatistic.WebApi.dll"]