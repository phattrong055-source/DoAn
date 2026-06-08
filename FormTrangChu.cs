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
    public partial class FormTrangChu : Form
    {
        string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=DoAnTotNghiep;Integrated Security=True";

        public FormTrangChu()
        {
            InitializeComponent();
        }

       
       

      

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string tuKhoa = txtTuKhoa.Text.Trim();           // nhu cầu chụp
            string hangMay = cmbHangMay.SelectedItem?.ToString(); // hãng
            string loaiMay = cmbLoaiMay.SelectedItem?.ToString(); // loại máy

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string sql = @"SELECT maMay AS [Mã Máy], 
                                  tenMay AS [Tên Máy], 
                                  hangMay AS [Hãng], 
                                  loaiThietBi AS [Loại Máy],
                                  chuyenChup AS [Chuyên Chụp], 
                                  giaThueTheoNgay AS [Giá Thuê Theo Ngày], 
                                  giaThueTheoGio AS [Giá Thuê Theo Giờ], 
                                  trangThai AS [Trạng Thái], 
                                  moTa AS [Mô Tả]
                           FROM MayAnh
                           WHERE chuyenChup LIKE @tuKhoa
                             AND (@hangMay IS NULL OR hangMay = @hangMay)
                             AND (@loaiMay IS NULL OR loaiThietBi = @loaiMay)";

                    SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                    da.SelectCommand.Parameters.AddWithValue("@tuKhoa", "%" + tuKhoa + "%");

                    if (string.IsNullOrEmpty(hangMay))
                        da.SelectCommand.Parameters.AddWithValue("@hangMay", DBNull.Value);
                    else
                        da.SelectCommand.Parameters.AddWithValue("@hangMay", hangMay);

                    if (string.IsNullOrEmpty(loaiMay))
                        da.SelectCommand.Parameters.AddWithValue("@loaiMay", DBNull.Value);
                    else
                        da.SelectCommand.Parameters.AddWithValue("@loaiMay", loaiMay);

                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    dataGridView1.DataSource = dt;

                    // Format tiền VNĐ
                    if (dataGridView1.Columns["Giá Thuê Theo Ngày"] != null)
                        dataGridView1.Columns["Giá Thuê Theo Ngày"].DefaultCellStyle.Format = "#,### đ";

                    if (dataGridView1.Columns["Giá Thuê Theo Giờ"] != null)
                        dataGridView1.Columns["Giá Thuê Theo Giờ"].DefaultCellStyle.Format = "#,### đ";

                    if (dt.Rows.Count == 0)
                    {
                        MessageBox.Show("Không tìm thấy máy ảnh phù hợp!");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi truy vấn cơ sở dữ liệu: " + ex.Message);
            }
        }

       

        

        

        private void btnTrangChu_Click_1(object sender, EventArgs e)
        {
            FormTrangChu FTC = new FormTrangChu();
            FTC.Show();
            this.Close();
        }

        private void btnMayAnh_Click_1(object sender, EventArgs e)
        {
            MayAnh Ma = new MayAnh();
            Ma.Show();
            this.Close();
        }
     
        private void bntPhuKien_Click(object sender, EventArgs e)
        {
            FormPhuKien FPK = new FormPhuKien();
            FPK.Show();
            this.Close();
        }

        private void btnKhachHang_Click(object sender, EventArgs e)
        {
            KhachHang KH = new KhachHang();
            KH.Show();
            this.Close();
        }
       
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

        private void btnHoaDon_Click_1(object sender, EventArgs e)
        {
            FormHoaDon FHD = new FormHoaDon();
            FHD.Show();
            this.Close();
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