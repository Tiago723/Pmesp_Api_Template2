#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Pmesp_Api_Template2.Api/Pmesp_Api_Template2.Api.csproj", "Pmesp_Api_Template2.Api/"]
RUN dotnet restore "Pmesp_Api_Template2.Api/Pmesp_Api_Template2.Api.csproj"
COPY . .
WORKDIR "/src/Pmesp_Api_Template2.Api"
RUN dotnet build "Pmesp_Api_Template2.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Pmesp_Api_Template2.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Pmesp_Api_Template2.Api.dll"]