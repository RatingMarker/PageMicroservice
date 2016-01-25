using System;
using PageMicroservice.Data.Contexts;

namespace PageMicroservice.Data.Infrastructure
{
    public interface IContextFactory: IDisposable
    {
        PageContext Get();
    }

    public class ContextFactory: Disposable, IContextFactory
    {
        private PageContext context;

        public PageContext Get()
        {
            if (context == null)
            {
                context = new PageContext();
            }
            return context;
        }

        protected override void DisposeCore()
        {
            context?.Dispose();
        }
    }
}