using System;
using PageMicroservice.Data.Contexts;

namespace PageMicroservice.Data.Infrastructure
{
    public interface IContextFactory: IDisposable
    {
        PageContext Get();
    }

    public class ContextFactory: IContextFactory
    {
        private PageContext context;

        public PageContext Get()
        {
            return context ?? (context = new PageContext());
        }

        public void Dispose()
        {
            context?.Dispose();
        }
    }
}