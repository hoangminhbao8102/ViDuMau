using BussinessLayer.MethodObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BussinessLayer.DLUSystem
{
    public class DLUSystem
    {
        //Khai bao cac doi tuong lien quan den he thong
        public LopHocMethod lopHocMethod = new LopHocMethod();
        public SinhVienMethod sinhVienMethod = new SinhVienMethod();

        public DLUSystem()
        {
            this.Init();
        }
        public void Init()
        {
            lopHocMethod = new LopHocMethod();
        }


        /// <summary>
        /// Kiem tra MSSV co hop le khong? MSSV : 7 ky tu so
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public bool IsMSSV(string mssv)
        {
            Regex regex = new Regex(@"^(\d{7})$");
            Match match = regex.Match(mssv);
            if (match.Success)
                return true;
            return false;
        }
    }
}
