using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Gohopo.DnTools.Net
{
    public static class HttpHelper
    {
        public static async Task<string> GetAsync(string url, Dictionary<string, string> headers = null)
        {
            using (HttpClient client = new HttpClient())
            {
                if (headers != null)
                {
                    foreach (var header in headers)
                    {
                        client.DefaultRequestHeaders.Add(header.Key, header.Value);
                    }
                }
                return await client.GetStringAsync(url);
            }
        }
        public static async Task<T> GetAsync<T>(string url, Dictionary<string, string> headers = null)
        {
            var result = await GetAsync(url, headers);
            return JsonConvert.DeserializeObject<T>(result);
        }
        public static async Task<string> PostAsync(string url, string body, string contentType = null, Dictionary<string, string> headers = null)
        {
            using (HttpClient client = new HttpClient())
            {
                if (headers != null)
                {
                    foreach (var header in headers)
                    {
                        client.DefaultRequestHeaders.Add(header.Key, header.Value);
                    }
                }
                using (HttpContent content = new StringContent(body, Encoding.UTF8))
                {
                    if (contentType != null)
                    {
                        content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(contentType);
                    }
                    HttpResponseMessage response = await client.PostAsync(url, content);
                    return await response.Content.ReadAsStringAsync();
                }
            }
        }
        public static async Task<T> PostAsync<T>(string url, string body, string contentType = null, Dictionary<string, string> headers = null)
        {
            var result = await PostAsync(url, body, contentType, headers);
            return JsonConvert.DeserializeObject<T>(result);
        }
    }
}
