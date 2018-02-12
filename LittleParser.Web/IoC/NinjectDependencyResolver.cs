using System;
using System.Web.Http.Dependencies;
using LittleParser.DataAccess.Dapper;
using LittleParser.DataAccess.Dapper.Factories;
using LittleParser.DataAccess.Persistance;
using LittleParser.Services.Services;
using LittleParser.Services.Services.Impl;
using Ninject;
using Ninject.Syntax;

namespace LittleParser.Web.IoC
{
    public class NinjectDependencyScope : IDependencyScope
    {
        IResolutionRoot _resolver;

        public NinjectDependencyScope(IResolutionRoot resolver)
        {
            this._resolver = resolver;
        }

        public object GetService(Type serviceType)
        {
            if (_resolver == null)
                throw new ObjectDisposedException("this", "This scope has been disposed");

            return _resolver.TryGet(serviceType);
        }

        public System.Collections.Generic.IEnumerable<object> GetServices(Type serviceType)
        {
            if (_resolver == null)
                throw new ObjectDisposedException("this", "This scope has been disposed");

            return _resolver.GetAll(serviceType);
        }

        public void Dispose()
        {
            IDisposable disposable = _resolver as IDisposable;
            if (disposable != null)
                disposable.Dispose();

            _resolver = null;
        }
    }

    // This class is the resolver, but it is also the global scope
    // so we derive from NinjectScope.
    public class NinjectDependencyResolver : NinjectDependencyScope, IDependencyResolver
    {
        readonly IKernel _kernel;

        public NinjectDependencyResolver(IKernel kernel)
            : base(kernel)
        {
            this._kernel = kernel;

            _kernel.Bind<IDbConnectionFactory>().To<DbSqlConnectionFactory>();
            _kernel.Bind<IUnitOfWork>().To<UnitOfWork>();
            _kernel.Bind<IApacheLogService>().To<ApacheLogService>();
        }

        public IDependencyScope BeginScope()
        {
            return new NinjectDependencyScope(_kernel.BeginBlock());
        }
    }


    //public class NinjectDependencyResolver : NinjectScope, IDependencyResolver
    //{
    //    private readonly IKernel _kernel;

    //    public NinjectDependencyResolver(IKernel kernelParam)
    //    {
    //        _kernel = kernelParam;
    //        Load();
    //    }

    //    public object GetService(Type serviceType)
    //    {
    //        return _kernel.TryGet(serviceType);
    //    }

    //    public IEnumerable<object> GetServices(Type serviceType)
    //    {
    //        return _kernel.GetAll(serviceType);
    //    }

    //    public IDependencyScope BeginScope()
    //    {
    //        return new NinjectScope(_kernel.BeginBlock());
    //    }

    //    public void Load()
    //    {
    //        _kernel.Bind<IDbConnectionFactory>().To<DbSqlConnectionFactory>();
    //        _kernel.Bind<IUnitOfWork>().To<UnitOfWork>();
    //        _kernel.Bind<IApacheLogService>().To<ApacheLogService>();
    //    }

    //    public void Dispose()
    //    {
    //        _kernel.Dispose();
    //    }
    //}
}