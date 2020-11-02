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
            ReportViewer1.Reset();
            //  var reportDate = "2020-10-28";
            var reportDate = TextBox1.Text.ToString();
            SqlConnection con = new SqlConnection(ConnectionString);
           
            ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("Slot1", GetTimeSlotUserStatsBySlotNo(con,reportDate,1)));
            ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("Slot2", GetTimeSlotUserStatsBySlotNo(con, reportDate, 2)));
            ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("Slot3", GetTimeSlotUserStatsBySlotNo(con, reportDate, 3)));
            ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("Slot4", GetTimeSlotUserStatsBySlotNo(con, reportDate, 4)));
            ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("Slot5", GetTimeSlotUserStatsBySlotNo(con, reportDate, 5)));
            ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("Slot6", GetTimeSlotUserStatsBySlotNo(con, reportDate, 6)));
            ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("Slot7", GetTimeSlotUserStatsBySlotNo(con, reportDate, 7)));
            ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("Slot8", GetTimeSlotUserStatsBySlotNo(con, reportDate, 8)));
            ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("Slot9", GetTimeSlotUserStatsBySlotNo(con, reportDate, 9)));
            ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("Slot10", GetTimeSlotUserStatsBySlotNo(con, reportDate, 10)));



            ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DealSlot1", GetTimeSlotTop10DealsBySlotNo(con, reportDate, 1)));
            ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DealSlot2", GetTimeSlotTop10DealsBySlotNo(con, reportDate, 2)));
            ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DealSlot3", GetTimeSlotTop10DealsBySlotNo(con, reportDate, 3)));
            ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DealSlot4", GetTimeSlotTop10DealsBySlotNo(con, reportDate, 4)));
            ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DealSlot5", GetTimeSlotTop10DealsBySlotNo(con, reportDate, 5)));
            ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DealSlot6", GetTimeSlotTop10DealsBySlotNo(con, reportDate, 6)));
            ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DealSlot7", GetTimeSlotTop10DealsBySlotNo(con, reportDate, 7)));
            ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DealSlot8", GetTimeSlotTop10DealsBySlotNo(con, reportDate, 8)));
            ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DealSlot9", GetTimeSlotTop10DealsBySlotNo(con, reportDate, 9)));
            ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DealSlot10", GetTimeSlotTop10DealsBySlotNo(con, reportDate, 10)));


            ///////////////

            ///////////////
            using (var command3 = new SqlCommand("uspReport_TimeSlot_AllDeals_Get", con)
                    {
                        CommandType = CommandType.StoredProcedure

                    })
                    {
                command3.Parameters.Add("@ReportDate", SqlDbType.Date).Value = reportDate;
                con.Open();
                        //command.ExecuteNonQuery();
                        SqlDataReader dr3 = command3.ExecuteReader(CommandBehavior.CloseConnection);
                        DataTable dt3 = new DataTable();
                
                        dt3.Load(dr3);
                        
                        ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("AllDeal", dt3));


                        /////////
                        DataTable dt4 = new DataTable();
                        dt4.Columns.Add("ReportDate");
                for (int i = 0; i <= 10; i++)
                {

                    
                    dt4.Rows.Add(reportDate);
                }
                        
                        ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DatePram", dt4));
                        
                    }
            ReportParameter rp1 = new ReportParameter("ReportDate", reportDate);
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Report/DM_TimeSlotData.rdlc");
            ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { rp1 });
            ReportViewer1.LocalReport.EnableHyperlinks = true;

            

        }
        public DataTable GetTimeSlotUserStatsBySlotNo(SqlConnection con, string reportDate, int slotNo)
        {
            using (var command = new SqlCommand("uspReport_TimeSlot_UserStats_Get", con)
            {
                CommandType = CommandType.StoredProcedure

            })
            {
                command.Parameters.Add("@SlotNumber", SqlDbType.Int).Value = slotNo;
                command.Parameters.Add("@ReportDate", SqlDbType.Date).Value = reportDate;

                con.Open();
                //command.ExecuteNonQuery();
                SqlDataReader dr = command.ExecuteReader(CommandBehavior.CloseConnection);
                DataTable dt = new DataTable();
                dt.Load(dr);
                return dt;

            }
        }

        public DataTable GetTimeSlotTop10DealsBySlotNo(SqlConnection con, string reportDate, int slotNo)
        {
            using (var command2 = new SqlCommand("uspReport_TimeSlot_Top10Deals_Get", con)
            {
                CommandType = CommandType.StoredProcedure

            })
            {
                command2.Parameters.Add("@SlotNumber", SqlDbType.Int).Value = slotNo;
                command2.Parameters.Add("@ReportDate", SqlDbType.Date).Value = reportDate;

                con.Open();
                //command.ExecuteNonQuery();
                SqlDataReader dr2 = command2.ExecuteReader(CommandBehavior.CloseConnection);
                DataTable dt2 = new DataTable();
                dt2.Load(dr2);

                return dt2;
            
            }
        }

    }
       
    
}
