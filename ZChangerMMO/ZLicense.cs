using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using ZChangerMMO.Licensing.Signing;
using ZChangerMMO.Licensing.Licensing;
using HWId = ZChangerMMO.Licensing.Licensing.HardwareIdentifier;
using SerialNo = ZChangerMMO.Licensing.Licensing.SerialNumber;
using System.Security.Cryptography;
using Microsoft.Win32;

namespace ZChangerMMO
{
    internal class ZLicense
    {
        internal const string APP_CODE = "ZCB";
        internal const string DATE_FROMAT = "dd/MM/yyyy: hh:mm:ss";
        internal const string APP_REG_PATH = "SOFTWARE\\WindowsAppServiceB2B\\ZBrowser";
        // Public
        internal static string PublicKey = "BgIAAACkAABSU0ExAAQAAAEAAQA1YQvRSrhGFxeRm36HpXxbdHgfVpbiDOx4b8+4x2CzSloK37EtjRSTuv2CGqLyrheyV261TSPAMR1hq4ukkCo7exkb0B/VMuOr9HRZ0ed6qzoyHtifXeoaJA7ArAyEh/NarF44Z+tF33o901v1bWiZ/xMjuZ/siJRh9m0zAU+U0A==";

//#if DEBUG
//        // Private
//        internal static string PrivateKey = "BwIAAACkAABSU0EyAAQAAAEAAQA1YQvRSrhGFxeRm36HpXxbdHgfVpbiDOx4b8+4x2CzSloK37EtjRSTuv2CGqLyrheyV261TSPAMR1hq4ukkCo7exkb0B/VMuOr9HRZ0ed6qzoyHtifXeoaJA7ArAyEh/NarF44Z+tF33o901v1bWiZ/xMjuZ/siJRh9m0zAU+U0J/zabzsuoztrFP/8JwtxNBzMYCjrde/Rf632lAkGJ5CglcSnV+6L0aITb8rjIuGDWwbmlak1RfIvi9f+LkIttSrmkgrR+jSk2NMTxrW//0knOO4HGFq1nz3AoFwfmOF4gYi1NNxFw5GTImEae5x2PIx+at5uWhWUb0S+nctBAf7lRKypvctYg/T6ETSBXtaUO3/V/Ci4RpgeDR+qkRMhyEowM1S49qTMyscC4cw5NqvEjO4GiTptUZUVWPeJ3W1l80HEl6zaTlJ5/3/LO/hbhtYM/+MoYSoq00JbGLstbUQPkUtGksMTEfLR+WBlbtisvtAdQAq/ePxOmOZVSBVmSNdPuXu9VmXHNoRhssWjVkOwA8zwsLkuMhF7hGVWsm3rk7j7W6UXeLRhT+LdZl6wIkcvHdn/NYo02TK0Q2J4GHHLd/rGfiGK9JNyM4jWINg4vzJYUzIRR8miJZOKpCO6Jc1jqwXWV0DeJQ8GPNvy8ymybgSPceKv9I0c/+ScY7rEptVIqn+D6h8cc2xLeyyiDYAZ51JBpPxdg/IAE40uUC1emNG557cCRg1wEcKHgQbglgr3mshxM8Ckb3fVqQ7X04=";
//        /// <summary>
//        /// For keygen only
//        /// </summary>
//        /// <returns></returns>
//        internal string GenerateLicense()
//        {
//            var license = Lic.Builder
//                .WithRsaPrivateKey(PrivateKey)              // .WithSigner(ISigner)
//                .WithHardwareIdentifier(HardwareIdentifier) // .WithoutHardwareIdentifier()
//                .WithoutSerialNumber()                      //.WithSerialNumber(SerialNumber)             
//                                                            //.ExpiresIn(TimeSpan.), 
//                .ExpiresOn(ExpirationDate)
//                .WithProperty("IssueDate", IssueDate.ToString(DATE_FROMAT))
//                .WithProperty("ExpirationDate", ExpirationDate.ToString(DATE_FROMAT))
//                //.WithProperty("SerialNumber", SerialNumber)
//                .WithProperty("ZId", ZId)
//                .WithProperty("Username", Username)         //... other key value pairs
//                .WithProperty("SupportedApps", SupportedApps)
//                .WithProperty("SupportedDevices", SupportedDevices)
//                .SignAndCreate();

//            return license.Serialize();
//        }

//        /// <summary>
//        /// 
//        /// </summary>
//        /// <param name="hardwareIdentifier"></param>
//        /// <param name="zId"></param>
//        /// <param name="expirationDate"></param>
//        /// <param name="issueDate"></param>
//        /// <param name="userName"></param>
//        /// <param name="supportedApps"></param>
//        /// <param name="supportedDevices"></param>
//        /// <returns></returns>
//        internal string GenerateLicense(string hardwareIdentifier,
//                                        string zId,
//                                        DateTime expirationDate,
//                                        DateTime issueDate,
//                                        string userName,
//                                        string supportedApps,
//                                        string supportedDevices)
//        {
//            return GenerateLicense($"{hardwareIdentifier}-{zId}", expirationDate, issueDate, userName, supportedApps, supportedDevices);
//        }

//        /// <summary>
//        /// 
//        /// </summary>
//        /// <param name="requestedMachineId"></param>
//        /// <param name="expirationDate"></param>
//        /// <param name="issueDate"></param>
//        /// <param name="userName"></param>
//        /// <param name="supportedApps"></param>
//        /// <param name="supportedDevices"></param>
//        /// <returns></returns>
//        internal string GenerateLicense(string requestedMachineId,
//                                        DateTime expirationDate,
//                                        DateTime issueDate,
//                                        string userName,
//                                        string supportedApps,
//                                        string supportedDevices)
//        {
//            var zId = string.Empty;
//            var hardwareIdentifier = string.Empty;

//            try
//            {
//                hardwareIdentifier = requestedMachineId.Substring(0, 40);
//                zId = requestedMachineId.Substring(41, 29);
//            }
//            catch
//            {
//                return string.Empty;
//            }

//            SignedLicense license = Lic.Builder
//                .WithRsaPrivateKey(PrivateKey)              // .WithSigner(ISigner)
//                .WithHardwareIdentifier(hardwareIdentifier) // .WithoutHardwareIdentifier()
//                .WithoutSerialNumber()                      //.WithSerialNumber(SerialNumber)   
//                                                            //.ExpiresIn(TimeSpan.), 
//                .ExpiresOn(expirationDate)
//                .WithProperty("IssueDate", issueDate.ToString(DATE_FROMAT))
//                .WithProperty("ExpirationDate", expirationDate.ToString(DATE_FROMAT))
//                //.WithProperty("SerialNumber", serialNumber)
//                .WithProperty("ZId", zId)
//                .WithProperty("Username", userName)         //... other key value pairs
//                .WithProperty("SupportedApps", supportedApps)
//                .WithProperty("SupportedDevices", supportedDevices)
//                .SignAndCreate();

//            return license.Serialize();
//        }
//#endif

