using System.Data;
using Microsoft.Data.SqlClient;
using QuanLyBanXoi.DTO;

namespace QuanLyBanXoi.DAL
{
    public class MonXoiDAL
    {
        private readonly DBConnect _db = new DBConnect();

        public DataTable GetAll()
        {
            string query = "SELECT MaMon, TenMon, LoaiMon, DonGia, TrangThai FROM MonXoi";
            return _db.ExecuteQuery(query);
        }

        public bool Insert(MonXoiDTO mon)
        {
            string query = "INSERT INTO MonXoi (TenMon, LoaiMon, DonGia, TrangThai) VALUES (@TenMon, @LoaiMon, @DonGia, @TrangThai)";
            SqlParameter[] parameters = {
                new SqlParameter("@TenMon", mon.TenMon),
                new SqlParameter("@LoaiMon", mon.LoaiMon),
                new SqlParameter("@DonGia", mon.DonGia),
                new SqlParameter("@TrangThai", mon.TrangThai)
            };
            return _db.ExecuteNonQuery(query, parameters) > 0;
        }

        public bool Update(MonXoiDTO mon)
        {
            string query = "UPDATE MonXoi SET TenMon = @TenMon, LoaiMon = @LoaiMon, DonGia = @DonGia, TrangThai = @TrangThai WHERE MaMon = @MaMon";
            SqlParameter[] parameters = {
                new SqlParameter("@MaMon", mon.MaMon),
                new SqlParameter("@TenMon", mon.TenMon),
                new SqlParameter("@LoaiMon", mon.LoaiMon),
                new SqlParameter("@DonGia", mon.DonGia),
                new SqlParameter("@TrangThai", mon.TrangThai)
            };
            return _db.ExecuteNonQuery(query, parameters) > 0;
        }

        public bool Delete(int maMon)
        {
            string query = "DELETE FROM MonXoi WHERE MaMon = @MaMon";
            SqlParameter[] parameters = {
                new SqlParameter("@MaMon", maMon)
            };
            return _db.ExecuteNonQuery(query, parameters) > 0;
        }
    }
}