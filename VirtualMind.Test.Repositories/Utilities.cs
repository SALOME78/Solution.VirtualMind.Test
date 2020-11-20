using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using VirtualMind.Test.Model;

namespace VirtualMind.Test.Repositories
{
    public class Utilities
    {
        public bool ValidatedLimit(CurrencyPurchase entity)
        {
            if (entity.IDExchangeCurrency == 1)
            {
                if (entity.Amount > 200)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            if (entity.IDExchangeCurrency == 2)
            {
                if (entity.Amount > 300)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            return false;
        }
        public async Task<ExchangeRate> GetExchangeRateAsync(int ExchangeRateID)
        {
            ExchangeRate exchange_Rate = new ExchangeRate();

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://www.bancoprovincia.com.ar/Principal/Dolar"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    //var exchange_Rate = JsonConvert.DeserializeObject<dynamic>(apiResponse);

                    var result = apiResponse.Trim('[', ']')
                                  .Split(",")
                                  .Select(x => x.Trim('"'))
                                  .ToArray();

                    exchange_Rate.PurchasePrice = Convert.ToDecimal(result[0]);
                    exchange_Rate.SalePrice = Convert.ToDecimal(result[1]);
                    exchange_Rate.Description = result[2];
                }
            }

            return exchange_Rate;
        }
    }
}
