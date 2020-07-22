using DevExpress.Utils.MVVM;
using DevExpress.Utils.MVVM.Services;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using FastMember;
using System.Data;
using ZChangerMMO.ViewModels;

namespace ZChangerMMO.Views.Email
{
    public partial class EmailListView : XtraUserControl
    {
        MVVMContextFluentAPI<EmailListViewModel> fluentAPI;

        public EmailListView()
        {
            InitializeComponent();
            if (!mvvmContext1.IsDesignMode)
                InitializeBindings();

            emailGridView.OptionsSelection.MultiSelect = true;
            emailGridView.OptionsSelection.MultiSelectMode = GridMultiSelectMode.RowSelect;
            emailGridView.OptionsView.ShowGroupPanel = false;
            emailGridView.OptionsDetail.ShowDetailTabs = false;
            
        }

        private void InitializeBindings()
        {
            fluentAPI = mvvmContext1.OfType<EmailListViewModel>();
            mvvmContext1.RegisterService(NotificationService.Create(toastNotificationsManager1));
            //fluentAPI.SetBinding(emailGridView, gView => gView.LoadingPanelVisible, x => x.IsLoading);

            DataSet dataSet11 = new DataSet();
            DataTable tableEmail = dataSet11.Tables.Add("Emails");
            DataTable tableDevice = dataSet11.Tables.Add("Devices");
            var emails = fluentAPI.ViewModel.GetEmails();
            using (var reader = ObjectReader.Create(emails))
            {
                tableEmail.Load(reader);
            }
            var devices = fluentAPI.ViewModel.GetDevices();
            using (var reader = ObjectReader.Create(devices))
            {
                tableDevice.Load(reader);
            }

            //Set up a master-detail relationship between the DataTables
            DataColumn keyColumn = dataSet11.Tables["Emails"].Columns["ID"];
            DataColumn foreignKeyColumn = dataSet11.Tables["Devices"].Columns["EmailID"];
            dataSet11.Relations.Add("EmailDevices", keyColumn, foreignKeyColumn);

            //Bind the grid control to the data source
            gridControl1.DataSource = dataSet11.Tables["Emails"];
            gridControl1.ForceInitialize();

            //Assign a CardView to the relationship
            GridView deviceGridView = new GridView(gridControl1);
            deviceGridView.OptionsView.ShowGroupPanel = false;
            gridControl1.LevelTree.Nodes.Add("EmailDevices", deviceGridView);

            //Hide the CategoryID column for the master View
            emailGridView.Columns["ID"].VisibleIndex = -1;

            // Create columns for the detail pattern View
            deviceGridView.PopulateColumns(dataSet11.Tables["Devices"]);
            //Hide the CategoryID column for the detail View
            deviceGridView.Columns["EmailID"].VisibleIndex = -1;
            deviceGridView.Columns["Email"].VisibleIndex = -1;
            deviceGridView.Columns["ID"].VisibleIndex = -1;

            //Handle select row event
            fluentAPI.WithEvent<ColumnView, FocusedRowObjectChangedEventArgs>(emailGridView, "FocusedRowObjectChanged")
              .SetBinding(x => x.SelectedItem,
                  args => GetEmailItem(args),
                  (gView, entity) => gView.FocusedRowHandle = gView.FindRow(entity));

            //fluentAPI.WithEvent<RowCellClickEventArgs>(emailGridView, "RowCellClick")
            //   .EventToCommand(
            //       x => x.Edit(null), x => x.SelectedItem,
            //       args => (args.Clicks == 2) && (args.Button == System.Windows.Forms.MouseButtons.Left));


            AddMasterGridOptionButtons();
            AddDetailGridOptionButtons(deviceGridView);
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

        #region Option Buttons

        private RepositoryItem GetMasterRepositoryItem()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EmailListView));

            var riButtonEdit = new RepositoryItemButtonEdit();
            riButtonEdit.TextEditStyle = TextEditStyles.HideTextEditor;
            riButtonEdit.Buttons[0].Kind = ButtonPredefines.Glyph;
            riButtonEdit.Buttons[0].Image = Properties.Resources.add_16px;
            riButtonEdit.Buttons.Add(new EditorButton() { Kind = ButtonPredefines.Glyph, Image = Properties.Resources.edit_16px });
            riButtonEdit.Buttons.Add(new EditorButton() { Kind = ButtonPredefines.Glyph, Image = Properties.Resources.delete_16px });

            var fluentAPI = mvvmContext1.OfType<EmailListViewModel>();
            fluentAPI.BindCommand(riButtonEdit.Buttons[0], x => x.CreateDevice());
            fluentAPI.BindCommand(riButtonEdit.Buttons[1], x => x.Update());
            fluentAPI.BindCommand(riButtonEdit.Buttons[2], x => x.Delete());

            return riButtonEdit;
        }

        private void AddMasterGridOptionButtons()
        {
            var repositoryItem = GetMasterRepositoryItem();
            var unboundColumn = emailGridView.Columns.AddVisible("Options");
            unboundColumn.UnboundType = DevExpress.Data.UnboundColumnType.Object;
            unboundColumn.Width = 120;
            unboundColumn.MaxWidth = 120;
            unboundColumn.MinWidth = 120;
            gridControl1.RepositoryItems.Add(repositoryItem);
            unboundColumn.ColumnEdit = repositoryItem;
        }

        private RepositoryItem CreateDetailGridRepositoryItem()
        {
            var riButtonEdit = new RepositoryItemButtonEdit();
            riButtonEdit.TextEditStyle = TextEditStyles.HideTextEditor;
            riButtonEdit.Buttons[0].Kind = ButtonPredefines.Glyph;
            riButtonEdit.Buttons[0].Image = Properties.Resources.play_16px;
            riButtonEdit.Buttons.Add(new EditorButton() { Kind = ButtonPredefines.Glyph, Image = Properties.Resources.edit_16px });
            riButtonEdit.Buttons.Add(new EditorButton() { Kind = ButtonPredefines.Glyph, Image = Properties.Resources.delete_16px });

            var fluentAPI = mvvmContext1.OfType<EmailListViewModel>();
            fluentAPI.BindCommand(riButtonEdit.Buttons[0], x => x.CreateDevice());

            return riButtonEdit;
        }

        private void AddDetailGridOptionButtons(GridView deviceGridView)
        {
            var unboundColumn = deviceGridView.Columns.AddVisible("Options");
            unboundColumn.UnboundType = DevExpress.Data.UnboundColumnType.Object;
            unboundColumn.Width = 120;
            unboundColumn.MaxWidth = 120;
            unboundColumn.MinWidth = 120;
            unboundColumn.ColumnEdit = CreateDetailGridRepositoryItem();
        }

        #endregion Option Buttons       
    }
}
