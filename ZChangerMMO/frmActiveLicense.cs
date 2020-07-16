using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZChangerMMO.Events;
using System.Deployment.Application;
using System.Reflection;

namespace ZChangerMMO
{
    internal partial class frmActiveLicense : Form
    {
        int _count = 0;
        ZLicense _licenceEngine { get; set; }
        bool _cancelClose = true;

        public string LicenseKey;
        public bool LicenseOK;

        public frmActiveLicense(ZLicense licenceEngine, string formTitle)
        {
            InitializeComponent();
            LicenseKey = string.Empty;
            LicenseOK = false;

            _licenceEngine = licenceEngine;
            txt_MachineId.Text = licenceEngine.GetRequestLicense();
            Text += $"ZChanger MMO v{CurrentVersion} - {formTitle}";
        }

        string CurrentVersion
        {
            get
            {
                return ApplicationDeployment.IsNetworkDeployed
                       ? ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString()
                       : Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
        }

        private void RequestLicenceForm_Load(object sender, EventArgs e)
        {
            mmeLicenseKey.SelectAll();
            mmeLicenseKey.Focus();

            if (!string.IsNullOrEmpty(_licenceEngine.LicenseKey))
            {
                mmeLicenseKey.Text = _licenceEngine.LicenseKey;
            }
//#if DEBUG
//            mmeLicenseKey.Text = _licenceEngine.GenerateLicense(_licenceEngine.GetRequestLicense(),
//                expirationDate: new DateTime(2020, 05, 30),
//                issueDate: DateTime.Now,
//                userName: "Connor",
//                supportedApps: "",
//                supportedDevices: "");
//#endif
        }

        private void RequestLicenceForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_cancelClose)
            {
                e.Cancel = true;
            }
        }

        private void btnActive_Click(object sender, EventArgs e)
        {
            var inputLicense = mmeLicenseKey.Text.Trim();
            if (!string.IsNullOrEmpty(inputLicense))
            {
                var licenseMessage = _licenceEngine.VerifyLicense(inputLicense);
                if (!string.IsNullOrEmpty(licenseMessage))
                {
                    XtraMessageBox.Show($"Your license is {licenseMessage}!\n\nPlease try again or contact cuong.tran@starssolution", "Licence error", MessageBoxButtons.OK);
                    _count += 1;
                    if (_count > 3)
                    {
                        _cancelClose = false;
                        
                        DialogResult = DialogResult.OK;
                        Close();
                    }
                    else
                    {
                        _cancelClose = true;
                    }
                }
                else
                {
                    _cancelClose = false;
                    _licenceEngine.SaveLicenseKey(inputLicense);
                    LicenseKey = inputLicense;
                    LicenseOK = true;

                    DialogResult = DialogResult.Cancel;
                    Close();
                }
            }
            else
            {
                _cancelClose = true;
                mmeLicenseKey.Focus();
            }
        }
    }
}
