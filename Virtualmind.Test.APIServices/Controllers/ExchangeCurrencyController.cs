using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VirtualMind.Test.Data;
using VirtualMind.Test.Interface;
using VirtualMind.Test.Repositories.Common;

namespace Virtualmind.Test.APIServices.Controllers
{
    [Authorize]
    //[EnableCors("CorsPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class ExchangeCurrencyController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IUnitOfWork unitOfWork;

        public ExchangeCurrencyController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Get()
        {
            var data = await unitOfWork.ExchangeRate.GetAllAsync();
            return Ok(data);
        }

        // GET: api/ExchangeCurrency/5
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var data = await unitOfWork.ExchangeRate.GetByIdAsync(id);
                if (data == null) return Ok();
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
