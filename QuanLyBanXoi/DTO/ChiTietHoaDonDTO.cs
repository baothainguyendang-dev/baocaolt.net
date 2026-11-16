namespace QuanLyBanXoi.DTO
{
    public class ChiTietHoaDonDTO
    {
        public int MaCTHD { get; set; }
        public int MaHD { get; set; }
        public int MaMon { get; set; }
        public string TenMon { get; set; } = string.Empty; // Dùng để hiển thị lên bảng
        public int SoLuong { get; set; }
        public string ToppingKem { get; set; } = string.Empty;
        public decimal ThanhTien { get; set; }
    }
}