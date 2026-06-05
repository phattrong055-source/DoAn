using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class FormInPhieu : Form
    {
        private DataTable _dtChiTiet;
        private string _hoTen, _sdt, _diaChi, _email, _cccd;
        private string _ngayLap, _ngayThue, _ngayTraDuKien, _ghiChu;
        private string _tongSoMay, _tongTienThue;
        private string _hinhThucThue;
        private string _tienCoc;
        
        public FormInPhieu(
    DataTable dtChiTiet,
    string hoTen,
    string sdt,
    string diaChi,
    string email,
    string cccd,
    string ngayLap,
    string ngayThue,
    string ngayTraDuKien,
    string ghiChu,
    string tienCoc,
    string tongSoMay,
    string tongTienThue,
    string hinhThucThue)
        {
            InitializeComponent();
            this._dtChiTiet = dtChiTiet;
            this._hoTen = hoTen;
            this._sdt = sdt;
            this._diaChi = diaChi;
            this._email = email;
            this._cccd = cccd;
            this._ngayLap = ngayLap;
            this._ngayThue = ngayThue;
            this._ngayTraDuKien = ngayTraDuKien;
            this._ghiChu = ghiChu;
            this._tienCoc = tienCoc;
            this._tongSoMay = tongSoMay;
            this._tongTienThue = tongTienThue;
            this._hinhThucThue = hinhThucThue;
        }

        private void FormInPhieu_Load(object sender, EventArgs e)
        {
            try
            {
                // 1. Định nghĩa tài nguyên tệp cấu trúc RDLC Report
                this.reportViewer1.LocalReport.ReportEmbeddedResource = "WindowsFormsApp1.ReportInPhieu.rdlc";
                this.reportViewer1.LocalReport.DataSources.Clear();

                DoAnTotNghiepDataSet ds = new DoAnTotNghiepDataSet();
                var phieuThueAdapter = new DoAnTotNghiepDataSetTableAdapters.PhieuThueTableAdapter();
                var chiTietAdapter = new DoAnTotNghiepDataSetTableAdapters.ChiTietPhieuThueTableAdapter();
                var khachHangAdapter = new DoAnTotNghiepDataSetTableAdapters.KhachHangTableAdapter();
                var mayAnhAdapter = new DoAnTotNghiepDataSetTableAdapters.MayAnhTableAdapter();

                phieuThueAdapter.Fill(ds.PhieuThue);
                khachHangAdapter.Fill(ds.KhachHang);
                mayAnhAdapter.Fill(ds.MayAnh);

                int maPhieuHienTai = 1;
                var phieuLocTmp = ds.PhieuThue.Select("maPhieuThue > 0");
                if (phieuLocTmp.Length > 0)
                {
                    maPhieuHienTai = Convert.ToInt32(phieuLocTmp[phieuLocTmp.Length - 1]["maPhieuThue"]);
                }


                chiTietAdapter.Fill(ds.ChiTietPhieuThue, maPhieuHienTai);

                // 2. Liên kết dữ liệu bảng động DataSet1 từ lưới chính
                if (_dtChiTiet != null && _dtChiTiet.Rows.Count > 0)
                {
                    this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", _dtChiTiet));
                }
                else
                {
                    DataRow[] rowsChiTiet = ds.ChiTietPhieuThue.Select($"maPhieuThue = {maPhieuHienTai}");
                    if (rowsChiTiet.Length > 0)
                    {
                        DataTable dtFiltered = ds.ChiTietPhieuThue.Clone();
                        foreach (DataRow r in rowsChiTiet)
                        {
                            dtFiltered.ImportRow(r);
                        }
                        this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", dtFiltered));
                    }
                    else
                    {
                        this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", ds.ChiTietPhieuThue.Clone()));
                    }
                }


                // 3. SỬA TOÀN BỘ LỖI CHÍNH TẢ Ở CẤU TRÚC ĐIỀU KIỆN ÉP PARAMETER ĐỘNG
                var reportParamsInfo = this.reportViewer1.LocalReport.GetParameters();
                if (reportParamsInfo != null && reportParamsInfo.Count > 0)
                {
                    List<ReportParameter> dynamicParams = new List<ReportParameter>();
                    foreach (var p in reportParamsInfo)
                    {
                        string pName = p.Name.ToLower();
                        string pValue = " ";

                        if (pName.Contains("ngaylap"))
                            pValue = _ngayLap;
                        else if (pName.Contains("ngaythue"))
                            pValue = _ngayThue;
                        else if (pName.Contains("ngaytra"))
                            pValue = _ngayTraDuKien;
                        else if (pName.Contains("ghichu"))
                        {
                            pValue = string.IsNullOrWhiteSpace(_ghiChu)
                                ? ""
                                : _ghiChu;
                        }
                        else if (pName.Contains("tongsomay"))
                            pValue = _tongSoMay;
                        else if (pName.Contains("tongtien"))
                            pValue = _tongTienThue;
                        else if (pName.Contains("ten") || pName.Contains("hoten"))
                            pValue = _hoTen;
                        else if (pName.Contains("sdt"))
                            pValue = _sdt;
                        else if (pName.Contains("diachi"))
                            pValue = _diaChi;
                        else if (pName.Contains("email"))
                            pValue = _email;
                        else if (pName.Contains("cccd"))
                            pValue = _cccd;
                        else if (pName.Contains("hinhthucthue"))
                            pValue = _hinhThucThue;
                        else if (pName.Contains("tiencoc"))
                            pValue = _tienCoc;


                        dynamicParams.Add(new ReportParameter(p.Name, pValue));
                    }
                    this.reportViewer1.LocalReport.SetParameters(dynamicParams);
                }

                this.reportViewer1.RefreshReport();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi hiển thị báo cáo Report: " + ex.Message, "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
    }
}