using System.Threading;
using System.Windows.Forms;

namespace ZChangerMMO
{
    /// <summary>
    ///
    /// </summary>
    public class SplashManager : ISplashHost
    {
        SplashManager() { }

        #region ISplashHost Members

        /// <summary>
        /// Gets the splash dialog.
        /// </summary>
        /// <value>The splash dialog.</value>
        public frmSplashDialog SplashDialog { get; private set; }

        ISplashDialog ISplashHost.SplashDialog => SplashDialog;

        /// <summary>
        ///
        /// </summary>
        /// <param name="maxSteps"></param>
        public void ShowSplashDialog(int maxSteps)
        {
            if((SplashDialog == null) || SplashDialog.IsDisposed || !SplashDialog.Running)
            {
                SplashDialog = new frmSplashDialog();
                // load splash screen
                ThreadPool.QueueUserWorkItem(delegate
                {
                    SplashDialog.SetLoadSteps(maxSteps);
                    SplashDialog.Show();

                    while(SplashDialog.Running)
                        Application.DoEvents();

                    SplashDialog.Close();
                });
            }
        }

        /// <summary>
        ///
        /// </summary>
        public void ShowSplashDialog() => ShowSplashDialog(0);

        /// <summary>
        ///
        /// </summary>
        public void CloseSplashDialog()
        {
            if((SplashDialog != null) && !SplashDialog.IsDisposed && SplashDialog.Running)
                SplashDialog.Running = false;
        }

            #endregion

        #region static members

        /// <summary>
        ///
        /// </summary>
        static SplashManager _instance;

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <value>The instance.</value>
        public static SplashManager Instance
        {
            get
            {
                if(_instance == null)
                    _instance = new SplashManager();
                return _instance;
            }
        }
        #endregion
    }
}