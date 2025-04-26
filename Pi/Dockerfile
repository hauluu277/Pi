# Lựa chọn 1: Dùng Linux container
FROM --platform=linux/amd64 mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Hoặc lựa chọn 2: Nếu PHẢI dùng Windows container
FROM --platform=windows/amd64 mcr.microsoft.com/dotnet/sdk:8.0-nanoserver-ltsc2022 AS build

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# 1. Copy solution và các file .csproj
COPY Pi.sln .
COPY Pi.API/*.csproj Pi.API/
COPY Pi.Application/*.csproj Pi.Application/
COPY Pi.Domain/*.csproj Pi.Domain/
COPY Pi.Infrastracture/*.csproj Pi.Infrastracture/

# 2. Restore dependencies
RUN dotnet restore

# 3. Copy ALL source code (FIXED PATH)
COPY . .

# 4. Build
WORKDIR "/src/Pi.API"
RUN dotnet build -c Release --no-restore

# 5. Publish
FROM build AS publish
RUN dotnet publish "Pi.API.csproj" -c Release -o /app/publish

# 6. Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Pi.API.dll"]