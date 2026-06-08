using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class FormThongKe : Form
    {
        SqlConnection conn = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=DoAnTotNghiep;Integrated Security=True");

        public FormThongKe()
        {
            InitializeComponent();

            // Gắn sự kiện nút Xem
            btnXem.Click += btnXem_Click;
            btnXem2.Click += btnXem2_Click;
        }

        private void btnXem_Click(object sender, EventArgs e)
        {
            LoadThongKe();
        }

        private void LoadThongKe()
        {
            if (comboBoxThoiGian.SelectedItem == null) return;

            string selection = comboBoxThoiGian.SelectedItem.ToString();
            DataTable dt = new DataTable();

            try
            {
                conn.Open();

                if (selection == "Ngày")
                {
                    int year = DateTime.Now.Year;
                    int month = DateTime.Now.Month;
                    int daysInMonth = DateTime.DaysInMonth(year, month);

                    string query = @"
                    ;WITH Days AS
                    (
                        SELECT 1 AS DayNum
                        UNION ALL
                        SELECT DayNum + 1 FROM Days WHERE DayNum < @daysInMonth
                    )
                    SELECT
                        DATEFROMPARTS(@year, @month, DayNum) AS NgayLap,
                        ISNULL((SELECT COUNT(*) FROM HoaDon WHERE CAST(ngayLap AS DATE) = DATEFROMPARTS(@year, @month, DayNum)),0) AS TongHoaDon,
                        ISNULL((SELECT SUM(tongTienThue) FROM PhieuTra WHERE CAST(ngayTra AS DATE) = DATEFROMPARTS(@year, @month, DayNum)),0) AS TongChiPhiThue,
                        ISNULL((SELECT SUM(tongPhiPhatSinh) FROM PhieuTra WHERE CAST(ngayTra AS DATE) = DATEFROMPARTS(@year, @month, DayNum)),0) AS PhiPhatSinh
                    FROM Days
                    ORDER BY DayNum
                    OPTION (MAXRECURSION 0);";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@year", year);
                        cmd.Parameters.AddWithValue("@month", month);
                        cmd.Parameters.AddWithValue("@daysInMonth", daysInMonth);

                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(dt);
                    }
                }
                else if (selection == "Tháng")
                {
                    int year = DateTime.Now.Year;

                    string query = @"
                    ;WITH Months AS
                    (
                        SELECT 1 AS Thang
                        UNION ALL
                        SELECT Thang + 1 FROM Months WHERE Thang < 12
                    )
                    SELECT
                        Thang,
                        ISNULL((SELECT COUNT(*) FROM HoaDon WHERE YEAR(ngayLap) = @year AND MONTH(ngayLap) = Thang),0) AS TongHoaDon,
                        ISNULL((SELECT SUM(tongTienThue) FROM PhieuTra WHERE YEAR(ngayTra) = @year AND MONTH(ngayTra) = Thang),0) AS TongChiPhiThue,
                        ISNULL((SELECT SUM(tongPhiPhatSinh) FROM PhieuTra WHERE YEAR(ngayTra) = @year AND MONTH(ngayTra) = Thang),0) AS PhiPhatSinh
                    FROM Months
                    ORDER BY Thang
                    OPTION (MAXRECURSION 0);";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@year", year);

                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(dt);
                    }
                }
                else if (selection == "Năm")
                {
                    string query = @"
                    SELECT
                        YEAR(ngayLap) AS Nam,
                        COUNT(*) AS TongHoaDon,
                        (SELECT SUM(tongTienThue) FROM PhieuTra WHERE YEAR(ngayTra) = YEAR(hd.ngayLap)) AS TongChiPhiThue,
                        (SELECT SUM(tongPhiPhatSinh) FROM PhieuTra WHERE YEAR(ngayTra) = YEAR(hd.ngayLap)) AS PhiPhatSinh
                    FROM HoaDon hd
                    GROUP BY YEAR(ngayLap)
                    ORDER BY Nam;";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(dt);
                    }
                }

                dataGridViewThongKe.DataSource = dt;
                dataGridViewThongKe.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi load thống kê: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        // --- Bảng thống kê Máy ---
        private void btnXem2_Click(object sender, EventArgs e)
        {
            if (comboBoxMay.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn loại thống kê máy.");
                return;
            }

            string loaiThongKe = comboBoxMay.SelectedItem.ToString().Trim();
            DataTable dtMay = new DataTable();

            try
            {
                conn.Open();
                string query = "";

                if (loaiThongKe == "Máy thuê nhiều nhất")
                {
                    query = @"
                SELECT 
                    m.tenMay AS TenMay,
                    COUNT(ct.maChiTiet) AS SoLanThue,
                    SUM(ct.donGia) AS TongTienThue
                FROM ChiTietPhieuThue ct
                INNER JOIN MayAnh m ON ct.maMay = m.maMay
                GROUP BY m.tenMay
                ORDER BY SoLanThue DESC, TenMay;";
                }
                else if (loaiThongKe == "Doanh Thu Từng Máy")
                {
                    query = @"
                SELECT 
                    m.tenMay AS TenMay,
                    COUNT(ct.maChiTiet) AS SoLanThue,
                    SUM(ct.donGia) AS TongTienThue
                FROM ChiTietPhieuThue ct
                INNER JOIN MayAnh m ON ct.maMay = m.maMay
                GROUP BY m.tenMay
                ORDER BY TongTienThue DESC, TenMay;";
                }
                else
                {
                    MessageBox.Show("Loại thống kê không hợp lệ.");
                    return;
                }

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.CommandType = CommandType.Text; // đảm bảo CommandText được khởi tạo
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dtMay);
                }

                dataGridViewMay.DataSource = dtMay;
                dataGridViewMay.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi load thống kê máy: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }
    }
}