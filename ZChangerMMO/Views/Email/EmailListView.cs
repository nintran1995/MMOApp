using DevExpress.Mvvm.POCO;
using DevExpress.Utils.MVVM;
using DevExpress.Utils.MVVM.Services;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using FastMember;
using System;
using System.ComponentModel;
using System.Data;
using System.Threading;
using System.Windows.Documents;
using ZChangerMMO.ViewModels;

namespace ZChangerMMO.Views.Email
{
    public partial class EmailListView : XtraUserControl
    {
        public EmailListView()
        {
            InitializeComponent();
            if (!mvvmContextEmailList.IsDesignMode)
                InitializeBindings();
        }

        MVVMContextFluentAPI<EmailListViewModel> fluentAPIEmailList;
        MVVMContextFluentAPI<DeviceListViewModel> fluentAPIDeviceList;

        DataTable emailDataTable;
        DataTable deviceDataTable;

        RepositoryItemButtonEdit masterButtons;
        RepositoryItemButtonEdit detailNormalButtons;
        RepositoryItemButtonEdit detailRuningButtons;

        //private BindingList<GridDataSource> _gridDataSource = new BindingList<GridDataSource>();

        public void SetDatasource()
        {
            // Set data
            DataSet dataSet11 = new DataSet();
            emailDataTable = dataSet11.Tables.Add("Emails");
            deviceDataTable = dataSet11.Tables.Add("Devices");
            var emails = fluentAPIEmailList.ViewModel.GetEmails();
            using (var reader = ObjectReader.Create(emails))
            {
                emailDataTable.Load(reader);
            }
            var devices = fluentAPIDeviceList.ViewModel.GetDevices();
            using (var reader = ObjectReader.Create(devices))
            {
                deviceDataTable.Load(reader);
            }

            //Set up a master-detail relationship between the DataTables
            DataColumn keyColumn = emailDataTable.Columns["ID"];
            DataColumn foreignKeyColumn = deviceDataTable.Columns["EmailID"];
            dataSet11.Relations.Add("EmailDevices", keyColumn, foreignKeyColumn);

            //Bind the grid control to the data source
            gridControl1.DataSource = emailDataTable;
            gridControl1.ForceInitialize();

            //Master grid
            emailGridView.OptionsSelection.MultiSelect = true;
            emailGridView.OptionsView.ShowGroupPanel = false;
            emailGridView.OptionsDetail.ShowDetailTabs = false;

            //Assign a CardView to the relationship
            deviceGridView = new GridView(gridControl1);
            deviceGridView.OptionsView.ShowGroupPanel = false;
            gridControl1.LevelTree.Nodes.Add("EmailDevices", deviceGridView);

            //Hide the CategoryID column for the master View
            emailGridView.Columns["ID"].VisibleIndex = -1;

            // Create columns for the detail pattern View
            deviceGridView.PopulateColumns(deviceDataTable);
            //Hide the CategoryID column for the detail View
            deviceGridView.Columns["EmailID"].VisibleIndex = -1;
            deviceGridView.Columns["Email"].VisibleIndex = -1;
            deviceGridView.Columns["ID"].VisibleIndex = -1;
            deviceGridView.Columns["Running"].VisibleIndex = -1;
        }

        private void InitializeBindings()
        {
            // Init FluentAPI
            fluentAPIEmailList = mvvmContextEmailList.OfType<EmailListViewModel>();
            fluentAPIDeviceList = mvvmContextDeviceList.OfType<DeviceListViewModel>();

            // Register Service
            mvvmContextEmailList.RegisterService(NotificationService.Create(toastNotificationsManager1));

            //Bind Event
            fluentAPIEmailList.BindCommand(btnNew, x => x.Create());

            SetDatasource();

            //Handle select row event
            fluentAPIEmailList.WithEvent<ColumnView, FocusedRowObjectChangedEventArgs>(emailGridView, "FocusedRowObjectChanged")
              .SetBinding(x => x.SelectedItem,
                  args => GetEmailItem(args),
                  (gView, entity) => gView.FocusedRowHandle = gView.FindRow(entity));

            fluentAPIDeviceList.WithEvent<ColumnView, FocusedRowObjectChangedEventArgs>(deviceGridView, "FocusedRowObjectChanged")
              .SetBinding(x => x.SelectedItem,
                  args => GetDeviceItem(args),
                  (gView, entity) => gView.FocusedRowHandle = gView.FindRow(entity));

            //fluentAPI.WithEvent<RowCellClickEventArgs>(emailGridView, "RowCellClick")
            //   .EventToCommand(
            //       x => x.Edit(null), x => x.SelectedItem,
            //       args => (args.Clicks == 2) && (args.Button == System.Windows.Forms.MouseButtons.Left));

            // Init button
            AddMasterGridOptionButtons();
            AddDetailGridOptionButtons();
        }

        private Models.Email GetEmailItem(FocusedRowObjectChangedEventArgs e)
        {
            var dataRowView = e.Row as DataRowView;
            var itemArray = dataRowView.Row.ItemArray;
            var email = new Models.Email()
            {
                ID = (long)itemArray[2],
                Name = (string)itemArray[3],
                EmailAccount = (string)itemArray[1]
            };

            return email;
        }

        private Models.Device GetDeviceItem(FocusedRowObjectChangedEventArgs e)
        {
            var dataRowView = e.Row as DataRowView;
            var itemArray = dataRowView.Row.ItemArray;
            var device = new Models.Device()
            {
                ID = (long)itemArray[2],
                Name = (string)itemArray[3],
                Type = (Models.DeviceType)Enum.ToObject(typeof(Models.DeviceType), itemArray[4]),
                EmailID = (long)itemArray[1],
                Email = (Models.Email)itemArray[0],
            };

            return device;
        }

