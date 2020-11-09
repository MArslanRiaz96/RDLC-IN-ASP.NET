using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RDLC_19
{
    public partial class EovendopanelSurveyRedeems : System.Web.UI.Page
    {
        string ConnectionString = @"data source=myde7j9kix.database.windows.net; initial catalog=ProdReporting; trusted_connection=false;User ID=applogin;Password=Eovendo2013;";
        protected void Page_Load(object sender, EventArgs e)
        {

            
        }

        protected void btnClick_Click(object sender, EventArgs e)
        {
            var reportDate = "2020-11-03";
            SqlConnection con = new SqlConnection(ConnectionString);
            using (var command = new SqlCommand("uspReportEovendopanelSurveyRedeems", con)
            {
                CommandType = CommandType.StoredProcedure

            })
            {
                command.Parameters.Add("@ReportDate", SqlDbType.Date).Value = reportDate;
                con.Open();
                SqlDataReader dr = command.ExecuteReader(CommandBehavior.CloseConnection);
                DataTable dt = new DataTable();
                dt.Load(dr);
                ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", dt));
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Report/EovendopanelSurveyRedeems.rdlc");
                ReportViewer1.LocalReport.EnableHyperlinks = true;
            }
        }
    }
}