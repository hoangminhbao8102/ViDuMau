using BussinessLayer.DBAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.DLUSystem
{
    public class CTSVSubsystem : DLUSystem
    {
        //U1: Lay DS sinh vien theo lop
        /// <summary>
        /// U1: Chuc nang lay danh sach sinh vien theo ten cua mot lop hoc
        /// </summary>
        /// <param name="TenLop">Ten cua mot lop hoc</param>
        /// <returns></returns>
        public List<SinhVien> GetSinhVienByIDLopHoc(string TenLop)
        {
            //Kiem tra rang buoc gia tri 
            //Proxy Pattern
            return sinhVienMethod.Select_By("TenLop", TenLop);
            //Ghi log chuc nang o day
            //...
        }

        //U2: Tim kiem sinh vien theo MSSV

        //U3: Lay tat ca sinh vien

        /// <summary>
        /// U3: Chuc nang lay tat ca danh sach sinh vien
        /// </summary>
        /// <returns></returns>
        public List<SinhVien> GetAllSinhVien()
        {
            return sinhVienMethod.Select_All();
        }

        //U4: Lay thong tin 1 sinh vien theo MSSV

        public SinhVien GetSinhVienByMSSV(string MSSV)
        {
            //Can kiem tra MSSV co hop le hay khong?
            if(IsMSSV(MSSV))
                return sinhVienMethod.Select_By("MSSV", MSSV)[0];
            return null;
        }

        //...
    }
}
