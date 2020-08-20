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
    public partial class WebForm1 : System.Web.UI.Page
    {
        string ConnectionString = @"data source=myde7j9kix.database.windows.net; initial catalog=ProdReporting; trusted_connection=false;User ID=applogin;Password=Eovendo2013;";
        protected void Page_Load(object sender, EventArgs e)
        {


            //con.Open();

        }
        protected void btnClick_Click(object sender, EventArgs e)
        {
            var reportDate = "2020-08-19";
            SqlConnection con = new SqlConnection(ConnectionString);
            //SqlCommand cmd = new SqlCommand("Exec [dbo].[uspReport_TimeSlot_AllDeals_Get]", con)
            //{
            //    CommandType = CommandType.StoredProcedure
            //};
            using (var command = new SqlCommand("uspReport_TimeSlot_UserStats_Get", con)
            {
                CommandType = CommandType.StoredProcedure

            })
            {
                command.Parameters.Add("@SlotNumber", SqlDbType.Int).Value = 7;
                command.Parameters.Add("@ReportDate", SqlDbType.Date).Value = reportDate;

                con.Open();
                //command.ExecuteNonQuery();
                SqlDataReader dr = command.ExecuteReader(CommandBehavior.CloseConnection);
                DataTable dt = new DataTable();
                dt.Load(dr);
                //con.Close();

                ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("Slot1", dt));
                ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("Slot2", dt));
                ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("Slot3", dt));
                ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("Slot4", dt));
                ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("Slot5", dt));
                ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("Slot6", dt));
                ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("Slot7", dt));
                ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("Slot8", dt));
                ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("Slot9", dt));
                ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("Slot10", dt));



                ///////////////
                using (var command2 = new SqlCommand("uspReport_TimeSlot_Top10Deals_Get", con)
                {
                    CommandType = CommandType.StoredProcedure

                })
                {
                    command2.Parameters.Add("@SlotNumber", SqlDbType.Int).Value = 1;
                    command2.Parameters.Add("@ReportDate", SqlDbType.Date).Value = reportDate;

                    con.Open();
                    //command.ExecuteNonQuery();
                    SqlDataReader dr2 = command2.ExecuteReader(CommandBehavior.CloseConnection);
                    DataTable dt2 = new DataTable();
                    dt2.Load(dr2);




                    ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DealSlot1", dt2));
                    ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DealSlot2", dt2));
                    ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DealSlot3", dt2));
                    ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DealSlot4", dt2));
                    ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DealSlot5", dt2));
                    ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DealSlot6", dt2));
                    ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DealSlot7", dt2));
                    ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DealSlot8", dt2));
                    ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DealSlot9", dt2));
                    ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DealSlot10", dt2));



                    ///////////////
                    using (var command3 = new SqlCommand("uspReport_TimeSlot_AllDeals_Get", con)
                    {
                        CommandType = CommandType.StoredProcedure

                    })
                    {
                        con.Open();
                        //command.ExecuteNonQuery();
                        SqlDataReader dr3 = command3.ExecuteReader(CommandBehavior.CloseConnection);
                        DataTable dt3 = new DataTable();
                        dt3.Load(dr3);
                        ReportParameter rp1 = new ReportParameter("ReportDate", DateTime.Now.ToShortDateString());
                        ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("AllDeal", dt3));


                        /////////
                        DataTable dt4 = new DataTable();
                        ///
                        ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DatePram", dt4));
                        ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Report/DM_TimeSlotData.rdlc");
                        ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { rp1 });
                        ReportViewer1.LocalReport.EnableHyperlinks = true;
                    }
                }
            }
        }
    }
}
