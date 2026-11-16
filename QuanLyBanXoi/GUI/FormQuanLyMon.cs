using System;
using System.Drawing;
using System.Windows.Forms;
using QuanLyBanXoi.BUS;
using QuanLyBanXoi.DTO;

namespace QuanLyBanXoi.GUI
{
    public class FormQuanLyMon : Form
    {
        private readonly MonXoiBUS _monBus = new MonXoiBUS();
        private readonly ToppingBUS _tpBus = new ToppingBUS();

        // Controls Tab Món Xôi
        private TextBox txtMaMon = new(), txtTenMon = new(), txtGiaMon = new();
        private ComboBox cboLoaiMon = new(), cboTrangThaiMon = new();
        private DataGridView dgvMon = new();

        // Controls Tab Topping
        private TextBox txtMaTp = new(), txtTenTp = new(), txtGiaTp = new();
        private ComboBox cboTrangThaiTp = new();
        private DataGridView dgvTp = new();

        public FormQuanLyMon()
        {
            Text = "Quản Lý Thực Đơn - Quán Xôi Bà Béo";
            Size = new Size(850, 560);
            StartPosition = FormStartPosition.CenterScreen;

            TabControl tabControl = new TabControl { Dock = DockStyle.Fill };
            TabPage tabMon = new TabPage("Quản Lý Món Xôi & Đồ Uống");
            TabPage tabTopping = new TabPage("Quản Lý Topping Ăn Kèm");

            SetupTabMon(tabMon);
            SetupTabTopping(tabTopping);

            tabControl.TabPages.Add(tabMon);
            tabControl.TabPages.Add(tabTopping);
            Controls.Add(tabControl);

            LoadDataMon();
            LoadDataTopping();
        }

        private void SetupTabMon(TabPage tab)
        {
            tab.Controls.Add(new Label { Text = "Mã món:", Location = new Point(20, 23), AutoSize = true });
            txtMaMon.Location = new Point(100, 20); txtMaMon.Width = 100; txtMaMon.ReadOnly = true;
            tab.Controls.Add(txtMaMon);

            tab.Controls.Add(new Label { Text = "Tên món:", Location = new Point(230, 23), AutoSize = true });
            txtTenMon.Location = new Point(300, 20); txtTenMon.Width = 220;
            tab.Controls.Add(txtTenMon);

            tab.Controls.Add(new Label { Text = "Loại món:", Location = new Point(550, 23), AutoSize = true });
            cboLoaiMon.Location = new Point(620, 20); cboLoaiMon.Width = 170;
            cboLoaiMon.DropDownStyle = ComboBoxStyle.DropDownList;
            cboLoaiMon.Items.AddRange(new object[] { "Xôi mặn", "Xôi ngọt", "Đồ uống" });
            cboLoaiMon.SelectedIndex = 0;
            tab.Controls.Add(cboLoaiMon);

            tab.Controls.Add(new Label { Text = "Đơn giá:", Location = new Point(20, 63), AutoSize = true });
            txtGiaMon.Location = new Point(100, 60); txtGiaMon.Width = 180;
            tab.Controls.Add(txtGiaMon);

            tab.Controls.Add(new Label { Text = "Trạng thái:", Location = new Point(310, 63), AutoSize = true });
            cboTrangThaiMon.Location = new Point(390, 60); cboTrangThaiMon.Width = 150;
            cboTrangThaiMon.DropDownStyle = ComboBoxStyle.DropDownList;
            cboTrangThaiMon.Items.AddRange(new object[] { "Còn hàng", "Hết hàng" });
            cboTrangThaiMon.SelectedIndex = 0;
            tab.Controls.Add(cboTrangThaiMon);

            Button btnThem = new Button { Text = "Thêm Món", Location = new Point(20, 105), Size = new Size(100, 32), BackColor = Color.LightGreen };
            Button btnSua = new Button { Text = "Cập Nhật", Location = new Point(130, 105), Size = new Size(100, 32), BackColor = Color.LightSkyBlue };
            Button btnXoa = new Button { Text = "Xóa Món", Location = new Point(240, 105), Size = new Size(100, 32), BackColor = Color.LightCoral };
            Button btnMoi = new Button { Text = "Làm Mới", Location = new Point(350, 105), Size = new Size(100, 32) };

            btnThem.Click += (s, e) => {
                decimal.TryParse(txtGiaMon.Text, out decimal gia);
                var res = _monBus.ThemMon(new MonXoiDTO { TenMon = txtTenMon.Text, LoaiMon = cboLoaiMon.Text, DonGia = gia, TrangThai = cboTrangThaiMon.Text });
                MessageBox.Show(res.Message);
                if (res.Success) { LoadDataMon(); ClearMon(); }
            };

            btnSua.Click += (s, e) => {
                int.TryParse(txtMaMon.Text, out int ma);
                decimal.TryParse(txtGiaMon.Text, out decimal gia);
                var res = _monBus.SuaMon(new MonXoiDTO { MaMon = ma, TenMon = txtTenMon.Text, LoaiMon = cboLoaiMon.Text, DonGia = gia, TrangThai = cboTrangThaiMon.Text });
                MessageBox.Show(res.Message);
                if (res.Success) LoadDataMon();
            };

            btnXoa.Click += (s, e) => {
                int.TryParse(txtMaMon.Text, out int ma);
                var res = _monBus.XoaMon(ma);
                MessageBox.Show(res.Message);
                if (res.Success) { LoadDataMon(); ClearMon(); }
            };

            btnMoi.Click += (s, e) => ClearMon();

            tab.Controls.AddRange(new Control[] { btnThem, btnSua, btnXoa, btnMoi });

            dgvMon.Location = new Point(20, 150);
            dgvMon.Size = new Size(785, 320);
            dgvMon.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvMon.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvMon.ReadOnly = true;
            dgvMon.AllowUserToAddRows = false;
            dgvMon.CellClick += (s, e) => {
                if (e.RowIndex >= 0)
                {
                    var row = dgvMon.Rows[e.RowIndex];
                    txtMaMon.Text = row.Cells["MaMon"].Value?.ToString();
                    txtTenMon.Text = row.Cells["TenMon"].Value?.ToString();
                    cboLoaiMon.Text = row.Cells["LoaiMon"].Value?.ToString();
                    txtGiaMon.Text = row.Cells["DonGia"].Value?.ToString();
                    cboTrangThaiMon.Text = row.Cells["TrangThai"].Value?.ToString();
                }
            };
            tab.Controls.Add(dgvMon);
        }

