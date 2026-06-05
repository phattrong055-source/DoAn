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
    public partial class MayAnh : Form
    {
        private string connectionString = @"Data Source=DESKTOP-K3O3CHO;Initial Catalog=DoAnTotNghiep;Integrated Security=True";
        private SqlConnection conn = null;
        private bool isThem = true; // Kiểm tra trạng thái Thêm hay Sửa
        private CultureInfo cultureVN = new CultureInfo("vi-VN"); // Định dạng tiền tệ VN 

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
            conn = new SqlConnection(connectionString);

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
            catch { }
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


        // Hàm tự động sinh số Mã Máy tiếp theo
        private int GetNextMayAnhID()
        {
            int nextID = 1;
            try
            {
                if (conn.State == ConnectionState.Closed) conn.Open();
                string query = "SELECT ISNULL(MAX(maMay), 0) + 1 FROM MayAnh";
                SqlCommand cmd = new SqlCommand(query, conn);
                nextID = Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch
            {
                nextID = 1;
            }
            finally
            {
                if (conn.State == ConnectionState.Open) conn.Close();
            }
            return nextID;
        }

        // Tải danh sách máy ảnh lên DataGridView 
        private void LoadData()
        {
            try
            {
                if (conn.State == ConnectionState.Closed) conn.Open();
                
                string query = "SELECT maMay AS [Mã Máy], tenMay AS [Tên Máy], hangMay AS [Hãng], " +
                               "loaiThietBi AS [Loại Máy], soSerial AS [Số Seri], giaThueTheoGio AS [Giá Thuê/Giờ], " +
                               "giaThueTheoNgay AS [Giá Thuê/Ngày], moTa AS [Tình Trạng], trangThai AS [Trạng Thái] FROM MayAnh";

                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;

                // Định dạng hiển thị dấu chấm hàng nghìn trên Grid
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
                if (conn.State == ConnectionState.Open) conn.Close();
            }
        }

        private void SetInterfaceState(bool isLocked)
        {
            txtMaMay.Enabled = false; // Mã máy tự tăng, luôn khóa
            txtTenMay.Enabled = !isLocked;
            txtHang.Enabled = !isLocked;
            txtLoaiMay.Enabled = !isLocked;
            txtSoSeri.Enabled = !isLocked;
            txtGiaThueTheoGio.Enabled = !isLocked;
            txtGiaThueTheoNgay.Enabled = !isLocked;
            txtTinhTrang.Enabled = !isLocked;
            txtTrangThai.Enabled = !isLocked;

            btnThem.Enabled = isLocked;
            btnSua.Enabled = isLocked;
            btnXoa.Enabled = isLocked;
            btnLuu.Enabled = !isLocked;
            btnHuy.Enabled = !isLocked;
        }

        private void ClearInputFields()
        {
            txtMaMay.Clear();
            txtTenMay.Clear();
            txtHang.Clear();
            txtLoaiMay.Clear();
            txtSoSeri.Clear();
            txtGiaThueTheoGio.Clear();
            txtGiaThueTheoNgay.Clear();
            txtTinhTrang.Clear();
            txtTrangThai.Clear();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            isThem = true;
            ClearInputFields();
            SetInterfaceState(false);
            txtMaMay.Text = GetNextMayAnhID().ToString();
            txtTenMay.Focus();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaMay.Text))
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
            if (string.IsNullOrEmpty(txtMaMay.Text))
            {
                MessageBox.Show("Vui lòng chọn máy ảnh muốn xóa bỏ!", "Nhắc nhở", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult confirm = MessageBox.Show("Bạn có chắc chắn muốn xóa máy ảnh này không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm == DialogResult.Yes)
            {
                try
                {
                    if (conn.State == ConnectionState.Closed) conn.Open();
                    string query = "DELETE FROM MayAnh WHERE maMay = @maMay";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@maMay", int.Parse(txtMaMay.Text));

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Xóa thông tin máy ảnh thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearInputFields();
                    LoadData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Không thể xóa máy ảnh này: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    if (conn.State == ConnectionState.Open) conn.Close();
                }
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtTenMay.Text) || string.IsNullOrEmpty(txtSoSeri.Text))
            {
                MessageBox.Show("Tên máy ảnh và Số Seri không được bỏ trống!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Xử lý loại bỏ dấu chấm phân cách hàng nghìn trước khi lưu vào database kiểu Decimal
            string cleanGiaGio = txtGiaThueTheoGio.Text.Replace(".", "");
            string cleanGiaNgay = txtGiaThueTheoNgay.Text.Replace(".", "");

            if (!decimal.TryParse(cleanGiaGio, out decimal giaGio) || !decimal.TryParse(cleanGiaNgay, out decimal giaNgay))
            {
                MessageBox.Show("Giá thuê giờ hoặc ngày không hợp lệ!", "Lỗi số", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                if (conn.State == ConnectionState.Closed) conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;

                if (isThem)
                {
                    // Đã sửa đổi tên trường tương ứng: hangMay, loaiThietBi, soSerial, moTa
                    cmd.CommandText = "INSERT INTO MayAnh (maMay, tenMay, hangMay, loaiThietBi, soSerial, giaThueTheoGio, giaThueTheoNgay, moTa, trangThai) " +
                                      "VALUES (@maMay, @tenMay, @hangMay, @loaiThietBi, @soSerial, @giaThueTheoGio, @giaThueTheoNgay, @moTa, @trangThai)";
                }
                else
                {
                    cmd.CommandText = "UPDATE MayAnh SET tenMay=@tenMay, hangMay=@hangMay, loaiThietBi=@loaiThietBi, soSerial=@soSerial, " +
                                      "giaThueTheoGio=@giaThueTheoGio, giaThueTheoNgay=@giaThueTheoNgay, moTa=@moTa, trangThai=@trangThai WHERE maMay=@maMay";
                }

                cmd.Parameters.AddWithValue("@maMay", int.Parse(txtMaMay.Text));
                cmd.Parameters.AddWithValue("@tenMay", txtTenMay.Text);
                cmd.Parameters.AddWithValue("@hangMay", string.IsNullOrEmpty(txtHang.Text) ? (object)DBNull.Value : txtHang.Text);
                cmd.Parameters.AddWithValue("@loaiThietBi", string.IsNullOrEmpty(txtLoaiMay.Text) ? (object)DBNull.Value : txtLoaiMay.Text);
                cmd.Parameters.AddWithValue("@soSerial", txtSoSeri.Text);
                cmd.Parameters.AddWithValue("@giaThueTheoGio", giaGio);
                cmd.Parameters.AddWithValue("@giaThueTheoNgay", giaNgay);
                cmd.Parameters.AddWithValue("@moTa", string.IsNullOrEmpty(txtTinhTrang.Text) ? (object)DBNull.Value : txtTinhTrang.Text);
                cmd.Parameters.AddWithValue("@trangThai", string.IsNullOrEmpty(txtTrangThai.Text) ? (object)DBNull.Value : txtTrangThai.Text);

                cmd.ExecuteNonQuery();
                MessageBox.Show(isThem ? "Thêm máy ảnh thành công!" : "Cập nhật thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                SetInterfaceState(true);
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi lưu dữ liệu: " + ex.Message, "Lỗi kết nối", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (conn.State == ConnectionState.Open) conn.Close();
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
            comboBox1.SelectedIndex = 0;
            LoadData();
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                txtMaMay.Text = row.Cells["Mã Máy"].Value.ToString();
                txtTenMay.Text = row.Cells["Tên Máy"].Value.ToString();
                txtHang.Text = row.Cells["Hãng"].Value.ToString();
                txtLoaiMay.Text = row.Cells["Loại Máy"].Value.ToString();
                txtSoSeri.Text = row.Cells["Số Seri"].Value.ToString();
                txtTinhTrang.Text = row.Cells["Tình Trạng"].Value.ToString();
                txtTrangThai.Text = row.Cells["Trạng Thái"].Value.ToString();

                if (decimal.TryParse(row.Cells["Giá Thuê/Giờ"].Value.ToString(), out decimal giaGio))
                    txtGiaThueTheoGio.Text = string.Format(cultureVN, "{0:N0}", giaGio);

                if (decimal.TryParse(row.Cells["Giá Thuê/Ngày"].Value.ToString(), out decimal giaNgay))
                    txtGiaThueTheoNgay.Text = string.Format(cultureVN, "{0:N0}", giaNgay);
            }
        }

        // Tìm kiếm 
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem == null || conn == null) return;

            string selectedStatus = comboBox1.SelectedItem.ToString();
            string query = "SELECT maMay AS [Mã Máy], tenMay AS [Tên Máy], hangMay AS [Hãng], " +
                           "loaiThietBi AS [Loại Máy], soSerial AS [Số Seri], giaThueTheoGio AS [Giá Thuê/Giờ], " +
                           "giaThueTheoNgay AS [Giá Thuê/Ngày], moTa AS [Tình Trạng], trangThai AS [Trạng Thái] FROM MayAnh";

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
                            cmd.Parameters.AddWithValue("@hangMay", $"%{selectedStatus}%");
                        }

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            da.Fill(dt); 
                            dataGridView1.DataSource = dt;
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

                
                using (SqlConnection localConn = new SqlConnection(conn.ConnectionString))
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