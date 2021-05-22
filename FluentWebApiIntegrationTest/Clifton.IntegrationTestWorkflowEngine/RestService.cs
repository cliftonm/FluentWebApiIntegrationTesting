using System;
using System.Net;

using Newtonsoft.Json;
using RestSharp;

using Clifton.Core.ExtensionMethods;

namespace Clifton.IntegrationTestWorkflowEngine
{
    public static class RestService
    {
        public static (HttpStatusCode status, string content) Get(string url)
        {
            var response = Execute(Method.GET, url);

            return (response.StatusCode, response.Content);
        }

        public static (T item, HttpStatusCode status, string content) Get<T>(string url) where T : new()
        {
            var response = Execute(Method.GET, url);
            T ret = TryDeserialize<T>(response);

            return (ret, response.StatusCode, response.Content);
        }

        public static (HttpStatusCode status, string content) Post(string url, object data = null)
        {
            var response = Execute(Method.POST, url, data);

            return (response.StatusCode, response.Content);
        }

        private static IRestResponse Execute(Method method, string url, object data = null)
        {
            var client = new RestClient(url);
            var request = new RestRequest(method);
            data.IfNotNull(() => request.AddJsonBody(data));
            var response = client.Execute(request);

            return response;
        }

        private static T TryDeserialize<T>(IRestResponse response) where T : new()
        {
            T ret = new T();
            int code = (int)response.StatusCode;

            if (code >= 200 && code < 300)
            {
                ret = JsonConvert.DeserializeObject<T>(response.Content);
            }

            return ret;
        }
    }
}
