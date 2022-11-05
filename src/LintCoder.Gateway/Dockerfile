#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 5003
EXPOSE 5004

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["LintCoder.Gateway/LintCoder.Gateway.csproj", "LintCoder.Gateway/"]
RUN dotnet restore "LintCoder.Gateway/LintCoder.Gateway.csproj"
COPY . .
WORKDIR "/src/LintCoder.Gateway"
RUN dotnet build "LintCoder.Gateway.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "LintCoder.Gateway.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LintCoder.Gateway.dll"]