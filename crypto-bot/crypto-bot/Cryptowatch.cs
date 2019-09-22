using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.WebSockets;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Collections;

namespace crypto_bot
{
    public class Allowance
    {
        public int cost { get; set; }
        public long remaining { get; set; }
    }
    public class ExchangeResult
    {
        public int id { get; set; }
        public string symbol { get; set; }
        public string name { get; set; }
        public string route { get; set; }
        public bool active { get; set; }
    }    
    public class ExchangeRoot
    {
        public List<ExchangeResult> result { get; set; }
        public Allowance allowance { get; set; }
    }
    public class MarketResult
    {
        public int id { get; set; }
        public string exchange { get; set; }
        public string pair { get; set; }
        public bool active { get; set; }
        public string route { get; set; }
    }
    public class MarketRoot
    {
        public List<MarketResult> result { get; set; }
        public Allowance allowance { get; set; }
    }

    public class Cryptowatch
    {
        private static HttpClient client = new HttpClient();

        public async Task<ArrayList> getMarket(string param)
        {
            ArrayList result = new ArrayList();
            try
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await client.GetAsync("https://api.cryptowat.ch/markets/"+param);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                MarketRoot jsonResult = JsonConvert.DeserializeObject<MarketRoot>(responseBody);                
                foreach (var item in jsonResult.result)
                {
                    result.Add(item.pair);
                }
            }
            catch (Exception a)
            {
                Console.Write("+++GET MARKET ERROR: CRASH: " + a);
            }
            return result;
        }

        public async Task<ArrayList> getExchanges()
        {
            ArrayList result = new ArrayList();
            try
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await client.GetAsync("https://api.cryptowat.ch/exchanges");
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                ExchangeRoot jsonResult = JsonConvert.DeserializeObject<ExchangeRoot>(responseBody);
                
                foreach (var item in jsonResult.result)
                {
                    result.Add(item.name);
                }                
            }
            catch(Exception a)
            {
                Console.Write("+++GET MARKET ERROR: CRASH: " + a);
            }
            return result;
        }
    }
}
      
