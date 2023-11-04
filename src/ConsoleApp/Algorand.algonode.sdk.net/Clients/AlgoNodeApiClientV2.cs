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
        /// <summary>
        /// v2/accounts/account-id
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        public async Task<AlgoAccountAsset> GetAccountInformationAsync(string assetId, string address)
        {
            // Create a GET request
            var request = new RestRequest($"/{_apiVersion}/accounts/{address}/assets/{assetId}", Method.Get);

            // Execute the request and await the response
            var response = await _client.ExecuteGetAsync(request);

            AlgoAccountAsset account = new AlgoAccountAsset();
            // Check the response status
            if (response.IsSuccessful)
            {
                // Deserialize the response content into an Asset object
                if ( !string.IsNullOrEmpty(response?.Content) && response?.Content?.Length > 0)
                    account = JsonConvert.DeserializeObject<AlgoAccountAsset>(response.Content);

                return account;
            }
            else
            {
                throw new Exception("Request failed: " + response.ErrorMessage);
            }
        }


        #endregion


        #region Asset

        public async Task<Asset> GetAssetInformationAsync(string assetId)
        {
            // Create a GET request
            var request = new RestRequest($"/{_apiVersion}/assets/{assetId}", Method.Get);

            // Execute the request and await the response
            var response = await _client.ExecuteGetAsync(request);

            Asset asset = new Asset();
            // Check the response status
            if (response.IsSuccessful)
            {
                // Deserialize the response content into an Asset object
                if (!string.IsNullOrEmpty(response?.Content) && response?.Content?.Length > 0)
                    asset = JsonConvert.DeserializeObject<Asset>(response?.Content);

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
