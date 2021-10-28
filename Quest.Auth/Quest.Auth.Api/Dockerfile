#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
#EXPOSE 80
#EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Quest.Auth/Quest.Auth.Api/Quest.Auth.Api.csproj", "Quest.Auth/Quest.Auth.Api/"]
RUN dotnet restore "Quest.Auth/Quest.Auth.Api/Quest.Auth.Api.csproj"
COPY . .
WORKDIR "/src/Quest.Auth/Quest.Auth.Api"
RUN dotnet build "Quest.Auth.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Quest.Auth.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
#ENTRYPOINT ["dotnet", "Quest.Auth.Api.dll"]
CMD ASPNETCORE_URLS=http://*:$PORT dotnet Quest.Auth.Api.dll