using DevExpress.Mvvm.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using ZChangerMMO.Models;

namespace ZChangerMMO.MyDbContextDataModel {

    /// <summary>
    /// IMyDbContextUnitOfWork extends the IUnitOfWork interface with repositories representing specific entities.
    /// </summary>
    public interface IMyDbContextUnitOfWork : IUnitOfWork {
        
        /// <summary>
        /// The Device entities repository.
        /// </summary>
		IRepository<Device, long> Devices { get; }
        
        /// <summary>
        /// The Email entities repository.
        /// </summary>
		IRepository<Email, long> Emails { get; }
    }
}
