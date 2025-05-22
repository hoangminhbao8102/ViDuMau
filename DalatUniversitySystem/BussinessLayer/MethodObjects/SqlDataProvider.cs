using BussinessLayer.DBAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.MethodObjects
{
    public class SqlDataProvider 
    {
        public dbCTSVEntities GetContext()
        {
            return new dbCTSVEntities();
        }
    }
}
