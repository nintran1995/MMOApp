using DevExpress.Mvvm.DataModel;
using DevExpress.Mvvm.DataModel.EF6;
using System;
using ZChangerMMO.Models;

namespace ZChangerMMO.MyDbContextDataModel
{

    /// <summary>
    /// A MyDbContextUnitOfWork instance that represents the run-time implementation of the IMyDbContextUnitOfWork interface.
    /// </summary>
    public class MyDbContextUnitOfWork : DbUnitOfWork<MyDbContext>, IMyDbContextUnitOfWork {

        public MyDbContextUnitOfWork(Func<MyDbContext> contextFactory)
            : base(contextFactory) {
        }

        IRepository<Device, long> IMyDbContextUnitOfWork.Devices {
            get { return GetRepository(x => x.Set<Device>(), (Device x) => x.ID); }
        }

        IRepository<Email, long> IMyDbContextUnitOfWork.Emails {
            get { return GetRepository(x => x.Set<Email>(), (Email x) => x.ID); }
        }
    }
}
