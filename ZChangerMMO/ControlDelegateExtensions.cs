﻿using System.Threading;
using System.Windows.Forms;

namespace ZChangerMMO
{
    public static class ControlDelegateExtensions
    {
        /// <summary>
        ///     Gets the checked internal.
        /// </summary>
        /// <param name="cb">The cb.</param>
        /// <returns></returns>
        private static bool GetCheckedInternal(CheckBox cb)
        {
            return cb.Checked;
        }

        /// <summary>
        ///     Gets the text internal.
        /// </summary>
        /// <param name="ctrl">The CTRL.</param>
        /// <returns></returns>
        private static string GetTextInternal(Control ctrl)
        {
            return ctrl.Text;
        }

        private static int InternalGetProgressBarValue(ProgressBar pb)
        {
            return pb.Value;
        }

        private static void InternalSetProgressBarMaximum(ProgressBar pb, int max)
        {
            pb.Maximum = max;
        }

        private static void InternalSetProgressBarMinimum(ProgressBar pb, int min)
        {
            pb.Minimum = min;
        }

        private static void InternalSetProgressBarValue(ProgressBar pb, int value)
        {
            pb.Value = value;
        }

        /// <summary>
        ///     Sets the checked internal.
        /// </summary>
        /// <param name="cb">The cb.</param>
        /// <param name="checked">if set to <c>true</c> [@checked].</param>
        private static void SetCheckedInternal(CheckBox cb, bool @checked)
        {
            cb.Checked = @checked;
        }

        /// <summary>
        ///     Sets the text internal.
        /// </summary>
        /// <param name="ctrl">The CTRL.</param>
        /// <param name="text">The text.</param>
        private static void SetTextInternal(Control ctrl, string text)
        {
            ctrl.Text = text;
        }

        /// <summary>
        ///     Shows the ext.
        /// </summary>
        /// <param name="form">The Form.</param>
        public static void AsyncShow(this Form form)
        {
            try
            {
                if (form.InvokeRequired)
                    form.Invoke((GenericDelegate) form.Show);
                else
                    form.Show();
            }
            catch (ThreadAbortException)
            {
            }
        }

        /// <summary>
        ///     Shows the ext.
        /// </summary>
        /// <param name="form">The Form.</param>
        public static void AsyncShow(this Form form, IWin32Window owner)
        {
            try
            {
                if (form.InvokeRequired)
                    form.Invoke((ShowDelegate) delegate(IWin32Window o) { form.Show(o); });
                else
                    form.Show(owner);
            }
            catch (ThreadAbortException)
            {
            }
        }

        /// <summary>
        ///     Shows the ext.
        /// </summary>
        /// <param name="form">The Form.</param>
        public static DialogResult AsyncShowDialog(this Form form)
        {
            try
            {
                if (form.InvokeRequired)
                    return (DialogResult) form.Invoke((EmptyShowDialogDelegate) form.ShowDialog);
                return form.ShowDialog();
            }
            catch (ThreadAbortException)
            {
                return DialogResult.Abort;
            }
        }

        /// <summary>
        ///     Shows the ext.
        /// </summary>
        /// <param name="form">The Form.</param>
        public static DialogResult AsyncShowDialog(this Form form, IWin32Window owner)
        {
            try
            {
                if (form.InvokeRequired)
                    return (DialogResult) form.Invoke((ShowDialogDelegate) delegate(IWin32Window o)
                    {
                        return form.ShowDialog(o);
                    });
                return form.ShowDialog(owner);
            }
            catch (ThreadAbortException)
            {
                return DialogResult.Abort;
            }
        }

        /// <summary>
        ///     Closes the ext.
        /// </summary>
        /// <param name="form">The form.</param>
        public static void CloseExt(this Form form)
        {
            try
            {
                if (form.InvokeRequired)
                    form.Invoke(new GenericDelegate(form.Close));
                else
                    form.Close();
            }
            catch (ThreadAbortException)
            {
            }
        }

        /// <summary>
        ///     Gets the checked.
        /// </summary>
        /// <param name="cb">The cb.</param>
        /// <returns></returns>
        public static bool GetChecked(this CheckBox cb)
        {
            if (cb.InvokeRequired)
                return (bool) cb.Invoke(new GetCheckedDelegate(GetCheckedInternal), cb);
            return GetCheckedInternal(cb);
        }

        public static string GetText(this Control ctrl)
        {
            if (ctrl.InvokeRequired)
                return (string) ctrl.Invoke(new GetTextDelegate(GetTextInternal), ctrl);
            return GetTextInternal(ctrl);
        }

