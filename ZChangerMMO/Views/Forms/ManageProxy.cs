using CommandModel;
using DevExpress.XtraEditors;
using System;
using System.Activities.Expressions;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows.Forms;
using ZChangerMMO.DataModels;
using ZChangerMMO.Events;
using ZChangerMMO.Views.Controls;
using ZChangerMMO.Views.Events;

namespace ZChangerMMO.Views.Forms
{
    public partial class ManageProxy : DevExpress.XtraEditors.XtraForm
    {

        #region Events
        public event EventHandler<SaveProxyEventArgs> SaveClick;
        #endregion

        #region Properties
        private Profile Profile { get; set; }
        private IList<Proxy> ProxyList { get; set; }
        private IList<string> ByPassProxySiteList { get; set; }
        private IList<Proxy> FailProxyList { get; set; }

        #endregion

        public ManageProxy(Profile profile)
        {
            InitializeComponent();
            Profile = profile;
            FailProxyList = new List<Proxy>();
            ProxyList = new List<Proxy>();
            ByPassProxySiteList = new List<string>();
            ProxyList = profile.Proxies.Clone();
            ByPassProxySiteList = new List<string>(profile.ByPassProxySites);
            addASignleProxy_AddProxyUserControl.AddProxyClick += new EventHandler<AddProxy_AddProxyClickEventArgs>(UserControl_AddProxyClick);
            proxyListTab_ProxyListUserControl.UpdateProxyListEvent += new EventHandler<UpdateProxyListEventArgs>(UserControl_UpdateProxyListEventHandler);
            byPassProxy_ByPassProxiesUserControl.AddByPassSiteListEvent += new EventHandler<AddByPassSiteListEventArgs>(UserControl_AddByPassSiteListEventHandler);
            byPassProxy_ByPassProxiesUserControl.UpdateByPassSiteListEvent += new EventHandler<UpdateByPassSiteListEventArgs>(UserControl_UpdateByPassSiteListEventHandler);
            testProxyTab_TestProxyUserControl.TestProxyClick += new EventHandler(UserControl_TestProxyEventHandler);
            addBulkProxies1.simpleButtonExportProxyList.Click += new EventHandler(ButtonExportProxyListClick);
            addBulkProxies1.simpleButtonImportProxyList.Click += new EventHandler(ButtonImportProxyListClick);
        }


