# FROM mcr.microsoft.com/dotnet/core/sdk:3.1 as build

# ARG BUILDCONFIG=REALEASE
# ARG VERSION=1.0.0

# COPY ResSys.FilmCatalog.csproj /build/

# RUN dotnet restore ./build/ResSys.FilmCatalog.csproj

# COPY . ./build/
# WORKDIR /build/
# RUN dotnet publish ./ResSys.FilmCatalog.csproj -c $BUILDCONFIG -o out /p:Version=$VERSION

# FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
# WORKDIR /app

# COPY --from=build /build/out .

# ENTRYPOINT ["dotnet", "ResSys.FilmCatalog.dll"]



FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build-env
WORKDIR /app


COPY *.csproj ./
RUN dotnet restore


COPY . ./
RUN dotnet publish -c Release -o out


FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "ResSys.AuthorCatalog.dll"]