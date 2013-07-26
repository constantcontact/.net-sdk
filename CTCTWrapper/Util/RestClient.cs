﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Collections.Specialized;
using System.IO;
using System.Configuration;

namespace CTCT.Util
{
    /// <summary>
    /// Class implementation of REST client.
    /// </summary>
    public class RestClient : IRestClient
    {
        /// <summary>
        /// Make an Http GET request.
        /// </summary>
        /// <param name="url">Request URL.</param>
        /// <param name="accessToken">Constant Contact OAuth2 access token</param>
        /// <param name="apiKey">The API key for the application</param>
        /// <returns>The response body, http info, and error (if one exists).</returns>
        public CUrlResponse Get(string url, string accessToken, string apiKey)
        {
            return this.HttpRequest(url, WebRequestMethods.Http.Get, accessToken, apiKey, null);
        }

        /// <summary>
        /// Make an Http POST request.
        /// </summary>
        /// <param name="url">Request URL.</param>
        /// <param name="accessToken">Constant Contact OAuth2 access token</param>
        /// <param name="apiKey">The API key for the application</param>
        /// <param name="data">Data to send with request.</param>
        /// <returns>The response body, http info, and error (if one exists).</returns>
        public CUrlResponse Post(string url, string accessToken, string apiKey, string data)
        {
            return this.HttpRequest(url, WebRequestMethods.Http.Post, accessToken, apiKey, data);
        }

        /// <summary>
        /// Make an Http PUT request.
        /// </summary>
        /// <param name="url">Request URL.</param>
        /// <param name="accessToken">Constant Contact OAuth2 access token</param>
        /// <param name="apiKey">The API key for the application</param>
        /// <param name="data">Data to send with request.</param>
        /// <returns>The response body, http info, and error (if one exists).</returns>
        public CUrlResponse Put(string url, string accessToken, string apiKey, string data)
        {
            return this.HttpRequest(url, WebRequestMethods.Http.Put, accessToken, apiKey, data);
        }

        /// <summary>
        /// Make an Http DELETE request.
        /// </summary>
        /// <param name="url">Request URL.</param>
        /// <param name="accessToken">Constant Contact OAuth2 access token</param>
        /// <param name="apiKey">The API key for the application</param>
        /// <returns>The response body, http info, and error (if one exists).</returns>
        public CUrlResponse Delete(string url, string accessToken, string apiKey)
        {
            return this.HttpRequest(url, "DELETE", accessToken, apiKey, null);
        }

        private CUrlResponse HttpRequest(string url, string method, string accessToken, string apiKey, string data)
        {
            // Initialize the response
            HttpWebResponse response = null;
            string responseText = null;
            CUrlResponse urlResponse = new CUrlResponse();

            var address = url;

            if (!string.IsNullOrEmpty(apiKey))
            {
                address = string.Format("{0}{1}api_key={2}", url, url.Contains("?") ? "&" : "?", apiKey);
            }

            HttpWebRequest request = WebRequest.Create(address) as HttpWebRequest;
                                                                                             
            request.Method = method;
            request.ContentType = "application/json";
            request.Accept = "application/json";
            // Add token as HTTP header
            request.Headers.Add("Authorization", "Bearer " + accessToken);

            if (data != null)
            {
                // Convert the request contents to a byte array and include it
                byte[] requestBodyBytes = System.Text.Encoding.UTF8.GetBytes(data);
                using (var stream = request.GetRequestStream())
                {
                    stream.Write(requestBodyBytes, 0, requestBodyBytes.Length);
                }
            }

            // Now try to send the request
            try
            {
                response = request.GetResponse() as HttpWebResponse;
                // Expect the unexpected
                if (request.HaveResponse == true && response == null)
                {
                    throw new WebException("Response was not returned or is null");
                }
                urlResponse.StatusCode = response.StatusCode;
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    throw new WebException("Response with status: " + response.StatusCode + " " + response.StatusDescription);
                }
            }
            catch (WebException e)
            {
                if (e.Response != null)
                {
                    response = (HttpWebResponse)e.Response;
                    urlResponse.IsError = true;
                }
            }
            finally
            {
                if (response != null)
                {
                    // Get the response content
                    using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                    {
                        responseText = reader.ReadToEnd();
                    }
                    response.Close();
                    if (urlResponse.IsError && responseText.Contains("error_message"))
                    {
                        urlResponse.Info = CUrlRequestError.FromJSON<IList<CUrlRequestError>>(responseText);
                    }
                    else
                    {
                        urlResponse.Body = responseText;
                    }
                }
            }

            return urlResponse;
        }
    }
}