        #region Master Buttons

        private RepositoryItem GetMasterRepositoryItem()
        {
            var riButtonEdit = new RepositoryItemButtonEdit();
            riButtonEdit.TextEditStyle = TextEditStyles.HideTextEditor;
            riButtonEdit.Buttons[0].Kind = ButtonPredefines.Glyph;
            riButtonEdit.Buttons[0].Image = Properties.Resources.add_16px;
            riButtonEdit.Buttons.Add(new EditorButton() { Kind = ButtonPredefines.Glyph, Image = Properties.Resources.edit_16px });
            riButtonEdit.Buttons.Add(new EditorButton() { Kind = ButtonPredefines.Glyph, Image = Properties.Resources.delete_16px });

            fluentAPIEmailList.BindCommand(riButtonEdit.Buttons[0], x => x.CreateDevice());
            fluentAPIEmailList.BindCommand(riButtonEdit.Buttons[1], x => x.Update());
            fluentAPIEmailList.BindCommand(riButtonEdit.Buttons[2], x => x.Delete());

            return riButtonEdit;
        }

        private void AddMasterGridOptionButtons()
        {
            var unboundColumn = emailGridView.Columns.AddVisible("Options");
            unboundColumn.UnboundType = DevExpress.Data.UnboundColumnType.Object;
            unboundColumn.Width = 120;
            unboundColumn.MaxWidth = 120;
            unboundColumn.MinWidth = 120;

            var repositoryItem = GetMasterRepositoryItem();
            gridControl1.RepositoryItems.Add(repositoryItem);

            unboundColumn.ColumnEdit = repositoryItem;
        }

        #endregion Master Buttons

        #region Detail Buttons

        private RepositoryItemButtonEdit GetDetailGridNormalRepositoryItem()
        {
            var normalRiButtonEdit = new RepositoryItemButtonEdit();
            normalRiButtonEdit.TextEditStyle = TextEditStyles.HideTextEditor;
            normalRiButtonEdit.Buttons[0].Kind = ButtonPredefines.Glyph;
            normalRiButtonEdit.Buttons[0].Image = Properties.Resources.play_16px;
            normalRiButtonEdit.Buttons.Add(new EditorButton() { Kind = ButtonPredefines.Glyph, Image = Properties.Resources.edit_16px });
            normalRiButtonEdit.Buttons.Add(new EditorButton() { Kind = ButtonPredefines.Glyph, Image = Properties.Resources.delete_16px });

            fluentAPIDeviceList.BindCommand(normalRiButtonEdit.Buttons[0], (x, p) => x.Run(p), x => deviceDataTable);
            fluentAPIDeviceList.WithCommand(x => x.Run(null)).After(() => deviceGridView.LayoutChanged());
            fluentAPIEmailList.BindCommand(normalRiButtonEdit.Buttons[1], x => x.CreateDevice());

            normalRiButtonEdit.Buttons[0].Click += EmailListView_Click;
            return normalRiButtonEdit;
        }

        private RepositoryItemButtonEdit GetDetailGridRunningRepositoryItem()
        {
            var runningRiButtonEdit = new RepositoryItemButtonEdit();
            runningRiButtonEdit.TextEditStyle = TextEditStyles.HideTextEditor;
            runningRiButtonEdit.Buttons[0].Kind = ButtonPredefines.Glyph;
            runningRiButtonEdit.Buttons[0].Image = Properties.Resources.stop_16px;
            runningRiButtonEdit.Buttons.Add(new EditorButton() { Kind = ButtonPredefines.Glyph, Image = Properties.Resources.edit_16px });
            runningRiButtonEdit.Buttons.Add(new EditorButton() { Kind = ButtonPredefines.Glyph, Image = Properties.Resources.delete_16px });

            fluentAPIDeviceList.BindCommand(runningRiButtonEdit.Buttons[0], (x, d) => x.Stop(d), x => deviceDataTable);
            fluentAPIDeviceList.WithCommand(x => x.Stop(null)).After(() => deviceGridView.LayoutChanged());
            fluentAPIEmailList.BindCommand(runningRiButtonEdit.Buttons[1], x => x.CreateDevice());
            runningRiButtonEdit.Buttons[0].Click += EmailListView_Click;

            return runningRiButtonEdit;
        }

        private void EmailListView_Click(object sender, EventArgs e)
        {
            gridControl1.Refresh();
        }

        private void DeviceGridView_CustomRowCellEdit(object sender, CustomRowCellEditEventArgs e)
        {
            GridView View = (GridView)sender;
            if (e.Column.Name == "colOptions")
            {
                var running = (bool)View.GetRowCellValue(e.RowHandle, "Running");
                if (running == true)
                {
                    e.RepositoryItem = detailRuningButtons;
                }
                else
                {
                    e.RepositoryItem = detailNormalButtons;
                }
            }
        }

        private void AddDetailGridOptionButtons()
        {
            var unboundColumn = deviceGridView.Columns.AddVisible("Options");
            unboundColumn.UnboundType = DevExpress.Data.UnboundColumnType.Object;
            unboundColumn.Width = 120;
            unboundColumn.MaxWidth = 120;
            unboundColumn.MinWidth = 120;

            detailNormalButtons = GetDetailGridNormalRepositoryItem();
            detailRuningButtons = GetDetailGridRunningRepositoryItem();

            gridControl1.RepositoryItems.Add(detailNormalButtons);
            gridControl1.RepositoryItems.Add(detailRuningButtons);

            deviceGridView.CustomRowCellEdit += DeviceGridView_CustomRowCellEdit;
        }

        #endregion Detail Buttons       
    }
}
