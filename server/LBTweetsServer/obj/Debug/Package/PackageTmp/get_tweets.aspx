<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="get_tweets.aspx.cs" Inherits="LBTweetsServer.request" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        #xml_frame
        {
            width: 1035px;
            height: 234px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <br />
    
        <asp:Label ID="Label1" runat="server" Text="Phone Number :"></asp:Label>
    
    &nbsp;&nbsp;
            
    &nbsp;&nbsp;
        <asp:RadioButtonList ID="RadioButtonList1" runat="server" 
            DataSourceID="SqlDataSource1" DataTextField="phone_number" 
            DataValueField="phone_number">
        </asp:RadioButtonList>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
            ConnectionString="<%$ ConnectionStrings:LBTweetsConnectionString %>" 
            SelectCommand="SELECT [phone_number] FROM [user_profile]">
        </asp:SqlDataSource>
        <br />
        <asp:Label ID="Label7" runat="server" Text="Latitude"></asp:Label>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:TextBox ID="latitude_textbox" runat="server"></asp:TextBox>
        <br />
        <br />
        <asp:Label ID="Label8" runat="server" Text="Longitude"></asp:Label>
        &nbsp;&nbsp; <asp:TextBox ID="longitude_textbox" runat="server"></asp:TextBox>
        <br />
        <br />
        <asp:Button ID="get_tweets_button" runat="server" Text="Get Tweets" 
            onclick="get_tweets_button_Click" />
        <br />
    
    </div>
    </form>
</body>
</html>
