FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-stage
WORKDIR /Sources

COPY /Notes.Api/Notes.Api.csproj ./
RUN dotnet restore

COPY /Notes.Api ./
RUN dotnet publish \
    --output ./Notes.Published \
    --configuration Release \
    --self-contained false \
    --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /Application
COPY --from=build-stage /Sources/Notes.Published ./
ENTRYPOINT ["dotnet", "Notes.Api.dll"]