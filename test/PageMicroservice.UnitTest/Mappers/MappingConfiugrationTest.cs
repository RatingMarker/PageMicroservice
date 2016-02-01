using AutoMapper;
using PageMicroservice.Api.Models;
using PageMicroservice.Models;
using Xunit;

namespace PageMicroservice.UnitTest.Mappers
{
    public class MappingConfiugrationTest
    {
        [Fact]
        public void FactMethodName()
        {
            //arrange
            MapperConfiguration config = new MapperConfiguration(
                cfg =>
                {
                    cfg.CreateMap<Site, SiteViewModel>();
                });

            //act


            //assert
            config.AssertConfigurationIsValid();
        }
    }
}