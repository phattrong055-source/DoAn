using System;
using System.Data;
using System.Globalization;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace WindowsFormsApp1
{
    public partial class MayAnh : Form
    {
        string nguon = @"Data Source=DESKTOP-S4KUUMQ\SQLEXPRESS;Initial Catalog=DoAnTotNghiep;Integrated Security=True";

        private SqlConnection conn = null;
        private bool isThem = true;
        private int? maMayDangChon = null;
        private CultureInfo cultureVN = new CultureInfo("vi-VN");

        public MayAnh()
        {
            InitializeComponent();

            btnThem.Click += btnThem_Click;
            btnSua.Click += btnSua_Click;
            btnXoa.Click += btnXoa_Click;
            btnLuu.Click += btnLuu_Click;
            btnHuy.Click += btnHuy_Click;
            btnLamMoi.Click += btnLamMoi_Click;

            dataGridView1.CellClick += dataGridView1_CellClick;
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;

            txtGiaThueTheoGio.TextChanged += txtGiaThueTheoGio_TextChanged;
            txtGiaThueTheoNgay.TextChanged += txtGiaThueTheoNgay_TextChanged;
        }

        private void MayAnh_Load(object sender, EventArgs e)
        {
            conn = new SqlConnection(nguon);

            LoadHangMayToComboBox();
            LoadData();
            SetInterfaceState(true);
        }

        private void btnMayAnh_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void FormatCurrencyTextBox(TextBox textBox)
        {
            if (string.IsNullOrEmpty(textBox.Text)) return;

            try
            {
                string cleanString = textBox.Text.Replace(".", "");

                if (long.TryParse(cleanString, out long value))
                {
                    textBox.Text = string.Format(cultureVN, "{0:N0}", value);
                    textBox.SelectionStart = textBox.Text.Length;
                }
            }
            catch
            {
            }
        }

        private void txtGiaThueTheoGio_TextChanged(object sender, EventArgs e)
        {
            txtGiaThueTheoGio.TextChanged -= txtGiaThueTheoGio_TextChanged;
            FormatCurrencyTextBox(txtGiaThueTheoGio);
            txtGiaThueTheoGio.TextChanged += txtGiaThueTheoGio_TextChanged;
        }

        private void txtGiaThueTheoNgay_TextChanged(object sender, EventArgs e)
        {
            txtGiaThueTheoNgay.TextChanged -= txtGiaThueTheoNgay_TextChanged;
            FormatCurrencyTextBox(txtGiaThueTheoNgay);
            txtGiaThueTheoNgay.TextChanged += txtGiaThueTheoNgay_TextChanged;
        }

        private int LayMaMayMoi(SqlConnection conn, SqlTransaction tran)
        {
            string sql = "SELECT ISNULL(MAX(maMay), 0) + 1 FROM MayAnh";

            using (SqlCommand cmd = new SqlCommand(sql, conn, tran))
            {
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        private void LoadData()
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();

                string query = @"
                    SELECT 
                        maMay AS [Mã Máy], 
                        tenMay AS [Tên Máy], 
                        hangMay AS [Hãng], 
                        chuyenChup AS [Chuyên Chụp],
                        loaiThietBi AS [Loại Máy], 
                        soSerial AS [Số Seri], 
                        giaThueTheoGio AS [Giá Thuê/Giờ], 
                        giaThueTheoNgay AS [Giá Thuê/Ngày], 
                        moTa AS [Tình Trạng], 
                        trangThai AS [Trạng Thái] 
                    FROM MayAnh";

                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;

                if (dataGridView1.Columns["Giá Thuê/Giờ"] != null)
                    dataGridView1.Columns["Giá Thuê/Giờ"].DefaultCellStyle.Format = "N0";

                if (dataGridView1.Columns["Giá Thuê/Ngày"] != null)
                    dataGridView1.Columns["Giá Thuê/Ngày"].DefaultCellStyle.Format = "N0";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh sách máy ảnh: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
        }

        private void SetInterfaceState(bool isLocked)
        {
            txtTenMay.Enabled = !isLocked;
            txtHang.Enabled = !isLocked;
            txtChuyenChup.Enabled = !isLocked;
            txtLoaiMay.Enabled = !isLocked;
            txtSoSeri.Enabled = !isLocked;
            txtGiaThueTheoGio.Enabled = !isLocked;
            txtGiaThueTheoNgay.Enabled = !isLocked;
            txtTinhTrang.Enabled = !isLocked;
            txtTrangThai.Enabled = !isLocked;

            chkPin.Enabled = !isLocked;
            chkHatSang.Enabled = !isLocked;
            chkChanQuay.Enabled = !isLocked;
            chkFlash.Enabled = !isLocked;

            txtPin.Enabled = !isLocked;
            txtHatSang.Enabled = !isLocked;
            txtChanQuay.Enabled = !isLocked;
            txtFlash.Enabled = !isLocked;

            btnThem.Enabled = isLocked;
            btnSua.Enabled = isLocked;
            btnXoa.Enabled = isLocked;
            btnLuu.Enabled = !isLocked;
            btnHuy.Enabled = !isLocked;
        }

        private void ClearInputFields()
        {
            maMayDangChon = null;

            txtTenMay.Clear();
            txtHang.Clear();
            txtChuyenChup.Clear();
            txtLoaiMay.Clear();
            txtSoSeri.Clear();
            txtGiaThueTheoGio.Clear();
            txtGiaThueTheoNgay.Clear();
            txtTinhTrang.Clear();
            txtTrangThai.Clear();

            chkPin.Checked = false;
            chkHatSang.Checked = false;
            chkChanQuay.Checked = false;
            chkFlash.Checked = false;

            txtPin.Clear();
            txtHatSang.Clear();
            txtChanQuay.Clear();
            txtFlash.Clear();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            isThem = true;
            ClearInputFields();
            SetInterfaceState(false);
            txtTenMay.Focus();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (maMayDangChon == null)
            {
                MessageBox.Show("Vui lòng chọn máy ảnh cần sửa từ danh sách bên dưới!", "Nhắc nhở", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            isThem = false;
            SetInterfaceState(false);
            txtTenMay.Focus();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (maMayDangChon == null)
            {
                MessageBox.Show("Vui lòng chọn máy ảnh muốn xóa bỏ!", "Nhắc nhở", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult confirm = MessageBox.Show("Bạn có chắc chắn muốn xóa máy ảnh này không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirm == DialogResult.Yes)
            {
                SqlTransaction tran = null;

                try
                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();

                    tran = conn.BeginTransaction();

                    int maMay = maMayDangChon.Value;

                    string sqlXoaPhuKien = "DELETE FROM PhuKienMay WHERE maMay = @maMay";

                    using (SqlCommand cmdXoaPK = new SqlCommand(sqlXoaPhuKien, conn, tran))
                    {
                        cmdXoaPK.Parameters.AddWithValue("@maMay", maMay);
                        cmdXoaPK.ExecuteNonQuery();
                    }

                    string sqlXoaMay = "DELETE FROM MayAnh WHERE maMay = @maMay";

                    using (SqlCommand cmdXoaMay = new SqlCommand(sqlXoaMay, conn, tran))
                    {
                        cmdXoaMay.Parameters.AddWithValue("@maMay", maMay);
                        cmdXoaMay.ExecuteNonQuery();
                    }

                    tran.Commit();

                    MessageBox.Show("Xóa thông tin máy ảnh thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearInputFields();
                    LoadData();
                }
                catch (Exception ex)
                {
                    if (tran != null)
                        tran.Rollback();

                    MessageBox.Show("Không thể xóa máy ảnh này: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    if (conn.State == ConnectionState.Open)
                        conn.Close();
                }
            }
        }

        private int LaySoLuong(TextBox txt)
        {
            if (string.IsNullOrWhiteSpace(txt.Text))
                return 1;

            if (!int.TryParse(txt.Text, out int soLuong))
                return 1;

            if (soLuong <= 0)
                return 1;

            return soLuong;
        }

        private int LayMaChiTietMoi(SqlConnection conn, SqlTransaction tran)
        {
            string sql = "SELECT ISNULL(MAX(maChiTiet), 0) + 1 FROM PhuKienMay";

            using (SqlCommand cmd = new SqlCommand(sql, conn, tran))
            {
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        private void ThemPhuKienTheoMay(SqlConnection conn, SqlTransaction tran, int maMay, int maPhuKien, int soLuong)
        {
            int maChiTiet = LayMaChiTietMoi(conn, tran);

            string sql = @"
                INSERT INTO PhuKienMay 
                    (maChiTiet, maMay, maPhuKien, soLuong)
                VALUES 
                    (@maChiTiet, @maMay, @maPhuKien, @soLuong)";

            using (SqlCommand cmd = new SqlCommand(sql, conn, tran))
            {
                cmd.Parameters.AddWithValue("@maChiTiet", maChiTiet);
                cmd.Parameters.AddWithValue("@maMay", maMay);
                cmd.Parameters.AddWithValue("@maPhuKien", maPhuKien);
                cmd.Parameters.AddWithValue("@soLuong", soLuong);

                cmd.ExecuteNonQuery();
            }
        }

        private void LoadPhuKienTheoMay(int maMay)
        {
            chkPin.Checked = false;
            chkHatSang.Checked = false;
            chkChanQuay.Checked = false;
            chkFlash.Checked = false;

            txtPin.Clear();
            txtHatSang.Clear();
            txtChanQuay.Clear();
            txtFlash.Clear();

            try
            {
                using (SqlConnection localConn = new SqlConnection(nguon))
                {
                    localConn.Open();

                    string sql = "SELECT maPhuKien, soLuong FROM PhuKienMay WHERE maMay = @maMay";

                    using (SqlCommand cmd = new SqlCommand(sql, localConn))
                    {
                        cmd.Parameters.AddWithValue("@maMay", maMay);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                int maPhuKien = Convert.ToInt32(dr["maPhuKien"]);
                                string soLuong = dr["soLuong"].ToString();

                                if (maPhuKien == 1)
                                {
                                    chkPin.Checked = true;
                                    txtPin.Text = soLuong;
                                }
                                else if (maPhuKien == 2)
                                {
                                    chkHatSang.Checked = true;
                                    txtHatSang.Text = soLuong;
                                }
                                else if (maPhuKien == 3)
                                {
                                    chkChanQuay.Checked = true;
                                    txtChanQuay.Text = soLuong;
                                }
                                else if (maPhuKien == 4)
                                {
                                    chkFlash.Checked = true;
                                    txtFlash.Text = soLuong;
                                }
                            }
                        }
                    }
                }
            }
            catch
            {
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtTenMay.Text) || string.IsNullOrEmpty(txtSoSeri.Text))
            {
                MessageBox.Show("Tên máy ảnh và Số Seri không được bỏ trống!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string cleanGiaGio = txtGiaThueTheoGio.Text.Replace(".", "");
            string cleanGiaNgay = txtGiaThueTheoNgay.Text.Replace(".", "");

            if (!decimal.TryParse(cleanGiaGio, out decimal giaGio) || !decimal.TryParse(cleanGiaNgay, out decimal giaNgay))
            {
                MessageBox.Show("Giá thuê giờ hoặc ngày không hợp lệ!", "Lỗi số", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SqlTransaction tran = null;

            try
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();

                tran = conn.BeginTransaction();

                int maMay;

                if (isThem)
                {
                    maMay = LayMaMayMoi(conn, tran);
                }
                else
                {
                    if (maMayDangChon == null)
                    {
                        MessageBox.Show("Vui lòng chọn máy ảnh cần sửa!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        tran.Rollback();
                        return;
                    }

                    maMay = maMayDangChon.Value;
                }

                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.Transaction = tran;

                    if (isThem)
                    {
                        cmd.CommandText = @"
                            INSERT INTO MayAnh 
                                (maMay, tenMay, hangMay, chuyenChup, loaiThietBi, soSerial, giaThueTheoGio, giaThueTheoNgay, moTa, trangThai) 
                            VALUES 
                                (@maMay, @tenMay, @hangMay, @chuyenChup, @loaiThietBi, @soSerial, @giaThueTheoGio, @giaThueTheoNgay, @moTa, @trangThai)";
                    }
                    else
                    {
                        cmd.CommandText = @"
                            UPDATE MayAnh 
                            SET 
                                tenMay = @tenMay, 
                                hangMay = @hangMay,
                                chuyenChup = @chuyenChup,
                                loaiThietBi = @loaiThietBi, 
                                soSerial = @soSerial,
                                giaThueTheoGio = @giaThueTheoGio, 
                                giaThueTheoNgay = @giaThueTheoNgay, 
                                moTa = @moTa, 
                                trangThai = @trangThai 
                            WHERE maMay = @maMay";
                    }

                    cmd.Parameters.AddWithValue("@maMay", maMay);
                    cmd.Parameters.AddWithValue("@tenMay", txtTenMay.Text);
                    cmd.Parameters.AddWithValue("@hangMay", string.IsNullOrEmpty(txtHang.Text) ? (object)DBNull.Value : txtHang.Text);
                    cmd.Parameters.AddWithValue("@chuyenChup", string.IsNullOrEmpty(txtChuyenChup.Text) ? (object)DBNull.Value : txtChuyenChup.Text);
                    cmd.Parameters.AddWithValue("@loaiThietBi", string.IsNullOrEmpty(txtLoaiMay.Text) ? (object)DBNull.Value : txtLoaiMay.Text);
                    cmd.Parameters.AddWithValue("@soSerial", txtSoSeri.Text);
                    cmd.Parameters.AddWithValue("@giaThueTheoGio", giaGio);
                    cmd.Parameters.AddWithValue("@giaThueTheoNgay", giaNgay);
                    cmd.Parameters.AddWithValue("@moTa", string.IsNullOrEmpty(txtTinhTrang.Text) ? (object)DBNull.Value : txtTinhTrang.Text);
                    cmd.Parameters.AddWithValue("@trangThai", string.IsNullOrEmpty(txtTrangThai.Text) ? (object)DBNull.Value : txtTrangThai.Text);

                    cmd.ExecuteNonQuery();
                }

                string sqlXoaPhuKienCu = "DELETE FROM PhuKienMay WHERE maMay = @maMay";

                using (SqlCommand cmdXoa = new SqlCommand(sqlXoaPhuKienCu, conn, tran))
                {
                    cmdXoa.Parameters.AddWithValue("@maMay", maMay);
                    cmdXoa.ExecuteNonQuery();
                }

                if (chkPin.Checked)
                {
                    ThemPhuKienTheoMay(conn, tran, maMay, 1, LaySoLuong(txtPin));
                }

                if (chkHatSang.Checked)
                {
                    ThemPhuKienTheoMay(conn, tran, maMay, 2, LaySoLuong(txtHatSang));
                }

                if (chkChanQuay.Checked)
                {
                    ThemPhuKienTheoMay(conn, tran, maMay, 3, LaySoLuong(txtChanQuay));
                }

                if (chkFlash.Checked)
                {
                    ThemPhuKienTheoMay(conn, tran, maMay, 4, LaySoLuong(txtFlash));
                }

                tran.Commit();

                maMayDangChon = maMay;

                MessageBox.Show(
                    isThem ? "Thêm máy ảnh và phụ kiện thành công!" : "Cập nhật máy ảnh và phụ kiện thành công!",
                    "Thành công",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                SetInterfaceState(true);
                LoadData();
            }
            catch (Exception ex)
            {
                if (tran != null)
                    tran.Rollback();

                MessageBox.Show("Lỗi lưu dữ liệu: " + ex.Message, "Lỗi kết nối", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            ClearInputFields();
            SetInterfaceState(true);
            LoadData();
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            ClearInputFields();

            if (comboBox1.Items.Count > 0)
                comboBox1.SelectedIndex = 0;

            LoadData();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                if (row.Cells["Mã Máy"].Value != null && int.TryParse(row.Cells["Mã Máy"].Value.ToString(), out int maMay))
                {
                    maMayDangChon = maMay;
                }
                else
                {
                    maMayDangChon = null;
                    return;
                }

                txtTenMay.Text = row.Cells["Tên Máy"].Value == DBNull.Value ? "" : row.Cells["Tên Máy"].Value.ToString();
                txtHang.Text = row.Cells["Hãng"].Value == DBNull.Value ? "" : row.Cells["Hãng"].Value.ToString();
                txtChuyenChup.Text = row.Cells["Chuyên Chụp"].Value == DBNull.Value ? "" : row.Cells["Chuyên Chụp"].Value.ToString();
                txtLoaiMay.Text = row.Cells["Loại Máy"].Value == DBNull.Value ? "" : row.Cells["Loại Máy"].Value.ToString();
                txtSoSeri.Text = row.Cells["Số Seri"].Value == DBNull.Value ? "" : row.Cells["Số Seri"].Value.ToString();
                txtTinhTrang.Text = row.Cells["Tình Trạng"].Value == DBNull.Value ? "" : row.Cells["Tình Trạng"].Value.ToString();
                txtTrangThai.Text = row.Cells["Trạng Thái"].Value == DBNull.Value ? "" : row.Cells["Trạng Thái"].Value.ToString();

                if (row.Cells["Giá Thuê/Giờ"].Value != DBNull.Value &&
                    decimal.TryParse(row.Cells["Giá Thuê/Giờ"].Value.ToString(), out decimal giaGio))
                {
                    txtGiaThueTheoGio.Text = string.Format(cultureVN, "{0:N0}", giaGio);
                }
                else
                {
                    txtGiaThueTheoGio.Clear();
                }

                if (row.Cells["Giá Thuê/Ngày"].Value != DBNull.Value &&
                    decimal.TryParse(row.Cells["Giá Thuê/Ngày"].Value.ToString(), out decimal giaNgay))
                {
                    txtGiaThueTheoNgay.Text = string.Format(cultureVN, "{0:N0}", giaNgay);
                }
                else
                {
                    txtGiaThueTheoNgay.Clear();
                }

                LoadPhuKienTheoMay(maMayDangChon.Value);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem == null || conn == null)
                return;

            string selectedStatus = comboBox1.SelectedItem.ToString();

            string query = @"
                SELECT 
                    maMay AS [Mã Máy], 
                    tenMay AS [Tên Máy], 
                    hangMay AS [Hãng], 
                    chuyenChup AS [Chuyên Chụp],
                    loaiThietBi AS [Loại Máy], 
                    soSerial AS [Số Seri], 
                    giaThueTheoGio AS [Giá Thuê/Giờ], 
                    giaThueTheoNgay AS [Giá Thuê/Ngày], 
                    moTa AS [Tình Trạng], 
                    trangThai AS [Trạng Thái] 
                FROM MayAnh";

            try
            {
                if (selectedStatus != "Tất cả")
                {
                    query += " WHERE hangMay LIKE @hangMay";
                }

                using (SqlConnection localConn = new SqlConnection(conn.ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(query, localConn))
                    {
                        if (selectedStatus != "Tất cả")
                        {
                            cmd.Parameters.AddWithValue("@hangMay", "%" + selectedStatus + "%");
                        }

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            da.Fill(dt);
                            dataGridView1.DataSource = dt;

                            if (dataGridView1.Columns["Giá Thuê/Giờ"] != null)
                                dataGridView1.Columns["Giá Thuê/Giờ"].DefaultCellStyle.Format = "N0";

                            if (dataGridView1.Columns["Giá Thuê/Ngày"] != null)
                                dataGridView1.Columns["Giá Thuê/Ngày"].DefaultCellStyle.Format = "N0";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi lọc dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadHangMayToComboBox()
        {
            try
            {
                string query = "SELECT DISTINCT hangMay FROM MayAnh WHERE hangMay IS NOT NULL AND hangMay != ''";

                using (SqlConnection localConn = new SqlConnection(nguon))
                {
                    localConn.Open();

                    using (SqlCommand cmd = new SqlCommand(query, localConn))
                    {
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            comboBox1.Items.Clear();
                            comboBox1.Items.Add("Tất cả");

                            while (dr.Read())
                            {
                                comboBox1.Items.Add(dr["hangMay"].ToString().Trim());
                            }
                        }
                    }
                }

                comboBox1.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tự động tải danh sách hãng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnKhachHang_Click(object sender, EventArgs e)
        {
            KhachHang fKhachHang = new KhachHang();
            fKhachHang.Show();

            this.Hide();
        }

        private void btnHoaDon_Click(object sender, EventArgs e)
        {

        }
    }
}