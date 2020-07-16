using EnumsNET;
using Microsoft.Win32;
using System;
using System.IO;
using System.Net.NetworkInformation;
using ZChangerMMO.Model;
using ZChangerMMO.Utility;

namespace ZChangerMMO
{
    internal static class Helper
    {
        internal const string APP_REG_PATH = "SOFTWARE\\WindowsAppServiceB2B\\ZBrowser";
        internal const string APP_REG_BARRIE_PATH = "SOFTWARE\\99620DC4-DA31-4EF5-884A-23E4129456AE";

        internal static string ConvertFileSizeToString(long size)
        {
            string[] sizeSuffixes = { "B", "KB", "MB", "GB", "TB", "PB", "EB", "ZB", "YB" };
            const string formatTemplate = "{0}{1:0.##} {2}";

            if (size == 0)
            {
                return string.Format(formatTemplate, null, 0, sizeSuffixes[0]);
            }

            double absSize = Math.Abs((double)size);
            double fpPower = Math.Log(absSize, 1000);
            int intPower = (int)fpPower;
            int iUnit = (intPower >= sizeSuffixes.Length) ? (sizeSuffixes.Length - 1) : intPower;
            double normSize = absSize / Math.Pow(1000, iUnit);

            return string.Format(formatTemplate, (size < 0) ? "-" : null, normSize, sizeSuffixes[iUnit]);
        }

        internal static void EmptyFolder(string path)
        {
            DirectoryInfo di = new DirectoryInfo(path);

            foreach (FileInfo file in di.GetFiles())
            {
                file.Delete();
            }
            foreach (DirectoryInfo dir in di.GetDirectories())
            {
                dir.Delete(true);
            }
        }

        internal static string GetActionLogEnumDescription(LogActionType type) => type.AsString(EnumFormat.Description);

        internal static long GetFileSize(string path)
        {
            if (File.Exists(path))
            {
                return new FileInfo(path).Length;
            }
            else
            {
                return 0;
            }
        }

        internal static long GetFolderSize(string path)
        {
            if (Directory.Exists(path))
            {
                long size = 0;
                DirectoryInfo dirInfo = new DirectoryInfo(path);

                foreach (FileInfo fi in dirInfo.GetFiles("*", SearchOption.AllDirectories))
                {
                    size += fi.Length;
                }

                return size;
            }
            else
            {
                return 0;
            }
        }

        internal static string GetLanguageCode(string language)
        {
            switch (language)
            {
                case "English":
                    return "en";
                case "Chinese":
                    return "zh";
                case "Spanish":
                    return "es";
                case "Arabic":
                    return "ar";
                case "Portuguese":
                    return "pt";
                case "Indonesian":
                    return "id";
                case "French":
                    return "fr";
                case "Japanese":
                    return "ja";
                case "Russian":
                    return "ru";
                case "German":
                    return "de";
                default:
                    return "en";
            }
        }

        internal static string GetUserAgent(string browserType)
        {
            switch (browserType)
            {
                case "Chrome":
                    return "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/68.0.3440.106 Safari/537.36";
                case "Firefox":
                    return "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:53.0) Gecko/20100101 Firefox/53.0";
                case "Opera":
                    return "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/65.0.3325.181 Safari/537.36 OPR/52.0.2871.64";
                case "Safari":
                    return "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_11_6) AppleWebKit/605.1.15 (KHTML, like Gecko) Version/11.1.2 Safari/605.1.15";
                case "Edge":
                    return "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/64.0.3282.140 Safari/537.36 Edge/17.17134";
                case "IE":
                    return "Mozilla/5.0 (compatible, MSIE 11, Windows NT 6.3; Trident/7.0; rv:11.0) like Gecko";
                default:
                    return "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/68.0.3440.106 Safari/537.36";
            }
        }

        internal static PhysicalAddress GetMacAddress()
        {
            foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
            {
                // Only consider Ethernet network interfaces
                if (nic.NetworkInterfaceType == NetworkInterfaceType.Ethernet &&
                    nic.OperationalStatus == OperationalStatus.Up)
                {
                    return nic.GetPhysicalAddress();
                }
            }
            return null;
        }
        internal static string GetDeviceId()
        {
            var id = GetMacAddress();
            if (id == null)
            {
                throw new Exception("Error when get device id");
            }

            return id.ToString();
        }
        
        /// <summary>
        /// This C# code reads a key from the windows registry.
        /// </summary>
        /// <param name="keyName">
        /// <returns></returns>
        internal static string ReadRG(string keyName)
        {
            try
            {
                RegistryKey sk = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(APP_REG_PATH);
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
        internal static void WriteRG(string keyName, string value)
        {
            RegistryKey sk1 = Microsoft.Win32.Registry.LocalMachine.CreateSubKey(APP_REG_PATH);
            sk1.SetValue(keyName.ToUpper(), value);
        }
    }
}
