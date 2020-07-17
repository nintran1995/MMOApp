using DevExpress.Mvvm;
using System;
using System.ComponentModel;
using System.Windows.Forms;
using ZChangerMMO.ViewModels;

namespace ZChangerMMO.Views
{
    public partial class frmMainView : DevExpress.XtraEditors.XtraForm
    {
        public frmMainView()
        {
            InitializeComponent();
            if (!DesignMode)
                InitializeNavigation();
            ribbonControl1.Merge += ribbonControl1_Merge;
        }
        void InitializeNavigation()
        {
            var fluentAPI = mvvmContext1.OfType<MyDbContextViewModel>();
            fluentAPI.BindCommand(biAccounts, (x, m) => x.Show(m), x => x.Modules[0]);
            fluentAPI.BindCommand(biCategories, (x, m) => x.Show(m), x => x.Modules[1]);

            fluentAPI.WithEvent(this, "Load")
                .EventToCommand(x => x.OnLoaded(null), x => x.DefaultModule);
            fluentAPI.WithEvent<FormClosingEventArgs>(this, "FormClosing")
                .EventToCommand(x => x.OnClosing(null), new Func<CancelEventArgs, object>((args) => args));

            Messenger.Default.Register<string>(this, OnUserNameMessage);
        }

        void OnUserNameMessage(string userName)
        {
            if (string.IsNullOrEmpty(userName))
                this.Text = "Expenses Application";
            else
                this.Text = "Expenses Application - (" + userName + ")";
        }

        void ribbonControl1_Merge(object sender, DevExpress.XtraBars.Ribbon.RibbonMergeEventArgs e)
        {
            ribbonControl1.SelectedPage = e.MergedChild.SelectedPage;
        }
    }
}