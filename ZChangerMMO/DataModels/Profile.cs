using CommandModel;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace ZChangerMMO.DataModels
{
    public class Profile
    {
        public Profile()
        {
            BackupItems = new List<BackupItem>();
            ActionLogs = new List<ActionLog>();
            Proxies = new List<Proxy>();
            ByPassProxySites = new List<string>();
            CPU = new CPU();
            WebGL = new WebGL();
            Battery = new Battery();
            Canvas = new Canvas();
            Screen = new Screen();
            OperatingSystem = "WindowsPC";
        }

        [XmlIgnore]
        public ICollection<ActionLog> ActionLogs { get; set; }

        [XmlIgnore]
        public ICollection<BackupItem> BackupItems { get; set; }

        public string Description { get; set; }

        public string Email { get; set; }

        public string FolderPath { get; set; }

        #region Fonts
        public Fonts Fonts { get; set; }
        #endregion Fonts

        [Key]
        [Display(AutoGenerateField = false)]
        public long Id { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 4)]
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

    //    public int DeviceIdx { get; set; }

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

        public bool ProxyEnabled { get; set; }

        internal string _Proxies { get; set; }

        [NotMapped]
        public List<Proxy> Proxies
        {
            get { return _Proxies == null ? null : JsonConvert.DeserializeObject<List<Proxy>>(_Proxies); }
            set { _Proxies = JsonConvert.SerializeObject(value); }
        }

        internal string _ByPassProxySites { get; set; }

        [NotMapped]
        public List<string> ByPassProxySites
        {
            get { return _ByPassProxySites == null ? null : JsonConvert.DeserializeObject<List<string>>(_ByPassProxySites); }
            set { _ByPassProxySites = JsonConvert.SerializeObject(value); }
        }
    }
}
