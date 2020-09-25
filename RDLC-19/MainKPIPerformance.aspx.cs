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
    public partial class MainKPIPerformance : System.Web.UI.Page
    {
        
        Int64 reportDateint = 20200910;
        List<string> ids = new List<string>();
        List<ReportDataSource> datasources = new List<ReportDataSource>();
        //string ConnectionString = @"data source=PRODBIEovendo.northeurope.cloudapp.azure.com; initial catalog=Userdbmigrate; trusted_connection=false;User ID=Eocereus;Password=Astrophyt12!;";
        string ConnectionString = @"data source=myde7j9kix.database.windows.net; initial catalog=ProdReporting; trusted_connection=false;User ID=applogin;Password=Eovendo2013;";
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnClick_Click(object sender, EventArgs e)
        {
            var reportDate = "2020-09-11";
            SqlConnection con = new SqlConnection(ConnectionString);
            


           

            ReportViewer1.ProcessingMode = ProcessingMode.Local;
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Report/MainKPIPerformance.rdlc");
            ReportViewer1.LocalReport.DataSources.Clear();
            using (var command3 = new SqlCommand("uspReportMainKPIPerformanceGet", con)
            {
                CommandType = CommandType.StoredProcedure

            })
            {
                con.Open();
                command3.Parameters.Add("@AsOfRunDate", SqlDbType.Date).Value = reportDate;
                //command.ExecuteNonQuery();
                SqlDataReader dr3 = command3.ExecuteReader(CommandBehavior.CloseConnection);
                DataTable dt3 = new DataTable();
                dt3.Load(dr3);
                ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("Services", dt3));



                foreach (DataRow row in dt3.Rows)
                {
                    ids.Add(row["ServiceId"].ToString());
                }


            }

            ReportViewer1.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(SetSubDataSource1);
            this.ReportViewer1.LocalReport.Refresh();




        }

        public void SetSubDataSource1(object sender, SubreportProcessingEventArgs e)
        {
            SqlConnection con = new SqlConnection(ConnectionString);
            string query = "select ServiceId, ServiceName, ServiceStartDate, DeviceType, ClickType, Day1, Day2, Day3, Day4, Day5, Day6, Day7, Day8, Day9, Day10, Day11, Day12, Day13, Day14, Day15, Weekly, Monthly, Alltime from dbo.Report_MainKPIPerformance where ServiceId = @ServiceId and ClickType = 'TileClick' and DateKey = @DateKey ORDER BY ReportSectionOrder, DeviceType";
            using (var command4 = new SqlCommand(query, con)
            {
                // CommandType = CommandType.StoredProcedure

            })
            {
                con.Open();
                command4.Parameters.Add("@ServiceId", SqlDbType.VarChar).Value = (((ReportParameterInfo)e.Parameters[0]).Values[0]).ToString();
               
                command4.Parameters.Add("@DateKey", SqlDbType.Int).Value = reportDateint;
                //command.ExecuteNonQuery();
                SqlDataReader dr4 = command4.ExecuteReader(CommandBehavior.CloseConnection);
                DataTable dt4 = new DataTable();

                dt4.Load(dr4);
                //   ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("TileClick", dt4));
                datasources.Add(new ReportDataSource("TileClick", dt4));
                e.DataSources.Add(new ReportDataSource("TileClick", dt4));
            }

            string query2 = "select ServiceId, ServiceName, ServiceStartDate, DeviceType, ClickType, Day1, Day2, Day3, Day4, Day5, Day6, Day7, Day8, Day9, Day10, Day11, Day12, Day13, Day14, Day15, Weekly, Monthly, Alltime from dbo.Report_MainKPIPerformance where ServiceId = @ServiceId and ClickType = 'DetailPageClick' and DateKey = @DateKey ORDER BY ReportSectionOrder, DeviceType";
            using (var command5 = new SqlCommand(query2, con)
            {
                // CommandType = CommandType.StoredProcedure

            })
            {
                con.Open();
               command5.Parameters.Add("@ServiceId", SqlDbType.VarChar).Value = (((ReportParameterInfo)e.Parameters[0]).Values[0]).ToString();
               command5.Parameters.Add("@DateKey", SqlDbType.Int).Value = reportDateint;
                //command.ExecuteNonQuery();
                SqlDataReader dr5 = command5.ExecuteReader(CommandBehavior.CloseConnection);
                DataTable dt5 = new DataTable();

                dt5.Load(dr5);
                //   ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DetailPageClick", dt5));
                datasources.Add(new ReportDataSource("DetailPageClick", dt5));
                e.DataSources.Add(new ReportDataSource("DetailPageClick", dt5));

            }


            string query3 = "select ServiceId, ServiceName, ServiceStartDate, DeviceType, ClickType, Day1, Day2, Day3, Day4, Day5, Day6, Day7, Day8, Day9, Day10, Day11, Day12, Day13, Day14, Day15, Weekly, Monthly, Alltime from dbo.Report_MainKPIPerformance where ServiceId = @ServiceId and ClickType = 'Callback' and DateKey = @DateKey ORDER BY ReportSectionOrder, DeviceType";
            using (var command6 = new SqlCommand(query3, con)
            {
                // CommandType = CommandType.StoredProcedure

            })
            {
                con.Open();
                command6.Parameters.Add("@ServiceId", SqlDbType.VarChar).Value = (((ReportParameterInfo)e.Parameters[0]).Values[0]).ToString();
                
                command6.Parameters.Add("@DateKey", SqlDbType.Int).Value = reportDateint;
                //command.ExecuteNonQuery();
                SqlDataReader dr6 = command6.ExecuteReader(CommandBehavior.CloseConnection);
                DataTable dt6 = new DataTable();

                dt6.Load(dr6);
                //       ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("Callback", dt6));
                datasources.Add(new ReportDataSource("Callback", dt6));
                e.DataSources.Add(new ReportDataSource("Callback", dt6));

            }

        }

       
    }
}