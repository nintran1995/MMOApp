using CommandModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZChangerMMO.Events
{
    public class SaveProxyEventArgs : EventArgs
    {
        public IList<Proxy>  ProxyList { get; set; }
        public IList<string>  ByPassProxySiteList { get; set; }
    }
}
