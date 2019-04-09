using AutoMapper;
using BillsMonster.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BillsMonster.Application.Infrastructure.AutoMapper
{
    public class AutoMapperConfig : IAutoMapperConfig
    {
        public void Setup()
        {
            //Mapper.Initialize(config =>
            //{
            //    config.CreateMap()
            //})
        }
    }
}
