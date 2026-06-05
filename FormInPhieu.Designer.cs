
namespace WindowsFormsApp1
{
    partial class FormInPhieu
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource5 = new Microsoft.Reporting.WinForms.ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource6 = new Microsoft.Reporting.WinForms.ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource7 = new Microsoft.Reporting.WinForms.ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource8 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.ChiTietPhieuThueBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.DoAnTotNghiepDataSet = new WindowsFormsApp1.DoAnTotNghiepDataSet();
            this.MayAnhBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.KhachHangBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.PhieuThueBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.ChiTietPhieuThueTableAdapter = new WindowsFormsApp1.DoAnTotNghiepDataSetTableAdapters.ChiTietPhieuThueTableAdapter();
            this.MayAnhTableAdapter = new WindowsFormsApp1.DoAnTotNghiepDataSetTableAdapters.MayAnhTableAdapter();
            this.KhachHangTableAdapter = new WindowsFormsApp1.DoAnTotNghiepDataSetTableAdapters.KhachHangTableAdapter();
            this.PhieuThueTableAdapter = new WindowsFormsApp1.DoAnTotNghiepDataSetTableAdapters.PhieuThueTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.ChiTietPhieuThueBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DoAnTotNghiepDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MayAnhBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.KhachHangBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PhieuThueBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // ChiTietPhieuThueBindingSource
            // 
            this.ChiTietPhieuThueBindingSource.DataMember = "ChiTietPhieuThue";
            this.ChiTietPhieuThueBindingSource.DataSource = this.DoAnTotNghiepDataSet;
            // 
            // DoAnTotNghiepDataSet
            // 
            this.DoAnTotNghiepDataSet.DataSetName = "DoAnTotNghiepDataSet";
            this.DoAnTotNghiepDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // MayAnhBindingSource
            // 
            this.MayAnhBindingSource.DataMember = "MayAnh";
            this.MayAnhBindingSource.DataSource = this.DoAnTotNghiepDataSet;
            // 
            // KhachHangBindingSource
            // 
            this.KhachHangBindingSource.DataMember = "KhachHang";
            this.KhachHangBindingSource.DataSource = this.DoAnTotNghiepDataSet;
            // 
            // PhieuThueBindingSource
            // 
            this.PhieuThueBindingSource.DataMember = "PhieuThue";
            this.PhieuThueBindingSource.DataSource = this.DoAnTotNghiepDataSet;
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource5.Name = "DataSet1";
            reportDataSource5.Value = this.ChiTietPhieuThueBindingSource;
            reportDataSource6.Name = "DataSet2";
            reportDataSource6.Value = this.MayAnhBindingSource;
            reportDataSource7.Name = "DataSet3";
            reportDataSource7.Value = this.KhachHangBindingSource;
            reportDataSource8.Name = "DataSet4";
            reportDataSource8.Value = this.PhieuThueBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource5);
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource6);
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource7);
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource8);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "WindowsFormsApp1.ReportHoaDon.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.ServerReport.BearerToken = null;
            this.reportViewer1.Size = new System.Drawing.Size(983, 656);
            this.reportViewer1.TabIndex = 0;
            // 
            // ChiTietPhieuThueTableAdapter
            // 
            this.ChiTietPhieuThueTableAdapter.ClearBeforeFill = true;
            // 
            // MayAnhTableAdapter
            // 
            this.MayAnhTableAdapter.ClearBeforeFill = true;
            // 
            // KhachHangTableAdapter
            // 
            this.KhachHangTableAdapter.ClearBeforeFill = true;
            // 
            // PhieuThueTableAdapter
            // 
            this.PhieuThueTableAdapter.ClearBeforeFill = true;
            // 
            // FormInPhieu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(983, 656);
            this.Controls.Add(this.reportViewer1);
            this.Name = "FormInPhieu";
            this.Text = "FormHoaDon";
            this.Load += new System.EventHandler(this.FormInPhieu_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ChiTietPhieuThueBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DoAnTotNghiepDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MayAnhBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.KhachHangBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PhieuThueBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource ChiTietPhieuThueBindingSource;
        private DoAnTotNghiepDataSet DoAnTotNghiepDataSet;
        private System.Windows.Forms.BindingSource MayAnhBindingSource;
        private System.Windows.Forms.BindingSource KhachHangBindingSource;
        private DoAnTotNghiepDataSetTableAdapters.ChiTietPhieuThueTableAdapter ChiTietPhieuThueTableAdapter;
        private DoAnTotNghiepDataSetTableAdapters.MayAnhTableAdapter MayAnhTableAdapter;
        private DoAnTotNghiepDataSetTableAdapters.KhachHangTableAdapter KhachHangTableAdapter;
        private System.Windows.Forms.BindingSource PhieuThueBindingSource;
        private DoAnTotNghiepDataSetTableAdapters.PhieuThueTableAdapter PhieuThueTableAdapter;
    }
}