using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows;
using ZChangerMMO.ViewModels;

namespace ZChangerMMO.Views
{
    public partial class EmailAccountView : DevExpress.XtraEditors.XtraUserControl
    {
        public EmailAccountView()
        {
            InitializeComponent();
            InitializeGridView();
        }

        private void InitializeGridView()
        {
            mvvmContext1.ViewModelType = typeof(EmailAccountViewModel);
            var fluentAPI = mvvmContext1.OfType<EmailAccountViewModel>();

            ExpandedMasterRowsManager expandedMasterRowsManager = new ExpandedMasterRowsManager(emailGridView);

            fluentAPI.SetBinding(gridControl1, grid => grid.DataSource, x => x.Emails);

            fluentAPI.SetBinding(expandedMasterRowsManager, x => x.ExpandedMasterRowsCollection, y => y.ExpandedEntitiesCollection);
            //fluentAPI.SetBinding(countLabelControl, x => x.Text, y => y.ExpandedEntitiesCount);

            fluentAPI.WithEvent<CustomMasterRowEventArgs>(emailGridView, "MasterRowExpanded").EventToCommand(x => x.Update(null), ea => { return expandedMasterRowsManager.GetCategoryID(ea.RowHandle); });
            fluentAPI.WithEvent<CustomMasterRowEventArgs>(emailGridView, "MasterRowCollapsed").EventToCommand(x => x.Update(null), ea => { return expandedMasterRowsManager.GetCategoryID(ea.RowHandle); });
            fluentAPI.WithEvent(emailGridView, "DataSourceChanged").EventToCommand(x => x.UpdateDataBindings());

            //fluentAPI.WithEvent(gridView1, "EditValueChanged").EventToCommand(x => x.UpdateCommands(GetCategoryIDFromEditor()));

            //fluentAPI.BindCommand(buttonExpand, x => x.ExpandEntity(null), x => GetCategoryIDFromEditor());
            //fluentAPI.BindCommand(buttonCollapse, x => x.CollapseEntity(null), x => GetCategoryIDFromEditor());
            //fluentAPI.BindCommand(buttonDelete, x => x.DeleteEntity(null), x => GetCategoryIDFromEditor());

            AddOptionButtons();
        }

        #region Option Buttons

