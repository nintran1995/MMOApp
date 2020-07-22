using Autofac;
using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Documents;
using ZChangerMMO.Common;
using ZChangerMMO.Domain;
using ZChangerMMO.Models;
using ZChangerMMO.UI;

namespace ZChangerMMO.ViewModels
{
    /// <summary>
    /// Represents the single Email object view model.
    /// </summary>
    public class EmailListViewModel : CollectionViewModel<Models.Email>
    {
        private IEmailRepository _emailRepository;
        private IDeviceRepository _deviceRepository;

        public EmailListViewModel()
        {
            _emailRepository = Bootstrapper.Container.Resolve<IEmailRepository>();
            _deviceRepository = Bootstrapper.Container.Resolve<IDeviceRepository>();
            //_ = OnLoaded();
        }

        [ServiceProperty(SearchMode = ServiceSearchMode.PreferParents)]
        protected virtual IDocumentManagerService DocumentManagerService { get { return null; } }

        [AsyncCommand]
        public async Task OnLoaded()
        {
            SetLoading(true);
            try
            {
                var customers = await Task.Run(() =>
                {
                    return _emailRepository.GetAll();
                });
                SetItems(customers);
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

        [Command]
        public void CreateDevice()
        {
            var document = DocumentManagerService.CreateDocument("DeviceView", SelectedItem.ID, this);
            document.Show();
        }

        [Command]
        public void Create()
        {
            var document = DocumentManagerService.CreateDocument("EmailView", null, this);
            document.Show();
        }

        [Command]
        public void Update()
        {
            var document = DocumentManagerService.CreateDocument("EmailView", SelectedItem.ID, this);
            document.Show();
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
            var result = _emailRepository.GetAll().ToList();
            SetItems(result);
            return result;
        }

        public List<Device> GetDevices()
        {
            var result = _deviceRepository.GetAll().ToList();
            return result;
        }
    }
}
