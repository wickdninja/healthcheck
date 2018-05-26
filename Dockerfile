# build
FROM microsoft/aspnetcore-build:2.0 AS build-env
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY *.csproj ./
RUN dotnet restore

# Copy everything else and build
COPY . ./
RUN dotnet publish -c Release -o bin

# Create nuget package
RUN dotnet pack -c Release -o out

# Publish nuget package to artifactory
RUN dotnet nuget push ./out/*.nupkg

# Build runtime image
FROM microsoft/aspnetcore:2.0
WORKDIR /bin
COPY --from=build-env /app/bin .