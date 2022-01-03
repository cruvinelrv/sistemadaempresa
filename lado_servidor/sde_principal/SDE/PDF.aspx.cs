using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SDE.Web
{
    public partial class PDF : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
	        string method = Request.QueryString["method"];
	        string name = Request.QueryString["name"];
        	
	        byte[] data = Request.BinaryRead(Request.TotalBytes);
        	
	        Response.ContentType = "application/pdf";
	        Response.AddHeader("Content-Length", data.Length.ToString());
	        Response.AddHeader("Content-disposition", method + "; filename=" + name);
	        Response.BinaryWrite(data);
	        Response.End();
            //:))))
        }
    }
}
