using CommandModel;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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

        public Profile(Models.Device device)
        {
            switch (device.Type)
            {
                case Models.DeviceType.Macintosh:
                    {
                        MacintoshProfile(device);
                        break;
                    }
                case Models.DeviceType.iPhone:
                    {
                        IPhoneProfile(device);
                        break;
                    }
                case Models.DeviceType.iPad:
                    {
                        IPadProfile(device);
                        break;
                    }
                case Models.DeviceType.Samsung:
                    {
                        SumSungProfile(device);
                        break;
                    }
                case Models.DeviceType.Xiaomi:
                    {
                        XiaomiProfile(device);
                        break;
                    }
                case Models.DeviceType.Oppo:
                    {
                        OppeProfile(device);
                        break;
                    }
                default:
                    {
                        WindowsPCProfile(device);
                        break;
                    }
            }
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

        private void MacintoshProfile(Models.Device device)
        {
            Id = device.ID;
            Name = device.Name;
            Email = device.Email.EmailAccount;
            Description = "";
            OperatingSystem = "";

            #region Device
            CPU = new CPU { DeviceMemory = 16, HardwareConcurrency = 8 };
            Battery = new Battery { Charging = true, ChargingTime = 1486, DischargingTime = 1328, Level = 0.52 };
            EnableAudioApi = false;
            EnablePlugins = false;
            EnableMediaPlugins = false;
            #endregion Device

            Fonts = Fonts.Windows10;

            #region Content
            RandomTimersEnabled = true;
            UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/68.0.3440.106 Safari/537.36";
            Screen = new CommandModel.Screen { Width = 1680, Height = 1050, Color = 24 };
            HistoryLength = 2;
            #endregion Content

            #region Fingerprint
            WebGL = new WebGL { Plus1 = 1007, Plus2 = 25502, Plus3 = 31603, Plus4 = 42975, Plus5 = 12677, BrowserplugsR = 88 };
            FakeClientRects = true;
            Canvas = new Canvas { R = 5, G = 4, B = 1 };
            #endregion Fingerprint

            #region Geo
            EnableNetwork = true;
            Language = "en";
            GeoIpEnabled = true;
            #endregion Geo

            #region Proxy
            ProxyEnabled = true;
            //Proxies = proxies,
            ByPassProxySites = new List<string>();
            #endregion
        }

        private void IPhoneProfile(Models.Device device)
        {
            Id = device.ID;
            Name = device.Name;
            Email = device.Email.EmailAccount;
            Description = "";
            OperatingSystem = "";

            #region Device
            CPU = new CPU { DeviceMemory = 16, HardwareConcurrency = 8 };
            Battery = new Battery { Charging = true, ChargingTime = 1486, DischargingTime = 1328, Level = 0.52 };
            EnableAudioApi = false;
            EnablePlugins = false;
            EnableMediaPlugins = false;
            #endregion Device

            Fonts = Fonts.Windows10;

            #region Content
            RandomTimersEnabled = true;
            UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/68.0.3440.106 Safari/537.36";
            Screen = new CommandModel.Screen { Width = 1680, Height = 1050, Color = 24 };
            HistoryLength = 2;
            #endregion Content

            #region Fingerprint
            WebGL = new WebGL { Plus1 = 1007, Plus2 = 25502, Plus3 = 31603, Plus4 = 42975, Plus5 = 12677, BrowserplugsR = 88 };
            FakeClientRects = true;
            Canvas = new Canvas { R = 5, G = 4, B = 1 };
            #endregion Fingerprint

            #region Geo
            EnableNetwork = true;
            Language = "en";
            GeoIpEnabled = true;
            #endregion Geo

            #region Proxy
            ProxyEnabled = true;
            //Proxies = proxies,
            ByPassProxySites = new List<string>();
            #endregion
        }

        private void IPadProfile(Models.Device device)
        {
            Id = device.ID;
            Name = device.Name;
            Email = device.Email.EmailAccount;
            Description = "";
            OperatingSystem = "";

            #region Device
            CPU = new CPU { DeviceMemory = 16, HardwareConcurrency = 8 };
            Battery = new Battery { Charging = true, ChargingTime = 1486, DischargingTime = 1328, Level = 0.52 };
            EnableAudioApi = false;
            EnablePlugins = false;
            EnableMediaPlugins = false;
            #endregion Device

            Fonts = Fonts.Windows10;

            #region Content
            RandomTimersEnabled = true;
            UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/68.0.3440.106 Safari/537.36";
            Screen = new CommandModel.Screen { Width = 1680, Height = 1050, Color = 24 };
            HistoryLength = 2;
            #endregion Content

            #region Fingerprint
            WebGL = new WebGL { Plus1 = 1007, Plus2 = 25502, Plus3 = 31603, Plus4 = 42975, Plus5 = 12677, BrowserplugsR = 88 };
            FakeClientRects = true;
            Canvas = new Canvas { R = 5, G = 4, B = 1 };
            #endregion Fingerprint

            #region Geo
            EnableNetwork = true;
            Language = "en";
            GeoIpEnabled = true;
            #endregion Geo

            #region Proxy
            ProxyEnabled = true;
            //Proxies = proxies,
            ByPassProxySites = new List<string>();
            #endregion
        }

        private void SumSungProfile(Models.Device device)
        {
            Id = device.ID;
            Name = device.Name;
            Email = device.Email.EmailAccount;
            Description = "";
            OperatingSystem = "";

            #region Device
            CPU = new CPU { DeviceMemory = 16, HardwareConcurrency = 8 };
            Battery = new Battery { Charging = true, ChargingTime = 1486, DischargingTime = 1328, Level = 0.52 };
            EnableAudioApi = false;
            EnablePlugins = false;
            EnableMediaPlugins = false;
            #endregion Device

            Fonts = Fonts.Windows10;

            #region Content
            RandomTimersEnabled = true;
            UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/68.0.3440.106 Safari/537.36";
            Screen = new CommandModel.Screen { Width = 1680, Height = 1050, Color = 24 };
            HistoryLength = 2;
            #endregion Content

            #region Fingerprint
            WebGL = new WebGL { Plus1 = 1007, Plus2 = 25502, Plus3 = 31603, Plus4 = 42975, Plus5 = 12677, BrowserplugsR = 88 };
            FakeClientRects = true;
            Canvas = new Canvas { R = 5, G = 4, B = 1 };
            #endregion Fingerprint

            #region Geo
            EnableNetwork = true;
            Language = "en";
            GeoIpEnabled = true;
            #endregion Geo

            #region Proxy
            ProxyEnabled = true;
            //Proxies = proxies,
            ByPassProxySites = new List<string>();
            #endregion
        }

        private void XiaomiProfile(Models.Device device)
        {
            Id = device.ID;
            Name = device.Name;
            Email = device.Email.EmailAccount;
            Description = "";
            OperatingSystem = "";

            #region Device
            CPU = new CPU { DeviceMemory = 16, HardwareConcurrency = 8 };
            Battery = new Battery { Charging = true, ChargingTime = 1486, DischargingTime = 1328, Level = 0.52 };
            EnableAudioApi = false;
            EnablePlugins = false;
            EnableMediaPlugins = false;
            #endregion Device

            Fonts = Fonts.Windows10;

            #region Content
            RandomTimersEnabled = true;
            UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/68.0.3440.106 Safari/537.36";
            Screen = new CommandModel.Screen { Width = 1680, Height = 1050, Color = 24 };
            HistoryLength = 2;
            #endregion Content

            #region Fingerprint
            WebGL = new WebGL { Plus1 = 1007, Plus2 = 25502, Plus3 = 31603, Plus4 = 42975, Plus5 = 12677, BrowserplugsR = 88 };
            FakeClientRects = true;
            Canvas = new Canvas { R = 5, G = 4, B = 1 };
            #endregion Fingerprint

            #region Geo
            EnableNetwork = true;
            Language = "en";
            GeoIpEnabled = true;
            #endregion Geo

            #region Proxy
            ProxyEnabled = true;
            //Proxies = proxies,
            ByPassProxySites = new List<string>();
            #endregion
        }

        private void OppeProfile(Models.Device device)
        {
            Id = device.ID;
            Name = device.Name;
            Email = device.Email.EmailAccount;
            Description = "";
            OperatingSystem = "";

            #region Device
            CPU = new CPU { DeviceMemory = 16, HardwareConcurrency = 8 };
            Battery = new Battery { Charging = true, ChargingTime = 1486, DischargingTime = 1328, Level = 0.52 };
            EnableAudioApi = false;
            EnablePlugins = false;
            EnableMediaPlugins = false;
            #endregion Device

            Fonts = Fonts.Windows10;

            #region Content
            RandomTimersEnabled = true;
            UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/68.0.3440.106 Safari/537.36";
            Screen = new CommandModel.Screen { Width = 1680, Height = 1050, Color = 24 };
            HistoryLength = 2;
            #endregion Content

            #region Fingerprint
            WebGL = new WebGL { Plus1 = 1007, Plus2 = 25502, Plus3 = 31603, Plus4 = 42975, Plus5 = 12677, BrowserplugsR = 88 };
            FakeClientRects = true;
            Canvas = new Canvas { R = 5, G = 4, B = 1 };
            #endregion Fingerprint

            #region Geo
            EnableNetwork = true;
            Language = "en";
            GeoIpEnabled = true;
            #endregion Geo

            #region Proxy
            ProxyEnabled = true;
            //Proxies = proxies,
            ByPassProxySites = new List<string>();
            #endregion
        }

        private void WindowsPCProfile(Models.Device device)
        {
            Id = device.ID;
            Name = device.Name;
            Email = device.Email.EmailAccount;
            Description = "";
            OperatingSystem = "";

            #region Device
            CPU = new CPU { DeviceMemory = 16, HardwareConcurrency = 8 };
            Battery = new Battery { Charging = true, ChargingTime = 1486, DischargingTime = 1328, Level = 0.52 };
            EnableAudioApi = false;
            EnablePlugins = false;
            EnableMediaPlugins = false;
            #endregion Device

            Fonts = Fonts.Windows10;

            #region Content
            RandomTimersEnabled = true;
            UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/68.0.3440.106 Safari/537.36";
            Screen = new CommandModel.Screen { Width = 1680, Height = 1050, Color = 24 };
            HistoryLength = 2;
            #endregion Content

            #region Fingerprint
            WebGL = new WebGL { Plus1 = 1007, Plus2 = 25502, Plus3 = 31603, Plus4 = 42975, Plus5 = 12677, BrowserplugsR = 88 };
            FakeClientRects = true;
            Canvas = new Canvas { R = 5, G = 4, B = 1 };
            #endregion Fingerprint

            #region Geo
            EnableNetwork = true;
            Language = "en";
            GeoIpEnabled = true;
            #endregion Geo

            #region Proxy
            ProxyEnabled = true;
            //Proxies = proxies,
            ByPassProxySites = new List<string>();
            #endregion
        }
    }
}
