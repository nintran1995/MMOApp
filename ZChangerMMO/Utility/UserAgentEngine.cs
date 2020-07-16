using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZChangerMMO
{
    public class UserAgentEngine
    {
        public enum Devices
        {
            WindowsPC,
            Macintosh,
            iPhone,
            iPad,
            Samsung,
            Xiaomi,
            Oppo,
        }

        private UserAgent _ua;
        private string _appCodeName;
        private string _locate = "en-US";

        public UserAgentEngine(Devices device, string appCodeName, string locate = "en-US")
        {
            try
            {
                _locate = convertLocate(locate.Substring(0, 2));
            }
            catch
            {
                _locate = "en-US";
            }
            _appCodeName = appCodeName;
            switch (device)
            {
                case Devices.Macintosh:
                    _ua = new MacAgent("Macintosh", appCodeName);

                    break;
                case Devices.iPhone:
                    _ua = new iPhoneUserAgent("iPhone", appCodeName);
                    break;
                case Devices.iPad:
                    _ua = new iPadUserAgent("iPad", appCodeName);
                    break;
                case Devices.Samsung:
                    _ua = new SamsungPhoneAgent("Samsung", appCodeName);
                    break;
                case Devices.Xiaomi:
                    _ua = new XiaomiPhoneAgent("Xiaomi", appCodeName);
                    break;
                case Devices.Oppo:
                    _ua = new OppoPhoneAgent("Oppo", appCodeName);
                    break;
                default:
                    //Windows PC
                    _ua = new WindowsAgent("Windows 10", appCodeName);
                    break;
            }
        }

        public string GetUserAgentString()
        {
            return _ua.GetUserAgent(_locate);
        }

        string convertLocate(string locate)
        {
            switch (locate)
            {
                case "en":
                    return "en-US";
                case "zh":
                    return "zh-CN";
                case "es":
                    return "es-MX";
                case "ar":
                    return "ar-QA";
                case "pt":
                    return "pt-BR";
                case "id":
                    return "id-ID";
                case "fr":
                    return "fr-FR";
                case "ja":
                    return "ja-JP";
                case "ru":
                    return "ru-RU";
                case "de":
                    return "de-DE";
                //case "vi":
                default:
                    return "vi-VN";
            };

        }
    }

    public abstract class UserAgent
    {
        /*
        {
           "folder": "/Browsers - Windows/Legacy Browsers",
           "description": "Firefox 20.0 (Win 8 32)",
           "userAgent": "Mozilla/5.0 (Windows NT 6.2; rv:20.0) Gecko/20121202 Firefox/20.0",
           "appCodename": "",
           "appName": "",
           "appVersion": "",
           "platform": "",
           "vendor": "",
           "vendorSub": "",
           "browserName": "Firefox",
           "browserMajor": "20",
           "browserVersion": "20.0",
           "deviceModel": "",
           "deviceType": "",
           "deviceVendor": "",
           "engineName": "Gecko",
           "engineVersion": "20.0",
           "osName": "Windows",
           "osVersion": "8",
           "cpuArchitecture": ""
       }
       */
        public string Name { get; set; }

        public string Description { get; set; }

        public string UserAgentx { get; set; }

        public string AppCodename { get; set; }

        public string AppName { get; set; }

        public string AppVersion { get; set; }

        public string AppInfo { get; set; }

        public string Platform { get; set; }

        public string Vendor { get; set; }

        public string VendorSub { get; set; }

        public string BrowserName { get; set; }

        public string BrowserMajor { get; set; }

        public string BrowserVersion { get; set; }

        public string DeviceModel { get; set; }

        public string DeviceType { get; set; }

        public string DeviceVendor { get; set; }

        public string EngineName { get; set; }

        public string EngineVersion { get; set; }

        public string OSName { get; set; }

        public string OSVersion { get; set; }

        public string CPUArchitecture { get; set; }

        public string Locate { get; set; }

        private static Random rnd = new Random();

        public abstract string GetUserAgent(string locate);

        internal static int GetRandomNumber(int max)
        {
            return rnd.Next(max);
        }

        internal static int GetRandomNumber(int min, int max)
        {
            var rtn = 0;
            if (min < max)
            {
                rtn = rnd.Next(min) + rnd.Next(max + 1 - min);
            }
            else
            {
                rtn = rnd.Next(max) + rnd.Next(min + 1 - max);
            }
            if (rtn > max) rtn = max;

            return rtn;
        }

        internal static string GetXRandomHexNumber(int length, bool upper = false)
        {
            var rslt = string.Empty;
            byte[] buffer = new byte[length / 2];
            rnd.NextBytes(buffer);
            string result = String.Concat(buffer.Select(x => x.ToString("X2")).ToArray());
            if (length % 2 == 0)
            {
                rslt = result;
            }
            else
            {
                rslt = result + rnd.Next(16).ToString("X");
            }

            if (upper) return rslt.ToUpper();

            return rslt;
        }

        internal static string GetRandomItemInList(List<string> list)
        {
            int r = GetRandomNumber(0, list.Count - 1);

            return (string)list[r];
        }

        internal static string GetRandomString(int length, bool upper = false)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            if (upper)
            {
                return (new string(Enumerable.Repeat(chars, length)
                    .Select(s => s[rnd.Next(s.Length)]).ToArray())).ToUpper();
            }
            else
            {
                return new string(Enumerable.Repeat(chars, length)
                  .Select(s => s[rnd.Next(s.Length)]).ToArray());
            }
        }

        internal static string GetXRandomNumber(int length)
        {
            const string chars = "0123456789";
            return rnd.Next(9) + new string(Enumerable.Repeat(chars, length - 1)
              .Select(s => s[rnd.Next(s.Length)]).ToArray());
        }

        internal static string GetRandomCarrier(string CountryCode)
        {
            List<string> list;
            if (CountryCode.StartsWith("VN"))
            {
                list = new List<string>() { "Vinaphone", "Viettel", "Vietnamobile", "Mobifone" };
            }
            else if (CountryCode.StartsWith("BH"))
            {
                list = new List<string>() { "Batelco", "Viva", "Zain" };
            }
            else if (CountryCode.StartsWith("OM"))
            {
                list = new List<string>() { "Omantel", "Ooredoo", "DIGI Mobil", "Idilis", "Lycamobile", "Orange", "Telecom", "Vodafone" };
            }
            else if (CountryCode.StartsWith("KW"))
            {
                list = new List<string>() { "Ooredoo", "Viva", "Zain" };
            }
            else if (CountryCode.StartsWith("US"))
            {
                list = new List<string>() { "GTA Wireless LLC", "Verizon Wireless", "T-Mobile", "Alaska Communications" };
            }
            else if (CountryCode.StartsWith("TW"))
            {
                list = new List<string>() { "Asia Pacific Telecom", "Far EasTone", "Chunghwa Telecom", "Taiwan Mobile" };
            }
            else if (CountryCode.StartsWith("TH"))
            {
                list = new List<string>() { "True Move", "ACT Mobile", "Digital Phone Co." };
            }
            else if (CountryCode.StartsWith("RU"))
            {
                list = new List<string>() { "MegaFon", "ACT Mobile", "Digital Phone Co." };
            }
            else if (CountryCode.StartsWith("KR"))
            {
                list = new List<string>() { "SK Telecom", "ACT Mobile" };
            }
            else if (CountryCode.StartsWith("IT"))
            {
                list = new List<string>() { "WIND" };
            }
            else if (CountryCode.StartsWith("IN"))
            {
                list = new List<string>() { "Vodafone", "S Tel", "Uninor" };
            }
            else if (CountryCode.StartsWith("SG"))
            {
                list = new List<string>() { "Singtel", "M1", "Uninor" };
            }
            else if (CountryCode.StartsWith("PH"))
            {
                list = new List<string>() { "Globe Telecom" };
            }
            else if (CountryCode.StartsWith("MY"))
            {
                list = new List<string>() {"Celcom",
                    "DiGi",
                    "Electcoms",
                    "Maxis",
                    "P1",
                    "Telekom",
                    "Tune Talk",
                    "U Mobile",
                    "Yes"};
            }
            else if (CountryCode.StartsWith("LT"))
            {
                list = new List<string>() { "UAB Tele2", "Omnitel", "Bite Lithuania", "Thuraya" };
            }
            else if (CountryCode.StartsWith("EE"))
            {
                list = new List<string>() { "EMT", "Elisa Estonia", "Tele2 Estonia", "Thuraya" };
            }
            else if (CountryCode.StartsWith("TK"))
            {
                list = new List<string>() { "Turkcell", "TelSIM", "Avea", "Thuraya" };
            }
            else if (CountryCode.StartsWith("JP"))
            {
                list = new List<string>() { "NTT Docomo" };
            }
            else if (CountryCode.StartsWith("ID"))
            {
                list = new List<string>() { "Telkomsel", "Esia", "H3G CP" };
            }
            else if (CountryCode.StartsWith("HK"))
            {
                list = new List<string>() { "China Unicom Ltd" };
            }
            else if (CountryCode.StartsWith("GB"))
            {
                list = new List<string>() { "O2 Ltd." };
            }
            else if (CountryCode.StartsWith("FR"))
            {
                list = new List<string>() {
                    "Bouygues",
                    "Free Mobile",
                    "Globalstar",
                    "La Poste Mobile",
                    "NRJ Mobile",
                    "Orange",
                    "SFR",
                    "Transatel Mobile",
                    "Virgin Mobile"};
            }
            else if (CountryCode.StartsWith("ES"))
            {
                list = new List<string>() { "Vodafone" };
            }
            else if (CountryCode.StartsWith("DE"))
            {
                list = new List<string>() { "KPN" };
            }
            else if (CountryCode.StartsWith("CA"))
            {
                list = new List<string>() { "Rogers Wireless" };
            }
            else if (CountryCode.StartsWith("BR"))
            {
                list = new List<string>() { "Sercomtel Cel" };
            }
            else /*if (countryCode.StartsWith("AU"))*/
            {
                list = new List<string>() { "AT&T", "Verizon", "T-Mobile", "Sprint" };
            }

            return GetRandomItemInList(list);
        }
    }

    public class iPhoneUserAgent : UserAgent
    {
        public iPhoneUserAgent(string CodeName, string AppCodename)
        {
            Name = CodeName;
            this.AppCodename = AppCodename;
        }

        public override string GetUserAgent(string locate = "en_US")
        {
            /*
            {
                "folder": "/Mobile Devices/Devices/Apple (iPhone etc)",
                "description": "iPad - iOS 9_3 - Safari 9 (601.1)",
                "userAgent": "Mozilla/5.0 (iPad; CPU OS 9_3_2 like Mac OS X) AppleWebKit/601.1.46 (KHTML, like Gecko) Version/9.0 Mobile/13F69 Safari/601.1",
                "appCodename": "",
                "appName": "",
                "appVersion": "",
                "platform": "",
                "vendor": "",
                "vendorSub": "",
                "browserName": "Mobile Safari",
                "browserMajor": "9",
                "browserVersion": "9.0",
                "deviceModel": "iPad",
                "deviceType": "tablet",
                "deviceVendor": "Apple",
                "engineName": "WebKit",
                "engineVersion": "601.1.46",
                "osName": "iOS",
                "osVersion": "9.3.2",
                "cpuArchitecture": ""
            }
             */
            DeviceModel = "iPhone";
            DeviceVendor = "Apple";
            EngineName = "WebKit";
            Locate = locate;

            BrowserVersion = GetRandomItemInList(new List<string>() {
                "603.3.8",
                "604.1", "604.1.15","604.1.38", "604.5.6",
                "605.1", "605.1.15",
            });
            EngineVersion = BrowserVersion;
            var BrowserVersionParts = BrowserVersion.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries)[0];
            var BrowserVersionShort = $"{BrowserVersionParts[0]}.{BrowserVersionParts[1]}";

            //Random device
            if (Name.Equals("iphone", StringComparison.CurrentCultureIgnoreCase))
            {
                Name = GetRandomItemInList(new List<string>() {
                    "iphone 8","iphone 8 plus",
                    "iphone x", "iphone xs",  "iphone xs max",
                    "iphone xr",
                    "iphone 11","iphone 11 pro","iphone 11 pro max",
                    ""
                });
            }

            switch (Name.ToLower())
            {
                case "iphone 8":
                    OSName = "iPhone10,4";
                    OSVersion = GetRandomItemInList(new List<string>() {
                        "13.1","13.1.1", "13.1.2", "13.1.3",
                        "13.2","13.2.1", "13.2.2", "13.2.3",
                    });
                    break;
                case "iphone 8 plus":
                    OSName = "iPhone10,5";
                    OSVersion = GetRandomItemInList(new List<string>() {
                        "13.1","13.1.1", "13.1.2", "13.1.3",
                        "13.2","13.2.1", "13.2.2", "13.2.3",
                    });
                    break;
                case "iphone x":
                    OSName = "iPhone10,6";
                    OSVersion = GetRandomItemInList(new List<string>() {
                        "13.1","13.1.1", "13.1.2", "13.1.3",
                        "13.2","13.2.1", "13.2.2", "13.2.3",
                    });
                    break;
                case "iphone xs":
                    OSName = "iPhone11,2";
                    OSVersion = GetRandomItemInList(new List<string>() {
                        "13.1","13.1.1", "13.1.2", "13.1.3",
                        "13.2","13.2.1", "13.2.2", "13.2.3",
                        "13.3","13.3.1",
                    });
                    break;
                case "iphone xs max":
                    OSName = "iPhone11,6";
                    OSVersion = GetRandomItemInList(new List<string>() {
                        "13.2","13.2.1", "13.2.2", "13.2.3",
                        "13.3","13.3.1",
                    });
                    break;
                case "iphone xr":
                    OSName = "iPhone11,8";
                    OSVersion = GetRandomItemInList(new List<string>() {
                        "13.2","13.2.1", "13.2.2", "13.2.3",
                        "13.3","13.3.1",
                    });
                    break;
                case "iphone 11":
                    OSName = "iPhone12,1";
                    OSVersion = GetRandomItemInList(new List<string>() {
                        "13.3","13.3.1",
                        "13.4","13.4.1",
                    });
                    break;
                case "iphone 11 pro":
                    OSName = "iPhone12,3";
                    OSVersion = GetRandomItemInList(new List<string>() {
                        "13.3","13.3.1",
                        "13.4","13.4.1",
                    });
                    break;
                case "iphone 11 pro max":
                    OSName = "iPhone12,5";
                    OSVersion = GetRandomItemInList(new List<string>() {
                        "13.3","13.3.1",
                        "13.4","13.4.1",
                    });
                    break;
                default:
                    OSVersion = GetRandomItemInList(new List<string>() {
                        "13.2.1", "13.2.2", "13.2.3",
                        "13.3.1",
                        "13.4.1",
                    });
                    break;
            }

            switch (AppCodename.ToLower())
            {
                case "facebook":
                    AppInfo = $"[FBAN/FBIOS;FBDV/{OSName};FBMD/{DeviceModel};FBSN/iOS;FBSV/{OSVersion};FBSS/3;FBCR/{GetRandomCarrier(Locate).Replace(" ", "")};FBID/phone;FBLC/{Locate};FBOP/5]";
                    break;
                case "instagram":
                    AppVersion = GetRandomItemInList(new List<string>() {
                        "135.0.0.22.118","131.0.0.21.117","125.0.0.18.125","122.1.0.16.239"
                        ,"137.0.0.30.124"
                    });
                    AppInfo = $"Instagram {AppVersion} ({OSName}; iOS {OSVersion.Replace(".", "_")}; en_US; en-US; scale=2.00; 750x1334; {GetXRandomNumber(9)})";
                    //Mozilla/5.0 (iPhone; CPU iPhone OS 13_1_3 like Mac OS X) AppleWebKit/605.1.15 (KHTML, like Gecko) Mobile/15E148 Instagram 116.0.0.24.361 (iPhone10,1; iOS 13_1_3; en_US; en-US; scale=2.00; 750x1334; 178418958)
                    //Mozilla/5.0 (iPhone; CPU iPhone OS 11_4 like Mac OS X) AppleWebKit/605.1.15 (KHTML, like Gecko) Mobile/15F79 Instagram 52.0.0.14.164 (iPhone8,4; iOS 11_4; ru_RU; ru-RU; scale=2.00; gamut=normal; 640x1136)
                    break;
                case "snapchat":
                    AppVersion = GetRandomItemInList(new List<string>() {
                        "10.77.5.59","10.77.0.54","10.75.0.67",
                        "10.76.5.67","10.80.5.79"
                    });
                    AppInfo = $"Snapchat/{AppVersion} ({OSName}; iOS {OSVersion}; gzip)";
                    //Mozilla/5.0 (iPhone; CPU iPhone OS 13_3 like Mac OS X) AppleWebKit/605.1.15 (KHTML, like Gecko) Snapchat/10.74.0.85 (iPhone10,1; iOS 13.3; gzip)
                    break;
                default:
                    AppVersion = GetRandomItemInList(new List<string>() {
                        "136.0.0.22.123" 
                        ,"135.0.0.22.118","131.0.0.21.117"
                        ,"137.0.0.30.124"
                    });

                    AppInfo = $"";
                    UserAgentx = $"Mozilla/5.0 (iPhone; CPU iPhone OS {OSVersion.Replace(".", "_")} like Mac OS X) AppleWebKit/{EngineVersion} (KHTML, like Gecko) Version/{OSVersion} Mobile/15E148 Safari/{BrowserVersionShort}";
                    //Mozilla/5.0 (iPhone; CPU iPhone OS 11_0 like Mac OS X) AppleWebKit/604.1.38 (KHTML, like Gecko) Version/11.0 Mobile/15A356 Safari/604.1

                    break;
            }

            if (!string.IsNullOrEmpty(AppInfo))
            {
                UserAgentx = $"Mozilla/5.0 ({DeviceModel}; CPU {DeviceModel} OS {OSVersion.Replace(".", "_")} like Mac OS X) AppleWebKit/{EngineVersion} (KHTML, like Gecko) Mobile/15E148 {AppInfo}";
            }

            return UserAgentx;
        }
    }

    public class iPadUserAgent : UserAgent
    {
        public iPadUserAgent(string CodeName, string AppCodename)
        {
            Name = CodeName;
            this.AppCodename = AppCodename;
        }

        public override string GetUserAgent(string locate = "en_US")
        {
            /*
            {
                "folder": "/Mobile Devices/Devices/Apple (iPhone etc)",
                "description": "iPad - iOS 9_3 - Safari 9 (601.1)",
                "userAgent": "Mozilla/5.0 (iPad; CPU OS 9_3_2 like Mac OS X) AppleWebKit/601.1.46 (KHTML, like Gecko) Version/9.0 Mobile/13F69 Safari/601.1",
                "appCodename": "",
                "appName": "",
                "appVersion": "",
                "platform": "",
                "vendor": "",
                "vendorSub": "",
                "browserName": "Mobile Safari",
                "browserMajor": "9",
                "browserVersion": "9.0",
                "deviceModel": "iPad",
                "deviceType": "tablet",
                "deviceVendor": "Apple",
                "engineName": "WebKit",
                "engineVersion": "601.1.46",
                "osName": "iOS",
                "osVersion": "9.3.2",
                "cpuArchitecture": ""
            }
             */
            DeviceModel = "iPad";
            DeviceVendor = "Apple";
            EngineName = "WebKit";
            Locate = locate;

            BrowserVersion = GetRandomItemInList(new List<string>() {
                "604.1", "604.1.15", "604.5.6",
                "605.1", "605.1.15",
            });
            EngineVersion = BrowserVersion;

            //Random device
            if (Name.Equals("ipad", StringComparison.CurrentCultureIgnoreCase))
            {
                Name = GetRandomItemInList(new List<string>() {
                    "ipad mini", "ipad mini 5",
                    "ipad air",
                    "ipad pro 3", "ipad pro 4",
                    ""
                });
            }

            switch (Name.ToLower())
            {
                #region iPad Mini
                case "ipad mini":
                    OSName = "iPad5,1";
                    OSVersion = GetRandomItemInList(new List<string>() {
                    "13.2.1", "13.2.2", "13.2.3",
                    "13.3.1",
                    "13.4.1",
                });
                    break;
                case "ipad mini 5":
                    OSName = "iPad11,2";
                    OSVersion = GetRandomItemInList(new List<string>() {
                    "13.2.1", "13.2.2", "13.2.3",
                    "13.3.1",
                    "13.4.1",
                });
                    break;
                #endregion

                #region iPad Air
                case "ipad air":
                    OSName = "iPad4,3";
                    OSVersion = GetRandomItemInList(new List<string>() {
                    "13.2.1", "13.2.2", "13.2.3",
                    "13.3.1",
                    "13.4.1",
                });
                    break;
                #endregion

                #region iPad Pro
                case "ipad pro 3":
                    OSName = "iPad8,7";
                    OSVersion = GetRandomItemInList(new List<string>() {
                    "13.2.1", "13.2.2", "13.2.3",
                    "13.3.1",
                    "13.4.1",
                });
                    break;
                case "ipad pro 4":
                    OSName = "iPad8,8";
                    OSVersion = GetRandomItemInList(new List<string>() {
                    "13.2.1", "13.2.2", "13.2.3",
                    "13.3.1",
                    "13.4.1",
                });
                    break;
                #endregion

                default:
                    OSVersion = GetRandomItemInList(new List<string>() {
                    "13.2.1", "13.2.2", "13.2.3",
                    "13.3.1",
                    "13.4.1",
                });
                    break;
            }

            switch (AppCodename.ToLower())
            {
                case "facebook":
                    AppInfo = $"[FBAN/FBIOS;FBDV/{OSName};FBMD/{DeviceModel};FBSN/iOS;FBSV/{OSVersion};FBSS/3;FBCR/{GetRandomCarrier(Locate).Replace(" ", "")};FBID/phone;FBLC/{Locate};FBOP/5]";
                    break;
                case "instagram":
                    AppVersion = GetRandomItemInList(new List<string>() {
                        "136.0.0.22.123"
                        ,"135.0.0.22.118","131.0.0.21.117"
                        ,"137.0.0.30.124"
                    });
                    AppInfo = $"Instagram {AppVersion} ({OSName}; iOS {OSVersion.Replace(".", "_")}; en_US; en-US; scale=2.00; 750x1334; {GetXRandomNumber(9)})";
                    //Instagram 116.0.0.24.361 (iPhone10,1; iOS 13_1_3; en_US; en-US; scale=2.00; 750x1334; 178418958)
                    break;
                case "snapchat":
                    AppVersion = GetRandomItemInList(new List<string>() {
                        "10.77.5.59","10.77.0.54","10.75.0.67",
                        "10.76.5.67","10.80.5.79",
                        "10.74.1.1"
                    });
                    AppInfo = $"Snapchat/{AppVersion} ({OSName}; iOS {OSVersion}; gzip)";
                    //Snapchat/10.74.0.85 (iPhone10,1; iOS 13.3; gzip)
                    break;
                default:
                    AppVersion = GetRandomItemInList(new List<string>() {
                        "136.0.0.22.123"
                        ,"135.0.0.22.118","131.0.0.21.117"
                        ,"137.0.0.30.124"
                    });

                    AppInfo = $"";
                    UserAgentx = $"Mozilla/5.0 ({DeviceModel}; CPU OS {OSVersion.Replace(".", "_")} like Mac OS X) AppleWebKit/{EngineVersion} (KHTML, like Gecko) Version/{OSVersion} Mobile/15E148";
                    //Mozilla/5.0 (iPad; CPU OS 12_1_4 like Mac OS X) AppleWebKit/605.1.15 (KHTML, like Gecko) Version/12.0 Mobile/15E148 Safari/604.1
                    break;
            }

            if (!string.IsNullOrEmpty(AppInfo))
            {
                UserAgentx = $"Mozilla/5.0 ({DeviceModel}; CPU OS {OSVersion.Replace(".", "_")} like Mac OS X) AppleWebKit/{EngineVersion} (KHTML, like Gecko) Mobile/15E148 {AppInfo}";
                //Mozilla/5.0 (iPad; CPU OS 13_3_1 like Mac OS X) AppleWebKit/605.1.15 (KHTML, like Gecko) Mobile/15E148 [FBAN/FBIOS;FBDV/iPad7,11;FBMD/iPad;FBSN/iOS;FBSV/13.3.1;FBSS/2;FBID/tablet;FBLC/en_US;FBOP/5;FBCR/]
            }
            return UserAgentx;
        }
    }

    public class SamsungPhoneAgent : UserAgent
    {
        public SamsungPhoneAgent(string CodeName, string AppCodename)
        {
            Name = CodeName;
            this.AppCodename = AppCodename;
        }

        public override string GetUserAgent(string locate = "en_US")
        {
            DeviceModel = "Galaxy";
            DeviceVendor = "Samsung";
            OSName = "Android";
            OSVersion = GetRandomItemInList(new List<string>() {
                "9.0",
                "10.0","10.2",
            });
            /*
            G950F (Europe, Global Single-SIM); G950FD (Global Dual-SIM); 
            G950U (USA Unlocked); G950A (AT&T); G950P (Sprint); 
            G950T (T-Mobile); G950V (Verizon); G950R4 (US Cellular); 
            G950W (Canada); G950S/G950K/G950L (South Korea); G9500 (China)
            */
            DeviceType = GetRandomItemInList(new List<string>() {
                /*Galaxy S8*/
                "G950F",//"G950F","G950W","G950S","G950K","G950L",
                /*Galaxy S8+*/
                "G955F",//"G955F","G955U","G955W",
                /*Galaxy S9*/
                "G960F",//"G960F","G960F","G960V","G960A","G960T",
                /*Galaxy S9+*/
                "G965F",
                /*Galaxy Note 9*/
                "N960F",
                /*Galaxy S10*/
                "G970F",//"G970F","G970F","G970U","G977N",
                /*Galaxy S10+*/
                "G975F",
                /*Galaxy Note 10*/
                "N976V",//"N8020",
                /*Galaxy S20*/
                "G988U","G981B"/*S20 5G*/,
                /*Galaxy S20 Ultra 5G*/
                "G9880",//"G988F","G988F","G988U",                
            });
            //DeviceType = "G9880";
            EngineName = "WebKit";
            Locate = locate;

            BrowserVersion = GetRandomItemInList(new List<string>() {
                "603.3.8",
                "604.1", "604.1.15", "604.5.6",
                "605.1", "605.1.15",
            });

            var ChromeVersion = GetRandomItemInList(new List<string>() {
                "82.0.3987", $"83.0.3987.{GetXRandomNumber(2)}",
                "83.0.3945", $"84.0.3745.{GetXRandomNumber(2)}",
                "84.0.3987", $"80.0.3987.{GetXRandomNumber(2)}",
            });

            EngineVersion = BrowserVersion;

            UserAgentx = $"Mozilla/5.0 (Linux; {OSName} {OSVersion}; SM-{DeviceType} Build/{GetRandomString(3, true)}{GetXRandomNumber(2)}{GetRandomString(1, true)}; wv) AppleWebKit/{BrowserVersion} (KHTML, like Gecko) Version/4.0 Chrome/{ChromeVersion} Mobile Safari/{BrowserVersion}";
            //Mozilla/5.0 (Linux; Android 8.0; SM-A520F Build/XYZ12F; wv) AppleWebKit/537.36 (KHTML, like Gecko) Version/4.0 Chrome/66.0.3359.158 Mobile Safari/537.36

            switch (AppCodename.ToLower())
            {
                case "facebook":
                    UserAgentx = $"Mozilla/5.0 (Linux; {OSName} {OSVersion}; SM-{DeviceType} Build/{GetRandomString(1, true)}{GetXRandomNumber(2)}{GetRandomString(2, true)}; wv) AppleWebKit/{BrowserVersion} (KHTML, like Gecko) Version/4.0 Chrome/{ChromeVersion} Mobile Safari/{BrowserVersion} [FB_IAB/FB4A;FBAV/{GetRandomNumber(198, 269)}.{GetRandomNumber(0, 10)}.{GetRandomNumber(0, 10)}.{GetRandomNumber(30, 90)}.{GetRandomNumber(100, 200)};]";
                    //Mozilla/5.0 (Linux; Android 7.0; SM-G610M Build/NRD90M; wv) AppleWebKit/537.36 (KHTML, like Gecko) Version/4.0 Chrome/69.0.3497.100 Mobile Safari/537.36 [FB_IAB/FB4A;FBAV/193.0.0.45.101;]

                    break;
                case "instagram":
                    var cpuInfo = GetRandomItemInList(new List<string>() {
                        "samsungexynos7872","samsungexynos7884","samsungexynos7884A","samsungexynos7885","samsungexynos7904"
                        ,"samsungexynos9609","samsungexynos9610","samsungexynos9611"
                        ,"samsungexynos9800","samsungexynos9810","samsungexynos9820","samsungexynos9825"
                        ,"samsungexynos980","samsungexynos990"
                    });
                    AppVersion = GetRandomItemInList(new List<string>() {
                        "136.0.0.22.123"
                        ,"135.0.0.22.118","131.0.0.21.117"
                        ,"137.0.0.30.124"
                    });
                    //Mozilla/5.0 (Linux; Android 8.0.0; SM-A520F Build/R16NW; wv) AppleWebKit/537.36 (KHTML, like Gecko) Version/4.0 Chrome/66.0.3359.158 Mobile Safari/537.36 Instagram 46.0.0.15.96 Android (26/8.0.0; 480dpi; 1080x1920; samsung; SM-A520F; G960FXXS7DTB5; samsungexynos7880; pt_BR; 109556226)
                    UserAgentx = $"Mozilla/5.0 (Linux; {OSName} {OSVersion}; SM-{DeviceType} Build/{GetRandomString(1, true)}{GetXRandomNumber(2)}{GetRandomString(2, true)}; wv) AppleWebKit/{BrowserVersion} (KHTML, like Gecko) Version/4.0 Chrome/{ChromeVersion} Mobile Safari/{BrowserVersion} Instagram {AppVersion} Android ({GetAPILevel(OSVersion)}/{OSVersion}; 480dpi; 1080x1920; samsung; SM-{DeviceType}; {DeviceType}{GetRandomString(3, true)}{GetRandomNumber(9)}{GetRandomString(3, true)}; {cpuInfo}; pt_BR; 1{GetXRandomNumber(8)})";

                    break;
                case "snapchat":
                    AppVersion = GetRandomItemInList(new List<string>() {
                        "10.77.5.59","10.77.0.54","10.75.0.67",
                        "10.76.5.67","10.80.5.79","10.74.0.85",
                        "10.74.1.1","10.73.0.98"
                    });
                    UserAgentx = $"Mozilla/5.0 (Linux; {OSName} {OSVersion}; SM-{DeviceType} Build/{GetRandomString(3, true)}{GetXRandomNumber(2)}{GetRandomString(1, true)}; wv) AppleWebKit/{BrowserVersion} (KHTML, like Gecko) Version/4.0 Chrome/{ChromeVersion} Mobile Safari/{BrowserVersion} (SM-{DeviceType}; {OSName} {OSVersion}#1{GetXRandomNumber(12)}#{GetAPILevel(OSVersion)}; gzip)";
                    //Mozilla/5.0 (Linux; Android 8.1.0; LG-Q710AL Build/O11019; wv) AppleWebKit/537.36 (KHTML, like Gecko) Version/4.0 Chrome/73.0.3683.90 Mobile Safari/537.36Snapchat10.54.0.31 (LG-Q710AL; Android 8.1.0#1916317118700#27; gzip)

                    break;
                case "samsungbrowser":
                    UserAgentx = $"Mozilla/5.0 (Linux; {OSName} {OSVersion}; SAMSUNG SM-{DeviceType}) AppleWebKit/{BrowserVersion} (KHTML, like Gecko) SamsungBrowser/{OSVersion} Chrome/{BrowserVersion} Mobile Safari/{BrowserVersion}";
                    //"Mozilla/5.0 (Linux;  Android 8.0; SAMSUNG SM-960F) AppleWebKit/537.36 (KHTML, like Gecko) SamsungBrowser/8.0 Chrome/66.0.3359.158 Mobile Safari/537.36"

                    break;
                default:

                    break;
            }

            return UserAgentx;
        }

        string GetAPILevel(string AndroidVersion)
        {
            var ospx = AndroidVersion.Substring(0, 2).ToLower();
            switch (ospx)
            {
                case "8.0":
                    return "26";
                case "8.1":
                    return "27";
                case "9.0":
                    return "28";
                case "10.0":
                    return "29";
                default:
                    return "28";
            }
        }
    }

    public class XiaomiPhoneAgent : UserAgent
    {
        public XiaomiPhoneAgent(string CodeName, string AppCodename)
        {
            Name = CodeName;
            this.AppCodename = AppCodename;
        }

        public override string GetUserAgent(string locate = "en_US")
        {
            DeviceModel = "Remi";
            DeviceVendor = "Xiaomi";
            OSName = "Android";
            OSVersion = GetRandomItemInList(new List<string>() {
                "8.0",
                "9.0",
                "10.0"
            });
            /*
            G950F (Europe, Global Single-SIM); G950FD (Global Dual-SIM); 
            G950U (USA Unlocked); G950A (AT&T); G950P (Sprint); 
            G950T (T-Mobile); G950V (Verizon); G950R4 (US Cellular); 
            G950W (Canada); G950S/G950K/G950L (South Korea); G9500 (China)
            */
            DeviceType = GetRandomItemInList(new List<string>() {
                "Redmi Note 5A","Redmi Note 5A Prime","Redmi Note 5",
                "Redmi Note 6 Pro",
                "Redmi Note 7","Redmi Note 7s","Redmi Note 7 Pro","Redmi Note 8","Redmi Note 8 Pro","Redmi Note 8T",
                "Redmi Note 9S","Redmi Note 9 Pro Max","Redmi Note 9 Pro","Redmi Note 9",
                "Redmi K20","Redmi K20 Pro",
                "Redmi K30","Redmi K30 5G","Redmi K30 5G Racing","Redmi K30 Pro","Redmi K30 Pro Zoom",
                "Mi 8",
                "Mi 9",
                "Mi 10",
            });

            EngineName = "WebKit";
            Locate = locate;

            BrowserVersion = GetRandomItemInList(new List<string>() {
                "604.1", "604.1.15", "604.5.6",
                "605.1", "605.1.15",
            });

            var ChromeVersion = GetRandomItemInList(new List<string>() {                
                "81.0.3865", $"81.0.3965.{GetXRandomNumber(2)}",
                "82.0.3904", $"82.0.3104.{GetXRandomNumber(2)}",
                "83.0.3945", $"83.0.3245.{GetXRandomNumber(2)}",
                "84.0.3987", $"84.0.4987.{GetXRandomNumber(2)}",
            });

            EngineVersion = BrowserVersion;

            AppVersion = GetRandomItemInList(new List<string>() {
                "12.1.1","12.1.2","12.1.3","12.1.4","12.1.5","","12.1.6",
            });

            UserAgentx = $"Mozilla/5.0 (Linux; U; {OSName} {OSVersion}; {Locate.ToLower()}; {DeviceType} Build/PKQ1.1{GetXRandomNumber(5)}.{GetXRandomNumber(3)}) AppleWebKit/{BrowserVersion} (KHTML, like Gecko) Version/4.0 Chrome/{ChromeVersion} Mobile Safari/{BrowserVersion} XiaoMi/MiuiBrowser/{AppVersion}-g";

            switch (AppCodename.ToLower())
            {
                case "facebook":
                    UserAgentx += $" [FB_IAB/FB4A;FBAV/{GetRandomNumber(198, 269)}.{GetRandomNumber(0, 10)}.{GetRandomNumber(0, 10)}.{GetRandomNumber(30, 90)}.{GetRandomNumber(100, 200)};]";
                    //Mozilla/5.0 (Linux; Android 7.0; SM-G610M Build/NRD90M; wv) AppleWebKit/537.36 (KHTML, like Gecko) Version/4.0 Chrome/69.0.3497.100 Mobile Safari/537.36 [FB_IAB/FB4A;FBAV/193.0.0.45.101;]

                    break;
                case "snapchat":
                    AppVersion = GetRandomItemInList(new List<string>() {
                    "10.77.5.59","10.77.0.54","10.75.0.67",
                    "10.76.5.67","10.80.5.79","10.74.0.85",
                    "10.74.1.1","10.73.0.98"
                });
                    UserAgentx += $"({DeviceType}; {OSName} {OSVersion}#1{GetXRandomNumber(12)}#{GetAPILevel(OSVersion)}; gzip)";
                    //Mozilla/5.0 (Linux; Android 8.1.0; LG-Q710AL Build/O11019; wv) AppleWebKit/537.36 (KHTML, like Gecko) Version/4.0 Chrome/73.0.3683.90 Mobile Safari/537.36Snapchat10.54.0.31 (LG-Q710AL; Android 8.1.0#1916317118700#27; gzip)

                    break;
                default:
                    break;
            }

            return UserAgentx;
        }

        string GetAPILevel(string AndroidVersion)
        {
            var ospx = AndroidVersion.Substring(0, 2).ToLower();
            switch (ospx)
            {
                case "8.0":
                    return "26";
                case "8.1":
                    return "27";
                case "9.0":
                    return "28";
                case "10.0":
                    return "29";
                default:
                    return "28";
            }
        }
    }

    public class OppoPhoneAgent : UserAgent
    {
        public OppoPhoneAgent(string CodeName, string AppCodename)
        {
            Name = CodeName;
            this.AppCodename = AppCodename;
        }

        public override string GetUserAgent(string locate = "en_US")
        {
            DeviceModel = "Oppo";
            DeviceVendor = "F11";
            OSName = "Android";
            OSVersion = GetRandomItemInList(new List<string>() {
                "9", "10"
            });
            /*
            G950F (Europe, Global Single-SIM); G950FD (Global Dual-SIM); 
            G950U (USA Unlocked); G950A (AT&T); G950P (Sprint); 
            G950T (T-Mobile); G950V (Verizon); G950R4 (US Cellular); 
            G950W (Canada); G950S/G950K/G950L (South Korea); G9500 (China)
            */
            DeviceType = GetRandomItemInList(new List<string>() {
                /*F11*/
                "CPH1911",
                /*F11 Pro*/
                "CPH1969",
            });

            EngineName = "WebKit";
            Locate = locate;

            BrowserVersion = GetRandomItemInList(new List<string>() {
                "603.3.8",
                "604.1", "604.1.15", "604.5.6",
                "605.1", "605.1.15",
            });

            var ChromeVersion = GetRandomItemInList(new List<string>() {
                "81.0.3865", $"81.0.3965.{GetXRandomNumber(2)}",
                "82.0.3904", $"82.0.3104.{GetXRandomNumber(2)}",
                "83.0.3945", $"83.0.3245.{GetXRandomNumber(2)}",
                "84.0.3987", $"84.0.4987.{GetXRandomNumber(2)}",
            });

            EngineVersion = BrowserVersion;

            AppVersion = GetRandomItemInList(new List<string>() {
                "12.1.1","12.1.2","12.1.3","12.1.4","12.1.5","","12.1.6",
            });

            UserAgentx = $"Mozilla/5.0 (Linux; {OSName} {OSVersion}; {DeviceType}) AppleWebKit/{BrowserVersion} (KHTML, like Gecko) Chrome/{ChromeVersion} Mobile Safari/{BrowserVersion}";
            //Mozilla/5.0 (Linux; Android 9; CPH1911) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/74.0.3729.136 Mobile Safari/537.36
            switch (AppCodename.ToLower())
            {
                case "facebook":
                    UserAgentx += $" [FB_IAB/FB4A;FBAV/{GetRandomNumber(198, 269)}.{GetRandomNumber(0, 10)}.{GetRandomNumber(0, 10)}.{GetRandomNumber(30, 90)}.{GetRandomNumber(100, 200)};]";
                    //Mozilla/5.0 (Linux; Android 7.0; SM-G610M Build/NRD90M; wv) AppleWebKit/537.36 (KHTML, like Gecko) Version/4.0 Chrome/69.0.3497.100 Mobile Safari/537.36 [FB_IAB/FB4A;FBAV/193.0.0.45.101;]

                    break;
                case "snapchat":
                    AppVersion = GetRandomItemInList(new List<string>() {
                    "10.77.5.59","10.77.0.54","10.75.0.67",
                    "10.76.5.67","10.80.5.79","10.74.0.85",
                    "10.74.1.1","10.73.0.98"
                });
                    UserAgentx += $"({DeviceType}; {OSName} {OSVersion}#1{GetXRandomNumber(12)}#{GetAPILevel(OSVersion)}; gzip)";
                    //Mozilla/5.0 (Linux; Android 8.1.0; LG-Q710AL Build/O11019; wv) AppleWebKit/537.36 (KHTML, like Gecko) Version/4.0 Chrome/73.0.3683.90 Mobile Safari/537.36Snapchat10.54.0.31 (LG-Q710AL; Android 8.1.0#1916317118700#27; gzip)

                    break;
                default:
                    break;
            }

            return UserAgentx;
        }

        string GetAPILevel(string AndroidVersion)
        {
            var ospx = AndroidVersion.Substring(0, 2).ToLower();
            switch (ospx)
            {
                case "9":
                    return "28";
                case "10":
                    return "29";
                default:
                    return "28";
            }
        }
    }

    public class MacAgent : UserAgent
    {
        public MacAgent(string CodeName, string AppCodename)
        {
            Name = CodeName;
            this.AppCodename = AppCodename;
        }

        public override string GetUserAgent(string locate = "en_US")
        {
            DeviceModel = "Macintosh";
            DeviceVendor = "Apple";
            OSName = "MAC OS X";
            OSVersion = GetRandomItemInList(new List<string>() {
            "10.13.2","10.13.3","10.13.4","10.13.5","10.13.6",
            "10.14","10.14.1","10.14.2","10.14.3","10.14.4","10.14.5","10.14.6",
            "10.15","10.15.1","10.15.2","10.15.3","10.15.4",
        });

            EngineName = "WebKit";
            Locate = locate;

            BrowserVersion = GetRandomItemInList(new List<string>() {
                "604.1", "604.1.15", "604.5.6",
                "605.1", "605.1.15",
            });

            EngineVersion = BrowserVersion;

            UserAgentx = $"Mozilla/5.0 (Macintosh; Intel Mac OS X {OSVersion.Replace(".", "_")}) AppleWebKit/{BrowserVersion} (KHTML, like Gecko) Version/{OSVersion} Safari/{BrowserVersion}";
            //Mozilla/5.0 (Macintosh; Intel Mac OS X 10_15_2) AppleWebKit/605.1.15 (KHTML, like Gecko) Version/13.0.4 Safari/605.1.15

            return UserAgentx;
        }
    }

    public class WindowsAgent : UserAgent
    {
        public WindowsAgent(string CodeName, string AppCodename)
        {
            Name = CodeName;
            this.AppCodename = AppCodename;
        }

        public override string GetUserAgent(string locate = "en_US")
        {
            DeviceModel = "PC";
            DeviceVendor = "Dell";
            OSName = "Windows 10";
            OSVersion = GetRandomItemInList(new List<string>() {
            "10.13.2","10.13.3","10.13.4","10.13.5","10.13.6",
            "10.14","10.14.1","10.14.2","10.14.3","10.14.4","10.14.5","10.14.6",
            "10.15","10.15.1","10.15.2","10.15.3","10.15.4",
        });

            EngineName = "WebKit";
            Locate = locate;

            BrowserVersion = GetRandomItemInList(new List<string>() {
            "604.1", "604.1.15", "604.5.6",
            "605.1", "605.1.15",
        });

            BrowserName = GetRandomItemInList(new List<string>() {
            "EDGE",
            "Chrome",
        });

            var ChromeVersion = GetRandomItemInList(new List<string>() {
                "81.0.3865", $"81.0.3965.{GetXRandomNumber(2)}",
                "82.0.3904", $"82.0.3104.{GetXRandomNumber(2)}",
                "83.0.3945", $"83.0.3245.{GetXRandomNumber(2)}",
                "84.0.3987", $"84.0.4987.{GetXRandomNumber(2)}",
            });

            EngineVersion = BrowserVersion;

            switch (BrowserName.ToLower())
            {
                case "edge":
                    AppVersion = GetRandomItemInList(new List<string>() {
                    "80.0.361",
                    "81.0.416", "82.0.616", "83.0.518"
                });

                    if (AppVersion.StartsWith("18."))
                    {
                        UserAgentx = $"Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/{BrowserVersion} (KHTML, like Gecko) Chrome/{ChromeVersion} Safari/{BrowserVersion} Edge/{AppVersion}";
                        //Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/70.0.3538.102 Safari/537.36 Edge/18.18363
                    }
                    else
                    {
                        ChromeVersion = GetRandomItemInList(new List<string>() {
                        "82.0.3987",
                        $"82.0.{GetRandomNumber(3, 4)}{GetXRandomNumber(2)}.{GetXRandomNumber(2)}",
                    });
                        UserAgentx = $"Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/{BrowserVersion} (KHTML, like Gecko) Chrome/{ChromeVersion} Safari/{BrowserVersion} Edg/{AppVersion}.{GetXRandomNumber(2)}";
                        //Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/80.0.3987.132 Safari/537.36 Edg/80.0.361.66
                    }
                    break;
                default:
                    UserAgentx = $"Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/{BrowserVersion} (KHTML, like Gecko) Chrome/{ChromeVersion} Safari/{BrowserVersion}";
                    //Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/74.0.3729.169 Safari/537.36
                    break;
            }

            return UserAgentx;
        }
    }
}


