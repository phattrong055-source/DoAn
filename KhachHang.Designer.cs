namespace WindowsFormsApp1
{
    partial class KhachHang
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanelFields = new System.Windows.Forms.TableLayoutPanel();
            this.lblMaKhachHang = new System.Windows.Forms.Label();
            this.txtMaKhachHang = new System.Windows.Forms.TextBox();
            this.lblHoTen = new System.Windows.Forms.Label();
            this.txtHoTen = new System.Windows.Forms.TextBox();
            this.lblSdt = new System.Windows.Forms.Label();
            this.txtSdt = new System.Windows.Forms.TextBox();
            this.lblCccd = new System.Windows.Forms.Label();
            this.txtCccd = new System.Windows.Forms.TextBox();
            this.lblEmail = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.lblNgayTao = new System.Windows.Forms.Label();
            this.dtpNgayTao = new System.Windows.Forms.DateTimePicker();
            this.lblDiaChi = new System.Windows.Forms.Label();
            this.txtDiaChi = new System.Windows.Forms.TextBox();
            this.tableLayoutPanelButtons = new System.Windows.Forms.TableLayoutPanel();
            this.btnThem = new System.Windows.Forms.Button();
            this.btnSua = new System.Windows.Forms.Button();
            this.btnXoa = new System.Windows.Forms.Button();
            this.btnLuu = new System.Windows.Forms.Button();
            this.btnHuy = new System.Windows.Forms.Button();
            this.btnLamMoi = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.panelSearch = new System.Windows.Forms.Panel();
            this.btnTimKiem = new System.Windows.Forms.Button();
            this.txtTimKiem = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanelMenu = new System.Windows.Forms.TableLayoutPanel();
            this.btnTrangChu = new System.Windows.Forms.Button();
            this.btnMayAnh = new System.Windows.Forms.Button();
            this.btnPhuKien = new System.Windows.Forms.Button();
            this.btnKhachHang = new System.Windows.Forms.Button();
            this.btnPhieuThue = new System.Windows.Forms.Button();
            this.btnPhieuTra = new System.Windows.Forms.Button();
            this.btnHoaDon = new System.Windows.Forms.Button();
            this.btnThongKe = new System.Windows.Forms.Button();
            this.btnDangXuat = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanelFields.SuspendLayout();
            this.tableLayoutPanelButtons.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.panelSearch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.tableLayoutPanelMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.tableLayoutPanelFields);
            this.groupBox1.Controls.Add(this.tableLayoutPanelButtons);
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(308, 18);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox1.Size = new System.Drawing.Size(1480, 308);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Thông Tin Chi Tiết Khách Hàng";
            // 
            // tableLayoutPanelFields
            // 
            this.tableLayoutPanelFields.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanelFields.ColumnCount = 6;
            this.tableLayoutPanelFields.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tableLayoutPanelFields.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanelFields.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tableLayoutPanelFields.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanelFields.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 165F));
            this.tableLayoutPanelFields.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanelFields.Controls.Add(this.lblMaKhachHang, 0, 0);
            this.tableLayoutPanelFields.Controls.Add(this.txtMaKhachHang, 1, 0);
            this.tableLayoutPanelFields.Controls.Add(this.lblHoTen, 2, 0);
            this.tableLayoutPanelFields.Controls.Add(this.txtHoTen, 3, 0);
            this.tableLayoutPanelFields.Controls.Add(this.lblSdt, 4, 0);
            this.tableLayoutPanelFields.Controls.Add(this.txtSdt, 5, 0);
            this.tableLayoutPanelFields.Controls.Add(this.lblCccd, 0, 1);
            this.tableLayoutPanelFields.Controls.Add(this.txtCccd, 1, 1);
            this.tableLayoutPanelFields.Controls.Add(this.lblEmail, 2, 1);
            this.tableLayoutPanelFields.Controls.Add(this.txtEmail, 3, 1);
            this.tableLayoutPanelFields.Controls.Add(this.lblNgayTao, 4, 1);
            this.tableLayoutPanelFields.Controls.Add(this.dtpNgayTao, 5, 1);
            this.tableLayoutPanelFields.Controls.Add(this.lblDiaChi, 0, 2);
            this.tableLayoutPanelFields.Controls.Add(this.txtDiaChi, 1, 2);
            this.tableLayoutPanelFields.Location = new System.Drawing.Point(22, 38);
            this.tableLayoutPanelFields.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tableLayoutPanelFields.Name = "tableLayoutPanelFields";
            this.tableLayoutPanelFields.RowCount = 3;
            this.tableLayoutPanelFields.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanelFields.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanelFields.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanelFields.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 31F));
            this.tableLayoutPanelFields.Size = new System.Drawing.Size(1436, 154);
            this.tableLayoutPanelFields.TabIndex = 0;
            // 
            // lblMaKhachHang
            // 
            this.lblMaKhachHang.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblMaKhachHang.Location = new System.Drawing.Point(4, 0);
            this.lblMaKhachHang.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMaKhachHang.Name = "lblMaKhachHang";
            this.lblMaKhachHang.Size = new System.Drawing.Size(142, 51);
            this.lblMaKhachHang.TabIndex = 0;
            this.lblMaKhachHang.Text = "Mã Khách Hàng:";
            this.lblMaKhachHang.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtMaKhachHang
            // 
            this.txtMaKhachHang.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtMaKhachHang.Location = new System.Drawing.Point(154, 5);
            this.txtMaKhachHang.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtMaKhachHang.Name = "txtMaKhachHang";
            this.txtMaKhachHang.Size = new System.Drawing.Size(315, 31);
            this.txtMaKhachHang.TabIndex = 1;
            // 
            // lblHoTen
            // 
            this.lblHoTen.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblHoTen.Location = new System.Drawing.Point(477, 0);
            this.lblHoTen.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblHoTen.Name = "lblHoTen";
            this.lblHoTen.Size = new System.Drawing.Size(142, 51);
            this.lblHoTen.TabIndex = 2;
            this.lblHoTen.Text = "Họ Tên:";
            this.lblHoTen.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtHoTen
            // 
            this.txtHoTen.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtHoTen.Location = new System.Drawing.Point(627, 5);
            this.txtHoTen.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtHoTen.Name = "txtHoTen";
            this.txtHoTen.Size = new System.Drawing.Size(315, 31);
            this.txtHoTen.TabIndex = 3;
            // 
            // lblSdt
            // 
            this.lblSdt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSdt.Location = new System.Drawing.Point(950, 0);
            this.lblSdt.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSdt.Name = "lblSdt";
            this.lblSdt.Size = new System.Drawing.Size(157, 51);
            this.lblSdt.TabIndex = 4;
            this.lblSdt.Text = "Số Điện Thoại:";
            this.lblSdt.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtSdt
            // 
            this.txtSdt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSdt.Location = new System.Drawing.Point(1115, 5);
            this.txtSdt.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtSdt.Name = "txtSdt";
            this.txtSdt.Size = new System.Drawing.Size(317, 31);
            this.txtSdt.TabIndex = 5;
            // 
            // lblCccd
            // 
            this.lblCccd.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCccd.Location = new System.Drawing.Point(4, 51);
            this.lblCccd.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCccd.Name = "lblCccd";
            this.lblCccd.Size = new System.Drawing.Size(142, 51);
            this.lblCccd.TabIndex = 6;
            this.lblCccd.Text = "Số CCCD:";
            this.lblCccd.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtCccd
            // 
            this.txtCccd.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtCccd.Location = new System.Drawing.Point(154, 56);
            this.txtCccd.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtCccd.Name = "txtCccd";
            this.txtCccd.Size = new System.Drawing.Size(315, 31);
            this.txtCccd.TabIndex = 7;
            // 
            // lblEmail
            // 
            this.lblEmail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblEmail.Location = new System.Drawing.Point(477, 51);
            this.lblEmail.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(142, 51);
            this.lblEmail.TabIndex = 8;
            this.lblEmail.Text = "Email:";
            this.lblEmail.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtEmail
            // 
            this.txtEmail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtEmail.Location = new System.Drawing.Point(627, 56);
            this.txtEmail.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(315, 31);
            this.txtEmail.TabIndex = 9;
            // 
            // lblNgayTao
            // 
            this.lblNgayTao.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblNgayTao.Location = new System.Drawing.Point(950, 51);
            this.lblNgayTao.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblNgayTao.Name = "lblNgayTao";
            this.lblNgayTao.Size = new System.Drawing.Size(157, 51);
            this.lblNgayTao.TabIndex = 10;
            this.lblNgayTao.Text = "Ngày Tạo:";
            this.lblNgayTao.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dtpNgayTao
            // 
            this.dtpNgayTao.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtpNgayTao.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpNgayTao.Location = new System.Drawing.Point(1115, 56);
            this.dtpNgayTao.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dtpNgayTao.Name = "dtpNgayTao";
            this.dtpNgayTao.Size = new System.Drawing.Size(317, 31);
            this.dtpNgayTao.TabIndex = 11;
            // 
            // lblDiaChi
            // 
            this.lblDiaChi.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblDiaChi.Location = new System.Drawing.Point(4, 102);
            this.lblDiaChi.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDiaChi.Name = "lblDiaChi";
            this.lblDiaChi.Size = new System.Drawing.Size(142, 52);
            this.lblDiaChi.TabIndex = 12;
            this.lblDiaChi.Text = "Địa Chỉ:";
            this.lblDiaChi.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtDiaChi
            // 
            this.tableLayoutPanelFields.SetColumnSpan(this.txtDiaChi, 5);
            this.txtDiaChi.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtDiaChi.Location = new System.Drawing.Point(154, 107);
            this.txtDiaChi.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtDiaChi.Name = "txtDiaChi";
            this.txtDiaChi.Size = new System.Drawing.Size(1278, 31);
            this.txtDiaChi.TabIndex = 13;
            // 
            // tableLayoutPanelButtons
            // 
            this.tableLayoutPanelButtons.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanelButtons.ColumnCount = 7;
            this.tableLayoutPanelButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanelButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanelButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanelButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanelButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanelButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanelButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanelButtons.Controls.Add(this.btnThem, 0, 0);
            this.tableLayoutPanelButtons.Controls.Add(this.btnSua, 1, 0);
            this.tableLayoutPanelButtons.Controls.Add(this.btnXoa, 2, 0);
            this.tableLayoutPanelButtons.Controls.Add(this.btnLuu, 4, 0);
            this.tableLayoutPanelButtons.Controls.Add(this.btnHuy, 5, 0);
            this.tableLayoutPanelButtons.Controls.Add(this.btnLamMoi, 6, 0);
            this.tableLayoutPanelButtons.Location = new System.Drawing.Point(22, 208);
            this.tableLayoutPanelButtons.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tableLayoutPanelButtons.Name = "tableLayoutPanelButtons";
            this.tableLayoutPanelButtons.RowCount = 1;
            this.tableLayoutPanelButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelButtons.Size = new System.Drawing.Size(1436, 77);
            this.tableLayoutPanelButtons.TabIndex = 1;
            // 
            // btnThem
            // 
            this.btnThem.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnThem.Location = new System.Drawing.Point(47, 11);
            this.btnThem.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnThem.Name = "btnThem";
            this.btnThem.Size = new System.Drawing.Size(135, 54);
            this.btnThem.TabIndex = 0;
            this.btnThem.Text = "Thêm";
            this.btnThem.UseVisualStyleBackColor = true;
            this.btnThem.Click += new System.EventHandler(this.btnThem_Click);
            // 
            // btnSua
            // 
            this.btnSua.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnSua.Location = new System.Drawing.Point(276, 11);
            this.btnSua.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnSua.Name = "btnSua";
            this.btnSua.Size = new System.Drawing.Size(135, 54);
            this.btnSua.TabIndex = 1;
            this.btnSua.Text = "Sửa";
            this.btnSua.UseVisualStyleBackColor = true;
            this.btnSua.Click += new System.EventHandler(this.btnSua_Click);
            // 
            // btnXoa
            // 
            this.btnXoa.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnXoa.Location = new System.Drawing.Point(505, 11);
            this.btnXoa.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Size = new System.Drawing.Size(135, 54);
            this.btnXoa.TabIndex = 2;
            this.btnXoa.Text = "Xóa";
            this.btnXoa.UseVisualStyleBackColor = true;
            this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click);
            // 
            // btnLuu
            // 
            this.btnLuu.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnLuu.Location = new System.Drawing.Point(794, 11);
            this.btnLuu.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnLuu.Name = "btnLuu";
            this.btnLuu.Size = new System.Drawing.Size(135, 54);
            this.btnLuu.TabIndex = 3;
            this.btnLuu.Text = "Lưu";
            this.btnLuu.UseVisualStyleBackColor = true;
            this.btnLuu.Click += new System.EventHandler(this.btnLuu_Click);
            // 
            // btnHuy
            // 
            this.btnHuy.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnHuy.Location = new System.Drawing.Point(1023, 11);
            this.btnHuy.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(135, 54);
            this.btnHuy.TabIndex = 4;
            this.btnHuy.Text = "Hủy";
            this.btnHuy.UseVisualStyleBackColor = true;
            this.btnHuy.Click += new System.EventHandler(this.btnHuy_Click);
            // 
            // btnLamMoi
            // 
            this.btnLamMoi.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnLamMoi.Location = new System.Drawing.Point(1253, 11);
            this.btnLamMoi.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnLamMoi.Name = "btnLamMoi";
            this.btnLamMoi.Size = new System.Drawing.Size(135, 54);
            this.btnLamMoi.TabIndex = 5;
            this.btnLamMoi.Text = "Làm Mới";
            this.btnLamMoi.UseVisualStyleBackColor = true;
            this.btnLamMoi.Click += new System.EventHandler(this.btnLamMoi_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.panelSearch);
            this.groupBox2.Controls.Add(this.dataGridView1);
            this.groupBox2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(308, 346);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox2.Size = new System.Drawing.Size(1480, 622);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Danh Sách Khách Hàng Trên Hệ Thống";
            // 
            // panelSearch
            // 
            this.panelSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panelSearch.Controls.Add(this.btnTimKiem);
            this.panelSearch.Controls.Add(this.txtTimKiem);
            this.panelSearch.Controls.Add(this.label1);
            this.panelSearch.Location = new System.Drawing.Point(836, 0);
            this.panelSearch.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panelSearch.Name = "panelSearch";
            this.panelSearch.Size = new System.Drawing.Size(645, 38);
            this.panelSearch.TabIndex = 26;
            // 
            // btnTimKiem
            // 
            this.btnTimKiem.Location = new System.Drawing.Point(538, -2);
            this.btnTimKiem.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnTimKiem.Name = "btnTimKiem";
            this.btnTimKiem.Size = new System.Drawing.Size(98, 35);
            this.btnTimKiem.TabIndex = 27;
            this.btnTimKiem.Text = "Tìm Kiếm";
            this.btnTimKiem.UseVisualStyleBackColor = true;
            // 
            // txtTimKiem
            // 
            this.txtTimKiem.Location = new System.Drawing.Point(99, 0);
            this.txtTimKiem.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtTimKiem.Name = "txtTimKiem";
            this.txtTimKiem.Size = new System.Drawing.Size(428, 31);
            this.txtTimKiem.TabIndex = 26;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(4, 0);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(105, 35);
            this.label1.TabIndex = 25;
            this.label1.Text = "Tìm Kiếm:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(22, 51);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersWidth = 62;
            this.dataGridView1.Size = new System.Drawing.Size(1436, 546);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox3.Controls.Add(this.tableLayoutPanelMenu);
            this.groupBox3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.Location = new System.Drawing.Point(18, 18);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox3.Size = new System.Drawing.Size(270, 949);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "TRANG CHỦ";
            // 
            // tableLayoutPanelMenu
            // 
            this.tableLayoutPanelMenu.ColumnCount = 1;
            this.tableLayoutPanelMenu.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelMenu.Controls.Add(this.btnTrangChu, 0, 0);
            this.tableLayoutPanelMenu.Controls.Add(this.btnMayAnh, 0, 1);
            this.tableLayoutPanelMenu.Controls.Add(this.btnPhuKien, 0, 2);
            this.tableLayoutPanelMenu.Controls.Add(this.btnKhachHang, 0, 3);
            this.tableLayoutPanelMenu.Controls.Add(this.btnPhieuThue, 0, 4);
            this.tableLayoutPanelMenu.Controls.Add(this.btnPhieuTra, 0, 5);
            this.tableLayoutPanelMenu.Controls.Add(this.btnHoaDon, 0, 6);
            this.tableLayoutPanelMenu.Controls.Add(this.btnThongKe, 0, 7);
            this.tableLayoutPanelMenu.Controls.Add(this.btnDangXuat, 0, 8);
            this.tableLayoutPanelMenu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelMenu.Location = new System.Drawing.Point(4, 29);
            this.tableLayoutPanelMenu.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tableLayoutPanelMenu.Name = "tableLayoutPanelMenu";
            this.tableLayoutPanelMenu.RowCount = 9;
            this.tableLayoutPanelMenu.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.11111F));
            this.tableLayoutPanelMenu.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.11111F));
            this.tableLayoutPanelMenu.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.11111F));
            this.tableLayoutPanelMenu.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.11111F));
            this.tableLayoutPanelMenu.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.11111F));
            this.tableLayoutPanelMenu.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.11111F));
            this.tableLayoutPanelMenu.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.11111F));
            this.tableLayoutPanelMenu.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.11111F));
            this.tableLayoutPanelMenu.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.11111F));
            this.tableLayoutPanelMenu.Size = new System.Drawing.Size(262, 915);
            this.tableLayoutPanelMenu.TabIndex = 0;
            // 
            // btnTrangChu
            // 
            this.btnTrangChu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnTrangChu.Location = new System.Drawing.Point(8, 5);
            this.btnTrangChu.Margin = new System.Windows.Forms.Padding(8, 5, 8, 5);
            this.btnTrangChu.Name = "btnTrangChu";
            this.btnTrangChu.Size = new System.Drawing.Size(246, 91);
            this.btnTrangChu.TabIndex = 0;
            this.btnTrangChu.Text = "🏠  Trang chủ";
            this.btnTrangChu.UseVisualStyleBackColor = true;
            // 
            // btnMayAnh
            // 
            this.btnMayAnh.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnMayAnh.Location = new System.Drawing.Point(8, 106);
            this.btnMayAnh.Margin = new System.Windows.Forms.Padding(8, 5, 8, 5);
            this.btnMayAnh.Name = "btnMayAnh";
            this.btnMayAnh.Size = new System.Drawing.Size(246, 91);
            this.btnMayAnh.TabIndex = 1;
            this.btnMayAnh.Text = "📷  Máy ảnh";
            this.btnMayAnh.UseVisualStyleBackColor = true;
            this.btnMayAnh.Click += new System.EventHandler(this.btnMayAnh_Click);
            // 
            // btnPhuKien
            // 
            this.btnPhuKien.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnPhuKien.Location = new System.Drawing.Point(8, 207);
            this.btnPhuKien.Margin = new System.Windows.Forms.Padding(8, 5, 8, 5);
            this.btnPhuKien.Name = "btnPhuKien";
            this.btnPhuKien.Size = new System.Drawing.Size(246, 91);
            this.btnPhuKien.TabIndex = 2;
            this.btnPhuKien.Text = "💼  Phụ kiện";
            this.btnPhuKien.UseVisualStyleBackColor = true;
            // 
            // btnKhachHang
            // 
            this.btnKhachHang.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnKhachHang.Location = new System.Drawing.Point(8, 308);
            this.btnKhachHang.Margin = new System.Windows.Forms.Padding(8, 5, 8, 5);
            this.btnKhachHang.Name = "btnKhachHang";
            this.btnKhachHang.Size = new System.Drawing.Size(246, 91);
            this.btnKhachHang.TabIndex = 3;
            this.btnKhachHang.Text = "👥  Khách hàng";
            this.btnKhachHang.UseVisualStyleBackColor = true;
            // 
            // btnPhieuThue
            // 
            this.btnPhieuThue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnPhieuThue.Location = new System.Drawing.Point(8, 409);
            this.btnPhieuThue.Margin = new System.Windows.Forms.Padding(8, 5, 8, 5);
            this.btnPhieuThue.Name = "btnPhieuThue";
            this.btnPhieuThue.Size = new System.Drawing.Size(246, 91);
            this.btnPhieuThue.TabIndex = 5;
            this.btnPhieuThue.Text = "📅  Phiếu thuê";
            this.btnPhieuThue.UseVisualStyleBackColor = true;
            this.btnPhieuThue.Click += new System.EventHandler(this.btnPhieuThue_Click);
            // 
            // btnPhieuTra
            // 
            this.btnPhieuTra.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnPhieuTra.Location = new System.Drawing.Point(8, 510);
            this.btnPhieuTra.Margin = new System.Windows.Forms.Padding(8, 5, 8, 5);
            this.btnPhieuTra.Name = "btnPhieuTra";
            this.btnPhieuTra.Size = new System.Drawing.Size(246, 91);
            this.btnPhieuTra.TabIndex = 6;
            this.btnPhieuTra.Text = "🔄  Phiếu trả";
            this.btnPhieuTra.UseVisualStyleBackColor = true;
            // 
            // btnHoaDon
            // 
            this.btnHoaDon.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnHoaDon.Location = new System.Drawing.Point(8, 611);
            this.btnHoaDon.Margin = new System.Windows.Forms.Padding(8, 5, 8, 5);
            this.btnHoaDon.Name = "btnHoaDon";
            this.btnHoaDon.Size = new System.Drawing.Size(246, 91);
            this.btnHoaDon.TabIndex = 8;
            this.btnHoaDon.Text = "📄  Hóa đơn";
            this.btnHoaDon.UseVisualStyleBackColor = true;
            this.btnHoaDon.Click += new System.EventHandler(this.btnHoaDon_Click);
            // 
            // btnThongKe
            // 
            this.btnThongKe.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnThongKe.Location = new System.Drawing.Point(8, 712);
            this.btnThongKe.Margin = new System.Windows.Forms.Padding(8, 5, 8, 5);
            this.btnThongKe.Name = "btnThongKe";
            this.btnThongKe.Size = new System.Drawing.Size(246, 91);
            this.btnThongKe.TabIndex = 9;
            this.btnThongKe.Text = "📊  Thống kê";
            this.btnThongKe.UseVisualStyleBackColor = true;
            // 
            // btnDangXuat
            // 
            this.btnDangXuat.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnDangXuat.Location = new System.Drawing.Point(8, 813);
            this.btnDangXuat.Margin = new System.Windows.Forms.Padding(8, 5, 8, 5);
            this.btnDangXuat.Name = "btnDangXuat";
            this.btnDangXuat.Size = new System.Drawing.Size(246, 97);
            this.btnDangXuat.TabIndex = 11;
            this.btnDangXuat.Text = "🚪  Đăng xuất";
            this.btnDangXuat.UseVisualStyleBackColor = true;
            // 
            // KhachHang
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1806, 986);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "KhachHang";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Quản Lý Khách Hàng";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.KhachHang_Load);
            this.groupBox1.ResumeLayout(false);
            this.tableLayoutPanelFields.ResumeLayout(false);
            this.tableLayoutPanelFields.PerformLayout();
            this.tableLayoutPanelButtons.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.panelSearch.ResumeLayout(false);
            this.panelSearch.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.tableLayoutPanelMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelFields;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelButtons;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Panel panelSearch;
        private System.Windows.Forms.Label lblMaKhachHang;
        private System.Windows.Forms.TextBox txtMaKhachHang;
        private System.Windows.Forms.Label lblHoTen;
        private System.Windows.Forms.TextBox txtHoTen;
        private System.Windows.Forms.Label lblSdt;
        private System.Windows.Forms.TextBox txtSdt;
        private System.Windows.Forms.Label lblCccd;
        private System.Windows.Forms.TextBox txtCccd;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Label lblNgayTao;
        private System.Windows.Forms.DateTimePicker dtpNgayTao;
        private System.Windows.Forms.Label lblDiaChi;
        private System.Windows.Forms.TextBox txtDiaChi;
        private System.Windows.Forms.Button btnThem;
        private System.Windows.Forms.Button btnSua;
        private System.Windows.Forms.Button btnXoa;
        private System.Windows.Forms.Button btnLuu;
        private System.Windows.Forms.Button btnHuy;
        private System.Windows.Forms.Button btnLamMoi;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelMenu;
        private System.Windows.Forms.Button btnKhachHang;
        private System.Windows.Forms.Button btnPhuKien;
        private System.Windows.Forms.Button btnMayAnh;
        private System.Windows.Forms.Button btnTrangChu;
        private System.Windows.Forms.Button btnDangXuat;
        private System.Windows.Forms.Button btnThongKe;
        private System.Windows.Forms.Button btnHoaDon;
        private System.Windows.Forms.Button btnPhieuTra;
        private System.Windows.Forms.Button btnPhieuThue;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnTimKiem;
        private System.Windows.Forms.TextBox txtTimKiem;
    }
}