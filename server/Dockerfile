FROM mcr.microsoft.com/dotnet/core/sdk:3.1 as build-env

ARG BUILDCONFIG=RELEASE
ARG VERSION=0.0.1

WORKDIR /app
EXPOSE 5000

COPY MatchDay.sln .
COPY ./src ./src
RUN dotnet restore ./src/MatchDayApp.Api

COPY . .
RUN dotnet publish ./src/MatchDayApp.Api/MatchDayApp.Api.csproj -c $BUILDCONFIG --no-restore -o out /p:Version=$VERSION

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1

WORKDIR /app
COPY --from=build-env /app/out .

ENTRYPOINT ["dotnet", "MatchDayApp.Api.dll"]