#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["Sunstone.API/Sunstone.API.csproj", "Sunstone.API/"]
RUN dotnet restore "Sunstone.API/Sunstone.API.csproj"
COPY . .
WORKDIR "/src/Sunstone.API"
RUN dotnet build "Sunstone.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Sunstone.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Sunstone.API.dll"]