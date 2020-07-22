using System.Windows.Forms;
using ZChangerMMO.UI;
using ZChangerMMO.Views.Email;

namespace ZChangerMMO.Views
{
    public partial class frmMainView : DevExpress.XtraEditors.XtraForm
    {
        public frmMainView()
        {
            Bootstrapper.BuildContainer();
            InitializeComponent();
            if (!DesignMode)
                new EmailListView { Dock = DockStyle.Fill, Parent = this };
        }
    }
}