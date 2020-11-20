using System;
using System.Collections.Generic;
using System.Text;

namespace VirtualMind.Test.Model
{
    public class Transactions
    {
        public int ID { get; set; }
        public int IDUser { get; set; }
        public int IDDocument { get; set; }
        public int IDExchangeCurrency { get; set; }
        public string Description { get; set; }
        public int Amount { get; set; }
        public decimal? ExchangeRate { get; set; }
        public decimal? SubTotal { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? ModifyDate { get; set; }
        public bool IsEnabled { get; set; }
    }
}
