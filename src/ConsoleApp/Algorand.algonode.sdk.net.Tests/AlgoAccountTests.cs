using Algorand.algonode.sdk.net.Clients;
using Microsoft.Extensions.Configuration;


namespace Algorand.algonode.sdk.net.Tests
{
    [TestClass]
    public class AlgoAccountTests
    {
        string _apiKey = "";
        string _testAlgoAddress = "";
        string _lfoAssetId = "";
        string _reserveAddress = "";
        string _hostAddress = "";
        AlgoNodeApiClientV2 _clientV2;

        public AlgoAccountTests()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .AddUserSecrets<AlgoAccountTests>();
            var config = builder.Build();

            _apiKey = config["Configuration:ApiKey"];
            _testAlgoAddress = config["Configuration:TestAlgoAddress"];
            _lfoAssetId = config["Configuration:LFOAssetId"];
            _reserveAddress = config["Configuration:ReserveAddress"];
            _hostAddress = config["Configuration:HostAddress"];

            _clientV2 = new AlgoNodeApiClientV2(_hostAddress);
        }

        [TestMethod]
        public async Task GetAccountInfo_Should_Work()
        {
            var account = await _clientV2.GetAccountInformationAsync(_lfoAssetId, _testAlgoAddress);
            Assert.IsTrue(account.AssetHolding.Amount > 1);
        }

    }
}
