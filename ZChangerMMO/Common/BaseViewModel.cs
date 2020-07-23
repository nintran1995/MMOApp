using DevExpress.Mvvm;

namespace ZChangerMMO.Common
{
    public class BaseViewModel : ViewModelBase
    {
        public bool IsLoading
        {
            get { return GetProperty(() => IsLoading); }
            private set { SetProperty(() => IsLoading, value); }
        }
       
        protected INavigationService Navigation => GetService<INavigationService>();
        protected IMessageBoxService MessageBoxService => GetService<IMessageBoxService>();
        protected INotificationService NotificationService => GetService<INotificationService>();

        protected void SetLoading(bool value)
        {
            IsLoading = value;
        }

        public void ShowNotification(string text)
        {
            INotification notification = NotificationService.CreatePredefinedNotification(text, "", "");
            notification.ShowAsync();
        }
    }
}