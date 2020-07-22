using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.POCO;
using System;
using System.Threading.Tasks;
using ZChangerMMO.Common;

namespace ZChangerMMO.ViewModels
{
    public class EmailViewModel : EntityViewModel<Models.Email>
    {
        private bool isAdd;
        protected EmailViewModel()
        { }

        protected override async void OnParameterChanged(object parameter)
        {
            base.OnParameterChanged(parameter);
            if (parameter is long id)
            {
                isAdd = false;
                SetLoading(true);
                var item = await Task.Run(() => _uoW.Emails.Get(id));
                SetItem(item);
                SetLoading(false);
            }
            else
            {
                isAdd = true;
                SetItem(new Models.Email());
            }
        }

        [AsyncCommand]
        public async Task Save()
        {
            if (isAdd)
            {
                await CreateNew();
            }
            else
            {
                await Update();
            }
        }

        [AsyncCommand]
        public async Task CreateNew()
        {
            SetLoading(true);
            try
            {
                await Task.Run(() =>
                {
                    _uoW.Emails.Add(Item);
                    _uoW.Commit();
                });
                ShowNotification("Created!");
            }
            catch (Exception e)
            {
                MessageBoxService.ShowMessage(e.Message, "Error", MessageButton.OK);
            }
            finally
            {
                SetLoading(false);
            }
        }

        public bool CanCreate()
        {
            return Item?.ID == 0;
        }

        [AsyncCommand]
        public async Task Update()
        {
            SetLoading(true);
            try
            {
                await Task.Run(() =>
                {
                    _uoW.Emails.Update(Item);
                    _uoW.Commit();
                });
                ShowNotification("Updated!");
            }
            catch (Exception e)
            {
                MessageBoxService.ShowMessage(e.Message, "Error", MessageButton.OK);
            }
            finally
            {
                SetLoading(false);
            }
        }

        [AsyncCommand]
        public async Task Delete()
        {
            SetLoading(true);
            try
            {
                await Task.Run(() =>
                {
                    _uoW.Emails.Update(Item);
                    _uoW.Commit();
                });
                ShowNotification("Deleted!");
                Navigation.GoBack();
            }
            catch (Exception e)
            {
                MessageBoxService.ShowMessage(e.Message, "Error", MessageButton.OK);
            }
            finally
            {
                SetLoading(false);
            }
        }

        public bool CanDelete()
        {
            return Item?.ID > 0;
        }

        public bool UpdateCommands()
        {
            this.RaiseCanExecuteChanged(x => x.CreateNew());
            return Item?.ID > 0;
        }
    }
}
