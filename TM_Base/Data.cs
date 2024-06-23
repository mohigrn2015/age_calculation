using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TM_Console.TM_Base
{
    internal class Data
    {
        public async void GetData()
        {
            TextWriter textWriter = new TextWriter();
            try
            {
                HttpClient _httpClient = new HttpClient();
                var url = "https://reqres.in/api/users?page=2";
                var response = await _httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    var json = JsonConvert.SerializeObject(data);
                    await textWriter.LogV2Async(json);
                }
                else
                {
                    await textWriter.LogV2Async("No Data found!");
                }
            }
            catch (Exception)
            {
                throw;
            }            
        }
    }
}
