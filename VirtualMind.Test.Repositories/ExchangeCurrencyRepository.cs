using Dapper;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using VirtualMind.Test.Interface;
using VirtualMind.Test.Model;
using VirtualMind.Test.Repositories.Common;

namespace VirtualMind.Test.Repositories
{
    public class ExchangeCurrencyRepository : IExchangeCurrencyRepository
    {
        private readonly IConfiguration configuration;
        public ExchangeCurrencyRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task<IReadOnlyList<ExchangeCurrency>> GetAllAsync()
        {
            var sql = "SELECT * FROM ExchangeCurrency";
            using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.QueryAsync<ExchangeCurrency>(sql);
                return result.ToList();
            }
        }
        public async Task<ExchangeCurrency> GetByIdAsync(int id)
        {
            string err, Resultjson = string.Empty;
            ResultJson re = new ResultJson();

            ExchangeRate exchange_Rate = new ExchangeRate();
            var ServicesExtern = new Utilities();

            try
            {
                switch (id)
                {
                    case 1: // AMARICAN DOLLAR (USD)
                        exchange_Rate = await ServicesExtern.GetExchangeRateAsync(id);

                        break;
                    case 2: // BRAZILIAN REAL (BRL)
                        exchange_Rate = await ServicesExtern.GetExchangeRateAsync(id);
                        var _4th = exchange_Rate.PurchasePrice / 4;
                        exchange_Rate.PurchasePrice = _4th;
                        exchange_Rate.SalePrice = 0;

                        break;
                    case 3: // CANADIAN DOLLAR (USD)
                        err = "We dont have this kind of currency, we will have Canadian dollar in the future!";

                        re.Code = "404";
                        re.Message = err;
                        Resultjson = JsonConvert.SerializeObject(re);

                        throw new CustomException(Resultjson);

                    default: // DOES NOT EXIST
                        err = "This kind of currency does not exist in our database!";

                        re.Code = "404";
                        re.Message = err;
                        Resultjson = JsonConvert.SerializeObject(re);

                        throw new CustomException(Resultjson);
                }

                var sql = "SELECT * FROM ExchangeCurrency WHERE ID = @Id";
                using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
                {
                    connection.Open();
                    var result = await connection.QuerySingleOrDefaultAsync<ExchangeCurrency>(sql, new { Id = id });

                    result.Value = exchange_Rate.PurchasePrice;

                    return result;
                }
            }
            catch (WebException e)
            {
                throw e;
            }
            catch (CustomException e)
            {
                throw e;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
