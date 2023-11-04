using Algorand.algonode.sdk.net.Models;
using Algorand.algonode.sdk.net.Models.Api.Response;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;

namespace Algorand.algonode.sdk.net.Clients
{
    public class AlgoNodeApiClientV2 
    {
        private string _apiVersion = "v2";
        private readonly RestClient _client;
        private string _hostAddress;

        public string ApiVersion { get => _apiVersion; set => _apiVersion = value; }
        public string HostAddress { get => _hostAddress; set => _hostAddress = value; }

        public AlgoNodeApiClientV2(string hostAddress)
        {
            _hostAddress = hostAddress;
            _client = new RestClient(_hostAddress);
        }

        #region Account



        #endregion


        #region Asset

        public async Task<Asset> GetAssetInformationAsync(string assetId)
        {
            // Create a GET request
            var request = new RestRequest($"/v2/assets/{assetId}", Method.Get);

            // Execute the request and await the response
            var response = await _client.ExecuteGetAsync(request);

            Asset asset = new Asset();
            // Check the response status
            if (response.IsSuccessful)
            {
                // Deserialize the response content into an Asset object
                if (response.Content.Length > 0)
                    asset = JsonConvert.DeserializeObject<Asset>(response.Content);

                return asset;
            }
            else
            {
                throw new Exception("Request failed: " + response.ErrorMessage);
            }
        }

        #endregion


        private string FormatError(Exception ex)
            => $"Exception: {ex.Message} | StackTrace: {ex.StackTrace}";

    }
}
