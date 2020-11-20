using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VirtualMind.Test.Data;
using VirtualMind.Test.Interface;
using VirtualMind.Test.Interface.Transactions;
using VirtualMind.Test.Model;
using VirtualMind.Test.Repositories.Common;

namespace Virtualmind.Test.APIServices.Controllers
{
    [Authorize]
    //[EnableCors("CorsPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class CurrencyPurchaseController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ITransactions _transactions;

        public CurrencyPurchaseController(IUnitOfWork unitOfWork, ITransactions transactions)
        {
            this.unitOfWork = unitOfWork;
            this._transactions = transactions;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Get()
        {
            //var data = await unitOfWork.Transaction.GetAllAsync();
            var data = this._transactions.GetAllAsync();
            return Ok(data.Result);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Add([FromBody] CurrencyPurchaseViewModel transactionViewModel)
        {
            CurrencyPurchase model = new CurrencyPurchase();

            try
            {
                model.IDUser = transactionViewModel.IdUser;
                model.IDExchangeCurrency = transactionViewModel.IdExchangeCurrency;
                model.Amount = transactionViewModel.Amount;

                var data = await unitOfWork.Transaction.AddAsync(model);
                return Ok(data);
            }
            catch (WebException e)
            {
                throw e;
            }
            catch (CustomException e)
            {
                return NotFound(e.Message);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
