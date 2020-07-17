using DevExpress.Mvvm.DataModel;
using DevExpress.Mvvm.POCO;
using DevExpress.Mvvm.ViewModel;
using ZChangerMMO.Common;
using ZChangerMMO.Models;
using ZChangerMMO.MyDbContextDataModel;

namespace ZChangerMMO.ViewModels
{
    /// <summary>
    /// Represents the single Email object view model.
    /// </summary>
    public partial class EmailViewModel : SingleObjectViewModel<Email, long, IMyDbContextUnitOfWork>
    {

        /// <summary>
        /// Creates a new instance of EmailViewModel as a POCO view model.
        /// </summary>
        /// <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
        public static EmailViewModel Create(IUnitOfWorkFactory<IMyDbContextUnitOfWork> unitOfWorkFactory = null)
        {
            return ViewModelSource.Create(() => new EmailViewModel(unitOfWorkFactory));
        }

        /// <summary>
        /// Initializes a new instance of the EmailViewModel class.
        /// This constructor is declared protected to avoid undesired instantiation of the EmailViewModel type without the POCO proxy factory.
        /// </summary>
        /// <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
        protected EmailViewModel(IUnitOfWorkFactory<IMyDbContextUnitOfWork> unitOfWorkFactory = null)
            : base(unitOfWorkFactory ?? UnitOfWorkSource.GetUnitOfWorkFactory(), x => x.Emails, x => x.Name)
        {
        }


        /// <summary>
        /// The view model that contains a look-up collection of Devices for the corresponding navigation property in the view.
        /// </summary>
        public IEntitiesViewModel<Device> LookUpDevices
        {
            get
            {
                return GetLookUpEntitiesViewModel(
                    propertyExpression: (EmailViewModel x) => x.LookUpDevices,
                    getRepositoryFunc: x => x.Devices);
            }
        }


        /// <summary>
        /// The view model for the EmailDevices detail collection.
        /// </summary>
        public CollectionViewModelBase<Device, Device, long, IMyDbContextUnitOfWork> EmailDevicesDetails
        {
            get
            {
                return GetDetailsCollectionViewModel(
                    propertyExpression: (EmailViewModel x) => x.EmailDevicesDetails,
                    getRepositoryFunc: x => x.Devices,
                    foreignKeyExpression: x => x.EmailID,
                    navigationExpression: x => x.Email);
            }
        }

        public CollectionViewModel<Device, long, IMyDbContextUnitOfWork> EmailDeviceDetails
        {
            get { return GetDetailsCollectionViewModel((EmailViewModel x) => x.EmailDeviceDetails, x => x.Devices, x => x.EmailID, (x, key) => x.EmailID = key); }
        }
    }
}
