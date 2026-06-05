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

namespace WindowsFormsApp1
{
    public partial class FormPhieuThue : Form
    {
        // Chuỗi kết nối Database
        string nguon = @"Data Source=DESKTOP-K3O3CHO;Initial Catalog=DoAnTotNghiep;Integrated Security=True";
        string lenh = "";
        SqlConnection KetNoi;
        SqlCommand THucHien;
        SqlDataReader Doc;

        // Biến lưu trữ DataTable làm nguồn cho dataGridView2 để tránh lỗi khi thao tác dòng
        DataTable dtMayAnhTrongKho = new DataTable();

        public FormPhieuThue()
        {
            InitializeComponent();
            LoadDuLieu();
        }

        private void FormPhieuThue_Load(object sender, EventArgs e)
        {
        }

        // Tải dữ liệu khách hàng và máy ảnh từ SQL
        void LoadDuLieu()
        {
            using (SqlConnection conn = new SqlConnection(nguon))
            {
                try
                {
                    conn.Open();
                    string sqlKH = @"SELECT maKhachHang, hoTen, sdt, diaChi, email, cccd, ngayTao FROM dbo.KhachHang";
                    SqlDataAdapter daKH = new SqlDataAdapter(sqlKH, conn);
                    DataTable dtKH = new DataTable();
                    daKH.Fill(dtKH);
                    dataGridView1.DataSource = dtKH;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi tải danh sách khách hàng: " + ex.Message);
                }

                try
                {
                    string sqlMA = @"SELECT maMay, tenMay, hangMay, soSerial, loaiThietBi, chuyenChup, giaThueTheoGio, giaThueTheoNgay, trangThai, moTa FROM dbo.MayAnh WHERE trangThai = N'Sẵn Sàng'";
                    SqlDataAdapter daMA = new SqlDataAdapter(sqlMA, conn);
                    dtMayAnhTrongKho = new DataTable();
                    daMA.Fill(dtMayAnhTrongKho);
                    dataGridView2.DataSource = dtMayAnhTrongKho;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi tải danh sách máy ảnh: " + ex.Message);
                }
            }
        }

