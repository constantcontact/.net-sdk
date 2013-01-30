Constant Contact IOS SDK
=========================

## Installation

In order to use the Constant Contact SDK you have to follow these steps:

1) Add the CTCT.dll library to your references project.

2) Place your credentials in app.config file under the appSettings tag.

`<add key="APIKey" value="APIkey"/>`
<br>
`<add key="ConsumerSecret" value="ConsumerSecret"/>`
<br>
`<add key="Password" value="password"/>`
<br>
`<add key="Username" value="username"/>`
<br>
`<add key="RedirectURL" value="RedirectURL"/>`

## Usage

1) In the file you wish to use the SDK include the following code in your file:

 `using CTCT; `
<br>
 `using CTCT.Util;` 
<br>
`using CTCT.Components.Contacts;`
<br>
`using CTCT.Auth;`
<br>
`using CTCT.Services;`  

2) Create a ConstantContact object

`ConstantContact constantContact = new ConstantContact(); `                                                                                     
                  
3) Use the functions of the SDK using the created object.   
             
######Example for getting an contact

`Contact contact = constantContact.GetContact(int contactId);`                                                      

