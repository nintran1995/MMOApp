using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZChangerMMO.Views.Events
{
    public class UpdateByPassSiteListEventArgs : EventArgs
    {
        public IList<string> ByPassSiteList { get; set; }
    }
}
