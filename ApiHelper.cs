using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using MessagePack;

namespace ChargesWFM.UI
{
    public static class ApiHelper
    {
        #region GET methods
        /// <summary>
        /// Helper method to send a GET http request. Json Response is deserialized to T
        /// </summary>
        /// <param name="relativeUrl">Relative Url</param>
        /// <param name="httpClient">Http Client</param>
        /// <param name="authorizationToken">JWT token for authorization</param>
        /// <typeparam name="T">Response Type</typeparam>
        /// <returns>Response data deserialized to T</returns>
        public static async Task<T> GetUsingJsonAsync<T>(string relativeUrl, HttpClient httpClient, string authorizationToken = null)
        {
            var response = await GetUsingJsonAsync(relativeUrl, httpClient, authorizationToken);
            if (response.IsSuccessStatusCode)
            {
                var stream = await response.Content.ReadAsStreamAsync();
                return await JsonSerializer.DeserializeAsync<T>(stream);
            }
            else if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                throw new UnauthorizedException();
            }
            else
            {
                throw new Exception($"GET Request failed with code: {response.StatusCode}, Url: {relativeUrl}");
            }
        }

        /// <summary>
        /// Send a GET http request
        /// </summary>
        /// <param name="relativeUrl">Relative Url</param>
        /// <param name="httpClient">Http Client</param>
        /// <param name="authorizationToken">JWT token for authorization</param>
        /// <returns>Http response</returns>
        public static async Task<HttpResponseMessage> GetUsingJsonAsync(string relativeUrl, HttpClient httpClient, string authorizationToken = null)
        {
            return await SendAsync(relativeUrl, HttpMethod.Get, httpClient, contentType: "application/json", authorizationToken: authorizationToken);
        }

        /// <summary>
        /// Helper method to send a GET http request. MsgPack Response is deserialized to T
        /// </summary>
        /// <param name="relativeUrl">Relative Url</param>
        /// <param name="httpClient">Http Client</param>
        /// <param name="authorizationToken">JWT token for authorization</param>
        /// <param name="queryParams">Query parameters</param>
        /// <typeparam name="T">Response Type</typeparam>
        /// <returns>Response data deserialized to T</returns>
        public static async Task<T> GetUsingMsgPackAsync<T>(string relativeUrl, HttpClient httpClient, string authorizationToken = null, Dictionary<string, string> queryParams = null)
        {
            if (queryParams != null)
            {
                // var content = new FormUrlEncodedContent(queryParams);
                // var urlEncodedString = await content.ReadAsStringAsync();
                // relativeUrl += $"?{urlEncodedString}";
                foreach (var item in queryParams)
                {
                    relativeUrl += "/" + item.Key + "/" + item.Value;
                }
            }
            var response = await GetUsingMsgPackAsync(relativeUrl, httpClient, authorizationToken);
            httpClient.DefaultRequestHeaders.Clear();
            if (response.IsSuccessStatusCode && response.StatusCode == HttpStatusCode.OK)
            {
                var stream = await response.Content.ReadAsStreamAsync();
                return await MessagePackSerializer.DeserializeAsync<T>(stream);
            }
            return response.IsSuccessStatusCode && response.StatusCode == HttpStatusCode.NoContent
                ? default
                : throw new Exception($"GET Request failed with code: {response.StatusCode}, Url: {relativeUrl}");
        }
        /// <summary>
        /// Helper method to send a GET http request. MsgPack Response is deserialized to T
        /// </summary>
        /// <param name="relativeUrl">Relative Url</param>
        /// <param name="httpClient">Http Client</param>
        /// <param name="queryParams">Query parameters</param>
        /// <param name="authorizationToken">JWT token for authorization</param>
        /// <typeparam name="T">Response Type</typeparam>
        /// <returns>Response data deserialized to T</returns>
        public static async Task<T> GetUsingMsgPackAsync<T>(string relativeUrl, HttpClient httpClient, IEnumerable<KeyValuePair<string, string>> queryParams, string authorizationToken = null)
        {
            if (queryParams != null)
            {
                var content = new FormUrlEncodedContent(queryParams);
                var urlEncodedString = await content.ReadAsStringAsync();
                relativeUrl += $"?{urlEncodedString}";
            }
            var response = await GetUsingMsgPackAsync(relativeUrl, httpClient, authorizationToken);
            httpClient.DefaultRequestHeaders.Clear();
            if (response.IsSuccessStatusCode && response.StatusCode == HttpStatusCode.OK)
            {
                var stream = await response.Content.ReadAsStreamAsync();
                return await MessagePackSerializer.DeserializeAsync<T>(stream);
            }
            return response.IsSuccessStatusCode && response.StatusCode == HttpStatusCode.NoContent
                ? default
                : throw new Exception($"GET Request failed with code: {response.StatusCode}, Url: {relativeUrl}");
        }

