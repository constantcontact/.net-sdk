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

#### 2.1) For windows forms

```csharp
_accessToken = OAuth.AuthenticateFromWinProgram(ref state);
```

#### 2.2) For web forms

(This is just an example, the login actions is done at a button click)

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

### 3a) Create a service object directly, for example create a `ContactService` object

```csharp
IUserServiceContext userServiceContext = new UserServiceContext(_apiKey, _accessToken);
ContactService contactService = new ContactService(userServiceContext);
```

### 3b) Create a service object using the `ConstantContactFactory`, for example create a `ContactService` object

```csharp
IUserServiceContext userServiceContext = new UserServiceContext(_apiKey, _accessToken);
ConstantContactFactory serviceFactory = new ConstantContactFactory(userServiceContext);
ContactService contactService = serviceFactory.CreateContactService();
```

### 4) Use the SDK via the created object 
             
Example of getting a contact:

```csharp
int contactId = 12345;
Contact contact = contactService.GetContact(contactId);
```
