using System;
using System.Drawing;
using System.Windows.Forms;

namespace QuanLyBanXoi.GUI
{
    public class FormMain : Form
    {
        private Panel pnlSidebar = new();
        private Panel pnlHeader = new();
        private Panel pnlContent = new();
        private Label lblTitle = new();
        private Button? currentButton;

        public FormMain()
        {
            Text = "Hệ Thống Quản Lý - Quán Xôi Bà Béo";
            ClientSize = new Size(1080, 630);
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;

            SetupLayout();

            // Mở mặc định màn hình Quản lý thực đơn khi khởi động
            OpenChildForm(new FormQuanLyMon(), "QUẢN LÝ THỰC ĐƠN & TOPPING - QUÁN XÔI BÀ BÉO");
        }

        private void SetupLayout()
        {
            // 1. Thanh Menu bên trái (Sidebar)
            pnlSidebar.Dock = DockStyle.Left;
            pnlSidebar.Width = 225;
            pnlSidebar.BackColor = Color.FromArgb(45, 52, 54);

            Label lblLogo = new Label
            {
                Text = "QUÁN XÔI BÀ BÉO",
                Dock = DockStyle.Top,
                Height = 90,
                ForeColor = Color.Gold,
                Font = new Font("Segoe UI", 12.5f, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter
            };

            Button btnThongKe = CreateMenuButton("Thống Kê & Lịch Sử");
            btnThongKe.Click += (s, e) => {
                ActivateButton(btnThongKe);
                MessageBox.Show("Chức năng của Thành viên 3 (Đang phát triển)!", "Thông báo");
            };

            Button btnQuanLyMon = CreateMenuButton("Quản Lý Thực Đơn");
            btnQuanLyMon.Click += (s, e) => {
                ActivateButton(btnQuanLyMon);
                OpenChildForm(new FormQuanLyMon(), "QUẢN LÝ THỰC ĐƠN & TOPPING - QUÁN XÔI BÀ BÉO");
            };

            Button btnBanHang = CreateMenuButton("Bán Hàng & Lập Đơn");
            btnBanHang.Click += (s, e) => {
                ActivateButton(btnBanHang);
                MessageBox.Show("Chức năng của Thành viên 2 (Đang phát triển)!", "Thông báo");
            };

            Button btnThoat = CreateMenuButton("Thoát Ứng Dụng");
            btnThoat.Dock = DockStyle.Bottom;
            btnThoat.BackColor = Color.FromArgb(192, 57, 43);
            btnThoat.Click += (s, e) => Application.Exit();

            pnlSidebar.Controls.Add(btnThoat);
            pnlSidebar.Controls.Add(btnThongKe);
            pnlSidebar.Controls.Add(btnQuanLyMon);
            pnlSidebar.Controls.Add(btnBanHang);
            pnlSidebar.Controls.Add(lblLogo);

            // 2. Thanh tiêu đề phía trên (Header)
            pnlHeader.Dock = DockStyle.Top;
            pnlHeader.Height = 55;
            pnlHeader.BackColor = Color.FromArgb(243, 156, 18);

            lblTitle.Dock = DockStyle.Fill;
            lblTitle.ForeColor = Color.White;
            lblTitle.Font = new Font("Segoe UI", 13.5f, FontStyle.Bold);
            lblTitle.TextAlign = ContentAlignment.MiddleLeft;
            lblTitle.Padding = new Padding(20, 0, 0, 0);
            pnlHeader.Controls.Add(lblTitle);

            // 3. Vùng hiển thị nội dung các Form con
            pnlContent.Dock = DockStyle.Fill;
            pnlContent.BackColor = Color.WhiteSmoke;

            Controls.Add(pnlContent);
            Controls.Add(pnlHeader);
            Controls.Add(pnlSidebar);

            ActivateButton(btnQuanLyMon);
        }

        private Button CreateMenuButton(string text)
        {
            return new Button
            {
                Text = "  " + text,
                Dock = DockStyle.Top,
                Height = 52,
                FlatStyle = FlatStyle.Flat,
                FlatAppearance = { BorderSize = 0 },
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10.5f, FontStyle.Regular),
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(15, 0, 0, 0),
                Cursor = Cursors.Hand
            };
        }

        private void ActivateButton(Button btn)
        {
            if (currentButton != null && currentButton.Dock != DockStyle.Bottom)
            {
                currentButton.BackColor = Color.FromArgb(45, 52, 54);
                currentButton.Font = new Font("Segoe UI", 10.5f, FontStyle.Regular);
            }
            currentButton = btn;
            currentButton.BackColor = Color.FromArgb(230, 126, 34);
            currentButton.Font = new Font("Segoe UI", 10.5f, FontStyle.Bold);
        }

        private void OpenChildForm(Form childForm, string title)
        {
            pnlContent.Controls.Clear();
            lblTitle.Text = title;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            pnlContent.Controls.Add(childForm);
            childForm.Show();
        }
    }
}