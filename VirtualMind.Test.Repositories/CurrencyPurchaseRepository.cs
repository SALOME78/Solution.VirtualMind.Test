using Dapper;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using VirtualMind.Test.Interface;
using VirtualMind.Test.Model;
using VirtualMind.Test.Repositories.Common;

namespace VirtualMind.Test.Repositories
{
    public class CurrencyPurchaseRepository: ICurrencyPurchaseRepository
    {
        private readonly IConfiguration configuration;
        public CurrencyPurchaseRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task<IReadOnlyList<CurrencyPurchase>> GetAllAsync()
        {
            var sql = "SELECT * FROM Transactions";
            using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.QueryAsync<CurrencyPurchase>(sql);
                return result.ToList();
            }
        }
        public async Task<int> AddAsync(CurrencyPurchase entity)
        {
            var ServicesExtern = new Utilities();
            ExchangeRate exchange_Rate = new ExchangeRate();

            string err, Resultjson = string.Empty;
            ResultJson re = new ResultJson();

            try
            {
                switch (entity.IDExchangeCurrency)
                {
                    case 1: // AMERICAN DOLLAR (USD)

                        var validated_Amount = ServicesExtern.ValidatedLimit(entity);

                        if (validated_Amount)
                        {
                            err = "The amount entered should be minor or equal to $200 for American Dollar!";

                            re.Code = "404";
                            re.Message = err;
                            Resultjson = JsonConvert.SerializeObject(re);

                            throw new CustomException(Resultjson);
                        }

                        exchange_Rate = await ServicesExtern.GetExchangeRateAsync(entity.IDExchangeCurrency);

                        break;
                    case 2: // BRAZILIAN REAL (BRL)
                        var validated_Amount2 = ServicesExtern.ValidatedLimit(entity);

                        if (validated_Amount2)
                        {
                            err = "The amount entered should be minor or equal to $300 for Brazilian Real!";

                            re.Code = "404";
                            re.Message = err;
                            Resultjson = JsonConvert.SerializeObject(re);

                            throw new CustomException(Resultjson);
                        }

                        exchange_Rate = await ServicesExtern.GetExchangeRateAsync(entity.IDExchangeCurrency);
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

                var subTotal = entity.Amount / exchange_Rate.PurchasePrice;

                using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
                {
                    connection.Open();

                    var sqlTransaction = connection.BeginTransaction();
                    var para = new DynamicParameters();

                    para.Add("@UserID", entity.IDUser);
                    para.Add("@DocumentID", 1);
                    para.Add("@ExchangeCurrencyID", entity.IDExchangeCurrency);
                    para.Add("@Description", "TRANSACTION OF CURRENCY PURCHASE");
                    para.Add("@Amount", entity.Amount);
                    para.Add("@ExchangeRate", exchange_Rate.PurchasePrice);
                    para.Add("@SubTotal", subTotal);

                    int result = connection.Execute("sp_Insert_CurrencyPurchase", para, sqlTransaction, 0, System.Data.CommandType.StoredProcedure);
                    if (result > 0)
                    {
                        sqlTransaction.Commit();
                        return result;
                    }
                    else
                    {
                        sqlTransaction.Rollback();
                        return 0;
                    }
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
