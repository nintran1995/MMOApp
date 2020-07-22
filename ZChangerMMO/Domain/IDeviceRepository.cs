
using ZChangerMMO.Models;

namespace ZChangerMMO.Domain
{
    public interface IDeviceRepository : IRepository<Device>
    {
        Device GetByEmailID(long id);
    }
}
