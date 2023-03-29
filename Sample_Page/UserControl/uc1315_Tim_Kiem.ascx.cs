using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TKS_Thuc_Tap_Utility;

namespace TKS_Thuc_Tap_Web.Sample_Page.UserControl
{
    public partial class uc1315_Tim_Kiem : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                dtmNgay_Bat_Dau.Date = DateTime.Now.AddDays(-30);
                dtmNgay_Ket_Thuc.Date = DateTime.Now;
            }
        }
    }
}