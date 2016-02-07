# Food.gov.uk Free Api Consumer

## Dependencies

+ Microsoft .Net 4.6.1
+ IDE: Visual Studio 2012+


## Architecture

The application is split into two components.

1. Web - The presentation layer - ASP.Net MVC web application

2. Common - Service layer - Holds all of the service functions and data models for the application (e.g. Api consumer, Food.org.uk Api data models etc..)


## Tools Used

### Unity Application Block

Dependencies are exposed to the presentation layer as interfaces.

All the dependencies are registered in ___Web -> App_Start -> UnityConfig.cs__ and passed via constructor or property injection into the controllers.

### Log4Net
Log4Net is used for activity and error logging.

## Code Structure

+ Main API consumer interface is in ___Common -> Infrastructure -> Api -> IApi.cs__ and implementation __Api.cs__ is in the same folder. This is a open generic type returning any data model class. A derived class __ApiBase.cs__ is used for handling activity logging.
+ Closed constructed type services handles specific Api calls. They are in __Common -> Services__ folder. This application has two services which have main API depencency injected: __AuthorityService__ and __EstablishmentService__. Interface created for both service which are also injected into WebUI's controller. 
+ Strong typed data model used for API Json results in this application which are stored in __Common -> Model__ and __Common -> ViewModel.__


## Testing
   
+ Provided both unit testing and integration testing using NUnit and Moq

## Installation and Running
+ Debug in Visual Studio or deploy to web server.
