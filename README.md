# Saga Pattern using Rebus with RabbitMQ

I created this project as a example for the Saga pattern using Rebus with RabbitMQ.

Inside the project you will find the following projects:

* RebusDotnetCore - WebApplication
```
It serves as a frontend for the Orchestrator project. You will find the Rebus and rabbitMQ configuration inside the Startup.cs file.
```
* Orchestrator - Using Saga pattern
```
Is the Saga controller. The file DemoSaga orchestrates all the event flow inside this example. DemoSagaData is used to determine when a Saga is finished.
```
* Service1 and Service2
```
Empty services that only subscribe to a event and publish another one back to the orchestrator.
```



## Getting Started

This instructions will get you a copy of the project up and running on your local machine for development and testing purposes.

### Prerequisites

* [.NET Core 3.1](https://dotnet.microsoft.com/download/dotnet-core/3.1) - Cross-Platform version of .NET for building websites, services and console apps.
* [Rebus](https://github.com/rebus-org/Rebus) - Message Bus for .NET
* [RabbitMQ](https://www.rabbitmq.com/) - Message Broker
```
You can either use a RabbitMQ installed on your local machine or the docker image 'rabbitmq-management' at docker hub.
```
* [Docker](https://www.docker.com/) - if you choose to use the rabbitmq-management image

### Configuring

* On the Startup.cs file, change the 'amqp://localhost:5672' to the correct uri from your rabbitMQ. Normally the port 5672 is the default.

## Authors

* **Felipe Almeida**

## Acknowledgments
* I was inspired to create this example using a real problem I had at work.