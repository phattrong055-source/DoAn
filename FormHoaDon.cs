using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class FormHoaDon : Form
    {
        string nguon = @"Data Source=.\SQLEXPRESS;Initial Catalog=DoAnTotNghiep;Integrated Security=True";

        CultureInfo vn = new CultureInfo("vi-VN");

        public FormHoaDon()
        {
            InitializeComponent();
        }

        // ================== HÀM XỬ LÝ TIỀN VIỆT NAM ==================

        private string DinhDangTien(decimal soTien)
        {
            return soTien.ToString("C0", vn);
        }

        private decimal ChuyenSangSo(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return 0;

            text = text.Replace("₫", "")
                       .Replace("VNĐ", "")
                       .Replace("VND", "")
                       .Replace(".", "")
                       .Replace(",", "")
                       .Trim();

            decimal ketQua;
            decimal.TryParse(text, out ketQua);

            return ketQua;
        }

        private decimal ChuanHoaTienTuCSDL(object value)
        {
            if (value == DBNull.Value || value == null)
                return 0;

            decimal soTien = Convert.ToDecimal(value);

            // Vì CSDL của bạn có dạng 350.00 nhưng thực tế muốn hiểu là 350.000đ
            if (soTien > 0 && soTien < 10000)
            {
                soTien = soTien * 1000;
            }

            return soTien;
        }

        private void DinhDangTienTrongDataGridView(DataGridView dgv, string tenCot)
        {
            if (dgv.Columns.Contains(tenCot))
            {
                dgv.Columns[tenCot].DefaultCellStyle.Format = "C0";
                dgv.Columns[tenCot].DefaultCellStyle.FormatProvider = vn;
            }
        }

        // ================== NÚT TÌM KIẾM HÓA ĐƠN THEO MÃ PHIẾU TRẢ ==================

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaPhieuTra.Text))
            {
                MessageBox.Show("Vui lòng nhập mã phiếu trả!");
                return;
            }

            int maPhieuTra;

            if (!int.TryParse(txtMaPhieuTra.Text.Trim(), out maPhieuTra))
            {
                MessageBox.Show("Mã phiếu trả không hợp lệ!");
                return;
            }

            using (SqlConnection conn = new SqlConnection(nguon))
            {
                conn.Open();

                SqlCommand HD = new SqlCommand(
    @"SELECT maHoaDon
      FROM HoaDon
      WHERE maPhieuTra = @maPhieuTra", conn);

                HD.Parameters.AddWithValue("@maPhieuTra", maPhieuTra);

                SqlDataReader readerHD = HD.ExecuteReader();

                if (readerHD.Read())
                {
                    txtMaHoaDon.Text = readerHD["maHoaDon"].ToString();
                }
                else
                {
                    MessageBox.Show("Không tìm thấy hóa đơn theo phiếu trả này!");
                }

                readerHD.Close();

                // 1. Lấy thông tin phiếu trả
                SqlCommand cmdPT = new SqlCommand(@"
                    SELECT *
                    FROM PhieuTra
                    WHERE maPhieuTra = @maPT", conn);

                cmdPT.Parameters.AddWithValue("@maPT", maPhieuTra);

                SqlDataReader readerPT = cmdPT.ExecuteReader();

                if (readerPT.Read())
                {
                    int maPhieuThue = Convert.ToInt32(readerPT["maPhieuThue"]);

                    if (readerPT["ngayTra"] != DBNull.Value)
                    {
                        dtpNgayLapHD.Value = Convert.ToDateTime(readerPT["ngayTra"]);
                        
                    }

                    readerPT.Close();

                    SqlCommand cmd = new SqlCommand(@" SELECT thanhToan FROM HoaDon WHERE maPhieuTra = @maPhieuTra", conn);

                    cmd.Parameters.AddWithValue("@maPhieuTra", maPhieuTra);

                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        txtPhuongThucTT.Text = reader["thanhToan"].ToString();
                    }

                    reader.Close();


                    // 2. Lấy thông tin khách hàng từ mã phiếu thuê
                    SqlCommand cmdKH = new SqlCommand(@"
                        SELECT kh.*
                        FROM KhachHang kh
                        INNER JOIN PhieuThue pt ON kh.maKhachHang = pt.maKhachHang
                        WHERE pt.maPhieuThue = @maPT", conn);

                    cmdKH.Parameters.AddWithValue("@maPT", maPhieuThue);

                    SqlDataReader readerKH = cmdKH.ExecuteReader();

                    if (readerKH.Read())
                    {
                        txtHoTen.Text = readerKH["hoTen"].ToString();
                        txtSDT.Text = readerKH["sdt"].ToString();
                        txtDiaChi.Text = readerKH["diaChi"].ToString();
                        txtEmail.Text = readerKH["email"].ToString();
                        txtCCCD.Text = readerKH["cccd"].ToString();
                    }

                    readerKH.Close();

                    // 3. Lấy chi tiết máy thuê
                    SqlDataAdapter daCT = new SqlDataAdapter(@"
                        SELECT 
                            ct.maMay,
                            m.tenMay,
                            ct.soLuongThue,
                            ct.donGia,
                            ct.soLuongThue * ct.donGia AS thanhTien
                        FROM ChiTietPhieuThue ct
                        INNER JOIN MayAnh m ON ct.maMay = m.maMay
                        WHERE ct.maPhieuThue = @maPT", conn);

                    daCT.SelectCommand.Parameters.AddWithValue("@maPT", maPhieuThue);

                    DataTable dtCT = new DataTable();
                    daCT.Fill(dtCT);

                    // Chuẩn hóa tiền từ CSDL và giữ dạng số decimal để tính toán không bị sai
                    foreach (DataRow row in dtCT.Rows)
                    {
                        if (row["donGia"] != DBNull.Value)
                        {
                            row["donGia"] = ChuanHoaTienTuCSDL(row["donGia"]);
                        }

                        if (row["thanhTien"] != DBNull.Value)
                        {
                            row["thanhTien"] = ChuanHoaTienTuCSDL(row["thanhTien"]);
                        }
                    }

                    dataGridView1.AutoGenerateColumns = true;
                    dataGridView1.DataSource = dtCT;

                    DinhDangTienTrongDataGridView(dataGridView1, "donGia");
                    DinhDangTienTrongDataGridView(dataGridView1, "thanhTien");

                    // 4. Tính tổng tiền thuê
                    decimal tongTienThue = 0;

                    foreach (DataRow row in dtCT.Rows)
                    {
                        if (row["thanhTien"] != DBNull.Value)
                        {
                            tongTienThue += Convert.ToDecimal(row["thanhTien"]);
                        }
                    }

                    // 5. Lấy tổng phí phát sinh từ bảng PhiPhatSinh
                    SqlCommand cmdPhi = new SqlCommand(@"
                        SELECT SUM(soTien)
                        FROM PhiPhatSinh
                        WHERE maPhieuTra = @maPT", conn);

                    cmdPhi.Parameters.AddWithValue("@maPT", maPhieuTra);

                    object result = cmdPhi.ExecuteScalar();

                    decimal tongPhiPhatSinh = 0;

                    if (result != DBNull.Value && result != null)
                    {
                        tongPhiPhatSinh = Convert.ToDecimal(result);
                    }

                    // Nếu phí phát sinh trong CSDL đang là 100.00 nhưng muốn hiểu là 100.000đ
                    tongPhiPhatSinh = ChuanHoaTienTuCSDL(tongPhiPhatSinh);

                    // 6. Tính tổng thanh toán
                    decimal tongThanhToan = tongTienThue + tongPhiPhatSinh;

                    // 7. Hiển thị tiền Việt Nam trên form
                    txtTongTienThue.Text = DinhDangTien(tongTienThue);
                    txtTongPhiPhatSinh.Text = DinhDangTien(tongPhiPhatSinh);
                    txtTongThanhToan.Text = DinhDangTien(tongThanhToan);
                }
                else
                {
                    readerPT.Close();
                    MessageBox.Show("Không tìm thấy phiếu trả!");
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (txtMaHoaDon.Text.Trim() == "")
            {
                MessageBox.Show("Chưa có mã hóa đơn để in!");
                return;
            }

            string maHD = txtMaHoaDon.Text.Trim();

            FormRepHoaDon f = new FormRepHoaDon(maHD);
            f.ShowDialog();
        }
    }
}