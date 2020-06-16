<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="XML_APP._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
   <center>
        <h2>Inport/Export Database data to XML file</h2>
   </center>
    <div>
        <br />
        <table>
            <tr>
                <td>
                    Select file:
                </td>
                <td>
                    <asp:FileUpload ID="FileUpload1" runat="server" />
                </td>
                <td>
                    <asp:Button ID="btnImport" runat ="server" Text="Import" OnClick="btnImport_Click" />    
                </td>
            </tr>
        </table>
        <div>
            <br />
            <center>
                <asp:Label ID="lbMessage" runat="server" Font-Bold="true" />
            </center>
            <br />
            <asp:GridView ID="gvData" runat="server" AutoGenerateColumns="false">
                <EmptyDataTemplate>
                    <div style="padding:10px">
                        Data not found.
                    </div>
                </EmptyDataTemplate>
                <Columns>
                    <asp:BoundField HeaderText="Employee ID" DataField="EmployeeID" ItemStyle-Width="100"/>
                    <asp:BoundField HeaderText="Company Name" DataField="CompanyName" ItemStyle-Width="260" />
                    <asp:BoundField HeaderText="Contact Name" DataField="ContactName" ItemStyle-Width="160"/>
                    <asp:BoundField HeaderText="Contact Title" DataField="ContactTitle" ItemStyle-Width="160"/>
                    <asp:BoundField HeaderText="Address" DataField="EmployeeAddress" ItemStyle-Width="360"/>
                    <asp:BoundField HeaderText="Postal Code" DataField="PostalCode" ItemStyle-Width="160"/>
                </Columns>
            </asp:GridView>
            <br />
            <asp:Button ID="btnExport" runat="server" Text="Export" OnClick="btnExport_Click" />
        </div>
    </div>
       
</asp:Content>
