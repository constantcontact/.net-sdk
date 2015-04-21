using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Reflection;

namespace CTCT.Util
{
    /// <summary>
    /// Low-level Class responsible with HTTP requests in Constant Contact.
    /// </summary>
    public class HttpProcessor : ProcessorBase
    {
        /// <summary>
        /// Convenience method that automatically converts from Strings to bytes before calling makeHttpRequest.
        /// </summary>
        /// <param name="url">Url</param>
        /// <param name="method">Method</param>
        /// <param name="contentType">ContentType</param>
        /// <param name="accessToken">AccessToken</param>
        /// <param name="apiKey">ApiKey</param>
        /// <param name="data">Data if any</param>
        /// <returns>RawApiResponse</returns>
        public RawApiResponse HttpRequest(string url, HttpMethod method, IContentType contentType, string accessToken, string apiKey, string data)
        {
            byte[] bytes = null;

            if (data != null)
            {
                bytes = System.Text.Encoding.UTF8.GetBytes(data);
            }

            return HttpRequest(url, method, contentType, accessToken, apiKey, bytes);
        }

        /// <summary>
        /// Makes a HTTP request to the Endpoint specified in urlParam and using the HTTP method specified by httpMethod.
        /// </summary>
        /// <param name="url">Url</param>
        /// <param name="method">Method</param>
        /// <param name="contentType">ContentType</param>
        /// <param name="accessToken">AccessToken</param>
        /// <param name="apiKey">ApiKey</param>
        /// <param name="data">Data if any</param>
        /// <returns>RawApiResponse</returns>
        public RawApiResponse HttpRequest(string url, HttpMethod method, IContentType contentType, string accessToken, string apiKey, byte[] data)
        {
            var address = url;

            if (!string.IsNullOrEmpty(apiKey))
            {
                address = string.Format("{0}{1}api_key={2}", url, url.Contains("?") ? "&" : "?", apiKey);
            }

            HttpWebRequest request = WebRequest.Create(address) as HttpWebRequest;

            request.Method = method.ToString();
            request.Accept = JSON_CONTENT_TYPE;
            request.Headers[X_CTCT_REQUEST_SOURCE_HEADER] = "sdk.NET." + GetWrapperAssemblyVersion().ToString();
            request.ContentType = contentType.Value;

            // Add token as HTTP header
            request.Headers.Add(AUTHORIZATION_HEADER, "Bearer " + accessToken);

            if (data != null)
            {
                using (var stream = request.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }
            }

            // Initialize the response
            HttpWebResponse response = null;
            RawApiResponse rawApiResponse = new RawApiResponse();

            // Now try to send the request
            try
            {
                response = request.GetResponse() as HttpWebResponse;
                // Expect the unexpected
                if (request.HaveResponse == true && response == null)
                {
                    throw new WebException("Response was not returned or is null");
                }
                foreach (string header in response.Headers.AllKeys)
                {
                    rawApiResponse.Headers.Add(header, response.GetResponseHeader(header));
                }

                rawApiResponse.StatusCode = response.StatusCode;

                if (response.StatusCode != HttpStatusCode.OK &&
                    response.StatusCode != HttpStatusCode.Created &&
                    response.StatusCode != HttpStatusCode.Accepted &&
                    response.StatusCode != HttpStatusCode.NoContent)
                {
                    throw new WebException("Response with status: " + response.StatusCode + " " + response.StatusDescription);
                }
            }
            catch (WebException e)
            {
                response = e.Response as HttpWebResponse;
                rawApiResponse.IsError = true;
            }
            finally
            {
                if (response != null)
                {
                    string responseText = null;

                    // Get the response content
                    using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                    {
                        responseText = reader.ReadToEnd();
                    }
                    response.Close();
                    if (rawApiResponse.IsError && responseText.Contains("error_message"))
                    {
                        rawApiResponse.Info = RawApiRequestError.FromJSON<IList<RawApiRequestError>>(responseText);
                    }
                    else
                    {
                        rawApiResponse.Body = responseText;
                    }
                }
            }

            return rawApiResponse;
        }

        /// <summary>
        /// Get the wrapper version
        /// </summary>
        /// <returns>Wrapper version</returns>
        protected Version GetWrapperAssemblyVersion()
        {
            Version version = Assembly.GetExecutingAssembly().GetName().Version;
            return version;
        }
    }
}
