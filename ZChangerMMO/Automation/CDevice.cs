using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZChangerMMO.DeviceInfo
{
    //public enum DeviceTypes
    //{
    //    Windows10 = 1,
    //    MacOS_Sierra = 20,
    //    MacOS_HighSierra = 21,
    //    MacOS_Mojave = 22,
    //    MacOS_Catalina = 23,
    //    iOS_10 = 30,
    //    iOS_11 = 31,
    //    iOS_12 = 32,
    //    iOS_13 = 33,
    //    Android_Nougat = 40,
    //    Android_Oreo = 40,
    //    Android_Nougat = 40,
    //    Android_Nougat = 40,
    //    Android_Nougat = 40,
    //    Android_Nougat = 40,
    //    Android_Nougat = 40,
    //    Android_Nougat = 40,
    //}

    public enum DeviceTypes
    {
        PC = 1,
        MobilePhone = 2,
        Tablet = 3
    }

    public enum DeviceModels
    {
        #region iPhone //https://en.wikipedia.org/wiki/IPhone
        iPhone_8 = 101,
        iPhone_8_Plus = 102,

        iPhone_X = 103,
        iPhone_XS = 104,
        iPhone_XS_Max = 105,
        iPhone_XR = 106,

        iPhone_11 = 107,
        iPhone_11_Pro = 108,
        iPhone_11_Pro_Max = 109,

        iPhone_SE_2nd = 110,
        #endregion iPhone

        #region iPad //https://en.wikipedia.org/wiki/IPad
        iPad_5 = 201,

        iPad_Pro_2nd_129 = 202,
        iPad_Pro_2nd_105 = 203,

        iPad_Pro_3rd = 204,

        iPad_Mini_5 = 205,

        iPad_7 = 206,

        iPad_Pro_4th_129 = 207,
        iPad_Pro_4th_110 = 208,
        #endregion iPad

        #region Macbook Pro //https://en.wikipedia.org/wiki/MacBook
        Macbook_Pro_13 = 301,
        Macbook_Pro_16 = 302,
        #endregion Macbook Pro

        #region Dell Laptop //https://en.wikipedia.org/wiki/Dell_laptops
        Dell_Inspiron = 401,
        Dell_XPS = 402,
        Dell_Vostro = 403,
        #endregion Dell Laptop

        #region Samsung Galaxy //https://en.wikipedia.org/wiki/Samsung_Galaxy
        Samsung_Galaxy_S8 = 501,
        Samsung_Galaxy_S8_Edge = 502,

        Samsung_Galaxy_S9 = 503,
        Samsung_Galaxy_S9_Plus = 504,

        Samsung_Galaxy_S10e = 505,
        Samsung_Galaxy_S10_Plus = 506,

        Samsung_Galaxy_S20 = 507,
        Samsung_Galaxy_S20_Ultra = 508,

        #endregion Samsung Galaxy

        #region Xiaomi Mi Note //https://en.wikipedia.org/wiki/List_of_Xiaomi_products#Mi_Note_Series
        Mi_Note_10 = 601,
        Mi_Note_10_Pro = 602,
        Mi_Note_10_Lite = 603,

        Mi_Max_3 = 604,
        Mi_Mix_3 = 605,
        Mi_Mix_Alpha = 606,

        Mi_8 = 607,
        Mi_8_EE = 608,
        Mi_8_SE = 609,

        Mi_9 = 610,
        Mi_9_SE = 611,
        #endregion Xiaomi Mi Note
    }

    public class OSVersion
    {
        public string Name { get; set; }
        public string Version { get; set; }
        public DateTime InitialReleaseDate { get; set; }
        public string APILevel { get; set; }
    }

    public class DeviceDimensions
    {
        public int H { get; set; }
        public int W { get; set; }
        public int D { get; set; }
    }

    public class ScreenSize
    {
        public string Name { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

    }

    public class Device
    {
        public DeviceTypes Type { get; set; }
        public DeviceModels Model { get; set; }
        public OSVersion ReleaseWithOS { get; set; }
        public DateTime ReleaseDate { get; set; }
        public DateTime Discontinued { get; set; }
        public DateTime Ended { get; set; }
        public OSVersion FinalOS { get; set; }

        public virtual List<Device> AllDevices()
        {
            return new List<Device>()
            {
                #region iPhone
                new Device()
                {
                    Type = DeviceTypes.MobilePhone,
                    Model = DeviceModels.iPhone_8,
                    ReleaseWithOS = new OSVersion()
                    {
                        Name = "iOS 11.0",
                        Version = "11.0",
                        InitialReleaseDate = new DateTime(2017, 09, 22),
                        APILevel = string.Empty
                    },
                    ReleaseDate = new DateTime(2017, 09, 22),
                    Discontinued = new DateTime(2020, 04, 15),
                    Ended = new DateTime(2020, 04, 15),
                    FinalOS = new OSVersion()
                    {
                        Name = "iOS 13.4.1",
                        Version = "13.4.1",
                        InitialReleaseDate = new DateTime(2020, 04, 29),
                        APILevel = string.Empty
                    },
                },
                #endregion iPhone
                new Device()
                {

                },
            };
        }

    }
}
