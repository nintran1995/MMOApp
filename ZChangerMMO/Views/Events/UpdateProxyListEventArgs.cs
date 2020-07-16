using CommandModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZChangerMMO.Views.Events
{
    public class UpdateProxyListEventArgs : EventArgs
    {
        public IList<Proxy> ProxyList { get; set; }
    }
}
