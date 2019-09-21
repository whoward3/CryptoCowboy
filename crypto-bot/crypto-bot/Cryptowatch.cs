using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.WebSockets;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;


namespace crypto_bot
{
    public class Cryptowatch
    {
        private string assets;
        private string pairs;
        private string exchanges;
        private string markets;
        private Uri BaseAddress;
        private readonly HttpClient client = new HttpClient();

        public void Crytowatch() {
            {
                BaseAddress = new Uri("https://api.cryptowat.ch");
            }
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public void getAssetss()
        {

        }
        public void getPairs()
        {

        }
        public void getExchanges()
        {

        }
        public void getMarkets()
        {

        }
    }
}
      
