using System;
using System.ComponentModel;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace CommandModel
{
	public static class EnumHelper
	{
		public static string GetValueAsString(this ZC_Command item)
		{
			string str;
			FieldInfo field = item.GetType().GetField(item.ToString());
			object[] customAttributes = field.GetCustomAttributes(typeof(DescriptionAttribute), false);
			str = (customAttributes.Length == 0 ? item.ToString() : (customAttributes[0] as DescriptionAttribute).Description);
			return str;
		}
	}
}