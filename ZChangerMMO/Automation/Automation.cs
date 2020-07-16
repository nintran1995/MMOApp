using Clifton.Core.Pipes;
using CommandModel;
using ZChangerMMO.BaseHost;
using Fclp;
using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using System.Xml.Serialization;
using ZChangerMMO.BackupAndRestore;
using ZChangerMMO.DataModels;
using ZChangerMMO.Model;
using ZChangerMMO.Utility;
using ZChangerMMO.Licensing.Licensing;

namespace ZChangerMMO
{
    class COptions
    {
        public bool Auto { get; set; }

        public bool Restore { get; set; }

        public bool Backup { get; set; }

        public bool IsRandom { get; set; }

        public string Config { get; set; }

        public string Proxy { get; set; }
    }

    public class CConfig
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public int ProxyEnabled { get; set; }
        public int PluginsEnabled { get; set; }
        public int AudioAPIEnabled { get; set; }
        public int MediaPluginsEnabled { get; set; }
        public int FingerPrintGeoLanguage { get; set; }
        public int DeviceCPUMemory { get; set; }
        public int DeviceCPUHardwareConcurency { get; set; }
        public int DeviceBatteryCharging { get; set; }
        public int DeviceBatteryChargingTime { get; set; }
        public int DeviceBatteryDischargingTime { get; set; }
        public int DeviceBatteryLevel { get; set; }
        public int DeviceUserAgent { get; set; }
        public int DeviceUserAgentScreenWidth { get; set; }
        public int DeviceUserAgentScreenHeight { get; set; }
        public int DeviceUserAgentScreenColor { get; set; }
        public int DeviceHistoryLength { get; set; }
        public int FingerPrintWebGLPlus1 { get; set; }
        public int FingerPrintWebGLPlus2 { get; set; }
        public int FingerPrintWebGLPlus3 { get; set; }
        public int FingerPrintWebGLPlus4 { get; set; }
        public int FingerPrintWebGLPlus5 { get; set; }
        public int FingerPrintWebGLBrowserPlugsR { get; set; }
        public int FingerPrintFakeValuesForGetClientRects { get; set; }
        public int FingerPrintCanvasR { get; set; }
        public int FingerPrintCanvasG { get; set; }
        public int FingerPrintCanvasB { get; set; }
    }

    public class CProfile
    {
        public string FingerPrintGeoLanguage { get; set; }
        public int DeviceCPUMemory { get; set; }
        public int DeviceCPUHardwareConcurency { get; set; }
        public bool DeviceBatteryCharging { get; set; }
        public int DeviceBatteryChargingTime { get; set; }
        public int DeviceBatteryDischargingTime { get; set; }
        public int DeviceBatteryLevel { get; set; }
        public string DeviceUserAgent { get; set; }
        public int DeviceUserAgentScreenWidth { get; set; }
        public int DeviceUserAgentScreenHeight { get; set; }
        public int DeviceUserAgentScreenColor { get; set; }
        public int DeviceHistoryLength { get; set; }
        public int FingerPrintWebGLPlus1 { get; set; }
        public int FingerPrintWebGLPlus2 { get; set; }
        public int FingerPrintWebGLPlus3 { get; set; }
        public int FingerPrintWebGLPlus4 { get; set; }
        public int FingerPrintWebGLPlus5 { get; set; }
        public int FingerPrintWebGLBrowserPlugsR { get; set; }
        public int FingerPrintFakeValuesForGetClientRects { get; set; }
        public int FingerPrintCanvasR { get; set; }
        public int FingerPrintCanvasG { get; set; }
        public int FingerPrintCanvasB { get; set; }
    }

    public class CProxy
    {
        public string Name { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Scheme { get; set; }
    }

    public class Automation
    {
        #region Properties
        public string[] DefaultBackupList = new string[] {
            "Extensions",
            "Cookies",
            "Current Session",
            "Current Tabs",
            "Login Data",
            "Preferences",
            "Secure Preferences",
            "Visited Links",
            "Web Data",
            "History"
        };

        public string[] DefaultBackupDataItems = new string[] {
            "First Run",
            "Last Browser",
            "Last Version",
            "Local State",
            "Module Info Cache",
            "Safe Browsing Cookies",
            "Safe Browsing Cookies-journal"
        };

        CConfig _cConfig;
        List<CProxy> _cProxy;
        bool _enabled = false;
        bool _isRestore = false;
        bool _isBackup = false;
        bool _isRandom = false;
        Random _random = new Random();
        CProfile _cProfile = new CProfile();
        Profile profile = new Profile();
        string BackUpFilePath = "";
        string AutomationBackUpFileName = "automationBackUp";
        public List<BackupDataItem> BackupDataItems;
        public List<BackupDataItem> CommonBackupDataItems;
        #endregion

        public Automation()
        {
            // register host with chrome
            Host = new ChromeServerHost();
            if (!Host.IsRegisteredWithChrome())
            {
                Host.GenerateManifest(Description, AllowedOrigins);
                Host.Register();
            }

            serverPipes = new Dictionary<long, ServerPipe>();
            browserProcessDic = new Dictionary<long, int>();
            BrowserInstallPath = GetInstalledBrowserPath();
            BackUpFilePath = $"{ GetSavedBackUpPath("AutomationProfile")}\\{AutomationBackUpFileName}.zcg";
            BackupDataItems = GetBackupItems();
            CommonBackupDataItems = GetDefaultBackupDataItems();
        }

        #region Public actions

        public void RunBackup()
        {
            string profilePath = GetProfileFolder(profile.Id, profile.Name);
            string destFolder = GetSavedBackUpPath(profile.Name);
            var backupProcess = new BackupProcess(profilePath, destFolder, profile);
            List<BackupDataItem> listItems = BackupDataItems.Concat(CommonBackupDataItems).ToList();
            backupProcess.StartBackUpAutomation(AutomationBackUpFileName, listItems);
        }

        public void RunRestore()
        {
            try
            {
                RestoreProcess restoreProcess = new RestoreProcess();
                //BackupItem selectedBackupItem = gridView2.GetFocusedRow() as BackupItem;
                string destFolder = $"{GetProfileFolder(profile.Id, profile.Name)}";
                restoreProcess.ExtractFileToDirectory(BackUpFilePath, destFolder);
                // get backup profile
                var profilePath = $"{destFolder}\\{Constants.ProfileBackUpFileName}";
                if (File.Exists(profilePath))
                {
                    var mySerializer = new XmlSerializer(typeof(ProfileXML));
                    // To read the file, create a FileStream.
                    var myFileStream = new FileStream(profilePath, FileMode.Open);
                    // Call the Deserialize method and cast to the object type.
                    var savedProfile = (ProfileXML)mySerializer.Deserialize(myFileStream);
                    myFileStream.Close();
                    UpdateAutomationProfile(savedProfile);
                    File.Delete(profilePath);
                }
            }
            catch (Exception ex)
            {
            }
        }

        public void RandomProfile()
        {
            RandomDevice(profile);
            RandomContent(profile);
            RandomFingerprint(profile);
            RandomGeo(profile);
        }

        public void RunBrowser()
        {
            try
            {
                string profileFolder = GetProfileFolder(profile.Id, profile.Name);
                profileFolder = expireValue(profileFolder, "D:\\AppData");
                CreateThreadToHandlerPipeServer();
                Process process = new Process();
                process.StartInfo.FileName = BrowserExecute;
                process.StartInfo.Arguments = $"--user-data-dir=\"{profileFolder}\"";
                process.Start();
                browserProcessDic.Add(profile.Id, process.Id);
            }
            catch (Exception ex)
            {
            }
        }

        public bool StopBrowser()
        {
            return false;
        }

        public bool ShouldRestore()
        {
            return _isRestore;
        }

        public bool ShouldBackup()
        {
            return _isBackup;
        }

        public bool ShouldRandom()
        {
            return _isRandom;
        }

        public bool Enabled()
        {
            return _enabled;
        }

        public void ParseCommandLineArgs(string[] args)
        {
            // create a generic parser for the ApplicationArguments type
            var p = new FluentCommandLineParser<COptions>();

            // specify which property the value will be assigned too.
            p.Setup(arg => arg.Auto)
             .As('a', "auto") // define the short and long option name
             .SetDefault(false);

            p.Setup(arg => arg.Restore)
           .As('r', "restore")
           .SetDefault(false);

            p.Setup(arg => arg.Backup)
           .As('b', "backup")
           .SetDefault(false);

            p.Setup(arg => arg.IsRandom)
           .As('i', "israndom")
           .SetDefault(false);

            p.Setup(arg => arg.Config)
             .As('c', "config")
             .SetDefault("{}");

            p.Setup(arg => arg.Proxy)
             .As('p', "proxy")
             .SetDefault("{}");

            var result = p.Parse(args);

            if (result.HasErrors == false)
            {
                try
                {
                    _enabled = p.Object.Auto;
                    _isRestore = p.Object.Restore;
                    _isBackup = p.Object.Backup;
                    _isRandom = p.Object.IsRandom;
                    _cConfig = JsonConvert.DeserializeObject<CConfig>(p.Object.Config);
                    _cProxy = JsonConvert.DeserializeObject<List<CProxy>>(p.Object.Proxy);
                    _cProfile = new CProfile();
                    profile = new Profile()
                    {
                        Name = !String.IsNullOrEmpty(_cConfig.Name) ? _cConfig.Name : "AutomationProfile",
                        Email = !String.IsNullOrEmpty(_cConfig.Email) ? _cConfig.Email : "automation@gmail.com",
                        Description = "Automation profile",

                        #region Device
                        CPU = new CPU { DeviceMemory = 16, HardwareConcurrency = 8 },
                        Battery = new Battery { Charging = true, ChargingTime = 1486, DischargingTime = 1328, Level = 0.52 },
                        EnableAudioApi = false,
                        EnablePlugins = false,
                        EnableMediaPlugins = false,
                        #endregion Device

                        Fonts = Fonts.Windows10,

                        #region Content
                        RandomTimersEnabled = true,
                        UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/68.0.3440.106 Safari/537.36",
                        Screen = new CommandModel.Screen { Width = 1680, Height = 1050, Color = 24 },
                        HistoryLength = 2,
                        #endregion Content

                        #region Fingerprint
                        WebGL = new WebGL { Plus1 = 1007, Plus2 = 25502, Plus3 = 31603, Plus4 = 42975, Plus5 = 12677, BrowserplugsR = 88 },
                        FakeClientRects = true,
                        Canvas = new Canvas { R = 5, G = 4, B = 1 },
                        #endregion Fingerprint

                        #region Geo
                        EnableNetwork = true,
                        Language = "en",
                        GeoIpEnabled = true,
                        #endregion Geo
                        #region Proxy
                        ProxyEnabled = true,
                        Proxies = new List<Proxy>(),
                        ByPassProxySites = new List<string>()
                        #endregion
                    };
                    //profile.Proxies = _cProxy.Select(cp => new Proxy()
                    //{
                    //    Enabled = true,
                    //    Host = cp.Host,
                    //    Name = cp.Name,
                    //    Port = cp.Port.ToString(),
                    //    Scheme = cp.Scheme,
                    //    UserName = cp.UserName,
                    //    Password = cp.Password
                    //}).ToList();
                    BackUpFilePath = $"{ GetSavedBackUpPath(_cConfig.Name)}\\{AutomationBackUpFileName}.zcg";

                }
                catch
                {

                }
            }
        }
        #endregion

        #region private functions
        private void UpdateAutomationProfile(ProfileXML savedProfile)
        {
            try
            {
                profile.Description = savedProfile.Description;
                profile.Email = savedProfile.Email;
                profile.Fonts = savedProfile.Fonts;
                profile.Name = savedProfile.Name;
                profile.OperatingSystem = savedProfile.OperatingSystem;
                profile.CPU.DeviceMemory = savedProfile.CPU.DeviceMemory;
                profile.CPU.HardwareConcurrency = savedProfile.CPU.HardwareConcurrency;
                profile.Battery.Charging = savedProfile.Battery.Charging;
                profile.Battery.ChargingTime = savedProfile.Battery.ChargingTime;
                profile.Battery.DischargingTime = savedProfile.Battery.DischargingTime;
                profile.Battery.Level = savedProfile.Battery.Level;
                profile.EnableAudioApi = savedProfile.EnableAudioApi;
                profile.EnablePlugins = savedProfile.EnablePlugins;
                profile.EnableMediaPlugins = savedProfile.EnableMediaPlugins;
                profile.RandomTimersEnabled = savedProfile.RandomTimersEnabled;
                profile.UserAgent = savedProfile.UserAgent;
                profile.Screen.Color = savedProfile.Screen.Color;
                profile.Screen.Height = savedProfile.Screen.Height;
                profile.Screen.Width = savedProfile.Screen.Width;
                profile.HistoryLength = savedProfile.HistoryLength;
                profile.WebGL.BrowserplugsR = savedProfile.WebGL.BrowserplugsR;
                profile.WebGL.Plus1 = savedProfile.WebGL.Plus1;
                profile.WebGL.Plus2 = savedProfile.WebGL.Plus2;
                profile.WebGL.Plus3 = savedProfile.WebGL.Plus3;
                profile.WebGL.Plus4 = savedProfile.WebGL.Plus4;
                profile.WebGL.Plus5 = savedProfile.WebGL.Plus5;
                profile.FakeClientRects = savedProfile.FakeClientRects;
                profile.Canvas.B = savedProfile.Canvas.B;
                profile.Canvas.G = savedProfile.Canvas.G;
                profile.Canvas.R = savedProfile.Canvas.R;
                profile.EnableNetwork = savedProfile.EnableNetwork;
                profile.Language = savedProfile.Language;
                profile.GeoIpEnabled = savedProfile.GeoIpEnabled;
                profile.ByPassProxySites = savedProfile.ByPassProxySites;
                profile.Proxies = savedProfile.Proxies;
            }
            catch (Exception ex)
            {
                throw new Exception("Update backup profile error");
            }
        }

        public List<BackupDataItem> GetBackupItems()
        {
            List<BackupDataItem> items = new List<BackupDataItem>();
            foreach (string item in DefaultBackupList)
            {
                items.Add(new BackupDataItem { Name = item, Size = string.Empty, Type = string.Equals("Extensions", item) ? BackupDataItemType.FOLDER : BackupDataItemType.FILE, ItemLevel = ItemLevel.PROFILE });
            }

            return items;
        }

        public List<BackupDataItem> GetDefaultBackupDataItems()
        {
            List<BackupDataItem> list = new List<BackupDataItem>();

            foreach (string fileName in DefaultBackupDataItems)
            {
                list.Add(new BackupDataItem { Name = fileName, Size = string.Empty, Type = BackupDataItemType.FILE, ItemLevel = ItemLevel.USER });
            }

            return list;
        }
        #endregion

        #region Location Management
        public string BrowserInstallPath { get; set; }

        public string BrowserProfilePathBase
        {
            get
            {
                return expireValue($"{BrowserInstallPath}\\BrowserData", "C:\\");
            }
        }

        private string GetInstalledBrowserPath()
        {
            RegistryKey regKey = Registry.LocalMachine.OpenSubKey(Constants.ZChangerLocationReg, true);
            return expireValue(regKey.GetValue("Browser").ToString(), "aaa");
        }

        public string BrowserExecute
        {
            get
            {
                return expireValue(String.Format("{0}\\chrome.exe", GetInstalledBrowserPath()), "C:\\chrome.exe");
            }
        }

        private string GetProfileFolder(long id, string emailAddress)
        {
            if (!Directory.Exists(BrowserProfilePathBase))
            {
                Directory.CreateDirectory(BrowserProfilePathBase);
            }

            string profileFolder = $"{BrowserProfilePathBase}\\ZC_{id}_{emailAddress}";
            if (!Directory.Exists(profileFolder))
            {
                Directory.CreateDirectory(profileFolder);
            }

            return expireValue(profileFolder, "lalala");
        }

        private string GetSavedBackUpPath(string email)
        {
            string path = $"{Application.StartupPath}\\BackupProfiles\\ZC_{email}";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            return expireValue(path, "22222");
        }
        #endregion

        #region Validation Licence
        /// <summary>
        /// This C# code reads a key from the windows registry.
        /// </summary>
        /// <param name="keyName">
        /// <returns></returns>
        static string ReadRG(string keyName)
        {
            string subKey = "SOFTWARE\\WindowsAppServiceB2B";
            try
            {
                RegistryKey sk = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(subKey);
                if (sk == null)
                    return string.Empty;
                else
                    return sk.GetValue(keyName.ToUpper()).ToString();
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// This C# code writes a key to the windows registry.
        /// </summary>
        /// <param name="keyName">
        /// <param name="value">
        static void WriteRG(string keyName, string value)
        {
            string subKey = "SOFTWARE\\WindowsAppServiceB2B";

            RegistryKey sk1 = Microsoft.Win32.Registry.LocalMachine.CreateSubKey(subKey);
            sk1.SetValue(keyName.ToUpper(), value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="requestValue"></param>
        /// <param name="expiredValue"></param>
        /// <returns></returns>
        static string expireValue(string requestValue, string expiredValue)
        {
            if (TimeLock.IsExpired())
            {             
                var generator = new RandomGenerator();

                if (generator.RandomNumber(1, 6) == 3)
                {
                    return expiredValue;
                }
            }

            return requestValue;
        }
        #endregion

        #region Comunicate web browser properties

        static ChromeServerHost Host;
        static string[] AllowedOrigins = new string[] { "chrome-extension://khjlaccalgjpbimfpoifhncdempmnddn/" };
        static string Description = "Hosting for tranfer message between web browser and app";
        // contact with host
        Dictionary<long, ServerPipe> serverPipes;
        Dictionary<long, int> browserProcessDic;

        #endregion

        #region Comunicate web browser actions

        private void CreateThreadToHandlerPipeServer()
        {
            Thread serverThread = new Thread(ServerThread);
            serverThread.Start();
        }

        private void ServerThread(object data)
        {
            ServerPipe serverPipe = new ServerPipe("Test", p => p.StartStringReaderAsync());
            serverPipe.DataReceived += (sndr, args) =>
            {
                DataRecievedHandler(serverPipe, args);
            };
            serverPipe.Connected += (sndr, args) =>
            {
                Profile selectedObject = profile;
                if (!serverPipes.ContainsKey(selectedObject.Id))
                {
                    serverPipe.Id = selectedObject.Id;
                    serverPipes.Add(selectedObject.Id, serverPipe);
                }
            };
            serverPipe.PipeClosed += (sndr, args) =>
            {
                DissposeHostConnection(serverPipe.Id);
            };
        }

        public void DataRecievedHandler(ServerPipe serverPipe, PipeEventArgs arguments)
        {
            var commandJsonString = arguments.String;
            BaseCommand baseCommand = JsonConvert.DeserializeObject<BaseCommand>(commandJsonString);
            if (baseCommand == null) return;
            switch (baseCommand.Command)
            {
                case ZC_Command.CONFIRM_CONNECT_HOST:
                    {
                        ConfirmConnectedHost response = JsonConvert.DeserializeObject<ConfirmConnectedHost>(commandJsonString);
                        if (response != null && response.IsSuccess)
                        {
                            Profile selectedObject = profile;

                            List<string> ByPassListPattern = selectedObject.ByPassProxySites.Select(p =>
                            {
                                return $"*{p}*";
                            }).ToList();


                            SetProfileRequest request = new SetProfileRequest()
                            {
                                Command = ZC_Command.SET_PROFILE_REQUEST,
                                Profile = new BrowserProfile()
                                {
                                    Id = selectedObject.Id,
                                    Email = selectedObject.Email,
                                    CPU = selectedObject.CPU,
                                    Battery = selectedObject.Battery,
                                    EnableAudioApi = selectedObject.EnableAudioApi,
                                    EnablePlugins = selectedObject.EnablePlugins,
                                    EnableMediaPlugins = selectedObject.EnableMediaPlugins,
                                    Fonts = selectedObject.Fonts,
                                    RandomTimersEnabled = selectedObject.RandomTimersEnabled,
                                    UserAgent = selectedObject.UserAgent,
                                    Screen = selectedObject.Screen,
                                    HistoryLength = selectedObject.HistoryLength,
                                    WebGL = selectedObject.WebGL,
                                    FakeClientRects = true,
                                    Canvas = selectedObject.Canvas,
                                    EnableNetwork = selectedObject.EnableNetwork,
                                    Language = selectedObject.Language,
                                    GeoIpEnabled = selectedObject.GeoIpEnabled,
                                    ProxyEnabled = selectedObject.ProxyEnabled,
                                    Proxies = selectedObject.Proxies.Where(p => p.Enabled).ToList(),
                                    ByPassProxySites = ByPassListPattern,
                                }
                            };

                            serverPipe.WriteString(JsonConvert.SerializeObject(request));
                        }
                        break;
                    }
                case ZC_Command.NOTIFICATION:
                    {
                        Notification response = JsonConvert.DeserializeObject<Notification>(commandJsonString);
                        if (response != null && response.IsSuccess)
                        {
                            Notification request = new Notification()
                            {
                                Type = NotificationType.INFOR,
                                IsSuccess = true,
                                Message = "Hello from zchanger application"
                            };

                            serverPipe.WriteString(JsonConvert.SerializeObject(request));
                        }
                        break;
                    }
                case ZC_Command.DISCONNECT:
                    {
                        DissposeHostConnection(serverPipe.Id);
                        break;
                    }
            }
        }

        private void DissposeHostConnection(long profileId)
        {
            if (browserProcessDic.ContainsKey(profileId))
            {
                int browserProcessId;
                if (browserProcessDic.TryGetValue(profileId, out browserProcessId))
                {
                    browserProcessDic.Remove(profileId);

                    var process = Process.GetProcessById(browserProcessId);
                    if (process != null)
                    {
                        process.Kill();
                    }
                }
            }
            if (serverPipes.ContainsKey(profileId))
            {
                serverPipes.Remove(profileId);
            }
        }

        #endregion

        #region Random
        void RandomDevice(Profile profile)
        {
            if (_cConfig.DeviceCPUMemory == 1)
            {
                var cores = new List<int>() { 8, 12, 16, 32 };
                profile.CPU.DeviceMemory = cores[_random.Next(4)];
            }
            if (_cConfig.DeviceCPUHardwareConcurency == 1)
            {
                var cores = new List<int>() { 8, 12, 16, 32 };
                profile.CPU.HardwareConcurrency = cores[_random.Next(4)];
            }

            if (_cConfig.DeviceBatteryCharging == 1)
            {
                var cores = new List<bool>() { true, false };
                profile.Battery.Charging = cores[_random.Next(2)];
            }
            if (_cConfig.DeviceBatteryChargingTime == 1)
            {
                profile.Battery.ChargingTime = (_random.Next(6100) + 1200);
            }
            if (_cConfig.DeviceBatteryDischargingTime == 1)
            {
                profile.Battery.DischargingTime = (_random.Next(7000) + 1150);
            }
            if (_cConfig.DeviceBatteryLevel == 1)
            {
                profile.Battery.Level = (_random.Next(100) * 0.01);
            }
        }

        private void RandomContent(Profile profile)
        {
            var screens = new List<CommandModel.Screen>()
                {
                    new CommandModel.Screen(){
                        Width= 1920,
                        Height = 1080,
                        Color = 32
                    },
                     new CommandModel.Screen(){
                        Width= 1680,
                        Height = 1050,
                        Color = 32
                    },
                    new CommandModel.Screen() {
                        Width= 1600,
                        Height = 900,
                        Color = 32
                    },
                    new CommandModel.Screen() {
                        Width= 1440,
                        Height = 900,
                        Color = 24
                    },
                    new CommandModel.Screen() {
                        Width= 1366,
                        Height = 768,
                        Color = 24
                    },
                    new CommandModel.Screen() {
                        Width= 1360,
                        Height = 768,
                        Color = 24
                    }
                };
            var screen = screens[_random.Next(6)];

            if (_cConfig.DeviceUserAgent == 1)
            {
                // profile.UserAgent = UserAgentStore.GetRandom(_random);
                var device = _random.Next(5);
                var ua = new UserAgentEngine((UserAgentEngine.Devices)device, /*facebook, instagram, snapchat,""*/"", "en_US");
                profile.UserAgent = ua.GetUserAgentString();
            }

            if (_cConfig.DeviceUserAgentScreenWidth == 1)
            {
                profile.Screen.Width = screen.Width;
            }
            if (_cConfig.DeviceUserAgentScreenHeight == 1)
            {
                profile.Screen.Height = screen.Height;
            }
            if (_cConfig.DeviceUserAgentScreenColor == 1)
            {
                profile.Screen.Color = screen.Color;
            }
            if (_cConfig.DeviceHistoryLength == 1)
            {
                var historisLength = new List<int> { 1, 2, 3, 4 };
                profile.HistoryLength = historisLength[_random.Next(4)];
            }
        }
        private void RandomFingerprint(Profile profile)
        {
            if (_cConfig.FingerPrintWebGLPlus1 == 1)
            {
                profile.WebGL.Plus1 = (_random.Next(36) + 1000);
            }
            if (_cConfig.FingerPrintWebGLPlus2 == 1)
            {
                profile.WebGL.Plus2 = (_random.Next(36384) + 10384);
            }
            if (_cConfig.FingerPrintWebGLPlus3 == 1)
            {
                profile.WebGL.Plus3 = (_random.Next(50188) + 20188);
            }
            if (_cConfig.FingerPrintWebGLPlus4 == 1)
            {
                profile.WebGL.Plus4 = (_random.Next(50188) + 20188);
            }
            if (_cConfig.FingerPrintWebGLPlus5 == 1)
            {
                profile.WebGL.Plus5 = (_random.Next(6100) + 8192);
            }
            if (_cConfig.FingerPrintWebGLBrowserPlugsR == 1)
            {
                profile.WebGL.BrowserplugsR = _random.Next(114);
            }

            if (_cConfig.FingerPrintCanvasR == 1)
            {
                profile.Canvas.R = _random.Next(6);
            }
            if (_cConfig.FingerPrintCanvasG == 1)
            {
                profile.Canvas.G = _random.Next(6);
            }
            if (_cConfig.FingerPrintCanvasB == 1)
            {
                profile.Canvas.B = _random.Next(6);
            }
        }
        private void RandomGeo(Profile profile)
        {
            if (_cConfig.FingerPrintGeoLanguage == 1)
            {
                var languages = new List<string>() { "en", "zh", "es", "ar", "pt", "id", "fr", "ja", "ru", "de" };
                profile.Language = languages[_random.Next(10)];
            }
        }

        #endregion
    }
}