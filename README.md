# Free Api Consumer

## Dependencies

+ Microsoft .Net 4.6.1
+ IDE: Visual Studio 2015


## Architecture

The application is split into two components.

1. Web - The presentation layer - ASP.Net MVC web application

2. Common - Service layer - Holds all service functions and data models for the application.


## Tools Used

### Unity Application Block

Dependencies are exposed to the presentation layer as interfaces.

All the dependencies are registered in ___Web -> App_Start -> UnityConfig.cs__ and passed via constructor or property injection into the controllers.

### Log4Net
Log4Net is used for activity and error logging.

## Code Structure

+ Main API consumer interface is in __Common__ -> __Infrastructure__ -> __Api__ -> __IApi.cs__ and implementation __Api.cs__ is in the same folder. This is a open generic type returning any data model class. A derived class __ApiBase.cs__ is used for handling activity logging.
+ Closed constructed type services handle specific Api calls. They are in __Common -> Repository__ folder. __AuthorityRepository__ , __EstablishmentRepository__ and __RatingKeyRepository__. Interfaces created for all repositories are injected into WebUI's controller. 
+ Strong typed data model used for API Json results in this application which are stored in __Common -> Model__ and __Common -> ViewModel.__
+ Rating search page is in __Home -> Index__. Controller: __HomeController__. Other views and controllers in __Web__ projects haven't been changed from default MVC template applied by Visual Studio.

## Other Functions
+ This site used MemoryCache which cache the API results for 30 minutes by default. 
+ Support both English and Welsh language as provided by Main API site. 

## Testing
   
+ Provided both unit testing and integration testing using NUnit and Moq

## Installation and Running
+ Debug in Visual Studio or deploy to web server.
+ Live demo: http://lllxf911-001-site1.ctempurl.com/

## Future Enhancement

+ Language support was added at very last minute. So I haven't got much time to do decent testing on it. 
+ If I have time, I'd like to use AngularJS in front end. Pass API results to an Angular model and shift server side functions to client side. It also improve user experiences. As calling real API might 'freeze' the browser for a free seconds.
+ I'd also like to handle multiple language support properly. I just passed the language  into RouteData (all done by MVC routing and action parameters). Ideally, It should use CultureInfo or setting up proper language resources files using Key/Value type data. 
+ Caching probably need more work, such as config settings, UI to clear cache/condition to clear etc...
+ Can potentially change strong closed constructed type models to open generic models. So that it can use single model class per API calls. 