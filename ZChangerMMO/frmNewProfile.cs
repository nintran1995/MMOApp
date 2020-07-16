using CommandModel;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Windows.Forms;
using ZChangerMMO.DataModels;
using ZChangerMMO.Events;

namespace ZChangerMMO
{
    public partial class frmNewProfile : Form
    {
        public frmNewProfile() => InitializeComponent();

        public event EventHandler<AddNewProfileEventArgs> NewProfileAction;

        void btn_Cancel_Click(object sender, EventArgs e) => Close();

        bool IsEmailValid(string emailaddress)
        {
            try
            {
                MailAddress m = new MailAddress(emailaddress);

                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }
        string validateData()
        {
            if (string.IsNullOrWhiteSpace(txt_Name.Text))
            {
                return "Name is required!";
            }
            if (string.IsNullOrWhiteSpace(txt_Email.Text) || !IsEmailValid(txt_Email.Text))
            {
                return "Email is empty or invalid!";
            }

            return string.Empty;
        }

        void btn_Save_Click(object sender, EventArgs e)
        {
            try
            {
                var errorMsg = validateData();
                if (!string.IsNullOrWhiteSpace(errorMsg))
                {
                    XtraMessageBox.Show(errorMsg, "Invalid data", MessageBoxButtons.OK);
                    this.DialogResult = DialogResult.None;
                    return;
                }


                List<Proxy> proxies = new List<Proxy>();
                //proxies.Add(new Proxy() {
                //    Name = "Austria",
                //    Host = "217.196.81.221",
                //    Port = "50579",
                //    Scheme = "socks4"
                //});
                //proxies.Add(new Proxy()
                //{
                //    Name = "Amenia",
                //    Host = "212.34.239.253",
                //    Port = "1080",
                //    Scheme = "socks4"

                //}); 
                //proxies.Add(new Proxy()
                //{
                //    Name = "Japan",
                //    Host = "163.43.30.234",
                //    Port = "1080",
                //    Scheme = "socks4"
                //});
                using (SQLiteProfileDbContext dbContext = new SQLiteProfileDbContext())
                {
                    Profile newProfile = new Profile
                    {
                        Name = txt_Name.Text,
                        Email = txt_Email.Text,
                        Description = txt_Description.Text,

                        #region Device
                        CPU = new CPU { DeviceMemory = 16, HardwareConcurrency = 8 },
                        Battery = new Battery { Charging = true, ChargingTime = 1486, DischargingTime = 1328, Level = 0.52 },
                        EnableAudioApi = false,
                        EnablePlugins = false,
                        EnableMediaPlugins = false,
                        #endregion Device

                        Fonts = Fonts.Windows10,

                        #region Content
                        RandomTimersEnabled = true,
                        UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/68.0.3440.106 Safari/537.36",
                        Screen = new CommandModel.Screen { Width = 1680, Height = 1050, Color = 24 },
                        HistoryLength = 2,
                        #endregion Content

                        #region Fingerprint
                        WebGL = new WebGL { Plus1 = 1007, Plus2 = 25502, Plus3 = 31603, Plus4 = 42975, Plus5 = 12677, BrowserplugsR = 88 },
                        FakeClientRects = true,
                        Canvas = new Canvas { R = 5, G = 4, B = 1 },
                        #endregion Fingerprint

                        #region Geo
                        EnableNetwork = true,
                        Language = "en",
                        GeoIpEnabled = true,
                        #endregion Geo
                        #region Proxy
                        ProxyEnabled = true,
                        Proxies = proxies,
                        ByPassProxySites = new List<string>()
                        #endregion
                    };

                    dbContext.Profiles.Add(newProfile);
                    dbContext.SaveChanges();
                }

                EventHandler<AddNewProfileEventArgs> handler = NewProfileAction;

                if (handler != null)
                {
                    NewProfileAction(this, new AddNewProfileEventArgs(AddNewProfileActionType.ADD_SUCCESS, "Add new profile successfully"));
                }

                Close();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Save Config").ToString();
            }
        }

        void NewProfile_Load(object sender, EventArgs e) => txt_Name.Focus();

        private void NewProfile_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult == DialogResult.None)
                e.Cancel = true;
        }
    }
}
