using CommandModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZChangerMMO.Views.Events
{
    public class AddProxy_AddProxyClickEventArgs : EventArgs
    {
        public AddProxy_AddProxyClickEventArgs()
        {

        }

        public Proxy Proxy { get; set; }
    }
}
