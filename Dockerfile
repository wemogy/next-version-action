#######################################################
# Step 1: Build the application in a container        #
#######################################################

FROM mcr.microsoft.com/dotnet/sdk:5.0 as build

# Copy .csproj files for NuGet restore
COPY ["src/Wemogy.ReleaseVersionAction/Wemogy.ReleaseVersionAction.csproj", "src/Wemogy.ReleaseVersionAction/"]

RUN dotnet restore src/Wemogy.ReleaseVersionAction/Wemogy.ReleaseVersionAction.csproj

# Copy the rest of the files over
COPY ["src/Wemogy.ReleaseVersionAction/", "src/Wemogy.ReleaseVersionAction/"]

# Build the application
WORKDIR /src/Wemogy.ReleaseVersionAction/
RUN dotnet publish --output /out/ --configuration Release --no-self-contained

#######################################################
# Step 2: Run the build outcome in a container        #
#######################################################

FROM mcr.microsoft.com/dotnet/runtime:5.0

COPY --from=build /out .

# Start the application
ENTRYPOINT ["dotnet", "/Wemogy.ReleaseVersionAction.dll"] # Use /... as GitHub overrides the working directory
