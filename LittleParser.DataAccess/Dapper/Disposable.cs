using System;

namespace LittleParser.DataAccess.Dapper
{
    public class Disposable : IDisposable
    {
        private bool _disposed;
        private readonly IDisposable[] _disposables;

        public Disposable(params IDisposable[] disposables)
        {
            _disposables = disposables;
            _disposed = false;
        }
        
        public void Dispose()
        {
            Disposing(true);
            GC.SuppressFinalize(this);
        }

        private void Disposing(bool disposing)
        {
            if (!_disposed)
            {
                foreach (var item in _disposables)
                {
                    item?.Dispose();
                }
                
                _disposed = true;
            }
        }

        ~Disposable()
        {
            Disposing(false);
        }
    }
}