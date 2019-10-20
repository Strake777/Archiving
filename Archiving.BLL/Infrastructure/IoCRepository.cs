using Autofac;
using Archiving.DAL.Interfaces;
using Archiving.DAL.Repositories;

namespace Archiving.BLL.Infrastructure
{
    public class IoCRepository
    {
        private static IContainer _container;

        public static void Initialize(string connectionString)
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<AccountRepository>().As<IAccountRepository>().WithParameter("connectionString", connectionString);
            _container = builder.Build();
        }

        public static IAccountRepository AccountRepository
        {
            get { return _container.Resolve<IAccountRepository>(); }
        }
    }
}
