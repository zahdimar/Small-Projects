# Use the SDK image to build the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy the csproj files and restore dependencies
COPY ["Shorturler.Web/Shorturler.Web.csproj", "Shorturler.Web/"]
COPY ["Shorturler.Database/Shorturler.Database.csproj", "Shorturler.Database/"]
RUN dotnet restore "Shorturler.Web/Shorturler.Web.csproj"

# Copy the entire solution and build the projects
COPY . .
WORKDIR "/src/Shorturler.Web"
RUN dotnet build "Shorturler.Web.csproj" -c Release -o /app/build

# Publish the web application
FROM build AS publish
RUN dotnet publish "Shorturler.Web.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Use the runtime image to run the application
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Copy the published output from the previous stage
COPY --from=publish /app/publish .

# Set the entry point for the application
ENTRYPOINT ["dotnet", "Shorturler.Web.dll"]