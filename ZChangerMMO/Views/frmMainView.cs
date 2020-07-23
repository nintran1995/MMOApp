using DevExpress.Utils.MVVM.Services;
using ZChangerMMO.UI;
using ZChangerMMO.ViewModels;

namespace ZChangerMMO.Views
{
    public partial class frmMainView : DevExpress.XtraEditors.XtraForm
    {
        public frmMainView()
        {
            InitializeComponent();
            if (!DesignMode)
            {
                Bootstrapper.BuildContainer();
                InitializeNavigation();
            }
        }

        void InitializeNavigation()
        {
            // creating the NavigationFrame as INavigationService
            var navigationService = NavigationService.Create(navigationFrame);
            // registering the service instance
            mvvmContext1.RegisterService(navigationService);
            // Initialize the Fluent API
            var fluent = mvvmContext1.OfType<MainViewModel>();
            // Bind the OnShown command to the Shown event
            fluent.WithEvent(this, "Shown")
                .EventToCommand(x => x.OnShown());
        }
    }
}