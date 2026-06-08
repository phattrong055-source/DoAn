using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Globalization;

namespace WindowsFormsApp1
{
    public partial class FormPhieuThue : Form
    {
        string nguon = @"Data Source=DESKTOP-S4KUUMQ\SQLEXPRESS;Initial Catalog=DoAnTotNghiep;Integrated Security=True";
        string lenh = "";
        SqlConnection KetNoi;
        SqlCommand THucHien;
        SqlDataReader Doc;
        public FormPhieuThue()
        {
            InitializeComponent();
            CauHinhHienThiTienVND();
            LoadDuLieu();
        }
        private string FormatTienVND(decimal soTien)
        {
            return string.Format(CultureInfo.GetCultureInfo("vi-VN"), "{0:N0} VNĐ", soTien);
        }

        private void CauHinhHienThiTienVND()
        {
            dataGridView2.CellFormatting += DinhDangTienVNDTrongBang;
            dataGridView3.CellFormatting += DinhDangTienVNDTrongBang;

            txtTienCoc.Leave += TxtTienCoc_Leave;
        }

        private void DinhDangTienVNDTrongBang(object sender, DataGridViewCellFormattingEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;

            if (dgv == null || e.RowIndex < 0 || e.ColumnIndex < 0)
            {
                return;
            }

            string tenCot = dgv.Columns[e.ColumnIndex].Name;

            if (tenCot == "giaThueTheoGio" ||
                tenCot == "giaThueTheoNgay" ||
                tenCot == "DonGia" ||
                tenCot == "ThanhTien")
            {
                if (e.Value != null && e.Value != DBNull.Value)
                {
                    decimal soTien;

                    if (decimal.TryParse(e.Value.ToString(), out soTien))
                    {
                        e.Value = FormatTienVND(soTien);
                        e.FormattingApplied = true;
                    }
                }
            }
        }

        private void TxtTienCoc_Leave(object sender, EventArgs e)
        {
            decimal tienCoc;

            if (TryParseMoney(txtTienCoc.Text, out tienCoc))
            {
                txtTienCoc.Text = FormatTienVND(tienCoc);
            }
        }

        void LoadDuLieu()
        {
            SqlConnection conn = new SqlConnection(nguon);

            try
            {
                conn.Open();

                string sql = @"SELECT maKhachHang, hoTen, sdt, diaChi, email, cccd, ngayTao FROM dbo.KhachHang";

                SqlDataAdapter da = new SqlDataAdapter(sql, conn);

                DataTable dt = new DataTable();

                da.Fill(dt);

                dataGridView1.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }

            try
            {
                conn.Open();

                string sql = @"SELECT maMay, tenMay, hangMay, soSerial, loaiThietBi, chuyenChup, giaThueTheoGio, giaThueTheoNgay, trangThai, moTa FROM dbo.MayAnh";

                SqlDataAdapter da = new SqlDataAdapter(sql, conn);

                DataTable dt = new DataTable();

                da.Fill(dt);

                dataGridView2.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }


        }
        private void btnPhuKien_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
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

        private void btnTimKiemKH_Click(object sender, EventArgs e)
        {
            string sdtTim = txtTimKiemKH.Text.Trim(); // txtSDT là ô nhập SDT

            if (string.IsNullOrEmpty(sdtTim))
            {
                MessageBox.Show("Vui lòng nhập số điện thoại để tìm kiếm.");
                return;
            }

            using (SqlConnection conn = new SqlConnection(nguon))
            {
                try
                {
                    conn.Open();
                    string sql = @"SELECT maKhachHang, hoTen, sdt, diaChi, email, cccd, ngayTao 
                           FROM KhachHang 
                           WHERE sdt = @sdt";

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@sdt", sdtTim);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        // Hiển thị dữ liệu lên DataGridView
                        dataGridView1.DataSource = dt;

                        // Điền thông tin khách hàng vào các TextBox
                        txtHoTen.Text = dt.Rows[0]["hoTen"].ToString();
                        txtSDT.Text = dt.Rows[0]["sdt"].ToString();
                        txtDiaChi.Text = dt.Rows[0]["diaChi"].ToString();
                        txtEmail.Text = dt.Rows[0]["email"].ToString();
                        txtCCCD.Text = dt.Rows[0]["cccd"].ToString();
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy khách hàng, vui lòng nhập thông tin mới.");

                        txtHoTen.Text = "";
                        txtDiaChi.Text = "";
                        txtEmail.Text = "";
                        txtCCCD.Text = "";
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi tìm kiếm: " + ex.Message);
                }
            }
        }

        private void btnTimKiemMA_Click_1(object sender, EventArgs e)
        {
            string tenMayTim = txtTimKiemMA.Text.Trim(); // txtTimKiemMA là ô nhập tên máy

            if (string.IsNullOrEmpty(tenMayTim))
            {
                MessageBox.Show("Vui lòng nhập tên máy để tìm kiếm.");
                return;
            }

            using (SqlConnection conn = new SqlConnection(nguon))
            {
                try
                {
                    conn.Open();
                    string sql = @"SELECT maMay, tenMay, hangMay, soSerial, loaiThietBi, chuyenChup, 
                                  giaThueTheoGio, giaThueTheoNgay, trangThai, moTa 
                           FROM MayAnh 
                           WHERE tenMay LIKE @tenMay";

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@tenMay", "%" + tenMayTim + "%");

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        dataGridView2.DataSource = dt;
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy máy ảnh phù hợp.");
                        dataGridView2.DataSource = null;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi tìm kiếm máy ảnh: " + ex.Message);
                }
            }
        }




