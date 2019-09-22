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
    public class OfferChange
    {
        public double percentage { get; set; }
        public double absolute { get; set; }
    }
    public class OfferPrice
    {
        public double last { get; set; }
        public double high { get; set; }
        public double low { get; set; }
        public OfferChange change { get; set; }
    }
    public class OfferResult
    {
        public OfferPrice price { get; set; }
        public double volume { get; set; }
        public double volumeQuote { get; set; }
    }
    public class OfferRoot
    {
        public OfferResult result { get; set; }
        public Allowance allowance { get; set; }
    }

    public class Cryptowatch
    {
        private static HttpClient client = new HttpClient();

        public async Task<ArrayList> GetOffer(string market,string pair)
        {
            ArrayList result = new ArrayList();
            try
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await client.GetAsync("https://api.cryptowat.ch/markets/"+market+"/"+ pair+"/summary");
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                OfferRoot jsonResult = JsonConvert.DeserializeObject<OfferRoot>(responseBody);
                /*Result Structure
                Exchange(0) | pair(1)
                    "Offer Volume:" volume(2) | "% Change:" change(3)
                            | "Last Price:" last(4) 
                            | "Max Price:" max(5)
                            | "Min Price:" min(6)
                 */
                result.Add(market); result.Add(pair); result.Add(jsonResult.result.volume); result.Add(jsonResult.result.price.change.percentage);
                result.Add(jsonResult.result.price.last); result.Add(jsonResult.result.price.high); result.Add(jsonResult.result.price.low);
            }
            catch (Exception a)
            {
                Console.Write("+++GET MARKET ERROR: CRASH: " + a);
            }
            return result;
        }

        public async Task<ArrayList> GetMarket(string param)
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

        public async Task<ArrayList> GetExchanges()
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
      
