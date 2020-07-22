using ZChangerMMO.Domain;

namespace ZChangerMMO.Infrastructure.Repositories
{
    public class UoW : IUoW
    {
        private ZChangerContext _context;
        private IEmailRepository _emails;
        private IDeviceRepository _devices;

        public IEmailRepository Emails => _emails ?? (_emails = new EmailRepository(_context));
        public IDeviceRepository Devices => _devices ?? (_devices = new DeviceRepository(_context));

        public UoW(ZChangerContext context)
        {
            _context = context;
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}