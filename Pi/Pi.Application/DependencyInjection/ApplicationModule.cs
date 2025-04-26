using Autofac;
using Pi.Domain.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Pi.Application.DependencyInjection
{
    public class ApplicationModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //var assembly= Assembly.GetExecutingAssembly();

            //builder.RegisterAssemblyTypes(assembly)
            //    .Where(x=>x.Namespace != null && x.Namespace.StartsWith("Pi.Application.Interfaces"))
            //    .AsImplementedInterfaces()
            //    .InstancePerLifetimeScope();



            var assemblies = new[]
            {
                typeof(DomainModule).Assembly,        // assembly hiện tại
                Assembly.Load("Pi.Domain"),           // assembly chứa implement
                Assembly.Load("Pi.Infrastracture")    // nếu cần
            };

            builder.RegisterAssemblyTypes(assemblies)
                .Where(t => t.GetInterfaces().Any() && !t.IsAbstract)
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            base.Load(builder);

            base.Load(builder);
        }
    }
}
