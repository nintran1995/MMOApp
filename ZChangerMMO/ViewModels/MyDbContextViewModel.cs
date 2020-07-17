using DevExpress.Mvvm.POCO;
using DevExpress.Mvvm.ViewModel;
using System;
using ZChangerMMO.MyDbContextDataModel;

namespace ZChangerMMO.ViewModels
{
    /// <summary>
    /// Represents the root POCO view model for the MyDbContext data model.
    /// </summary>
    public partial class MyDbContextViewModel : DocumentsViewModel<MyDbContextModuleDescription, IMyDbContextUnitOfWork>
    {

        const string TablesGroup = "Tables";

        const string ViewsGroup = "Views";

        /// <summary>
        /// Creates a new instance of MyDbContextViewModel as a POCO view model.
        /// </summary>
        public static MyDbContextViewModel Create()
        {
            return ViewModelSource.Create(() => new MyDbContextViewModel());
        }


        /// <summary>
        /// Initializes a new instance of the MyDbContextViewModel class.
        /// This constructor is declared protected to avoid undesired instantiation of the MyDbContextViewModel type without the POCO proxy factory.
        /// </summary>
        protected MyDbContextViewModel()
            : base(UnitOfWorkSource.GetUnitOfWorkFactory())
        {
        }

        protected override MyDbContextModuleDescription[] CreateModules()
        {
            return new MyDbContextModuleDescription[] {
                new MyDbContextModuleDescription( "Emails", "EmailCollectionView", TablesGroup, GetPeekCollectionViewModelFactory(x => x.Emails)),
                new MyDbContextModuleDescription( "Devices", "DeviceCollectionView", TablesGroup, GetPeekCollectionViewModelFactory(x => x.Devices)),
            };
        }
    }

    public partial class MyDbContextModuleDescription : ModuleDescription<MyDbContextModuleDescription>
    {
        public MyDbContextModuleDescription(string title, string documentType, string group, Func<MyDbContextModuleDescription, object> peekCollectionViewModelFactory = null)
            : base(title, documentType, group, peekCollectionViewModelFactory)
        {
        }
    }
}