using AutoMapper;
using CurrencyRate.Application.Dtos;
using CurrencyRate.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyRate.Application.Mapping
{
    public class GeneralMapping : Profile
    {

        public GeneralMapping()
        {
            CreateMap<ExchangeRate, CurrencyDto>()
                .ReverseMap();

        }
    }
}
