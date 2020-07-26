
using ZChangerMMO.Models;

namespace ZChangerMMO.Infrastructure
{
    public interface IDeviceRepository : IRepository<Device>
    {
        Device GetByEmailID(long id);
    }
}
