<%@ Page Title="" Language="C#" MasterPageFile="~/WebAdmin/MPBlank.Master" AutoEventWireup="true" CodeBehind="Blank_Page.aspx.cs" Inherits="TKS_Thuc_Tap_Web.Sample_Page.Blank_Page" %>
<%@ Register src="UserControl/uc1313_Edit_Form.ascx" tagname="uc1313_Edit_Form" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:uc1313_Edit_Form ID="uc1313_Edit_Form1" runat="server" />
</asp:Content>
