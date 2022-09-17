using Inventory.Dto;
using Inventory.Models;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Services
{
    public class DeviceService
    {
        private RestClient _restClient = null!;
        private readonly string _baseUrl = "http://localhost:8001/device";

        public async Task<IEnumerable<Device>> GetAllDevice()
        {
            _restClient = new(_baseUrl);
            var request = new RestRequest();
            var response = await _restClient.GetAsync(request);
            var deviceList = JsonConvert.DeserializeObject<List<Device>>(response?.Content);
            return deviceList;
        }

        public async Task<bool> AddDevice(DeviceDto device)
        {
            _restClient = new(_baseUrl);
            var request = new RestRequest();
            request.AddBody(device);
            
            try
            {
                var response = await _restClient.PostAsync(request);
                return true;
            } 
            catch(Exception)
            {
                return false;
            }
            
        }
    }
}
