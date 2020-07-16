using System;
using System.Collections.Generic;

namespace CommandModel
{
    public class Battery
    {
        public bool Charging { get; set; }
        public int ChargingTime { get; set; }
        public int DischargingTime { get; set; }
        public double Level { get; set; }
    }
    public class CPU
    {
        public int HardwareConcurrency { get; set; }
        public int DeviceMemory { get; set; }
    }
    public enum Fonts
    {
        Windows10 = 1,
        MacOS = 2,
        Windows7 = 3,
        Linux = 4,
        BasicList1 = 5,
        BasicList2 = 6,
        Randomize = 7,
        NoFonts = 8
    }
    public class Screen
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public int Color { get; set; }
    }
    public class WebGL
    {
        public int Plus1 { get; set; }
        public int Plus2 { get; set; }
        public int Plus3 { get; set; }
        public int Plus4 { get; set; }
        public int Plus5 { get; set; }
        public int BrowserplugsR { get; set; }
    }
    public class Canvas
    {
        public int R { get; set; }
        public int G { get; set; }
        public int B { get; set; }

    }

    //public interface ICloneable
    //{

    //}

    [ComplexType]
    public class Proxy : ICloneable
    {
        public string Name { get; set; }
        public string Host { get; set; }
        public string Port { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public Boolean Enabled { get; set; }
        public string Scheme { get; set; }

        public Proxy()
        {
            this.Enabled = true;
        }

        public object Clone()
        {
            return new Proxy()
            {
                Name = this.Name,
                Host = this.Host,
                Port = this.Port,
                UserName = this.UserName,
                Password = this.Password,
                Enabled = this.Enabled,
                Scheme = this.Scheme,
            };
        }
    }

    internal class ComplexTypeAttribute : Attribute
    {
    }

    public class BrowserProfile
    {
        public long Id { get; set; }
        public string Email { get; set; }

        #region Devive
        public CPU CPU;
        public Battery Battery;
        public bool EnableAudioApi { get; set; }
        public bool EnablePlugins { get; set; }
        public bool EnableMediaPlugins { get; set; }
        #endregion Devive

        #region Fonts
        public Fonts Fonts { get; set; }
        #endregion Fonts

        #region Content
        public bool RandomTimersEnabled { get; set; }
        public string UserAgent { get; set; }
        public Screen Screen;
        public int HistoryLength { get; set; }
        #endregion Content

        #region FingerPrint
        public WebGL WebGL;
        public bool FakeClientRects { get; set; }
        public Canvas Canvas;
        #endregion FingerPrint

        #region GEO
        public bool EnableNetwork { get; set; }
        public string Language { get; set; }
        public bool GeoIpEnabled { get; set; }
        #endregion GEO

        #region Proxy
        public bool ProxyEnabled { get; set; }

        public List<Proxy> Proxies { get; set; }

        public List<string> ByPassProxySites { get; set; }
        #endregion

        public BrowserProfile()
        {
        }
    }
}