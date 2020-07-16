using NativeMessaging;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;

namespace ZChangerMMO.HostNativeMessing
{
    public class MyHost : Host
    {
        const bool SendConfirmationReceipt = true;

        public MyHost() : base(SendConfirmationReceipt) { }

        protected override void ProcessReceivedMessage(JObject data) => SendMessage(data);

        public override string Hostname => Constants.HostNativeMessaging;
    }
}
