<%@ Page Title="" Language="C#" MasterPageFile="~/WebAdmin/MPAdmin.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="TKS_Thuc_Tap_Web.Dashboard.Dashboard" %>
<%@ Register src="UserControl/uc8000_Dashboard.ascx" tagname="uc8000_Dashboard" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:uc8000_Dashboard ID="uc8000_Dashboard1" runat="server" />
</asp:Content>
