﻿using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.POCO;
using System;
using System.Threading.Tasks;
using ZChangerMMO.Common;
using ZChangerMMO.Models;

namespace ZChangerMMO.ViewModels
{
    public class DeviceViewModel : EntityViewModel<Models.Device>
    {
        protected DeviceViewModel()
        { }

        protected override async void OnParameterChanged(object parameter)
        {
            base.OnParameterChanged(parameter);
            if (parameter is long id)
            {
                var item = new Device()
                {
                    EmailID = id
                };
                SetItem(item);
            }
            else if (parameter is Device device)
            {
                SetItem(device);
            }
        }

        [AsyncCommand]
        public async Task Save()
        {
            SetLoading(true);
            try
            {
                await Task.Run(() =>
                {
                    _uoW.Devices.Update(Item);
                    _uoW.Commit();
                });
                ShowNotification("Saved!");
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

        public void UpdateCommands()
        {
            this.RaiseCanExecuteChanged(x => x.CreateNew());
        }

        [AsyncCommand]
        public async Task Delete()
        {
            SetLoading(true);
            try
            {
                await Task.Run(() =>
                {
                    _uoW.Devices.Update(Item);
                    _uoW.Commit();
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
        }

        public bool CanDelete()
        {
            return Item?.ID > 0;
        }

        [AsyncCommand]
        public async Task CreateNew()
        {
            SetLoading(true);
            this.RaisePropertiesChanged();
            try
            {
                await Task.Run(() =>
                {
                    _uoW.Devices.Add(Item);
                    _uoW.Commit();
                });
                ShowNotification("Created!");
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

        public bool CanCreateNew()
        {
            return !string.IsNullOrEmpty(Item?.Name);
        }

        public bool CanCreate()
        {
            return Item?.ID == 0;
        }

        [Command]
        public void Cancel()
        {
            Navigation.GoBack();
        }
    }
}
