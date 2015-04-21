using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CTCT.Webhooks.Model;
using CTCT.Components;
using CTCT.Exceptions;
using CTCT.Util;
using CTCT.Webhooks.Helper;

namespace CTCT.Webhooks
{
    /// <summary>
    /// Main Webhook Utility class 
    /// This is meant to be used by users to validate and parse Webhooks received from ConstantContact
    /// </summary>
    public class CTCTWebhookUtil
    {
        /// <summary>
        /// The client secret associated with the api key
        /// </summary>
        public string ClientSecret { get; private set; }

        /// <summary>
        /// Header name of the HmacSha256 hash
        /// </summary>
        public const string HmacHeaderName = "x-ctct-hmac-sha256";

        /// <summary>
        /// Class constructor
        /// </summary>
        /// <param name="clientSecret">Client secret key</param>
        public CTCTWebhookUtil(string clientSecret)
        {
            this.ClientSecret = clientSecret;
        }

        /// <summary>
        /// Validates and parses the bodyMessage into BillingChangeNotification
        /// </summary>
        /// <param name="xCtctHmacSHA256">The value in the x-ctct-hmac-sha256 header</param>
        /// <param name="bodyMessage">The body message from the POST received from ConstantContact in Webhook callback</param>
        /// <returns>BillingChangeNotification object corresponding to bodyMessage in case of success; an exception is thrown otherwise.</returns>
        public BillingChangeNotification GetBillingChangeNotification(string xCtctHmacSHA256, string bodyMessage)  
        {
            if (IsValidWebhook(xCtctHmacSHA256, bodyMessage)) 
            {
                return Component.FromJSON<BillingChangeNotification>(bodyMessage);
            } 
            else 
            {
                throw new CtctException(CTCT.Resources.Errors.InvalidWebhook);
            }
        }

        /// <summary>
        /// Validates a Webhook message
        /// </summary>
        /// <param name="xCtctHmacSHA256">The value in the x-ctct-hmac-sha256 header</param>
        /// <param name="bodyMessage">The body message from the POST received from ConstantContact in Webhook callback</param>
        /// <returns>True if in case of success; False if the Webhook is invalid</returns>
        public bool IsValidWebhook(string xCtctHmacSHA256, string bodyMessage) 
        {
            if (ClientSecret == null) 
            {
                throw new CtctException(CTCT.Resources.Errors.NoClientSecret);
            }
            return new WebHookValidator(xCtctHmacSHA256, bodyMessage, ClientSecret).IsValid();
        }
    }
}