        private void btnThemMA_Click_1(object sender, EventArgs e)
        {
            if (dataGridView2.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn máy.");
                return;
            }

            DataGridViewRow row = dataGridView2.CurrentRow;

            int maMay = Convert.ToInt32(
                row.Cells["maMay"].Value);

            string tenMay =
                row.Cells["tenMay"]
                .Value.ToString();

            decimal giaGio =
                Convert.ToDecimal(
                row.Cells["giaThueTheoGio"]
                .Value);

            decimal giaNgay =
                Convert.ToDecimal(
                row.Cells["giaThueTheoNgay"]
                .Value);

            string trangThai =
                row.Cells["trangThai"]
                .Value.ToString();

            // KIỂM TRA TRẠNG THÁI

            if (trangThai != "Còn ? CH")
            {
                MessageBox.Show(
                    "Máy hiện đang: "
                    + trangThai);

                return;
            }

            // KHÔNG CHO THÊM TRÙNG

            foreach (DataGridViewRow r in dataGridView3.Rows)
            {
                if (!r.IsNewRow &&
                    r.Cells["MaMay"].Value != null)
                {
                    int maTonTai =
                        Convert.ToInt32(
                        r.Cells["MaMay"]
                        .Value);

                    if (maTonTai == maMay)
                    {
                        MessageBox.Show(
                            "Máy đã có trong phiếu thuê.\nXóa khỏi chi tiết mới thêm lại được."
                        );

                        return;
                    }
                }
            }

            bool coGio =
                !string.IsNullOrEmpty(
                txtGioThue.Text);

            bool coNgay =
                !string.IsNullOrEmpty(
                txtNgayThue.Text);

            if (coGio && coNgay)
            {
                MessageBox.Show(
                    "Chỉ nhập giờ hoặc ngày."
                );

                return;
            }

            if (!coGio && !coNgay)
            {
                MessageBox.Show(
                    "Nhập thời gian thuê."
                );

                return;
            }

            DateTime ngayNhan =
                dtpNgayNhan.Value;

            DateTime ngayTra =
                dtpNgayTra.Value;

            if (ngayTra.Date <
                ngayNhan.Date)
            {
                MessageBox.Show(
                    "Ngày trả phải bằng hoặc lớn hơn ngày nhận."
                );

                return;
            }

            int soNgayToiDa =
                (ngayTra.Date -
                ngayNhan.Date)
                .Days;

            if (soNgayToiDa == 0)
                soNgayToiDa = 1;

            int soGioToiDa =
                (int)
                (
                ngayTra -
                ngayNhan
                ).TotalHours;

            if (soGioToiDa <= 0)
                soGioToiDa = 24;

            decimal donGia = 0;
            decimal thanhTien = 0;

            string thoiGianThue = "";

            int soLuong = 1;

            // THUÊ GIỜ

            if (coGio)
            {
                int soGio;

                if (!int.TryParse(
                    txtGioThue.Text,
                    out soGio))
                {
                    MessageBox.Show(
                        "Số giờ không hợp lệ."
                    );

                    return;
                }

                if (soGio >
                    soGioToiDa)
                {
                    MessageBox.Show(
                        "Tối đa "
                        + soGioToiDa
                        + " giờ"
                    );

                    return;
                }

                donGia = giaGio;

                thanhTien =
                    giaGio *
                    soGio;

                thoiGianThue =
                    soGio +
                    " giờ";
            }

            // THUÊ NGÀY

            if (coNgay)
            {
                int soNgay;

                if (!int.TryParse(
                    txtNgayThue.Text,
                    out soNgay))
                {
                    MessageBox.Show(
                        "Số ngày không hợp lệ."
                    );

                    return;
                }

                if (soNgay >
                    soNgayToiDa)
                {
                    MessageBox.Show(
                        "Tối đa "
                        + soNgayToiDa
                        + " ngày"
                    );

                    return;
                }

                donGia =
                    giaNgay;

                thanhTien =
                    giaNgay *
                    soNgay;

                thoiGianThue =
                    soNgay +
                    " ngày";
            }

            // THÊM VÀO CHI TIẾT

            int index =
                dataGridView3
                .Rows.Add();

            dataGridView3
                .Rows[index]
                .Cells["MaMay"]
                .Value = maMay;

            dataGridView3
                .Rows[index]
                .Cells["TenMay"]
                .Value = tenMay;

            dataGridView3
                .Rows[index]
                .Cells["ThoiGianThue"]
                .Value =
                thoiGianThue;

            dataGridView3
                .Rows[index]
                .Cells["SoLuong"]
                .Value =
                soLuong;

            dataGridView3
                .Rows[index]
                .Cells["DonGia"]
                .Value =
                donGia;

            dataGridView3
                .Rows[index]
                .Cells["ThanhTien"]
                .Value =
                thanhTien;

            dataGridView3
                .Rows[index]
                .Cells["GhiChu"]
                .Value = "";

            // XÓA KHỎI DANH SÁCH MÁY

            dataGridView2.Rows.Remove(
                dataGridView2.CurrentRow);

            txtGioThue.Clear();
            txtNgayThue.Clear();

            CapNhatTongTien();
            CapNhatTongSoMay();

            MessageBox.Show(
                "Thêm máy thành công."
            );
        }
        // Hàm cập nhật tổng tiền thuê
        private void CapNhatTongTien()
        {
            decimal tong = 0;

            foreach (DataGridViewRow row in dataGridView3.Rows)
            {
                if (!row.IsNewRow && row.Cells["ThanhTien"].Value != null)
                {
                    decimal thanhTien;

                    if (TryParseMoney(row.Cells["ThanhTien"].Value.ToString(), out thanhTien))
                    {
                        tong += thanhTien * 10;
                    }
                }
            }

            txtTongTienThue.Text = FormatTienVND(tong);
        }

