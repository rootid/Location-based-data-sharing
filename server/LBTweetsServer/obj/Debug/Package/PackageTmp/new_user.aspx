<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="new_user.aspx.cs" Inherits="LBTweetsServer.new_user" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:Label ID="Label1" runat="server" Text="Phone Number : "></asp:Label>
    
        <asp:TextBox ID="phone_number_textbox" runat="server"></asp:TextBox>
    
        <br />
        <br />
        <asp:Label ID="Label2" runat="server" Text="Password : "></asp:Label>
    
        <asp:TextBox ID="password_textbox" runat="server"></asp:TextBox>
        <br />
        <br />
        <asp:CheckBox ID="entertainment_checkbox" runat="server" />
        <asp:Label ID="Label3" runat="server" Text="Entertainment"></asp:Label>
        <br />
        <br />
        <asp:CheckBox ID="sporsts_checkbox" runat="server" />
        <asp:Label ID="Label4" runat="server" Text="Sports"></asp:Label>
        <br />
        <br />
        <asp:CheckBox ID="shopping_checkbox" runat="server" />
        <asp:Label ID="Label5" runat="server" Text="Shopping"></asp:Label>
        <br />
        <br />
        <asp:CheckBox ID="campus_checkbox" runat="server" />
        <asp:Label ID="Label6" runat="server" Text="Campus"></asp:Label>
        <br />
        <br />
        <asp:Button ID="register_button" runat="server" Text="Register" 
            onclick="register_button_Click" />
    </div>
    </form>
</body>
</html>
