using Autofac;
using Autofac.Integration.Mvc;
using AxadoTest.Data.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace AxadoTest.Web
{
    public static class AutofacConfig
    {
        public static void Register()
        {
            var builder = new ContainerBuilder();

            builder.RegisterControllers(typeof(MvcApplication).Assembly)
               .InstancePerRequest();

            var data = Assembly.Load("AxadoTest.Data");
            builder.RegisterAssemblyTypes(data).Where(t => t.Name.EndsWith("Repository")).AsImplementedInterfaces().InstancePerRequest();
            builder.RegisterAssemblyTypes(data).Where(t => t.Name.EndsWith("Validator")).AsImplementedInterfaces().InstancePerRequest();
            builder.RegisterAssemblyTypes(data).Where(t => t.Name.EndsWith("Service")).AsImplementedInterfaces().InstancePerRequest();

            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerRequest();
            builder.RegisterType<DatabaseFactory>().As<IDatabaseFactory>().InstancePerRequest();

            builder.RegisterModelBinders(Assembly.GetExecutingAssembly());
            builder.RegisterModelBinderProvider();

            builder.RegisterModule<AutofacWebTypesModule>();
            builder.RegisterSource(new ViewRegistrationSource());

            builder.RegisterFilterProvider();

            var container = builder.Build();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}
