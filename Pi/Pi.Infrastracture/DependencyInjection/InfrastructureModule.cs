using Autofac;
using Pi.Domain.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Pi.API.Configurations.DependencyInjection
{
    public class InfrastructureModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //var assembly = Assembly.GetExecutingAssembly();

            //builder.RegisterAssemblyTypes(assembly)
            //   .Where(x => x.Namespace != null && x.Namespace.StartsWith("Pi.Infrastracture.Repositories"))
            //   .AsImplementedInterfaces()
            //   .InstancePerLifetimeScope();

            //builder.RegisterAssemblyTypes(assembly)
            //   .Where(x => x.Namespace != null && x.Namespace.StartsWith("Pi.Infrastracture.Services"))
            //   .AsImplementedInterfaces()
            //   .InstancePerLifetimeScope();



            //var assemblies = AppDomain.CurrentDomain.GetAssemblies()
            //.Where(a => a.FullName.StartsWith("Pi.") && !a.IsDynamic)
            //.ToArray();

            //builder.RegisterAssemblyTypes(assemblies)
            //       .Where(t => t.GetInterfaces().Any() && !t.IsAbstract)
            //       .AsImplementedInterfaces()
            //       .InstancePerLifetimeScope();



            var assemblies = new[]
            {
                typeof(DomainModule).Assembly,        // assembly hiện tại
                Assembly.Load("Pi.Domain"),   // assembly chứa implement
                Assembly.Load("Pi.Application")          // nếu cần
            };

            builder.RegisterAssemblyTypes(assemblies)
                .Where(t => t.GetInterfaces().Any() && !t.IsAbstract)
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            base.Load(builder);
        }
    }
}
