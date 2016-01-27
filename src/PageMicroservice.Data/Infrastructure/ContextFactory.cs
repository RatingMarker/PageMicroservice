using System;
using PageMicroservice.Data.Contexts;

namespace PageMicroservice.Data.Infrastructure
{
    public interface IContextFactory
    {
        PageContext Get();
    }

    public class ContextFactory: IContextFactory
    {
        public PageContext Get()
        {
            return new PageContext();
        }
    }
}