using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ZChangerMMO.Views.Controls
{
    public enum TestProxyStatus
    {
        Testing,
        Normal
    }

    public partial class TestProxies : UserControl
    {
        public TestProxyStatus Status { get; set; }

        [Description("Invoked when user clicks button")]
        public event EventHandler TestProxyClick;
        public TestProxies()
        {
            InitializeComponent();
            UpdateUI(TestProxyStatus.Normal);
        }

        private void btn_TestProxy_Click(object sender, EventArgs e)
        {
            if (this.TestProxyClick != null)
                this.TestProxyClick(this, e);
        }

        public void UpdateUI(TestProxyStatus status) {
            Status = status;
            switch (status)
            {
                case TestProxyStatus.Normal:
                    {
                        this.progressPanel_Testing.Visible = false;
                        this.groupControlBulkTestProxies.Visible = true;
                        this.btn_TestProxy.Enabled = true;
                        break;
                    }
                case TestProxyStatus.Testing:
                    {
                        this.progressPanel_Testing.Visible = true;
                        this.groupControlBulkTestProxies.Visible = false;
                        this.btn_TestProxy.Enabled = false;
                        break;
                    }
            }
        }
    }
}
