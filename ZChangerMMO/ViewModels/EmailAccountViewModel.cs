using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.POCO;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using ZChangerMMO.Models;

namespace ZChangerMMO.ViewModels
{
    public class EmailAccountViewModel
    {
        MyDbContext _context = new MyDbContext();
        public EmailAccountViewModel()
        {
            ExpandedEntitiesCollection = new ObservableCollection<object>();
        }

        private BindingList<Email> emails;
        public ObservableCollection<object> ExpandedEntitiesCollection { get; set; }
        public int ExpandedEntitiesCount { get { return ExpandedEntitiesCollection.Count; } }

        private static void InitData(BindingList<Email> categoryBindingList)
        {
            categoryBindingList.Add(new Email()
            {
                ID = 1,
                Name = "Beverages",
                EmailAccount = "Soft drinks, coffees, teas, beers, and ales",
                Devices = new BindingList<Device> {
                    new Device() { ID = 1, Name = "Guaraná Fantástica", EmailID = 1 },
                    new Device() { ID = 2, Name = "Guaraná Fantástica", EmailID = 1 }
                }
            });
        }
        public BindingList<Email> GetData()
        {
            _context.Emails.Load();
            return _context.Emails.Local.ToBindingList();
        }

        public BindingList<Email> Emails
        {
            get
            {
                if (emails == null)
                    emails = GetData();
                return emails;
            }
            set
            {
                emails = value;
            }
        }

        public void Update(object entityID)
        {
            UpdateDataBindings();
            UpdateCommands(entityID);
        }

        public void UpdateDataBindings()
        {
            this.RaisePropertyChanged(x => x.ExpandedEntitiesCount);
        }

        public void UpdateCommands(object categoryID)
        {
            this.RaiseCanExecuteChanged(x => x.CollapseEntity(categoryID));
            this.RaiseCanExecuteChanged(x => x.DeleteEntity(categoryID));
            this.RaiseCanExecuteChanged(x => x.ExpandEntity(categoryID));
        }

        public void DeleteEntity(object categoryID)
        {
            CollapseEntity(categoryID);
            Emails.Remove(FindEntity(categoryID));
            Update(categoryID);
        }

        public void ExpandEntity(object categoryID)
        {
            ExpandedEntitiesCollection.Add(categoryID);
            Update(categoryID);
        }

        public void CollapseEntity(object categoryID)
        {
            ExpandedEntitiesCollection.Remove(categoryID);
            Update(categoryID);
        }

        public bool CanDeleteEntity(object categoryID)
        {
            return EntityExists(categoryID);
        }

        public bool CanCollapseEntity(object categoryID)
        {
            return EntityExists(categoryID) && ExpandedEntitiesCollection.Contains(categoryID);
        }

        public bool CanExpandEntity(object categoryID)
        {
            return EntityExists(categoryID) && !ExpandedEntitiesCollection.Contains(categoryID);
        }

        private bool EntityExists(object categoryID)
        {
            return FindEntity(categoryID) != null;
        }

        private Email FindEntity(object categoryID)
        {
            return Emails.FirstOrDefault(entity => entity.ID == Convert.ToInt32(categoryID));
        }
    }
}