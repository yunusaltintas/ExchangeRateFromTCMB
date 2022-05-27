using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CurrencyRate.Application.Dtos.BaseResponse
{
    public class CustomResponseDto
    {
        [JsonIgnore] public int StatusCode { get; set; }
        public bool IsSuccess { get; set; }
        public ErrorMessage ErrorMessage { get; set; }

        public CustomResponseDto Success(int statusCode) =>
            new() { StatusCode = statusCode, IsSuccess = true };

        public CustomResponseDto Error(int statusCode, ErrorMessage errorMessage) => new()
        { StatusCode = statusCode, ErrorMessage = errorMessage, IsSuccess = false };
    }

    public class CustomResponseDto<T> : CustomResponseDto where T : class, new()
    {
        public T Data { get; set; }

        public CustomResponseDto<T> Success(int statusCode, T data) =>
            new() { Data = data, StatusCode = statusCode, IsSuccess = true };

        public CustomResponseDto<T> Success(int statusCode) =>
            new() { StatusCode = statusCode, IsSuccess = true };

        public CustomResponseDto<T> Error(int statusCode, ErrorMessage errorMessage) => new()
        { StatusCode = statusCode, ErrorMessage = errorMessage, IsSuccess = false };

        public CustomResponseDto<T> Error(int statusCode) => new()
        { StatusCode = statusCode, IsSuccess = false };
    }
}
