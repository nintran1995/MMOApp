using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZChangerMMO.Common;
using ZChangerMMO.Models;

namespace ZChangerMMO.ViewModels
{
    /// <summary>
    /// Represents the single Email object view model.
    /// </summary>
    public class EmailListViewModel : CollectionViewModel<Models.Email>
    {     
        public EmailListViewModel()
        { }

        protected override void OnNavigatedTo()
        {
            // do something if needed
        }
        protected override void OnNavigatedFrom()
        {
            // do something if needed
        }

        [Command]
        public void CreateDevice()
        {
            Navigation.Navigate(nameof(Views.Device.DeviceView), SelectedItem.ID, this);
        }

        [Command]
        public void Create()
        {
            Navigation.Navigate(nameof(Views.Email.EmailView), null, this);
        }

        [Command]
        public void Update()
        {
            Navigation.Navigate(nameof(Views.Email.EmailView), SelectedItem.ID, this);
        }

        [AsyncCommand]
        public async Task Delete()
        {
            SetLoading(true);
            try
            {
                await Task.Run(() =>
                {
                    var emailDelete = _uoW.Emails.Get(SelectedItem.ID);
                    if (emailDelete != null)
                    {
                        _uoW.Emails.Delete(SelectedItem);
                        _uoW.Commit();
                    }
                });
                ShowNotification("Deleted!");
                //Navigation.GoBack();
            }
            catch (Exception e)
            {
                MessageBoxService.ShowMessage(e.Message, "Error", MessageButton.OK);
            }
            finally
            {
                SetLoading(false);
            }


            //_uoW.Emails.Update(Item);
            //_uoW.Commit();
            //var deleteItem = _emailRepository.Get(SelectedItem.ID);
            //if (deleteItem != null)
            //    _emailRepository.Delete(deleteItem);
        }

        public List<Email> GetEmails()
        {
            var result = _uoW.Emails.GetAll().ToList();
            SetItems(result);
            return result;
        }
    }
}
