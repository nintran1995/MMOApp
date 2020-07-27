using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Data;
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
    public class EmailListViewModel : CollectionViewModel<Models.Email>
    {
        public EmailListViewModel()
        {
            _runner = new Runner();
            _runner.DisconnectEvent += Runner_DisconnectEvent;
        }

        private readonly Runner _runner;
        private void Runner_DisconnectEvent(object sender, long e)
        {
            DisconectID = e;
            RaisePropertyChanged("DisconectID");
        }

        public long DisconectID { get; set; }

        protected override void OnNavigatedTo()
        {
            // do something if needed
        }
        protected override void OnNavigatedFrom()
        {
            // do something if needed
        }

        #region Email Events

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

        #endregion Email Events

        #region Device Events

        public Models.Device SelectedDevice
        {
            get { return GetProperty(() => SelectedDevice); }
            set { SetProperty(() => SelectedDevice, value); }
        }

        public List<Device> GetDevices()
        {
            var result = _uoW.Devices.GetAll().ToList();
            return result;
        }

        [Command]
        public void CreateDevice()
        {
            Navigation.Navigate(nameof(Views.Device.DeviceView), SelectedItem.ID, this);
        }

        [Command]
        public void UpdateDevice()
        {
            Navigation.Navigate(nameof(Views.Device.DeviceView), SelectedDevice, this);
        }

        [AsyncCommand]
        public async Task DeleteDevice()
        {
            SetLoading(true);
            try
            {
                await Task.Run(() =>
                {
                    var emailDelete = _uoW.Emails.Get(SelectedDevice.ID);
                    if (emailDelete != null)
                    {
                        _uoW.Devices.Delete(SelectedDevice);
                        _uoW.Commit();
                    }
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

        [Command]
        public void Run(DataTable deviceDataTable)
        {
            try
            {
                foreach (DataRow dr in deviceDataTable.Rows)
                {
                    if ((long)dr["ID"] == SelectedDevice.ID)
                    {
                        _runner.Play(SelectedDevice);
                        dr["Running"] = true;
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Run Browser", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        [Command]
        public void Stop(DataTable deviceDataTable)
        {
            long disconectID = SelectedDevice.ID;
            try
            {
                foreach (DataRow dr in deviceDataTable.Rows)
                {
                    if ((long)dr["ID"] == disconectID)
                    {
                        _runner.Stop(disconectID);
                        dr["Running"] = false;
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Stop Browser", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void StopException(DataTable deviceDataTable)
        {
            try
            {
                foreach (DataRow dr in deviceDataTable.Rows)
                {
                    if ((long)dr["ID"] == DisconectID)
                    {
                        _runner.Stop(DisconectID);
                        dr["Running"] = false;
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Stop Browser", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion Device Events
    }
}
