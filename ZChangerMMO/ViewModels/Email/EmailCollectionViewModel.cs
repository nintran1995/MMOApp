using DevExpress.Mvvm.DataModel;
using DevExpress.Mvvm.POCO;
using ZChangerMMO.Common;
using ZChangerMMO.Models;
using ZChangerMMO.MyDbContextDataModel;

namespace ZChangerMMO.ViewModels
{

    /// <summary>
    /// Represents the Emails collection view model.
    /// </summary>
    public partial class EmailCollectionViewModel : CollectionViewModel<Email, long, IMyDbContextUnitOfWork> {

        /// <summary>
        /// Creates a new instance of EmailCollectionViewModel as a POCO view model.
        /// </summary>
        /// <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
        public static EmailCollectionViewModel Create(IUnitOfWorkFactory<IMyDbContextUnitOfWork> unitOfWorkFactory = null) {
            return ViewModelSource.Create(() => new EmailCollectionViewModel(unitOfWorkFactory));
        }

        /// <summary>
        /// Initializes a new instance of the EmailCollectionViewModel class.
        /// This constructor is declared protected to avoid undesired instantiation of the EmailCollectionViewModel type without the POCO proxy factory.
        /// </summary>
        /// <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
        protected EmailCollectionViewModel(IUnitOfWorkFactory<IMyDbContextUnitOfWork> unitOfWorkFactory = null)
            : base(unitOfWorkFactory ?? UnitOfWorkSource.GetUnitOfWorkFactory(), x => x.Emails) {
        }        
    }
}