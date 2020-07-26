using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using ZChangerMMO.Models;

namespace ZChangerMMO.Infrastructure
{
    public class EmailRepository : GenericRepository<Email>, IEmailRepository
    {
        public EmailRepository(ZChangerContext context) : base(context)
        { }

        public async Task<IEnumerable<Device>> GetDevices(long emailID)
        {
            var devices = await _context.Devices
                .Where(i => i.EmailID == emailID)
                .ToListAsync();
            return devices;
        }
    }
}
