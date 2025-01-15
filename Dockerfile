# Usar la imagen base de .NET SDK
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Establecer el directorio de trabajo
WORKDIR /src

# Copiar el archivo .csproj desde la ruta correcta
COPY ./jmasAPI/jmasAPI.csproj ./jmasAPI/jmasAPI.csproj

# Restaurar las dependencias
RUN dotnet restore ./jmasAPI/jmasAPI.csproj

# Copiar el resto de los archivos
COPY . ./

# Construir el proyecto
RUN dotnet build ./jmasAPI/jmasAPI.csproj -c Release -o /app/build

# Publicar el proyecto
RUN dotnet publish ./jmasAPI/jmasAPI.csproj -c Release -o /app/publish

# Usar la imagen base de .NET para la ejecución
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base

# Establecer el directorio de trabajo
WORKDIR /app

# Copiar los archivos publicados desde la etapa anterior
COPY --from=build /app/publish .

# Exponer el puerto en el que se ejecutará la aplicación
EXPOSE 80

# Establecer el comando para ejecutar la aplicación
ENTRYPOINT ["dotnet", "jmasAPI.dll"]
