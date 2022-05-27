using CurrencyRate.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyRate.Domain.Entities
{
    public class ExchangeRate :CommonEntity
    {
        public DateTime LastUpdated { get; set; }

        public decimal Rate { get; set; }

        public string CurrencyCode { get; set; }
    }
}
