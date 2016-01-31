using System.Collections.Generic;
using Autofac;
using AutoMapper;
using PageMicroservice.Api.Mappers;

namespace PageMicroservice.Api.Configurations
{
    public class AutoMapperModule: Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(
                context =>
                {
                    //var profiles = context.Resolve<IEnumerable<Profile>>();

                    var config = new MapperConfiguration(
                        cfg =>
                        {
                            cfg.AddProfile<DomainToViewModelMappingProfile>();
                            cfg.AddProfile<ViewModelToDomainMappingProfile>();
                        });

                    return config.CreateMapper();
                }).SingleInstance()
                   .As<IMapper>();

            base.Load(builder);
        }
    }
}