FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build

WORKDIR /src

COPY . .

RUN dotnet publish -c Release -o /app

FROM mcr.microsoft.com/dotnet/aspnet:5.0

WORKDIR /app

COPY --from=build /app .

EXPOSE 7000

ENTRYPOINT ["dotnet", "HelloService.dll"]
