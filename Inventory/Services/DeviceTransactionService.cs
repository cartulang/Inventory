using Inventory.Models;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Inventory.Services
{
    public class DeviceTransactionService
    {
        private RestClient _restClient = null!;
        private readonly string _baseUrl = "http://localhost:8001/transaction";
        public async Task<List<DeviceTransaction>> GetAllTransactions()
        {
            _restClient = new(_baseUrl);
            var request = new RestRequest();
            var response = await _restClient.GetAsync(request);
            var transactionList = JsonConvert.DeserializeObject<List<DeviceTransaction>>(response?.Content);
            return transactionList;
        }
    }
}
