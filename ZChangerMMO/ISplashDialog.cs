using System.Windows.Forms;

namespace ZChangerMMO
{
    /// <summary>
    /// Interface ISplashDialog
    /// </summary>
    public interface ISplashDialog
    {
        /// <summary>
        /// Gets a value indicating whether this instance is disposed.
        /// </summary>
        /// <value><c>true</c> if this instance is disposed; otherwise, <c>false</c>.</value>
        bool IsDisposed { get; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="ISplashDialog" /> is running.
        /// </summary>
        /// <value><c>true</c> if running; otherwise, <c>false</c>.</value>
        bool Running { get; set; }

        /// <summary>
        /// Closes this instance.
        /// </summary>
        void Close();

        /// <summary>
        /// Hides this instance.
        /// </summary>
        void Hide();

        /// <summary>
        /// Increments the load step.
        /// </summary>
        /// <param name="value">The value.</param>
        void IncrementLoadStep(int value);

        /// <summary>
        /// Sets the load steps.
        /// </summary>
        /// <param name="value">The value.</param>
        void SetLoadSteps(int value);

        /// <summary>
        /// Sets the step text.
        /// </summary>
        /// <param name="text">The text.</param>
        void SetStepText(string text);

        /// <summary>
        /// Shows this instance.
        /// </summary>
        void Show();

        /// <summary>
        /// Shows the specified owner.
        /// </summary>
        /// <param name="owner">The owner.</param>
        void Show(IWin32Window owner);

        /// <summary>
        /// Shows the dialog.
        /// </summary>
        /// <returns>DialogResult.</returns>
        DialogResult ShowDialog();

        /// <summary>
        /// Shows the dialog.
        /// </summary>
        /// <param name="owner">The owner.</param>
        /// <returns>DialogResult.</returns>
        DialogResult ShowDialog(IWin32Window owner);
    }
}