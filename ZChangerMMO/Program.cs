using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Threading;
using System.Windows.Forms;
using ZChangerMMO.Licensing.Licensing;

namespace ZChangerMMO
{
    static class Program
    {
        static Automation auto { get; set; }

        /// <summary>
        /// The start up step count
        /// </summary>
        static readonly int _startUpStepCount = 8;

        [STAThread]
        static void Main(string[] args)
        {      
            SetupApplicationhandlers();
            
            AppDomain.CurrentDomain.SetData("BrowserData", Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Browser_Data"));
            auto = new Automation();
            auto.ParseCommandLineArgs(args);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // show splash
            ShowSplash();
            SplashManager.SplashDialog.IncrementLoadStep(1);
            SplashManager.SplashDialog.SetStepText("Starting..");
            
            if (IsConnectedToInternet())
            {
                var licenceEngine = new ZLicense();
                var licenseKey = licenceEngine.LicenseKey;

                if (auto.Enabled())
                {
                    if (string.IsNullOrEmpty(licenceEngine.VerifyLicense(licenseKey)))
                    {
                        licenceEngine = new ZLicense(licenseKey);
                       
                        RunApp(licenceEngine);
                    }
                }
                else
                {
                    var licenseMsg = string.Empty;
                    bool licenseOK = false;

                    SplashManager.SplashDialog.IncrementLoadStep(1);
                    SplashManager.SplashDialog.SetStepText("Checking license..");

                    if (string.IsNullOrEmpty(licenseKey))
                    {
                        licenseOK = false;
                    }
                    else
                    {
                        licenseMsg = licenceEngine.VerifyLicense(licenseKey);
                        licenseOK = string.IsNullOrEmpty(licenseMsg);
                    }

                    if (!licenseOK)
                    {
                        SplashManager.SplashDialog.HideExt();
                        var requestLicenceForm = new frmActiveLicense(licenceEngine, licenseMsg);
                        var lcFormResult = requestLicenceForm.ShowDialog();
                        if (lcFormResult != DialogResult.OK)
                        {
                            return;
                        }
                        else if (requestLicenceForm.LicenseOK)
                        {
                            licenseOK = true;
                        }

                        // bring it back.
                        SplashManager.SplashDialog.AsyncShow();
                        SplashManager.SplashDialog.IncrementLoadStep(1);
                    }

                    if (licenseOK)
                    {
                        SplashManager.SplashDialog.IncrementLoadStep(1);
                        licenceEngine = new ZLicense(licenseKey);

                        if (IsRunAsAdmin())
                        {
                            RunApp(licenceEngine);
                        }
                        else
                        {
                            MessageBox.Show("This application requires elevated credentials in order to operate correctly!", "Administrator privileges requires", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Vilas license server manage could not determine!", "Internet requires", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        static void RunApp(ZLicense lic)
        {
            if (auto.Enabled())
            {
                if (auto.ShouldBackup())
                {
                    // Run backup automation
                    auto.RunBackup();
                }
                else
                {
                    // Restore and run browser automation
                    if (auto.ShouldRestore())
                    {
                        auto.RunRestore();
                    }

                    if (auto.ShouldRandom())
                    {
                        auto.RandomProfile();
                    }
                    auto.RunBrowser();
                    Thread.Sleep(10000);
                }
            }
            else
            {
                SplashManager.SplashDialog.IncrementLoadStep(1);
                SplashManager.SplashDialog.SetStepText("......");

                Application.Run(new frmMain(lic));
            }
        }

        /// <summary>
        /// Gets the splash manager.
        /// </summary>
        /// <value>The splash manager.</value>
        internal static SplashManager SplashManager => SplashManager.Instance;

        /// <summary>
        /// Shows the splash.
        /// </summary>
        static void ShowSplash() => SplashManager.ShowSplashDialog(_startUpStepCount);

        /// <summary>
        /// Setups the applicationhandlers.
        /// </summary>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        static bool SetupApplicationhandlers()
        {
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            Application.ApplicationExit += delegate
            {
                // open browser? should this be closed?
                // if (Automation.Enabled())
                {
                    // Automation.StopBrowser();
                }
            };

            return true;
        }

        [DllImport("wininet.dll")]
        static extern bool InternetGetConnectedState(out int Description, int ReservedValue);
        static bool IsConnectedToInternet()
        {
            int num;
            return InternetGetConnectedState(out num, 0);
        }

        private static bool IsRunAsAdmin()
        {
            var id = WindowsIdentity.GetCurrent();
            var principal = new WindowsPrincipal(id);

            return principal.IsInRole(WindowsBuiltInRole.Administrator);
        }
    }
}
