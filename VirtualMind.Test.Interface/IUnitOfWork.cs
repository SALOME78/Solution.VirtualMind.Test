using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualMind.Test.Interface
{
    public interface IUnitOfWork
    {
        IExchangeCurrencyRepository ExchangeRate { get; }
        ICurrencyPurchaseRepository Transaction { get; }
    }
}
