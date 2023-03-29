using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TKS_Thuc_Tap_Utility;
using TKS_Thuc_Tap_Entity.Danh_Muc;
using TKS_Thuc_Tap_Controller.Danh_Muc;

namespace TKS_Thuc_Tap_Web.Sample_Page.UserControl
{
    public partial class uc1312_Danh_Muc : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnXoa_Select_Click(object sender, EventArgs e)
        {
            CDM_Don_Vi_Tinh_Controller v_objCtrDVT = new CDM_Don_Vi_Tinh_Controller();
            try
            {
                List<object> Rows = grdData.GetSelectedFieldValues("Auto_ID", "Ten_Don_Vi_Tinh");

                int v_iCount = 0;
                foreach (object id in Rows)
                {
                    v_iCount++;
                    object[] v_arrID = (object[])id;

                    int v_iAuto_ID = CUtility.Convert_To_Int32(v_arrID[0]);
                    v_objCtrDVT.Delete_DM_Don_Vi_Tinh(v_iAuto_ID, CSession.Active_User_Name);

                }

                if (v_iCount <= 0)
                    throw new Exception("Xin vui lòng chọn danh sách cần xóa.");

                CCommonFunction.ShowMessage("Xóa danh sách được chọn thành công.");
                grdData.DataBind();

            }
            catch (Exception ex)
            {
                CCommonFunction.Set_Error_MessageBox(ex.Message);
            }
        }
    }
}