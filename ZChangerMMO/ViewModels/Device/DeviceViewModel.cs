using System;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;
using DevExpress.Mvvm.DataModel;
using DevExpress.Mvvm.ViewModel;
using ZChangerMMO.MyDbContextDataModel;
using ZChangerMMO.Common;
using ZChangerMMO.Models;

namespace ZChangerMMO.ViewModels {

    /// <summary>
    /// Represents the single Device object view model.
    /// </summary>
    public partial class DeviceViewModel : SingleObjectViewModel<Device, long, IMyDbContextUnitOfWork> {

        /// <summary>
        /// Creates a new instance of DeviceViewModel as a POCO view model.
        /// </summary>
        /// <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
        public static DeviceViewModel Create(IUnitOfWorkFactory<IMyDbContextUnitOfWork> unitOfWorkFactory = null) {
            return ViewModelSource.Create(() => new DeviceViewModel(unitOfWorkFactory));
        }

        /// <summary>
        /// Initializes a new instance of the DeviceViewModel class.
        /// This constructor is declared protected to avoid undesired instantiation of the DeviceViewModel type without the POCO proxy factory.
        /// </summary>
        /// <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
        protected DeviceViewModel(IUnitOfWorkFactory<IMyDbContextUnitOfWork> unitOfWorkFactory = null)
            : base(unitOfWorkFactory ?? UnitOfWorkSource.GetUnitOfWorkFactory(), x => x.Devices, x => x.Name) {
                }


        /// <summary>
        /// The view model that contains a look-up collection of Emails for the corresponding navigation property in the view.
        /// </summary>
        public IEntitiesViewModel<Email> LookUpEmails {
            get {
                return GetLookUpEntitiesViewModel(
                    propertyExpression: (DeviceViewModel x) => x.LookUpEmails,
                    getRepositoryFunc: x => x.Emails);
            }
        }

    }
}
