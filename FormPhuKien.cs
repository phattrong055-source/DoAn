using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class FormPhuKien : Form
    {
        string nguon = @"Data Source=DESKTOP-S4KUUMQ\SQLEXPRESS;Initial Catalog=DoAnTotNghiep;Integrated Security=True";

        public FormPhuKien()
        {
            InitializeComponent();

            // GẮN SỰ KIỆN CLICK DATAGRIDVIEW
            dataGridView1.CellClick += dataGridView1_CellClick;

            LoadDuLieu();
        }

        // ================= HIỂN THỊ DỮ LIỆU =================
        void LoadDuLieu()
        {
            using (SqlConnection conn = new SqlConnection(nguon))
            {
                try
                {
                    conn.Open();

                    string sql = "SELECT maPhuKien, tenPhuKien, moTa FROM dbo.PhuKien";

                    SqlDataAdapter da = new SqlDataAdapter(sql, conn);

                    DataTable dt = new DataTable();

                    da.Fill(dt);

                    dataGridView1.DataSource = dt;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message);
                }
            }
        }

        // ================= XÓA TRẮNG TEXTBOX =================
        void XoaTrang()
        {
            txtMaPK.Clear();
            txtTenPhuKien.Clear();
            txtMoTa.Clear();

            txtMaPK.Focus();
        }

        // ================= THÊM =================
        private void btnThem_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(nguon))
            {
                try
                {
                    conn.Open();

                    string sql = @"INSERT INTO dbo.PhuKien
                                   (maPhuKien, tenPhuKien, moTa)
                                   VALUES
                                   (@maPhuKien, @tenPhuKien, @moTa)";

                    SqlCommand cmd = new SqlCommand(sql, conn);

                    cmd.Parameters.AddWithValue("@maPhuKien", txtMaPK.Text);
                    cmd.Parameters.AddWithValue("@tenPhuKien", txtTenPhuKien.Text);
                    cmd.Parameters.AddWithValue("@moTa", txtMoTa.Text);

                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Thêm thành công!");

                    LoadDuLieu();
                    XoaTrang();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi thêm: " + ex.Message);
                }
            }
        }

        // ================= SỬA =================
        private void btnSua_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(nguon))
            {
                try
                {
                    conn.Open();

                    string sql = @"UPDATE dbo.PhuKien
                                   SET tenPhuKien = @tenPhuKien,
                                       moTa = @moTa
                                   WHERE maPhuKien = @maPhuKien";

                    SqlCommand cmd = new SqlCommand(sql, conn);

                    cmd.Parameters.AddWithValue("@maPhuKien", txtMaPK.Text);
                    cmd.Parameters.AddWithValue("@tenPhuKien", txtTenPhuKien.Text);
                    cmd.Parameters.AddWithValue("@moTa", txtMoTa.Text);

                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Sửa thành công!");

                    LoadDuLieu();
                    XoaTrang();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi sửa: " + ex.Message);
                }
            }
        }

        // ================= XÓA =================
        private void btnXoa_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Bạn có chắc chắn muốn xóa không?",
                "Xác nhận",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (result == DialogResult.Yes)
            {
                using (SqlConnection conn = new SqlConnection(nguon))
                {
                    try
                    {
                        conn.Open();

                        string sql = @"DELETE FROM dbo.PhuKien
                                       WHERE maPhuKien = @maPhuKien";

                        SqlCommand cmd = new SqlCommand(sql, conn);

                        cmd.Parameters.AddWithValue("@maPhuKien", txtMaPK.Text);

                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Xóa thành công!");

                        LoadDuLieu();
                        XoaTrang();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi xóa: " + ex.Message);
                    }
                }
            }
        }

        // ================= CLICK DATAGRIDVIEW =================
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                txtMaPK.Text =
                    dataGridView1.Rows[e.RowIndex].Cells["maPhuKien"].Value.ToString();

                txtTenPhuKien.Text =
                    dataGridView1.Rows[e.RowIndex].Cells["tenPhuKien"].Value.ToString();

                txtMoTa.Text =
                    dataGridView1.Rows[e.RowIndex].Cells["moTa"].Value.ToString();
            }
        }

      
        private void btnTrangChu_Click(object sender, EventArgs e)
        {
            FormTrangChu FTC = new FormTrangChu();
            FTC.Show();
            this.Close();
        }

        /*private void btnMayAnh_Click(object sender, EventArgs e)
        {
            MayAnh Ma = new MayAnh();
            Ma.Show();
            this.Close();
        }*/

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