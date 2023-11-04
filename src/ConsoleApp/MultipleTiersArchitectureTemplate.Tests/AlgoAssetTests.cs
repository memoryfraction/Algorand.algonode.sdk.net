using Algorand.algonode.sdk.net.Clients;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultipleTiersArchitectureTemplate.Tests
{
    [TestClass]
    public class AlgoAssetTests
    {
        string _apiKey = "";
        string _testAlgoAddress = "";
        string _lfoAssetId = "";
        string _reserveAddress = "";
        string _hostAddress = "";
        AlgoNodeApiClientV2 _clientV2;

        public AlgoAssetTests()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .AddUserSecrets<AlgoAssetTests>();
            var config = builder.Build();

            _apiKey = config["Configuration:ApiKey"];
            _testAlgoAddress = config["Configuration:TestAlgoAddress"];
            _lfoAssetId = config["Configuration:LFOAssetId"];
            _reserveAddress = config["Configuration:ReserveAddress"];
            _hostAddress = config["Configuration:HostAddress"];

            _clientV2 = new AlgoNodeApiClientV2(_hostAddress);
        }

        [TestMethod]
        public async Task GetAsset_Should_Work()
        {
            var asset = await _clientV2.GetAssetInformationAsync(_lfoAssetId);
            Assert.IsTrue(asset.Params.Total == 10000000000000);
            Assert.IsTrue(asset.Params.Name == "LeaderFundOne");
        }


    }
}
