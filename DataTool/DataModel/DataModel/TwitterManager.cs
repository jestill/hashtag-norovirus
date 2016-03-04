using Newtonsoft.Json;
using Norovirus.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Norovirus
{
    public class TwitterManager
    {
        private const string consumerKey = "abG7oJ4y6mBUMRkQQWqYFavOw";
        private const string consumerSecret = "m4CgQndRSYchq7FvB06UiUDItnuipU3IwgCSRcH1dVxWYI3BvE";

        public async Task GetAccessToken()
        {
            var tokenUrl = "https://api.twitter.com/oauth2/token/";
            using (var client = new HttpClient())
            {
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, tokenUrl);

                client.BaseAddress = new Uri(tokenUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Base64Encode(consumerKey + ':' + consumerSecret));

                var requestContent = string.Format("grant_type={0}", Uri.EscapeDataString("client_credentials"));
                request.Content = new StringContent(requestContent, Encoding.UTF8, "application/x-www-form-urlencoded");
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsAsync<Response>();
                System.Diagnostics.Debug.WriteLine("Here");
                // Could encrypt and stash token somewhere
            }
        }

        public async Task<TwitterResponse> GetApiData(string parameters)
        {
            //var parameters = "?q=%norovirus";
            var apiUrl = "https://api.twitter.com/1.1/search/tweets.json?" + parameters;
            var bearerToken = "AAAAAAAAAAAAAAAAAAAAALPRtwAAAAAAlmn4QM4g1hD%2FPhD9HN7IlV1F2Wo%3DzM3hSPFRwCyocPcqKCsk9rtLX6zxfiVX4B0RG5kf1N1g02W06a";
            
            using (var client = new HttpClient())
            {
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, apiUrl);

                client.BaseAddress = new Uri(apiUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken);

                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsAsync<TwitterResponse>();
                //var content = await response.Content.ReadAsStringAsync();
                return content;
            }
        }

        private string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }
    }

    public class Response
    {
        [JsonProperty("token_type")]
        public string TokenType { get; set; }

        [JsonProperty("access_token")]
        public string AccessToken { get; set; }
    }
}