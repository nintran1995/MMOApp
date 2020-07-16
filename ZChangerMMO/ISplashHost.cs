namespace ZChangerMMO
{
    /// <summary>
    /// Interface ISplashHost
    /// </summary>
    public interface ISplashHost
    {
        /// <summary>
        /// Gets the splash dialog.
        /// </summary>
        /// <value>The splash dialog.</value>
        ISplashDialog SplashDialog { get; }

        /// <summary>
        /// Closes the splash dialog.
        /// </summary>
        void CloseSplashDialog();

        /// <summary>
        /// Shows the splash dialog.
        /// </summary>
        void ShowSplashDialog();

        /// <summary>
        /// Shows the splash dialog.
        /// </summary>
        /// <param name="maxSteps">The maximum steps.</param>
        void ShowSplashDialog(int maxSteps);
    }
}