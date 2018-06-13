using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using System.Windows.Threading;
using Newtonsoft.Json;
using static CoinIRRial.AllWindow;
using static CoinIRRial.coinmarketcap;

namespace CoinIRRial
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static Dictionary<string, CoinPrice> CoinsPrice = new Dictionary<string, CoinPrice>();
        private static List<AminCart> AminCarts = new List<AminCart>();

        DispatcherTimer TimerState;
        DispatcherTimer TimerGetMoney;

        class AminCart
        {
            public string CSubject { get; set; }
            public string CName { get; set; }
            public double CCount { get; set; }
            public double CUSDPrice { get; set; }
            public double CIIRPrice { get; set; }
        }

        public MainWindow()
        {
            InitializeComponent();

            TXTIRRPrice.Text = GetIRR().ToString();

            TimerGetMoney = new DispatcherTimer();
            TimerGetMoney.Tick += new EventHandler(delegate (object s, EventArgs a)
            {
                FillCurrency();
            });

            TimerState = new DispatcherTimer();
            TimerState.Tick += new EventHandler(delegate (object s, EventArgs a)
            {
                AllCoordination();
            });
        }

        private void AllCoordination()
        {
            LVCoinprice.ItemsSource = CoinsPrice.Values.ToList<CoinPrice>();
            AminGrid.ItemsSource = AminCarts.ToList<AminCart>();

            double sumIRR = 0;
            double sumUSD = 0;
            for (int q = 0; q < AminCarts.Count; q++)
            {
                sumUSD += Convert.ToDouble(AminCarts[q].CUSDPrice);
                sumIRR += Convert.ToDouble(AminCarts[q].CIIRPrice);
            }

            TXTSUMIRRPrice.Text = SepratNumber(Convert.ToInt64(Math.Round(sumIRR)));
            TXTSUMUSDPrice.Text = SepratNumber(Convert.ToInt64(Math.Round(sumUSD)));
        }

        public static string SepratNumber(long InputNumber)
        {
            string Result = InputNumber.ToString("N0", new System.Globalization.NumberFormatInfo()
            {
                NumberGroupSizes = new[] { 3 },
                NumberGroupSeparator = ","
            });

            return Result;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            FillCurrency();

            TimerGetMoney.Interval = TimeSpan.FromSeconds(60);
            TimerGetMoney.Start();

            TimerState.Interval = TimeSpan.FromSeconds(30);
            TimerState.Start();
        }


        public void FillCurrency()
        {
            AminCarts.Clear();


            AminCarts.Add(new AminCart { CSubject = "cryptonator", CName = "BTC", CCount = 0.00003952 });
            AminCarts.Add(new AminCart { CSubject = "cryptonator", CName = "BCN", CCount = 40000 });
            AminCarts.Add(new AminCart { CSubject = "cryptonator", CName = "DOGE", CCount = 46058.8235294 });
            AminCarts.Add(new AminCart { CSubject = "cryptonator", CName = "XRP", CCount = 896 });

            AminCarts.Add(new AminCart { CSubject = "cryptopia", CName = "BTC", CCount = 0.00000001 });
            AminCarts.Add(new AminCart { CSubject = "cryptopia", CName = "ETN", CCount = 5000 });
            AminCarts.Add(new AminCart { CSubject = "cryptopia", CName = "GAY", CCount = 1000 });

            AminCarts.Add(new AminCart { CSubject = "hitbtc", CName = "BCC", CCount = 4 });

            AminCarts.Add(new AminCart { CSubject = "tradesatoshi", CName = "RDD", CCount = 10000 });

            AminCarts.Add(new AminCart { CSubject = "kucoin", CName = "DGB", CCount = 6100.82023655 });
            AminCarts.Add(new AminCart { CSubject = "kucoin", CName = "POWR", CCount = 200 });

            AminCarts.Add(new AminCart { CSubject = "livecoin", CName = "MGO", CCount = 300 });
            AminCarts.Add(new AminCart { CSubject = "livecoin", CName = "SXC", CCount = 834.53847023 });

            AminCarts.Add(new AminCart { CSubject = "binance", CName = "XVG", CCount = 20000 });
            AminCarts.Add(new AminCart { CSubject = "binance", CName = "TRX", CCount = 4996.80000000 });
            AminCarts.Add(new AminCart { CSubject = "binance", CName = "XLM", CCount = 2129.26900000 });
            AminCarts.Add(new AminCart { CSubject = "binance", CName = "ADA", CCount = 1000.64000000 });
            AminCarts.Add(new AminCart { CSubject = "binance", CName = "REQ", CCount = 999.00000000 });
            AminCarts.Add(new AminCart { CSubject = "binance", CName = "ZRX", CCount = 249.85000000 });
            AminCarts.Add(new AminCart { CSubject = "binance", CName = "MIOTA", CCount = 199.86000000 });
            AminCarts.Add(new AminCart { CSubject = "binance", CName = "NAV", CCount = 149.85000000 });
            AminCarts.Add(new AminCart { CSubject = "binance", CName = "XRP", CCount = 104.37300000 });
            AminCarts.Add(new AminCart { CSubject = "binance", CName = "ICX", CCount = 99.92000000 });
            AminCarts.Add(new AminCart { CSubject = "binance", CName = "VEN", CCount = 50.03400000 });
            AminCarts.Add(new AminCart { CSubject = "binance", CName = "ETHOS", CCount = 50.02400000 });
            AminCarts.Add(new AminCart { CSubject = "binance", CName = "LSK", CCount = 43.16480000 });
            AminCarts.Add(new AminCart { CSubject = "binance", CName = "KMD", CCount = 30.00000000 });
            AminCarts.Add(new AminCart { CSubject = "binance", CName = "WTC", CCount = 30.00000000 });
            AminCarts.Add(new AminCart { CSubject = "binance", CName = "BNB", CCount = 19.89897036 });
            AminCarts.Add(new AminCart { CSubject = "binance", CName = "QTUM", CCount = 9.99400000 });
            AminCarts.Add(new AminCart { CSubject = "binance", CName = "GAS", CCount = 3.00479220 });
            AminCarts.Add(new AminCart { CSubject = "binance", CName = "NEO", CCount = 1.99800000 });
            AminCarts.Add(new AminCart { CSubject = "binance", CName = "XVG", CCount = 1.30000000 });
            AminCarts.Add(new AminCart { CSubject = "binance", CName = "DNT", CCount = 0.60000000 });
            AminCarts.Add(new AminCart { CSubject = "binance", CName = "SNT", CCount = 0.48000000 });
            AminCarts.Add(new AminCart { CSubject = "binance", CName = "QSP", CCount = 0.34000000 });
            AminCarts.Add(new AminCart { CSubject = "binance", CName = "BTS", CCount = 0.32000000 });
            AminCarts.Add(new AminCart { CSubject = "binance", CName = "BTG", CCount = 0.14126893 });
            AminCarts.Add(new AminCart { CSubject = "binance", CName = "CTR", CCount = 0.06000000 });
            AminCarts.Add(new AminCart { CSubject = "binance", CName = "XZC", CCount = 0.04897080 });
            AminCarts.Add(new AminCart { CSubject = "binance", CName = "ARN", CCount = 0.04400000 });
            AminCarts.Add(new AminCart { CSubject = "binance", CName = "POWR", CCount = 0.03000000 });
            AminCarts.Add(new AminCart { CSubject = "binance", CName = "RDN", CCount = 0.02800000 });
            AminCarts.Add(new AminCart { CSubject = "binance", CName = "OMG", CCount = 0.01000000 });
            AminCarts.Add(new AminCart { CSubject = "binance", CName = "ZEC", CCount = 0.00293734 });
            AminCarts.Add(new AminCart { CSubject = "binance", CName = "USDT", CCount = 0.00114172 });
            AminCarts.Add(new AminCart { CSubject = "binance", CName = "BTC", CCount = 0.00007146 });




            ////GIvehChi
            //AminCarts.Add(new AminCart { CSubject = "cryptopia", CName = "BTA", CCount = 1300 });
            //AminCarts.Add(new AminCart { CSubject = "cryptopia", CName = "DGB", CCount = 10000 });
            //AminCarts.Add(new AminCart { CSubject = "cryptopia", CName = "DOGE", CCount = 854015 });
            //AminCarts.Add(new AminCart { CSubject = "cryptopia", CName = "ETN", CCount = 7000 });
            //AminCarts.Add(new AminCart { CSubject = "cryptopia", CName = "ETHD", CCount = 800 });
            //AminCarts.Add(new AminCart { CSubject = "cryptopia", CName = "GNT", CCount = 1100 });
            //AminCarts.Add(new AminCart { CSubject = "cryptopia", CName = "GRS", CCount = 800 });
            //AminCarts.Add(new AminCart { CSubject = "cryptopia", CName = "IFT", CCount = 214 });
            //AminCarts.Add(new AminCart { CSubject = "cryptopia", CName = "KMD", CCount = 149 });
            //AminCarts.Add(new AminCart { CSubject = "cryptopia", CName = "LINDA", CCount = 130000 });
            //AminCarts.Add(new AminCart { CSubject = "cryptopia", CName = "XEM", CCount = 700 });
            //AminCarts.Add(new AminCart { CSubject = "cryptopia", CName = "XPD", CCount = 1218 });
            //AminCarts.Add(new AminCart { CSubject = "cryptopia", CName = "POT", CCount = 3100 });
            //AminCarts.Add(new AminCart { CSubject = "cryptopia", CName = "RDD", CCount = 50000 });
            //AminCarts.Add(new AminCart { CSubject = "cryptopia", CName = "SXC", CCount = 387 });
            //AminCarts.Add(new AminCart { CSubject = "cryptopia", CName = "PAY", CCount = 310 });
            //AminCarts.Add(new AminCart { CSubject = "cryptopia", CName = "WRC", CCount = 341 });

            //AminCarts.Add(new AminCart { CSubject = "binance", CName = "TRX", CCount = 8991 });
            //AminCarts.Add(new AminCart { CSubject = "binance", CName = "XVG", CCount = 6493.5 });
            //AminCarts.Add(new AminCart { CSubject = "binance", CName = "ADA", CCount = 3196.8 });
            //AminCarts.Add(new AminCart { CSubject = "binance", CName = "DNT", CCount = 2997 });
            //AminCarts.Add(new AminCart { CSubject = "binance", CName = "SNT", CCount = 2397.6 });
            //AminCarts.Add(new AminCart { CSubject = "binance", CName = "XLM", CCount = 1998 });
            //AminCarts.Add(new AminCart { CSubject = "binance", CName = "QSP", CCount = 1698.3 });
            //AminCarts.Add(new AminCart { CSubject = "binance", CName = "BTS", CCount = 1598.4 });
            //AminCarts.Add(new AminCart { CSubject = "binance", CName = "XRP", CCount = 2327 });
            //AminCarts.Add(new AminCart { CSubject = "binance", CName = "ZRX", CCount = 499.5 });
            //AminCarts.Add(new AminCart { CSubject = "binance", CName = "CTR", CCount = 299.7 });
            //AminCarts.Add(new AminCart { CSubject = "binance", CName = "MIOTA", CCount = 299.7 });
            //AminCarts.Add(new AminCart { CSubject = "binance", CName = "ARN", CCount = 219.78 });
            //AminCarts.Add(new AminCart { CSubject = "binance", CName = "VEN", CCount = 169.83 });
            //AminCarts.Add(new AminCart { CSubject = "binance", CName = "POWR", CCount = 149.85 });
            //AminCarts.Add(new AminCart { CSubject = "binance", CName = "RDN", CCount = 139.86 });
            //AminCarts.Add(new AminCart { CSubject = "binance", CName = "ETHOS", CCount = 119.88 });
            //AminCarts.Add(new AminCart { CSubject = "binance", CName = "ICX", CCount = 99.9 });
            //AminCarts.Add(new AminCart { CSubject = "binance", CName = "OMG", CCount = 49.95 });
            //AminCarts.Add(new AminCart { CSubject = "binance", CName = "LSK", CCount = 39.96 });
            //AminCarts.Add(new AminCart { CSubject = "binance", CName = "QTUM", CCount = 19.98 });
            //AminCarts.Add(new AminCart { CSubject = "binance", CName = "ETH", CCount = 1 });
            //AminCarts.Add(new AminCart { CSubject = "binance", CName = "BTC", CCount = 0.19849001 });


            List<string> Currencys = new List<string>();
            for (int i = 0; i < AminCarts.Count - 1 ; i++)
            {
                Currencys.Add(AminCarts[i].CName.ToString());
            }
            

            //List<string> Currencys = new List<string>();

            //Currencys.Add("BTC");
            //Currencys.Add("ETH");
            //Currencys.Add("XRP");
            //Currencys.Add("BCH");
            //Currencys.Add("LTC");
            //Currencys.Add("MIOTA");
            //Currencys.Add("DASH");
            //Currencys.Add("ADA");
            //Currencys.Add("XEM");
            //Currencys.Add("BCC");
            //Currencys.Add("XLM");
            //Currencys.Add("ETN");
            //Currencys.Add("DOGE");
            //Currencys.Add("BCN");
            //Currencys.Add("XVG");
            //Currencys.Add("DGB");
            //Currencys.Add("GAS");
            //Currencys.Add("NEO");
            //Currencys.Add("MGO");
            //Currencys.Add("POWR");
            //Currencys.Add("RDD");
            //Currencys.Add("GAY");
            //Currencys.Add("LSK");
            //Currencys.Add("QTUM");
            //Currencys.Add("BCC");
            //Currencys.Add("SXC");

            //CoinsPrice.Clear();

            decimal CoinIRR = Convert.ToDecimal(TXTIRRPrice.Text.ToString());

            Task.Run(() =>
            {
                coinmarketcap allcoinmarketcap = new coinmarketcap();
                List<Allcoinmarketcap> Currencies = allcoinmarketcap.GetCurrency();

                for (int i = 0; i < Currencys.Count; i++)
                {
                    string CoinCName = Currencys[i];

                    ////cryptocompare
                    //Decimal Currency = GetCurrency(CoinCName);

                    //coinmarketcap
                    string strCurrency = Currencies.FirstOrDefault<Allcoinmarketcap>(P => P.symbol.Equals(CoinCName)).price_usd.ToString();
                    Decimal Currency = Convert.ToDecimal(strCurrency);

                    CoinPrice coinPrice = new CoinPrice();
                    coinPrice.CName = "";
                    coinPrice.CBrand = CoinCName;
                    coinPrice.CDPrice = Currency.ToString();

                    coinPrice.CPrice = ChariTableClass.MyUtilities.SepratNumber(
                        Convert.ToInt32(
                            Math.Round(Currency * CoinIRR)));

                    coinPrice.CPicture = "";

                    if (CoinCName != null)
                    {
                        try
                        {
                            CoinsPrice.Add(CoinCName, coinPrice);
                        }
                        catch
                        {
                            CoinsPrice[CoinCName] = coinPrice;
                        }

                        for (int q = 0; q < AminCarts.Count; q++)
                        {
                            string cname = AminCarts[q].CName.ToString();
                            if (cname == CoinCName)
                            {
                                double ccount = AminCarts[q].CCount;
                                string ccoinprice = coinPrice.CDPrice.ToString();
                                AminCarts[q].CUSDPrice = Convert.ToDouble(ccoinprice) * ccount;
                                AminCarts[q].CIIRPrice = Convert.ToDouble(ccoinprice) * ccount *
                                    Convert.ToDouble(CoinIRR);
                            }
                        }
                    }
                }
            });

            AllCoordination();
        }

        public decimal GetIRR()
        {
            string MyUrl = "http://service.arzlive.com/p.js";

            string ResultService = GET(MyUrl);
            if (ResultService != "")
            {
                int startJsone = ResultService.IndexOf('{');
                int finishJsone = ResultService.IndexOf('}', startJsone);

                ResultService = ResultService.Substring(startJsone, (finishJsone - startJsone) + 1);

                bool saveprice = false;
                string IRRPrice = "0";
                JsonTextReader reader = new JsonTextReader(new StringReader(ResultService));

                while (reader.Read()) {
                    if (reader.Value != null) {
                        if (saveprice) {
                            IRRPrice = reader.Value.ToString();

                            return Convert.ToDecimal(IRRPrice);
                        }

                        if (reader.Value.ToString() == "3_40")
                            saveprice = true;
                        }
                    }
                return 0;
            }
            return 0;
        }

        public decimal GetCurrency(string Currency)
        {
            //string MyUrl = "https://min-api.cryptocompare.com/data/price?fsym=BTC&tsyms=USD";
            string MyUrl = "https://min-api.cryptocompare.com/data/price?fsym="+ Currency + "&tsyms=USD";

            string ResultService = GET(MyUrl);
            if (ResultService != "")
                if (!ResultService.Contains("Error"))
                {
                    var ResultServiceXML = JsonConvert.DeserializeXmlNode(ResultService);
                    var ResultMoney = ResultServiceXML.InnerText.ToString();
                    var ResultMoneyD = Convert.ToDecimal(ResultMoney);
                    return ResultMoneyD;
                }
            return 0;
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
                if (errorResponse != null)
                {
                    using (Stream responseStream = errorResponse.GetResponseStream())
                    {
                        StreamReader reader = new StreamReader(responseStream, Encoding.GetEncoding("utf-8"));
                        String errorText = reader.ReadToEnd();
                        // log errorText
                    }
                }
                return "";
                throw;
            }
        }

        private void BTN1_Click(object sender, RoutedEventArgs e)
        {
            FillCurrency();
        }
    }
}