        public static int GetValue(this ProgressBar pb)
        {
            try
            {
                if (pb.InvokeRequired)
                    return (int) pb.Invoke(new GetProgressBarValueDelegate(InternalGetProgressBarValue), pb);
                return InternalGetProgressBarValue(pb);
            }
            catch (ThreadAbortException)
            {
                return 0;
            }
        }

        /// <summary>
        ///     Hides the ext.
        /// </summary>
        /// <param name="form">The Form.</param>
        public static void HideExt(this Form form)
        {
            try
            {
                if (form.InvokeRequired)
                    form.Invoke(new GenericDelegate(form.Hide));
                else
                    form.Hide();
            }
            catch (ThreadAbortException)
            {
            }
        }

        /// <summary>
        ///     increments the progress bar
        /// </summary>
        /// <param name="pb">The pb.</param>
        /// <param name="increment">The increment.</param>
        public static void IncrementExt(this DevExpress.XtraEditors.ProgressBarControl pb, int increment)
        {
            try
            {
                if (pb.InvokeRequired)
                    pb.Invoke(new ProgressBarIncrementDelegate(pb.Increment), increment);
                else
                    pb.Increment(increment);
            }
            catch (ThreadAbortException)
            {
            }
        }

        /// <summary>
        ///     Sets the checked.
        /// </summary>
        /// <param name="cb">The cb.</param>
        /// <param name="checked">if set to <c>true</c> [@checked].</param>
        public static void SetChecked(this CheckBox cb, bool @checked)
        {
            if (cb.InvokeRequired)
                cb.Invoke(new SetCheckedDelegate(SetCheckedInternal), cb, @checked);
            else
                SetCheckedInternal(cb, @checked);
        }

        /// <summary>
        ///     Sets the progress bar maximum.
        /// </summary>
        /// <param name="pb">The pb.</param>
        /// <param name="max">The max.</param>
        public static void SetMaximum(this ProgressBar pb, int max)
        {
            try
            {
                if (pb.InvokeRequired)
                    pb.Invoke(new SetProgressBarMaximumDelegate(InternalSetProgressBarMaximum), pb, max);
                else
                    InternalSetProgressBarMaximum(pb, max);
            }
            catch (ThreadAbortException)
            {
            }
        }


        /// <summary>
        ///     Sets the progress bar minimum.
        /// </summary>
        /// <param name="pb">The pb.</param>
        /// <param name="min">The min.</param>
        public static void SetMinimum(this ProgressBar pb, int min)
        {
            try
            {
                if (pb.InvokeRequired)
                    pb.Invoke(new SetProgressBarMinimumDelegate(InternalSetProgressBarMinimum), pb, min);
                else
                    InternalSetProgressBarMinimum(pb, min);
            }
            catch (ThreadAbortException)
            {
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ctrl"></param>
        /// <param name="text"></param>
        public static void SetText(this Control ctrl, string text)
        {
            if (ctrl.InvokeRequired)
                ctrl.Invoke(new SetTextDelegate(SetTextInternal), ctrl, text);
            else
                SetTextInternal(ctrl, text);
        }

        /// <summary>
        ///     Sets the progress bar value.
        /// </summary>
        /// <param name="pb">The pb.</param>
        /// <param name="value">The value.</param>
        public static void SetValue(this ProgressBar pb, int value)
        {
            try
            {
                if (pb.InvokeRequired)
                    pb.Invoke(new SetProgressBarValueDelegate(InternalSetProgressBarValue), pb, value);
                else
                    InternalSetProgressBarValue(pb, value);
            }
            catch (ThreadAbortException)
            {
            }
        }

        private delegate DialogResult EmptyShowDialogDelegate();

        private delegate void GenericDelegate();

        private delegate bool GetCheckedDelegate(CheckBox checkbox);

        private delegate int GetProgressBarValueDelegate(ProgressBar pb);

        private delegate string GetTextDelegate(Control ctrl);

        private delegate void ProgressBarIncrementDelegate(int increment);

        private delegate void SetCheckedDelegate(CheckBox checkbox, bool @checked);

        private delegate void SetProgressBarMaximumDelegate(ProgressBar pb, int max);

        private delegate void SetProgressBarMinimumDelegate(ProgressBar pb, int min);

        private delegate void SetProgressBarValueDelegate(ProgressBar pb, int value);

        private delegate void SetTextDelegate(Control ctrl, string text);

        private delegate void ShowDelegate(IWin32Window o);

        private delegate DialogResult ShowDialogDelegate(IWin32Window o);
    }
}