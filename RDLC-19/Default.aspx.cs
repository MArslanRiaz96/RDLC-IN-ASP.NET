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
    public partial class Default : System.Web.UI.Page
    {
        string ConnectionString = @"data source=.; initial catalog=StudentDb; trusted_connection=true;";
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnClick_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(ConnectionString);
            SqlCommand cmd = new SqlCommand("SELECT Id,Name FROM Student", con);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            con.Close();
            rv.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", dt));
            rv.LocalReport.ReportPath = Server.MapPath("~/Report/Report.rdlc");
            rv.LocalReport.EnableHyperlinks = true;


        }
    }
}