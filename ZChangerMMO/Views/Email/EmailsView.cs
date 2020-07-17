using DevExpress.Utils.MVVM.UI;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using ZChangerMMO.ViewModels;

namespace ZChangerMMO.Views.Email
{
    [ViewType("EmailCollectionView")]
    public partial class EmailsView : DevExpress.XtraEditors.XtraUserControl
    {
        public EmailsView()
        {
            InitializeComponent();
            if (!mvvmContext1.IsDesignMode)
                InitializeBindings();
            gridView1.OptionsBehavior.Editable = false;
            gridView1.OptionsSelection.MultiSelect = true;
            gridView1.OptionsSelection.MultiSelectMode = GridMultiSelectMode.RowSelect;
        }

        void InitializeBindings()
        {
            var fluentAPI = mvvmContext1.OfType<EmailCollectionViewModel>();
            fluentAPI.SetBinding(gridView1, gView => gView.LoadingPanelVisible, x => x.IsLoading);
            fluentAPI.SetBinding(gridControl1, gControl => gControl.DataSource, x => x.Entities);

            //Handle select row event
            fluentAPI.WithEvent<ColumnView, FocusedRowObjectChangedEventArgs>(gridView1, "FocusedRowObjectChanged")
              .SetBinding(x => x.SelectedEntity,
                  args => args.Row as Models.Email,
                  (gView, entity) => gView.FocusedRowHandle = gView.FindRow(entity));

            fluentAPI.WithEvent<RowCellClickEventArgs>(gridView1, "RowCellClick")
               .EventToCommand(
                   x => x.Edit(null), x => x.SelectedEntity,
                   args => (args.Clicks == 2) && (args.Button == System.Windows.Forms.MouseButtons.Left));

            fluentAPI.WithEvent<SelectionChangedEventArgs>(gridView1, "SelectionChanged")
                .SetBinding(x => x.Selection, e => GetSelectedEmails());
        }

        IEnumerable<Models.Email> GetSelectedEmails()
        {
            return gridView1.GetSelectedRows().Select(r => gridView1.GetRow(r) as Models.Email);
        }
    }
}
