using BussinessLayer.DBAccess;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.MethodObjects
{
    public class LopHocMethod : SqlDataProvider
    {
        public List<LopHoc> Select_All()
        {
            using (var db = GetContext())
            {
                var ls = db.LopHocs.AsQueryable();
                if (ls != null && ls.Any())
                    return ls.ToList();
                return new List<LopHoc>();
            }
        }
        public LopHoc Select_ID(int id)
        {
            using (var db = GetContext())
            {
                return db.LopHocs.FirstOrDefault(s => s.ID == id);
            }
        }
        public List<LopHoc> Select_IDs(List<string> IDs)
        {
            using (var db = GetContext())
            {
                var ls = db.LopHocs.AsQueryable();
                if (ls != null && ls.Any())
                {
                    ls = ls.Where(s => IDs.Contains(s.ID.ToString()));
                    return ls.ToList();
                }
                return new List<LopHoc>();
            }
        }
        public List<LopHoc> Select_By(string ColumnName, string Value)
        {
            using (var db = GetContext())
            {
                ColumnName = ColumnName.ToLower();
                Value = Value.ToLower();
                string sql = "Select * From LopHoc Where CONVERT(nvarchar," + ColumnName + ") = '" + Value + "'";
                var ls = db.LopHocs.SqlQuery(sql);
                if (ls != null && ls.Any()) return ls.ToList<LopHoc>();
                return new List<LopHoc>();
            }
        }
        public List<LopHoc> Select_By(string ColumnName, string Value, int PageSize, int PageIndex, out int TotalRows)
        {
            TotalRows = 0;
            using (var db = GetContext())
            {
                ColumnName = ColumnName.ToLower();
                Value = Value.ToLower();
                string sql = "Select * From LopHoc Where CONVERT(nvarchar," + ColumnName + ") = '" + Value + "'";
                var ls = db.LopHocs.SqlQuery(sql);
                if (ls != null && ls.Any())
                {
                    TotalRows = ls.Count();
                    return ls.OrderByDescending(s => s.ID).Skip(PageSize * PageIndex).Take(PageSize).ToList<LopHoc>();
                }
                return new List<LopHoc>();
            }
        }
        public int InsertUpdate(LopHoc obj)
        {
            using (var db = GetContext())
            {
                using (var db1 = GetContext())
                {
                    var find = db.LopHocs.FirstOrDefault(s => s.ID == obj.ID);
                    if (find != null) db1.Entry(obj).State = EntityState.Modified;
                    else obj = db1.LopHocs.Add(obj);
                    db1.SaveChanges();
                    return obj.ID;
                }
            }
        }
        public void Delete(int id)
        {
            using (var db = GetContext())
            {
                var obj = db.LopHocs.FirstOrDefault(s => s.ID == id);
                if (obj != null)
                {
                    db.LopHocs.Remove(obj);
                    db.SaveChanges();
                }
            }
        }
        public void Delete_IDs(List<string> IDs)
        {
            using (var db = GetContext())
            {
                var ls = db.LopHocs.AsQueryable();
                if (ls != null && ls.Any())
                {
                    ls = ls.Where(s => IDs.Contains(s.ID.ToString()));
                    foreach (var item in ls)
                        db.LopHocs.Remove(item);
                    db.SaveChanges();
                }
            }
        }
        public List<LopHoc> Find_KeyWord(string Keyword, int PageSize, int PageIndex, out int TotalRows)
        {
            TotalRows = 0;
            using (var db = GetContext())
            {
                if (!string.IsNullOrWhiteSpace(Keyword))
                {
                    var obj = db.LopHocs.FirstOrDefault(s => s.ID.ToString().CompareTo(Keyword) == 0);
                    if (obj != null)
                    {
                        List<LopHoc> ls = new List<LopHoc>();
                        ls.Add(obj);
                        TotalRows = 1;
                        return ls;
                    }
                    var list = db.LopHocs.AsQueryable();
                    list = list.Where(s => s.ID.ToString().Contains(Keyword)
                    || s.TenLop.ToLower().Contains(Keyword)
                    || s.GVCN.ToLower().Contains(Keyword)
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
                    var list = db.LopHocs.AsQueryable();
                    if (list != null && list.Any())
                    {
                        TotalRows = list.Count();
                        return list.OrderByDescending(s => s.ID).Skip(PageSize * PageIndex).Take(PageSize).ToList();
                    }
                }
                return new List<LopHoc>();
            }
        }
        public void Import(List<LopHoc> list)
        {
            using (var db = GetContext())
            {
                using (DbContextTransaction transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        db.LopHocs.AddRange(list);
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
