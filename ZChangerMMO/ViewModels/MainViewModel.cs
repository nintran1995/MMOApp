namespace ZChangerMMO.ViewModels
{
    using DevExpress.Mvvm;
    using DevExpress.Mvvm.POCO;

    public class MainViewModel
    {
        public MainViewModel() { }

        protected INavigationService NavigationService
        {
            get { return this.GetService<INavigationService>(); }
        }

        public void OnShown()
        {
            NavigationService.Navigate(nameof(Views.Email.EmailListView), null, this, false);
        }
    }
}