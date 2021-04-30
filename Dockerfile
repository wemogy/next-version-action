#######################################################
# Step 1: Build the application in a container        #
#######################################################

FROM mcr.microsoft.com/dotnet/sdk:5.0 as build

ARG NUGET_USERNAME
ARG NUGET_TOKEN

# Copy .csproj files for NuGet restore
COPY ["src/Wemogy.ReleaseVersionAction/Wemogy.ReleaseVersionAction.csproj", "src/Wemogy.ReleaseVersionAction/"]

RUN dotnet nuget add source https://nuget.pkg.github.com/wemogy/index.json --name github --username $NUGET_USERNAME --password $NUGET_TOKEN --store-password-in-clear-text
RUN dotnet restore src/Wemogy.ReleaseVersionAction/Wemogy.ReleaseVersionAction.csproj

# Copy the rest of the files over
COPY ["src/Wemogy.ReleaseVersionAction/", "src/Wemogy.ReleaseVersionAction/"]

# Build the application
WORKDIR /src/Wemogy.ReleaseVersionAction/
RUN dotnet build --output /out/ --configuration Release

#######################################################
# Step 2: Run the build outcome in a container        #
#######################################################

FROM mcr.microsoft.com/dotnet/runtime:5.0

COPY --from=build /out .

# Start the application
ENTRYPOINT ["dotnet", "Wemogy.ReleaseVersionAction.dll"]
