using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TestApp.Reports
{
    public partial class ReportTemplate : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    rvSiteMapping.Height = Unit.Pixel(Convert.ToInt32(Request["Height"]) - 58);
                    rvSiteMapping.ShowCredentialPrompts = false;
                    rvSiteMapping.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
                    rvSiteMapping.ServerReport.ReportServerUrl = new Uri("http://win-dk4bgmj2cd2.globalnet.tn:80/ReportServer_GNET_DW"); // Add the Reporting Server URL                    
                    rvSiteMapping.ServerReport.ReportPath = String.Format("{0}", Request["ReportName"].ToString());
                    rvSiteMapping.ServerReport.Refresh();
                }
            }
            catch (Exception ex)
            {

            }

        }
    }
}