/*
map:
      "iPhone"     : "Phone"
      "iPad"       : "Tablet"
      "iPod"       : "Phone"
      "iPod touch" : "Phone"
      "Apple-iPhone"     : "Phone"
      "Apple-iPad"       : "Tablet"
      "Apple-iPod"       : "Phone"
      "Apple-iPod touch" : "Phone"
      "Apple iPhone"     : "Phone"
      "Apple iPad"       : "Tablet"
      "Apple iPod"       : "Phone"
      "Apple iPod touch" : "Phone"
      "Apple iPhone iOS"     : "Phone"
      "Apple iPad iOS"       : "Tablet"
      "Apple iPod iOS"       : "Phone"
      "Apple iPod touch iOS" : "Phone"
      "Watch1,1" : "Watch"
      "Watch1C1" : "Watch"
      "Apple-Watch1C1" : "Watch"
      "Watch1" : "Watch"
      "Watch1_1" : "Watch"
      "Watch1,2" : "Watch"
      "Watch1C2" : "Watch"
      "Apple-Watch1C2" : "Watch"
      "Watch1_2" : "Watch"
      "Watch2,6" : "Watch"
      "Watch2C6" : "Watch"
      "Apple-Watch2C6" : "Watch"
      "Watch2_6" : "Watch"
      "Watch2,7" : "Watch"
      "Watch2C7" : "Watch"
      "Apple-Watch2C7" : "Watch"
      "Watch2_7" : "Watch"
      "Watch2,3" : "Watch"
      "Watch2C3" : "Watch"
      "Apple-Watch2C3" : "Watch"
      "Watch2_3" : "Watch"
      "Watch2,4" : "Watch"
      "Watch2C4" : "Watch"
      "Apple-Watch2C4" : "Watch"
      "Watch2_4" : "Watch"
      "Watch3,1" : "Watch"
      "Watch3C1" : "Watch"
      "Apple-Watch3C1" : "Watch"
      "Watch3" : "Watch"
      "Watch3_1" : "Watch"
      "Watch3,2" : "Watch"
      "Watch3C2" : "Watch"
      "Apple-Watch3C2" : "Watch"
      "Watch3_2" : "Watch"
      "Watch3,3" : "Watch"
      "Watch3C3" : "Watch"
      "Apple-Watch3C3" : "Watch"
      "Watch3_3" : "Watch"
      "Watch3,4" : "Watch"
      "Watch3C4" : "Watch"
      "Apple-Watch3C4" : "Watch"
      "Watch3_4" : "Watch"
      "Watch4,1" : "Watch"
      "Watch4C1" : "Watch"
      "Apple-Watch4C1" : "Watch"
      "Watch4" : "Watch"
      "Watch4_1" : "Watch"
      "Watch4,2" : "Watch"
      "Watch4C2" : "Watch"
      "Apple-Watch4C2" : "Watch"
      "Watch4_2" : "Watch"
      "Watch4,3" : "Watch"
      "Watch4C3" : "Watch"
      "Apple-Watch4C3" : "Watch"
      "Watch4_3" : "Watch"
      "Watch4,4" : "Watch"
      "Watch4C4" : "Watch"
      "Apple-Watch4C4" : "Watch"
      "Watch4_4" : "Watch"
      "Watch5,1" : "Watch"
      "Watch5C1" : "Watch"
      "Apple-Watch5C1" : "Watch"
      "Watch5" : "Watch"
      "Watch5_1" : "Watch"
      "Watch5,2" : "Watch"
      "Watch5C2" : "Watch"
      "Apple-Watch5C2" : "Watch"
      "Watch5_2" : "Watch"
      "Watch5,3" : "Watch"
      "Watch5C3" : "Watch"
      "Apple-Watch5C3" : "Watch"
      "Watch5_3" : "Watch"
      "Watch5,4" : "Watch"
      "Watch5C4" : "Watch"
      "Apple-Watch5C4" : "Watch"
      "Watch5_4" : "Watch"
      "iPhone1,1" : "Phone"
      "iPhone1C1" : "Phone"
      "Apple-iPhone1C1" : "Phone"
      "iPhone1" : "Phone"
      "iPhone1_1" : "Phone"
      "iPhone1,2" : "Phone"
      "iPhone1C2" : "Phone"
      "Apple-iPhone1C2" : "Phone"
      "iPhone1_2" : "Phone"
      "iPhone2,1" : "Phone"
      "iPhone2C1" : "Phone"
      "Apple-iPhone2C1" : "Phone"
      "iPhone2" : "Phone"
      "iPhone2_1" : "Phone"
      "iPhone3,1" : "Phone"
      "iPhone3C1" : "Phone"
      "Apple-iPhone3C1" : "Phone"
      "iPhone3" : "Phone"
      "iPhone3_1" : "Phone"
      "iPhone3,2" : "Phone"
      "iPhone3C2" : "Phone"
      "Apple-iPhone3C2" : "Phone"
      "iPhone3_2" : "Phone"
      "iPhone3,3" : "Phone"
      "iPhone3C3" : "Phone"
      "Apple-iPhone3C3" : "Phone"
      "iPhone3_3" : "Phone"
      "iPhone4,1" : "Phone"
      "iPhone4C1" : "Phone"
      "Apple-iPhone4C1" : "Phone"
      "iPhone4" : "Phone"
      "iPhone4_1" : "Phone"
      "iPhone5,1" : "Phone"
      "iPhone5C1" : "Phone"
      "Apple-iPhone5C1" : "Phone"
      "iPhone5" : "Phone"
      "iPhone5_1" : "Phone"
      "iPhone5,2" : "Phone"
      "iPhone5C2" : "Phone"
      "Apple-iPhone5C2" : "Phone"
      "iPhone5_2" : "Phone"
      "iPhone5,3" : "Phone"
      "iPhone5C3" : "Phone"
      "Apple-iPhone5C3" : "Phone"
      "iPhone5_3" : "Phone"
      "iPhone5,4" : "Phone"
      "iPhone5C4" : "Phone"
      "Apple-iPhone5C4" : "Phone"
      "iPhone5_4" : "Phone"
      "iPhone6,1" : "Phone"
      "iPhone6C1" : "Phone"
      "Apple-iPhone6C1" : "Phone"
      "iPhone6" : "Phone"
      "iPhone6_1" : "Phone"
      "iPhone6,2" : "Phone"
      "iPhone6C2" : "Phone"
      "Apple-iPhone6C2" : "Phone"
      "iPhone6_2" : "Phone"
      "iPhone7,1" : "Phone"
      "iPhone7C1" : "Phone"
      "Apple-iPhone7C1" : "Phone"
      "iPhone7" : "Phone"
      "iPhone7_1" : "Phone"
      "iPhone7,2" : "Phone"
      "iPhone7C2" : "Phone"
      "Apple-iPhone7C2" : "Phone"
      "iPhone7_2" : "Phone"
      "iPhone8,1" : "Phone"
      "iPhone8C1" : "Phone"
      "Apple-iPhone8C1" : "Phone"
      "iPhone8" : "Phone"
      "iPhone8_1" : "Phone"
      "iPhone8,2" : "Phone"
      "iPhone8C2" : "Phone"
      "Apple-iPhone8C2" : "Phone"
      "iPhone8_2" : "Phone"
      "iPhone8,4" : "Phone"
      "iPhone8C4" : "Phone"
      "Apple-iPhone8C4" : "Phone"
      "iPhone8_4" : "Phone"
      "iPhone9,1" : "Phone"
      "iPhone9C1" : "Phone"
      "Apple-iPhone9C1" : "Phone"
      "iPhone9" : "Phone"
      "iPhone9_1" : "Phone"
      "iPhone9,3" : "Phone"
      "iPhone9C3" : "Phone"
      "Apple-iPhone9C3" : "Phone"
      "iPhone9_3" : "Phone"
      "iPhone9,2" : "Phone"
      "iPhone9C2" : "Phone"
      "Apple-iPhone9C2" : "Phone"
      "iPhone9_2" : "Phone"
      "iPhone9,4" : "Phone"
      "iPhone9C4" : "Phone"
      "Apple-iPhone9C4" : "Phone"
      "iPhone9_4" : "Phone"
      "iPhone10,1" : "Phone"
      "iPhone10C1" : "Phone"
      "Apple-iPhone10C1" : "Phone"
      "iPhone10" : "Phone"
      "iPhone10_1" : "Phone"
      "iPhone10,2" : "Phone"
      "iPhone10C2" : "Phone"
      "Apple-iPhone10C2" : "Phone"
      "iPhone10_2" : "Phone"
      "iPhone10,3" : "Phone"
      "iPhone10C3" : "Phone"
      "Apple-iPhone10C3" : "Phone"
      "iPhone10_3" : "Phone"
      "iPhone10,4" : "Phone"
      "iPhone10C4" : "Phone"
      "Apple-iPhone10C4" : "Phone"
      "iPhone10_4" : "Phone"
      "iPhone10,5" : "Phone"
      "iPhone10C5" : "Phone"
      "Apple-iPhone10C5" : "Phone"
      "iPhone10_5" : "Phone"
      "iPhone10,6" : "Phone"
      "iPhone10C6" : "Phone"
      "Apple-iPhone10C6" : "Phone"
      "iPhone10_6" : "Phone"
      "iPhone X" : "Phone"
      "iPhone11,8" : "Phone"
      "iPhone11C8" : "Phone"
      "Apple-iPhone11C8" : "Phone"
      "iPhone11_8" : "Phone"
      "iPhone11,2" : "Phone"
      "iPhone11C2" : "Phone"
      "Apple-iPhone11C2" : "Phone"
      "iPhone11_2" : "Phone"
      "iPhone11,6" : "Phone"
      "iPhone11C6" : "Phone"
      "Apple-iPhone11C6" : "Phone"
      "iPhone11_6" : "Phone"
      "iPhone11,4" : "Phone"
      "iPhone11C4" : "Phone"
      "Apple-iPhone11C4" : "Phone"
      "iPhone11_4" : "Phone"
      "iPhone12,1" : "Phone"
      "iPhone12C1" : "Phone"
      "Apple-iPhone12C1" : "Phone"
      "iPhone12" : "Phone"
      "iPhone12_1" : "Phone"
      "iPhone12,3" : "Phone"
      "iPhone12C3" : "Phone"
      "Apple-iPhone12C3" : "Phone"
      "iPhone12_3" : "Phone"
      "iPhone12,5" : "Phone"
      "iPhone12C5" : "Phone"
      "Apple-iPhone12C5" : "Phone"
      "iPhone12_5" : "Phone"
      "iPad1,1" : "Tablet"
      "iPad1C1" : "Tablet"
      "Apple-iPad1C1" : "Tablet"
      "iPad1" : "Tablet"
      "iPad1_1" : "Tablet"
      "iPad2,1" : "Tablet"
      "iPad2C1" : "Tablet"
      "Apple-iPad2C1" : "Tablet"
      "iPad2" : "Tablet"
      "iPad2_1" : "Tablet"
      "iPad2,2" : "Tablet"
      "iPad2C2" : "Tablet"
      "Apple-iPad2C2" : "Tablet"
      "iPad2_2" : "Tablet"
      "iPad2,3" : "Tablet"
      "iPad2C3" : "Tablet"
      "Apple-iPad2C3" : "Tablet"
      "iPad2_3" : "Tablet"
      "iPad2,4" : "Tablet"
      "iPad2C4" : "Tablet"
      "Apple-iPad2C4" : "Tablet"
      "iPad2_4" : "Tablet"
      "iPad2,5" : "Tablet"
      "iPad2C5" : "Tablet"
      "Apple-iPad2C5" : "Tablet"
      "iPad2_5" : "Tablet"
      "iPad2,6" : "Tablet"
      "iPad2C6" : "Tablet"
      "Apple-iPad2C6" : "Tablet"
      "iPad2_6" : "Tablet"
      "iPad2,7" : "Tablet"
      "iPad2C7" : "Tablet"
      "Apple-iPad2C7" : "Tablet"
      "iPad2_7" : "Tablet"
      "iPad3,1" : "Tablet"
      "iPad3C1" : "Tablet"
      "Apple-iPad3C1" : "Tablet"
      "iPad3" : "Tablet"
      "iPad3_1" : "Tablet"
      "iPad3,2" : "Tablet"
      "iPad3C2" : "Tablet"
      "Apple-iPad3C2" : "Tablet"
      "iPad3_2" : "Tablet"
      "iPad3,3" : "Tablet"
      "iPad3C3" : "Tablet"
      "Apple-iPad3C3" : "Tablet"
      "iPad3_3" : "Tablet"
      "iPad3,4" : "Tablet"
      "iPad3C4" : "Tablet"
      "Apple-iPad3C4" : "Tablet"
      "iPad3_4" : "Tablet"
      "iPad3,5" : "Tablet"
      "iPad3C5" : "Tablet"
      "Apple-iPad3C5" : "Tablet"
      "iPad3_5" : "Tablet"
      "iPad3,6" : "Tablet"
      "iPad3C6" : "Tablet"
      "Apple-iPad3C6" : "Tablet"
      "iPad3_6" : "Tablet"
      "iPad4,1" : "Tablet"
      "iPad4C1" : "Tablet"
      "Apple-iPad4C1" : "Tablet"
      "iPad4" : "Tablet"
      "iPad4_1" : "Tablet"
      "iPad4,2" : "Tablet"
      "iPad4C2" : "Tablet"
      "Apple-iPad4C2" : "Tablet"
      "iPad4_2" : "Tablet"
      "iPad4,3" : "Tablet"
      "iPad4C3" : "Tablet"
      "Apple-iPad4C3" : "Tablet"
      "iPad4_3" : "Tablet"
      "iPad4,4" : "Tablet"
      "iPad4C4" : "Tablet"
      "Apple-iPad4C4" : "Tablet"
      "iPad4_4" : "Tablet"
      "iPad4,5" : "Tablet"
      "iPad4C5" : "Tablet"
      "Apple-iPad4C5" : "Tablet"
      "iPad4_5" : "Tablet"
      "iPad4,6" : "Tablet"
      "iPad4C6" : "Tablet"
      "Apple-iPad4C6" : "Tablet"
      "iPad4_6" : "Tablet"
      "iPad4,7" : "Tablet"
      "iPad4C7" : "Tablet"
      "Apple-iPad4C7" : "Tablet"
      "iPad4_7" : "Tablet"
      "iPad4,8" : "Tablet"
      "iPad4C8" : "Tablet"
      "Apple-iPad4C8" : "Tablet"
      "iPad4_8" : "Tablet"
      "iPad4,9" : "Tablet"
      "iPad4C9" : "Tablet"
      "Apple-iPad4C9" : "Tablet"
      "iPad4_9" : "Tablet"
      "iPad5,1" : "Tablet"
      "iPad5C1" : "Tablet"
      "Apple-iPad5C1" : "Tablet"
      "iPad5" : "Tablet"
      "iPad5_1" : "Tablet"
      "iPad5,2" : "Tablet"
      "iPad5C2" : "Tablet"
      "Apple-iPad5C2" : "Tablet"
      "iPad5_2" : "Tablet"
      "iPad5,3" : "Tablet"
      "iPad5C3" : "Tablet"
      "Apple-iPad5C3" : "Tablet"
      "iPad5_3" : "Tablet"
      "iPad5,4" : "Tablet"
      "iPad5C4" : "Tablet"
      "Apple-iPad5C4" : "Tablet"
      "iPad5_4" : "Tablet"
      "iPad6,3" : "Tablet"
      "iPad6C3" : "Tablet"
      "Apple-iPad6C3" : "Tablet"
      "iPad6_3" : "Tablet"
      "iPad6,4" : "Tablet"
      "iPad6C4" : "Tablet"
      "Apple-iPad6C4" : "Tablet"
      "iPad6_4" : "Tablet"
      "iPad6,7" : "Tablet"
      "iPad6C7" : "Tablet"
      "Apple-iPad6C7" : "Tablet"
      "iPad6_7" : "Tablet"
      "iPad6,8" : "Tablet"
      "iPad6C8" : "Tablet"
      "Apple-iPad6C8" : "Tablet"
      "iPad6_8" : "Tablet"
      "iPad6,11" : "Tablet"
      "iPad6C11" : "Tablet"
      "Apple-iPad6C11" : "Tablet"
      "iPad61" : "Tablet"
      "iPad6_11" : "Tablet"
      "iPad6,12" : "Tablet"
      "iPad6C12" : "Tablet"
      "Apple-iPad6C12" : "Tablet"
      "iPad62" : "Tablet"
      "iPad6_12" : "Tablet"
      "iPad7,1" : "Tablet"
      "iPad7C1" : "Tablet"
      "Apple-iPad7C1" : "Tablet"
      "iPad7" : "Tablet"
      "iPad7_1" : "Tablet"
      "iPad7,2" : "Tablet"
      "iPad7C2" : "Tablet"
      "Apple-iPad7C2" : "Tablet"
      "iPad7_2" : "Tablet"
      "iPad7,3" : "Tablet"
      "iPad7C3" : "Tablet"
      "Apple-iPad7C3" : "Tablet"
      "iPad7_3" : "Tablet"
      "iPad7,4" : "Tablet"
      "iPad7C4" : "Tablet"
      "Apple-iPad7C4" : "Tablet"
      "iPad7_4" : "Tablet"
      "iPad7,5" : "Tablet"
      "iPad7C5" : "Tablet"
      "Apple-iPad7C5" : "Tablet"
      "iPad7_5" : "Tablet"
      "iPad7,6" : "Tablet"
      "iPad7C6" : "Tablet"
      "Apple-iPad7C6" : "Tablet"
      "iPad7_6" : "Tablet"
      "iPad7,11" : "Tablet"
      "iPad7C11" : "Tablet"
      "Apple-iPad7C11" : "Tablet"
      "iPad71" : "Tablet"
      "iPad7_11" : "Tablet"
      "iPad7,12" : "Tablet"
      "iPad7C12" : "Tablet"
      "Apple-iPad7C12" : "Tablet"
      "iPad72" : "Tablet"
      "iPad7_12" : "Tablet"
      "iPad8,1" : "Tablet"
      "iPad8C1" : "Tablet"
      "Apple-iPad8C1" : "Tablet"
      "iPad8" : "Tablet"
      "iPad8_1" : "Tablet"
      "iPad8,2" : "Tablet"
      "iPad8C2" : "Tablet"
      "Apple-iPad8C2" : "Tablet"
      "iPad8_2" : "Tablet"
      "iPad8,3" : "Tablet"
      "iPad8C3" : "Tablet"
      "Apple-iPad8C3" : "Tablet"
      "iPad8_3" : "Tablet"
      "iPad8,4" : "Tablet"
      "iPad8C4" : "Tablet"
      "Apple-iPad8C4" : "Tablet"
      "iPad8_4" : "Tablet"
      "iPad8,5" : "Tablet"
      "iPad8C5" : "Tablet"
      "Apple-iPad8C5" : "Tablet"
      "iPad8_5" : "Tablet"
      "iPad8,6" : "Tablet"
      "iPad8C6" : "Tablet"
      "Apple-iPad8C6" : "Tablet"
      "iPad8_6" : "Tablet"
      "iPad8,7" : "Tablet"
      "iPad8C7" : "Tablet"
      "Apple-iPad8C7" : "Tablet"
      "iPad8_7" : "Tablet"
      "iPad8,8" : "Tablet"
      "iPad8C8" : "Tablet"
      "Apple-iPad8C8" : "Tablet"
      "iPad8_8" : "Tablet"
      "iPad11,1" : "Tablet"
      "iPad11C1" : "Tablet"
      "Apple-iPad11C1" : "Tablet"
      "iPad11" : "Tablet"
      "iPad11_1" : "Tablet"
      "iPad11,2" : "Tablet"
      "iPad11C2" : "Tablet"
      "Apple-iPad11C2" : "Tablet"
      "iPad11_2" : "Tablet"
      "iPad11,3" : "Tablet"
      "iPad11C3" : "Tablet"
      "Apple-iPad11C3" : "Tablet"
      "iPad11_3" : "Tablet"
      "iPad11,4" : "Tablet"
      "iPad11C4" : "Tablet"
      "Apple-iPad11C4" : "Tablet"
      "iPad11_4" : "Tablet"
      "iPod1,1" : "Phone"
      "iPod1C1" : "Phone"
      "Apple-iPod1C1" : "Phone"
      "iPod1" : "Phone"
      "iPod1_1" : "Phone"
      "iPod2,1" : "Phone"
      "iPod2C1" : "Phone"
      "Apple-iPod2C1" : "Phone"
      "iPod2" : "Phone"
      "iPod2_1" : "Phone"
      "iPod3,1" : "Phone"
      "iPod3C1" : "Phone"
      "Apple-iPod3C1" : "Phone"
      "iPod3" : "Phone"
      "iPod3_1" : "Phone"
      "iPod4,1" : "Phone"
      "iPod4C1" : "Phone"
      "Apple-iPod4C1" : "Phone"
      "iPod4" : "Phone"
      "iPod4_1" : "Phone"
      "iPod5,1" : "Phone"
      "iPod5C1" : "Phone"
      "Apple-iPod5C1" : "Phone"
      "iPod5" : "Phone"
      "iPod5_1" : "Phone"
      "iPod7,1" : "Phone"
      "iPod7C1" : "Phone"
      "Apple-iPod7C1" : "Phone"
      "iPod7" : "Phone"
      "iPod7_1" : "Phone"
      "iPod9,1" : "Phone"
      "iPod9C1" : "Phone"
      "Apple-iPod9C1" : "Phone"
      "iPod9" : "Phone"
      "iPod9_1" : "Phone"

- lookup:
    name: 'AppleDeviceName'
    map:
      "iPhone"     : "Apple iPhone"
      "iPad"       : "Apple iPad"
      "iPod"       : "Apple iPod"
      "iPod touch" : "Apple iPod touch"
      "Apple-iPhone"     : "Apple iPhone"
      "Apple-iPad"       : "Apple iPad"
      "Apple-iPod"       : "Apple iPod"
      "Apple-iPod touch" : "Apple iPod touch"
      "Apple iPhone"     : "Apple iPhone"
      "Apple iPad"       : "Apple iPad"
      "Apple iPod"       : "Apple iPod"
      "Apple iPod touch" : "Apple iPod touch"
      "Apple iPhone iOS"     : "Apple iPhone"
      "Apple iPad iOS"       : "Apple iPad"
      "Apple iPod iOS"       : "Apple iPod"
      "Apple iPod touch iOS" : "Apple iPod touch"
      "Watch1,1" : "Apple Watch"
      "Watch1C1" : "Apple Watch"
      "Apple-Watch1C1" : "Apple Watch"
      "Watch1" : "Apple Watch"
      "Watch1_1" : "Apple Watch"
      "Watch1,2" : "Apple Watch"
      "Watch1C2" : "Apple Watch"
      "Apple-Watch1C2" : "Apple Watch"
      "Watch1_2" : "Apple Watch"
      "Watch2,6" : "Apple Watch"
      "Watch2C6" : "Apple Watch"
      "Apple-Watch2C6" : "Apple Watch"
      "Watch2_6" : "Apple Watch"
      "Watch2,7" : "Apple Watch"
      "Watch2C7" : "Apple Watch"
      "Apple-Watch2C7" : "Apple Watch"
      "Watch2_7" : "Apple Watch"
      "Watch2,3" : "Apple Watch"
      "Watch2C3" : "Apple Watch"
      "Apple-Watch2C3" : "Apple Watch"
      "Watch2_3" : "Apple Watch"
      "Watch2,4" : "Apple Watch"
      "Watch2C4" : "Apple Watch"
      "Apple-Watch2C4" : "Apple Watch"
      "Watch2_4" : "Apple Watch"
      "Watch3,1" : "Apple Watch"
      "Watch3C1" : "Apple Watch"
      "Apple-Watch3C1" : "Apple Watch"
      "Watch3" : "Apple Watch"
      "Watch3_1" : "Apple Watch"
      "Watch3,2" : "Apple Watch"
      "Watch3C2" : "Apple Watch"
      "Apple-Watch3C2" : "Apple Watch"
      "Watch3_2" : "Apple Watch"
      "Watch3,3" : "Apple Watch"
      "Watch3C3" : "Apple Watch"
      "Apple-Watch3C3" : "Apple Watch"
      "Watch3_3" : "Apple Watch"
      "Watch3,4" : "Apple Watch"
      "Watch3C4" : "Apple Watch"
      "Apple-Watch3C4" : "Apple Watch"
      "Watch3_4" : "Apple Watch"
      "Watch4,1" : "Apple Watch"
      "Watch4C1" : "Apple Watch"
      "Apple-Watch4C1" : "Apple Watch"
      "Watch4" : "Apple Watch"
      "Watch4_1" : "Apple Watch"
      "Watch4,2" : "Apple Watch"
      "Watch4C2" : "Apple Watch"
      "Apple-Watch4C2" : "Apple Watch"
      "Watch4_2" : "Apple Watch"
      "Watch4,3" : "Apple Watch"
      "Watch4C3" : "Apple Watch"
      "Apple-Watch4C3" : "Apple Watch"
      "Watch4_3" : "Apple Watch"
      "Watch4,4" : "Apple Watch"
      "Watch4C4" : "Apple Watch"
      "Apple-Watch4C4" : "Apple Watch"
      "Watch4_4" : "Apple Watch"
      "Watch5,1" : "Apple Watch"
      "Watch5C1" : "Apple Watch"
      "Apple-Watch5C1" : "Apple Watch"
      "Watch5" : "Apple Watch"
      "Watch5_1" : "Apple Watch"
      "Watch5,2" : "Apple Watch"
      "Watch5C2" : "Apple Watch"
      "Apple-Watch5C2" : "Apple Watch"
      "Watch5_2" : "Apple Watch"
      "Watch5,3" : "Apple Watch"
      "Watch5C3" : "Apple Watch"
      "Apple-Watch5C3" : "Apple Watch"
      "Watch5_3" : "Apple Watch"
      "Watch5,4" : "Apple Watch"
      "Watch5C4" : "Apple Watch"
      "Apple-Watch5C4" : "Apple Watch"
      "Watch5_4" : "Apple Watch"
      "iPhone1,1" : "iPhone"
      "iPhone1C1" : "iPhone"
      "Apple-iPhone1C1" : "iPhone"
      "iPhone1" : "iPhone"
      "iPhone1_1" : "iPhone"
      "iPhone1,2" : "iPhone"
      "iPhone1C2" : "iPhone"
      "Apple-iPhone1C2" : "iPhone"
      "iPhone1_2" : "iPhone"
      "iPhone2,1" : "iPhone"
      "iPhone2C1" : "iPhone"
      "Apple-iPhone2C1" : "iPhone"
      "iPhone2" : "iPhone"
      "iPhone2_1" : "iPhone"
      "iPhone3,1" : "iPhone"
      "iPhone3C1" : "iPhone"
      "Apple-iPhone3C1" : "iPhone"
      "iPhone3" : "iPhone"
      "iPhone3_1" : "iPhone"
      "iPhone3,2" : "iPhone"
      "iPhone3C2" : "iPhone"
      "Apple-iPhone3C2" : "iPhone"
      "iPhone3_2" : "iPhone"
      "iPhone3,3" : "iPhone"
      "iPhone3C3" : "iPhone"
      "Apple-iPhone3C3" : "iPhone"
      "iPhone3_3" : "iPhone"
      "iPhone4,1" : "iPhone"
      "iPhone4C1" : "iPhone"
      "Apple-iPhone4C1" : "iPhone"
      "iPhone4" : "iPhone"
      "iPhone4_1" : "iPhone"
      "iPhone5,1" : "iPhone"
      "iPhone5C1" : "iPhone"
      "Apple-iPhone5C1" : "iPhone"
      "iPhone5" : "iPhone"
      "iPhone5_1" : "iPhone"
      "iPhone5,2" : "iPhone"
      "iPhone5C2" : "iPhone"
      "Apple-iPhone5C2" : "iPhone"
      "iPhone5_2" : "iPhone"
      "iPhone5,3" : "iPhone"
      "iPhone5C3" : "iPhone"
      "Apple-iPhone5C3" : "iPhone"
      "iPhone5_3" : "iPhone"
      "iPhone5,4" : "iPhone"
      "iPhone5C4" : "iPhone"
      "Apple-iPhone5C4" : "iPhone"
      "iPhone5_4" : "iPhone"
      "iPhone6,1" : "iPhone"
      "iPhone6C1" : "iPhone"
      "Apple-iPhone6C1" : "iPhone"
      "iPhone6" : "iPhone"
      "iPhone6_1" : "iPhone"
      "iPhone6,2" : "iPhone"
      "iPhone6C2" : "iPhone"
      "Apple-iPhone6C2" : "iPhone"
      "iPhone6_2" : "iPhone"
      "iPhone7,1" : "iPhone"
      "iPhone7C1" : "iPhone"
      "Apple-iPhone7C1" : "iPhone"
      "iPhone7" : "iPhone"
      "iPhone7_1" : "iPhone"
      "iPhone7,2" : "iPhone"
      "iPhone7C2" : "iPhone"
      "Apple-iPhone7C2" : "iPhone"
      "iPhone7_2" : "iPhone"
      "iPhone8,1" : "iPhone"
      "iPhone8C1" : "iPhone"
      "Apple-iPhone8C1" : "iPhone"
      "iPhone8" : "iPhone"
      "iPhone8_1" : "iPhone"
      "iPhone8,2" : "iPhone"
      "iPhone8C2" : "iPhone"
      "Apple-iPhone8C2" : "iPhone"
      "iPhone8_2" : "iPhone"
      "iPhone8,4" : "iPhone"
      "iPhone8C4" : "iPhone"
      "Apple-iPhone8C4" : "iPhone"
      "iPhone8_4" : "iPhone"
      "iPhone9,1" : "iPhone"
      "iPhone9C1" : "iPhone"
      "Apple-iPhone9C1" : "iPhone"
      "iPhone9" : "iPhone"
      "iPhone9_1" : "iPhone"
      "iPhone9,3" : "iPhone"
      "iPhone9C3" : "iPhone"
      "Apple-iPhone9C3" : "iPhone"
      "iPhone9_3" : "iPhone"
      "iPhone9,2" : "iPhone"
      "iPhone9C2" : "iPhone"
      "Apple-iPhone9C2" : "iPhone"
      "iPhone9_2" : "iPhone"
      "iPhone9,4" : "iPhone"
      "iPhone9C4" : "iPhone"
      "Apple-iPhone9C4" : "iPhone"
      "iPhone9_4" : "iPhone"
      "iPhone10,1" : "iPhone"
      "iPhone10C1" : "iPhone"
      "Apple-iPhone10C1" : "iPhone"
      "iPhone10" : "iPhone"
      "iPhone10_1" : "iPhone"
      "iPhone10,2" : "iPhone"
      "iPhone10C2" : "iPhone"
      "Apple-iPhone10C2" : "iPhone"
      "iPhone10_2" : "iPhone"
      "iPhone10,3" : "iPhone"
      "iPhone10C3" : "iPhone"
      "Apple-iPhone10C3" : "iPhone"
      "iPhone10_3" : "iPhone"
      "iPhone10,4" : "iPhone"
      "iPhone10C4" : "iPhone"
      "Apple-iPhone10C4" : "iPhone"
      "iPhone10_4" : "iPhone"
      "iPhone10,5" : "iPhone"
      "iPhone10C5" : "iPhone"
      "Apple-iPhone10C5" : "iPhone"
      "iPhone10_5" : "iPhone"
      "iPhone10,6" : "iPhone"
      "iPhone10C6" : "iPhone"
      "Apple-iPhone10C6" : "iPhone"
      "iPhone10_6" : "iPhone"
      "iPhone X" : "iPhone"
      "iPhone11,8" : "iPhone"
      "iPhone11C8" : "iPhone"
      "Apple-iPhone11C8" : "iPhone"
      "iPhone11_8" : "iPhone"
      "iPhone11,2" : "iPhone"
      "iPhone11C2" : "iPhone"
      "Apple-iPhone11C2" : "iPhone"
      "iPhone11_2" : "iPhone"
      "iPhone11,6" : "iPhone"
      "iPhone11C6" : "iPhone"
      "Apple-iPhone11C6" : "iPhone"
      "iPhone11_6" : "iPhone"
      "iPhone11,4" : "iPhone"
      "iPhone11C4" : "iPhone"
      "Apple-iPhone11C4" : "iPhone"
      "iPhone11_4" : "iPhone"
      "iPhone12,1" : "iPhone"
      "iPhone12C1" : "iPhone"
      "Apple-iPhone12C1" : "iPhone"
      "iPhone12" : "iPhone"
      "iPhone12_1" : "iPhone"
      "iPhone12,3" : "iPhone"
      "iPhone12C3" : "iPhone"
      "Apple-iPhone12C3" : "iPhone"
      "iPhone12_3" : "iPhone"
      "iPhone12,5" : "iPhone"
      "iPhone12C5" : "iPhone"
      "Apple-iPhone12C5" : "iPhone"
      "iPhone12_5" : "iPhone"
      "iPad1,1" : "iPad"
      "iPad1C1" : "iPad"
      "Apple-iPad1C1" : "iPad"
      "iPad1" : "iPad"
      "iPad1_1" : "iPad"
      "iPad2,1" : "iPad"
      "iPad2C1" : "iPad"
      "Apple-iPad2C1" : "iPad"
      "iPad2" : "iPad"
      "iPad2_1" : "iPad"
      "iPad2,2" : "iPad"
      "iPad2C2" : "iPad"
      "Apple-iPad2C2" : "iPad"
      "iPad2_2" : "iPad"
      "iPad2,3" : "iPad"
      "iPad2C3" : "iPad"
      "Apple-iPad2C3" : "iPad"
      "iPad2_3" : "iPad"
      "iPad2,4" : "iPad"
      "iPad2C4" : "iPad"
      "Apple-iPad2C4" : "iPad"
      "iPad2_4" : "iPad"
      "iPad2,5" : "iPad"
      "iPad2C5" : "iPad"
      "Apple-iPad2C5" : "iPad"
      "iPad2_5" : "iPad"
      "iPad2,6" : "iPad"
      "iPad2C6" : "iPad"
      "Apple-iPad2C6" : "iPad"
      "iPad2_6" : "iPad"
      "iPad2,7" : "iPad"
      "iPad2C7" : "iPad"
      "Apple-iPad2C7" : "iPad"
      "iPad2_7" : "iPad"
      "iPad3,1" : "iPad"
      "iPad3C1" : "iPad"
      "Apple-iPad3C1" : "iPad"
      "iPad3" : "iPad"
      "iPad3_1" : "iPad"
      "iPad3,2" : "iPad"
      "iPad3C2" : "iPad"
      "Apple-iPad3C2" : "iPad"
      "iPad3_2" : "iPad"
      "iPad3,3" : "iPad"
      "iPad3C3" : "iPad"
      "Apple-iPad3C3" : "iPad"
      "iPad3_3" : "iPad"
      "iPad3,4" : "iPad"
      "iPad3C4" : "iPad"
      "Apple-iPad3C4" : "iPad"
      "iPad3_4" : "iPad"
      "iPad3,5" : "iPad"
      "iPad3C5" : "iPad"
      "Apple-iPad3C5" : "iPad"
      "iPad3_5" : "iPad"
      "iPad3,6" : "iPad"
      "iPad3C6" : "iPad"
      "Apple-iPad3C6" : "iPad"
      "iPad3_6" : "iPad"
      "iPad4,1" : "iPad"
      "iPad4C1" : "iPad"
      "Apple-iPad4C1" : "iPad"
      "iPad4" : "iPad"
      "iPad4_1" : "iPad"
      "iPad4,2" : "iPad"
      "iPad4C2" : "iPad"
      "Apple-iPad4C2" : "iPad"
      "iPad4_2" : "iPad"
      "iPad4,3" : "iPad"
      "iPad4C3" : "iPad"
      "Apple-iPad4C3" : "iPad"
      "iPad4_3" : "iPad"
      "iPad4,4" : "iPad"
      "iPad4C4" : "iPad"
      "Apple-iPad4C4" : "iPad"
      "iPad4_4" : "iPad"
      "iPad4,5" : "iPad"
      "iPad4C5" : "iPad"
      "Apple-iPad4C5" : "iPad"
      "iPad4_5" : "iPad"
      "iPad4,6" : "iPad"
      "iPad4C6" : "iPad"
      "Apple-iPad4C6" : "iPad"
      "iPad4_6" : "iPad"
      "iPad4,7" : "iPad"
      "iPad4C7" : "iPad"
      "Apple-iPad4C7" : "iPad"
      "iPad4_7" : "iPad"
      "iPad4,8" : "iPad"
      "iPad4C8" : "iPad"
      "Apple-iPad4C8" : "iPad"
      "iPad4_8" : "iPad"
      "iPad4,9" : "iPad"
      "iPad4C9" : "iPad"
      "Apple-iPad4C9" : "iPad"
      "iPad4_9" : "iPad"
      "iPad5,1" : "iPad"
      "iPad5C1" : "iPad"
      "Apple-iPad5C1" : "iPad"
      "iPad5" : "iPad"
      "iPad5_1" : "iPad"
      "iPad5,2" : "iPad"
      "iPad5C2" : "iPad"
      "Apple-iPad5C2" : "iPad"
      "iPad5_2" : "iPad"
      "iPad5,3" : "iPad"
      "iPad5C3" : "iPad"
      "Apple-iPad5C3" : "iPad"
      "iPad5_3" : "iPad"
      "iPad5,4" : "iPad"
      "iPad5C4" : "iPad"
      "Apple-iPad5C4" : "iPad"
      "iPad5_4" : "iPad"
      "iPad6,3" : "iPad"
      "iPad6C3" : "iPad"
      "Apple-iPad6C3" : "iPad"
      "iPad6_3" : "iPad"
      "iPad6,4" : "iPad"
      "iPad6C4" : "iPad"
      "Apple-iPad6C4" : "iPad"
      "iPad6_4" : "iPad"
      "iPad6,7" : "iPad"
      "iPad6C7" : "iPad"
      "Apple-iPad6C7" : "iPad"
      "iPad6_7" : "iPad"
      "iPad6,8" : "iPad"
      "iPad6C8" : "iPad"
      "Apple-iPad6C8" : "iPad"
      "iPad6_8" : "iPad"
      "iPad6,11" : "iPad"
      "iPad6C11" : "iPad"
      "Apple-iPad6C11" : "iPad"
      "iPad61" : "iPad"
      "iPad6_11" : "iPad"
      "iPad6,12" : "iPad"
      "iPad6C12" : "iPad"
      "Apple-iPad6C12" : "iPad"
      "iPad62" : "iPad"
      "iPad6_12" : "iPad"
      "iPad7,1" : "iPad"
      "iPad7C1" : "iPad"
      "Apple-iPad7C1" : "iPad"
      "iPad7" : "iPad"
      "iPad7_1" : "iPad"
      "iPad7,2" : "iPad"
      "iPad7C2" : "iPad"
      "Apple-iPad7C2" : "iPad"
      "iPad7_2" : "iPad"
      "iPad7,3" : "iPad"
      "iPad7C3" : "iPad"
      "Apple-iPad7C3" : "iPad"
      "iPad7_3" : "iPad"
      "iPad7,4" : "iPad"
      "iPad7C4" : "iPad"
      "Apple-iPad7C4" : "iPad"
      "iPad7_4" : "iPad"
      "iPad7,5" : "iPad"
      "iPad7C5" : "iPad"
      "Apple-iPad7C5" : "iPad"
      "iPad7_5" : "iPad"
      "iPad7,6" : "iPad"
      "iPad7C6" : "iPad"
      "Apple-iPad7C6" : "iPad"
      "iPad7_6" : "iPad"
      "iPad7,11" : "iPad"
      "iPad7C11" : "iPad"
      "Apple-iPad7C11" : "iPad"
      "iPad71" : "iPad"
      "iPad7_11" : "iPad"
      "iPad7,12" : "iPad"
      "iPad7C12" : "iPad"
      "Apple-iPad7C12" : "iPad"
      "iPad72" : "iPad"
      "iPad7_12" : "iPad"
      "iPad8,1" : "iPad"
      "iPad8C1" : "iPad"
      "Apple-iPad8C1" : "iPad"
      "iPad8" : "iPad"
      "iPad8_1" : "iPad"
      "iPad8,2" : "iPad"
      "iPad8C2" : "iPad"
      "Apple-iPad8C2" : "iPad"
      "iPad8_2" : "iPad"
      "iPad8,3" : "iPad"
      "iPad8C3" : "iPad"
      "Apple-iPad8C3" : "iPad"
      "iPad8_3" : "iPad"
      "iPad8,4" : "iPad"
      "iPad8C4" : "iPad"
      "Apple-iPad8C4" : "iPad"
      "iPad8_4" : "iPad"
      "iPad8,5" : "iPad"
      "iPad8C5" : "iPad"
      "Apple-iPad8C5" : "iPad"
      "iPad8_5" : "iPad"
      "iPad8,6" : "iPad"
      "iPad8C6" : "iPad"
      "Apple-iPad8C6" : "iPad"
      "iPad8_6" : "iPad"
      "iPad8,7" : "iPad"
      "iPad8C7" : "iPad"
      "Apple-iPad8C7" : "iPad"
      "iPad8_7" : "iPad"
      "iPad8,8" : "iPad"
      "iPad8C8" : "iPad"
      "Apple-iPad8C8" : "iPad"
      "iPad8_8" : "iPad"
      "iPad11,1" : "iPad"
      "iPad11C1" : "iPad"
      "Apple-iPad11C1" : "iPad"
      "iPad11" : "iPad"
      "iPad11_1" : "iPad"
      "iPad11,2" : "iPad"
      "iPad11C2" : "iPad"
      "Apple-iPad11C2" : "iPad"
      "iPad11_2" : "iPad"
      "iPad11,3" : "iPad"
      "iPad11C3" : "iPad"
      "Apple-iPad11C3" : "iPad"
      "iPad11_3" : "iPad"
      "iPad11,4" : "iPad"
      "iPad11C4" : "iPad"
      "Apple-iPad11C4" : "iPad"
      "iPad11_4" : "iPad"
      "iPod1,1" : "iPod touch"
      "iPod1C1" : "iPod touch"
      "Apple-iPod1C1" : "iPod touch"
      "iPod1" : "iPod touch"
      "iPod1_1" : "iPod touch"
      "iPod2,1" : "iPod touch"
      "iPod2C1" : "iPod touch"
      "Apple-iPod2C1" : "iPod touch"
      "iPod2" : "iPod touch"
      "iPod2_1" : "iPod touch"
      "iPod3,1" : "iPod touch"
      "iPod3C1" : "iPod touch"
      "Apple-iPod3C1" : "iPod touch"
      "iPod3" : "iPod touch"
      "iPod3_1" : "iPod touch"
      "iPod4,1" : "iPod touch"
      "iPod4C1" : "iPod touch"
      "Apple-iPod4C1" : "iPod touch"
      "iPod4" : "iPod touch"
      "iPod4_1" : "iPod touch"
      "iPod5,1" : "iPod touch"
      "iPod5C1" : "iPod touch"
      "Apple-iPod5C1" : "iPod touch"
      "iPod5" : "iPod touch"
      "iPod5_1" : "iPod touch"
      "iPod7,1" : "iPod touch"
      "iPod7C1" : "iPod touch"
      "Apple-iPod7C1" : "iPod touch"
      "iPod7" : "iPod touch"
      "iPod7_1" : "iPod touch"
      "iPod9,1" : "iPod touch"
      "iPod9C1" : "iPod touch"
      "Apple-iPod9C1" : "iPod touch"
      "iPod9" : "iPod touch"
      "iPod9_1" : "iPod touch"

- lookup:
    name: 'AppleDeviceVersion'
    map:
      "iPhone"     : "iPhone"
      "iPad"       : "iPad"
      "iPod"       : "iPod"
      "iPod touch" : "iPod touch"
      "Apple-iPhone"     : "iPhone"
      "Apple-iPad"       : "iPad"
      "Apple-iPod"       : "iPod"
      "Apple-iPod touch" : "iPod touch"
      "Apple iPhone"     : "iPhone"
      "Apple iPad"       : "iPad"
      "Apple iPod"       : "iPod"
      "Apple iPod touch" : "iPod touch"
      "Apple iPhone iOS"     : "iPhone"
      "Apple iPad iOS"       : "iPad"
      "Apple iPod iOS"       : "iPod"
      "Apple iPod touch iOS" : "iPod touch"
      "Watch1,1" : "Apple Watch"
      "Watch1C1" : "Apple Watch"
      "Apple-Watch1C1" : "Apple Watch"
      "Watch1" : "Apple Watch"
      "Watch1_1" : "Apple Watch"
      "Watch1,2" : "Apple Watch"
      "Watch1C2" : "Apple Watch"
      "Apple-Watch1C2" : "Apple Watch"
      "Watch1_2" : "Apple Watch"
      "Watch2,6" : "Apple Watch Series 1"
      "Watch2C6" : "Apple Watch Series 1"
      "Apple-Watch2C6" : "Apple Watch Series 1"
      "Watch2_6" : "Apple Watch Series 1"
      "Watch2,7" : "Apple Watch Series 1"
      "Watch2C7" : "Apple Watch Series 1"
      "Apple-Watch2C7" : "Apple Watch Series 1"
      "Watch2_7" : "Apple Watch Series 1"
      "Watch2,3" : "Apple Watch Series 2"
      "Watch2C3" : "Apple Watch Series 2"
      "Apple-Watch2C3" : "Apple Watch Series 2"
      "Watch2_3" : "Apple Watch Series 2"
      "Watch2,4" : "Apple Watch Series 2"
      "Watch2C4" : "Apple Watch Series 2"
      "Apple-Watch2C4" : "Apple Watch Series 2"
      "Watch2_4" : "Apple Watch Series 2"
      "Watch3,1" : "Apple Watch Series 3"
      "Watch3C1" : "Apple Watch Series 3"
      "Apple-Watch3C1" : "Apple Watch Series 3"
      "Watch3" : "Apple Watch Series 3"
      "Watch3_1" : "Apple Watch Series 3"
      "Watch3,2" : "Apple Watch Series 3"
      "Watch3C2" : "Apple Watch Series 3"
      "Apple-Watch3C2" : "Apple Watch Series 3"
      "Watch3_2" : "Apple Watch Series 3"
      "Watch3,3" : "Apple Watch Series 3"
      "Watch3C3" : "Apple Watch Series 3"
      "Apple-Watch3C3" : "Apple Watch Series 3"
      "Watch3_3" : "Apple Watch Series 3"
      "Watch3,4" : "Apple Watch Series 3"
      "Watch3C4" : "Apple Watch Series 3"
      "Apple-Watch3C4" : "Apple Watch Series 3"
      "Watch3_4" : "Apple Watch Series 3"
      "Watch4,1" : "Apple Watch Series 4"
      "Watch4C1" : "Apple Watch Series 4"
      "Apple-Watch4C1" : "Apple Watch Series 4"
      "Watch4" : "Apple Watch Series 4"
      "Watch4_1" : "Apple Watch Series 4"
      "Watch4,2" : "Apple Watch Series 4"
      "Watch4C2" : "Apple Watch Series 4"
      "Apple-Watch4C2" : "Apple Watch Series 4"
      "Watch4_2" : "Apple Watch Series 4"
      "Watch4,3" : "Apple Watch Series 4"
      "Watch4C3" : "Apple Watch Series 4"
      "Apple-Watch4C3" : "Apple Watch Series 4"
      "Watch4_3" : "Apple Watch Series 4"
      "Watch4,4" : "Apple Watch Series 4"
      "Watch4C4" : "Apple Watch Series 4"
      "Apple-Watch4C4" : "Apple Watch Series 4"
      "Watch4_4" : "Apple Watch Series 4"
      "Watch5,1" : "Apple Watch Series 5 (40mm)"
      "Watch5C1" : "Apple Watch Series 5 (40mm)"
      "Apple-Watch5C1" : "Apple Watch Series 5 (40mm)"
      "Watch5" : "Apple Watch Series 5 (40mm)"
      "Watch5_1" : "Apple Watch Series 5 (40mm)"
      "Watch5,2" : "Apple Watch Series 5 (44mm)"
      "Watch5C2" : "Apple Watch Series 5 (44mm)"
      "Apple-Watch5C2" : "Apple Watch Series 5 (44mm)"
      "Watch5_2" : "Apple Watch Series 5 (44mm)"
      "Watch5,3" : "Apple Watch Series 5 (40mm, LTE)"
      "Watch5C3" : "Apple Watch Series 5 (40mm, LTE)"
      "Apple-Watch5C3" : "Apple Watch Series 5 (40mm, LTE)"
      "Watch5_3" : "Apple Watch Series 5 (40mm, LTE)"
      "Watch5,4" : "Apple Watch Series 5 (44mm, LTE)"
      "Watch5C4" : "Apple Watch Series 5 (44mm, LTE)"
      "Apple-Watch5C4" : "Apple Watch Series 5 (44mm, LTE)"
      "Watch5_4" : "Apple Watch Series 5 (44mm, LTE)"
      "iPhone1,1" : "iPhone"
      "iPhone1C1" : "iPhone"
      "Apple-iPhone1C1" : "iPhone"
      "iPhone1" : "iPhone"
      "iPhone1_1" : "iPhone"
      "iPhone1,2" : "iPhone 3G"
      "iPhone1C2" : "iPhone 3G"
      "Apple-iPhone1C2" : "iPhone 3G"
      "iPhone1_2" : "iPhone 3G"
      "iPhone2,1" : "iPhone 3GS"
      "iPhone2C1" : "iPhone 3GS"
      "Apple-iPhone2C1" : "iPhone 3GS"
      "iPhone2" : "iPhone 3GS"
      "iPhone2_1" : "iPhone 3GS"
      "iPhone3,1" : "iPhone 4 (GSM)"
      "iPhone3C1" : "iPhone 4 (GSM)"
      "Apple-iPhone3C1" : "iPhone 4 (GSM)"
      "iPhone3" : "iPhone 4 (GSM)"
      "iPhone3_1" : "iPhone 4 (GSM)"
      "iPhone3,2" : "iPhone 4"
      "iPhone3C2" : "iPhone 4"
      "Apple-iPhone3C2" : "iPhone 4"
      "iPhone3_2" : "iPhone 4"
      "iPhone3,3" : "iPhone 4 (CDMA)"
      "iPhone3C3" : "iPhone 4 (CDMA)"
      "Apple-iPhone3C3" : "iPhone 4 (CDMA)"
      "iPhone3_3" : "iPhone 4 (CDMA)"
      "iPhone4,1" : "iPhone 4S"
      "iPhone4C1" : "iPhone 4S"
      "Apple-iPhone4C1" : "iPhone 4S"
      "iPhone4" : "iPhone 4S"
      "iPhone4_1" : "iPhone 4S"
      "iPhone5,1" : "iPhone 5 (A1428)"
      "iPhone5C1" : "iPhone 5 (A1428)"
      "Apple-iPhone5C1" : "iPhone 5 (A1428)"
      "iPhone5" : "iPhone 5 (A1428)"
      "iPhone5_1" : "iPhone 5 (A1428)"
      "iPhone5,2" : "iPhone 5 (A1429)"
      "iPhone5C2" : "iPhone 5 (A1429)"
      "Apple-iPhone5C2" : "iPhone 5 (A1429)"
      "iPhone5_2" : "iPhone 5 (A1429)"
      "iPhone5,3" : "iPhone 5c (A1456/A1532)"
      "iPhone5C3" : "iPhone 5c (A1456/A1532)"
      "Apple-iPhone5C3" : "iPhone 5c (A1456/A1532)"
      "iPhone5_3" : "iPhone 5c (A1456/A1532)"
      "iPhone5,4" : "iPhone 5c (A1507/A1516/A1529)"
      "iPhone5C4" : "iPhone 5c (A1507/A1516/A1529)"
      "Apple-iPhone5C4" : "iPhone 5c (A1507/A1516/A1529)"
      "iPhone5_4" : "iPhone 5c (A1507/A1516/A1529)"
      "iPhone6,1" : "iPhone 5s (A1433/A1453)"
      "iPhone6C1" : "iPhone 5s (A1433/A1453)"
      "Apple-iPhone6C1" : "iPhone 5s (A1433/A1453)"
      "iPhone6" : "iPhone 5s (A1433/A1453)"
      "iPhone6_1" : "iPhone 5s (A1433/A1453)"
      "iPhone6,2" : "iPhone 5s (A1457/A1518/A1530)"
      "iPhone6C2" : "iPhone 5s (A1457/A1518/A1530)"
      "Apple-iPhone6C2" : "iPhone 5s (A1457/A1518/A1530)"
      "iPhone6_2" : "iPhone 5s (A1457/A1518/A1530)"
      "iPhone7,1" : "iPhone 6 Plus"
      "iPhone7C1" : "iPhone 6 Plus"
      "Apple-iPhone7C1" : "iPhone 6 Plus"
      "iPhone7" : "iPhone 6 Plus"
      "iPhone7_1" : "iPhone 6 Plus"
      "iPhone7,2" : "iPhone 6"
      "iPhone7C2" : "iPhone 6"
      "Apple-iPhone7C2" : "iPhone 6"
      "iPhone7_2" : "iPhone 6"
      "iPhone8,1" : "iPhone 6s"
      "iPhone8C1" : "iPhone 6s"
      "Apple-iPhone8C1" : "iPhone 6s"
      "iPhone8" : "iPhone 6s"
      "iPhone8_1" : "iPhone 6s"
      "iPhone8,2" : "iPhone 6s Plus"
      "iPhone8C2" : "iPhone 6s Plus"
      "Apple-iPhone8C2" : "iPhone 6s Plus"
      "iPhone8_2" : "iPhone 6s Plus"
      "iPhone8,4" : "iPhone SE"
      "iPhone8C4" : "iPhone SE"
      "Apple-iPhone8C4" : "iPhone SE"
      "iPhone8_4" : "iPhone SE"
      "iPhone9,1" : "iPhone 7"
      "iPhone9C1" : "iPhone 7"
      "Apple-iPhone9C1" : "iPhone 7"
      "iPhone9" : "iPhone 7"
      "iPhone9_1" : "iPhone 7"
      "iPhone9,3" : "iPhone 7"
      "iPhone9C3" : "iPhone 7"
      "Apple-iPhone9C3" : "iPhone 7"
      "iPhone9_3" : "iPhone 7"
      "iPhone9,2" : "iPhone 7 Plus"
      "iPhone9C2" : "iPhone 7 Plus"
      "Apple-iPhone9C2" : "iPhone 7 Plus"
      "iPhone9_2" : "iPhone 7 Plus"
      "iPhone9,4" : "iPhone 7 Plus"
      "iPhone9C4" : "iPhone 7 Plus"
      "Apple-iPhone9C4" : "iPhone 7 Plus"
      "iPhone9_4" : "iPhone 7 Plus"
      "iPhone10,1" : "iPhone 8 (A1863/A1906)"
      "iPhone10C1" : "iPhone 8 (A1863/A1906)"
      "Apple-iPhone10C1" : "iPhone 8 (A1863/A1906)"
      "iPhone10" : "iPhone 8 (A1863/A1906)"
      "iPhone10_1" : "iPhone 8 (A1863/A1906)"
      "iPhone10,2" : "iPhone 8 Plus (A1864/A1898)"
      "iPhone10C2" : "iPhone 8 Plus (A1864/A1898)"
      "Apple-iPhone10C2" : "iPhone 8 Plus (A1864/A1898)"
      "iPhone10_2" : "iPhone 8 Plus (A1864/A1898)"
      "iPhone10,3" : "iPhone X (A1865/A1902)"
      "iPhone10C3" : "iPhone X (A1865/A1902)"
      "Apple-iPhone10C3" : "iPhone X (A1865/A1902)"
      "iPhone10_3" : "iPhone X (A1865/A1902)"
      "iPhone10,4" : "iPhone 8 (A1905)"
      "iPhone10C4" : "iPhone 8 (A1905)"
      "Apple-iPhone10C4" : "iPhone 8 (A1905)"
      "iPhone10_4" : "iPhone 8 (A1905)"
      "iPhone10,5" : "iPhone 8 Plus (A1897)"
      "iPhone10C5" : "iPhone 8 Plus (A1897)"
      "Apple-iPhone10C5" : "iPhone 8 Plus (A1897)"
      "iPhone10_5" : "iPhone 8 Plus (A1897)"
      "iPhone10,6" : "iPhone X (A1901)"
      "iPhone10C6" : "iPhone X (A1901)"
      "Apple-iPhone10C6" : "iPhone X (A1901)"
      "iPhone10_6" : "iPhone X (A1901)"
      "iPhone X" : "iPhone X (A1901)"
      "iPhone11,8" : "iPhone XR"
      "iPhone11C8" : "iPhone XR"
      "Apple-iPhone11C8" : "iPhone XR"
      "iPhone11_8" : "iPhone XR"
      "iPhone11,2" : "iPhone XS"
      "iPhone11C2" : "iPhone XS"
      "Apple-iPhone11C2" : "iPhone XS"
      "iPhone11_2" : "iPhone XS"
      "iPhone11,6" : "iPhone XS Max"
      "iPhone11C6" : "iPhone XS Max"
      "Apple-iPhone11C6" : "iPhone XS Max"
      "iPhone11_6" : "iPhone XS Max"
      "iPhone11,4" : "iPhone XS Max China"
      "iPhone11C4" : "iPhone XS Max China"
      "Apple-iPhone11C4" : "iPhone XS Max China"
      "iPhone11_4" : "iPhone XS Max China"
      "iPhone12,1" : "iPhone 11"
      "iPhone12C1" : "iPhone 11"
      "Apple-iPhone12C1" : "iPhone 11"
      "iPhone12" : "iPhone 11"
      "iPhone12_1" : "iPhone 11"
      "iPhone12,3" : "iPhone 11 Pro"
      "iPhone12C3" : "iPhone 11 Pro"
      "Apple-iPhone12C3" : "iPhone 11 Pro"
      "iPhone12_3" : "iPhone 11 Pro"
      "iPhone12,5" : "iPhone 11 Pro Max"
      "iPhone12C5" : "iPhone 11 Pro Max"
      "Apple-iPhone12C5" : "iPhone 11 Pro Max"
      "iPhone12_5" : "iPhone 11 Pro Max"
      "iPad1,1" : "iPad"
      "iPad1C1" : "iPad"
      "Apple-iPad1C1" : "iPad"
      "iPad1" : "iPad"
      "iPad1_1" : "iPad"
      "iPad2,1" : "iPad 2 (Wi-Fi)"
      "iPad2C1" : "iPad 2 (Wi-Fi)"
      "Apple-iPad2C1" : "iPad 2 (Wi-Fi)"
      "iPad2" : "iPad 2 (Wi-Fi)"
      "iPad2_1" : "iPad 2 (Wi-Fi)"
      "iPad2,2" : "iPad 2 (GSM)"
      "iPad2C2" : "iPad 2 (GSM)"
      "Apple-iPad2C2" : "iPad 2 (GSM)"
      "iPad2_2" : "iPad 2 (GSM)"
      "iPad2,3" : "iPad 2 (CDMA)"
      "iPad2C3" : "iPad 2 (CDMA)"
      "Apple-iPad2C3" : "iPad 2 (CDMA)"
      "iPad2_3" : "iPad 2 (CDMA)"
      "iPad2,4" : "iPad 2 (Wi-Fi, revised)"
      "iPad2C4" : "iPad 2 (Wi-Fi, revised)"
      "Apple-iPad2C4" : "iPad 2 (Wi-Fi, revised)"
      "iPad2_4" : "iPad 2 (Wi-Fi, revised)"
      "iPad2,5" : "iPad mini (Wi-Fi)"
      "iPad2C5" : "iPad mini (Wi-Fi)"
      "Apple-iPad2C5" : "iPad mini (Wi-Fi)"
      "iPad2_5" : "iPad mini (Wi-Fi)"
      "iPad2,6" : "iPad mini (A1454)"
      "iPad2C6" : "iPad mini (A1454)"
      "Apple-iPad2C6" : "iPad mini (A1454)"
      "iPad2_6" : "iPad mini (A1454)"
      "iPad2,7" : "iPad mini (A1455)"
      "iPad2C7" : "iPad mini (A1455)"
      "Apple-iPad2C7" : "iPad mini (A1455)"
      "iPad2_7" : "iPad mini (A1455)"
      "iPad3,1" : "iPad (3rd gen, Wi-Fi)"
      "iPad3C1" : "iPad (3rd gen, Wi-Fi)"
      "Apple-iPad3C1" : "iPad (3rd gen, Wi-Fi)"
      "iPad3" : "iPad (3rd gen, Wi-Fi)"
      "iPad3_1" : "iPad (3rd gen, Wi-Fi)"
      "iPad3,2" : "iPad (3rd gen, Wi-Fi+LTE Verizon)"
      "iPad3C2" : "iPad (3rd gen, Wi-Fi+LTE Verizon)"
      "Apple-iPad3C2" : "iPad (3rd gen, Wi-Fi+LTE Verizon)"
      "iPad3_2" : "iPad (3rd gen, Wi-Fi+LTE Verizon)"
      "iPad3,3" : "iPad (3rd gen, Wi-Fi+LTE AT&T)"
      "iPad3C3" : "iPad (3rd gen, Wi-Fi+LTE AT&T)"
      "Apple-iPad3C3" : "iPad (3rd gen, Wi-Fi+LTE AT&T)"
      "iPad3_3" : "iPad (3rd gen, Wi-Fi+LTE AT&T)"
      "iPad3,4" : "iPad (4th gen, Wi-Fi)"
      "iPad3C4" : "iPad (4th gen, Wi-Fi)"
      "Apple-iPad3C4" : "iPad (4th gen, Wi-Fi)"
      "iPad3_4" : "iPad (4th gen, Wi-Fi)"
      "iPad3,5" : "iPad (4th gen, A1459)"
      "iPad3C5" : "iPad (4th gen, A1459)"
      "Apple-iPad3C5" : "iPad (4th gen, A1459)"
      "iPad3_5" : "iPad (4th gen, A1459)"
      "iPad3,6" : "iPad (4th gen, A1460)"
      "iPad3C6" : "iPad (4th gen, A1460)"
      "Apple-iPad3C6" : "iPad (4th gen, A1460)"
      "iPad3_6" : "iPad (4th gen, A1460)"
      "iPad4,1" : "iPad Air (Wi-Fi)"
      "iPad4C1" : "iPad Air (Wi-Fi)"
      "Apple-iPad4C1" : "iPad Air (Wi-Fi)"
      "iPad4" : "iPad Air (Wi-Fi)"
      "iPad4_1" : "iPad Air (Wi-Fi)"
      "iPad4,2" : "iPad Air (Wi-Fi+LTE)"
      "iPad4C2" : "iPad Air (Wi-Fi+LTE)"
      "Apple-iPad4C2" : "iPad Air (Wi-Fi+LTE)"
      "iPad4_2" : "iPad Air (Wi-Fi+LTE)"
      "iPad4,3" : "iPad Air (Rev)"
      "iPad4C3" : "iPad Air (Rev)"
      "Apple-iPad4C3" : "iPad Air (Rev)"
      "iPad4_3" : "iPad Air (Rev)"
      "iPad4,4" : "iPad mini 2 (Wi-Fi)"
      "iPad4C4" : "iPad mini 2 (Wi-Fi)"
      "Apple-iPad4C4" : "iPad mini 2 (Wi-Fi)"
      "iPad4_4" : "iPad mini 2 (Wi-Fi)"
      "iPad4,5" : "iPad mini 2 (Wi-Fi+LTE)"
      "iPad4C5" : "iPad mini 2 (Wi-Fi+LTE)"
      "Apple-iPad4C5" : "iPad mini 2 (Wi-Fi+LTE)"
      "iPad4_5" : "iPad mini 2 (Wi-Fi+LTE)"
      "iPad4,6" : "iPad mini 2 (Rev)"
      "iPad4C6" : "iPad mini 2 (Rev)"
      "Apple-iPad4C6" : "iPad mini 2 (Rev)"
      "iPad4_6" : "iPad mini 2 (Rev)"
      "iPad4,7" : "iPad mini 3 (Wi-Fi)"
      "iPad4C7" : "iPad mini 3 (Wi-Fi)"
      "Apple-iPad4C7" : "iPad mini 3 (Wi-Fi)"
      "iPad4_7" : "iPad mini 3 (Wi-Fi)"
      "iPad4,8" : "iPad mini 3 (A1600)"
      "iPad4C8" : "iPad mini 3 (A1600)"
      "Apple-iPad4C8" : "iPad mini 3 (A1600)"
      "iPad4_8" : "iPad mini 3 (A1600)"
      "iPad4,9" : "iPad mini 3 (A1601)"
      "iPad4C9" : "iPad mini 3 (A1601)"
      "Apple-iPad4C9" : "iPad mini 3 (A1601)"
      "iPad4_9" : "iPad mini 3 (A1601)"
      "iPad5,1" : "iPad mini 4 (Wi-Fi)"
      "iPad5C1" : "iPad mini 4 (Wi-Fi)"
      "Apple-iPad5C1" : "iPad mini 4 (Wi-Fi)"
      "iPad5" : "iPad mini 4 (Wi-Fi)"
      "iPad5_1" : "iPad mini 4 (Wi-Fi)"
      "iPad5,2" : "iPad mini 4 (Wi-Fi+LTE)"
      "iPad5C2" : "iPad mini 4 (Wi-Fi+LTE)"
      "Apple-iPad5C2" : "iPad mini 4 (Wi-Fi+LTE)"
      "iPad5_2" : "iPad mini 4 (Wi-Fi+LTE)"
      "iPad5,3" : "iPad Air 2 (Wi-Fi)"
      "iPad5C3" : "iPad Air 2 (Wi-Fi)"
      "Apple-iPad5C3" : "iPad Air 2 (Wi-Fi)"
      "iPad5_3" : "iPad Air 2 (Wi-Fi)"
      "iPad5,4" : "iPad Air 2 (Wi-Fi+LTE)"
      "iPad5C4" : "iPad Air 2 (Wi-Fi+LTE)"
      "Apple-iPad5C4" : "iPad Air 2 (Wi-Fi+LTE)"
      "iPad5_4" : "iPad Air 2 (Wi-Fi+LTE)"
      "iPad6,3" : "iPad Pro (9.7 inch) (Wi-Fi)"
      "iPad6C3" : "iPad Pro (9.7 inch) (Wi-Fi)"
      "Apple-iPad6C3" : "iPad Pro (9.7 inch) (Wi-Fi)"
      "iPad6_3" : "iPad Pro (9.7 inch) (Wi-Fi)"
      "iPad6,4" : "iPad Pro (9.7 inch) (Wi-Fi+LTE)"
      "iPad6C4" : "iPad Pro (9.7 inch) (Wi-Fi+LTE)"
      "Apple-iPad6C4" : "iPad Pro (9.7 inch) (Wi-Fi+LTE)"
      "iPad6_4" : "iPad Pro (9.7 inch) (Wi-Fi+LTE)"
      "iPad6,7" : "iPad Pro (12.9 inch) (Wi-Fi)"
      "iPad6C7" : "iPad Pro (12.9 inch) (Wi-Fi)"
      "Apple-iPad6C7" : "iPad Pro (12.9 inch) (Wi-Fi)"
      "iPad6_7" : "iPad Pro (12.9 inch) (Wi-Fi)"
      "iPad6,8" : "iPad Pro (12.9 inch) (Wi-Fi+LTE)"
      "iPad6C8" : "iPad Pro (12.9 inch) (Wi-Fi+LTE)"
      "Apple-iPad6C8" : "iPad Pro (12.9 inch) (Wi-Fi+LTE)"
      "iPad6_8" : "iPad Pro (12.9 inch) (Wi-Fi+LTE)"
      "iPad6,11" : "iPad (2017)"
      "iPad6C11" : "iPad (2017)"
      "Apple-iPad6C11" : "iPad (2017)"
      "iPad61" : "iPad (2017)"
      "iPad6_11" : "iPad (2017)"
      "iPad6,12" : "iPad (2017)"
      "iPad6C12" : "iPad (2017)"
      "Apple-iPad6C12" : "iPad (2017)"
      "iPad62" : "iPad (2017)"
      "iPad6_12" : "iPad (2017)"
      "iPad7,1" : "iPad Pro (2nd Gen, WiFi)"
      "iPad7C1" : "iPad Pro (2nd Gen, WiFi)"
      "Apple-iPad7C1" : "iPad Pro (2nd Gen, WiFi)"
      "iPad7" : "iPad Pro (2nd Gen, WiFi)"
      "iPad7_1" : "iPad Pro (2nd Gen, WiFi)"
      "iPad7,2" : "iPad Pro (2nd Gen, WiFi+Cellular)"
      "iPad7C2" : "iPad Pro (2nd Gen, WiFi+Cellular)"
      "Apple-iPad7C2" : "iPad Pro (2nd Gen, WiFi+Cellular)"
      "iPad7_2" : "iPad Pro (2nd Gen, WiFi+Cellular)"
      "iPad7,3" : "iPad Pro (10.5 inch, A1701)"
      "iPad7C3" : "iPad Pro (10.5 inch, A1701)"
      "Apple-iPad7C3" : "iPad Pro (10.5 inch, A1701)"
      "iPad7_3" : "iPad Pro (10.5 inch, A1701)"
      "iPad7,4" : "iPad Pro (10.5 inch, A1709)"
      "iPad7C4" : "iPad Pro (10.5 inch, A1709)"
      "Apple-iPad7C4" : "iPad Pro (10.5 inch, A1709)"
      "iPad7_4" : "iPad Pro (10.5 inch, A1709)"
      "iPad7,5" : "iPad (6th gen, A1893)"
      "iPad7C5" : "iPad (6th gen, A1893)"
      "Apple-iPad7C5" : "iPad (6th gen, A1893)"
      "iPad7_5" : "iPad (6th gen, A1893)"
      "iPad7,6" : "iPad (6th gen, A1954)"
      "iPad7C6" : "iPad (6th gen, A1954)"
      "Apple-iPad7C6" : "iPad (6th gen, A1954)"
      "iPad7_6" : "iPad (6th gen, A1954)"
      "iPad7,11" : "iPad (7th gen, 10.2-inch, WiFi)"
      "iPad7C11" : "iPad (7th gen, 10.2-inch, WiFi)"
      "Apple-iPad7C11" : "iPad (7th gen, 10.2-inch, WiFi)"
      "iPad71" : "iPad (7th gen, 10.2-inch, WiFi)"
      "iPad7_11" : "iPad (7th gen, 10.2-inch, WiFi)"
      "iPad7,12" : "iPad (7th gen, 10.2-inch, WiFi+Cellular)"
      "iPad7C12" : "iPad (7th gen, 10.2-inch, WiFi+Cellular)"
      "Apple-iPad7C12" : "iPad (7th gen, 10.2-inch, WiFi+Cellular)"
      "iPad72" : "iPad (7th gen, 10.2-inch, WiFi+Cellular)"
      "iPad7_12" : "iPad (7th gen, 10.2-inch, WiFi+Cellular)"
      "iPad8,1" : "iPad Pro (11 inch)"
      "iPad8C1" : "iPad Pro (11 inch)"
      "Apple-iPad8C1" : "iPad Pro (11 inch)"
      "iPad8" : "iPad Pro (11 inch)"
      "iPad8_1" : "iPad Pro (11 inch)"
      "iPad8,2" : "iPad Pro (11 inch)"
      "iPad8C2" : "iPad Pro (11 inch)"
      "Apple-iPad8C2" : "iPad Pro (11 inch)"
      "iPad8_2" : "iPad Pro (11 inch)"
      "iPad8,3" : "iPad Pro (11 inch)"
      "iPad8C3" : "iPad Pro (11 inch)"
      "Apple-iPad8C3" : "iPad Pro (11 inch)"
      "iPad8_3" : "iPad Pro (11 inch)"
      "iPad8,4" : "iPad Pro (11 inch)"
      "iPad8C4" : "iPad Pro (11 inch)"
      "Apple-iPad8C4" : "iPad Pro (11 inch)"
      "iPad8_4" : "iPad Pro (11 inch)"
      "iPad8,5" : "iPad Pro (3rd gen, 12.9 inch)"
      "iPad8C5" : "iPad Pro (3rd gen, 12.9 inch)"
      "Apple-iPad8C5" : "iPad Pro (3rd gen, 12.9 inch)"
      "iPad8_5" : "iPad Pro (3rd gen, 12.9 inch)"
      "iPad8,6" : "iPad Pro (3rd gen, 12.9 inch)"
      "iPad8C6" : "iPad Pro (3rd gen, 12.9 inch)"
      "Apple-iPad8C6" : "iPad Pro (3rd gen, 12.9 inch)"
      "iPad8_6" : "iPad Pro (3rd gen, 12.9 inch)"
      "iPad8,7" : "iPad Pro (3rd gen, 12.9 inch)"
      "iPad8C7" : "iPad Pro (3rd gen, 12.9 inch)"
      "Apple-iPad8C7" : "iPad Pro (3rd gen, 12.9 inch)"
      "iPad8_7" : "iPad Pro (3rd gen, 12.9 inch)"
      "iPad8,8" : "iPad Pro (3rd gen, 12.9 inch)"
      "iPad8C8" : "iPad Pro (3rd gen, 12.9 inch)"
      "Apple-iPad8C8" : "iPad Pro (3rd gen, 12.9 inch)"
      "iPad8_8" : "iPad Pro (3rd gen, 12.9 inch)"
      "iPad11,1" : "iPad Mini 5"
      "iPad11C1" : "iPad Mini 5"
      "Apple-iPad11C1" : "iPad Mini 5"
      "iPad11" : "iPad Mini 5"
      "iPad11_1" : "iPad Mini 5"
      "iPad11,2" : "iPad Mini 5"
      "iPad11C2" : "iPad Mini 5"
      "Apple-iPad11C2" : "iPad Mini 5"
      "iPad11_2" : "iPad Mini 5"
      "iPad11,3" : "iPad Air (3rd gen)"
      "iPad11C3" : "iPad Air (3rd gen)"
      "Apple-iPad11C3" : "iPad Air (3rd gen)"
      "iPad11_3" : "iPad Air (3rd gen)"
      "iPad11,4" : "iPad Air (3rd gen)"
      "iPad11C4" : "iPad Air (3rd gen)"
      "Apple-iPad11C4" : "iPad Air (3rd gen)"
      "iPad11_4" : "iPad Air (3rd gen)"
      "iPod1,1" : "iPod touch"
      "iPod1C1" : "iPod touch"
      "Apple-iPod1C1" : "iPod touch"
      "iPod1" : "iPod touch"
      "iPod1_1" : "iPod touch"
      "iPod2,1" : "iPod touch (2nd gen)"
      "iPod2C1" : "iPod touch (2nd gen)"
      "Apple-iPod2C1" : "iPod touch (2nd gen)"
      "iPod2" : "iPod touch (2nd gen)"
      "iPod2_1" : "iPod touch (2nd gen)"
      "iPod3,1" : "iPod touch (3rd gen)"
      "iPod3C1" : "iPod touch (3rd gen)"
      "Apple-iPod3C1" : "iPod touch (3rd gen)"
      "iPod3" : "iPod touch (3rd gen)"
      "iPod3_1" : "iPod touch (3rd gen)"
      "iPod4,1" : "iPod touch (4th gen)"
      "iPod4C1" : "iPod touch (4th gen)"
      "Apple-iPod4C1" : "iPod touch (4th gen)"
      "iPod4" : "iPod touch (4th gen)"
      "iPod4_1" : "iPod touch (4th gen)"
      "iPod5,1" : "iPod touch (5th gen)"
      "iPod5C1" : "iPod touch (5th gen)"
      "Apple-iPod5C1" : "iPod touch (5th gen)"
      "iPod5" : "iPod touch (5th gen)"
      "iPod5_1" : "iPod touch (5th gen)"
      "iPod7,1" : "iPod touch (6th gen)"
      "iPod7C1" : "iPod touch (6th gen)"
      "Apple-iPod7C1" : "iPod touch (6th gen)"
      "iPod7" : "iPod touch (6th gen)"
      "iPod7_1" : "iPod touch (6th gen)"
      "iPod9,1" : "iPod touch (7th gen)"
      "iPod9C1" : "iPod touch (7th gen)"
      "Apple-iPod9C1" : "iPod touch (7th gen)"
      "iPod9" : "iPod touch (7th gen)"
      "iPod9_1" : "iPod touch (7th gen)"
*/
