FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY Mov.Domain/Mov.Domain.csproj Mov.Domain/
COPY Mov.Application/Mov.Application.csproj Mov.Application/
COPY Mov.Infrastructure/Mov.Infrastructure.csproj Mov.Infrastructure/
COPY Mov.Api/Mov.Api.csproj Mov.Api/
RUN dotnet restore Mov.Api/Mov.Api.csproj

COPY . .
RUN dotnet publish Mov.Api/Mov.Api.csproj \
    --configuration Release \
    --no-restore \
    --output /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app

ENV ASPNETCORE_ENVIRONMENT=Production
ENV ASPNETCORE_URLS=http://+:8080

EXPOSE 8080

COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Mov.Api.dll"]
