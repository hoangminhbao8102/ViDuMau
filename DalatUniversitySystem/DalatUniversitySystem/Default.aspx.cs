using BussinessLayer.DLUSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DalatUniversitySystem
{
    public partial class _Default : Page
    {
        CTSVSubsystem cTSVSubsystem = new CTSVSubsystem();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.LoadSinhVien();
            }
        }

        private void LoadSinhVien()
        {
            this.GridView1.DataSource = cTSVSubsystem.GetAllSinhVien();
            this.GridView1.DataBind();
        }
    }
}