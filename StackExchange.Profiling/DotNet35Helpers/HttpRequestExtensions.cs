using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Routing;

namespace System.Web
{
	public static class HttpRequestExtensions
	{
		public static RequestContext GetRequestContext(this HttpRequest request)
		{
			#if !CSHARP30
				return request.RequestContext;
			#else
				var context = new HttpContextWrapper(HttpContext.Current);
				var routeData = RouteTable.Routes.GetRouteData(context); 
				return new RequestContext(context, routeData ?? new RouteData());
			#endif
		}
	}
}
