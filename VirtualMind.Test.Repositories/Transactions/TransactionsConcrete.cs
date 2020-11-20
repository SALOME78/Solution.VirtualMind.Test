using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualMind.Test.Data;
using VirtualMind.Test.Interface.Transactions;
using VirtualMind.Test.Model.ViewModels;

namespace VirtualMind.Test.Repositories
{
    public class TransactionsConcrete: ITransactions
    {
        private readonly ApplicationDbContext _context;
        public TransactionsConcrete(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IQueryable<CurrencyPurchaseRegistration>> GetAllAsync()
        {
            IQueryable<CurrencyPurchaseRegistration> allItems = (from cp in _context.Transactions
                                join doc in _context.Documents on cp.IDDocument equals doc.ID
                                join ec in _context.ExchangeCurrency on cp.IDExchangeCurrency equals ec.ID
                                select new CurrencyPurchaseRegistration()
                                {
                                    Id = cp.ID,
                                    UserId = cp.IDUser,
                                    DocumentId = cp.IDDocument,
                                    Document = doc.Description,
                                    ExchangeCurrencyId = cp.IDExchangeCurrency,
                                    ExchangeCurrency = ec.Description,
                                    Description = cp.Description,
                                    Amount = cp.Amount,
                                    ExchangeRate = cp.ExchangeRate,
                                    SubTotal = cp.SubTotal,
                                    CreateDate = cp.CreateDate,
                                    ModifyDate = cp.ModifyDate,
                                    IsEnabled = cp.IsEnabled
                                }).AsQueryable();
            return allItems;
        }

    }
}
