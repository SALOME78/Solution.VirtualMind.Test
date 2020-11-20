using System;
using System.Collections.Generic;
using System.Text;

namespace VirtualMind.Test.Model.ViewModels
{
    public class CurrencyPurchaseRegistration
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int DocumentId { get; set; }
        public string Document { get; set; }
        public int ExchangeCurrencyId { get; set; }
        public string ExchangeCurrency { get; set; }
        public string Description { get; set; }
        public Decimal? Amount { get; set; }
        public Decimal? ExchangeRate { get; set; }
        public Decimal? SubTotal { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? ModifyDate { get; set; }
        public bool IsEnabled { get; set; }        
    }
}
