using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VirtualMind.Test.Interface;
using VirtualMind.Test.Interface.Transactions;
using VirtualMind.Test.Repositories;

namespace Virtualmind.Test.APIServices.Infrastructure
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructure(this IServiceCollection services)
        {
            services.AddTransient<IExchangeCurrencyRepository, ExchangeCurrencyRepository>();
            services.AddTransient<ICurrencyPurchaseRepository, CurrencyPurchaseRepository>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IUsers, UsersConcrete>();
            services.AddTransient<ITransactions, TransactionsConcrete>();
        }
    }
}
