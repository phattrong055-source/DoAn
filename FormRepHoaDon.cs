using System;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

namespace WindowsFormsApp1
{
    public partial class FormRepHoaDon : Form
    {
        string maHoaDon;

        public FormRepHoaDon(string maHD)
        {
            InitializeComponent();
            maHoaDon = maHD;
        }

        private void FormRepHoaDon_Load(object sender, EventArgs e)
        {
            LoadReport();
        }

        void LoadReport()
        {
            try
            {
                CrystalReportHoaDon rpt = new CrystalReportHoaDon();

                ConnectionInfo ketNoi = new ConnectionInfo();
                ketNoi.ServerName = @"DESKTOP-S4KUUMQ\SQLEXPRESS";
                ketNoi.DatabaseName = "DoAnTotNghiep";
                ketNoi.IntegratedSecurity = true;

                foreach (Table table in rpt.Database.Tables)
                {
                    TableLogOnInfo logonInfo = table.LogOnInfo;
                    logonInfo.ConnectionInfo = ketNoi;
                    table.ApplyLogOnInfo(logonInfo);
                }

                // Vì maHoaDon của bạn đang là số: 1, 2, 3...
                rpt.RecordSelectionFormula = "{vw_HoaDonReport.maHoaDon} = " + maHoaDon;

                crystalReportViewer1.ReportSource = rpt;
                crystalReportViewer1.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi hiển thị hóa đơn: " + ex.Message);
            }
        }
    }
}