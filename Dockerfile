  
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY . .
RUN dotnet restore "Vrum.BFF/Vrum.BFF.csproj"
COPY ./Vrum.BFF ./Vrum.BFF
WORKDIR "/src/Vrum.BFF"
RUN dotnet build "Vrum.BFF.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Vrum.BFF.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

RUN useradd -m myappuser
USER myappuser

CMD ASPNETCORE_URLS="http://*:$PORT" dotnet Vrum.BFF.dll