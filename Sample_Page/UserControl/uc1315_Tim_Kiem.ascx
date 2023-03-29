<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="uc1315_Tim_Kiem.ascx.cs" Inherits="TKS_Thuc_Tap_Web.Sample_Page.UserControl.uc1315_Tim_Kiem" %>
<%@ Register assembly="DevExpress.Web.v17.1, Version=17.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web" tagprefix="dx" %>
<div class="row">
	<div class="col-md-12">
        <span style="font-size:11px; font-family: Tahoma">
            - Tìm kiếm đơn vị tính.<br />
        </span>
    </div>
</div>

<div class="row-spacer_20"></div>

<div class="row">
	<div class="col-md-12">
        <dx:ASPxRoundPanel ID="ASPxRoundPanel1" runat="server" ShowHeader="False" Width="100%" Theme="Office2010Blue">
            <PanelCollection>
                <dx:PanelContent runat="server">
                    <div class="row">
                        <div class="col-md-1" style="text-align: right; padding-top: 6px;">Ngày tạo:</div>
                        <div class="col-md-2">
                            <dx:ASPxDateEdit ID="dtmNgay_Bat_Dau" runat="server" Theme="Office2010Blue" Width="100%">
                            </dx:ASPxDateEdit>
                        </div>
                        <div class="col-md-1" style="text-align: center; width: 50px; padding-top: 6px;">--></div>
                        <div class="col-md-2">
                            <dx:ASPxDateEdit ID="dtmNgay_Ket_Thuc" runat="server" Theme="Office2010Blue" Width="100%">
                            </dx:ASPxDateEdit>
                        </div>
                        <div class="col-md-2">
                            <dx:ASPxButton ID="btnView" runat="server" Text="Tìm Kiếm" Theme="Office2010Blue">
                            </dx:ASPxButton>
                        </div>
                    </div>
                </dx:PanelContent>
            </PanelCollection>
        </dx:ASPxRoundPanel>
    </div>
</div>

<div class="row-spacer_20"></div>

<div class="row">
	<div class="col-md-12">
        <dx:ASPxGridView ID="grdData" runat="server" 
            DataSourceID="sqlData" 
            Width="100%" 
            KeyFieldName="Auto_ID" 
            Theme="Office2010Blue" ClientInstanceName="grdData" 
            AutoGenerateColumns="False" EnableTheming="True" >    
    
            <Columns>
                <dx:GridViewCommandColumn ShowSelectCheckbox="True" VisibleIndex="0" Width="40px">
                    <HeaderTemplate>
                        <input type="checkbox" onclick="grdData.SelectAllRowsOnPage(this.checked);" style="vertical-align: middle;"
                            title="Select/Unselect all rows on the page"></input>
                    </HeaderTemplate>
                </dx:GridViewCommandColumn>

                <dx:GridViewDataTextColumn Caption="MSHT" FieldName="Auto_ID" VisibleIndex="1" Width="60px">
                    <EditFormSettings Visible="False" />
                </dx:GridViewDataTextColumn>
        
                <dx:GridViewDataTextColumn Caption="Tên Đơn Vị Tính" FieldName="Ten_Don_Vi_Tinh" VisibleIndex="2" >
                    <CellStyle HorizontalAlign="Left">
                    </CellStyle>
                </dx:GridViewDataTextColumn>
            </Columns>
        </dx:ASPxGridView>
                
        <table cellpadding="0" cellspacing="0" width="100%" style="margin-top:15px">
            <tr>
                <td align="left">
                    &nbsp;
                </td>
                
                <td width="130px" align="right">
                    <dx:ASPxButton ID="btnExport_PDF" runat="server" Text="Kết Xuất PDF" Theme="Office2010Blue">
                    </dx:ASPxButton>
                </td>
                <td width="130px" align="right">
                    <dx:ASPxButton ID="btnExport_CSV" runat="server" Text="Kết Xuất CSV" Theme="Office2010Blue">
                    </dx:ASPxButton>
                </td>
                <td width="130px" align="right">
                    <dx:ASPxButton ID="btnExport_XLS" runat="server" Text="Kết Xuất XLS" Theme="Office2010Blue">
                    </dx:ASPxButton>
                </td>
                <td width="150px" align="right">
                    <dx:ASPxButton ID="btnExport_XLSX" runat="server" Text="Kết Xuất XLSX" Theme="Office2010Blue" Width="120px">
                    </dx:ASPxButton>
                </td>
            </tr>
        </table>
        <dx:ASPxGridViewExporter ID="gridExport" runat="server" GridViewID="grdData"></dx:ASPxGridViewExporter>

        <asp:SqlDataSource ID="sqlData" runat="server" 
            ConnectionString="<%$ appSettings:TKS_Thuc_Tap_Data_Conn_String %>" 
            SelectCommand="sp_sel_List_DM_Don_Vi_Tinh" SelectCommandType="StoredProcedure" >
        </asp:SqlDataSource>
    </div>
</div>