        // Hàm cập nhật tổng số máy
        private void CapNhatTongSoMay()
        {
            int tongMay = 0;
            foreach (DataGridViewRow row in dataGridView3.Rows)
            {
                if (row.Cells["SoLuong"].Value != null && row.Cells["SoLuong"].Value != DBNull.Value)
                {
                    if (int.TryParse(row.Cells["SoLuong"].Value.ToString(), out int soLuong))
                    {
                        tongMay += soLuong *10;
                    }
                }
            }
            txtTongSoMay.Text = tongMay.ToString();
        }

        // Khi xóa máy khỏi chi tiết phiếu thuê
        private void btnXoaMA_Click(object sender, EventArgs e)
        {
            if (dataGridView3.CurrentRow == null || dataGridView3.CurrentRow.IsNewRow) return;

            dataGridView3.Rows.Remove(dataGridView3.CurrentRow);

            // Cập nhật tổng tiền và tổng số máy
            CapNhatTongTien();
            CapNhatTongSoMay();
        }

        private void btnXoaMA_Click_1(object sender, EventArgs e)
        {


            if (dataGridView3.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn máy muốn thu hồi.");
                return;
            }

            // Kiểm tra xem có phải dòng mới không
            if (dataGridView3.CurrentRow.IsNewRow)
            {
                MessageBox.Show("Không thể xóa dòng này vì là dòng mới chưa thêm dữ liệu.");
                return;
            }

            // Lấy thông tin máy từ dòng đang chọn
            int maMay = Convert.ToInt32(dataGridView3.CurrentRow.Cells["MaMay"].Value);

            // Xóa máy khỏi DataGridView chi tiết phiếu thuê
            dataGridView3.Rows.Remove(dataGridView3.CurrentRow);

            // Kiểm tra xem máy này đã có trong DataGridView máy ảnh chưa
            bool mayDaCo = false;
            foreach (DataGridViewRow row in dataGridView2.Rows)
            {
                if (!row.IsNewRow && Convert.ToInt32(row.Cells["maMay"].Value) == maMay)
                {
                    mayDaCo = true;
                    break;
                }
            }

            // Nếu máy chưa có trong DataGridView máy ảnh, thêm lại
            if (!mayDaCo)
            {
                using (SqlConnection conn = new SqlConnection(nguon))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(
                        "SELECT maMay, tenMay, hangMay, soSerial, chuyenChup, giaThueTheoGio, giaThueTheoNgay, trangThai " +
                        "FROM MayAnh WHERE maMay=@maMay", conn);
                    cmd.Parameters.AddWithValue("@maMay", maMay);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    foreach (DataRow r in dt.Rows)
                    {
                        dataGridView2.Rows.Add(
                            r["maMay"],
                            r["tenMay"],
                            r["hangMay"],
                            r["soSerial"],
                            r["chuyenChup"],
                            r["giaThueTheoGio"],
                            r["giaThueTheoNgay"],
                            r["trangThai"]
                        );
                    }
                }
            }

