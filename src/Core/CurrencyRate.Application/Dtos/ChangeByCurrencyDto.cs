using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyRate.Application.Dtos
{
    public class ChangeByCurrencyDto : CurrencyDto
    {

        private decimal _change;

        public decimal Change
        {
            get { return decimal.Round(_change, 2, MidpointRounding. AwayFromZero); }
            set { _change = value; }
        }
    }
}
