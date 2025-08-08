# Stage 1: Build the application
# Use the .NET 9 SDK since your project targets net9.0
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /source

# Copy the project file from the root folder
COPY Anarcareweb.csproj .
RUN dotnet restore

# Copy the rest of the application files
COPY . .

# Publish the application from the root
RUN dotnet publish "Anarcareweb.csproj" -c Release -o /app/publish

# Stage 2: Create the final, smaller image
# Use the .NET 9 ASP.NET runtime image
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

# The entrypoint command to run your application
ENTRYPOINT ["dotnet", "Anarcareweb.dll"]