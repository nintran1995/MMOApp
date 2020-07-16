using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ZChangerMMO.Views.Events;
using CommandModel;

namespace ZChangerMMO.Views.Controls
{
    public partial class AddProxy : UserControl
    {
        [Description("Invoked when user clicks button")]
        public event EventHandler<AddProxy_AddProxyClickEventArgs> AddProxyClick;

        public AddProxy()
        {
            InitializeComponent();
        }

        private void btn_AddProxy_Click(object sender, EventArgs e)
        {
            Proxy newProxy = new Proxy()
            {
                Name = txt_Name.Text,
                Host = txt_Host.Text,
                Port = txt_Port.Text,
                UserName = txt_UserName.Text,
                Password = txt_Password.Text,
                Scheme = txt_Scheme.Text
            };

            AddProxy_AddProxyClickEventArgs eventArgs = new AddProxy_AddProxyClickEventArgs()
            {
                Proxy = newProxy
            };


            if (this.AddProxyClick != null)
                this.AddProxyClick(this, eventArgs);

            ClearForm();
        }

        private void ClearForm()
        {
            txt_Name.Text = String.Empty;
            txt_Host.Text = String.Empty;
            txt_Port.Text = String.Empty;
            txt_UserName.Text = String.Empty;
            txt_Password.Text = String.Empty;
            txt_Scheme.Text = String.Empty;
        }
    }
}
