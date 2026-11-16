using System.Data;
using Microsoft.Data.SqlClient;
using QuanLyBanXoi.DAL;
using QuanLyBanXoi.DTO;

namespace QuanLyBanXoi.BUS
{
    public class MonXoiBUS
    {
        private readonly MonXoiDAL _dal = new MonXoiDAL();

        public DataTable LayDanhSachMon() => _dal.GetAll();

        public (bool Success, string Message) ThemMon(MonXoiDTO mon)
        {
            if (string.IsNullOrWhiteSpace(mon.TenMon))
                return (false, "Tên món xôi không được để trống!");
            if (mon.DonGia <= 0)
                return (false, "Đơn giá phải lớn hơn 0!");

            bool ok = _dal.Insert(mon);
            return ok ? (true, "Thêm món thành công!") : (false, "Thêm món thất bại!");
        }

        public (bool Success, string Message) SuaMon(MonXoiDTO mon)
        {
            if (mon.MaMon <= 0)
                return (false, "Vui lòng chọn món cần sửa trên bảng!");
            if (string.IsNullOrWhiteSpace(mon.TenMon))
                return (false, "Tên món xôi không được để trống!");
            if (mon.DonGia <= 0)
                return (false, "Đơn giá phải lớn hơn 0!");

            bool ok = _dal.Update(mon);
            return ok ? (true, "Cập nhật thành công!") : (false, "Cập nhật thất bại!");
        }

        public (bool Success, string Message) XoaMon(int maMon)
        {
            if (maMon <= 0)
                return (false, "Vui lòng chọn món cần xóa trên bảng!");
            try
            {
                bool ok = _dal.Delete(maMon);
                return ok ? (true, "Xóa món thành công!") : (false, "Xóa thất bại!");
            }
            catch (SqlException)
            {
                return (false, "Món này đã có trong hóa đơn, không thể xóa! Hãy đổi trạng thái sang 'Hết hàng'.");
            }
        }
    }
}