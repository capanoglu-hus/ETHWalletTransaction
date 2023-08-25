using Microsoft.AspNetCore.Mvc;
using Nethereum.ABI.Util;
using Nethereum.BlockchainProcessing.BlockStorage.Entities;
using Nethereum.BlockchainProcessing.Processor;
using Nethereum.Hex.HexTypes;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Web3;
using Nethereum.Web3.Accounts;
using NUnit.Framework;
using Org.BouncyCastle.Asn1.Pkcs;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Transactions;
using TRA;


namespace TRA.Controllers
{
    [Route("api/controller")]
    [ApiController]
    public class Controller : ControllerBase
    {
        public static AModal aModal = new AModal();
        public static TModal tModal = new TModal();

        private readonly IConfiguration _configuration;

        public Controller(IConfiguration configuration)
        {
            _configuration = configuration;

        }



        [HttpPost("PostHash")]
        public async Task<ActionResult<AModal>> PostHash(AModal request)
        {
            var web3 = new Web3("https://mainnet.infura.io/v3/18d8f1f384554bad900d50f8a51c3bad"); //infura baðlantýsý

            var transaction = await web3.Eth.Transactions.GetTransactionByHash.SendRequestAsync(request.TransAddress); //girilen adrese göre iþlem hashlarýný getirir

            var amount = transaction.Value;

            var gas = transaction.GasPrice;

            aModal.TransAddress = request.TransAddress;
            aModal.TransactionFrom = transaction.From;
            aModal.TransactionTo = transaction.To;
            aModal.TransactionAmount = Web3.Convert.FromWei(amount.Value);
            aModal.GasPrice = Web3.Convert.FromWei(gas.Value);



            return aModal;
        }


        [HttpPost("PostAdres")]
        public async Task<ActionResult<TModal>> PostAdres(TModal request)
        {

            var transactions = new List<TransactionReceiptVO>(); //yapýlan iþlemlerin dizi gibi tutulmasý 
            var filterLogs = new List<FilterLogVO>();

            var web3 = new Web3("https://mainnet.infura.io/v3/18d8f1f384554bad900d50f8a51c3bad");

          
            //gönderen adresinin blokta bulunan iþlemlerle karþýlaþtýrýlmasý
            var processor = web3.Processing.Blocks.CreateBlockProcessor(steps =>
            {
                steps.TransactionStep.SetMatchCriteria(t =>
                    t.Transaction.IsFrom(request.Wallet));
                steps.TransactionReceiptStep.AddSynchronousProcessorHandler(tx => transactions.Add(tx));
              
            });

            
            var cancellationToken = new CancellationToken();
            //blok numarasýna göre 
            await processor.ExecuteAsync(
                toBlockNumber: new BigInteger(17985226),
                cancellationToken: cancellationToken,
                startAtBlockNumberIfNotProcessed: new BigInteger(17985226));

           
         

           tModal.Wallet = request.Wallet;
           tModal.TransCount = transactions.Count;
           tModal.Trans = transactions[0].TransactionHash;

            return tModal;
        }

    }



    

}




