using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace ZChangerMMO
{
    public partial class frmSplashDialog : DevExpress.XtraEditors.XtraForm, ISplashDialog
    {
        /// <summary>
        /// 
        /// </summary>
        public frmSplashDialog()
        {
            Running = true;

            InitializeComponent();

            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            BackColor = Color.Fuchsia;
            TransparencyKey = Color.Fuchsia;
            //logo.Image = Images.android192;
            //title.Image = Images.zchanger_title_new;
            //version.ForeColor = Color.FromArgb(255, 0, 192, 0);
            //status.ForeColor = Color.FromArgb(255, 0, 192, 0);
            version.Text = string.Format(CultureInfo.InvariantCulture,
                "Version {0}",
                Application.ProductVersion);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs"/> that contains the event data.</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            // e.Graphics.DrawImage(Images.splash_background, 0, 0, Width, Height);
            //ControlPaint.DrawBorder3D ( e.Graphics, this.ClientRectangle, Border3DStyle.Raised );
        }

        #region ISplashDialog Members

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="ISplashDialog"/> is running.
        /// </summary>
        /// <value><c>true</c> if running; otherwise, <c>false</c>.</value>
        public bool Running { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public void SetLoadSteps(int value)
        {
            try
            {
                progress.Properties.Minimum = 0;
                progress.Properties.Maximum = value;

                progress.EditValue = 0;
            }
            catch (System.Exception)
            {
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public void IncrementLoadStep(int value) => progress.IncrementExt(value);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        public void SetStepText(string text) => status.SetText(text);
        #endregion
    }
}