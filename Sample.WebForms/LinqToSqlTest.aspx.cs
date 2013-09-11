using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using StackExchange.Profiling;
using StackExchange.Profiling.Data;

namespace Sample.WebForms
{
	public partial class LinqToSqlTest : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			using (MiniProfiler.Current.Step("Page_Load"))
			{
				using (var ctx = GetDataContext())
				{
					var x = (from c in ctx.Customers
							 join o in ctx.Orders on c.CustomerID equals o.CustomerID
							 select new
							 {
								 CustomerID = c.CustomerID,
								 CompanyName = c.CompanyName,
								 TotalAmount = o.Order_Details.Sum(i => i.Quantity * i.UnitPrice)
							 }).ToList();
					_grdData.DataSource = x;
					_grdData.DataBind();
				}
			}
		}

		public static NortwindL2SDataContext GetDataContext()
		{
			var connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["NorthwindConnectionString"];
			var sqlConnection = new SqlConnection(connectionString.ConnectionString);
			var profiledConnection = new ProfiledDbConnection(sqlConnection, MiniProfiler.Current);
			return new NortwindL2SDataContext(profiledConnection);
		}
	}
}