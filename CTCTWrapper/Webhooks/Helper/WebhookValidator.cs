using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace CTCT.Webhooks.Helper
{
    /// <summary>
    /// Represents a helper class that validates a Webhook
    /// </summary>
    public class WebHookValidator
    {
        #region Properties

        /// <summary>
        /// HTTP header hash
        /// </summary>
        public string CtctHttpHeader { get; private set; }

        /// <summary>
        /// HTTP body
        /// </summary>
        public string Body { get; private set; }

        /// <summary>
        /// Client secret key
        /// </summary>
        public string SharedSecret { get; private set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="xCtctHmacSHA256">Header hash represented as base64 string</param>
        /// <param name="body">Body message</param>
        /// <param name="sharedSecret">Client secret</param>
        public WebHookValidator(string xCtctHmacSHA256, string body, string sharedSecret)
        {
            CtctHttpHeader = xCtctHmacSHA256;
            Body = body;
            SharedSecret = sharedSecret;
        }

        #endregion

        #region Methods

        /// <summary>
        /// To verify that the request came from Constant Contact, compute the HMAC digest and compare it to the value in the x-ctct-hmac-sha256 header.
        /// If they match, you can be sure that the webhook was sent by Constant Contact and the message has not been compromised.
        /// </summary>
        /// <returns>True if webhook is valid; False otherwise</returns>
        public bool IsValid()
        {
            string computedHash = HashHMAC(Body, SharedSecret);
            return computedHash.Equals(CtctHttpHeader, StringComparison.InvariantCultureIgnoreCase);
        }


        /// <summary>
        /// Compute HMACSHA256 hash based on message and secret
        /// </summary>
        /// <param name="message">Message</param>
        /// <param name="secret">Secret key</param>
        /// <returns>Base64 hash string</returns>
        private string HashHMAC(string message, string secret)
        {
            secret = secret ?? "";
            var encoding = new System.Text.ASCIIEncoding();
            byte[] keyByte = encoding.GetBytes(secret);
            byte[] messageBytes = encoding.GetBytes(message);
            using (var hmacsha256 = new HMACSHA256(keyByte))
            {
                byte[] hashmessage = hmacsha256.ComputeHash(messageBytes);
                return Convert.ToBase64String(hashmessage);
            }
        }

        #endregion
    }
}
