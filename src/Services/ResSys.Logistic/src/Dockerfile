#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
# COPY ["./ResSys.Logistic.UI.WebApi.csproj", "ResSys.Logistic.UI.WebApi/"]
COPY [".", "."]
RUN dotnet restore "ResSys.Logistic.UI.WebApi/ResSys.Logistic.UI.WebApi.csproj"
COPY . .
WORKDIR "/src/ResSys.Logistic.UI.WebApi"
RUN dotnet build "ResSys.Logistic.UI.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ResSys.Logistic.UI.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ResSys.Logistic.UI.WebApi.dll"]