        #region private actions
        private void LoadProxyGrid()
        {
            try
            {
                proxyListTab_ProxyListUserControl.grid_Proxy.DataSource = ProxyList.ToList();
                addASingleProxyTab_ProxyListUserControl.grid_Proxy.DataSource = ProxyList.ToList();
                addBulkProxiesTab_ProxyListUserControl.grid_Proxy.DataSource = ProxyList.ToList();
                testProxiesTab_ProxyListUserControl.grid_Proxy.DataSource = ProxyList.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SetDataSourceProxyGrid(object proxyList)
        {
            try
            {
                proxyListTab_ProxyListUserControl.grid_Proxy.DataSource = proxyList;
                addASingleProxyTab_ProxyListUserControl.grid_Proxy.DataSource = proxyList;
                addBulkProxiesTab_ProxyListUserControl.grid_Proxy.DataSource = proxyList;
                testProxiesTab_ProxyListUserControl.grid_Proxy.DataSource = proxyList;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void LoadByPassSitesList()
        {
            byPassProxy_ByPassProxiesUserControl.listBoxControl_ByPassSites.DataSource = ByPassProxySiteList;
            byPassProxy_ByPassProxiesUserControl.listBoxControl_ByPassSites.Refresh();
        }

        private void TestProxies(Uri url)
        {
            List<string> proxies = new List<string>();

            WebProxy myProxy = default(WebProxy);
            foreach (Proxy proxy in ProxyList)
            {

                testProxyTab_TestProxyUserControl.progressPanel_Testing.Text = $"Testing proxy: {proxy.Name}";
                try
                {
                    var proxyURI = new Uri(string.Format("http://{0}:{1}", proxy.Host, proxy.Port));
                    if (!String.IsNullOrEmpty(proxy.UserName) && !String.IsNullOrEmpty(proxy.Password))
                    {

                        ICredentials credentials = new NetworkCredential(proxy.UserName, proxy.Password);
                        myProxy = new WebProxy(proxyURI, true, null, credentials);
                    }
                    else
                    {
                        myProxy = new WebProxy(proxyURI, true);
                    }

                    //myProxy = new WebProxy(proxy.Host, int.Parse(proxy.Port));
                    WebRequest r = WebRequest.Create(url);
                    r.Timeout = 3000;
                    r.Proxy = myProxy;
                    WebResponse re = r.GetResponse();
                }
                catch (Exception ex)
                {
                    FailProxyList.Add(proxy);
                }
            }
        }
        #endregion


        #region events
        private void proxyListTab_ProxyListUserControl_Load(object sender, EventArgs e)
        {
            LoadProxyGrid();
            LoadByPassSitesList();
        }


        protected void UserControl_AddProxyClick(object sender, AddProxy_AddProxyClickEventArgs e)
        {
            ProxyList.Add(e.Proxy);
            LoadProxyGrid();
        }

        protected void UserControl_UpdateProxyListEventHandler(object sender, UpdateProxyListEventArgs e)
        {
            ProxyList = e.ProxyList;
            LoadProxyGrid();
        }

        protected void UserControl_AddByPassSiteListEventHandler(object sender, AddByPassSiteListEventArgs e)
        {
            ByPassProxySiteList.Add(e.Site);
            LoadByPassSitesList();
        }

        protected void UserControl_UpdateByPassSiteListEventHandler(object sender, UpdateByPassSiteListEventArgs e)
        {
            ByPassProxySiteList = e.ByPassSiteList;
            LoadByPassSitesList();
        }

        protected void UserControl_TestProxyEventHandler(object sender, EventArgs e)
        {
            testProxyTab_TestProxyUserControl.UpdateUI(TestProxyStatus.Testing);
            testProxyWorker.RunWorkerAsync();
        }



        #endregion

        private void btn_Save_Click(object sender, EventArgs e)
        {
            SaveProxyEventArgs args = new SaveProxyEventArgs()
            {
                ProxyList = ProxyList,
                ByPassProxySiteList = ByPassProxySiteList
            };

            if (this.SaveClick != null)
                this.SaveClick(this, args);

            this.Close();
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void testProxyWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            string url = testProxyTab_TestProxyUserControl.txt_Url.Text;
            try
            {
                var testURI = new Uri(url);
                TestProxies(testURI);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Bad Url", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void testProxyWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            testProxyTab_TestProxyUserControl.progressPanel_Testing.Text = String.Empty;
            if (FailProxyList.Count > 0)
            {
                var fails = FailProxyList.Select(p => p.Name);
                string message = String.Join(", ", fails);
                if (XtraMessageBox.Show(message, "Bad proxies", MessageBoxButtons.OK, MessageBoxIcon.Warning) == DialogResult.OK)
                {
                    FailProxyList = new List<Proxy>();
                }
            }
            testProxyTab_TestProxyUserControl.UpdateUI(TestProxyStatus.Normal);
        }

        private void testProxyWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {

        }

        #region Add Bulk Proxies
        protected void ButtonExportProxyListClick(object sender, EventArgs e)
        {
            var saveFileDialog = new SaveFileDialog
            {
                Title = "Export Proxies",
                Filter = "Text files (*.xls)|*.xls",
                FileName = "Proxies.xls"
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                proxyListTab_ProxyListUserControl.grid_Proxy.ExportToXlsx(saveFileDialog.FileName);
            }
        }

        internal static IList<Proxy> OpenFile(string fileName)
        {
            try
            {
                string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fileName + ";Extended Properties=Excel 12.0;";
                var adapter = new OleDbDataAdapter("select * from [Sheet$]", connectionString);
                var ds = new DataSet();
                string tableName = "excelData";
                adapter.Fill(ds, tableName);
                DataTable data = ds.Tables[tableName];

                var convertedList = (from rw in data.AsEnumerable()
                                     select new Proxy()
                                     {
                                         Name = Convert.ToString(rw["Name"]),
                                         Host = Convert.ToString(rw["Host"]),
                                         Port = Convert.ToString(rw["Port"]),
                                         Scheme = Convert.ToString(rw["Scheme"]),
                                         UserName = Convert.ToString(rw["UserName"]),
                                         Password = Convert.ToString(rw["Password"]),
                                         Enabled = Convert.ToBoolean(rw["Enabled"]),
                                     }).ToList();

                return convertedList;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        protected void ButtonImportProxyListClick(object sender, EventArgs e)
        {
            var openFileDialog = new OpenFileDialog
            {
                Title = "Import Proxies",
                Filter = "Text files (*.xls)|*.xls"
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                var fileName = openFileDialog.FileName;
                if (!File.Exists(fileName))
                {
                    MessageBox.Show("File not found");
                    return;
                }

                try
                {
                    var importedProxies = OpenFile(fileName);
                    if (importedProxies != null)
                    {
                        (ProxyList as List<Proxy>).AddRange(importedProxies);
                        LoadProxyGrid();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        #endregion Add Bulk Proxies
    }
}