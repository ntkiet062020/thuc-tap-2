using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TKS_Thuc_Tap_Utility;
using TKS_Thuc_Tap_Entity.Danh_Muc;
using TKS_Thuc_Tap_Controller.Danh_Muc;

namespace TKS_Thuc_Tap_Web.Danh_Muc.UserControl
{
    public partial class uc2003_Mau_Column : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnXoa_Select_Click(object sender, EventArgs e)
        {
            CDM_Mau_Column_Controller v_objCtrDVT = new CDM_Mau_Column_Controller();
            try
            {
                List<object> Rows = grdData.GetSelectedFieldValues("Auto_ID", "Mau_So_Mau");

                int v_iCount = 0;
                foreach (object id in Rows)
                {
                    v_iCount++;
                    object[] v_arrID = (object[])id;

                    int v_iAuto_ID = CUtility.Convert_To_Int32(v_arrID[0]);
                    v_objCtrDVT.Delete_DM_Mau_Column(v_iAuto_ID, CSession.Active_User_Name);

                }

                if (v_iCount <= 0)
                    throw new Exception("Xin vui lòng chọn danh sách cần xóa.");

                CCommonFunction.ShowMessage("Xóa danh sách được chọn thành công.");
                grdData.DataBind();

            }
            catch (Exception ex)
            {
                CCommonFunction.ShowWarning(ex.Message);
            }
        }
    }
}