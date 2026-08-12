# HelloWorld Microservice

# About
The HelloWorld Microservice is a Common Tech template WebAPI microservice with FunctionApp both with single functioning endpoints containing unit and automation tests and a working pipeline. Also standard system endpoints for health, version and error codes are included from the common packages. This microservice contains the expected base folder structure to be replicated in any new microservices.

# Installation guide
### System Requirements
- IDE capable of running .NET 10 or above i.e. Visual Studio

### Installation instructions
1. Clone the repository to your local machine.

2. Open the 'IPO.HelloWorld.sln' solution file in Visual Studio.

3. In the Web API project add a local development settings file called 'appsettings.Development.json'. Copy the contents of the below Configuration file and paste into the new 'appsettings.Development.json' file. You must update to include your own IPO email address, this will allow you access to the App Config in Stacks environment.

4. Build the solution.

5. Set the Web API (IPO.HelloWorld.API) as the Startup project in Visual Studio and run in debug configuration.

6. A command window will launch, in which you will see the Console output.

7. The swagger page will launch in your default browser ready to test the endpoints.

8. Alternatively in step 5 you can set the FunctionApp (IPO.HelloWorld.TemplateFunction) as the Startup project in Visual Studio and run in debug configuration.

## Configuration file:
IPO.HelloWorld.API
```JSON
{
  "IpoLogLevel": "Error",
  "AllowedHosts": "*",
  "Greeting": "Hello"
}
```