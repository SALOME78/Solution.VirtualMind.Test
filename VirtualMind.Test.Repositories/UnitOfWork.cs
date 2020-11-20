using System;
using System.Collections.Generic;
using System.Text;
using VirtualMind.Test.Interface;

namespace VirtualMind.Test.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(IExchangeCurrencyRepository ExchangeRateRepository,
                          ICurrencyPurchaseRepository currencyPurchaseRepository)
        {
            ExchangeRate = ExchangeRateRepository;
            Transaction = currencyPurchaseRepository;
        }
        public IExchangeCurrencyRepository ExchangeRate { get; }
        public ICurrencyPurchaseRepository Transaction { get; }
    }
}
