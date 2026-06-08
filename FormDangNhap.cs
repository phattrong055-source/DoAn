using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class FormDangNhap : Form
    {
        // CHUỖI KẾT NỐI SQL
        string nguon = @"Data Source=DESKTOP-S4KUUMQ\SQLEXPRESS;Initial Catalog=DoAnTotNghiep;Integrated Security=True";

        public FormDangNhap()
        {
            InitializeComponent();

            // ẨN MẬT KHẨU
            txtMatKhau.PasswordChar = '*';
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        // ================= ĐĂNG NHẬP =================
        private void buttonDangNhap_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(nguon);

            try
            {
                conn.Open();

                string taiKhoan = txtTenDN.Text.Trim();
                string matKhau = txtMatKhau.Text.Trim();

                // CÂU SQL KIỂM TRA ĐĂNG NHẬP
                string sql = @"SELECT COUNT(*) 
                               FROM TaiKhoan
                               WHERE tenDangNhap = @tenDangNhap
                               AND matKhau = @matKhau";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@tenDangNhap", taiKhoan);
                cmd.Parameters.AddWithValue("@matKhau", matKhau);

                int ketQua = (int)cmd.ExecuteScalar();

                if (ketQua > 0)
                {
                    MessageBox.Show("Đăng nhập thành công!");

                    FormTrangChu frm = new FormTrangChu();
                    frm.Show();

                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Sai tài khoản hoặc mật khẩu!");
                }
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
    }
}