        private void SetupTabTopping(TabPage tab)
        {
            tab.Controls.Add(new Label { Text = "Mã TP:", Location = new Point(20, 23), AutoSize = true });
            txtMaTp.Location = new Point(90, 20); txtMaTp.Width = 90; txtMaTp.ReadOnly = true;
            tab.Controls.Add(txtMaTp);

            tab.Controls.Add(new Label { Text = "Tên Topping:", Location = new Point(200, 23), AutoSize = true });
            txtTenTp.Location = new Point(290, 20); txtTenTp.Width = 220;
            tab.Controls.Add(txtTenTp);

            tab.Controls.Add(new Label { Text = "Đơn giá:", Location = new Point(530, 23), AutoSize = true });
            txtGiaTp.Location = new Point(590, 20); txtGiaTp.Width = 150;
            tab.Controls.Add(txtGiaTp);

            tab.Controls.Add(new Label { Text = "Trạng thái:", Location = new Point(20, 63), AutoSize = true });
            cboTrangThaiTp.Location = new Point(90, 60); cboTrangThaiTp.Width = 150;
            cboTrangThaiTp.DropDownStyle = ComboBoxStyle.DropDownList;
            cboTrangThaiTp.Items.AddRange(new object[] { "Còn hàng", "Hết hàng" });
            cboTrangThaiTp.SelectedIndex = 0;
            tab.Controls.Add(cboTrangThaiTp);

            Button btnThem = new Button { Text = "Thêm Topping", Location = new Point(20, 105), Size = new Size(110, 32), BackColor = Color.LightGreen };
            Button btnSua = new Button { Text = "Cập Nhật", Location = new Point(140, 105), Size = new Size(100, 32), BackColor = Color.LightSkyBlue };
            Button btnXoa = new Button { Text = "Xóa Topping", Location = new Point(250, 105), Size = new Size(110, 32), BackColor = Color.LightCoral };
            Button btnMoi = new Button { Text = "Làm Mới", Location = new Point(370, 105), Size = new Size(100, 32) };

            btnThem.Click += (s, e) => {
                decimal.TryParse(txtGiaTp.Text, out decimal gia);
                var res = _tpBus.ThemTopping(new ToppingDTO { TenTopping = txtTenTp.Text, DonGia = gia, TrangThai = cboTrangThaiTp.Text });
                MessageBox.Show(res.Message);
                if (res.Success) { LoadDataTopping(); ClearTp(); }
            };

            btnSua.Click += (s, e) => {
                int.TryParse(txtMaTp.Text, out int ma);
                decimal.TryParse(txtGiaTp.Text, out decimal gia);
                var res = _tpBus.SuaTopping(new ToppingDTO { MaTopping = ma, TenTopping = txtTenTp.Text, DonGia = gia, TrangThai = cboTrangThaiTp.Text });
                MessageBox.Show(res.Message);
                if (res.Success) LoadDataTopping();
            };

            btnXoa.Click += (s, e) => {
                int.TryParse(txtMaTp.Text, out int ma);
                var res = _tpBus.XoaTopping(ma);
                MessageBox.Show(res.Message);
                if (res.Success) { LoadDataTopping(); ClearTp(); }
            };

            btnMoi.Click += (s, e) => ClearTp();

            tab.Controls.AddRange(new Control[] { btnThem, btnSua, btnXoa, btnMoi });

            dgvTp.Location = new Point(20, 150);
            dgvTp.Size = new Size(785, 320);
            dgvTp.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvTp.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvTp.ReadOnly = true;
            dgvTp.AllowUserToAddRows = false;
            dgvTp.CellClick += (s, e) => {
                if (e.RowIndex >= 0)
                {
                    var row = dgvTp.Rows[e.RowIndex];
                    txtMaTp.Text = row.Cells["MaTopping"].Value?.ToString();
                    txtTenTp.Text = row.Cells["TenTopping"].Value?.ToString();
                    txtGiaTp.Text = row.Cells["DonGia"].Value?.ToString();
                    cboTrangThaiTp.Text = row.Cells["TrangThai"].Value?.ToString();
                }
            };
            tab.Controls.Add(dgvTp);
        }

        private void LoadDataMon() => dgvMon.DataSource = _monBus.LayDanhSachMon();
        private void LoadDataTopping() => dgvTp.DataSource = _tpBus.LayDanhSachTopping();

        private void ClearMon() { txtMaMon.Clear(); txtTenMon.Clear(); txtGiaMon.Clear(); cboLoaiMon.SelectedIndex = 0; cboTrangThaiMon.SelectedIndex = 0; }
        private void ClearTp() { txtMaTp.Clear(); txtTenTp.Clear(); txtGiaTp.Clear(); cboTrangThaiTp.SelectedIndex = 0; }
    }
}