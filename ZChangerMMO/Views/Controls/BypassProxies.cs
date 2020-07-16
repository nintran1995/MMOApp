using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZChangerMMO.Views.Events;

namespace ZChangerMMO.Views.Controls
{
    public partial class BypassProxies : UserControl
    {
        public event EventHandler<UpdateByPassSiteListEventArgs> UpdateByPassSiteListEvent;
        public event EventHandler<AddByPassSiteListEventArgs> AddByPassSiteListEvent;
        public BypassProxies()
        {
            InitializeComponent();
        }

        private void btn_AddSite_Click(object sender, EventArgs e)
        {
            AddByPassSiteListEventArgs args = new AddByPassSiteListEventArgs()
            {
                Site = txt_SiteAddress.Text,
            };

            if (this.AddByPassSiteListEvent != null)
                this.AddByPassSiteListEvent(this, args);

            ClearForm();
        }

        private void ClearForm()
        {
            txt_SiteAddress.Text = String.Empty;
        }

        private void btn_DeleteSite_Click(object sender, EventArgs e)
        {
            var selectedItem = listBoxControl_ByPassSites.SelectedItem.ToString();
            if (selectedItem == null) return;
            var listItems = listBoxControl_ByPassSites.DataSource as List<string>;
            listItems.Remove(selectedItem);
           
            var eventArgs = new UpdateByPassSiteListEventArgs()
            {
                ByPassSiteList = listItems,
            };

            if (this.UpdateByPassSiteListEvent != null)
                this.UpdateByPassSiteListEvent(this, eventArgs);
        }
    }
}
