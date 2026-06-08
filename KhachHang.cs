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
    public partial class KhachHang : Form
    {

        string nguon = @"Data Source=DESKTOP-S4KUUMQ\SQLEXPRESS;Initial Catalog=DoAnTotNghiep;Integrated Security=True";


        private SqlConnection conn = null;
        private bool isThem = true; // Kiểm tra xem người dùng đang ấn "Thêm" hay "Sửa"

        public KhachHang()
        {
            InitializeComponent();
        }

        private void KhachHang_Load(object sender, EventArgs e)
        {
            conn = new SqlConnection(nguon);
            RegisterEvents();
            LoadData();
            SetInterfaceState(true);

            // Đổi tên hiển thị cho nút tìm kiếm (nếu cần)
            btnTimKiem.Text = "Tìm kiếm";
        }

        // Hàm đăng ký sự kiện cho các nút bấm và lưới dữ liệu
        private void RegisterEvents()
        {
            btnThem.Click += btnThem_Click;
            btnSua.Click += btnSua_Click;
            btnXoa.Click += btnXoa_Click;
            btnLuu.Click += btnLuu_Click;
            btnHuy.Click += btnHuy_Click;
            btnLamMoi.Click += btnLamMoi_Click;
            dataGridView1.CellClick += dataGridView1_CellClick;

            // Đăng ký sự kiện khi bấm nút tìm kiếm
            btnTimKiem.Click += btnTimKiem_Click;
        }
        // SỰ KIỆN KHI BẤM NÚT TÌM KIẾM
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string sdtTimKiem = txtTimKiem.Text.Trim();

            // Nếu ô tìm kiếm trống thì tải lại toàn bộ danh sách
            if (string.IsNullOrEmpty(sdtTimKiem))
            {
                LoadData();
            }
            else
            {
                TimKiemKhachHangTheoSdt(sdtTimKiem);
            }
        }
        // HÀM LỌC DỮ LIỆU THEO SĐT ĐÃ NHẬP TỪ TEXTBOX
        private void TimKiemKhachHangTheoSdt(string sdt)
        {
            try
            {
                if (conn.State == ConnectionState.Closed) conn.Open();

                // Sử dụng LIKE để tìm kiếm chứa chuỗi (tìm gần đúng)
                string query = "SELECT maKhachHang AS [Mã KH], hoTen AS [Họ Tên], sdt AS [Số ĐT], " +
                               "diaChi AS [Địa Chỉ], email AS [Email], cccd AS [CCCD/CMND], ngayTao AS [Ngày Tạo] " +
                               "FROM KhachHang WHERE sdt LIKE '%' + @sdt + '%'";

                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                da.SelectCommand.Parameters.AddWithValue("@sdt", sdt);

                DataTable dt = new DataTable();
                da.Fill(dt);

                dataGridView1.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lọc dữ liệu: " + ex.Message, "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (conn.State == ConnectionState.Open) conn.Close();
            }
        }

        // HÀM ĐỔ DANH SÁCH SỐ ĐIỆN THOẠI VÀO COMBOBOX


        // Hàm tải dữ liệu  DataGridView
        private void LoadData()
        {
            try
            {
                if (conn.State == ConnectionState.Closed) conn.Open();

                string query = "SELECT maKhachHang AS [Mã KH], hoTen AS [Họ Tên], sdt AS [Số ĐT], " +
                               "diaChi AS [Địa Chỉ], email AS [Email], cccd AS [CCCD/CMND], ngayTao AS [Ngày Tạo] FROM KhachHang";

                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);

                dataGridView1.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message, "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (conn.State == ConnectionState.Open) conn.Close();
            }
        }

        // HÀM LỌC DỮ LIỆU THEO SĐT ĐÃ CHỌN TỪ COMBOBOX
        private void LocKhachHangTheoSdt(string sdt)
        {
            try
            {
                if (conn.State == ConnectionState.Closed) conn.Open();

                string query = "SELECT maKhachHang AS [Mã KH], hoTen AS [Họ Tên], sdt AS [Số ĐT], " +
                               "diaChi AS [Địa Chỉ], email AS [Email], cccd AS [CCCD/CMND], ngayTao AS [Ngày Tạo] " +
                               "FROM KhachHang WHERE sdt = @sdt";

                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                da.SelectCommand.Parameters.AddWithValue("@sdt", sdt);

                DataTable dt = new DataTable();
                da.Fill(dt);

                dataGridView1.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lọc dữ liệu: " + ex.Message, "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (conn.State == ConnectionState.Open) conn.Close();
            }
        }

        // Hàm tự động lấy Mã Khách Hàng lớn nhất trong database và cộng thêm 1
        private int GetNextCustomerID()
        {
            int nextID = 1; // Mặc định nếu database chưa có ai thì bắt đầu từ số 1
            try
            {
                if (conn.State == ConnectionState.Closed) conn.Open();

                // Câu lệnh lấy số lớn nhất, nếu trống (NULL) thì lấy bằng 0, rồi cộng thêm 1
                string query = "SELECT ISNULL(MAX(maKhachHang), 0) + 1 FROM KhachHang";

                SqlCommand cmd = new SqlCommand(query, conn);
                nextID = Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tự động lấy số Mã KH mới: " + ex.Message, "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (conn.State == ConnectionState.Open) conn.Close();
            }
            return nextID;
        }

        // Hàm chuyển đổi trạng thái giao diện khóa/Mở khóa các ô và nút bấm
        private void SetInterfaceState(bool isLocked)
        {
            // Khóa ô mã lại không cho phép nhập tay
            txtMaKhachHang.Enabled = false;

            txtHoTen.Enabled = !isLocked;
            txtSdt.Enabled = !isLocked;
            txtCccd.Enabled = !isLocked;
            txtEmail.Enabled = !isLocked;
            txtDiaChi.Enabled = !isLocked;
            dtpNgayTao.Enabled = !isLocked;

            btnThem.Enabled = isLocked;
            btnSua.Enabled = isLocked;
            btnXoa.Enabled = isLocked;
            btnLuu.Enabled = !isLocked;
            btnHuy.Enabled = !isLocked;
        }

        // Hàm làm sạch dữ liệu cũ trên các ô TextBox
        
        private void ClearInputFields()
        {
            txtMaKhachHang.Clear();
            txtHoTen.Clear();
            txtSdt.Clear();
            txtCccd.Clear();
            txtEmail.Clear();
            txtDiaChi.Clear();
            dtpNgayTao.Value = DateTime.Now;

            // Xóa trắng cả ô nhập tìm kiếm
            txtTimKiem.Clear();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                txtMaKhachHang.Text = row.Cells["Mã KH"].Value.ToString();
                txtHoTen.Text = row.Cells["Họ Tên"].Value.ToString();
                txtSdt.Text = row.Cells["Số ĐT"].Value.ToString();
                txtDiaChi.Text = row.Cells["Địa Chỉ"].Value.ToString();
                txtEmail.Text = row.Cells["Email"].Value.ToString();
                txtCccd.Text = row.Cells["CCCD/CMND"].Value.ToString();

                if (row.Cells["Ngày Tạo"].Value != DBNull.Value)
                {
                    dtpNgayTao.Value = Convert.ToDateTime(row.Cells["Ngày Tạo"].Value);
                }
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            isThem = true;
            ClearInputFields();
            SetInterfaceState(false);

            // Gọi hàm tự động tăng số và gán trực tiếp vào ô TextBox Mã
            txtMaKhachHang.Text = GetNextCustomerID().ToString();

            txtHoTen.Focus();
        }


        private void btnSua_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaKhachHang.Text))
            {
                MessageBox.Show("Vui lòng chọn khách hàng cần sửa thông tin từ danh sách bên dưới!", "Nhắc nhở", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            isThem = false;
            SetInterfaceState(false);
            txtHoTen.Focus();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaKhachHang.Text))
            {
                MessageBox.Show("Vui lòng chọn khách hàng muốn xóa từ danh sách!", "Nhắc nhở", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult confirm = MessageBox.Show("Bạn có chắc chắn muốn xóa khách hàng này ra khỏi hệ thống?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm == DialogResult.Yes)
            {
                try
                {
                    if (conn.State == ConnectionState.Closed) conn.Open();

                    string query = "DELETE FROM KhachHang WHERE maKhachHang = @maKhachHang";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@maKhachHang", int.Parse(txtMaKhachHang.Text));

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Xóa thông tin khách hàng thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    ClearInputFields();
                    LoadData();
                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi thực hiện xóa dữ liệu: " + ex.Message, "Lỗi kết nối", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    if (conn.State == ConnectionState.Open) conn.Close();
                }
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            // 1. Kiểm tra trống các trường  mặc định
            if (string.IsNullOrEmpty(txtMaKhachHang.Text) || string.IsNullOrEmpty(txtHoTen.Text))
            {
                MessageBox.Show("Mã khách hàng và Họ tên không được phép để trống!", "Cảnh báo dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            // 2. Ràng buộc Số điện thoại 10 số
            if (!string.IsNullOrEmpty(txtSdt.Text))
            {
                if (txtSdt.Text.Length != 10 || !txtSdt.Text.All(char.IsDigit))
                {
                    MessageBox.Show("Số điện thoại không hợp lệ! Phải nhập ĐỦ 10 CHỮ SỐ.", "Lỗi định dạng", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtSdt.Focus();
                    return;
                }
            }

            // 3. Ràng buộc CCCD phải đủ 12 số 
            if (!string.IsNullOrEmpty(txtCccd.Text))
            {
                if (txtCccd.Text.Length != 12 || !txtCccd.Text.All(char.IsDigit))
                {
                    MessageBox.Show("Số CCCD không hợp lệ! Phải nhập ĐỦ 12 CHỮ SỐ.", "Lỗi định dạng", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtCccd.Focus();
                    return;
                }
            }

            // 4. Ràng buộc Email phải có đuôi @gmail.com
            if (!string.IsNullOrEmpty(txtEmail.Text))
            {
                if (!txtEmail.Text.ToLower().EndsWith("@gmail.com"))
                {
                    MessageBox.Show("Email không đúng định dạng! Phải kết thúc bằng '@gmail.com'.", "Lỗi định dạng", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtEmail.Focus();
                    return;
                }
            }

            try
            {
                if (conn.State == ConnectionState.Closed) conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;

                if (isThem)
                {
                    string checkQuery = "SELECT COUNT(*) FROM KhachHang WHERE maKhachHang = @maKhachHang";
                    SqlCommand checkCmd = new SqlCommand(checkQuery, conn);
                    checkCmd.Parameters.AddWithValue("@maKhachHang", int.Parse(txtMaKhachHang.Text));
                    int exists = (int)checkCmd.ExecuteScalar();

                    if (exists > 0)
                    {
                        MessageBox.Show("Mã khách hàng này đã tồn tại trên hệ thống! Vui lòng chọn mã khác.", "Trùng lặp", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    cmd.CommandText = "INSERT INTO KhachHang (maKhachHang, hoTen, sdt, diaChi, email, cccd, ngayTao) " +
                                      "VALUES (@maKhachHang, @hoTen, @sdt, @diaChi, @email, @cccd, @ngayTao)";
                }
                else
                {
                    cmd.CommandText = "UPDATE KhachHang SET hoTen = @hoTen, sdt = @sdt, diaChi = @diaChi, " +
                                      "email = @email, cccd = @cccd, ngayTao = @ngayTao WHERE maKhachHang = @maKhachHang";
                }


                cmd.Parameters.AddWithValue("@maKhachHang", int.Parse(txtMaKhachHang.Text));
                cmd.Parameters.AddWithValue("@hoTen", txtHoTen.Text);
                cmd.Parameters.AddWithValue("@sdt", string.IsNullOrEmpty(txtSdt.Text) ? (object)DBNull.Value : txtSdt.Text);
                cmd.Parameters.AddWithValue("@diaChi", string.IsNullOrEmpty(txtDiaChi.Text) ? (object)DBNull.Value : txtDiaChi.Text);
                cmd.Parameters.AddWithValue("@email", string.IsNullOrEmpty(txtEmail.Text) ? (object)DBNull.Value : txtEmail.Text);
                cmd.Parameters.AddWithValue("@cccd", string.IsNullOrEmpty(txtCccd.Text) ? (object)DBNull.Value : txtCccd.Text);
                cmd.Parameters.AddWithValue("@ngayTao", dtpNgayTao.Value);

                cmd.ExecuteNonQuery();
                MessageBox.Show(isThem ? "Thêm mới khách hàng thành công!" : "Cập nhật thông tin khách hàng thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                SetInterfaceState(true);
                LoadData();
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi trong quá trình lưu trữ cơ sở dữ liệu: " + ex.Message, "Lỗi truy vấn", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (conn.State == ConnectionState.Open) conn.Close();
            }
        }

        // HỦY
        private void btnHuy_Click(object sender, EventArgs e)
        {
            ClearInputFields();
            SetInterfaceState(true);
            LoadData();
        }

        // LÀM MỚI 
        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            ClearInputFields();
            LoadData();
           
            MessageBox.Show("Hệ thống đã cập nhật và làm mới lại danh sách dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnMayAnh_Click(object sender, EventArgs e)
        {
            MayAnh fMayAnh = new MayAnh();
            fMayAnh.Show();
            this.Hide();
        }

        

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnHoaDon_Click(object sender, EventArgs e)
        {
            
        }

        private void btnPhieuThue_Click(object sender, EventArgs e)
        {
            FormPhieuThue fFormPhieuThue = new FormPhieuThue();
            fFormPhieuThue.Show();
            this.Hide();
        }

       /* private void btnThongKe_Click(object sender, EventArgs e)
        {
            FormThongKe fFormThongKe = new FormThongKe();
            fFormThongKe.Show();
            this.Hide();
        }*/
    }
}