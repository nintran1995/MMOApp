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
using ZChangerMMO.Model;
using ZChangerMMO.Utility;

namespace ZChangerMMO
{
    public partial class Licence : Form
    {
        public event EventHandler<ApplyNewLicenceEventArgs> OKAction;
        public event EventHandler<EventArgs> CancelAction;
        private string MachineId;
        private string CurrentLicence;
        private ZLicense LicenceEngine { get; set; }
        public Licence(ZLicense licenceEngine)
        {
            InitializeComponent();
            LicenceEngine = licenceEngine;
        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            try
            {
                string licence = this.txt_Key.Text;
                if (string.IsNullOrEmpty(licence))
                {
                    XtraMessageBox.Show("Input key is required", "Licence", MessageBoxButtons.OK);
                    this.DialogResult = DialogResult.None;
                    return;
                }

                if (LicenceEngine.VerifyLicense(licence) != "")
                {
                    XtraMessageBox.Show(LicenceEngine.VerifyLicense(licence), "Licence", MessageBoxButtons.OK);
                    this.DialogResult = DialogResult.None;
                    return;
                }

                if (OKAction != null)
                {
                    OKAction(this, null);
                }

                Close();
            }
            catch(Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Error when apply licence key", MessageBoxButtons.OK);
                this.DialogResult = DialogResult.None;
            }
        }

        private void Licence_Load(object sender, EventArgs e)
        {
            try
            {
                CurrentLicence = LicenceEngine.GetLicenseKey();
                MachineId = LicenceEngine.GetRequestLicense();
                this.txt_Id.Text = MachineId;
                this.txt_Key.Text = CurrentLicence;
                string productLicenceStatus = Helper.GetProductLicenceStatus(LicenceEngine.VerifyLicense(CurrentLicence));
                lbl_LicenceStatus.Text = productLicenceStatus;
            }
            catch(Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK);
            }
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            if (CancelAction != null)
            {
                CancelAction(this, null);
            }
            Close();
        }
    }
}
