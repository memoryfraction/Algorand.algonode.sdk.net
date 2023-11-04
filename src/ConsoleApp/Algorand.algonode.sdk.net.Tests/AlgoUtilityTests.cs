
using Algorand.algonode.sdk.net.Clients;
using Algorand.algonode.sdk.net.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace Algorand.algonode.sdk.net.Tests
{
    [TestClass]
    public class AlgoUtilityTests
    {
        string _apiKey = "";
        string _testAlgoAddress = "";
        string _lfoAssetId = "";
        string _reserveAddress = "";
        string _hostAddress = "";
        AlgoNodeApiClientV2 _clientV2;

        public AlgoUtilityTests()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .AddUserSecrets<AlgoUtilityTests>();
            var config = builder.Build();

            _apiKey = config["Configuration:ApiKey"];
            _testAlgoAddress = config["Configuration:TestAlgoAddress"];
            _lfoAssetId = config["Configuration:LFOAssetId"];
            _reserveAddress = config["Configuration:ReserveAddress"];
            _hostAddress = config["Configuration:HostAddress"];

            _clientV2 = new AlgoNodeApiClientV2(_hostAddress);
        }



        [TestMethod]
        public void Deserialize_AccountHolding_Should_Work()
        {
            var responseStr = "{\"amount\":1298815227,\"asset-id\":721366337,\"is-frozen\":false}";
            var assetHolding = JsonConvert.DeserializeObject<AssetHolding>(responseStr);
            Assert.IsNotNull(assetHolding);
            Assert.AreEqual(721366337, assetHolding.AssetId);
            Assert.AreEqual(1298815227, assetHolding.Amount);
            Assert.AreEqual(false, assetHolding.IsFrozen);
        }

        [TestMethod]
        public void Deserialize_AccountAsset_Should_Work()
        {
            var responseStr = "{\"asset-holding\":{\"amount\":1298815227,\"asset-id\":721366337,\"is-frozen\":false},\"round\":33390845}";
            var account = JsonConvert.DeserializeObject<AlgoAccountAsset>(responseStr);
            Assert.IsNotNull(account.AssetHolding);
            Assert.IsNotNull(account);
        }


        [TestMethod]
        public void TypeConvert_Should_Work()
        {
            long amount = 247082437;
            double actualAmount = amount / 1000000.0;
            Assert.IsTrue(actualAmount > 0);
        }

    }
}