        internal DateTime ExpirationDate { get; set; }
        internal DateTime IssueDate { get; set; }
        internal string SerialNumber { get; set; }
        internal string HardwareIdentifier { get; }
        internal string ZId { get; set; }
        internal string Username { get; set; }
        internal string SupportedApps { get; set; }
        internal string SupportedDevices { get; set; }
        internal string LicenseKey { get; private set; }
        internal IDictionary<string, string> Properties { get; set; }

        public ZLicense(string licenseKey = "")
        {
            try
            {
                if (string.IsNullOrEmpty(licenseKey))
                {
                    licenseKey = GetLicenseKey();                    
                }
                LicenseKey = licenseKey;

                var license = Lic.Verifier
                      .WithRsaPublicKey(PublicKey)         // .WithSigner(ISigner)
                      .WithApplicationCode(APP_CODE)       // .WithoutApplicationCode
                      .LoadAndVerify(LicenseKey);

                IssueDate = license.IssueDate;
                ExpirationDate = license.ExpirationDate;
                HardwareIdentifier = license.HardwareIdentifier;
                SerialNumber = license.SerialNumber;
                ZId = license.Properties["ZId"];
                Username = license.Properties["Username"];
                SupportedApps = license.Properties["SupportedApps"];
                SupportedDevices = license.Properties["SupportedDevices"];
            }
            catch
            {
                IssueDate = DateTime.Now;
                ExpirationDate = DateTime.Now;
                SerialNumber = SerialNo.Create(APP_CODE);
                HardwareIdentifier = HWId.ForCurrentComputer();
                ZId = HardwareInfo.GenerateUID();
                SupportedApps = "";
                SupportedDevices = "";
            }
        }

        internal void UpdateUserName(string userName)
        {
            this.Username = userName;
        }

