FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["src/WebApplication/WebApplication.csproj", "src/WebApplication/"]
RUN dotnet restore "src/WebApplication/WebApplication.csproj"
COPY . .
WORKDIR "/src/src/WebApplication"
RUN dotnet build "WebApplication.csproj" -c Release -o /app/build

FROM build AS publish

RUN apt-get -o Acquire::Check-Valid-Until=false update -yq && apt-get -o Acquire::Check-Valid-Until=false upgrade -yq && apt-get -o Acquire::Check-Valid-Until=false install -yq curl git nano
RUN curl -sL https://deb.nodesource.com/setup_10.x | bash - && apt-get -o Acquire::Check-Valid-Until=false install -yq nodejs build-essential

RUN dotnet publish "WebApplication.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebApplication.dll"]