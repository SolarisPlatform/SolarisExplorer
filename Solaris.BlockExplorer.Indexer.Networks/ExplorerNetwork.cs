using Microsoft.Extensions.Configuration;
using NBitcoin;

namespace Solaris.BlockExplorer.Indexer.Networks
{
    public class ExplorerNetwork : Network
    {
        public ExplorerNetwork(IConfiguration configuration)
        {
            //this.CoinTicker = config.CoinTag;

            var consensusFactory = new ConsensusFactory();
                
            //Consensus = new ConsensusConfig(config, consensusFactory);

            var networkConfiguration = configuration.GetSection("Network");
            var pubkeyAddressPrefix = networkConfiguration.GetValue<byte>("PubkeyAddressPrefix");

            Base58Prefixes = new byte[12][];
            Base58Prefixes[(int)Base58Type.PUBKEY_ADDRESS] = new[] { pubkeyAddressPrefix };
            //Base58Prefixes[(int)Base58Type.SCRIPT_ADDRESS] = new byte[] { (config.NetworkScriptAddressPrefix) };
        }
    }
}