        internal string VerifyLicense(string licenseText)
        {
            var errorMessage = string.Empty;
            try
            {
                SignedLicense license = Lic.Verifier
                   .WithRsaPublicKey(PublicKey)         // .WithSigner(ISigner)
                   .WithApplicationCode(APP_CODE)       // .WithoutApplicationCode
                   .LoadAndVerify(licenseText);

                if (DateTime.Compare(license.ExpirationDate, DateTime.Now) < 0)
                {
                    errorMessage = "License expired";
                }
                else
                {
                    if ((license.HardwareIdentifier != HardwareIdentifier) ||
                        (license.Properties["ZId"] != ZId) ||
                        (license.Properties["ExpirationDate"] != license.ExpirationDate.ToString(DATE_FROMAT)))
                    {
                        errorMessage = "Invalid license";
                    }

                    if (!HWId.IsCheckSumValid(license.HardwareIdentifier))
                    {
                        errorMessage = "Invalid license";
                    }
                }

                SupportedApps = license.Properties["SupportedApps"];
                SupportedDevices = license.Properties["SupportedDevices"];
            }
            catch (Exception ex)
            {
                if (ex.Message.StartsWith("License has been expired"))
                {
                    errorMessage = ex.Message;
                }
                else
                {
                    errorMessage = "Invalid license";
                }
            }

            return errorMessage;
        }

        internal string GetRequestLicense()
        {
            return $"{HardwareIdentifier}-{ZId}-{Environment.UserName}";
        }

        internal bool SaveLicenseKey(string key)
        {
            try
            {
                WriteRG("LicenseKey", key);
            }
            catch { return false; }

            return true;
        }

        internal string GetLicenseKey()
        {
            string key = string.Empty;
            try
            {
                key = ReadRG("LicenseKey");
            }
            catch { }

            return key;
        }
        /// <summary>
        /// This C# code reads a key from the windows registry.
        /// </summary>
        /// <param name="keyName">
        /// <returns></returns>
        static string ReadRG(string keyName)
        {
            string subKey = APP_REG_PATH;
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
            string subKey = APP_REG_PATH;

            RegistryKey sk1 = Microsoft.Win32.Registry.LocalMachine.CreateSubKey(subKey);
            sk1.SetValue(keyName.ToUpper(), value);
        }

        SigningKeyPair TestKeyPair()
        {
            return Lic.KeyGenerator.GenerateRsaKeyPair();
            //Console.WriteLine(pair.PrivateKey);
            //Console.WriteLine(pair.PublicKey);
        }
    }

    internal class WindowsComputerIdentifierCreator : IComputerCharacteristics
    {
        public IEnumerable<string> GetCharacteristicsForCurrentComputer()
        {
            yield return GetValue("ProcessorID", "Win32_Processor");
            yield return GetValue("SerialNumber", "Win32_BIOS");
            yield return GetValue("SerialNumber", "Win32_BaseBoard");
            yield return GetValue("SerialNumber", "Win32_PhysicalMedia");
        }

        private static string GetValue(string property, string type)
        {
            var sb = new StringBuilder();
            try
            {
                var searcher = new ManagementObjectSearcher($"select {property} from {type}");
                foreach (var share in searcher.Get())
                    foreach (PropertyData pc in share.Properties)
                        sb.Append(pc.Value);
            }
            catch
            {
                sb.Append(property).Append(type);
            }
            return sb.ToString();
        }
    }

    /// <summary>
    /// Class HardwareInfo.
    /// </summary>
    internal class HardwareInfo
    {

