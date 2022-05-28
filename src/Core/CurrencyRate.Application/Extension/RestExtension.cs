using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyRate.Application.Extension
{
    public class RestExtension : IRestExtension
    {
        private readonly RestClient _restClient;

        public RestExtension(RestClient restClient)
        {
            _restClient = restClient;
        }

        public async Task<RestResponse> Get(string url)
        {
            var request = new RestRequest(url, Method.Get);
            return await _restClient.ExecuteGetAsync(request);
        }
    }
}
