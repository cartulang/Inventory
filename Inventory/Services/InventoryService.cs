using Inventory.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Services
{
    public class InventoryService
    {
        private RestClient _restClient = null!;
        private readonly string baseUrl = "https://sheetdb.io/api/v1/gf2ahqr80s8lq";
        public async Task<List<Input>> GetInventory()
        {
            _restClient = new (baseUrl);
            var request = new RestRequest();
            var response = await _restClient.GetAsync(request);
            string? content = response.Content;
            var json = JsonConvert.DeserializeObject<List<Input>>(content);
            return json;
        }

        public async Task<bool> AddInput(Input input)
        {
            string inputJson = JsonConvert.SerializeObject(input);
            InputDto inputDto = new InputDto()
            {
                data = new JRaw($"[{inputJson}]")
            };
            var body = new InputDto();

            string json = JsonConvert.SerializeObject(inputDto, Formatting.Indented);
            _restClient = new(baseUrl);
            var request = new RestRequest().AddJsonBody(json);
            var response = await _restClient.PostAsync(request);
            return response.IsSuccessful;
        }
    }
}
