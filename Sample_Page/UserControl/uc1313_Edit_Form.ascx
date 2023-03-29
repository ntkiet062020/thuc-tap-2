﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="uc1313_Edit_Form.ascx.cs" Inherits="TKS_Thuc_Tap_Web.Sample_Page.UserControl.uc1313_Edit_Form" %>
<%@ Register assembly="DevExpress.Web.v17.1, Version=17.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web" tagprefix="dx" %>

<div class="TKS_Editor_Form">
    <div class="row">
		<div class="col-md-8">
            <asp:Literal ID="litError" runat="server"></asp:Literal>
            <div class="row border-top background">
            	<div class="col-md-2 part-01">
                	<h5>Phòng Ban</h5>
                </div>
                <div class="col-md-4 part-02">
                    <dx:ASPxComboBox ID="ASPxComboBox1" runat="server" Theme="Office2010Blue" ValueType="System.Int32">
                    </dx:ASPxComboBox>
                </div>
            </div>

            <div class="row border-top background">
            	<div class="col-md-2 part-01">
                	<h5>Yêu cầu</h5>
                </div>
                <div class="col-md-6 part-02">
                    
                    <dx:ASPxTextBox ID="ASPxTextBox1" runat="server" Theme="Office2010Blue" >
                    </dx:ASPxTextBox>
                    
                </div>
            </div>

            <div class="row border-top background">
            	<div class="col-md-2 part-01">
                	<h5>Ngày hoàn thành</h5>
                </div>
                <div class="col-md-4 part-02">
                    <dx:ASPxDateEdit ID="ASPxDateEdit1" runat="server" Theme="Office2010Blue" >
                    </dx:ASPxDateEdit>
                </div>
            </div>

            <div class="row border-top background">
            	<div class="col-md-2 part-01">
                	<h5>Số tiền</h5>
                </div>
                <div class="col-md-4 part-02">
                   <dx:ASPxSpinEdit ID="txtGia_Ban" runat="server" Number="0" Theme="Office2010Blue" />
                </div>
            </div>

            <div class="row border-top background">
            	<div class="col-md-2 part-01">
                	<h5>Mô tả</h5>
                </div>
                <div class="col-md-10 part-02">
                    <dx:ASPxMemo ID="ASPxMemo1" runat="server" Height="71px" Theme="Office2010Blue" >
                    </dx:ASPxMemo>
                </div>
            </div>

            <div class="row-spacer_10"></div>

            <div class="row">
                <div class="col-md-12" >
                    <div align="right">
                        <dx:ASPxButton ID="ASPxButton1" runat="server" Text="Lưu" height="35px"
                            Theme="Office2010Blue">
                        </dx:ASPxButton>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>

<div class="row-spacer_20"></div>
<div class="row-spacer_20"></div>
<div class="row-spacer_20"></div>


<div class="TKS_Editor_Form">
    <div class="row">
	    <div class="col-md-4">
            <h6>Form Mẫu Thông Tin</h6>
            <div class="row border-top background">
                <div class="col-md-6 part-01">
                    <h5>Mã đăng nhập</h5>
                </div>
                <div class="col-md-6 part-03">
                    <%=TKS_Thuc_Tap_Web.CSession.Active_User_Name %>
                </div>
            </div>

            <div class="row border-top background">
                <div class="col-md-6 part-01">
                    <h5>Họ tên </h5>
                </div>
                <div class="col-md-6 part-03">
                    <%=TKS_Thuc_Tap_Web.CSession.Active_User.Ho_Lot%> <%=TKS_Thuc_Tap_Web.CSession.Active_User.Ten%>
                </div>
            </div>

            <div class="row border-top background">
                <div class="col-md-6 part-01">
                    <h5>Điện thoại </h5>
                </div>
                <div class="col-md-6 part-03">
                    <%=TKS_Thuc_Tap_Web.CSession.Active_User.Dien_Thoai%>
                </div>
            </div>

            <div class="row border-top background">
                <div class="col-md-6 part-01">
                    <h5>Email</h5>
                </div>
                <div class="col-md-6 part-03">
                    <%=TKS_Thuc_Tap_Web.CSession.Active_User.Email%>
                </div>
            </div>

            <div class="row border-top background">
                <div class="col-md-6 part-01">
                    <h5>Lần đăng nhập cuối cùng </h5>
                </div>
                <div class="col-md-6 part-03">
                </div>
            </div>
        </div>
    </div>
</div>