        // Tìm kiếm khách hàng theo số điện thoại
        private void btnTimKiemKH_Click(object sender, EventArgs e)
        {
            string sdtTim = txtTimKiemKH.Text.Trim();
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
                    string sql = @"SELECT maKhachHang, hoTen, sdt, diaChi, email, cccd, ngayTao FROM KhachHang WHERE sdt = @sdt";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@sdt", sdtTim);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        dataGridView1.DataSource = dt;
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
                    MessageBox.Show("Lỗi tìm kiếm khách hàng: " + ex.Message);
                }
            }
        }

        // Tìm kiếm máy ảnh theo tên máy
        private void btnTimKiemMA_Click_1(object sender, EventArgs e)
        {
            string tenMayTim = txtTimKiemMA.Text.Trim();
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
                    string sql = @"SELECT maMay, tenMay, hangMay, soSerial, loaiThietBi, chuyenChup, giaThueTheoGio, giaThueTheoNgay, trangThai, moTa FROM MayAnh WHERE tenMay LIKE @tenMay AND trangThai = N'Sẵn Sàng'";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@tenMay", "%" + tenMayTim + "%");
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    dtMayAnhTrongKho = new DataTable();
                    da.Fill(dtMayAnhTrongKho);
                    dataGridView2.DataSource = dtMayAnhTrongKho;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi tìm kiếm máy ảnh: " + ex.Message);
                }
            }
        }

        // Thêm máy ảnh đang chọn vào chi tiết phiếu thuê (dataGridView3)
        private void btnThemMA_Click_1(object sender, EventArgs e)
        {
            if (dataGridView2.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn máy ảnh từ danh sách trống.");
                return;
            }

            DataGridViewRow row = dataGridView2.CurrentRow;
            int maMay = Convert.ToInt32(row.Cells["maMay"].Value);
            string tenMay = row.Cells["tenMay"].Value.ToString();
            string soSerial = row.Cells["soSerial"].Value?.ToString() ?? "";
            decimal giaGio = Convert.ToDecimal(row.Cells["giaThueTheoGio"].Value);
            decimal giaNgay = Convert.ToDecimal(row.Cells["giaThueTheoNgay"].Value);
            string trangThai = row.Cells["trangThai"].Value.ToString();

            if (trangThai != "Sẵn Sàng")
            {
                MessageBox.Show("Máy hiện tại không sẵn sàng để thuê. Trạng thái: " + trangThai);
                return;
            }

            // Kiểm tra xem máy đã tồn tại trong phiếu chưa
            foreach (DataGridViewRow r in dataGridView3.Rows)
            {
                if (!r.IsNewRow && r.Cells["MaMay"].Value != null)
                {
                    if (Convert.ToInt32(r.Cells["MaMay"].Value) == maMay)
                    {
                        MessageBox.Show("Máy này đã được thêm vào danh sách phiếu thuê.");
                        return;
                    }
                }
            }

            bool coGio = !string.IsNullOrEmpty(txtGioThue.Text);
            bool coNgay = !string.IsNullOrEmpty(txtNgayThue.Text);

            if (coGio && coNgay)
            {
                MessageBox.Show("Chỉ được nhập số giờ hoặc số ngày thuê.");
                return;
            }
            if (!coGio && !coNgay)
            {
                MessageBox.Show("Vui lòng điền số lượng thời gian thuê mong muốn.");
                return;
            }

            DateTime ngayNhan = dtpNgayNhan.Value;
            DateTime ngayTra = dtpNgayTra.Value;

            if (ngayTra < ngayNhan)
            {
                MessageBox.Show("Ngày hẹn trả máy không được nhỏ hơn ngày nhận máy.");
                return;
            }

            int soNgayToiDa = (ngayTra.Date - ngayNhan.Date).Days;
            if (soNgayToiDa == 0) soNgayToiDa = 1;

            int soGioToiDa = (int)(ngayTra - ngayNhan).TotalHours;
            if (soGioToiDa <= 0) soGioToiDa = 24;

            decimal thanhTien = 0;
            decimal donGia = 0;
            string thoiGianThue = "";
            int soLuong = 1;

            if (coGio)
            {
                if (!int.TryParse(txtGioThue.Text, out int soGio) || soGio <= 0)
                {
                    MessageBox.Show("Số giờ thuê nhập vào không hợp lệ.");
                    return;
                }
                if (soGio > soGioToiDa)
                {
                    MessageBox.Show("Số giờ vượt quá giới hạn khung ngày: Tối đa " + soGioToiDa + " giờ.");
                    return;
                }
                donGia = giaGio;
                thanhTien = giaGio * soGio;
                thoiGianThue = soGio + " giờ";
            }

            if (coNgay)
            {
                if (!int.TryParse(txtNgayThue.Text, out int soNgay) || soNgay <= 0)
                {
                    MessageBox.Show("Số ngày thuê nhập vào không hợp lệ.");
                    return;
                }
                if (soNgay > soNgayToiDa)
                {
                    MessageBox.Show("Số ngày vượt quá giới hạn khung ngày: Tối đa " + soNgayToiDa + " ngày.");
                    return;
                }
                donGia = giaNgay;
                thanhTien = giaNgay * soNgay;
                thoiGianThue = soNgay + " ngày";
            }

            // Thêm thông tin vào Grid3 (Chi tiết phiếu)
            int index = dataGridView3.Rows.Add();
            dataGridView3.Rows[index].Cells["MaMay"].Value = maMay;
            dataGridView3.Rows[index].Cells["TenMay"].Value = tenMay;
            dataGridView3.Rows[index].Cells["ThoiGianThue"].Value = thoiGianThue;
            dataGridView3.Rows[index].Cells["SoLuong"].Value = soLuong;
            dataGridView3.Rows[index].Cells["DonGia"].Value = donGia;
            dataGridView3.Rows[index].Cells["ThanhTien"].Value = thanhTien;
            dataGridView3.Rows[index].Cells["GhiChu"].Value = soSerial; // Tạm dùng cột ghi chú lưu Serial để form In lấy ra

            // Xóa dòng an toàn khỏi DataTable liên kết với dataGridView2
            if (dataGridView2.DataSource is DataTable dt)
            {
                int currencyIndex = dataGridView2.CurrentRow.Index;
                dt.Rows[currencyIndex].Delete();
                dt.AcceptChanges();
            }

            txtGioThue.Clear();
            txtNgayThue.Clear();
            CapNhatTongTien();
            CapNhatTongSoMay();
            MessageBox.Show("Đã thêm máy ảnh vào phiếu thành công.");
        }

        // Xóa máy ảnh khỏi phiếu thuê và hoàn lại danh sách trống
        private void btnXoaMA_Click_1(object sender, EventArgs e)
        {
            if (dataGridView3.CurrentRow == null || dataGridView3.CurrentRow.IsNewRow)
            {
                MessageBox.Show("Vui lòng chọn dòng máy ảnh hợp lệ muốn xóa khỏi phiếu.");
                return;
            }

            int maMay = Convert.ToInt32(dataGridView3.CurrentRow.Cells["MaMay"].Value);
            dataGridView3.Rows.Remove(dataGridView3.CurrentRow);

            // Kiểm tra xem máy đã có trong Grid kho chưa, nếu chưa thì tải lại kho
            bool mayDaCo = false;
            foreach (DataGridViewRow row in dataGridView2.Rows)
            {
                if (!row.IsNewRow && row.Cells["maMay"].Value != null && Convert.ToInt32(row.Cells["maMay"].Value) == maMay)
                {
                    mayDaCo = true;
                    break;
                }
            }

            if (!mayDaCo)
            {
                using (SqlConnection conn = new SqlConnection(nguon))
                {
                    try
                    {
                        conn.Open();
                        string sql = "SELECT maMay, tenMay, hangMay, soSerial, loaiThietBi, chuyenChup, giaThueTheoGio, giaThueTheoNgay, trangThai, moTa FROM MayAnh WHERE maMay=@maMay";
                        SqlCommand cmd = new SqlCommand(sql, conn);
                        cmd.Parameters.AddWithValue("@maMay", maMay);
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dtTmp = new DataTable();
                        da.Fill(dtTmp);

                        if (dtTmp.Rows.Count > 0 && dataGridView2.DataSource is DataTable dtSource)
                        {
                            DataRow newRow = dtSource.NewRow();
                            newRow.ItemArray = dtTmp.Rows[0].ItemArray;
                            dtSource.Rows.Add(newRow);
                            dtSource.AcceptChanges();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi khi hoàn trả máy về danh sách: " + ex.Message);
                    }
                }
            }

            CapNhatTongTien();
            CapNhatTongSoMay();
        }

        void CapNhatTongTien()
        {
            decimal tong = 0;

            foreach (DataGridViewRow row in dataGridView3.Rows)
            {
                if (!row.IsNewRow && row.Cells["ThanhTien"].Value != null)
                {
                    tong += Convert.ToDecimal(row.Cells["ThanhTien"].Value);
                }
            }

            // Tổng tiền thuê
            txtTongTienThue.Text = tong.ToString("N0");

            // Tiền cọc = 50%
            decimal tienCoc = tong / 2;
            txtTienCoc.Text = tienCoc.ToString("N0");
        }

        void CapNhatTongSoMay()
        {
            int tongMay = 0;
            foreach (DataGridViewRow row in dataGridView3.Rows)
            {
                if (!row.IsNewRow && row.Cells["SoLuong"].Value != null)
                {
                    if (int.TryParse(row.Cells["SoLuong"].Value.ToString(), out int soLuong))
                    {
                        tongMay += soLuong;
                    }
                }
            }
            txtTongSoMay.Text = tongMay.ToString();
        }

        // Tải thông tin phụ kiện đi kèm khi nhấn chọn máy ở dataGridView2
        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView2.CurrentRow == null) return;
            int maMay = Convert.ToInt32(dataGridView2.CurrentRow.Cells["maMay"].Value);

            cbPin.Checked = false; cbHatSang.Checked = false; cbChanMay.Checked = false; cbFlash.Checked = false;
            txtPin.Text = "0"; txtSLHatSang.Text = "0"; txtSLChanMay.Text = "0"; txtSLFlash.Text = "0";

            using (SqlConnection conn = new SqlConnection(nguon))
            {
                try
                {
                    conn.Open();
                    string sql = @"SELECT maPhuKien, soLuong FROM PhuKienMay WHERE maMay=@maMay";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@maMay", maMay);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        int maPhuKien = Convert.ToInt32(reader["maPhuKien"]);
                        int soLuong = Convert.ToInt32(reader["soLuong"]);

                        switch (maPhuKien)
                        {
                            case 1: cbPin.Checked = true; txtPin.Text = soLuong.ToString(); break;
                            case 2: cbHatSang.Checked = true; txtSLHatSang.Text = soLuong.ToString(); break;
                            case 3: cbChanMay.Checked = true; txtSLChanMay.Text = soLuong.ToString(); break;
                            case 4: cbFlash.Checked = true; txtSLFlash.Text = soLuong.ToString(); break;
                        }
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi lấy phụ kiện đi kèm: " + ex.Message);
                }
            }
        }

        // Lưu thông tin phiếu thuê xuống Cơ sở dữ liệu sử dụng Transaction
        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtHoTen.Text)) { MessageBox.Show("Vui lòng điền thông tin tên khách hàng."); return; }
            if (string.IsNullOrWhiteSpace(txtSDT.Text)) { MessageBox.Show("Vui lòng bổ sung số điện thoại khách hàng."); return; }

            bool coMayTrongPhieu = false;
            foreach (DataGridViewRow r in dataGridView3.Rows)
            {
                if (!r.IsNewRow) { coMayTrongPhieu = true; break; }
            }
            if (!coMayTrongPhieu)
            {
                MessageBox.Show("Vui lòng bổ sung ít nhất một thiết bị máy ảnh vào danh sách chọn.");
                return;
            }

            if (!int.TryParse(txtTongSoMay.Text.Trim(), out int tongSoMay)) { MessageBox.Show("Tổng số lượng máy lỗi."); return; }
            if (!TryParseMoney(txtTongTienThue.Text, out decimal tongTienThue)) { MessageBox.Show("Tổng tiền tính toán lỗi."); return; }

            decimal tienCoc = 0;
            if (!string.IsNullOrWhiteSpace(txtTienCoc.Text) && !TryParseMoney(txtTienCoc.Text, out tienCoc))
            {
                MessageBox.Show("Số tiền đặt cọc nhập vào không hợp lệ.");
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

                    SqlCommand checkKH = new SqlCommand(@"SELECT maKhachHang FROM KhachHang WHERE sdt = @sdt", conn, transaction);
                    checkKH.Parameters.AddWithValue("@sdt", sdt);
                    object result = checkKH.ExecuteScalar();

                    if (result != null && result != DBNull.Value)
                    {
                        maKhachHang = Convert.ToInt32(result);
                    }
                    else
                    {
                        bool maKhachHangTuTangTrongSQL = IsIdentityColumn(conn, transaction, "KhachHang", "maKhachHang");
                        if (maKhachHangTuTangTrongSQL)
                        {
                            SqlCommand insertKH = new SqlCommand(
                                @"INSERT INTO KhachHang (hoTen, sdt, diaChi, email, cccd, ngayTao) VALUES (@hoTen, @sdt, @diaChi, @email, @cccd, @ngayTao); SELECT CAST(SCOPE_IDENTITY() AS INT);", conn, transaction);
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
                                @"INSERT INTO KhachHang (maKhachHang, hoTen, sdt, diaChi, email, cccd, ngayTao) VALUES (@maKhachHang, @hoTen, @sdt, @diaChi, @email, @cccd, @ngayTao);", conn, transaction);
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

                    int maPhieuThue;
                    bool maPhieuThueTuTangTrongSQL = IsIdentityColumn(conn, transaction, "PhieuThue", "maPhieuThue");
                    if (maPhieuThueTuTangTrongSQL)
                    {
                        SqlCommand insertPT = new SqlCommand(
                            @"INSERT INTO PhieuThue (maKhachHang, ngayLap, ngayNhanMay, ngayDuKienTra, tongSoMayThue, tongTienThue, tienCoc, taiSanCoc, ghiChu, trangThai) VALUES (@maKhachHang, @ngayLap, @ngayNhanMay, @ngayDuKienTra, @tongSoMayThue, @tongTienThue, @tienCoc, @taiSanCoc, @ghiChu, @trangThai); SELECT CAST(SCOPE_IDENTITY() AS INT);", conn, transaction);
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
                            @"INSERT INTO PhieuThue (maPhieuThue, maKhachHang, ngayLap, ngayNhanMay, ngayDuKienTra, tongSoMayThue, tongTienThue, tienCoc, taiSanCoc, ghiChu, trangThai) VALUES (@maPhieuThue, @maKhachHang, @ngayLap, @ngayNhanMay, @ngayDuKienTra, @tongSoMayThue, @tongTienThue, @tienCoc, @taiSanCoc, @ghiChu, @trangThai);", conn, transaction);
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

                    string tenCotMaChiTiet = "";
                    if (ColumnExists(conn, transaction, "ChiTietPhieuThue", "maChiTiet")) tenCotMaChiTiet = "maChiTiet";
                    else if (ColumnExists(conn, transaction, "ChiTietPhieuThue", "maChiTietPhieuThue")) tenCotMaChiTiet = "maChiTietPhieuThue";

                    bool coCotMaChiTiet = tenCotMaChiTiet != "";
                    bool maChiTietTuTangTrongSQL = coCotMaChiTiet && IsIdentityColumn(conn, transaction, "ChiTietPhieuThue", tenCotMaChiTiet);

                    foreach (DataGridViewRow row in dataGridView3.Rows)
                    {
                        if (row.IsNewRow) continue;

                        int maMay = Convert.ToInt32(row.Cells["MaMay"].Value);
                        string thoiGianThue = row.Cells["ThoiGianThue"].Value.ToString();
                        int soLuongThue = Convert.ToInt32(row.Cells["SoLuong"].Value);
                        decimal donGia = Convert.ToDecimal(row.Cells["DonGia"].Value);
                        decimal thanhTien = Convert.ToDecimal(row.Cells["ThanhTien"].Value);
                        string ghiChu = row.Cells["GhiChu"].Value?.ToString() ?? "";

                        SqlCommand insertCT;
                        if (coCotMaChiTiet && !maChiTietTuTangTrongSQL)
                        {
                            int maChiTiet = GetNextId(conn, transaction, "ChiTietPhieuThue", tenCotMaChiTiet);
                            insertCT = new SqlCommand(
                                @"INSERT INTO ChiTietPhieuThue ([" + tenCotMaChiTiet + @"], maPhieuThue, maMay, thoiGianThue, soLuongThue, donGia, thanhTien, ghiChu) VALUES (@maChiTiet, @maPhieuThue, @maMay, @thoiGianThue, @soLuongThue, @donGia, @thanhTien, @ghiChu)", conn, transaction);
                            insertCT.Parameters.AddWithValue("@maChiTiet", maChiTiet);
                        }
                        else
                        {
                            insertCT = new SqlCommand(
                                @"INSERT INTO ChiTietPhieuThue (maPhieuThue, maMay, thoiGianThue, soLuongThue, donGia, thanhTien, ghiChu) VALUES (@maPhieuThue, @maMay, @thoiGianThue, @soLuongThue, @donGia, @thanhTien, @ghiChu)", conn, transaction);
                        }

                        insertCT.Parameters.AddWithValue("@maPhieuThue", maPhieuThue);
                        insertCT.Parameters.AddWithValue("@maMay", maMay);
                        insertCT.Parameters.AddWithValue("@thoiGianThue", thoiGianThue);
                        insertCT.Parameters.AddWithValue("@soLuongThue", soLuongThue);
                        insertCT.Parameters.AddWithValue("@donGia", donGia);
                        insertCT.Parameters.AddWithValue("@thanhTien", thanhTien);
                        insertCT.Parameters.AddWithValue("@ghiChu", ghiChu);
                        insertCT.ExecuteNonQuery();

                        SqlCommand updateMay = new SqlCommand(@"UPDATE MayAnh SET trangThai = @trangThai WHERE maMay = @maMay", conn, transaction);
                        updateMay.Parameters.AddWithValue("@trangThai", "Đang thuê");
                        updateMay.Parameters.AddWithValue("@maMay", maMay);
                        updateMay.ExecuteNonQuery();
                    }

                    transaction.Commit();
                    MessageBox.Show("Lưu toàn bộ hóa đơn phiếu thuê thành công!");
                    LamMoiSauKhiLuu();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    MessageBox.Show("Gặp lỗi hệ thống dữ liệu. Giao dịch Rollback: " + ex.Message);
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
            SqlCommand cmd = new SqlCommand(@"SELECT COLUMNPROPERTY(OBJECT_ID(@tableName), @columnName, 'IsIdentity')", conn, transaction);
            cmd.Parameters.AddWithValue("@tableName", "dbo." + tableName);
            cmd.Parameters.AddWithValue("@columnName", columnName);
            object result = cmd.ExecuteScalar();
            if (result == null || result == DBNull.Value) return false;
            return Convert.ToInt32(result) == 1;
        }

        private bool ColumnExists(SqlConnection conn, SqlTransaction transaction, string tableName, string columnName)
        {
            SqlCommand cmd = new SqlCommand(
                @"SELECT COUNT(*) FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = @tableName AND COLUMN_NAME = @columnName", conn, transaction);
            cmd.Parameters.AddWithValue("@tableName", tableName);
            cmd.Parameters.AddWithValue("@columnName", columnName);
            return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
        }

        private bool TryParseMoney(string text, out decimal value)
        {
            value = 0;
            if (string.IsNullOrWhiteSpace(text)) return false;
            string cleaned = text.Trim().Replace(" ", "").Replace(",", "").Replace(".", "");
            return decimal.TryParse(cleaned, out value);
        }

        private void btn_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn làm mới phiếu thuê?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                ResetFormPhieuThue();
            }
        }

        void LamMoiSauKhiLuu()
        {
            ResetFormPhieuThue();
        }

        void ResetFormPhieuThue()
        {
            txtTimKiemKH.Clear(); txtTimKiemMA.Clear();
            txtHoTen.Clear(); txtSDT.Clear(); txtDiaChi.Clear(); txtEmail.Clear(); txtCCCD.Clear();
            dtpNgayLap.Value = DateTime.Now; dtpNgayNhan.Value = DateTime.Now; dtpNgayTra.Value = DateTime.Now;
            txtGioThue.Clear(); txtNgayThue.Clear();
            txtTongSoMay.Clear(); txtTongTienThue.Clear(); txtTienCoc.Clear(); txtTaiSanCoc.Clear();
            dataGridView3.Rows.Clear();
            cbPin.Checked = false; cbHatSang.Checked = false; cbChanMay.Checked = false; cbFlash.Checked = false;
            txtPin.Text = "0"; txtSLHatSang.Text = "0"; txtSLChanMay.Text = "0"; txtSLFlash.Text = "0";
            LoadDuLieu();
        }

        private void btnDangXuat_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn đăng xuất không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                DangNhap frm = new DangNhap();
                frm.Show();
                this.Hide();
            }
        }

        private void btnTrangChu_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnPhuKien_Click(object sender, EventArgs e) { }
        private void button1_Click(object sender, EventArgs e) { }
        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e) { }

        // Gọi form In ấn và nạp chính xác dữ liệu từ GridView theo tên Cột
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

                    // ← CHỈ THÊM 6 DÒNG NÀY (không thêm foreach mới)
                    string soGioThue = "";
                    string soNgayThue = "";
                    if (thoiGianThue.EndsWith(" ngày"))
                        soNgayThue = thoiGianThue.Replace(" ngày", "").Trim();
                    else if (thoiGianThue.EndsWith(" giờ"))
                        soGioThue = thoiGianThue.Replace(" giờ", "").Trim();

                    // ← CẬP NHẬT Rows.Add (thêm soGioThue, soNgayThue vào giữa)
                    dtChiTiet.Rows.Add(
                        maMay,
                        tenMay,
                        serial,
                        donGia,         // giaThueTheoGio
                        donGia,         // giaThueTheoNgay
                        soGioThue,      // ← THÊM
                        soNgayThue,     // ← THÊM — sẽ = "2" khi thuê 2 ngày
                        thoiGianThue,
                        thanhTien,
                        tongSoMay,
                        tongTienThue,
                        tienCoc
                    );
                }

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
            dt.Columns.Add("giaoThueTheoGio", typeof(string));
            dt.Columns.Add("giaThueTheoNgay", typeof(string));
            dt.Columns.Add("soGioThue", typeof(string)); // ← THÊM MỚI
            dt.Columns.Add("soNgayThue", typeof(string)); // ← THÊM MỚI
            dt.Columns.Add("thoiGianThue", typeof(string));
            dt.Columns.Add("thanhTien", typeof(string));
            dt.Columns.Add("tongSoMayThue", typeof(string));
            dt.Columns.Add("tongTienThue", typeof(string));
            dt.Columns.Add("tienCoc", typeof(string));

            return dt;
        }
    }
}