using CurrencyRate.Application.Dtos.BaseResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyRate.Application.Dtos.Exceptions
{
    public class CustomException : Exception
    {
        public ErrorMessage ErrorMessage = new();

        public CustomException(string errorDescription, string errorCode) : base(errorDescription)
        {
            ErrorMessage.ErrorDescription = errorDescription;
            ErrorMessage.ErrorCode = errorCode;
        }
    }
}
