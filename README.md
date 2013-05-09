Constant Contact .NET SDK
=========================

## Installation

In order to use the Constant Contact SDK you have to follow these steps:

1) Add the CTCT.dll library to your references project.

2) Place your credentials in app.config file under the appSettings tag.

`<add key="APIKey" value="APIkey"/>`
<br>
`<add key="RedirectURL" value="RedirectURL"/>`

## Usage

1) In the file you wish to use the SDK include the following code in your file:

 `using CTCT; `
<br>
 `using CTCT.Components;` 
<br>
`using CTCT.Components.Contacts;`
<br>
`using CTCT.Components.EmailCampaigns;`
<br>
`using CTCT.Exceptions;;`  

2) Get the access token with the built-in authentication form (supports only windows forms at this point but the flow is the same for webform).  You can also retrieve (or hard code) your access token here.

`_accessToken = OAuth.AuthenticateFromWinProgram(ref state); ` 
<br>
Or
<br>
`_accessToken = "accessTokenValue"; ` 
<br>

3) Create a ConstantContact object

`ConstantContact constantContact = new ConstantContact(_apiKey, _accessToken); `                                                                                     
                  
4) Use the functions of the SDK using the created object.   
             
######Example for getting an contact

`Contact contact = constantContact.GetContact(int contactId);`                                                      