        /// <summary>
        /// Sends a GET http request
        /// </summary>
        /// <param name="relativeUrl">Relative Url</param>
        /// <param name="httpClient">Http Client</param>
        /// <param name="authorizationToken">JWT token for authorization</param>
        /// <returns>Http response</returns>
        public static async Task<HttpResponseMessage> GetUsingMsgPackAsync(string relativeUrl, HttpClient httpClient, string authorizationToken = null)
        {
            return await SendAsync(relativeUrl, HttpMethod.Get, httpClient, "application/x-msgpack", authorizationToken: authorizationToken);
        }
        #endregion

        #region POST methods
        /// <summary>
        /// Helper method to send a POST http request with json data. Json response is deserialized to T
        /// </summary>
        /// <param name="relativeUrl">Relative Url</param>
        /// <param name="data">Data to be posted</param>
        /// <param name="httpClient">Http Client</param>
        /// <param name="authorizationToken">JWT token for authorization</param>
        /// <typeparam name="T">Response Type</typeparam>
        /// <returns>Response data deserialized to T</returns>
        public static async Task<T> PostUsingJsonAsync<T>(string relativeUrl, object data, HttpClient httpClient, string authorizationToken = null)
        {
            var response = await PostUsingJsonAsync(relativeUrl, data, httpClient);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<T>();
            }
            else
            {
                throw new Exception($"POST Request failed with code: {response.StatusCode}, Url: {relativeUrl}");
            }
        }

        /// <summary>
        /// Sends a POST http request
        /// </summary>
        /// <remarks>
        /// Use <see cref="ApiHelper.PostUsingJsonAsync{T}(string, object, HttpClient)" /> to deserialize response automatically
        /// </remarks>
        /// <param name="relativeUrl">Relative Url</param>
        /// <param name="data">Data to be posted</param>
        /// <param name="httpClient">Http Client</param>
        /// <param name="authorizationToken">JWT token for authorization</param>
        /// <returns>Http Response</returns>
        public static async Task<HttpResponseMessage> PostUsingJsonAsync(string relativeUrl, object data, HttpClient httpClient, string authorizationToken = null)
        {
            var contentType = "application/json";
            var content = new StringContent(JsonSerializer.Serialize(data), Encoding.UTF8, contentType);
            return await SendAsync(relativeUrl, HttpMethod.Post, httpClient, contentType, content, authorizationToken);
        }

        /// <summary>
        /// Helper method to send a POST http request with MsgPack data. Response is deserialized to T
        /// </summary>
        /// <param name="relativeUrl">Relative Url</param>
        /// <param name="data">Data to be posted</param>
        /// <param name="httpClient">Http Client</param>
        /// <param name="authorizationToken">JWT token for authorization</param>
        /// <param name="queryParams">Query parameters</param>
        /// <typeparam name="T">Response Type</typeparam>
        /// <returns>Response data deserialized to T</returns>
        public static async Task<T> PostUsingMsgPackAsync<T>(string relativeUrl, object data, HttpClient httpClient, string authorizationToken = null, Dictionary<string, string> queryParams = null)
        {
            if (queryParams != null)
            {
                var content = new FormUrlEncodedContent(queryParams);
                var urlEncodedString = await content.ReadAsStringAsync();
                relativeUrl += $"?{urlEncodedString}";
            }
            var response = await PostUsingMsgPackAsync(relativeUrl, data, httpClient, authorizationToken);
            httpClient.DefaultRequestHeaders.Clear();
            if (response.IsSuccessStatusCode)
            {
                var stream = await response.Content.ReadAsStreamAsync();
                return await MessagePackSerializer.DeserializeAsync<T>(stream);
            }
            else if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                throw new UnauthorizedException();
            }
            else
            {
                throw new Exception($"Request failed. Url: {relativeUrl}, Status: {response.StatusCode}");
            }
        }

        /// <summary>
        /// Helper method to send a POST http request with MsgPack data. Response is deserialized to T
        /// </summary>
        /// <param name="relativeUrl">Relative Url</param>
        /// <param name="data">Data to be posted</param>
        /// <param name="httpClient">Http Client</param>
        /// <param name="authorizationToken">JWT token for authorization</param>
        /// <param name="queryParams">Query parameters</param>
        /// <returns>Response data deserialized to T</returns>
        public static async Task<HttpResponseMessage> PostUsingMsgPackAsync(string relativeUrl, object data, HttpClient httpClient, string authorizationToken, Dictionary<string, string> queryParams = null)
        {
            if (queryParams != null)
            {
                var content = new FormUrlEncodedContent(queryParams);
                var urlEncodedString = await content.ReadAsStringAsync();
                relativeUrl += $"?{urlEncodedString}";
            }
            return await PostUsingMsgPackAsync(relativeUrl, data, httpClient, authorizationToken);
        }

        /// <summary>
        /// Sends a POST http request
        /// </summary>
        /// <remarks>
        /// Use <see cref="ApiHelper.PostUsingMsgPackAsync(string, object, HttpClient)" /> to deserialize response automatically
        /// </remarks>
        /// <param name="relativeUrl">Relative Url</param>
        /// <param name="data">Data to be posted</param>
        /// <param name="httpClient">Http Client</param>
        /// <param name="authorizationToken">JWT token for authorization</param>
        /// <returns>Http Response</returns>
        public static async Task<HttpResponseMessage> PostUsingMsgPackAsync(string relativeUrl, object data, HttpClient httpClient, string authorizationToken = null)
        {
            var content = new ByteArrayContent(MessagePackSerializer.Serialize(data));
            content.Headers.ContentType = new MediaTypeWithQualityHeaderValue("application/x-msgpack");
            return await SendAsync(relativeUrl, HttpMethod.Post, httpClient, "application/x-msgpack", content, authorizationToken);
        }
        #endregion

        #region PUT methods
        public static async Task<HttpResponseMessage> PutUsingMsgPackAsync(string relativeUrl, object data, HttpClient httpClient, string authorizationToken = null, Dictionary<string, string> queryParams = null)
        {
            if (queryParams != null)
            {
                var formUrlContent = new FormUrlEncodedContent(queryParams);
                var urlEncodedString = await formUrlContent.ReadAsStringAsync();
                relativeUrl += $"?{urlEncodedString}";
            }
            var content = new ByteArrayContent(MessagePackSerializer.Serialize(data));
            content.Headers.ContentType = new MediaTypeWithQualityHeaderValue("application/x-msgpack");
            return await SendAsync(relativeUrl, HttpMethod.Put, httpClient, "application/x-msgpack", content, authorizationToken);
        }
        #endregion

        private static async Task<HttpResponseMessage> SendAsync(string relativeUrl, HttpMethod httpMethod, HttpClient httpClient, string contentType, HttpContent content = null, string authorizationToken = null)
        {
            if (!string.IsNullOrEmpty(authorizationToken))
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", authorizationToken);
            }
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept
                .Add(new MediaTypeWithQualityHeaderValue(contentType));
            var request = new HttpRequestMessage(httpMethod, relativeUrl);
            if (content != null)
            {
                request.Content = content;
            }
            return await httpClient.SendAsync(request);
        }
    }
}
