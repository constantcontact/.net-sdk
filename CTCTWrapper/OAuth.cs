using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CTCT
{
    /// <summary>
    /// Main class meant to be used by users to obtain access token by authentication their app
    /// </summary>
    public static class OAuth
    {
        /// <summary>
        /// Returns access token obtained after authenticating client app
        /// </summary>
        /// <param name="state">state query parameter</param>
        /// <returns>string representing access token if authentication succeded, null otherwise</returns>
        public static string AuthenticateFromWinProgram(ref string state){
            var authform = new AuthenticationForm();
            authform.State = state;

            var result = authform.ShowDialog();
            state = authform.State; 

            return authform.AccessToken;
        }
    }
}
