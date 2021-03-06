FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env

WORKDIR /app

# Copy csproj and restore as distinct layers
COPY *.sln ./
COPY /api/*.csproj ./api/
COPY /entities/*.csproj ./entities/
COPY /test/*.csproj ./test/
COPY /dal/*.csproj ./dal/

RUN dotnet restore
RUN dotnet tool install --global dotnet-ef --version 3.1.0
ENV PATH="$PATH:/root/.dotnet/tools"

# Copy everything else and build
COPY . ./
# COPY localhost.pfx ./out/localhost.pfx
RUN dotnet publish -c Release -o out
# RUN dotnet ef database update

# Build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS runtime
WORKDIR /app

COPY --from=build-env /app/out ./
COPY --from=build-env /app/entrypoint.sh ./entrypoint.sh

RUN chmod +x /app/entrypoint.sh

ENTRYPOINT ["/app/entrypoint.sh"]

EXPOSE 80 433
