using CommandModel;

namespace ZChangerMMO.DataModels
{
    public class Device
    {
        public Device(string name, string userAgent, Screen screen, CPU cPU, Battery battery)
        {
            this.Name = name;
            this.UserAgent = userAgent;
            this.Screen = screen;
            this.CPU = cPU;
            this.Battery = battery;
        }

        public string Name { get; set; }

        public string UserAgent { get; set; }

        public Screen Screen { get; set; }

        public CPU CPU { get; set; }

        public Battery Battery { get; set; }
    }
}
