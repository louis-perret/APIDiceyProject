#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["APIDiceyProject/APIDiceyProject.csproj", "APIDiceyProject/"]
COPY ["Api.Model/Api.Model.csproj", "Api.Model/"]
COPY ["ModelDTOExtensions/ModelDTOExtensions.csproj", "ModelDTOExtensions/"]
COPY ["Api.DTOs/Api.DTOs.csproj", "Api.DTOs/"]
COPY ["Exceptions/Exceptions.csproj", "Exceptions/"]
COPY ["Api.Services/Api.Services.csproj", "Api.Services/"]
COPY ["Api.Repositories/Api.Repositories.csproj", "Api.Repositories/"]
COPY ["ModelEntityExtensions/ModelEntityExtensions.csproj", "ModelEntityExtensions/"]
COPY ["Api.Entities/Api.Entities.csproj", "Api.Entities/"]
COPY ["Api.EF/Api.EF.csproj", "Api.EF/"]
RUN dotnet restore "APIDiceyProject/APIDiceyProject.csproj"
COPY . .
WORKDIR "/src/APIDiceyProject"
RUN dotnet build "APIDiceyProject.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "APIDiceyProject.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "APIDiceyProject.dll"]