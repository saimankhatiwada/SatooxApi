FROM mcr.microsoft.com/dotnet/sdk:7.0 AS base

WORKDIR /source

COPY . .

RUN dotnet restore "./Api/Api.csproj" 
RUN dotnet publish "./Api/Api.csproj" -c release -o /app --no-restore

FROM mcr.microsoft.com/dotnet/sdk:7.0

WORKDIR /app

COPY --from=base /app ./

EXPOSE 5000


ENTRYPOINT [ "dotnet", "Api.dll" ]