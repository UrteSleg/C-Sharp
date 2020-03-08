<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="L2_3_Leidiniai.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" runat="server" media="screen" href="~/StyleCSS.css" />
     <link rel="stylesheet" href="https://fonts.googleapis.com/css?family=Open+Sans:300" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <br />
            Leidinio pavadinimas:<br />
            <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
            <br />
            <br />
            Leidinio mėnuo:<br />
            <br />
            <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
            <br />
            <br />
            <asp:Button ID="Button1" runat="server" BackColor="White" BorderColor="White" BorderStyle="Outset" Text="Pateikti informacija" OnClick="Button1_Click" />
            <br />
            <br />
            <br />
            <asp:Table ID="Table2" runat="server" GridLines="Both" Height="158px" Width="381px">
            </asp:Table>
            <br />
            <br />
            <br />
            <asp:Table ID="Table3" runat="server" GridLines="Both" Height="181px" Width="380px">
            </asp:Table>
            <br />
            <br />
            <asp:Table ID="Table4" runat="server" GridLines="Both" Height="181px" Width="380px">
            </asp:Table>
            <br />
            <br />
            <asp:Table ID="Table5" runat="server" GridLines="Both" Height="181px" Width="380px">
            </asp:Table>
            <br />
            <br />
            <asp:Table ID="Table6" runat="server" GridLines="Both" Height="181px" Width="380px">
            </asp:Table>
            <br />
            <br />
            <asp:Table ID="Table7" runat="server" GridLines="Both" Height="181px" Width="380px">
            </asp:Table>
            <br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</div>
    </form>
</body>
</html>
