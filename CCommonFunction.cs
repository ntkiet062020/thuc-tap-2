using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Web.Mail;
using System.Net.Mail;
using System.Globalization;
using System.Text;
using System.Collections.Generic;
using DevExpress.Web;
using DevExpress.Web.ASPxTreeList;
using TKS_Thuc_Tap_Utility;
using TKS_Thuc_Tap_Entity.Admin;
using TKS_Thuc_Tap_Controller.Admin;
using TKS_Thuc_Tap_Entity.Danh_Muc;
using TKS_Thuc_Tap_Controller.Danh_Muc;

namespace TKS_Thuc_Tap_Web
{
    public class MyHyperlinkTemplate : ITemplate
    {
        public string m_strLink = "";
        public string m_strField_Name = "";
        public string m_strField_Parameter = "";

        public void InstantiateIn(Control container)
        {
            GridViewDataItemTemplateContainer gridContainer = (GridViewDataItemTemplateContainer)container;
            string v_strData = CUtility.Convert_To_String(gridContainer.Grid.GetRowValues(gridContainer.VisibleIndex, m_strField_Parameter));

            if (v_strData != "" && v_strData != "0")
            {
                string v_strURL = m_strLink + "&" + m_strField_Parameter + "=" + v_strData;
                HtmlGenericControl link = new HtmlGenericControl("a");
                link.Attributes.Add("href", "javascript:openWin('" + v_strURL + "',1000,600)");
                link.InnerHtml = gridContainer.Grid.GetRowValues(gridContainer.VisibleIndex, m_strField_Name).ToString() + "&nbsp;<span style=\"color:blue;\"><i class='fa fa-level-up'></i></span>";
                container.Controls.Add(link);
            }
        }
    }

    public class CCommonFunction
    {
        public static string Create_Random_Session_ID()
        {
            const string chars = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            Random rand = new Random();
            int LENGTH = 30;

            StringBuilder v_strRes = new StringBuilder();

            for (int i = 0; i < LENGTH; i++)
            {
                // Lấy kí tự ngẫu nhiên từ mảng chars
                string str = chars[rand.Next(chars.Length)].ToString();
                v_strRes.Append(str);
            }

            return v_strRes.ToString();
        }


        /// Scrip dùng để Link tới đường dẫn trong Web
        /// </summary>
        /// <param name="p_strDateTime">Chuỗi cần convert</param>
        /// <returns></returns>
        public static void OpenWindow(string p_strUrl)
        {
            string v_strScript = "window.open('" + p_strUrl + "', '', \"toolbar=no,location=no, " +
                "directories=no,status=no,menubar=no,scrollbars=yes,resizable=no,width=800,height=600\")";
            HttpContext.Current.Response.Write("<script language='javascript'>" + v_strScript + "</script>");
        }

        /// <summary>
        /// Mở 1 page mới ở dạng window thường
        /// </summary>
        /// <param name="p_strUrl"></param>
        /// <param name="p_intWidth"></param>
        /// <param name="p_intHeight"></param>
        public static void RedirectLink(string p_strUrl)
        {
            HttpContext.Current.Response.Redirect(p_strUrl, false);
        }

