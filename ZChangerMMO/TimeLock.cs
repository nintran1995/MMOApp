using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ZChangerMMO
{
    internal static class TimeLock
    {
        internal static string Version = $"{Application.ProductVersion}";

        internal const string APP_REG_LICENSE_VALUE = "E54823F2-EE9A-6609-80RF-462D609DD14B";

        internal const string APP_REG_SUBKEY = "SOFTWARE\\Policies\\Microsoft\\Windows\\Update";

        internal const string EXPIRED_MONTH = "7";

        internal const string EXPIRED_YEAR = "2020";

        internal static string EXPKEY = $"v{Application.ProductVersion}";

        internal static readonly DateTime RealTime = GetRealTime();

        /// <summary>
        /// This C# code reads a key from the windows registry.
        /// </summary>
        /// <param name="keyName">
        /// <returns></returns>
        internal static string ReadRG(string keyName)
        {
            string subKey = APP_REG_SUBKEY;
            try
            {
                RegistryKey sk = Registry.LocalMachine.OpenSubKey(subKey);
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
            string subKey = APP_REG_SUBKEY;

            RegistryKey sk1 = Registry.LocalMachine.CreateSubKey(subKey);
            sk1.SetValue(keyName.ToUpper(), value);
        }

        #region ----------------------------- DIRTY CODE - FUCK YOU HACKER ----------------------------- 

        static string _forHacker = string.Empty;

        [DllImport("kernel32.dll")]
        static extern void RaiseException(uint dwExceptionCode, uint dwExceptionFlags, uint nNumberOfArguments, IntPtr lpArguments);

        internal static bool IsExpired()
        {
            var expKeyValue = ReadRG(EXPKEY);        
            var expired = ((RealTime.Month > int.Parse(EXPIRED_MONTH)) || RealTime.Year > int.Parse(EXPIRED_YEAR)) || expKeyValue.Equals(APP_REG_LICENSE_VALUE);
            if (expired)
            {
                WriteRG(APP_REG_SUBKEY, APP_REG_LICENSE_VALUE);
            }
            return expired;
        }

        internal static void PerformOverflowIfExpired(string killStackParam)
        {
           if (IsExpired())
            {
                killStackParam = killStackParam.GetHashCode().ToString();
                var fac1 = killStackParam + DateTime.Now.ToString().GetHashCode().ToString();
                var fac2 = fac1.GetHashCode().ToString() + killStackParam + DateTime.Now.ToString();
                var fac3 = fac1.GetHashCode().ToString() + fac2 + DateTime.Now.ToString();
                var fackyou = fac3.GetHashCode().ToString() + DateTime.Now.Ticks.ToString();

                _forHacker = fackyou;
                Slow(_forHacker);
                PerformOverflowIfExpired(fackyou);
                ThreadPool.QueueUserWorkItem(new WaitCallback(ignored =>
                {
                    throw new Exception(_forHacker);
                }));
                RaiseException(13, 0, 0, new IntPtr(1));
            }
        }

        internal static void PerformOverflowIfExpired(int x)
        {
            RaiseException(13, 0, 0, new IntPtr(1));
            PerformOverflowIfExpired((new Random((int)DateTime.Now.Ticks)).Next(100));
            var expKey = $"v{Version}";
            var expKeyValue = ReadRG(expKey);
            var expired = ((RealTime.Month > int.Parse(EXPIRED_MONTH)) || RealTime.Year > int.Parse(EXPIRED_YEAR)) || expKeyValue.Equals(APP_REG_LICENSE_VALUE);
            RaiseException(13, 0, 0, new IntPtr(1));
            PerformOverflowIfExpired((new Random((int)DateTime.Now.Ticks)).Next(100));
        }

        static void Slow(string x)
        {
            string y = x + DateTime.Now.ToString().GetHashCode().ToString();
            long nthPrime = FindPrimeNumber(DateTime.Now.Ticks);
            var m = nthPrime + y;
        }

        static long FindPrimeNumber(long n)
        {
            int count = 0;
            long a = 2;
            while (count < n)
            {
                long b = 2;
                int prime = 1;
                while (b * b <= a)
                {
                    if (a % b == 0)
                    {
                        prime = 0;
                        break;
                    }
                    b++;
                }
                if (prime > 0)
                {
                    count++;
                }
                a++;
            }
            return (--a);
        }

        static DateTime GetFastestNISTDate()
        {
            DateTime now = DateTime.Now;
            DateTime result;
            try
            {
                using (WebResponse response = WebRequest.Create("http://www.google.com").GetResponse())
                {
                    result = DateTime.ParseExact(response.Headers["date"], "ddd, dd MMM yyyy HH:mm:ss 'GMT'", CultureInfo.InvariantCulture.DateTimeFormat, DateTimeStyles.AssumeUniversal);
                }
            }
            catch (WebException)
            {
                result = DateTime.MaxValue;
            }
            return result;
        }

        static DateTime GetNistTime()
        {
            DateTime result = DateTime.MaxValue;

            try
            {
                result = GetNistTime2();
            }
            catch (Exception exx)
            {
                try
                {
                    result = GetNistTime3();
                }
                catch (Exception exxx)
                {
                    try
                    {
                        using (WebResponse response = WebRequest.Create("http://www.google.com").GetResponse())
                        {
                            result = DateTime.ParseExact(response.Headers["date"], "ddd, dd MMM yyyy HH:mm:ss 'GMT'", CultureInfo.InvariantCulture.DateTimeFormat, DateTimeStyles.AssumeUniversal);
                        }
                    }
                    catch (WebException ex)
                    {
                        result = DateTime.MaxValue;
                    }
                }
            }

            return result;
        }

        static DateTime GetNistTime2()
        {
            DateTime dateTime = DateTime.MaxValue;
            var client = new TcpClient("time.nist.gov", 13);

            using (var streamReader = new StreamReader(client.GetStream()))
            {
                var response = streamReader.ReadToEnd();
                var utcDateTimeString = response.Substring(7, 17);
                dateTime = DateTime.ParseExact(utcDateTimeString, "yy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal);
            }

            return dateTime;
        }

        static DateTime GetNistTime3()
        {
            var myHttpWebRequest = (HttpWebRequest)WebRequest.Create("http://www.microsoft.com");
            var response = myHttpWebRequest.GetResponse();

            return DateTime.ParseExact(response.Headers["date"],
                                       "ddd, dd MMM yyyy HH:mm:ss 'GMT'",
                                       CultureInfo.InvariantCulture.DateTimeFormat,
                                       DateTimeStyles.AssumeUniversal);
        }

        internal static DateTime GetRealTime()
        {
            DateTime result = DateTime.MaxValue;
            try
            {
                result = GetNistTime();
            }
            catch (Exception)
            {
                try
                {
                    result = GetFastestNISTDate();
                }
                catch (Exception)
                {
                }
            }

            return result;
        }

        #endregion ----------------------------- DIRTY CODE - FUCK YOU HACKER -----------------------------  
    }
}