        /// <summary>
        /// Get motherboard serial number
        /// </summary>
        /// <returns>System.String.</returns>
        static string GetMotherboardID()
        {
            try
            {
                var _mbs = new ManagementObjectSearcher("Select SerialNumber From Win32_BaseBoard");
                var _mbsList = _mbs.Get();
                var _id = string.Empty;
                foreach (ManagementObject _mo in _mbsList)
                {
                    _id = _mo["SerialNumber"].ToString();
                    break;
                }

                return _id;
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Get CPU ID
        /// </summary>
        /// <returns>System.String.</returns>
        static string GetProcessorId()
        {
            try
            {
                var _mbs = new ManagementObjectSearcher("Select ProcessorId From Win32_processor");
                var _mbsList = _mbs.Get();
                var _id = string.Empty;
                foreach (ManagementObject _mo in _mbsList)
                {
                    _id = _mo["ProcessorId"].ToString();
                    break;
                }

                return _id;

            }
            catch
            {
                return string.Empty;
            }
        }
        internal static string GetMACAddress()
        {
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_NetworkAdapterConfiguration where IPEnabled=true");
            IEnumerable<ManagementObject> objects = searcher.Get().Cast<ManagementObject>();
            string mac = (from o in objects orderby o["IPConnectionMetric"] select o["MACAddress"].ToString()).FirstOrDefault();

            return mac;
        }

        internal static string GenerateUID()
        {
            return GenerateUID(GetMACAddress());
        }

        /// <summary>
        /// Combine CPU ID, Disk C Volume Serial Number and Motherboard Serial Number as device Id
        /// </summary>
        /// <param name="appName">Name of the application.</param>
        /// <returns>System.String.</returns>
        internal static string GenerateUID(string appName)
        {
            //Combine the IDs and get bytes
            var _id = $"{appName}.{GetProcessorId()}{GetMotherboardID()}";
            var _byteIds = Encoding.UTF8.GetBytes(_id);

            //Use MD5 to get the fixed length checksum of the ID string
            var _md5 = new MD5CryptoServiceProvider();
            var _checksum = _md5.ComputeHash(_byteIds);

            //Convert checksum into 4 ulong parts and use BASE36 to encode both
            var _part1Id = BASE36.Encode(BitConverter.ToUInt32(_checksum, 0));
            var _part2Id = BASE36.Encode(BitConverter.ToUInt32(_checksum, 4));
            var _part3Id = BASE36.Encode(BitConverter.ToUInt32(_checksum, 8));
            var _part4Id = BASE36.Encode(BitConverter.ToUInt32(_checksum, 12));

            //Concat these 4 part into one string
            return $"{_part1Id}-{_part2Id}-{_part3Id}-{_part4Id}".Substring(0, 28);
        }

        /// <summary>
        /// Gets the uid in bytes.
        /// </summary>
        /// <param name="UID">The uid.</param>
        /// <returns>System.Byte[].</returns>
        /// <exception cref="ArgumentException">Wrong UID</exception>
        /// <exception cref="System.ArgumentException">Wrong UID</exception>
        internal static byte[] GetUIDInBytes(string UID)
        {
            //Split 4 part Id into 4 ulong
            var _ids = UID.Split('-');

            if (_ids.Length != 4)
            {
                throw new ArgumentException("Wrong UID");
            }

            //Combine 4 part Id into one byte array
            var _value = new byte[16];
            Buffer.BlockCopy(BitConverter.GetBytes(BASE36.Decode(_ids[0])), 0, _value, 0, 8);
            Buffer.BlockCopy(BitConverter.GetBytes(BASE36.Decode(_ids[1])), 0, _value, 8, 8);
            Buffer.BlockCopy(BitConverter.GetBytes(BASE36.Decode(_ids[2])), 0, _value, 16, 8);
            Buffer.BlockCopy(BitConverter.GetBytes(BASE36.Decode(_ids[3])), 0, _value, 24, 8);

            return _value;
        }

        /// <summary>
        /// Validates the uid format.
        /// </summary>
        /// <param name="UID">The uid.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        internal static bool ValidateUIDFormat(string UID)
        {
            if (!string.IsNullOrWhiteSpace(UID))
            {
                var _ids = UID.Split('-');

                return (_ids.Length == 4);
            }
            else
            {
                return false;
            }
        }
    }

    internal class BASE36
    {
        /// <summary>
        /// The character list
        /// </summary>
        const string _charList = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        /// <summary>
        /// The character array
        /// </summary>
        static readonly char[] _charArray = _charList.ToCharArray();

        /// <summary>
        /// Reverses the specified s.
        /// </summary>
        /// <param name="s">The s.</param>
        /// <returns>System.String.</returns>
        static string Reverse(string s)
        {
            var charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }

        /// <summary>
        /// Decodes the specified input.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns>System.Int64.</returns>
        internal static long Decode(string input)
        {
            var _result = 0L;
            var _pow = 0d;
            for (var _i = input.Length - 1; _i >= 0; _i--)
            {
                var _c = input[_i];
                var pos = _charList.IndexOf(_c);
                if (pos > -1)
                {
                    _result += pos * (long)Math.Pow(_charList.Length, _pow);
                }
                else
                {
                    return -1;
                }

                _pow++;
            }
            return _result;
        }

        /// <summary>
        /// Encodes the specified input.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns>System.String.</returns>
        internal static string Encode(ulong input)
        {
            var _sb = new StringBuilder();
            do
            {
                _sb.Append(_charArray[input % (ulong)_charList.Length]);
                input /= (ulong)_charList.Length;
            } while (input != 0);

            return Reverse(_sb.ToString());
        }
    }
}