            // Cập nhật lại tổng tiền sau khi xóa máy
            decimal tongTien = 0;
            foreach (DataGridViewRow r in dataGridView3.Rows)
            {
                if (!r.IsNewRow && r.Cells["ThanhTien"].Value != null)
                {
                    decimal thanhTien;

                    if (TryParseMoney(r.Cells["ThanhTien"].Value.ToString(), out thanhTien))
                    {
                        tongTien += thanhTien;
                    }
                }
            }
            txtTongTienThue.Text = FormatTienVND(tongTien);
        }



        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView2.CurrentRow == null) return;

            int maMay = Convert.ToInt32(dataGridView2.CurrentRow.Cells["maMay"].Value);

            // Reset checkbox và số lượng
            cbPin.Checked = false;
            cbHatSang.Checked = false;
            cbChanMay.Checked = false;
            cbFlash.Checked = false;

            txtPin.Text = "0";
            txtSLHatSang.Text = "0";
            txtSLChanMay.Text = "0";
            txtSLFlash.Text = "0";

            using (SqlConnection conn = new SqlConnection(nguon))
            {
                conn.Open();
                string sql = @"SELECT maPhuKien, soLuong 
                       FROM PhuKienMay 
                       WHERE maMay=@maMay";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@maMay", maMay);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    int maPhuKien = Convert.ToInt32(reader["maPhuKien"]);
                    int soLuong = Convert.ToInt32(reader["soLuong"]);

                    // Tick checkbox và hiển thị số lượng
                    switch (maPhuKien)
                    {
                        case 1:
                            cbPin.Checked = true;
                            txtPin.Text = soLuong.ToString();
                            break;
                        case 2:
                            cbHatSang.Checked = true;
                            txtSLHatSang.Text = soLuong.ToString();
                            break;
                        case 3:
                            cbChanMay.Checked = true;
                            txtSLChanMay.Text = soLuong.ToString();
                            break;
                        case 4:
                            cbFlash.Checked = true;
                            txtSLFlash.Text = soLuong.ToString();
                            break;
                    }
                }
                reader.Close();
            }


        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtHoTen.Text))
            {
                MessageBox.Show("Vui lòng nhập họ tên khách hàng.");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtSDT.Text))
            {
                MessageBox.Show("Vui lòng nhập số điện thoại khách hàng.");
                return;
            }

            bool coMayTrongPhieu = false;
            foreach (DataGridViewRow r in dataGridView3.Rows)
            {
                if (!r.IsNewRow)
                {
                    coMayTrongPhieu = true;
                    break;
                }
            }

            if (!coMayTrongPhieu)
            {
                MessageBox.Show("Vui lòng thêm máy vào phiếu thuê trước khi lưu.");
                return;
            }

            int tongSoMay;
            decimal tongTienThue;
            decimal tienCoc;

            if (!int.TryParse(txtTongSoMay.Text.Trim(), out tongSoMay))
            {
                MessageBox.Show("Tổng số máy không hợp lệ.");
                return;
            }

            if (!TryParseMoney(txtTongTienThue.Text, out tongTienThue))
            {
                MessageBox.Show("Tổng tiền thuê không hợp lệ.");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtTienCoc.Text))
            {
                tienCoc = 0;
            }
            else if (!TryParseMoney(txtTienCoc.Text, out tienCoc))
            {
                MessageBox.Show("Tiền cọc không hợp lệ.");
                return;
            }

            using (SqlConnection conn = new SqlConnection(nguon))
            {
                conn.Open();

                SqlTransaction transaction = conn.BeginTransaction();

                try
                {
                    int maKhachHang;

                    string hoTen = txtHoTen.Text.Trim();
                    string sdt = txtSDT.Text.Trim();
                    string diaChi = txtDiaChi.Text.Trim();
                    string email = txtEmail.Text.Trim();
                    string cccd = txtCCCD.Text.Trim();

                    // 1. KIỂM TRA KHÁCH HÀNG CŨ THEO SỐ ĐIỆN THOẠI
                    SqlCommand checkKH = new SqlCommand(
                        @"SELECT maKhachHang 
                          FROM KhachHang 
                          WHERE sdt = @sdt",
                        conn,
                        transaction
                    );

                    checkKH.Parameters.AddWithValue("@sdt", sdt);

                    object result = checkKH.ExecuteScalar();

                    if (result != null && result != DBNull.Value)
                    {
                        maKhachHang = Convert.ToInt32(result);
                    }
                    else
                    {
                        // Lỗi bạn gặp là do maKhachHang trong SQL không tự tăng IDENTITY.
                        // Vì vậy đoạn này tự lấy MAX(maKhachHang) + 1 rồi insert vào bảng KhachHang.
                        bool maKhachHangTuTangTrongSQL = IsIdentityColumn(conn, transaction, "KhachHang", "maKhachHang");

                        if (maKhachHangTuTangTrongSQL)
                        {
                            SqlCommand insertKH = new SqlCommand(
                                @"INSERT INTO KhachHang
                                  (hoTen, sdt, diaChi, email, cccd, ngayTao)
                                  VALUES
                                  (@hoTen, @sdt, @diaChi, @email, @cccd, @ngayTao);
                                  SELECT CAST(SCOPE_IDENTITY() AS INT);",
                                conn,
                                transaction
                            );

                            insertKH.Parameters.AddWithValue("@hoTen", hoTen);
                            insertKH.Parameters.AddWithValue("@sdt", sdt);
                            insertKH.Parameters.AddWithValue("@diaChi", diaChi);
                            insertKH.Parameters.AddWithValue("@email", email);
                            insertKH.Parameters.AddWithValue("@cccd", cccd);
                            insertKH.Parameters.AddWithValue("@ngayTao", DateTime.Now);

                            maKhachHang = Convert.ToInt32(insertKH.ExecuteScalar());
                        }
                        else
                        {
                            maKhachHang = GetNextId(conn, transaction, "KhachHang", "maKhachHang");

                            SqlCommand insertKH = new SqlCommand(
                                @"INSERT INTO KhachHang
                                  (maKhachHang, hoTen, sdt, diaChi, email, cccd, ngayTao)
                                  VALUES
                                  (@maKhachHang, @hoTen, @sdt, @diaChi, @email, @cccd, @ngayTao);",
                                conn,
                                transaction
                            );

                            insertKH.Parameters.AddWithValue("@maKhachHang", maKhachHang);
                            insertKH.Parameters.AddWithValue("@hoTen", hoTen);
                            insertKH.Parameters.AddWithValue("@sdt", sdt);
                            insertKH.Parameters.AddWithValue("@diaChi", diaChi);
                            insertKH.Parameters.AddWithValue("@email", email);
                            insertKH.Parameters.AddWithValue("@cccd", cccd);
                            insertKH.Parameters.AddWithValue("@ngayTao", DateTime.Now);

                            insertKH.ExecuteNonQuery();
                        }
                    }

                    // 2. LƯU PHIẾU THUÊ
                    int maPhieuThue;
                    bool maPhieuThueTuTangTrongSQL = IsIdentityColumn(conn, transaction, "PhieuThue", "maPhieuThue");

                    if (maPhieuThueTuTangTrongSQL)
                    {
                        SqlCommand insertPT = new SqlCommand(
                            @"INSERT INTO PhieuThue
                              (maKhachHang, ngayLap, ngayNhanMay, ngayDuKienTra,
                               tongSoMayThue, tongTienThue, tienCoc, taiSanCoc, ghiChu, trangThai)
                              VALUES
                              (@maKhachHang, @ngayLap, @ngayNhanMay, @ngayDuKienTra,
                               @tongSoMayThue, @tongTienThue, @tienCoc, @taiSanCoc, @ghiChu, @trangThai);
                              SELECT CAST(SCOPE_IDENTITY() AS INT);",
                            conn,
                            transaction
                        );

                        insertPT.Parameters.AddWithValue("@maKhachHang", maKhachHang);
                        insertPT.Parameters.AddWithValue("@ngayLap", dtpNgayLap.Value);
                        insertPT.Parameters.AddWithValue("@ngayNhanMay", dtpNgayNhan.Value);
                        insertPT.Parameters.AddWithValue("@ngayDuKienTra", dtpNgayTra.Value);
                        insertPT.Parameters.AddWithValue("@tongSoMayThue", tongSoMay);
                        insertPT.Parameters.AddWithValue("@tongTienThue", tongTienThue);
                        insertPT.Parameters.AddWithValue("@tienCoc", tienCoc);
                        insertPT.Parameters.AddWithValue("@taiSanCoc", txtTaiSanCoc.Text.Trim());
                        insertPT.Parameters.AddWithValue("@ghiChu", "");
                        insertPT.Parameters.AddWithValue("@trangThai", "Đang thuê");

                        maPhieuThue = Convert.ToInt32(insertPT.ExecuteScalar());
                    }
                    else
                    {
                        maPhieuThue = GetNextId(conn, transaction, "PhieuThue", "maPhieuThue");

                        SqlCommand insertPT = new SqlCommand(
                            @"INSERT INTO PhieuThue
                              (maPhieuThue, maKhachHang, ngayLap, ngayNhanMay, ngayDuKienTra,
                               tongSoMayThue, tongTienThue, tienCoc, taiSanCoc, ghiChu, trangThai)
                              VALUES
                              (@maPhieuThue, @maKhachHang, @ngayLap, @ngayNhanMay, @ngayDuKienTra,
                               @tongSoMayThue, @tongTienThue, @tienCoc, @taiSanCoc, @ghiChu, @trangThai);",
                            conn,
                            transaction
                        );

                        insertPT.Parameters.AddWithValue("@maPhieuThue", maPhieuThue);
                        insertPT.Parameters.AddWithValue("@maKhachHang", maKhachHang);
                        insertPT.Parameters.AddWithValue("@ngayLap", dtpNgayLap.Value);
                        insertPT.Parameters.AddWithValue("@ngayNhanMay", dtpNgayNhan.Value);
                        insertPT.Parameters.AddWithValue("@ngayDuKienTra", dtpNgayTra.Value);
                        insertPT.Parameters.AddWithValue("@tongSoMayThue", tongSoMay);
                        insertPT.Parameters.AddWithValue("@tongTienThue", tongTienThue);
                        insertPT.Parameters.AddWithValue("@tienCoc", tienCoc);
                        insertPT.Parameters.AddWithValue("@taiSanCoc", txtTaiSanCoc.Text.Trim());
                        insertPT.Parameters.AddWithValue("@ghiChu", "");
                        insertPT.Parameters.AddWithValue("@trangThai", "Đang thuê");

                        insertPT.ExecuteNonQuery();
                    }

                    // 3. LƯU CHI TIẾT PHIẾU THUÊ TỪ DATAGRIDVIEW3
                    // Bảng của bạn đang có cột khóa chính là maChiTiet, không phải maChiTietPhieuThue.
                    // Vì vậy cần tự kiểm tra đúng tên cột rồi insert mã chi tiết vào.
                    string tenCotMaChiTiet = "";

                    if (ColumnExists(conn, transaction, "ChiTietPhieuThue", "maChiTiet"))
                    {
                        tenCotMaChiTiet = "maChiTiet";
                    }
                    else if (ColumnExists(conn, transaction, "ChiTietPhieuThue", "maChiTietPhieuThue"))
                    {
                        tenCotMaChiTiet = "maChiTietPhieuThue";
                    }

                    bool coCotMaChiTiet = tenCotMaChiTiet != "";
                    bool maChiTietTuTangTrongSQL = false;

                    if (coCotMaChiTiet)
                    {
                        maChiTietTuTangTrongSQL = IsIdentityColumn(conn, transaction, "ChiTietPhieuThue", tenCotMaChiTiet);
                    }

                    foreach (DataGridViewRow row in dataGridView3.Rows)
                    {
                        if (row.IsNewRow) continue;

                        int maMay = Convert.ToInt32(row.Cells["MaMay"].Value);
                        string thoiGianThue = row.Cells["ThoiGianThue"].Value.ToString();
                        int soLuong = Convert.ToInt32(row.Cells["SoLuong"].Value);
                        decimal donGia = Convert.ToDecimal(row.Cells["DonGia"].Value);
                        decimal thanhTien = Convert.ToDecimal(row.Cells["ThanhTien"].Value);

                        string ghiChu = "";
                        if (row.Cells["GhiChu"].Value != null)
                        {
                            ghiChu = row.Cells["GhiChu"].Value.ToString();
                        }

                        SqlCommand insertCT;

                        if (coCotMaChiTiet && !maChiTietTuTangTrongSQL)
                        {
                            int maChiTiet = GetNextId(conn, transaction, "ChiTietPhieuThue", tenCotMaChiTiet);

                            insertCT = new SqlCommand(
                                @"INSERT INTO ChiTietPhieuThue
                                  ([" + tenCotMaChiTiet + @"], maPhieuThue, maMay, thoiGianThue, soLuongThue, donGia, thanhTien, ghiChu)
                                  VALUES
                                  (@maChiTiet, @maPhieuThue, @maMay, @thoiGianThue, @soLuongThue, @donGia, @thanhTien, @ghiChu)",
                                conn,
                                transaction
                            );

                            insertCT.Parameters.AddWithValue("@maChiTiet", maChiTiet);
                        }
                        else
                        {
                            insertCT = new SqlCommand(
                                @"INSERT INTO ChiTietPhieuThue
                                  (maPhieuThue, maMay, thoiGianThue, soLuongThue, donGia, thanhTien, ghiChu)
                                  VALUES
                                  (@maPhieuThue, @maMay, @thoiGianThue, @soLuongThue, @donGia, @thanhTien, @ghiChu)",
                                conn,
                                transaction
                            );
                        }

                        insertCT.Parameters.AddWithValue("@maPhieuThue", maPhieuThue);
                        insertCT.Parameters.AddWithValue("@maMay", maMay);
                        insertCT.Parameters.AddWithValue("@thoiGianThue", thoiGianThue);
                        insertCT.Parameters.AddWithValue("@soLuongThue", soLuong);
                        insertCT.Parameters.AddWithValue("@donGia", donGia);
                        insertCT.Parameters.AddWithValue("@thanhTien", thanhTien);
                        insertCT.Parameters.AddWithValue("@ghiChu", ghiChu);

                        insertCT.ExecuteNonQuery();

                        // 4. CẬP NHẬT TRẠNG THÁI MÁY SAU KHI LƯU PHIẾU THUÊ
                        SqlCommand updateMay = new SqlCommand(
                            @"UPDATE MayAnh
                              SET trangThai = @trangThai
                              WHERE maMay = @maMay",
                            conn,
                            transaction
                        );

                        updateMay.Parameters.AddWithValue("@trangThai", "Đang thuê");
                        updateMay.Parameters.AddWithValue("@maMay", maMay);
                        updateMay.ExecuteNonQuery();
                    }

                    transaction.Commit();

                    MessageBox.Show("Lưu phiếu thuê thành công!");

                    LamMoiSauKhiLuu();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    MessageBox.Show("Lỗi khi lưu phiếu thuê: " + ex.Message);
                }
            }
        }

        private int GetNextId(SqlConnection conn, SqlTransaction transaction, string tableName, string columnName)
        {
            string sql = "SELECT ISNULL(MAX([" + columnName + "]), 0) + 1 FROM [" + tableName + "] WITH (UPDLOCK, HOLDLOCK)";

            SqlCommand cmd = new SqlCommand(sql, conn, transaction);
            return Convert.ToInt32(cmd.ExecuteScalar());
        }

        private bool IsIdentityColumn(SqlConnection conn, SqlTransaction transaction, string tableName, string columnName)
        {
            SqlCommand cmd = new SqlCommand(
                @"SELECT COLUMNPROPERTY(OBJECT_ID(@tableName), @columnName, 'IsIdentity')",
                conn,
                transaction
            );

            cmd.Parameters.AddWithValue("@tableName", "dbo." + tableName);
            cmd.Parameters.AddWithValue("@columnName", columnName);

            object result = cmd.ExecuteScalar();

            if (result == null || result == DBNull.Value)
                return false;

            return Convert.ToInt32(result) == 1;
        }

        private bool ColumnExists(SqlConnection conn, SqlTransaction transaction, string tableName, string columnName)
        {
            SqlCommand cmd = new SqlCommand(
                @"SELECT COUNT(*)
                  FROM INFORMATION_SCHEMA.COLUMNS
                  WHERE TABLE_SCHEMA = 'dbo'
                    AND TABLE_NAME = @tableName
                    AND COLUMN_NAME = @columnName",
                conn,
                transaction
            );

            cmd.Parameters.AddWithValue("@tableName", tableName);
            cmd.Parameters.AddWithValue("@columnName", columnName);

            return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
        }

        private bool TryParseMoney(string text, out decimal value)
        {
            value = 0;

            if (string.IsNullOrWhiteSpace(text))
            {
                return false;
            }

            string cleaned = text.Trim();
            cleaned = cleaned.Replace("VNĐ", "");
            cleaned = cleaned.Replace("VND", "");
            cleaned = cleaned.Replace("vnđ", "");
            cleaned = cleaned.Replace("vnd", "");
            cleaned = cleaned.Replace("đ", "");
            cleaned = cleaned.Replace("Đ", "");
            cleaned = cleaned.Replace(" ", "");
            cleaned = cleaned.Replace(",", "");
            cleaned = cleaned.Replace(".", "");

            return decimal.TryParse(cleaned, out value);
        }

        private void btn_Click(object sender, EventArgs e)
        {
            LamMoiPhieuThue();
        }

        private void LamMoiPhieuThue()
        {
            DialogResult result = MessageBox.Show(
                "Bạn có chắc chắn muốn làm mới phiếu thuê không?",
                "Xác nhận làm mới",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.No)
            {
                return;
            }

            ResetFormPhieuThue();
            MessageBox.Show("Đã làm mới phiếu thuê.");
        }

        private void LamMoiSauKhiLuu()
        {
            ResetFormPhieuThue();
        }

        private void ResetFormPhieuThue()
        {
            // 1. Xóa ô tìm kiếm
            txtTimKiemKH.Clear();
            txtTimKiemMA.Clear();

            // 2. Xóa thông tin khách hàng
            txtHoTen.Clear();
            txtSDT.Clear();
            txtDiaChi.Clear();
            txtEmail.Clear();
            txtCCCD.Clear();

            // 3. Reset ngày tháng về hiện tại
            dtpNgayLap.Value = DateTime.Now;
            dtpNgayNhan.Value = DateTime.Now;
            dtpNgayTra.Value = DateTime.Now;

            // 4. Xóa thời gian thuê
            txtGioThue.Clear();
            txtNgayThue.Clear();

            // 5. Xóa thông tin tổng phiếu thuê
            txtTongSoMay.Clear();
            txtTongTienThue.Clear();
            txtTienCoc.Clear();
            txtTaiSanCoc.Clear();

            // 6. Xóa danh sách máy đã thêm vào phiếu thuê
            dataGridView3.Rows.Clear();

            // 7. Reset checkbox phụ kiện
            cbPin.Checked = false;
            cbHatSang.Checked = false;
            cbChanMay.Checked = false;
            cbFlash.Checked = false;

            // 8. Reset số lượng phụ kiện đang có
            txtPin.Text = "0";
            txtSLHatSang.Text = "0";
            txtSLChanMay.Text = "0";
            txtSLFlash.Text = "0";

            // 9. Reset số lượng phụ kiện thuê

            // 10. Load lại dữ liệu ban đầu từ SQL
            LoadDuLieu();

            // 11. Bỏ chọn dòng trong các bảng
            dataGridView1.ClearSelection();
            dataGridView2.ClearSelection();
            dataGridView3.ClearSelection();
        }

        private void btnTrangChu_Click(object sender, EventArgs e)
        {
            FormTrangChu TC = new FormTrangChu();
            TC.Show();
            this.Close();
        }

        private void btnPhieuTra_Click(object sender, EventArgs e)
        {
            FormPhieuTra PTR = new FormPhieuTra();
            PTR.Show();
            this.Close();
        }

        private void btnIn_Click(object sender, EventArgs e)
        {
            string hinhThucThue = "";

            if (dataGridView3.Rows.Count > 0)
            {
                string tg = dataGridView3.Rows[0]
                    .Cells["ThoiGianThue"]
                    .Value?.ToString() ?? "";

                if (tg.Contains("ngày"))
                    hinhThucThue = "Thuê theo ngày";
                else if (tg.Contains("giờ"))
                    hinhThucThue = "Thuê theo giờ";
            }
            // Kiểm tra danh sách máy ảnh
            if (dataGridView3.Rows.Count <= 0 ||
                (dataGridView3.Rows.Count == 1 && dataGridView3.Rows[0].IsNewRow))
            {
                MessageBox.Show(
                    "Vui lòng chọn ít nhất một máy ảnh vào danh sách trước khi in phiếu!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            try
            {
                // Thông tin khách hàng
                string hoTen = txtHoTen.Text.Trim();
                string sdt = txtSDT.Text.Trim();
                string diaChi = txtDiaChi.Text.Trim();
                string email = txtEmail.Text.Trim();
                string cccd = txtCCCD.Text.Trim();

                // Thông tin phiếu thuê
                string ngayLap = dtpNgayLap.Value.ToString("dd/MM/yyyy");
                string ngayThue = dtpNgayNhan.Value.ToString("dd/MM/yyyy");
                string ngayTraDuKien = dtpNgayTra.Value.ToString("dd/MM/yyyy");
                string ghiChu = txtghiChu.Text.Trim();

                // Thông tin tổng hợp
                string tienCoc = txtTienCoc.Text.Trim();
                string tongSoMay = txtTongSoMay.Text.Trim();
                string tongTienThue = txtTongTienThue.Text.Trim();

                decimal tongTienThueValue = 0;
                decimal.TryParse(txtTongTienThue.Text.Replace(".", "").Replace(",", ""), out tongTienThueValue);

                decimal tienCocValue = 0;
                decimal.TryParse(txtTienCoc.Text.Replace(".", "").Replace(",", ""), out tienCocValue);

                // Tạo DataTable chi tiết
                DataTable dtChiTiet = TaoBangChiTiet();

                // Đổ dữ liệu từ DataGridView vào DataTable
                // Đổ dữ liệu từ DataGridView vào DataTable
                foreach (DataGridViewRow row in dataGridView3.Rows)
                {
                    if (row.IsNewRow) continue;

                    string maMay = row.Cells["MaMay"].Value?.ToString() ?? "";
                    string tenMay = row.Cells["TenMay"].Value?.ToString() ?? "";
                    string serial = row.Cells["GhiChu"].Value?.ToString() ?? "Sẵn Sàng";
                    string donGia = row.Cells["DonGia"].Value?.ToString() ?? "0";
                    string thoiGianThue = row.Cells["ThoiGianThue"].Value?.ToString() ?? "";
                    string soLuong = row.Cells["SoLuong"].Value?.ToString() ?? "1";
                    string thanhTien = row.Cells["ThanhTien"].Value?.ToString() ?? "0";

                    decimal donGiaValue = 0;
                    decimal thanhTienValue = 0;

                    decimal.TryParse(row.Cells["DonGia"].Value?.ToString(), out donGiaValue);
                    decimal.TryParse(row.Cells["ThanhTien"].Value?.ToString(), out thanhTienValue);

                    int soGioThue = 0;
                    int soNgayThue = 0;
                    string thoiGianThueValue = row.Cells["ThoiGianThue"].Value?.ToString() ?? "";

                    if (thoiGianThue.EndsWith(" ngày"))
                        soNgayThue = int.Parse(thoiGianThue.Replace(" ngày", "").Trim());
                    else if (thoiGianThue.EndsWith(" giờ"))
                        soGioThue = int.Parse(thoiGianThue.Replace(" giờ", "").Trim());

                    dtChiTiet.Rows.Add(
                          row.Cells["MaMay"].Value?.ToString() ?? "",
                          row.Cells["TenMay"].Value?.ToString() ?? "",
                          row.Cells["GhiChu"].Value?.ToString() ?? "Sẵn sàng",
                            donGiaValue,       // decimal
                            donGiaValue,       // decimal
                            soGioThue,         // int
                            soNgayThue,        // int
                            thoiGianThue,      // string
                            thanhTienValue,    // decimal
                            int.Parse(txtTongSoMay.Text),
                            tongTienThueValue, // decimal
                             tienCocValue       // decimal
);
                   

                    // Mở form in phiếu
                    FormInPhieu frmIn = new FormInPhieu(
                    dtChiTiet,
                    hoTen,
                    sdt,
                    diaChi,
                    email,
                    cccd,
                    ngayLap,
                    ngayThue,
                    ngayTraDuKien,
                    ghiChu,
                    tienCoc,
                    tongSoMay,
                    tongTienThue,
                    hinhThucThue);

                    frmIn.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Lỗi hệ thống khi chuẩn bị in dữ liệu:\n" + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private DataTable TaoBangChiTiet()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("maMay", typeof(string));
            dt.Columns.Add("tenMay", typeof(string));
            dt.Columns.Add("soSerial", typeof(string));
            dt.Columns.Add("giaoThueTheoGio", typeof(decimal));   // decimal
            dt.Columns.Add("giaThueTheoNgay", typeof(decimal));     // decimal
            dt.Columns.Add("soGioThue", typeof(int));             // số giờ kiểu int
            dt.Columns.Add("soNgayThue", typeof(int));            // số ngày kiểu int
            dt.Columns.Add("thoiGianThue", typeof(string));
            dt.Columns.Add("thanhTien", typeof(decimal));          // decimal
            dt.Columns.Add("tongSoMayThue", typeof(int));
            dt.Columns.Add("tongTienThue", typeof(decimal));
            dt.Columns.Add("tienCoc", typeof(decimal));

            return dt;
        }

        private void btnTrangChu_Click_1(object sender, EventArgs e)
        {
            FormTrangChu FTC = new FormTrangChu();
            FTC.Show();
            this.Close();
        }

       /* private void btnMayAnh_Click(object sender, EventArgs e)
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

       /* private void btnKhachHang_Click(object sender, EventArgs e)
        {
            KhachHang KH = new KhachHang();
            KH.Show();
            this.Close();
        }
       */
        private void btnPhieuThue_Click(object sender, EventArgs e)
        {
            FormPhieuThue PT = new FormPhieuThue();
            PT.Show();
            this.Close();
        }

        private void btnPhieuTra_Click_1(object sender, EventArgs e)
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

        private void btnDangXuat_Click_1(object sender, EventArgs e)
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
