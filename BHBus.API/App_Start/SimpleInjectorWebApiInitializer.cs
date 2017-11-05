[assembly: WebActivator.PostApplicationStartMethod(typeof(BHBus.API.App_Start.SimpleInjectorWebApiInitializer), "Initialize")]

namespace BHBus.API.App_Start
{
    using BHBus.Application;
    using BHBus.Application.Interfaces;
    using BHBus.Domain.Interfaces;
    using BHBus.Infra.Repositories;
    using SimpleInjector;
    using SimpleInjector.Integration.WebApi;
    using SimpleInjector.Lifestyles;
    using System.Web.Http;

    public static class SimpleInjectorWebApiInitializer
    {
        /// <summary>Initialize the container and register it as Web API Dependency Resolver.</summary>
        public static void Initialize()
        {
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();//new WebApiRequestLifestyle();

            InitializeContainer(container);

            container.RegisterWebApiControllers(GlobalConfiguration.Configuration);

            container.Verify();

            GlobalConfiguration.Configuration.DependencyResolver =
                new SimpleInjectorWebApiDependencyResolver(container);
        }

        private static void InitializeContainer(Container container)
        {            
            container.Register<IPassengerAppService, AppServiceBase>(Lifestyle.Scoped);
            container.Register<IBusLineAppService, BusLineAppService>(Lifestyle.Scoped);
            container.Register<IBalanceAppService, BalanceAppService>(Lifestyle.Scoped);

            container.Register<IPassengerRepository, PassengerRepository>(Lifestyle.Scoped);
            container.Register<IBusLineRepository, BusLineRepository>(Lifestyle.Scoped);
            container.Register<IBalanceRepository, BalanceRepository>(Lifestyle.Scoped);
        }
    }
}