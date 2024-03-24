# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
# Copy both .csproj files into the Docker image
COPY ["personal-vocab.API/personal-vocab.API.csproj", "personal-vocab.API/"]
COPY ["personal-vocab.DAL/personal-vocab.DAL.csproj", "personal-vocab.DAL/"]
# Restore the projects
RUN dotnet restore "personal-vocab.API/personal-vocab.API.csproj"
# Copy the rest of the application files
COPY . .
# Build the personal-vocab.API project
WORKDIR "/src/personal-vocab.API"
RUN dotnet build "personal-vocab.API.csproj" -c Release -o /app/build

FROM build AS publish
# Publish the personal-vocab.API project
RUN dotnet publish "personal-vocab.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
# Copy the published output from the publish stage
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "personal-vocab.API.dll"]
