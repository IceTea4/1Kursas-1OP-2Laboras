<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Forma1.aspx.cs" Inherits="_2Laboras.Forma1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Button ID="Button1" runat="server" Text="Nuskaityti duomenis" OnClick="Button1_Click" />
            <br />
            <br />
            <asp:Label ID="Label1" runat="server" Text="Pradiniai duomenys:"></asp:Label>
            <asp:Table ID="Table1" runat="server" BorderColor="Black" BorderWidth="1px" GridLines="Horizontal" BorderStyle="Solid"></asp:Table>
            <br />
            <asp:Table ID="Table2" runat="server" BorderColor="Black" BorderWidth="1px" GridLines="Horizontal"></asp:Table>
            <br />
            <br />
            <asp:Label ID="Label4" runat="server" Text="Profesoriaus pavardė:"></asp:Label>
            <br />
            <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
            <br />
            <asp:Label ID="Label5" runat="server" Text="Profesoriaus vardas:"></asp:Label>
            <br />
            <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
            <br />
            <br />
            <asp:Button ID="Button2" runat="server" Text="Apskaičiuoti" OnClick="Button2_Click" />
            <br />
            <br />
            <asp:Label ID="Label2" runat="server" Text="Rezultatai:"></asp:Label>
            <asp:Table ID="Table3" runat="server" BorderWidth="1px" BorderColor="Black" GridLines="Horizontal"></asp:Table>
            <asp:Label ID="Label3" runat="server" Text="Label"></asp:Label>
            <br />
            <br />
            <asp:Label ID="Label6" runat="server" Text="Label"></asp:Label>
        </div>
    </form>
</body>
</html>
