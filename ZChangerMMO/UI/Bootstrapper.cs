using Autofac;
using ZChangerMMO.Domain;
using ZChangerMMO.Infrastructure;
using ZChangerMMO.Infrastructure.Repositories;
using ZChangerMMO.ViewModels;

namespace ZChangerMMO.UI
{
    public class Bootstrapper
    {
        public static Autofac.IContainer Container { get; set; }

        public static void BuildContainer()
        {
            var builder = new Autofac.ContainerBuilder();

            builder.RegisterType<UoW>().As<IUoW>();
            builder.RegisterType<ZChangerContext>();

            builder.RegisterType<EmailRepository>().As<IEmailRepository>();
            builder.RegisterType<DeviceRepository>().As<IDeviceRepository>();

            builder.RegisterType<EmailListViewModel>();
            builder.RegisterType<DeviceViewModel>();
            //builder.RegisterType<VisitViewModel>().As<IVisitViewModel>();
            //builder.RegisterType<CustomerSelectViewModel>();
            //builder.RegisterType<PhoneViewModel>();

            Container = builder.Build();
        }
    }
}
