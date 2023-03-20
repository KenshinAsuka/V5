#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["V5.csproj", "."]
RUN dotnet restore "./V5.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "V5.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "V5.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "V5.dll"]