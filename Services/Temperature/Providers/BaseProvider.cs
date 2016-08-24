using System;
using Data.Store;

namespace Services.Temperature.Providers
{
    public class BaseProvider : IDisposable
    {
        private readonly StoreContext _context;

        protected StoreContext Context
        {
            get { return _context; }
        }

        public BaseProvider()
        {
            _context = new StoreContext();
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}