<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="uc2002_Drill_Down.ascx.cs" Inherits="TKS_Thuc_Tap_Web.Danh_Muc.UserControl.uc2002_Drill_Down" %>
<%@ Register assembly="DevExpress.Web.v17.1, Version=17.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web" tagprefix="dx" %>
<div class="row">
	<div class="col-md-12">
            <span style="font-size:11px; font-family: Tahoma">
            - Khai báo Drill Down.
            </span>
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
        
                    <dx:GridViewDataTextColumn Caption="Tên Field" FieldName="Field_Name" VisibleIndex="2" Width="200px" >
                        <CellStyle HorizontalAlign="Left">
                        </CellStyle>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="Link URL" FieldName="Link_URL" VisibleIndex="3" >
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="Parameter" FieldName="Parameter_Field" VisibleIndex="4" Width="200px" >
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="Function ID" FieldName="Func_ID" VisibleIndex="5" Width="80px" >
                    </dx:GridViewDataTextColumn>

                    <dx:GridViewCommandColumn VisibleIndex="6" Width="50px" ShowNewButton="True">
                    </dx:GridViewCommandColumn>
        
                    <dx:GridViewCommandColumn VisibleIndex="7" Width="40px" ShowEditButton="True">
                    </dx:GridViewCommandColumn>
        
                    <dx:GridViewCommandColumn VisibleIndex="8" Width="40px" ShowDeleteButton="True">
                    </dx:GridViewCommandColumn>

                </Columns>
            </dx:ASPxGridView>
                
            <table cellpadding="0" cellspacing="0" width="100%" style="margin-top:15px">
                <tr>
                    <td align="left">
                        <dx:ASPxButton ID="btnXoa_Select" runat="server" Text="Xóa Chọn" 
                            onclick="btnXoa_Select_Click" Theme="Office2010Blue">
                        </dx:ASPxButton>
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
                DeleteCommand="sp_del_DM_Drill_Down" DeleteCommandType="StoredProcedure" 
                InsertCommand="sp_ins_DM_Drill_Down" InsertCommandType="StoredProcedure" 
                SelectCommand="sp_sel_List_DM_Drill_Down" SelectCommandType="StoredProcedure" 
                UpdateCommand="sp_upd_DM_Drill_Down" UpdateCommandType="StoredProcedure">
                <DeleteParameters>
                    <asp:Parameter Name="Auto_ID" />
                    <asp:SessionParameter Name="Last_Updated_By" SessionField="Active_User_Name" />
                </DeleteParameters>
                <UpdateParameters>
                    <asp:Parameter Name="Auto_ID" Type="Int32" />
                    <asp:Parameter Name="Field_Name" Type="String" />
                    <asp:Parameter Name="Link_URL" Type="String" />
                    <asp:Parameter Name="Parameter_Field" Type="String" />
                    <asp:Parameter Name="Func_ID" Type="String" />
                    <asp:SessionParameter Name="Last_Updated_By" SessionField="Active_User_Name" Type="String" />
                </UpdateParameters>
                <InsertParameters>
                    <asp:Parameter Name="Field_Name" Type="String" />
                    <asp:Parameter Name="Link_URL" Type="String" />
                    <asp:Parameter Name="Parameter_Field" Type="String" />
                    <asp:Parameter Name="Func_ID" Type="String" />
                    <asp:SessionParameter Name="Last_Updated_By" SessionField="Active_User_Name" Type="String" />
                </InsertParameters>
            </asp:SqlDataSource>
    </div>
</div>

