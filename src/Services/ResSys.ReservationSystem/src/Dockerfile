#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
# COPY ["./ResSys.ReservationSystem.WebApi.csproj", "ResSys.ReservationSystem.WebApi/"]
COPY [".", "."]
RUN dotnet restore "Outer/Presentation/ResSys.ReservationSystem.WebApi/ResSys.ReservationSystem.WebApi.csproj"
COPY . .
WORKDIR "/src/Outer/Presentation/ResSys.ReservationSystem.WebApi"
RUN dotnet build "ResSys.ReservationSystem.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ResSys.ReservationSystem.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ResSys.ReservationSystem.WebApi.dll"]