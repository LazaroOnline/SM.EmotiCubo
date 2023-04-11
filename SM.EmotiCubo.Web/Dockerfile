FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 8089

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY *.sln ./
COPY SM.EmotiCubo.Web/SM.EmotiCubo.Web.csproj SM.EmotiCubo.Web/
RUN dotnet restore --use-current-runtime
COPY . .
WORKDIR /src/SM.EmotiCubo.Web
RUN dotnet build -c Release -o /app

FROM build AS publish
RUN dotnet publish --use-current-runtime --no-restore -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "SM.EmotiCubo.Web.dll"]
