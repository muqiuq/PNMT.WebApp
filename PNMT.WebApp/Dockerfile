#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["PNMT.WebApp/PNMT.WebApp/PNMT.WebApp.csproj", "PNMT.WebApp/PNMT.WebApp/"]
COPY ["PNMTD/PNMT.ApiClient/PNMT.ApiClient.csproj", "PNMTD/PNMT.ApiClient/"]
COPY ["PNMTD/PNMTD.Lib/PNMTD.Lib.csproj", "PNMTD/PNMTD.Lib/"]
RUN dotnet restore "PNMT.WebApp/PNMT.WebApp/PNMT.WebApp.csproj"
COPY . .
WORKDIR "/src/PNMT.WebApp/PNMT.WebApp"
RUN dotnet build "PNMT.WebApp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PNMT.WebApp.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PNMT.WebApp.dll"]