namespace QuanLyBanXoi.DTO
{
    public class ToppingDTO
    {
        public int MaTopping { get; set; }
        public string TenTopping { get; set; } = string.Empty;
        public decimal DonGia { get; set; }
        public string TrangThai { get; set; } = "Còn hàng";
    }
}