<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="LinqToSqlTest.aspx.cs" Inherits="Sample.WebForms.LinqToSqlTest" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div>
		<fieldset>
			<legend>Test LINQ to SQL</legend>
			<asp:GridView ID="_grdData" runat="server" CellPadding="4" EnableModelValidation="True"
				ForeColor="#333333" GridLines="None">
				<AlternatingRowStyle BackColor="White" ForeColor="#284775"></AlternatingRowStyle>

				<EditRowStyle BackColor="#999999"></EditRowStyle>

				<FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White"></FooterStyle>

				<HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White"></HeaderStyle>

				<PagerStyle HorizontalAlign="Center" BackColor="#284775" ForeColor="White"></PagerStyle>

				<RowStyle BackColor="#F7F6F3" ForeColor="#333333"></RowStyle>

				<SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333"></SelectedRowStyle>
			</asp:GridView>
		</fieldset>
    </div>
</asp:Content>
