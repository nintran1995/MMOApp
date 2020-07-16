using System;
using System.Linq;

namespace ZChangerMMO.Events
{
    public class ApplyNewLicenceEventArgs : EventArgs
    {
        public ApplyNewLicenceEventArgs(string licence, DateTime expirationDate)
        {
            Licence = licence;
            ExpirationDate = expirationDate;
        }

        public string Licence { get; private set; }
        public DateTime ExpirationDate { get; private set; }
    }
}