        /// <summary>
        /// Mở 1 page mới ở dạng window thường
        /// </summary>
        /// <param name="p_strUrl"></param>
        /// <param name="p_intWidth"></param>
        /// <param name="p_intHeight"></param>
        public static void RedirectLink_javascript(string p_strUrl)
        {
            HttpContext.Current.Response.Write("<script language='javascript'>document.location='" + p_strUrl + "'</script>");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_objFunc"></param>
        /// <returns></returns>
        public static string GetFuncUrl(CChuc_Nang p_objFunc)
        {
            string p_strUrl = p_objFunc.Func_URL;

            if (p_strUrl != "")
            {
                if (p_strUrl.Contains("?"))
                    p_strUrl += "&f=" + p_objFunc.Auto_ID.ToString();
                else
                    p_strUrl += "?f=" + p_objFunc.Auto_ID.ToString();
            }

            return p_strUrl;
        }

        public static void ShowWarning(string p_strMessage)
        {
            MessageBox.Show(p_strMessage);
        }

        public static void ShowMessage(string p_strMessage)
        {
            MessageBox.Show(p_strMessage);
        }

        /// <summary>
        /// Lấy quyền
        /// </summary>
        /// <param name="p_iFuncID"></param>
        /// <param name="p_strUrl"></param>
        /// <returns></returns>
        public static bool CheckPermission(int p_iFuncID, string p_strUrl)
        {
            bool v_bRes = false;
            CThanh_Vien_Controller v_objCtrThanh_Vien = new CThanh_Vien_Controller();
            CNhom_Quyen_Controller v_objCtrNhom_Quyen = new CNhom_Quyen_Controller();
            CChuc_Nang_Controller v_objCtrChuc_Nang = new CChuc_Nang_Controller();

            try
            {
                // Lấy danh sách nhóm quyền mà user có
                IList<CQuan_Ly_Thanh_Vien> v_arrUR = v_objCtrThanh_Vien.F1002_List_Quan_Ly_Thanh_Vien_By_Ma_Thanh_Vien(CSession.Active_User.Auto_ID);

                // Lấy dictionary function dựa trên các nhóm quyền
                IDictionary<int, string> v_dicFunc = new Dictionary<int, string>();
                foreach (CQuan_Ly_Thanh_Vien v_objQuan_Ly_Thanh_Vien in v_arrUR)
                {
                    IList<CPhan_Quyen_Chuc_Nang> v_arrFR = v_objCtrNhom_Quyen.F1004_List_Phan_Quyen_Chuc_Nang_By_Ma_Nhom_Quyen(v_objQuan_Ly_Thanh_Vien.Ma_Nhom_Quyen);
                    foreach (CPhan_Quyen_Chuc_Nang v_objFR in v_arrFR)
                        if (v_objFR.Permission_String[0] == '1')
                        {
                            if (!v_dicFunc.ContainsKey(v_objFR.Ma_Chuc_Nang))
                                v_dicFunc.Add(v_objFR.Ma_Chuc_Nang, v_objFR.Func_URL);
                        } // end for
                } // end for

                // Kiểm tra ID và Url
                if (v_dicFunc.ContainsKey(p_iFuncID))
                {
                    string v_str = v_dicFunc[p_iFuncID];
                    v_str = v_str.Replace("~/", "");

                    if (p_strUrl.ToUpper().Contains(v_str.ToUpper()))
                        v_bRes = true;
                }
            }

            catch (Exception ex)
            {
                CCommonFunction.ShowWarning(ex.Message);
            }

            return v_bRes;
        }

        /// <summary>
        /// Loại các column new, edit, delete nếu user hiện tại không có quyền
        /// </summary>
        /// <param name="p_grdData"></param>
        public static void Grid_Permission(DevExpress.Web.ASPxGridView p_grdData, Control p_objDelete_Button,
            int p_iFuncID)
        {
            CThanh_Vien_Controller v_objCtrThanh_Vien = new CThanh_Vien_Controller();
            CNhom_Quyen_Controller v_objCtrNhom_Quyen = new CNhom_Quyen_Controller();
            CChuc_Nang_Controller v_objCtrChuc_Nang = new CChuc_Nang_Controller();

            string v_strPer = "1000";

            // Lấy permission string
            try
            {
                // Lấy danh sách nhóm quyền mà user có
                IList<CQuan_Ly_Thanh_Vien> v_arrUR = v_objCtrThanh_Vien.F1002_List_Quan_Ly_Thanh_Vien_By_Ma_Thanh_Vien(CSession.Active_User.Auto_ID);

                // Lấy dictionary function dựa trên các nhóm quyền
                IDictionary<int, string> v_dicFunc = new Dictionary<int, string>();
                foreach (CQuan_Ly_Thanh_Vien v_objQuan_Ly_Thanh_Vien in v_arrUR)
                {
                    IList<CPhan_Quyen_Chuc_Nang> v_arrFR = v_objCtrNhom_Quyen.F1004_List_Phan_Quyen_Chuc_Nang_By_Ma_Nhom_Quyen(v_objQuan_Ly_Thanh_Vien.Ma_Nhom_Quyen);
                    foreach (CPhan_Quyen_Chuc_Nang v_objFR in v_arrFR)
                    {
                        if (v_objFR.Ma_Chuc_Nang == p_iFuncID)
                        {
                            if (v_objFR.Permission_String[1] == '1')
                            {
                                v_strPer = v_strPer.Remove(1, 1);
                                v_strPer = v_strPer.Insert(1, "1");
                            } // end for

                            if (v_objFR.Permission_String[2] == '1')
                            {
                                v_strPer = v_strPer.Remove(2, 1);
                                v_strPer = v_strPer.Insert(2, "1");
                            } // end for

                            if (v_objFR.Permission_String[3] == '1')
                            {
                                v_strPer = v_strPer.Remove(3, 1);
                                v_strPer = v_strPer.Insert(3, "1");
                            } // end for
                        }
                    }
                } // end for

                // Loại các column ra
                foreach (DevExpress.Web.GridViewColumn v_objCol in p_grdData.Columns)
                {
                    if (v_objCol.GetType().Name == "GridViewCommandColumn")
                    {
                        DevExpress.Web.GridViewCommandColumn v_button = (DevExpress.Web.GridViewCommandColumn)v_objCol;

                        if (v_button.ShowNewButton && v_strPer[1] == '0')
                            v_objCol.Visible = false;

                        if (v_button.ShowEditButton && v_strPer[2] == '0')
                            v_objCol.Visible = false;

                        if (v_button.ShowDeleteButton && v_strPer[3] == '0')
                        {
                            v_objCol.Visible = false;
                            if (p_objDelete_Button != null)
                                p_objDelete_Button.Visible = false;
                        }

                    }
                    //Xét mặc định cho Filter là Contains dùng cho gridview
                    if (v_objCol.GetType().Name == "GridViewDataTextColumn")
                    {
                        GridViewDataTextColumn v_colText = (GridViewDataTextColumn)v_objCol;
                        v_colText.Settings.AutoFilterCondition = AutoFilterCondition.Contains;
                    }
                }
            }

            catch (Exception ex)
            {
                CCommonFunction.ShowWarning(ex.Message);
            }
        }

        public static void Set_AutoFilter_Contain_In_Grid(DevExpress.Web.ASPxGridView p_grdData)
        {
            foreach (DevExpress.Web.GridViewColumn v_objCol in p_grdData.Columns)
            {
                //Xét mặc định cho Filter là Contains dùng cho gridview
                if (v_objCol.GetType().Name == "GridViewDataTextColumn")
                {
                    GridViewDataTextColumn v_colText = (GridViewDataTextColumn)v_objCol;
                    v_colText.Settings.AutoFilterCondition = AutoFilterCondition.Contains;
                }
            }
        }

        /// <summary>
        /// Loại các column new, edit, delete nếu user hiện tại không có quyền
        /// </summary>
        /// <param name="p_grdData"></param>
        public static void TreeList_Permission(DevExpress.Web.ASPxTreeList.ASPxTreeList p_grdData, int p_iFuncID)
        {
            CThanh_Vien_Controller v_objCtrThanh_Vien = new CThanh_Vien_Controller();
            CNhom_Quyen_Controller v_objCtrNhom_Quyen = new CNhom_Quyen_Controller();
            CChuc_Nang_Controller v_objCtrChuc_Nang = new CChuc_Nang_Controller();

            string v_strPer = "1000";

            // Lấy permission string
            try
            {
                // Lấy danh sách nhóm quyền mà user có
                IList<CQuan_Ly_Thanh_Vien> v_arrUR = v_objCtrThanh_Vien.F1002_List_Quan_Ly_Thanh_Vien_By_Ma_Thanh_Vien(CSession.Active_User.Auto_ID);

                // Lấy dictionary function dựa trên các nhóm quyền
                IDictionary<int, string> v_dicFunc = new Dictionary<int, string>();
                foreach (CQuan_Ly_Thanh_Vien v_objQuan_Ly_Thanh_Vien in v_arrUR)
                {
                    IList<CPhan_Quyen_Chuc_Nang> v_arrFR = v_objCtrNhom_Quyen.F1004_List_Phan_Quyen_Chuc_Nang_By_Ma_Nhom_Quyen(v_objQuan_Ly_Thanh_Vien.Ma_Nhom_Quyen);
                    foreach (CPhan_Quyen_Chuc_Nang v_objFR in v_arrFR)
                    {
                        if (v_objFR.Ma_Chuc_Nang == p_iFuncID)
                        {
                            if (v_objFR.Permission_String[1] == '1')
                            {
                                v_strPer = v_strPer.Remove(1, 1);
                                v_strPer = v_strPer.Insert(1, "1");
                            } // end for

                            if (v_objFR.Permission_String[2] == '1')
                            {
                                v_strPer = v_strPer.Remove(2, 1);
                                v_strPer = v_strPer.Insert(2, "1");
                            } // end for

                            if (v_objFR.Permission_String[3] == '1')
                            {
                                v_strPer = v_strPer.Remove(3, 1);
                                v_strPer = v_strPer.Insert(3, "1");
                            } // end for
                        }
                    }
                } // end for

                // Loại các column ra
                foreach (DevExpress.Web.ASPxTreeList.TreeListColumn v_objCol in p_grdData.Columns)
                {
                    if (v_objCol.GetType().Name == "TreeListCommandColumn")
                    {
                        DevExpress.Web.ASPxTreeList.TreeListCommandColumn v_button = (DevExpress.Web.ASPxTreeList.TreeListCommandColumn)v_objCol;

                        if (v_button.NewButton.Visible && v_strPer[1] == '0')
                            v_objCol.Visible = false;

                        if (v_button.EditButton.Visible && v_strPer[2] == '0')
                            v_objCol.Visible = false;

                        if (v_button.DeleteButton.Visible && v_strPer[3] == '0')
                            v_objCol.Visible = false;
                    }
                }
            }

            catch (Exception ex)
            {
                CCommonFunction.ShowWarning(ex.Message);
            }
        }

        /// <summary>
        /// Execute script
        /// </summary>
        /// <param name="p_strScript"></param>
        public static void ExecuteJSScript(string p_strScript)
        {
            HttpContext.Current.Response.Write("<script language='javascript' type='text/javascript'>" + p_strScript + "</script>");
        }

        public static bool Is_Valid_Email(string inputEmail)
        {
            if (inputEmail.Length > 0)
            {
                string strRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                      @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                      @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
                Regex re = new Regex(strRegex);
                if (re.IsMatch(inputEmail))
                    return (true);
                else
                    return (false);
            }
            else
                return (true);
        }

        public static void Format_DataItem(IDictionary<string, CDM_Drill_Down> p_dicDrill, string p_strField_Name, DevExpress.Web.ASPxGridView p_grdData)
        {
            if (p_dicDrill.ContainsKey(p_strField_Name.ToLower()))
            {
                CDM_Drill_Down v_objDrill = p_dicDrill[p_strField_Name.ToLower()];

                MyHyperlinkTemplate v_objLink = new MyHyperlinkTemplate();
                v_objLink.m_strField_Name = p_strField_Name;
                v_objLink.m_strLink = v_objDrill.Link_URL + "?f=" + v_objDrill.Func_ID;
                v_objLink.m_strField_Parameter = v_objDrill.Parameter_Field;

                ((GridViewDataColumn)p_grdData.Columns[p_strField_Name]).DataItemTemplate = v_objLink;
            }
        }

        public static void Format_Mau(IDictionary<string, CDM_Mau_Column> p_dicColor, string p_strField_Name, DevExpress.Web.GridViewColumn p_col)
        {
            if (p_dicColor.ContainsKey(p_strField_Name.ToLower()))
                p_col.CellStyle.BackColor = Color.FromName(p_dicColor[p_strField_Name.ToLower()].Ma_So_Mau);
        }

        public static void Format_Style_Grid(DevExpress.Web.ASPxGridView p_grdData)
        {
            CDM_Drill_Down_Controller v_objCtrDrill_Down = new CDM_Drill_Down_Controller();
            CDM_Mau_Column_Controller v_objCtrColor = new CDM_Mau_Column_Controller();

            // Danh sách drill down
            IList<CDM_Drill_Down> v_arrDrill = v_objCtrDrill_Down.List_DM_Drill_Down();
            IDictionary<string, CDM_Drill_Down> v_dicDrill = new Dictionary<string, CDM_Drill_Down>();
            foreach (CDM_Drill_Down v_objDrill in v_arrDrill)
            {
                if (!v_dicDrill.ContainsKey(v_objDrill.Field_Name.ToLower()))
                    v_dicDrill.Add(v_objDrill.Field_Name.ToLower(), v_objDrill);
            }

            // Danh sách định nghĩa màu column
            IList<CDM_Mau_Column> v_arrColor = v_objCtrColor.List_DM_Mau_Column();
            IDictionary<string, CDM_Mau_Column> v_dicColor = new Dictionary<string, CDM_Mau_Column>();
            foreach (CDM_Mau_Column v_objColor in v_arrColor)
                if (!v_dicColor.ContainsKey(v_objColor.Field_Name.ToLower()))
                    v_dicColor.Add(v_objColor.Field_Name.ToLower(), v_objColor);

            foreach (DevExpress.Web.GridViewColumn v_objCol in p_grdData.AllColumns)
            {
                if (v_objCol.GetType().Name == "GridViewDataTextColumn")
                {
                    GridViewDataTextColumn v_colText = (GridViewDataTextColumn)v_objCol;
                    Format_Mau(v_dicColor, v_colText.FieldName, v_colText);
                    Format_DataItem(v_dicDrill, v_colText.FieldName, p_grdData);
                }

                if (v_objCol.GetType().Name == "GridViewDataSpinEditColumn")
                {

                    GridViewDataSpinEditColumn v_colSpin = (GridViewDataSpinEditColumn)v_objCol;
                    Format_Mau(v_dicColor, v_colSpin.FieldName, v_colSpin);
                }

                if (v_objCol.GetType().Name == "GridViewDataDateColumn")
                {
                    GridViewDataDateColumn v_colDate = (GridViewDataDateColumn)v_objCol;
                    Format_Mau(v_dicColor, v_colDate.FieldName, v_colDate);
                }

                if (v_objCol.GetType().Name == "GridViewDataComboBoxColumn")
                {
                    GridViewDataComboBoxColumn v_colCombo = (GridViewDataComboBoxColumn)v_objCol;
                    Format_Mau(v_dicColor, v_colCombo.FieldName, v_colCombo);
                }
            }
        }

        public static void Format_Grid(DevExpress.Web.ASPxGridView p_grdData)
        {
            bool v_bShow_Footer = false;

            //Xét mặc định cho Filter là Contains dùng cho gridview
            foreach (DevExpress.Web.GridViewColumn v_objCol in p_grdData.AllColumns)
            {
                if (v_objCol.GetType().Name == "GridViewDataTextColumn")
                {
                    GridViewDataTextColumn v_colText = (GridViewDataTextColumn)v_objCol;
                    v_colText.Settings.AutoFilterCondition = AutoFilterCondition.Contains;
                    v_colText.PropertiesEdit.EncodeHtml = false;

                    if (v_colText.CellStyle.HorizontalAlign == HorizontalAlign.NotSet)
                    {
                        if (v_colText.FieldName.ToLower().Contains("ghi_chu") || v_colText.FieldName.ToLower().Contains("mo_ta"))
                            v_colText.CellStyle.HorizontalAlign = HorizontalAlign.Left;
                        else
                            v_colText.CellStyle.HorizontalAlign = HorizontalAlign.Center;
                    }
                }
            }


            //clear các summary
            foreach (DevExpress.Web.GridViewColumn v_objCol in p_grdData.Columns)
            {
                if (v_objCol.GetType().Name == "GridViewDataSpinEditColumn")
                {
                    p_grdData.TotalSummary.Clear();
                    break;
                }
            }

            // Format cho các column spin edit
            foreach (DevExpress.Web.GridViewColumn v_objCol in p_grdData.AllColumns)
            {
                if (v_objCol.GetType().Name == "GridViewDataSpinEditColumn")
                {

                    GridViewDataSpinEditColumn v_colSpin = (GridViewDataSpinEditColumn)v_objCol;
                    v_colSpin.PropertiesSpinEdit.Increment = 0;
                    v_colSpin.PropertiesSpinEdit.DisplayFormatString = CCommonData.Number_Format_String;
                    v_colSpin.PropertiesSpinEdit.SpinButtons.Visible = false;
                    v_colSpin.PropertiesSpinEdit.SpinButtons.ShowIncrementButtons = false;

                    ASPxSummaryItem v_objSummary = new ASPxSummaryItem();
                    v_objSummary.DisplayFormat = "{0:" + CCommonData.Number_Format_String + "}";
                    v_objSummary.FieldName = v_colSpin.FieldName;
                    v_objSummary.ShowInColumn = v_colSpin.Name;
                    v_objSummary.ShowInGroupFooterColumn = v_colSpin.Name;
                    v_objSummary.SummaryType = DevExpress.Data.SummaryItemType.Sum;

                    p_grdData.TotalSummary.Add(v_objSummary);
                    v_bShow_Footer = true;
                }
            }

            // Format cho các DateTime Edit
            foreach (DevExpress.Web.GridViewColumn v_objCol in p_grdData.AllColumns)
            {
                if (v_objCol.GetType().Name == "GridViewDataDateColumn")
                {
                    GridViewDataDateColumn v_colDate = (GridViewDataDateColumn)v_objCol;
                    if (v_colDate.PropertiesDateEdit.DisplayFormatString == "" 
                        || v_colDate.PropertiesDateEdit.DisplayFormatString == "g"
                        || v_colDate.PropertiesDateEdit.DisplayFormatString == "d")
                    {
                        v_colDate.PropertiesDateEdit.DisplayFormatString = CCommonData.DateTime_Format_String;
                        v_colDate.PropertiesDateEdit.EditFormatString = CCommonData.DateTime_Format_String;
                    }

                    v_colDate.CellStyle.HorizontalAlign = HorizontalAlign.Center;

                    if (v_colDate.Width.Value < 90)
                        v_colDate.Width = new Unit(90, UnitType.Pixel);
                }
            }

            // Format cho các combo box Edit
            foreach (DevExpress.Web.GridViewColumn v_objCol in p_grdData.AllColumns)
            {
                if (v_objCol.GetType().Name == "GridViewDataComboBoxColumn")
                {
                    GridViewDataComboBoxColumn v_colCombo = (GridViewDataComboBoxColumn)v_objCol;
                    v_colCombo.PropertiesComboBox.IncrementalFilteringMode = DevExpress.Web.IncrementalFilteringMode.Contains;
                                    }
            }

            // Format cho các command column
            foreach (DevExpress.Web.GridViewColumn v_objCol in p_grdData.AllColumns)
            {
                string v_strName = v_objCol.Name.ToUpper();

                if (v_objCol.GetType().Name == "GridViewCommandColumn")
                {
                    GridViewCommandColumn v_colCommand = (GridViewCommandColumn)v_objCol;
                    v_colCommand.Width = new Unit(50, UnitType.Pixel);

                    string v_strEditButton = "Sửa";
                    string v_strAddButton = "Thêm";
                    string v_strDeleteButton = "Xóa";

                    p_grdData.SettingsCommandButton.EditButton.Text = v_strEditButton;
                    p_grdData.SettingsCommandButton.NewButton.Text = v_strAddButton;
                    p_grdData.SettingsCommandButton.DeleteButton.Text = v_strDeleteButton;
                }
            }

            // Set font
            p_grdData.Font.Name = "Tahoma";
            p_grdData.Font.Size = new FontUnit(11, UnitType.Pixel);

            // Common setting
            p_grdData.Settings.ShowFooter = v_bShow_Footer;
            p_grdData.Settings.ShowGroupPanel = false;

            // Set Filter
            p_grdData.Settings.ShowFilterRow = false;
            p_grdData.Settings.ShowFilterRowMenu = false;
            p_grdData.Settings.ShowPreview = true;
            p_grdData.SettingsSearchPanel.Visible = true;

            // Setting Behaviour
            p_grdData.SettingsBehavior.AutoExpandAllGroups = true;
            p_grdData.SettingsBehavior.ConfirmDelete = true;
            //p_grdData.SettingsBehavior.ColumnResizeMode = ColumnResizeMode.Control;

            // Setting Cookie
            p_grdData.SettingsCookies.Enabled = true;
            p_grdData.SettingsCookies.StoreColumnsVisiblePosition = false;
            p_grdData.SettingsCookies.StoreColumnsWidth = false;
            p_grdData.SettingsCookies.StoreGroupingAndSorting = false;

            // Setting Editing
            p_grdData.SettingsEditing.EditFormColumnCount = 1;
            p_grdData.SettingsEditing.Mode = GridViewEditingMode.PopupEditForm;
            p_grdData.SettingsPopup.EditForm.HorizontalAlign = DevExpress.Web.PopupHorizontalAlign.WindowCenter;
            p_grdData.SettingsPopup.EditForm.VerticalAlign = DevExpress.Web.PopupVerticalAlign.WindowCenter;
            p_grdData.SettingsPopup.EditForm.Modal = true;
            p_grdData.SettingsPopup.EditForm.AllowResize = true;
            p_grdData.SettingsPopup.EditForm.Width = new Unit(400, UnitType.Pixel);

            // Setting Text
            string v_strCancel = "Hủy";
            string v_strSave = "Lưu";

            p_grdData.SettingsText.CommandCancel = v_strCancel;
            p_grdData.SettingsText.PopupEditFormCaption = "Edit Form";
            p_grdData.SettingsText.CommandUpdate = v_strSave;
            p_grdData.SettingsText.ConfirmDelete = "Bạn chắc chắn mình thật sự muốn xóa ?";
            p_grdData.SettingsText.GroupPanel = "Kéo thả cột vào đây để group phân loại dữ liệu";

            // Cell Style
            p_grdData.Styles.Cell.Wrap = DevExpress.Utils.DefaultBoolean.True;
            p_grdData.Styles.Cell.Font.Name = "Tahoma";
            p_grdData.Styles.Cell.Font.Size = new FontUnit(11, UnitType.Pixel);

            // Edit Form Cell
            p_grdData.Styles.EditFormCell.Wrap = DevExpress.Utils.DefaultBoolean.False;

            // Header Style
            p_grdData.Styles.Header.Font.Name = "Tahoma";
            p_grdData.Styles.Header.Font.Size = new FontUnit(11, UnitType.Pixel);
            p_grdData.Styles.Header.Wrap = DevExpress.Utils.DefaultBoolean.True;
            p_grdData.Styles.Header.HorizontalAlign = HorizontalAlign.Center;

            // Footer
            p_grdData.Styles.Footer.Font.Name = "Tahoma";
            p_grdData.Styles.Footer.Font.Bold = true;
            p_grdData.Styles.Footer.Font.Size = new FontUnit(11, UnitType.Pixel);
            p_grdData.Styles.Footer.Wrap = DevExpress.Utils.DefaultBoolean.True;

            // Filter rows
            p_grdData.Styles.FilterCell.Wrap = DevExpress.Utils.DefaultBoolean.True;
            p_grdData.Styles.FilterCell.Font.Name = "Tahoma";
            p_grdData.Styles.FilterCell.Font.Size = new FontUnit(11, UnitType.Pixel);

            // Edit form column
            p_grdData.Styles.EditFormColumnCaption.HorizontalAlign = HorizontalAlign.Right;
            p_grdData.Styles.EditFormColumnCaption.Wrap = DevExpress.Utils.DefaultBoolean.False;

            //p_grdData.SettingsPager.AlwaysShowPager = true;
            p_grdData.SettingsPager.PageSize = CCommonData.g_iPageSize;
        }

        public static void Format_Treelist(DevExpress.Web.ASPxTreeList.ASPxTreeList p_grdData)
        {
            bool v_bShow_Footer = false;

            // Format cho các column spin edit
            foreach (DevExpress.Web.ASPxTreeList.TreeListColumn v_objCol in p_grdData.Columns)
            {
                if (v_objCol.GetType().Name == "TreeListSpinEditColumn")
                {
                    TreeListSpinEditColumn v_colSpin = (TreeListSpinEditColumn)v_objCol;
                    v_colSpin.PropertiesSpinEdit.Increment = 0;

                    if (v_colSpin.PropertiesSpinEdit.DisplayFormatString == "g" || v_colSpin.PropertiesSpinEdit.DisplayFormatString == "")
                        v_colSpin.PropertiesSpinEdit.DisplayFormatString = CCommonData.Number_Format_String;
                    v_colSpin.PropertiesSpinEdit.SpinButtons.Visible = false;
                    v_colSpin.PropertiesSpinEdit.SpinButtons.ShowIncrementButtons = false;
                }
            }

            // Format cho các DateTime Edit
            foreach (DevExpress.Web.ASPxTreeList.TreeListColumn v_objCol in p_grdData.Columns)
            {
                if (v_objCol.GetType().Name == "TreeListDateTimeColumn")
                {
                    TreeListDateTimeColumn v_colDate = (TreeListDateTimeColumn)v_objCol;
                    if (v_colDate.PropertiesDateEdit.DisplayFormatString == "")
                    {
                        v_colDate.PropertiesDateEdit.DisplayFormatString = CCommonData.DateTime_Format_String;
                        v_colDate.PropertiesDateEdit.EditFormatString = CCommonData.DateTime_Format_String;
                    }

                    v_colDate.EditCellStyle.HorizontalAlign = HorizontalAlign.Center;
                }
            }

            // Format cho các combo box Edit
            foreach (DevExpress.Web.ASPxTreeList.TreeListColumn v_objCol in p_grdData.Columns)
            {
                if (v_objCol.GetType().Name == "GridViewDataComboBoxColumn")
                {
                    TreeListComboBoxColumn v_colCombo = (TreeListComboBoxColumn)v_objCol;
                    v_colCombo.PropertiesComboBox.IncrementalFilteringMode = DevExpress.Web.IncrementalFilteringMode.Contains;
                }
            }


            // Set font
            p_grdData.Font.Name = "Tahoma";
            p_grdData.Font.Size = new FontUnit(11, UnitType.Pixel);

            // Common setting
            p_grdData.Settings.ShowFooter = v_bShow_Footer;
            p_grdData.Settings.GridLines = GridLines.Both;
            p_grdData.Settings.SuppressOuterGridLines = true;

            // Setting Behaviour
            p_grdData.SettingsBehavior.AutoExpandAllNodes = true;
            p_grdData.SettingsBehavior.ExpandCollapseAction = TreeListExpandCollapseAction.NodeDblClick;

            // Setting Editing
            p_grdData.SettingsEditing.AllowRecursiveDelete = true;
            p_grdData.SettingsEditing.AllowNodeDragDrop = true;
            p_grdData.SettingsEditing.EditFormColumnCount = 1;
            p_grdData.SettingsEditing.Mode = TreeListEditMode.EditFormAndDisplayNode;

            // Setting paper
            p_grdData.SettingsPager.ShowDefaultImages = false;

            // Setting Text
            string v_strCancel = "Hủy";
            string v_strSave = "Lưu";

            p_grdData.SettingsText.CommandCancel = v_strCancel;
            p_grdData.SettingsText.CommandUpdate = v_strSave;
            p_grdData.SettingsText.ConfirmDelete = "Bạn chắc chắn mình thật sự muốn xóa ?";

            // Cell Style
            p_grdData.Styles.Cell.Wrap = DevExpress.Utils.DefaultBoolean.True;
            p_grdData.Styles.Cell.Font.Name = "Tahoma";
            p_grdData.Styles.Cell.Font.Size = new FontUnit(11, UnitType.Pixel);

            // Header Style
            p_grdData.Styles.Header.Font.Name = "Tahoma";
            p_grdData.Styles.Header.Font.Size = new FontUnit(11, UnitType.Pixel);
            p_grdData.Styles.Header.HorizontalAlign = HorizontalAlign.Center;
            p_grdData.Styles.Header.Wrap = DevExpress.Utils.DefaultBoolean.True;

            // Footer
            p_grdData.Styles.Footer.Wrap = DevExpress.Utils.DefaultBoolean.True;

            // Edit form column
            p_grdData.Styles.EditFormColumnCaption.HorizontalAlign = HorizontalAlign.Right;
            p_grdData.Styles.EditFormColumnCaption.Wrap = DevExpress.Utils.DefaultBoolean.False;

            // Border
            p_grdData.Border.BorderStyle = BorderStyle.Solid;
            p_grdData.Border.BorderWidth = new Unit(1, UnitType.Pixel);

            // Set multi language
            foreach (DevExpress.Web.ASPxTreeList.TreeListColumn v_objCol in p_grdData.Columns)
            {
                if (v_objCol.GetType().Name == "TreeListCommandColumn")
                {
                    TreeListCommandColumn v_colCommand = (TreeListCommandColumn)v_objCol;

                    v_colCommand.EditButton.Text = "Sửa";
                    v_colCommand.NewButton.Text = "Thêm";
                    v_colCommand.DeleteButton.Text = "Xóa";
                }
            }
        }

        public static string Set_Error_MessageBox(string p_strMessage)
        {
            StringBuilder v_sb = new StringBuilder();

            v_sb.AppendLine("<div class=\"alert error rounded\">");
            v_sb.AppendLine(p_strMessage);
            v_sb.AppendLine("</div>");

            return v_sb.ToString();
        }

        public static string Set_Success_MessageBox(string p_strMessage)
        {
            StringBuilder v_sb = new StringBuilder();

            v_sb.AppendLine("<div class=\"alert success rounded\">");
            v_sb.AppendLine(p_strMessage);
            v_sb.AppendLine("</div>");

            return v_sb.ToString();
        }

    }
}
