using DevExpress.Data.ODataLinq.Helpers;
using System.Data.Entity;
using System.Linq;
using ZChangerMMO.Domain;
using ZChangerMMO.Models;

namespace ZChangerMMO.Infrastructure.Repositories
{
    public class DeviceRepository : GenericRepository<Device>, IDeviceRepository
    {
        protected readonly ZChangerContext _context;
        public DeviceRepository(ZChangerContext context) : base(context)
        {
            _context = context;
        }

        public Device GetByEmailID(long id)
        {
            return _context.Devices.ToList().Where(x => x.EmailID == id).FirstOrDefault();
        }
    }
}
