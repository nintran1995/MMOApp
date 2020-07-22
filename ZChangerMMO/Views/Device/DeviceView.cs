using DevExpress.Utils.MVVM.Services;
using DevExpress.XtraEditors;
using ZChangerMMO.ViewModels;

namespace ZChangerMMO.Views.Device
{
    public partial class DeviceView : XtraUserControl
    {
        public DeviceView()
        {
            InitializeComponent();
            if (!DesignMode) InitBindings();
        }

        private void InitBindings()
        {
            var fluent = mvvmContext1.OfType<DeviceViewModel>();
            fluent.SetObjectDataSourceBinding(
                deviceBindingSource, x => x.Item, x => x.UpdateCommands());
            mvvmContext1.RegisterService(NotificationService.Create(toastNotificationsManager1));
        }
    }
}
