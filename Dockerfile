# https://hub.docker.com/_/microsoft-dotnet
FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /source

# переменные окружения
ENV Host = "localhost"

# прокидываем sln и csproj файлы в докер контейнер
COPY *.sln .
COPY Config/*.csproj ./Config/
COPY Database/*.csproj ./Database/
#COPY migrations/*.csproj ./migrations/
COPY WebApplication/*.csproj ./WebApplication/
RUN dotnet restore

# прокидываем библиотеки и сервисы в докер контейнер м собираем приложение
COPY Config/. ./Config/
COPY Database/. ./Database/
COPY WebApplication/. ./WebApplication/
WORKDIR /source/WebApplication
RUN dotnet restore
RUN dotnet publish -c release -o /app --no-restore

# запускаем образ
FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /app
COPY --from=build /app ./
EXPOSE 80
ENTRYPOINT ["dotnet", "WebApplication.dll"]