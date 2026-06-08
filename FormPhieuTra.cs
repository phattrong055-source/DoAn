using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class FormPhieuTra : Form
    {
        string nguon = @"Data Source=DESKTOP-S4KUUMQ\SQLEXPRESS;Initial Catalog=DoAnTotNghiep;Integrated Security=True";

        int tongPinDaGiao = 0;
        int tongHatSangDaGiao = 0;
        int tongChanQuayDaGiao = 0;
        int tongFlashDaGiao = 0;

        CultureInfo vn = new CultureInfo("vi-VN");

        public FormPhieuTra()
        {
            InitializeComponent();

            txtTienPS.Leave += txtTienPS_Leave;
            txtTienPS.TextChanged += txtTienPS_TextChanged;
        }

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

            if (soTien > 0 && soTien < 10000)
            {
                soTien = soTien * 1000;
            }

            return soTien;
        }

        private void TinhTongThanhToan()
        {
            decimal tongTienThue = ChuyenSangSo(txtTTThue.Text);
            decimal phiPhatSinh = ChuyenSangSo(txtPhiPS.Text);

            decimal tongThanhToan = tongTienThue + phiPhatSinh;

            txtTongPhi.Text = DinhDangTien(tongThanhToan);
        }

        private void txtTienPS_TextChanged(object sender, EventArgs e)
        {
            decimal phiPhatSinh = ChuyenSangSo(txtTienPS.Text);

            txtPhiPS.Text = DinhDangTien(phiPhatSinh);

            TinhTongThanhToan();
        }

        private void txtTienPS_Leave(object sender, EventArgs e)
        {
            if (txtTienPS.Text.Trim() == "")
            {
                txtTienPS.Text = "0";
            }

            decimal phiPhatSinh = ChuyenSangSo(txtTienPS.Text);

            txtTienPS.Text = DinhDangTien(phiPhatSinh);
            txtPhiPS.Text = DinhDangTien(phiPhatSinh);

            TinhTongThanhToan();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int maPhieuThue;

            if (!int.TryParse(txtPhieuThue.Text, out maPhieuThue))
            {
                MessageBox.Show("Mã phiếu thuê không hợp lệ!");
                return;
            }

            using (SqlConnection conn = new SqlConnection(nguon))
            {
                conn.Open();

                SqlCommand cmdCheck = new SqlCommand(
                    "SELECT COUNT(*) FROM PhieuThue WHERE maPhieuThue = @ma", conn);

                cmdCheck.Parameters.AddWithValue("@ma", maPhieuThue);

                int count = (int)cmdCheck.ExecuteScalar();

                if (count == 0)
                {
                    MessageBox.Show("Phiếu thuê không tồn tại!");
                    return;
                }

                SqlCommand cmdPhieu = new SqlCommand(@"
                    SELECT PT.ngayLap, 
                           PT.ngayNhanMay, 
                           PT.ngayDuKienTra, 
                           PT.trangThai,
                           PT.tienCoc,
                           PT.taiSanCoc,
                           KH.hoTen, 
                           KH.sdt, 
                           KH.diaChi, 
                           KH.email, 
                           KH.cccd
                    FROM PhieuThue PT
                    INNER JOIN KhachHang KH ON PT.maKhachHang = KH.maKhachHang
                    WHERE PT.maPhieuThue = @ma", conn);

                cmdPhieu.Parameters.AddWithValue("@ma", maPhieuThue);

                using (SqlDataReader reader = cmdPhieu.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        dtpNgayTra.Value = Convert.ToDateTime(reader["ngayLap"]);

                        txtNgayNhan.Text = reader["ngayNhanMay"].ToString();
                        txtNgayTra.Text = reader["ngayDuKienTra"].ToString();
                        txtTrangThai.Text = reader["trangThai"].ToString();

                        txtHoTen.Text = reader["hoTen"].ToString();
                        txtSDT.Text = reader["sdt"].ToString();
                        txtDiaChi.Text = reader["diaChi"].ToString();
                        txtEmail.Text = reader["email"].ToString();
                        txtCCCD.Text = reader["cccd"].ToString();

                        if (reader["tienCoc"] != DBNull.Value)
                        {
                            decimal tienCoc = ChuanHoaTienTuCSDL(reader["tienCoc"]);
                            txtCoc.Text = DinhDangTien(tienCoc);
                        }
                        else
                        {
                            txtCoc.Text = DinhDangTien(0);
                        }

                        if (reader["taiSanCoc"] != DBNull.Value)
                        {
                            txtTSCoc.Text = reader["taiSanCoc"].ToString();
                        }
                        else
                        {
                            txtTSCoc.Text = "";
                        }
                    }
                }

                SqlDataAdapter da = new SqlDataAdapter(@"
                    SELECT CT.maMay, 
                           M.tenMay, 
                           CT.thoiGianThue, 
                           CT.soLuongThue, 
                           CT.donGia, 
                           CT.thanhTien, 
                           CT.ghiChu
                    FROM ChiTietPhieuThue CT
                    INNER JOIN MayAnh M ON CT.maMay = M.maMay
                    WHERE CT.maPhieuThue = @ma", conn);

                da.SelectCommand.Parameters.AddWithValue("@ma", maPhieuThue);

                DataTable dt = new DataTable();
                da.Fill(dt);

                foreach (DataRow row in dt.Rows)
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
                dataGridView1.DataSource = dt;

                DinhDangTienTrongDataGridView(dataGridView1, "donGia");
                DinhDangTienTrongDataGridView(dataGridView1, "thanhTien");

                decimal tongTienThue = 0;

                foreach (DataRow row in dt.Rows)
                {
                    if (row["thanhTien"] != DBNull.Value)
                    {
                        tongTienThue += Convert.ToDecimal(row["thanhTien"]);
                    }
                }

                txtTTThue.Text = DinhDangTien(tongTienThue);

                if (txtPhiPS.Text.Trim() == "")
                {
                    txtPhiPS.Text = DinhDangTien(0);
                }

                TinhTongThanhToan();
            }
        }

        private void DinhDangTienTrongDataGridView(DataGridView dgv, string tenCot)
        {
            if (dgv.Columns.Contains(tenCot))
            {
                dgv.Columns[tenCot].DefaultCellStyle.Format = "C0";
                dgv.Columns[tenCot].DefaultCellStyle.FormatProvider = vn;
            }
        }

        private void btnNhanMay_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn ít nhất một máy trong danh sách!");
                return;
            }

            string maMay = dataGridView1.CurrentRow.Cells["maMay"].Value.ToString();

            foreach (DataGridViewRow row in dataGridView2.Rows)
            {
                if (row.Cells["maMay"].Value != null && row.Cells["maMay"].Value.ToString() == maMay)
                {
                    MessageBox.Show("Máy này đã được nhận trước đó!");
                    return;
                }
            }

            if (dataGridView2.Columns.Count == 0)
            {
                dataGridView2.Columns.Add("maMay", "Mã Máy");
                dataGridView2.Columns.Add("tenMay", "Tên Máy");
                dataGridView2.Columns.Add("hangMay", "Hãng Máy");
                dataGridView2.Columns.Add("soSerial", "Serial");
                dataGridView2.Columns.Add("loaiThietBi", "Loại Thiết Bị");
                dataGridView2.Columns.Add("chuyenChup", "Chuyên Chụp");
                dataGridView2.Columns.Add("giaThueGio", "Giá Thuê/Giờ");
                dataGridView2.Columns.Add("giaThueNgay", "Giá Thuê/Ngày");
                dataGridView2.Columns.Add("trangThai", "Trạng Thái");
                dataGridView2.Columns.Add("moTa", "Mô Tả");
            }

            using (SqlConnection conn = new SqlConnection(nguon))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(@"
                    SELECT maMay, tenMay, hangMay, soSerial, loaiThietBi, chuyenChup, 
                           giaThueTheoGio, giaThueTheoNgay, trangThai 
                    FROM dbo.MayAnh 
                    WHERE maMay = @ma", conn);

                cmd.Parameters.AddWithValue("@ma", maMay);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        int rowIndex = dataGridView2.Rows.Add();
                        DataGridViewRow newRow = dataGridView2.Rows[rowIndex];

                        newRow.Cells["maMay"].Value = reader["maMay"].ToString();
                        newRow.Cells["tenMay"].Value = reader["tenMay"].ToString();
                        newRow.Cells["hangMay"].Value = reader["hangMay"].ToString();
                        newRow.Cells["soSerial"].Value = reader["soSerial"].ToString();
                        newRow.Cells["loaiThietBi"].Value = reader["loaiThietBi"].ToString();
                        newRow.Cells["chuyenChup"].Value = reader["chuyenChup"].ToString();

                        decimal giaGio = ChuanHoaTienTuCSDL(reader["giaThueTheoGio"]);
                        decimal giaNgay = ChuanHoaTienTuCSDL(reader["giaThueTheoNgay"]);

                        newRow.Cells["giaThueGio"].Value = DinhDangTien(giaGio);
                        newRow.Cells["giaThueNgay"].Value = DinhDangTien(giaNgay);

                        newRow.Cells["trangThai"].Value = reader["trangThai"].ToString();
                        newRow.Cells["moTa"].Value = "";
                    }
                }

                SqlCommand cmdPhuKien = new SqlCommand(@"
                    SELECT maPhuKien, soLuong
                    FROM PhuKienMay
                    WHERE maMay = @maMay", conn);

                cmdPhuKien.Parameters.AddWithValue("@maMay", maMay);

                using (SqlDataReader readerPK = cmdPhuKien.ExecuteReader())
                {
                    while (readerPK.Read())
                    {
                        int maPhuKien = Convert.ToInt32(readerPK["maPhuKien"]);

                        int soLuong = 0;
                        if (readerPK["soLuong"] != DBNull.Value)
                        {
                            soLuong = Convert.ToInt32(readerPK["soLuong"]);
                        }

                        if (maPhuKien == 1)
                            tongPinDaGiao += soLuong;
                        else if (maPhuKien == 2)
                            tongHatSangDaGiao += soLuong;
                        else if (maPhuKien == 3)
                            tongChanQuayDaGiao += soLuong;
                        else if (maPhuKien == 4)
                            tongFlashDaGiao += soLuong;
                    }
                }

                txtSLPin.Text = tongPinDaGiao.ToString();
                txtSLHatSang.Text = tongHatSangDaGiao.ToString();
                txtSLChanQuay.Text = tongChanQuayDaGiao.ToString();
                txtSLFlash.Text = tongFlashDaGiao.ToString();
            }
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView2.CurrentRow == null) return;

            int maMay = Convert.ToInt32(dataGridView2.CurrentRow.Cells["maMay"].Value);

            using (SqlConnection conn = new SqlConnection(nguon))
            {
                conn.Open();

                string sql = @"SELECT maPhuKien, soLuong 
                               FROM PhuKienMay 
                               WHERE maMay = @maMay";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@maMay", maMay);

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    int maPhuKien = Convert.ToInt32(reader["maPhuKien"]);
                    int soLuong = Convert.ToInt32(reader["soLuong"]);

                    switch (maPhuKien)
                    {
                        case 1:
                            txtSLPin.Text = soLuong.ToString();
                            break;
                        case 2:
                            txtSLHatSang.Text = soLuong.ToString();
                            break;
                        case 3:
                            txtSLChanQuay.Text = soLuong.ToString();
                            break;
                        case 4:
                            txtSLFlash.Text = soLuong.ToString();
                            break;
                    }
                }

                reader.Close();
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            dataGridView2.EndEdit();

            int maPhieuThue;

            if (!int.TryParse(txtPhieuThue.Text.Trim(), out maPhieuThue))
            {
                MessageBox.Show("Mã phiếu thuê không hợp lệ!");
                return;
            }

            decimal tongTienThue = ChuyenSangSo(txtTTThue.Text);
            decimal phiPhatSinh = ChuyenSangSo(txtPhiPS.Text);
            decimal tongThanhToan = ChuyenSangSo(txtTongPhi.Text);

            using (SqlConnection conn = new SqlConnection(nguon))
            {
                conn.Open();

                SqlTransaction tran = conn.BeginTransaction();

                try
                {
                    if (KiemTraPhieuTraDaTonTai(conn, tran, maPhieuThue))
                    {
                        MessageBox.Show("Phiếu thuê này đã được lập phiếu trả rồi, không thể lưu lại lần nữa!");
                        tran.Rollback();
                        return;
                    }

                    int maPhieuTraMoi = LuuPhieuTra( conn,tran,maPhieuThue,tongTienThue,phiPhatSinh,tongThanhToan);
                    LuuPhiPhatSinh(conn,tran,maPhieuTraMoi,phiPhatSinh);
                    LuuHoaDon(conn,tran,maPhieuThue,tongTienThue,tongThanhToan);

                    SqlCommand cmdUpdatePhieuThue = new SqlCommand(@"
                        UPDATE PhieuThue
                        SET trangThai = N'Đã trả'
                        WHERE maPhieuThue = @maPhieuThue", conn, tran);

                    cmdUpdatePhieuThue.Parameters.AddWithValue("@maPhieuThue", maPhieuThue);
                    cmdUpdatePhieuThue.ExecuteNonQuery();

                    txtTrangThai.Text = "Đã trả";

                    CapNhatMayAnh(conn, tran);

                    int pinDaGiao = LaySoLuong(txtSLPin.Text);
                    int pinDaTra = LaySoLuong(txtSLPin1.Text);
                    int pinBiMat = pinDaGiao - pinDaTra;

                    int hatSangDaGiao = LaySoLuong(txtSLHatSang.Text);
                    int hatSangDaTra = LaySoLuong(txtSLHatSang1.Text);
                    int hatSangBiMat = hatSangDaGiao - hatSangDaTra;

                    int chanQuayDaGiao = LaySoLuong(txtSLChanQuay.Text);
                    int chanQuayDaTra = LaySoLuong(txtSLChanQuay1.Text);
                    int chanQuayBiMat = chanQuayDaGiao - chanQuayDaTra;

                    int flashDaGiao = LaySoLuong(txtSLFlash.Text);
                    int flashDaTra = LaySoLuong(txtSLFlash1.Text);
                    int flashBiMat = flashDaGiao - flashDaTra;

                    if (pinBiMat > 0)
                        XuLyPhuKienBiMat(conn, tran, 1, pinBiMat);

                    if (hatSangBiMat > 0)
                        XuLyPhuKienBiMat(conn, tran, 2, hatSangBiMat);

                    if (chanQuayBiMat > 0)
                        XuLyPhuKienBiMat(conn, tran, 3, chanQuayBiMat);

                    if (flashBiMat > 0)
                        XuLyPhuKienBiMat(conn, tran, 4, flashBiMat);

                    tran.Commit();

                    dataGridView2.Refresh();
                    MessageBox.Show("Lưu phiếu trả và phí phát sinh thành công!");
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    MessageBox.Show("Lỗi khi lưu phiếu trả: " + ex.Message);
                }
            }
        }

        private bool KiemTraPhieuTraDaTonTai(SqlConnection conn, SqlTransaction tran, int maPhieuThue)
        {
            SqlCommand cmd = new SqlCommand(@"
                SELECT COUNT(*)
                FROM PhieuTra
                WHERE maPhieuThue = @maPhieuThue", conn, tran);

            cmd.Parameters.AddWithValue("@maPhieuThue", maPhieuThue);

            int count = Convert.ToInt32(cmd.ExecuteScalar());

            return count > 0;
        }

        private int LayMaPhieuTraMoi(SqlConnection conn, SqlTransaction tran)
        {
            SqlCommand cmd = new SqlCommand(@"
                SELECT ISNULL(MAX(maPhieuTra), 0) + 1
                FROM PhieuTra", conn, tran);

            return Convert.ToInt32(cmd.ExecuteScalar());
        }

        private int LayMaPhatSinhMoi(SqlConnection conn, SqlTransaction tran)
        {
            SqlCommand cmd = new SqlCommand(@"
                SELECT ISNULL(MAX(maPhatSinh), 0) + 1
                FROM PhiPhatSinh", conn, tran);

            return Convert.ToInt32(cmd.ExecuteScalar());
        }

        private bool KiemTraCotTonTai(SqlConnection conn, SqlTransaction tran, string tenBang, string tenCot)
        {
            SqlCommand cmd = new SqlCommand(@"
                SELECT COUNT(*)
                FROM INFORMATION_SCHEMA.COLUMNS
                WHERE TABLE_NAME = @tenBang
                  AND COLUMN_NAME = @tenCot", conn, tran);

            cmd.Parameters.AddWithValue("@tenBang", tenBang);
            cmd.Parameters.AddWithValue("@tenCot", tenCot);

            int count = Convert.ToInt32(cmd.ExecuteScalar());

            return count > 0;
        }

        private int LuuPhieuTra(SqlConnection conn, SqlTransaction tran, int maPhieuThue, decimal tongTienThue, decimal phiPhatSinh, decimal tongThanhToan)
        {
            int maPhieuTraMoi = LayMaPhieuTraMoi(conn, tran);

            string cot = "";
            string giaTri = "";

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.Transaction = tran;

            if (KiemTraCotTonTai(conn, tran, "PhieuTra", "maPhieuTra"))
            {
                cot += "maPhieuTra,";
                giaTri += "@maPhieuTra,";
                cmd.Parameters.AddWithValue("@maPhieuTra", maPhieuTraMoi);
            }

            if (KiemTraCotTonTai(conn, tran, "PhieuTra", "maPhieuThue"))
            {
                cot += "maPhieuThue,";
                giaTri += "@maPhieuThue,";
                cmd.Parameters.AddWithValue("@maPhieuThue", maPhieuThue);
            }

            if (KiemTraCotTonTai(conn, tran, "PhieuTra", "ngayTraThucTe"))
            {
                cot += "ngayTraThucTe,";
                giaTri += "@ngayTra,";
                cmd.Parameters.AddWithValue("@ngayTra", dtpNgayTra.Value);
            }
            else if (KiemTraCotTonTai(conn, tran, "PhieuTra", "ngayTraTT"))
            {
                cot += "ngayTraTT,";
                giaTri += "@ngayTra,";
                cmd.Parameters.AddWithValue("@ngayTra", dtpNgayTra.Value);
            }
            else if (KiemTraCotTonTai(conn, tran, "PhieuTra", "ngayTra"))
            {
                cot += "ngayTra,";
                giaTri += "@ngayTra,";
                cmd.Parameters.AddWithValue("@ngayTra", dtpNgayTra.Value);
            }

            if (KiemTraCotTonTai(conn, tran, "PhieuTra", "tongTienThue"))
            {
                cot += "tongTienThue,";
                giaTri += "@tongTienThue,";
                cmd.Parameters.AddWithValue("@tongTienThue", tongTienThue);
            }

            if (KiemTraCotTonTai(conn, tran, "PhieuTra", "phiPhatSinh"))
            {
                cot += "phiPhatSinh,";
                giaTri += "@phiPhatSinh,";
                cmd.Parameters.AddWithValue("@phiPhatSinh", phiPhatSinh);
            }
            else if (KiemTraCotTonTai(conn, tran, "PhieuTra", "tienPhatSinh"))
            {
                cot += "tienPhatSinh,";
                giaTri += "@phiPhatSinh,";
                cmd.Parameters.AddWithValue("@phiPhatSinh", phiPhatSinh);
            }
            else if (KiemTraCotTonTai(conn, tran, "PhieuTra", "tongPhiPhatSinh"))
            {
                cot += "tongPhiPhatSinh,";
                giaTri += "@phiPhatSinh,";
                cmd.Parameters.AddWithValue("@phiPhatSinh", phiPhatSinh);
            }

            if (KiemTraCotTonTai(conn, tran, "PhieuTra", "tongThanhToan"))
            {
                cot += "tongThanhToan,";
                giaTri += "@tongThanhToan,";
                cmd.Parameters.AddWithValue("@tongThanhToan", tongThanhToan);
            }
            else if (KiemTraCotTonTai(conn, tran, "PhieuTra", "tongTien"))
            {
                cot += "tongTien,";
                giaTri += "@tongThanhToan,";
                cmd.Parameters.AddWithValue("@tongThanhToan", tongThanhToan);
            }
            else if (KiemTraCotTonTai(conn, tran, "PhieuTra", "tongTienTra"))
            {
                cot += "tongTienTra,";
                giaTri += "@tongThanhToan,";
                cmd.Parameters.AddWithValue("@tongThanhToan", tongThanhToan);
            }

            if (cot.EndsWith(","))
                cot = cot.Substring(0, cot.Length - 1);

            if (giaTri.EndsWith(","))
                giaTri = giaTri.Substring(0, giaTri.Length - 1);

            if (cot == "")
                throw new Exception("Không tìm thấy cột phù hợp trong bảng PhieuTra để lưu dữ liệu.");

            cmd.CommandText = "INSERT INTO PhieuTra (" + cot + ") VALUES (" + giaTri + ")";
            cmd.ExecuteNonQuery();

            return maPhieuTraMoi;
        }

        private void LuuPhiPhatSinh(SqlConnection conn, SqlTransaction tran, int maPhieuTra, decimal phiPhatSinh)
        {
            string ghiChu = LayGhiChuPhatSinh();

            if (phiPhatSinh <= 0 && string.IsNullOrWhiteSpace(ghiChu))
            {
                return;
            }

            int maPhatSinhMoi = LayMaPhatSinhMoi(conn, tran);

            SqlCommand cmd = new SqlCommand(@"
                INSERT INTO PhiPhatSinh
                (
                    maPhatSinh,
                    maPhieuTra,
                    soTien,
                    ghiChu
                )
                VALUES
                (
                    @maPhatSinh,
                    @maPhieuTra,
                    @soTien,
                    @ghiChu
                )", conn, tran);

            cmd.Parameters.AddWithValue("@maPhatSinh", maPhatSinhMoi);
            cmd.Parameters.AddWithValue("@maPhieuTra", maPhieuTra);
            cmd.Parameters.AddWithValue("@soTien", phiPhatSinh);
            cmd.Parameters.AddWithValue("@ghiChu", ghiChu);

            cmd.ExecuteNonQuery();
        }

        private void LuuHoaDon(
    SqlConnection conn,
    SqlTransaction tran,
    int maPhieuThue,
    decimal tongTienThue,
    decimal tongThanhToan)
        {
            decimal tienCoc = ChuyenSangSo(txtCoc.Text);

            string phuongThuc = "";

            if (cmbPhuongThuc.SelectedItem != null)
                phuongThuc = cmbPhuongThuc.Text;

            SqlCommand cmd = new SqlCommand(@"
        INSERT INTO HoaDon
        (
            maPhieuTra,
            ngayLap,
            tongTienThue,
            tongTienThanhToan,
            tienCoc,
            thanhToan
        )
        VALUES
        (
            @maPhieuTra,
            GETDATE(),
            @tongTienThue,
            @tongTienThanhToan,
            @tienCoc,
            @thanhToan
        )", conn, tran);

            cmd.Parameters.AddWithValue("@maPhieuTra", maPhieuThue);
            cmd.Parameters.AddWithValue("@tongTienThue", tongTienThue);
            cmd.Parameters.AddWithValue("@tongTienThanhToan", tongThanhToan);
            cmd.Parameters.AddWithValue("@tienCoc", tienCoc);
            cmd.Parameters.AddWithValue("@thanhToan", phuongThuc);

            cmd.ExecuteNonQuery();
        }

        private string LayGhiChuPhatSinh()
        {
            string[] danhSachTenControl =
            {
                "txtGhiChuPhatSinh",
                "txtGhiChuPS",
                "txtGhiChuPhiPhatSinh",
                "txtGhiChu",
                "rtbGhiChuPhatSinh",
                "rtbGhiChuPS",
                "richTextBox1"
            };

            foreach (string ten in danhSachTenControl)
            {
                Control[] controls = this.Controls.Find(ten, true);

                if (controls.Length > 0)
                {
                    TextBox txt = controls[0] as TextBox;
                    if (txt != null)
                    {
                        return txt.Text.Trim();
                    }

                    RichTextBox rtb = controls[0] as RichTextBox;
                    if (rtb != null)
                    {
                        return rtb.Text.Trim();
                    }
                }
            }

            return "";
        }

        private void CapNhatMayAnh(SqlConnection conn, SqlTransaction tran)
        {
            bool coCotMoTa = KiemTraCotTonTai(conn, tran, "MayAnh", "moTa");
            bool coCotGhiChu = KiemTraCotTonTai(conn, tran, "MayAnh", "ghiChu");

            foreach (DataGridViewRow row in dataGridView2.Rows)
            {
                if (row.IsNewRow) continue;
                if (row.Cells["maMay"].Value == null) continue;

                int maMay = Convert.ToInt32(row.Cells["maMay"].Value);

                string ghiChuMay = "";

                if (row.Cells["moTa"].Value != null)
                {
                    ghiChuMay = row.Cells["moTa"].Value.ToString();
                }

                SqlCommand cmdUpdateMay = new SqlCommand();
                cmdUpdateMay.Connection = conn;
                cmdUpdateMay.Transaction = tran;

                if (coCotMoTa)
                {
                    cmdUpdateMay.CommandText = @"
                        UPDATE MayAnh
                        SET trangThai = N'Còn ở CH',
                            moTa = @ghiChu
                        WHERE maMay = @maMay";
                }
                else if (coCotGhiChu)
                {
                    cmdUpdateMay.CommandText = @"
                        UPDATE MayAnh
                        SET trangThai = N'Còn ở CH',
                            ghiChu = @ghiChu
                        WHERE maMay = @maMay";
                }
                else
                {
                    cmdUpdateMay.CommandText = @"
                        UPDATE MayAnh
                        SET trangThai = N'Còn ở CH'
                        WHERE maMay = @maMay";
                }

                cmdUpdateMay.Parameters.AddWithValue("@ghiChu", ghiChuMay);
                cmdUpdateMay.Parameters.AddWithValue("@maMay", maMay);

                cmdUpdateMay.ExecuteNonQuery();

                if (row.Cells["trangThai"] != null)
                {
                    row.Cells["trangThai"].Value = "Còn ở CH";
                }
            }
        }

        private int LaySoLuong(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return 0;

            int soLuong;
            int.TryParse(text.Trim(), out soLuong);

            return soLuong;
        }

        private void XuLyPhuKienBiMat(SqlConnection conn, SqlTransaction tran, int maPhuKien, int soLuongBiMat)
        {
            int soLuongConPhaiTru = soLuongBiMat;

            foreach (DataGridViewRow row in dataGridView2.Rows)
            {
                if (row.IsNewRow) continue;

                if (soLuongConPhaiTru <= 0)
                    break;

                if (row.Cells["maMay"].Value == null)
                    continue;

                int maMay = Convert.ToInt32(row.Cells["maMay"].Value);

                SqlCommand cmdLayPhuKien = new SqlCommand(@"
                    SELECT soLuong
                    FROM PhuKienMay
                    WHERE maMay = @maMay
                      AND maPhuKien = @maPhuKien", conn, tran);

                cmdLayPhuKien.Parameters.AddWithValue("@maMay", maMay);
                cmdLayPhuKien.Parameters.AddWithValue("@maPhuKien", maPhuKien);

                object result = cmdLayPhuKien.ExecuteScalar();

                if (result == null || result == DBNull.Value)
                    continue;

                int soLuongHienCo = Convert.ToInt32(result);

                if (soLuongHienCo <= 0)
                    continue;

                if (soLuongHienCo <= soLuongConPhaiTru)
                {
                    SqlCommand cmdXoa = new SqlCommand(@"
                        DELETE FROM PhuKienMay
                        WHERE maMay = @maMay
                          AND maPhuKien = @maPhuKien", conn, tran);

                    cmdXoa.Parameters.AddWithValue("@maMay", maMay);
                    cmdXoa.Parameters.AddWithValue("@maPhuKien", maPhuKien);

                    cmdXoa.ExecuteNonQuery();

                    soLuongConPhaiTru -= soLuongHienCo;
                }
                else
                {
                    int soLuongMoi = soLuongHienCo - soLuongConPhaiTru;

                    SqlCommand cmdCapNhat = new SqlCommand(@"
                        UPDATE PhuKienMay
                        SET soLuong = @soLuongMoi
                        WHERE maMay = @maMay
                          AND maPhuKien = @maPhuKien", conn, tran);

                    cmdCapNhat.Parameters.AddWithValue("@soLuongMoi", soLuongMoi);
                    cmdCapNhat.Parameters.AddWithValue("@maMay", maMay);
                    cmdCapNhat.Parameters.AddWithValue("@maPhuKien", maPhuKien);

                    cmdCapNhat.ExecuteNonQuery();

                    soLuongConPhaiTru = 0;
                }
            }
        }

     

       
        

        

        private void btnTrangChu_Click(object sender, EventArgs e)
        {
            FormTrangChu FTC = new FormTrangChu();
            FTC.Show();
            this.Close();
        }

      /*  private void btnMayAnh_Click(object sender, EventArgs e)
        {
            MayAnh Ma = new MayAnh();
            Ma.Show();
            this.Close();
        }
      */
        private void bntPhuKien_Click(object sender, EventArgs e)
        {
            FormPhuKien FPK = new FormPhuKien();
            FPK.Show();
            this.Close();
        }

      /*  private void btnKhachHang_Click(object sender, EventArgs e)
        {
            KhachHang KH = new KhachHang();
            KH.Show();
            this.Close();
        }*/

        private void btnPhieuThue_Click(object sender, EventArgs e)
        {
            FormPhieuThue PT = new FormPhieuThue();
            PT.Show();
            this.Close();
        }

        private void btnPhieuTra_Click(object sender, EventArgs e)
        {
            FormPhieuTra PTR = new FormPhieuTra();
            PTR.Show();
            this.Close();
        }

        private void btnHoaDon_Click(object sender, EventArgs e)
        {
            FormHoaDon FHD = new FormHoaDon();
            FHD.Show();
            this.Close();
        }

        private void btnThongKe_Click(object sender, EventArgs e)
        {

        }

        private void btnDangXuat_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
              "Bạn có chắc chắn muốn đăng xuất không?",
              "Xác nhận",
              MessageBoxButtons.YesNo,
              MessageBoxIcon.Question
          );

            if (result == DialogResult.Yes)
            {
                FormDangNhap frm = new FormDangNhap();
                frm.Show();
                this.Hide();
            }
        }
    }
}