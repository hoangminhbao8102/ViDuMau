using BussinessLayer.DBAccess;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.MethodObjects
{
    public class SinhVienMethod : SqlDataProvider
    {
        public List<SinhVien> Select_All()
        {
            try
            {
                using (var db = GetContext())
                {
                    var ls = db.SinhViens.AsQueryable();
                    return ls?.ToList() ?? new List<SinhVien>();
                }
            }
            catch (Exception ex)
            {
                // Ghi log lỗi chi tiết để debug
                Console.WriteLine("Lỗi khi truy vấn danh sách sinh viên: " + ex.Message);
                return new List<SinhVien>();
            }
        }
        public  SinhVien Select_ID(int id)
        {
            using (var db = GetContext())
            {
                return db.SinhViens.FirstOrDefault(s => s.ID == id);
            }
        }
        public  List<SinhVien> Select_IDs(List<string> IDs)
        {
            using (var db = GetContext())
            {
                var ls = db.SinhViens.AsQueryable();
                if (ls != null && ls.Any())
                {
                    ls = ls.Where(s => IDs.Contains(s.ID.ToString()));
                    return ls.ToList();
                }
                return new List<SinhVien>();
            }
        }

        /// <summary>
        /// Day phuong thuc cho phep truy van du lieu theo 1 cot nap dp
        /// </summary>
        /// <param name="ColumnName">Ten cot</param>
        /// <param name="Value">Gia tri can truy van</param>
        /// <returns></returns>
        public  List<SinhVien> Select_By(string ColumnName, string Value)
        {
            using (var db = GetContext())
            {
                ColumnName = ColumnName.ToLower();
                Value = Value.ToLower();
                string sql = "Select * From SinhVien Where CONVERT(nvarchar," + ColumnName + ") = '" + Value + "'";
                var ls = db.SinhViens.SqlQuery(sql);
                if (ls != null && ls.Any()) return ls.ToList<SinhVien>();
                return new List<SinhVien>();
            }
        }
        public  List<SinhVien> Select_By(string ColumnName, string Value, int PageSize, int PageIndex, out int TotalRows)
        {
            TotalRows = 0;
            using (var db = GetContext())
            {
                ColumnName = ColumnName.ToLower();
                Value = Value.ToLower();
                string sql = "Select * From SinhVien Where CONVERT(nvarchar," + ColumnName + ") = '" + Value + "'";
                var ls = db.SinhViens.SqlQuery(sql);
                if (ls != null && ls.Any())
                {
                    TotalRows = ls.Count();
                    return ls.OrderByDescending(s => s.ID).Skip(PageSize * PageIndex).Take(PageSize).ToList<SinhVien>();
                }
                return new List<SinhVien>();
            }
        }
        public  int InsertUpdate(SinhVien obj)
        {
            using (var db = GetContext())
            {
                using (var db1 = GetContext())
                {
                    var find = db.SinhViens.FirstOrDefault(s => s.ID == obj.ID);
                    if (find != null) db1.Entry(obj).State = EntityState.Modified;
                    else obj = db1.SinhViens.Add(obj);
                    db1.SaveChanges();
                    return obj.ID;
                }
            }
        }
        public  void Delete(int id)
        {
            using (var db = GetContext())
            {
                var obj = db.SinhViens.FirstOrDefault(s => s.ID == id);
                if (obj != null)
                {
                    db.SinhViens.Remove(obj);
                    db.SaveChanges();
                }
            }
        }
        public  void Delete_IDs(List<string> IDs)
        {
            using (var db = GetContext())
            {
                var ls = db.SinhViens.AsQueryable();
                if (ls != null && ls.Any())
                {
                    ls = ls.Where(s => IDs.Contains(s.ID.ToString()));
                    foreach (var item in ls)
                        db.SinhViens.Remove(item);
                    db.SaveChanges();
                }
            }
        }
        public  List<SinhVien> Find_KeyWord(string Keyword, int PageSize, int PageIndex, out int TotalRows)
        {
            TotalRows = 0;
            using (var db = GetContext())
            {
                if (!string.IsNullOrWhiteSpace(Keyword))
                {
                    var obj = db.SinhViens.FirstOrDefault(s => s.ID.ToString().CompareTo(Keyword) == 0);
                    if (obj != null)
                    {
                        List<SinhVien> ls = new List<SinhVien>();
                        ls.Add(obj);
                        TotalRows = 1;
                        return ls;
                    }
                    var list = db.SinhViens.AsQueryable();
                    list = list.Where(s => s.ID.ToString().Contains(Keyword)
                    || s.MSSV.ToLower().Contains(Keyword)
                    || s.HoVaTen.ToLower().Contains(Keyword)
                    || s.Email.ToLower().Contains(Keyword)
                    || s.IDLop.ToString().Contains(Keyword)
                    || s.ID.ToString().Contains(Keyword)
                    );
                    if (list != null && list.Any())
                    {
                        TotalRows = list.Count();
                        return list.OrderByDescending(s => s.ID).Skip(PageSize * PageIndex).Take(PageSize).ToList();
                    }
                }
                else
                {
                    var list = db.SinhViens.AsQueryable();
                    if (list != null && list.Any())
                    {
                        TotalRows = list.Count();
                        return list.OrderByDescending(s => s.ID).Skip(PageSize * PageIndex).Take(PageSize).ToList();
                    }
                }
                return new List<SinhVien>();
            }
        }
        public  void Import(List<SinhVien> list)
        {
            using (var db = GetContext())
            {
                using (DbContextTransaction transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        db.SinhViens.AddRange(list);
                        db.SaveChanges();
                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                    }
                }
            }
        }
    }
}
