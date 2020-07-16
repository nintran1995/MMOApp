using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZChangerMMO.DataModels;
using ZChangerMMO.Model;

namespace ZChangerMMO
{
    internal static class Extensions
    {
        internal static IList<T> Clone<T>(this IList<T> listToClone) where T : ICloneable
        {
            return listToClone.Select(item => (T)item.Clone()).ToList();
        }

        internal static ProfileXML ToProfileXML(this Profile profile)
        {
            ProfileXML profileXML = new ProfileXML()
            {
                Description = profile.Description,
                Email = profile.Email,
                Fonts = profile.Fonts,
                Name = profile.Name,
                OperatingSystem = profile.OperatingSystem,
                CPU = profile.CPU,
                Battery = profile.Battery,
                EnableAudioApi = profile.EnableAudioApi,
                EnablePlugins = profile.EnablePlugins,
                EnableMediaPlugins = profile.EnableMediaPlugins,
                RandomTimersEnabled = profile.RandomTimersEnabled,
                UserAgent = profile.UserAgent,
                Screen = profile.Screen,
                HistoryLength = profile.HistoryLength,
                WebGL = profile.WebGL,
                FakeClientRects = profile.FakeClientRects,
                Canvas = profile.Canvas,
                EnableNetwork = profile.EnableNetwork,
                Language = profile.Language,
                GeoIpEnabled = profile.GeoIpEnabled,
                ByPassProxySites = profile.ByPassProxySites,
                Proxies = profile.Proxies,
            };

            return profileXML;
        }
    }
}
