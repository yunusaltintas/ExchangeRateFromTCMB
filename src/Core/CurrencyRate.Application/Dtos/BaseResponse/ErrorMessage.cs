using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyRate.Application.Dtos.BaseResponse
{
    public class ErrorMessage
    {
        public string ErrorDescription { get; set; }
        public string ErrorCode { get; set; }
    }
}
