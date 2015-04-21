using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Collections.Specialized;
using System.IO;
using System.Configuration;
using System.Reflection;

namespace CTCT.Util
{
    /// <summary>
    /// Class implementation of REST client.
    /// </summary>
    public class RestClient : IRestClient
    {
        /// <summary>
        /// Http processor
        /// </summary>
        protected HttpProcessor httpProcessor = new HttpProcessor();

        /// <summary>
        /// Make an Http GET request.
        /// </summary>
        /// <param name="url">Request URL.</param>
        /// <param name="accessToken">Constant Contact OAuth2 access token</param>
        /// <param name="apiKey">The API key for the application</param>
        /// <returns>The response body, http info, and error (if one exists).</returns>
        public RawApiResponse Get(string url, string accessToken, string apiKey)
        {
            return HttpRequest(url, HttpMethod.GET, accessToken, apiKey, null);
        }

        /// <summary>
        /// Make an Http POST request.
        /// </summary>
        /// <param name="url">Request URL.</param>
        /// <param name="accessToken">Constant Contact OAuth2 access token</param>
        /// <param name="apiKey">The API key for the application</param>
        /// <param name="data">Data to send with request.</param>
        /// <returns>The response body, http info, and error (if one exists).</returns>
        public RawApiResponse Post(string url, string accessToken, string apiKey, string data)
        {
            return HttpRequest(url, HttpMethod.POST, accessToken, apiKey, data);
        }

        /// <summary>
        /// Make an Http PATCH request.
        /// </summary>
        /// <param name="url">Request URL.</param>
        /// <param name="accessToken">Constant Contact OAuth2 access token</param>
        /// <param name="apiKey">The API key for the application</param>
        /// <param name="data">Data to send with request.</param>
        /// <returns>The response body, http info, and error (if one exists).</returns>
        public RawApiResponse Patch(string url, string accessToken, string apiKey, string data)
        {
            return HttpRequest(url, HttpMethod.PATCH, accessToken, apiKey, data);
        }

        /// <summary>
        /// Make an HTTP Post Multipart request.
        /// </summary>
        /// <param name="url">Request URL.</param>
        /// <param name="accessToken">Constant Contact OAuth2 access token</param>
        /// <param name="apiKey">The API key for the application</param>
        /// <param name="data">Data to send with request.</param>
        /// <returns>The response body, http info, and error (if one exists).</returns>
        public RawApiResponse PostMultipart(string url, string accessToken, string apiKey, byte[] data)
        {
            return MultipartHttpRequest(url, HttpMethod.POST, accessToken, apiKey, data);
        }

        /// <summary>
        /// Make an Http PUT request.
        /// </summary>
        /// <param name="url">Request URL.</param>
        /// <param name="accessToken">Constant Contact OAuth2 access token</param>
        /// <param name="apiKey">The API key for the application</param>
        /// <param name="data">Data to send with request.</param>
        /// <returns>The response body, http info, and error (if one exists).</returns>
        public RawApiResponse Put(string url, string accessToken, string apiKey, string data)
        {
            return HttpRequest(url, HttpMethod.PUT, accessToken, apiKey, data);
        }

        /// <summary>
        /// Make an Http DELETE request.
        /// </summary>
        /// <param name="url">Request URL.</param>
        /// <param name="accessToken">Constant Contact OAuth2 access token</param>
        /// <param name="apiKey">The API key for the application</param>
        /// <returns>The response body, http info, and error (if one exists).</returns>
        public RawApiResponse Delete(string url, string accessToken, string apiKey)
        {
            return HttpRequest(url, HttpMethod.DELETE, accessToken, apiKey, null);
        }


        private RawApiResponse HttpRequest(string url, HttpMethod httpMethod, string accessToken, string apiKey, string data)
        {
            return httpProcessor.HttpRequest(url, httpMethod, new JsonContentType(), accessToken, apiKey, data);
        }

        private RawApiResponse MultipartHttpRequest(string url, HttpMethod httpMethod, string accessToken, string apiKey, byte[] data)
        {
            return httpProcessor.HttpRequest(url, httpMethod, new FormDataContentType(), accessToken, apiKey, data);
        }
    }
}
