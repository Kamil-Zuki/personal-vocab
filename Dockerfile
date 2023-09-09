#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["personal-vocab.API/personal-vocab.API.csproj", "personal-vocab.API/"]
COPY ["personal-vocab.DAL/personal-vocab.DAL.csproj", "personal-vocab.DAL/"]
RUN dotnet restore "personal-vocab.API/personal-vocab.API.csproj"
COPY . .
WORKDIR "/src/personal-vocab.API"
RUN dotnet build "personal-vocab.API.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "personal-vocab.API.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "personal-vocab.API.dll"]