FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY ["Obourreal.Identity.Api/Obourreal.Identity.Api.csproj", "Obourreal.Identity.Api/"]
RUN dotnet restore "Obourreal.Identity.Api/Obourreal.Identity.Api.csproj"
COPY . .
WORKDIR "/src/Obourreal.Identity.Api"
RUN dotnet build "Obourreal.Identity.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Obourreal.Identity.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Obourreal.Identity.Api.dll"]
