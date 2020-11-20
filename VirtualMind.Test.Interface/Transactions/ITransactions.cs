using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualMind.Test.Model.ViewModels;

namespace VirtualMind.Test.Interface.Transactions
{
    public interface ITransactions
    {
        Task<IQueryable<CurrencyPurchaseRegistration>> GetAllAsync();
    }
}
