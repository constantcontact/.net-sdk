Constant Contact .NET SDK
=========================

## Installation

In order to use the Constant Contact SDK you have to follow these steps:

1) Download and build the project so that CTCT.dll is generated. Add the CTCT.dll library to your references project.

2) Place your credentials in the *app.config* or *web.config* file under the `appSettings` tag.

```xml
<appSettings>
    <add key="APIKey" value="APIkey"/>
    <add key="RedirectURL" value="RedirectURL"/>
</appSettings>
```

## Documentation

SDK Documentation is hosted at http://constantcontact.github.io/.net-sdk

API Documentation is located at http://developer.constantcontact.com/docs/developer-guides/api-documentation-index.html

## Usage

### 1) Include namespaces

In the file you wish to use the SDK include the following code in your file:

```csharp
using CTCT;
using CTCT.Components;
using CTCT.Components.Contacts;
using CTCT.Components.EmailCampaigns;
using CTCT.Exceptions;
```

### 2) Get the access token

#### 2.0) If the access token was already obtained, you may set the API Key and token like this. NOT ideal if you want to access multiple/variable Constant Contact accounts.

```csharp
private string _apiKey = "xxxxxxxxx"; 
private string _accessToken = "xxxx-xxxxx-xxxxx-xxxx"; 
```

#### 2.1) For windows forms

```csharp
_accessToken = OAuth.AuthenticateFromWinProgram(ref state);
```

#### 2.2) For web forms

(This will require the user to grant access in a browser window.)

```csharp
protected void Page_Load(object sender, EventArgs e)
{
    var code = HttpContext.Current.Request.QueryString["code"];
    if (!string.IsNullOrWhiteSpace(code))
    {
        _accessToken = OAuth.GetAccessTokenByCodeForWebApplication(HttpContext.Current, code);
    }
}
```

```csharp
protected void ButtonLogin_Click(object sender, EventArgs e)
{
   OAuth.AuthorizeFromWebApplication(HttpContext.Current, "ok");
}
```
### 3) Create a service object, for example create a `ContactService` object

#### 3.1) Create a service object directly

```csharp
IUserServiceContext userServiceContext = new UserServiceContext(_accessToken, _apiKey);
ContactService contactService = new ContactService(userServiceContext);
```

#### 3.2) Create a service object using the `ConstantContactFactory`

```csharp
IUserServiceContext userServiceContext = new UserServiceContext(_accessToken, _apiKey);
ConstantContactFactory serviceFactory = new ConstantContactFactory(userServiceContext);
ContactService contactService = serviceFactory.CreateContactService();
```

### 4) Use the SDK via the created object 
             
Example of getting a contact:

```csharp
int contactId = 12345;
Contact contact = contactService.GetContact(contactId);
```
