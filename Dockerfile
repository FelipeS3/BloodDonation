# Etapa 1: Build com SDK do .NET 8
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copiar arquivos
COPY *.sln ./
COPY BloodDonation.API/*.csproj ./BloodDonation.API/
COPY BloodDonation.Application/*.csproj ./BloodDonation.Application/
COPY BloodDonation.Infrastructure/*.csproj ./BloodDonation.Infrastructure/
COPY BloodDonation.Core/*.csproj ./BloodDonation.Core/

# Restaurar dependências
RUN dotnet restore

# Copiar resto do código e publicar
COPY . ./
RUN dotnet publish -c Release -o /publish

# Etapa 2: Rodar app com runtime do .NET 8
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /publish .
ENTRYPOINT ["dotnet", "BloodDonation.API.dll"]