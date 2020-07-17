using DevExpress.Utils.MVVM.UI;
using DevExpress.XtraEditors;
using ZChangerMMO.ViewModels;

namespace ZChangerMMO.Views.Email
{
    [ViewType("EmailView")]
    public partial class EmailsEditFormView : XtraUserControl
    {
        public EmailsEditFormView()
        {
            InitializeComponent();
            if (!DesignMode) InitBindings();
        }

        void InitBindings()
        {
            //Email Edit Form View
            var fluent = mvvmContext1.OfType<EmailViewModel>();
            fluent.SetObjectDataSourceBinding(
                emailBindingSource, x => x.Entity, x => x.Update());
        }
    }
}
