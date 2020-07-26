using Autofac;
using ZChangerMMO.ViewModels;

namespace ZChangerMMO.Infrastructure
{
    public class Bootstrapper
    {
        public static IContainer Container { get; set; }

        public static void BuildContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<UoW>().As<IUoW>();
            builder.RegisterType<ZChangerContext>();

            builder.RegisterType<EmailRepository>().As<IEmailRepository>();
            builder.RegisterType<DeviceRepository>().As<IDeviceRepository>();

            builder.RegisterType<EmailListViewModel>();
            builder.RegisterType<EmailViewModel>();
            builder.RegisterType<DeviceListViewModel>();
            builder.RegisterType<DeviceViewModel>();

            Container = builder.Build();
        }
    }
}
