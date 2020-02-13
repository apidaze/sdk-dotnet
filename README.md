
# Apidaze .NET SDK

The Apidaze .NET SDK contains the C# client of Apidaze REST API as well as an XML script builder.
The SDK allows you to leverage all Apidaze platform features such as making calls, sending text messages, serving IVR systems and many others in your C# based application.
The SDK also includes sample applications that demonstrate how to use the SDK interfaces.
See [Apidaze REST API specification](https://apidocs.voipinnovations.com) which includes XML Scripting Reference as well.

# Requirements
- .NET Core 3.1

# Installation

To install the SDK just build solution then install this SDK by referencing the .dll file.

You're now ready to use the SDK.

# Quickstart

## SDK client

### Initiate the SDK api action factory

```csharp
// initialize API action factory
var credentials = new Credentials(API_KEY, API_SECRET);
var apiFactory = ApplicationManager.CreateApiFactory(credentials);
```

Where `API_KEY` and `API_SECRET` should be replaced with the real key and secret from your Apidaze application.

### Initiate an API action
To execute any action such as making a call or send text message you need to call method from IApiActionFactory interface.

```csharp
// initialize a message API
var messageApi = apiFactory.CreateMessageApi();
```

#### Sub-applications
Optionally if you have created sub-applications you can use *ApplicationManager* to initiate *ApiActionFactory* by using on of the following methods

```csharp
// initiate API action factory
var credentials = new Credentials(API_KEY, API_SECRET);
var apiFactory = ApplicationManager.CreateApiFactory(credentials);

// initiate applications API
var applicationsApi = apiFactory.CreateApplicationsApi();

// get ApplicationAction by id
var appActionById = applicationsApi.GetApplicationActionById(id);

// get ApplicationAction by api_key assigned to sub-application
var appActionByApiKey = applicationsApi.GetApplicationActionByApiKey("apiKey");

// get ApplicationAction by sub-application name
var appActionByName = applicationsApi.GetApplicationActionByName("testName");
```

### Make a call


```csharp
// initiate ApplicationAction
var applicationClient = ApplicationManager.CreateApiFactory(new Credentials(apiKey, apiSecret));

// initialize callsApi
var callsApi = applicationClient.CreateCallsApi();

// make a call
var guid = callsApi.CreateCall(new PhoneNumber(callerId), origin, destination, CallType.NUMBER);
```

### Send a text message

```csharp
// initialize API factory
var apiFactory = ApplicationManager.CreateApiFactory(new Credentials(apiKey, apiSecret));

var from = "1412345678";
var to = "14987456123";
var messageBody = "Have a nice day!";

// initialize a message API
var messageApi = apiFactory.CreateMessageApi();

// send a text
var response = messageApi.SendTextMessage(new PhoneNumber(from), new PhoneNumber(to), messageBody);
```

### More examples
More examples are [here](https://github.com/apidaze/sdk-dotnet/tree/master/Apidaze.SDK.Tests.Integration) .

## Scripts builders

Scripts builders are used to build XML instructions described in [XML Scripting Reference](https://apidocs.voipinnovations.com).
To build an instruction which echo back received audio to the caller with some delay use the following code.
```csharp
ApidazeScript script = ApidazeScript.Build().AddNode(new Answer()).AddNode(new Echo { Delay = 500 });

string xml = script.ToXml();
```
The content of produced xml is as follow.
```xml
<document>
  <work>
    <answer/>
    <echo>500</echo>
  </work>
</document>
```

For more examples see [unit tests](https://github.com/apidaze/sdk-dotnet/tree/master/Apidaze.SDK.Tests.Unit/ScriptsBuilders)

Sample applications with real life scenarios (i.e. IVR demo) are [here](https://github.com/apidaze/sdk-dotnet/tree/master/Apidaze.SDK.Tests.Integration/IvrExample)