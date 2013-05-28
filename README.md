Constant Contact .NET SDK
=========================

## Installation

In order to use the Constant Contact SDK you have to follow these steps:

1) Download the source code, build the project, and then add the CTCT.dll library to your references project.

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

<<<<<<< HEAD
2) Get the access token

2.1) For windows forms
=======
2) Get the access token with the built-in authentication form (supports only windows forms at this point but the flow is the same for webform).  You can also retrieve (or hard code) your access token here.
>>>>>>> 875ea53f68ff8f52b78587bdfc065a2c62044afe

`_accessToken = OAuth.AuthenticateFromWinProgram(ref state); ` 
<br>
Or
<br>
`_accessToken = "accessTokenValue"; ` 
<br>

2.2) For web forms (this is just an example, the login actions is done at a button click)

`protected void Page_Load(object sender, EventArgs e)`
<br>
`{`
<br>
`	var code = HttpContext.Current.Request.QueryString["code"];`
<br>
`	if (!string.IsNullOrWhiteSpace(code))`
<br>
`	{`
<br>
`		_accessToken = OAuth.GetAccessTokenByCodeForWebApplication(HttpContext.Current, code);`
<br>
`	}`
<br>
`}`
<br>
<br>
`protected void ButtonLogin_Click(object sender, EventArgs e)`
<br>
`{`
<br>
`	OAuth.AuthorizeFromWebApplication(HttpContext.Current, "ok");`
<br>
`}`



3) Create a ConstantContact object

`ConstantContact constantContact = new ConstantContact(_apiKey, _accessToken); `                                                                                     
                  
4) Use the functions of the SDK using the created object.   
             
######Example for getting a contact

`Contact contact = constantContact.GetContact(int contactId);`                                                      

