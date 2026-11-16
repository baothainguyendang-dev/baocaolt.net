using System.Data;
using QuanLyBanXoi.DAL;
using QuanLyBanXoi.DTO;

namespace QuanLyBanXoi.BUS
{
    public class ToppingBUS
    {
        private readonly ToppingDAL _dal = new ToppingDAL();

        public DataTable LayDanhSachTopping() => _dal.GetAll();

        public (bool Success, string Message) ThemTopping(ToppingDTO tp)
        {
            if (string.IsNullOrWhiteSpace(tp.TenTopping))
                return (false, "Tên topping không được để trống!");
            if (tp.DonGia <= 0)
                return (false, "Đơn giá topping phải lớn hơn 0!");

            bool ok = _dal.Insert(tp);
            return ok ? (true, "Thêm topping thành công!") : (false, "Thêm topping thất bại!");
        }

        public (bool Success, string Message) SuaTopping(ToppingDTO tp)
        {
            if (tp.MaTopping <= 0)
                return (false, "Vui lòng chọn topping cần sửa!");
            if (string.IsNullOrWhiteSpace(tp.TenTopping) || tp.DonGia <= 0)
                return (false, "Dữ liệu không hợp lệ!");

            bool ok = _dal.Update(tp);
            return ok ? (true, "Cập nhật topping thành công!") : (false, "Cập nhật thất bại!");
        }

        public (bool Success, string Message) XoaTopping(int maTopping)
        {
            if (maTopping <= 0)
                return (false, "Vui lòng chọn topping cần xóa!");

            bool ok = _dal.Delete(maTopping);
            return ok ? (true, "Xóa topping thành công!") : (false, "Xóa thất bại!");
        }
    }
}