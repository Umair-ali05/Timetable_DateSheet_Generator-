# Stage 1: Build the application
FROM mcr.microsoft.com/dotnet/core/sdk:2.2 AS build
WORKDIR /src

# Restore dependencies (replace "Api.csproj" with your actual project file)
COPY *.csproj .
RUN dotnet restore

# Copy the rest of the application code
COPY . .

# Build the application
RUN dotnet build -c Release -o /app

# Publish the application
FROM build AS publish
RUN dotnet publish -c Release -o /app

# Stage 2: Create the runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:2.2 AS base
WORKDIR /app

# Copy the published application from the build stage
COPY --from=publish /app .

# Set environment variables

ENV ASPNETCORE_URLS http://0.0.0.0:5000

EXPOSE 5000

# Define the entry point for the container
ENTRYPOINT ["dotnet", "Timetable_DateSheet_Generator.dll"]
