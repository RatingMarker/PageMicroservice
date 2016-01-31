using Autofac;
using PageMicroservice.Data;
using PageMicroservice.Services;

namespace PageMicroservice.Api.Configurations
{
    public class AutofacModule: Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterModule<AutoMapperModule>();
            builder.RegisterModule<DataModule>();
            builder.RegisterModule<ServiceModule>();
        }
    }
}