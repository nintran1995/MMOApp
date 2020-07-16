using CommandModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZChangerMMO.Model
{
    public class ProfileXML
    {
        public ProfileXML()
        {
            Proxies = new List<Proxy>();
            ByPassProxySites = new List<string>();
        }

        public string Description { get; set; }

        public string Email { get; set; }

        public string FolderPath { get; set; }

        #region Fonts
        public Fonts Fonts { get; set; }
        #endregion Fonts

        public long Id { get; set; }

        public string Name { get; set; }

        public string OperatingSystem { get; set; }

        #region Devive
        public CPU CPU { get; set; }

        public Battery Battery { get; set; }

        public bool EnableAudioApi { get; set; }

        public bool EnablePlugins { get; set; }

        public bool EnableMediaPlugins { get; set; }
        #endregion Devive

        #region Content
        public bool RandomTimersEnabled { get; set; }

        public string UserAgent { get; set; }

        public Screen Screen { get; set; }

        public int HistoryLength { get; set; }
        #endregion Content

        #region FingerPrint
        public WebGL WebGL { get; set; }

        public bool FakeClientRects { get; set; }

        public Canvas Canvas { get; set; }
        #endregion FingerPrint

        #region GEO
        public bool EnableNetwork { get; set; }

        public string Language { get; set; }

        public bool GeoIpEnabled { get; set; }
        #endregion GEO

        internal string _Proxies { get; set; }

        public List<Proxy> Proxies{ get; set; }

        internal string _ByPassProxySites { get; set; }

        public List<string> ByPassProxySites{ get; set; }
    }
}
