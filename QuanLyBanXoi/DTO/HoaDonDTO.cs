using System;

namespace QuanLyBanXoi.DTO
{
    public class HoaDonDTO
    {
        public int MaHD { get; set; }
        public DateTime NgayLap { get; set; } = DateTime.Now;
        public string TenKhach { get; set; } = "Khách lẻ";
        public decimal TongTien { get; set; }
        public string? GhiChu { get; set; }
    }
}