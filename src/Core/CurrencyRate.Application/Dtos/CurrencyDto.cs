using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyRate.Application.Dtos
{
    public class CurrencyDto
    {
        private string _rate;

        public DateTime LastUpdated { get; set; }

        public decimal Rate { get; set; }

        public string CurrencyCode
        {

            get { return _rate + "-TRY"; }
            set { _rate = value; }
        }
    }
}
