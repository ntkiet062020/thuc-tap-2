using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Configuration;

namespace TKS_Thuc_Tap_Web
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            // Log
            if (ConfigurationManager.AppSettings["Is_Trace"] == "1")
                TKS_Thuc_Tap_Utility.CLogger.Enable_Trace = true;
            else
                TKS_Thuc_Tap_Utility.CLogger.Enable_Trace = false;

            TKS_Thuc_Tap_Utility.CLogger.File_Name = ConfigurationManager.AppSettings["Log_File_Path"];

            // Code that runs on application startup
            TKS_Thuc_Tap_Utility.CConfig.g_strTKS_Thuc_Tap_Admin_Conn_String = ConfigurationManager.AppSettings["TKS_Thuc_Tap_Admin_Conn_String"];
            TKS_Thuc_Tap_Utility.CConfig.g_strTKS_Thuc_Tap_Data_Conn_String = ConfigurationManager.AppSettings["TKS_Thuc_Tap_Data_Conn_String"];

            TKS_Thuc_Tap_Utility.CConfig.g_strCompany_Name = ConfigurationManager.AppSettings["Company_Name"].Replace("|", "&");
            TKS_Thuc_Tap_Utility.CConfig.g_strCompany_Address = ConfigurationManager.AppSettings["Company_Address"].Replace("|", "&");
            TKS_Thuc_Tap_Utility.CConfig.g_strCompany_Tel = ConfigurationManager.AppSettings["Company_Tel"];
            TKS_Thuc_Tap_Utility.CConfig.g_strCompany_Fax = ConfigurationManager.AppSettings["Company_Fax"];
            TKS_Thuc_Tap_Utility.CConfig.g_intReport_Line_Space = int.Parse(ConfigurationManager.AppSettings["Report_Line_Space"]);

            // Lang multilang data
            CCommonData.g_dicLang.Add("vi-VN", CCommonData.g_dicLanguage_VN);
            CCommonData.g_dicLang.Add("en-US", CCommonData.g_dicLanguage_EN);
        }

        protected void Session_Start(object sender, EventArgs e)
        {
            CSession.Lang_ID = CCommonData.g_strLangVN;
            Session.Timeout = 720;
            CSession.Active_Func_Group_ID = 1;
            Session["Session_ID"] = Session.SessionID;
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}