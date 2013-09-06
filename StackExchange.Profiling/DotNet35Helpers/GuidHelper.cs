using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace System
{
	public static class GuidHelper
	{
		//
		// Summary:
		//     Converts the string representation of a GUID to the equivalent System.Guid
		//     structure.
		//
		// Parameters:
		//   input:
		//     The GUID to convert.
		//
		//   result:
		//     The structure that will contain the parsed value.
		//
		// Returns:
		//     true if the parse operation was successful; otherwise, false.
		public static bool TryParse(string input, out Guid result)
		{
			#if !CSHARP30
				return Guid.TryPrase(input, out result);
			#else
				//http://geekswithblogs.net/colinbo/archive/2006/01/18/66307.aspx,
			//	by way of https://github.com/mmooney/MMDB.Shared/blob/master/Web/WebFormsHelper.cs
				if (input == null)
					throw new ArgumentNullException("s");
				Regex format = new Regex(
					"^[A-Fa-f0-9]{32}$|" +
					"^({|\\()?[A-Fa-f0-9]{8}-([A-Fa-f0-9]{4}-){3}[A-Fa-f0-9]{12}(}|\\))?$|" +
					"^({)?[0xA-Fa-f0-9]{3,10}(, {0,1}[0xA-Fa-f0-9]{3,6}){2}, {0,1}({)([0xA-Fa-f0-9]{3,4}, {0,1}){7}[0xA-Fa-f0-9]{3,4}(}})$");
				Match match = format.Match(input);
				if (match.Success)
				{
					result = new Guid(input);
					return true;
				}
				else
				{
					result = Guid.Empty;
					return false;
				}
			#endif
		}
	}
}
