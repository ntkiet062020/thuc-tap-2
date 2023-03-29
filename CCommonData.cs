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
using System.Collections.Generic;

namespace TKS_Thuc_Tap_Web
{
    public class CCommonData
    {
        // Các tham số cơ bản
        public static string g_strLangVN = ConfigurationManager.AppSettings["Lang_VN"];
        public static int g_iPageSize = 20;
        public static int g_iAdminFuncGroupID = 1;
        public static int g_iDefaultFuncID = int.Parse(ConfigurationManager.AppSettings["Default_Func_ID"]);
        public static string g_strDefault_Url = ConfigurationManager.AppSettings["Default_Url"];

        public static string DateTime_Format_String = ConfigurationManager.AppSettings["DateTime_Format_String"];
        public static string Number_Format_String = ConfigurationManager.AppSettings["Number_Format_String"];

        // Multi language
        public static IDictionary<string, string> g_dicLanguage_EN = new Dictionary<string, string>();
        public static IDictionary<string, string> g_dicLanguage_VN = new Dictionary<string, string>();
        public static IDictionary<string, IDictionary<string, string>> g_dicLang = new Dictionary<string, IDictionary<string, string>>();

        //import excel
        public static string g_strPathImportExcel = ConfigurationManager.AppSettings["Import_Excel"];


        #region Mail
        public static string g_strSMTP_Host = ConfigurationManager.AppSettings["smpt_Host"];
        public static bool g_bSMTP_UseDefaultCredentials = bool.Parse(ConfigurationManager.AppSettings["smpt_UseDefaultCredentials"]);
        public static int g_iSMTP_Port = int.Parse(ConfigurationManager.AppSettings["smpt_Port"]);
        public static string g_strSMTP_User = ConfigurationManager.AppSettings["smpt_User"];
        public static string g_strSMTP_Pass = ConfigurationManager.AppSettings["smpt_Pass"];
        public static bool g_bSMTP_EnableSsl = bool.Parse(ConfigurationManager.AppSettings["smpt_EnableSsl"]);
        public static string g_strEmail_From = ConfigurationManager.AppSettings["Email_From"];
        public static string g_strEmail_Warning_HoanThanh = ConfigurationManager.AppSettings["Email_Warning_HoanThanh"];
        #endregion
    }
}
