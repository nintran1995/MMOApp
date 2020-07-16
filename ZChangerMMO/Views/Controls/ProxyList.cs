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
using CommandModel;
using ZChangerMMO.Views.Events;
using System.Collections;

namespace ZChangerMMO.Views.Controls
{
    public partial class ProxyList : DevExpress.XtraEditors.XtraUserControl
    {
        [Description("Invoked when user clicks button")]
        public event EventHandler<UpdateProxyListEventArgs> UpdateProxyListEvent;
        public ProxyList()
        {
            InitializeComponent();
        }

        private void gridView_Proxy_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            var proxies = this.grid_Proxy.DataSource as List<Proxy>;
            var eventArgs = new UpdateProxyListEventArgs()
            {
                ProxyList = proxies
            };

            if (this.UpdateProxyListEvent != null)
                this.UpdateProxyListEvent(this, eventArgs);
        }

        private void btn_SelectAll_Click(object sender, EventArgs e)
        {
            gridView_Proxy.SelectAll();
        }

        private void btn_ClearSelection_Click(object sender, EventArgs e)
        {
            gridView_Proxy.ClearSelection();
        }

        private void btn_Enable_Click(object sender, EventArgs e)
        {
            ArrayList rows = new ArrayList();

            // Add the selected rows to the list. 
            Int32[] selectedRowHandles = gridView_Proxy.GetSelectedRows();
            for (int i = 0; i < selectedRowHandles.Length; i++)
            {
                int selectedRowHandle = selectedRowHandles[i];
                if (selectedRowHandle >= 0)
                    rows.Add(gridView_Proxy.GetRow(selectedRowHandle));
            }
            try
            {
                gridView_Proxy.BeginUpdate();
                for (int i = 0; i < rows.Count; i++)
                {
                    ProxyUserControl row = rows[i] as ProxyUserControl;
                    // Change the field value. 
                    row.Enabled = true;
                }
            }
            finally
            {
                gridView_Proxy.EndUpdate();
            }

            var proxies = this.grid_Proxy.DataSource as List<Proxy>;
            var eventArgs = new UpdateProxyListEventArgs()
            {
                ProxyList = proxies
            };
            if (this.UpdateProxyListEvent != null)
                this.UpdateProxyListEvent(this, eventArgs);
        }

        private void btn_Disable_Click(object sender, EventArgs e)
        {
            ArrayList rows = new ArrayList();

            // Add the selected rows to the list. 
            Int32[] selectedRowHandles = gridView_Proxy.GetSelectedRows();
            for (int i = 0; i < selectedRowHandles.Length; i++)
            {
                int selectedRowHandle = selectedRowHandles[i];
                if (selectedRowHandle >= 0)
                    rows.Add(gridView_Proxy.GetRow(selectedRowHandle));
            }
            try
            {
                gridView_Proxy.BeginUpdate();
                for (int i = 0; i < rows.Count; i++)
                {
                    ProxyUserControl row = rows[i] as ProxyUserControl;
                    // Change the field value. 
                    row.Enabled = false;
                }
            }
            finally
            {
                gridView_Proxy.EndUpdate();
            }

            var proxies = this.grid_Proxy.DataSource as List<Proxy>;
            var eventArgs = new UpdateProxyListEventArgs()
            {
                ProxyList = proxies
            };
            if (this.UpdateProxyListEvent != null)
                this.UpdateProxyListEvent(this, eventArgs);
        }

        private void btn_remove_Click(object sender, EventArgs e)
        {
            gridView_Proxy.DeleteSelectedRows();
            var proxies = this.grid_Proxy.DataSource as List<Proxy>;
            var eventArgs = new UpdateProxyListEventArgs()
            {
                ProxyList = proxies
            };
            if (this.UpdateProxyListEvent != null)
                this.UpdateProxyListEvent(this, eventArgs);
        }
    }
}
