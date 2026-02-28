# Build stage
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY *.sln .
COPY IntelliBudgetApi/*.csproj IntelliBudgetApi/
COPY IntelliBudgetApi.Api/*.csproj IntelliBudgetApi.Api/
COPY IntelliBudgetApi.Application/*.csproj IntelliBudgetApi.Application/
COPY IntelliBudgetApi.Domain/*.csproj IntelliBudgetApi.Domain/
COPY IntelliBudgetApi.Infra/*.csproj IntelliBudgetApi.Infra/

RUN dotnet restore

COPY . .
RUN dotnet publish IntelliBudgetApi -c Release -o /app/publish

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 8080
ENTRYPOINT ["dotnet", "IntelliBudgetApi.dll"]