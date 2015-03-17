using System;
using System.Configuration;
using System.Windows.Forms;
using System.Web;

namespace CTCT
{
    /// <summary>
    /// Form used for windows authentication
    /// </summary>
    public partial class AuthenticationForm : Form
    {
        private const string AccessTokenConst = "access_token";
        private const string StateConst = "state";
        private const string RequestAuthorizationUrl = "https://oauth2.constantcontact.com/oauth2/oauth/siteowner/authorize?response_type=token&client_id={0}&redirect_uri={1}&state={2}";

        private readonly string _url = string.Empty;
        private readonly string _redirectUrl = string.Empty;        
        private readonly string _apiKey = string.Empty;

        /// <summary>
        /// Access token field
        /// </summary>
        public string AccessToken;

        /// <summary>
        /// State field
        /// </summary>
        public string State;

        /// <summary>
        /// Constructor
        /// </summary>
        public AuthenticationForm()
        {
            InitializeComponent();

            _apiKey = ConfigurationManager.AppSettings["APIKey"];
            _redirectUrl = ConfigurationManager.AppSettings["RedirectURL"];

            _url = String.Format(RequestAuthorizationUrl, HttpUtility.UrlEncode(_apiKey), HttpUtility.UrlEncode(_redirectUrl),HttpUtility.UrlEncode(State));
        }

        private void AuthenticationForm_Load(object sender, EventArgs e)
        {
            webBrowserControl.Navigate(_url);
        }

        private void webBrowserControl_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            var url = webBrowserControl.Url;

            if (url.OriginalString.Contains(_redirectUrl))
            {
                if (url.Fragment.Contains(AccessTokenConst))
                {
                    var startIndex = url.Fragment.IndexOf(AccessTokenConst, StringComparison.Ordinal) + AccessTokenConst.Length + 1;
                    AccessToken = url.Fragment.Substring(startIndex, url.Fragment.IndexOf("&", StringComparison.Ordinal) - startIndex);                 
                }

                if (url.Fragment.Contains(StateConst))
                {
                    var sIndex = url.Fragment.IndexOf(StateConst, StringComparison.Ordinal) + StateConst.Length + 1;
                    var lenght = url.Fragment.Substring(sIndex).Contains("&") ? url.Fragment.Substring(sIndex).IndexOf("&") : url.Fragment.Substring(sIndex).Length;
                    State = url.Fragment.Substring(sIndex, lenght);
                }

                this.Close();
            }
        }
    }
}
