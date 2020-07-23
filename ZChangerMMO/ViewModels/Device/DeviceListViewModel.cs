using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZChangerMMO.Business;
using ZChangerMMO.Common;
using ZChangerMMO.Models;

namespace ZChangerMMO.ViewModels
{
    /// <summary>
    /// Represents the single Email object view model.
    /// </summary>
    public class DeviceListViewModel : CollectionViewModel<Models.Device>
    {
        public DeviceListViewModel()
        { }

        [Command]
        public void Run()
        {
            try
            {
                var runner = new Runner(SelectedItem);
                runner.Play();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Run Browser", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
                        _uoW.Devices.Delete(SelectedItem);
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
        }

        public List<Device> GetDevices()
        {
            var result = _uoW.Devices.GetAll().ToList();
            SetItems(result);
            return result;
        }
    }
}
