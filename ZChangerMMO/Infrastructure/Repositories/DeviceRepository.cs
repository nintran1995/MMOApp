using DevExpress.Data.ODataLinq.Helpers;
using System.Linq;
using ZChangerMMO.Models;

namespace ZChangerMMO.Infrastructure
{
    public class DeviceRepository : GenericRepository<Device>, IDeviceRepository
    {
        public DeviceRepository(ZChangerContext context) : base(context)
        { }

        public Device GetByEmailID(long id)
        {
            return _context.Devices.ToList().Where(x => x.EmailID == id).FirstOrDefault();
        }
    }
}
