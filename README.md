# Inscrip-ConsumerAndMailer

The consumer part of the AMQP-MailHog pipeline, using C#, EntityFramework, OpenAPI and RabbitMQ.

## Prerequisites

You'll need :
- Visual Studio 2022 (with "ASP.NET and web development" kit) (Optional, you can use the cmd if you don't need to debug it.)
- DotNET 7 (download the SDK [here](https://dotnet.microsoft.com/en-us/download/dotnet/7.0))
- Docker

## How to begin

### Using Visual Studio
Open the solution (.sln file) using Visual Studio. In case there are compiler errors showing up, you might need to get the NuGet packages; right-click the solution and select "Restore NuGet Packages".

### Using cmd
Go to the solution's folder. Use the command `dotnet restore`.

### Using the exe
Download the zip file found [here](https://github.com/Gameplushy/Inscrip-ConsumerAndMailer/releases/tag/full-release)

## How to use

### For all 3 possibilities
Execute in a cmd `docker run -it --rm --name rabbitmq -p 5672:5672 -p 15672:15672 rabbitmq:3-management` do run RabbitMQ.
You can check RabbitMQ's status with the following link: `http://localhost:15672/#/` 

Execute in another cmd `docker run -it --rm --name mailhog -p 1025:1025 -p 8025:8025 mailhog/mailhog:latest` do run MailHog.
You can check MailHog's status with the following link: `http://localhost:8025/` 

### Using Visual Studio
Build and execute the code. A console should open. It will check for RabbitMQ messages and send mail afterwards. Logging will be present. Press enter to stop the program.

### Using cmd
In cmd, use `dotnet run`. A console should open. It will check for RabbitMQ messages and send mail afterwards. Logging will be present. Press enter to stop the program.

### Using the exe
Execute the exe file. A console should open. It will check for RabbitMQ messages and send mail afterwards. Logging will be present. Press enter to stop the program.

## Warning
Make sure the queue int RabbitMQ already exists before turning on this app, otherwise it will crash.