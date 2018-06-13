using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CoinIRRial
{
    public class coinmarketcap
    {
        public class Allcoinmarketcap
        {
            public string name { get; set; }
            public string symbol { get; set; }
            public string rank { get; set; }
            public string price_usd { get; set; }
            public string price_btc { get; set; }
            public string market_cap_usd { get; set; }
            public string market_Dive_usd { get; set; }
        }

        public List<Allcoinmarketcap> GetCurrency()
        {
            try
            {
                string MyUrl = "https://api.coinmarketcap.com/v1/ticker/?limit=1500";

                string ResultService = GET(MyUrl);

                List<Allcoinmarketcap> AResultService = JsonConvert.DeserializeObject<List<Allcoinmarketcap>>(ResultService);

                for (int i = 0; i < AResultService.Count; i++)
                {
                    double Aup = Convert.ToDouble(AResultService[i].price_usd);
                    double ADown = Convert.ToDouble(AResultService[i].market_cap_usd);

                    if (ADown != 0)
                        AResultService[i].market_Dive_usd = (Aup / ADown).ToString();
                }

                //AminGrid.ItemsSource = AResultService.ToList<Allcoinmarketcap>();

                return AResultService;
            }
            catch (Exception ex)
            {
                return new List<Allcoinmarketcap>();
            }
        }

        string GET(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            try
            {
                WebResponse response = request.GetResponse();
                using (Stream responseStream = response.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);
                    return reader.ReadToEnd();
                }
            }
            catch (WebException ex)
            {
                WebResponse errorResponse = ex.Response;
                using (Stream responseStream = errorResponse.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(responseStream, Encoding.GetEncoding("utf-8"));
                    String errorText = reader.ReadToEnd();
                    // log errorText
                }
                return "";
                throw;
            }
        }
    }
}
