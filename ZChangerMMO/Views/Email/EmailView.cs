using DevExpress.Utils.MVVM.Services;
using DevExpress.XtraEditors;
using ZChangerMMO.ViewModels;

namespace ZChangerMMO.Views.Email
{
    public partial class EmailView : XtraUserControl
    {
        public EmailView()
        {
            InitializeComponent();
            if (!DesignMode) InitBindings();
        }

        private void InitBindings()
        {
            var fluent = mvvmContext1.OfType<EmailViewModel>();
            fluent.SetObjectDataSourceBinding(
                emailBindingSource, x => x.Item, x => x.UpdateCommands());
            mvvmContext1.RegisterService(NotificationService.Create(toastNotificationsManager1));
        }
    }
}
