using System;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace Clifton.Core.ExtensionMethods
{
	public static class WinFormExtensionMethods
	{
		public static void BeginInvoke(this Control control, Action action)
		{
			if (!control.InvokeRequired)
			{
				action();
			}
			else
			{
				control.BeginInvoke(action);
			}
		}

		public static void Invoke(this Control control, Action action)
		{
			if (!control.InvokeRequired)
			{
				action();
			}
			else
			{
				control.Invoke(action);
			}
		}
	}
}