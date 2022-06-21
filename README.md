# At-Home-service
## Prerequisites

- Install dotnet core 6.0

## Run Application with dotnet from command line

Go inside directory -> src/AtHome.API

Run the following command line:

```bash
dotnet "run" "--project" "src\AtHome.API.csproj"
```
Open Browser on : https://localhost:7144/swagger/index.html

Swagger Should be loaded.

![alt text](https://github.com/pablosangueza/At-Home-service/blob/main/resources/AtHomeSwaggerAPI.png)

## Run Application with Docker

Inside src directory: 

```bash
docker build -f .\AtHome.API\Dockerfile -t at-home-service .
```

```bash
docker run -it --rm -p 8081:80 at-home-service
```
Open Browser on : http://localhost:8081/swagger/index.html

## Run unit tests
Inside src directory: 
```bash
dotnet "test" "AtHome.Service.Test\AtHome.Service.Test.csproj"
```

```bash
dotnet "test"  "AtHome.ShippingCompanyAPI.Test\AtHome.ShippingCompanyAPI.Test.csproj"
```
## Additional notes

- As we don't have external API for testing purpose, was created the service type "REST_RANDOM_MOCK" that simulate a external API call a get the shipping amount randomically for any company. This is only for testing prupposes.

![alt text](https://github.com/pablosangueza/At-Home-service/blob/main/resources/AtHomeRestRandom.png)




