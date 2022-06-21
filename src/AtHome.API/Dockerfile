#Instructions to build the Blog Engine API into a Docker container
FROM mcr.microsoft.com/dotnet/aspnet:6.0 as base
ENV ASPNETCORE_ENVIRONMENT=Development
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 as devbase
WORKDIR /src

FROM devbase as build
COPY . .
RUN dotnet build "AtHome.sln" -c Release -o /app

FROM build AS publish
RUN dotnet publish "AtHome.API/AtHome.API.csproj" -c Release -o /app

FROM base as final
RUN sed 's/DEFAULT@SECLEVEL=2/DEFAULT@SECLEVEL=1/' /etc/ssl/openssl.cnf > /etc/ssl/openssl.cnf.changed && mv /etc/ssl/openssl.cnf.changed /etc/ssl/openssl.cnf
WORKDIR /app

COPY --from=publish /app .
ENTRYPOINT ["dotnet", "AtHome.API.dll"]