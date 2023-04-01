# AspNetMicroservice
Learning Microservice Step By Step following a udemy course given below... 🙂

[![Screenshot_6](https://user-images.githubusercontent.com/1147445/85838002-907dc280-b7a1-11ea-8219-f84e3af8ba52.png)](https://www.udemy.com/course/microservices-architecture-and-implementation-on-dotnet/?couponCode=FA24745CC57592AB612A)

There is a couple of microservices which implemented **e-commerce** modules over **Catalog, Basket, Discount** and **Ordering** microservices with **NoSQL (MongoDB, Redis)** and **Relational databases (PostgreSQL, Sql Server)** with communicating over **RabbitMQ Event Driven Communication** and using **Ocelot API Gateway**.

**Refer the main repository -> https://github.com/aspnetrun/run-aspnetcore-microservices**

## Languages & Tools
- .NET Core v6
- C#
- NoSQL (MongoDB, Redis)
- SQL (PostgreSQL, TSQL)
- RabbitMQ
- Ocelot Gateway
- Visual Studio 22 Community
- Docker

## Run Project 
```bash
git clone "https://github.com/madcoderBubt/AspNetMicroservice.git"
```
```powershell
docker-compose -f docker-compose.yml -f docker-compose.override.yml up -d
```

> Make Sure Docker is running on your System 🙂