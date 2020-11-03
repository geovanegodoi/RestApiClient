using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace RestApiClient
{
    public class RestApiClient
    {
        private static HttpClient _httpClient = new HttpClient();
        private string _token;
        private string _baseUri;

        protected RestApiClient(string baseUri)
        {
            _baseUri = baseUri;
            InitializeTlsProtocol();
        }

        private void InitializeTlsProtocol()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 |
                                                   SecurityProtocolType.Tls11 |
                                                   SecurityProtocolType.Tls;
        }

        public static RestApiClient Build(string baseUri)
        {
            return new RestApiClient(baseUri);
        }

        public RestApiClient WithToken(string token)
        {
            _token = token;
            return this;
        }

        public async Task<Out> GetAsync<Out>(Dictionary<string, string> queryString)
        {
            PrepareHeaderRequest();
            
            var finalUri = _baseUri + queryString.ToQueryString();

            using (var response = await _httpClient.GetAsync(finalUri))
            {
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsAsync<Out>();
                }
                throw new ApiException(response.ReasonPhrase, _baseUri, "GET");
            }
        }

        public async Task<Out> PostAsync<In, Out>(In body)
        {
            PrepareHeaderRequest();

            var content = new StringContent(body.ToJson(), Encoding.UTF8, "application/json");

            using (var response = await _httpClient.PostAsync(_baseUri, content))
            {
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsAsync<Out>();
                }
                throw new ApiException(response.ReasonPhrase, _baseUri, "POST");
            }
        }

        public async Task<Out> PutAsync<In, Out>(In body)
        {
            PrepareHeaderRequest();

            var content = new StringContent(body.ToJson(), Encoding.UTF8, "application/json");

            using (var response = await _httpClient.PutAsync(_baseUri, content))
            {
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsAsync<Out>();
                }
                throw new ApiException(response.ReasonPhrase, _baseUri, "PUT");
            }
        }

        public async Task DeleteAsync()
        {
            PrepareHeaderRequest();

            using (var response = await _httpClient.DeleteAsync(_baseUri))
            {
                if (!response.IsSuccessStatusCode)
                {
                    throw new ApiException(response.ReasonPhrase, _baseUri, "DELETE");
                }
                
            }
        }

        private void PrepareHeaderRequest()
        {
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.CacheControl = new CacheControlHeaderValue { NoCache = true };
            if (!_token.IsNullOrEmpty())
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
            }
            if (_baseUri.IsNullOrEmpty())
            {
                throw new ApiException("Base URI not set");
            }
        }
    }
}
