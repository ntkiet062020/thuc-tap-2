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
using TKS_Thuc_Tap_Utility;
using TKS_Thuc_Tap_Entity.Admin;
using System.Collections.Generic;

namespace TKS_Thuc_Tap_Web
{
    public class CSession
    {
        /// <summary>
        /// User đăng nhập
        /// </summary>
        public static CThanh_Vien Active_User
        {
            get
            {
                if (HttpContext.Current.Session["Active_User"] == null)
                    return null;
                return (CThanh_Vien)HttpContext.Current.Session["Active_User"];
            }

            set
            {
                HttpContext.Current.Session["Active_User"] = value;
            }
        }

        /// <summary>
        /// User đăng nhập
        /// </summary>
        public static string Active_User_Name
        {
            get
            {
                if (HttpContext.Current.Session["Active_User_Name"] == null)
                    return "";
                return HttpContext.Current.Session["Active_User_Name"].ToString();
            }

            set
            {
                HttpContext.Current.Session["Active_User_Name"] = value;
            }
        }

        public static int Active_Func_Group_ID
        {
            get
            {
                if (HttpContext.Current.Session["Active_Func_Group_ID"] == null)
                    return 1;
                return int.Parse(HttpContext.Current.Session["Active_Func_Group_ID"].ToString());
            }

            set
            {
                HttpContext.Current.Session["Active_Func_Group_ID"] = value;
            }
        }

        /// <summary>
        /// User đăng nhập
        /// </summary>
        public static string Url_Referrer
        {
            get
            {
                if (HttpContext.Current.Session["Url_Referrer"] == null)
                    return "";
                return HttpContext.Current.Session["Url_Referrer"].ToString();
            }

            set
            {
                HttpContext.Current.Session["Url_Referrer"] = value;
            }
        }

        /// <summary>
        /// User đăng nhập
        /// </summary>
        public static string Lang_ID
        {
            get
            {
                if (HttpContext.Current.Session["Lang_ID"] == null)
                    return CCommonData.g_strLangVN;
                return HttpContext.Current.Session["Lang_ID"].ToString();
            }

            set
            {
                HttpContext.Current.Session["Lang_ID"] = value;
            }
        }
    }
}
