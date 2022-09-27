
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="PowershellUtil.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Powershell Util</title>

    <link rel="stylesheet" href="resources/css/psutil-1.0.css" />
</head>
<body>
    <center>
        <form id="frmMain" runat="server">
            <h1>Powershell Util</h1>
            <div>
                <asp:DropDownList ID="cmbScripts" runat="server" AutoPostBack="true" Font-Names="Roboto" Font-Size="Small" ForeColor="Black" Width="494px" OnSelectedIndexChanged="cmbScripts_SelectedIndexChanged"></asp:DropDownList>
                <br />
                <br />
                <br />
            </div>
            <div id="divHtmlForm" runat="server" style="overflow: auto; resize:both; height: auto; width: auto">
            </div>
            <br />
            <br />
            <asp:Button ID="btnExecute" runat="server" Text="Execute" Font-Names="Roboto" Font-Size="Small" OnClick="btnExecute_Click" Width="162px"></asp:Button>
        </form>
        <hr />
    </center>
    <center><div id="divMainContent" runat="server" style="overflow: auto; resize:both; height: auto; width: auto"></center>
    </div>
</body>
</html>
