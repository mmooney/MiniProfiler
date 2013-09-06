using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

#if CSHARP30
	namespace System.Web
	{
		public interface IHtmlString
		{
			string ToHtmlString();
		}

		public class HtmlString : IHtmlString
		{
			private readonly string _value;

			// Summary:
			//     Initializes a new instance of the System.Web.HtmlString class.
			//
			// Parameters:
			//   value:
			//     An HTML-encoded string that should not be encoded again.
			public HtmlString(string value)
			{
				_value = value;
			}

			// Summary:
			//     Returns an HTML-encoded string.
			//
			// Returns:
			//     An HTML-encoded string.
			public string ToHtmlString()
			{
				return HttpUtility.HtmlEncode(_value);
			}

			//
			// Summary:
			//     Returns a string that represents the current object.
			//
			// Returns:
			//     A string that represents the current object.
			public override string ToString()
			{
				return _value;
			}
		}
	}
#endif
