namespace QuanLyBanXoi.DTO
{
    public class MonXoiDTO
    {
        public int MaMon { get; set; }
        public string TenMon { get; set; } = string.Empty;
        public string LoaiMon { get; set; } = string.Empty;
        public decimal DonGia { get; set; }
        public string TrangThai { get; set; } = "Còn hàng";
    }
}