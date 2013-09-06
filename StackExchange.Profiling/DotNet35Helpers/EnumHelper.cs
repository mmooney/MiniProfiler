using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System
{
	public static class EnumHelper
	{
		//
		// Summary:
		//     Converts the string representation of the name or numeric value of one or
		//     more enumerated constants to an equivalent enumerated object. The return
		//     value indicates whether the conversion succeeded.
		//
		// Parameters:
		//   value:
		//     The string representation of the enumeration name or underlying value to
		//     convert.
		//
		//   result:
		//     When this method returns, contains an object of type TEnum whose value is
		//     represented by value. This parameter is passed uninitialized.
		//
		// Type parameters:
		//   TEnum:
		//     The enumeration type to which to convert value.
		//
		// Returns:
		//     true if the value parameter was converted successfully; otherwise, false.
		//
		// Exceptions:
		//   System.ArgumentException:
		//     TEnum is not an enumeration type.
		public static bool TryParse<T>(string str, out T result) where T : struct
		{
			#if !CSHARP30
				return Enum.TryParse(value, out result);
			#else
				return EnumHelper.TryParse(str, true, out result);
			#endif
		}

		//
		// Summary:
		//     Converts the string representation of the name or numeric value of one or
		//     more enumerated constants to an equivalent enumerated object. A parameter
		//     specifies whether the operation is case-sensitive. The return value indicates
		//     whether the conversion succeeded.
		//
		// Parameters:
		//   value:
		//     The string representation of the enumeration name or underlying value to
		//     convert.
		//
		//   ignoreCase:
		//     true to ignore case; false to consider case.
		//
		//   result:
		//     When this method returns, contains an object of type TEnum whose value is
		//     represented by value. This parameter is passed uninitialized.
		//
		// Type parameters:
		//   TEnum:
		//     The enumeration type to which to convert value.
		//
		// Returns:
		//     true if the value parameter was converted successfully; otherwise, false.
		//
		// Exceptions:
		//   System.ArgumentException:
		//     TEnum is not an enumeration type.
		public static bool TryParse<T>(string str, bool ignoreCase, out T result) where T : struct
		{
			#if !CSHARP30
				return Enum.TryParse(value, ignoreCase, out result);
			#else
				bool caseSensitive = !ignoreCase;
				//http://stackoverflow.com/a/1082578/203479
				// Can't make this a type constraint...
				if (!typeof(T).IsEnum)
				{
					throw new ArgumentException("Type parameter must be an enum");
				}
				var names = Enum.GetNames(typeof(T));
				result = (Enum.GetValues(typeof(T)) as T[])[0];  // For want of a better default
				foreach (var name in names)
				{
					if (String.Equals(name, str, caseSensitive ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase))
					{
						result = (T)Enum.Parse(typeof(T), name);
						return true;
					}
				}
				return false;
			#endif
		}
	}
}