        private void RiButtonEdit_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == ButtonPredefines.Close)
                MessageBox.Show("Close");
            if (e.Button.Kind == ButtonPredefines.OK)
                MessageBox.Show("OK");
        }

        private RepositoryItem CreateRepositoryItem()
        {
            var riButtonEdit = new RepositoryItemButtonEdit();
            riButtonEdit.TextEditStyle = TextEditStyles.HideTextEditor;
            riButtonEdit.Buttons[0].Kind = ButtonPredefines.Plus;
            riButtonEdit.Buttons.Add(new EditorButton() { Kind = ButtonPredefines.Close });
            riButtonEdit.ButtonClick += RiButtonEdit_ButtonClick;

            return riButtonEdit;
        }

        private void EmailGridView_CustomRowCellEdit(object sender, CustomRowCellEditEventArgs e)
        {
            if (e.Column.FieldName != "Options")
                return;
            e.RepositoryItem = CreateRepositoryItem();
        }

        private void AddOptionButtons()
        {
            var unboundColumn = emailGridView.Columns.AddVisible("Options");
            unboundColumn.UnboundType = DevExpress.Data.UnboundColumnType.Object;
            unboundColumn.Width = 50;

            emailGridView.CustomRowCellEdit += EmailGridView_CustomRowCellEdit;
            emailGridView.OptionsView.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
        }

        #endregion Option Buttons       

        //private int GetCategoryIDFromEditor()
        //{
        //    return Convert.ToInt32(categoryIdEdit.EditValue);
        //}

        class ExpandedMasterRowsManager : IDisposable
        {
            public ExpandedMasterRowsManager(GridView _view)
            {
                view = _view;
                SubscribeViewEvents(true);
            }

            GridView view;
            ObservableCollection<object> expandedMasterRowsCollection;
            bool viewIsNull { get { return view == null; } }
            bool lockExpandedMasterRowsCollectionChanged { get; set; }
            bool lockMasterRowExpanded = false;

            public ObservableCollection<object> ExpandedMasterRowsCollection
            {
                get
                {
                    return expandedMasterRowsCollection;
                }
                set
                {
                    if (expandedMasterRowsCollection == value) return;
                    SubscribeCollectionChangedEvent(false);
                    expandedMasterRowsCollection = value;
                    SubscribeCollectionChangedEvent(true);
                }
            }

            private void SubscribeCollectionChangedEvent(bool subscribe)
            {
                if (expandedMasterRowsCollection == null) return;
                if (subscribe)
                    expandedMasterRowsCollection.CollectionChanged += ExpandedMasterRowsCollectionChanged;
                else
                    expandedMasterRowsCollection.CollectionChanged -= ExpandedMasterRowsCollectionChanged;
            }

            private void SubscribeViewEvents(bool subscribe)
            {
                if (viewIsNull) return;
                if (subscribe)
                {
                    view.Disposed += view_Disposed;
                    view.MasterRowExpanded += view_MasterRowExpanded;
                    view.MasterRowCollapsed += view_MasterRowCollapsed;
                    view.DataSourceChanged += view_DataSourceChanged;
                }
                else
                {
                    view.Disposed -= view_Disposed;
                    view.MasterRowExpanded -= view_MasterRowExpanded;
                    view.MasterRowCollapsed -= view_MasterRowCollapsed;
                    view.DataSourceChanged -= view_DataSourceChanged;
                    view = null;
                }
            }

            private void view_DataSourceChanged(object sender, EventArgs e)
            {
                lockExpandedMasterRowsCollectionChanged = true;
                expandedMasterRowsCollection?.Clear();
                lockExpandedMasterRowsCollectionChanged = false;
            }

            private void view_Disposed(object sender, EventArgs e)
            {
                SubscribeViewEvents(false);
            }

            private void view_MasterRowCollapsed(object sender, CustomMasterRowEventArgs e)
            {
                MasterRowExpanded(false, GetCategoryID(e.RowHandle));
            }

            private void view_MasterRowExpanded(object sender, CustomMasterRowEventArgs e)
            {
                MasterRowExpanded(true, GetCategoryID(e.RowHandle));
            }

            public long GetCategoryID(int rowHandle)
            {
                return (view.GetRow(rowHandle) as Models.Email).ID;
            }

            private void MasterRowExpanded(bool expanded, long categoryID)
            {
                if (lockMasterRowExpanded) return;
                lockExpandedMasterRowsCollectionChanged = true;
                if (expanded)
                    expandedMasterRowsCollection.Add(categoryID);
                else
                    expandedMasterRowsCollection.Remove(categoryID);
                lockExpandedMasterRowsCollectionChanged = false;
            }

            private void ExpandedMasterRowsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
            {
                if (lockExpandedMasterRowsCollectionChanged || viewIsNull) return;
                lockMasterRowExpanded = true;
                switch (e.Action)
                {
                    case NotifyCollectionChangedAction.Add:
                        foreach (int categoryID in e.NewItems)
                            ExpandMasterRow(categoryID);
                        break;
                    case NotifyCollectionChangedAction.Remove:
                        foreach (int categoryID in e.OldItems)
                            CollapseMasterRow(categoryID);
                        break;
                    case NotifyCollectionChangedAction.Reset:
                        CollapseAllMasterRows();
                        break;
                }
                lockMasterRowExpanded = false;
            }

            private void ExpandMasterRow(int categoryID)
            {
                view.ExpandMasterRow(GetRowHandle(categoryID));
            }

            private void CollapseMasterRow(int categoryID)
            {
                view.CollapseMasterRow(GetRowHandle(categoryID));
            }

            private void CollapseAllMasterRows()
            {
                view.CollapseAllDetails();
            }

            private int GetRowHandle(int categoryID)
            {
                return view.LocateByValue("ID", categoryID, null);
            }

            public void Dispose()
            {
                SubscribeViewEvents(false);
                SubscribeCollectionChangedEvent(false);
            }
        }
    }
}
