using System.Data;
using Microsoft.Data.SqlClient;
using QuanLyBanXoi.DTO;

namespace QuanLyBanXoi.DAL
{
    public class ToppingDAL
    {
        private readonly DBConnect _db = new DBConnect();

        public DataTable GetAll()
        {
            string query = "SELECT MaTopping, TenTopping, DonGia, TrangThai FROM Topping";
            return _db.ExecuteQuery(query);
        }

        public bool Insert(ToppingDTO tp)
        {
            string query = "INSERT INTO Topping (TenTopping, DonGia, TrangThai) VALUES (@TenTopping, @DonGia, @TrangThai)";
            SqlParameter[] parameters = {
                new SqlParameter("@TenTopping", tp.TenTopping),
                new SqlParameter("@DonGia", tp.DonGia),
                new SqlParameter("@TrangThai", tp.TrangThai)
            };
            return _db.ExecuteNonQuery(query, parameters) > 0;
        }

        public bool Update(ToppingDTO tp)
        {
            string query = "UPDATE Topping SET TenTopping = @TenTopping, DonGia = @DonGia, TrangThai = @TrangThai WHERE MaTopping = @MaTopping";
            SqlParameter[] parameters = {
                new SqlParameter("@MaTopping", tp.MaTopping),
                new SqlParameter("@TenTopping", tp.TenTopping),
                new SqlParameter("@DonGia", tp.DonGia),
                new SqlParameter("@TrangThai", tp.TrangThai)
            };
            return _db.ExecuteNonQuery(query, parameters) > 0;
        }

        public bool Delete(int maTopping)
        {
            string query = "DELETE FROM Topping WHERE MaTopping = @MaTopping";
            SqlParameter[] parameters = {
                new SqlParameter("@MaTopping", maTopping)
            };
            return _db.ExecuteNonQuery(query, parameters) > 0;
        }
    }
}