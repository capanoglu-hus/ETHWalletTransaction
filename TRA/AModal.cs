

using Nethereum.Hex.HexTypes;
using System.Numerics;

namespace TRA
{
    public class AModal
    {

        public string TransAddress { get; set; } = string.Empty;

        public string? TransactionFrom { get; set; }

        public string? TransactionTo { get; set; }

        public decimal? TransactionAmount { get; set; }

        public decimal? GasPrice { get; set; }


    }

}
