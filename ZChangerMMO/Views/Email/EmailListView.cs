using DevExpress.Utils.MVVM;
using DevExpress.Utils.MVVM.Services;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using FastMember;
using System;
using System.Data;
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

        DataTable emailDataTable;
        DataTable deviceDataTable;

        RepositoryItemButtonEdit detailGridButtons;

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
            var devices = fluentAPIEmailList.ViewModel.GetDevices();
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

            fluentAPIEmailList.WithEvent<ColumnView, FocusedRowObjectChangedEventArgs>(deviceGridView, "FocusedRowObjectChanged")
              .SetBinding(x => x.SelectedDevice,
                  args => GetDeviceItem(args),
                  (gView, entity) => gView.FocusedRowHandle = gView.FindRow(entity));

            fluentAPIEmailList.SetTrigger(x => x.DisconectID, x =>
            {
                fluentAPIEmailList.ViewModel.StopException(deviceDataTable);
                gridControl1.Invoke(new Action(() =>
                {
                    deviceGridView.LayoutChanged();
                }));
            });

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
            var dataRow = (e.Row as DataRowView).Row;

            return new Models.Email()
            {
                ID = (long)dataRow["ID"],
                Name = (string)dataRow["Name"],
                EmailAccount = (string)dataRow["EmailAccount"]
            };
        }

        private Models.Device GetDeviceItem(FocusedRowObjectChangedEventArgs e)
        {
            var dataRow = (e.Row as DataRowView).Row;

            return new Models.Device()
            {
                ID = (long)dataRow["ID"],
                Name = (string)dataRow["Name"],
                Type = (Models.DeviceType)Enum.ToObject(typeof(Models.DeviceType), dataRow["Type"]),
                EmailID = (long)dataRow["EmailID"],
                Email = (Models.Email)dataRow["Email"],
            };
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

            var masterGridButtons = GetMasterRepositoryItem();
            gridControl1.RepositoryItems.Add(masterGridButtons);

            unboundColumn.ColumnEdit = masterGridButtons;
        }

        #endregion Master Buttons

        #region Detail Buttons
        public void SetNormalButton(EditorButtonCollection editorButtons)
        {
            editorButtons[0].Kind = ButtonPredefines.Glyph;
            editorButtons[0].Image = Properties.Resources.play_16px;

            fluentAPIEmailList.BindCommand(editorButtons[0], (x, p) => x.Run(p), x => deviceDataTable);
            fluentAPIEmailList.WithCommand(x => x.Run(null)).After(() => deviceGridView.LayoutChanged());
        }

        public void SetRunningButton(EditorButtonCollection editorButtons)
        {
            editorButtons[0].Kind = ButtonPredefines.Glyph;
            editorButtons[0].Image = Properties.Resources.stop_16px;

            fluentAPIEmailList.BindCommand(editorButtons[0], (x, d) => x.Stop(d), x => deviceDataTable);
            fluentAPIEmailList.WithCommand(x => x.Stop(null)).After(() => deviceGridView.LayoutChanged());
        }

        private RepositoryItemButtonEdit GetDetailGridRepositoryItem(bool isRunning)
        {
            var normalRiButtonEdit = new RepositoryItemButtonEdit();
            normalRiButtonEdit.TextEditStyle = TextEditStyles.HideTextEditor;
            if (isRunning == true)
            {
                SetRunningButton(normalRiButtonEdit.Buttons);
            }
            else
            {
                SetNormalButton(normalRiButtonEdit.Buttons);
            }
            normalRiButtonEdit.Buttons.Add(new EditorButton() { Kind = ButtonPredefines.Glyph, Image = Properties.Resources.edit_16px });
            normalRiButtonEdit.Buttons.Add(new EditorButton() { Kind = ButtonPredefines.Glyph, Image = Properties.Resources.data_backup_16px });
            normalRiButtonEdit.Buttons.Add(new EditorButton() { Kind = ButtonPredefines.Glyph, Image = Properties.Resources.delete_16px });

            fluentAPIEmailList.BindCommand(normalRiButtonEdit.Buttons[1], x => x.UpdateDevice());
            fluentAPIEmailList.BindCommand(normalRiButtonEdit.Buttons[2], x => x.DeleteDevice());
            fluentAPIEmailList.BindCommand(normalRiButtonEdit.Buttons[3], x => x.DeleteDevice());

            return normalRiButtonEdit;
        }

        private void DeviceGridView_CustomRowCellEdit(object sender, CustomRowCellEditEventArgs e)
        {
            GridView View = (GridView)sender;
            if (e.Column.Name == "colOptions")
            {
                var running = (bool)View.GetRowCellValue(e.RowHandle, "Running");
                e.RepositoryItem = GetDetailGridRepositoryItem(running);
            }
        }

        private void AddDetailGridOptionButtons()
        {
            var unboundColumn = deviceGridView.Columns.AddVisible("Options");
            unboundColumn.UnboundType = DevExpress.Data.UnboundColumnType.Object;
            unboundColumn.Width = 120;
            unboundColumn.MaxWidth = 120;
            unboundColumn.MinWidth = 120;

            detailGridButtons = GetDetailGridRepositoryItem(false);
            gridControl1.RepositoryItems.Add(detailGridButtons);

            deviceGridView.CustomRowCellEdit += DeviceGridView_CustomRowCellEdit;
        }

        #endregion Detail Buttons       
    }
}
