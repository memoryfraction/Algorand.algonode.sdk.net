using Algorand.algonode.sdk.net.Clients;
using Microsoft.Extensions.Configuration;

namespace Algorand.algonode.sdk.net.Tests
{
    [TestClass]
    public class AlgoIntegratedTests
    {
        string _apiKey = "";
        string _testAlgoAddress = "";
        string _lfoAssetId = "";
        string _reserveAddress = "";
        string _hostAddress = "";
        AlgoNodeApiClientV2 _clientV2;

        public AlgoIntegratedTests()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .AddUserSecrets<AlgoIntegratedTests>();
            var config = builder.Build();

            _apiKey = config["Configuration:ApiKey"];
            _testAlgoAddress = config["Configuration:TestAlgoAddress"];
            _lfoAssetId = config["Configuration:LFOAssetId"];
            _reserveAddress = config["Configuration:ReserveAddress"];
            _hostAddress = config["Configuration:HostAddress"];

            _clientV2 = new AlgoNodeApiClientV2(_hostAddress);
        }
       

        [TestMethod]
        public async Task GetAssetBalance_Should_Work()
        {
            //1 get asset decimals
            var lfoAssetInfo = await _clientV2.GetAssetInformationAsync(_lfoAssetId);
            var decimals = lfoAssetInfo.Params.Decimals;

            //2 get asset amount
            var lfoAccount = await _clientV2.GetAccountInformationAsync(_lfoAssetId, _testAlgoAddress);

            //3 asset actual balance = amount / decimals
            lfoAccount.Balance = lfoAccount.AssetHolding.Amount / (double)(Math.Pow(10, decimals));
            Assert.IsTrue(lfoAccount.Balance > 0);
        }



        [TestMethod]
        public async Task GetTotalCirculation_Should_Work()
        {
            //1 Get total/ 10^decimal
            var lfoAssetInfo = await _clientV2.GetAssetInformationAsync(_lfoAssetId);
            var decimals = lfoAssetInfo.Params.Decimals;
            var total = lfoAssetInfo.Params.Total;
            var totalSupply = (double)total / (double)Math.Pow(10, decimals);

            //2 Get amount from reserve account
            var reserveAddrLfo = await _clientV2.GetAccountInformationAsync (_lfoAssetId, _reserveAddress);
            reserveAddrLfo.Balance = (double)reserveAddrLfo.AssetHolding.Amount / (double)(Math.Pow(10, decimals));

            //3 CirculatingSupply(流通代币总量) = totalSupply(总发行量) - reserveBalance(储备账户发行总量)
            var CirculatingSupply = totalSupply - reserveAddrLfo.Balance;
            Assert.IsTrue(CirculatingSupply > 20000);
        }



    }
}