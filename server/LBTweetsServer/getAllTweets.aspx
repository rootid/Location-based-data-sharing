<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="getAllTweets.aspx.cs" Inherits="LBTweetsServer.getAllTweets" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
            DataKeyNames="rowID" DataSourceID="SqlDataSource1">
            <Columns>
                <asp:BoundField DataField="rowID" HeaderText="rowID" InsertVisible="False" 
                    ReadOnly="True" SortExpression="rowID" />
                <asp:BoundField DataField="tweet_value" HeaderText="tweet_value" 
                    SortExpression="tweet_value" />
                <asp:BoundField DataField="tags" HeaderText="tags" SortExpression="tags" />
                <asp:BoundField DataField="ttl" HeaderText="ttl" SortExpression="ttl" />
                <asp:BoundField DataField="time_stamp" HeaderText="time_stamp" 
                    SortExpression="time_stamp" />
                <asp:BoundField DataField="latitude" HeaderText="latitude" 
                    SortExpression="latitude" />
                <asp:BoundField DataField="longitude" HeaderText="longitude" 
                    SortExpression="longitude" />
            </Columns>
        </asp:GridView>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
            ConnectionString="<%$ ConnectionStrings:LBTweetsConnectionString2 %>" 
            SelectCommand="SELECT * FROM [tweets_tags]"></asp:SqlDataSource>
        <br />
        <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" 
            DataKeyNames="phone_number" DataSourceID="SqlDataSource2">
            <Columns>
                <asp:BoundField DataField="phone_number" HeaderText="phone_number" 
                    ReadOnly="True" SortExpression="phone_number" />
                <asp:BoundField DataField="passwrd" HeaderText="passwrd" 
                    SortExpression="passwrd" />
                <asp:BoundField DataField="interests" HeaderText="interests" 
                    SortExpression="interests" />
            </Columns>
        </asp:GridView>
        <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
            ConnectionString="<%$ ConnectionStrings:LBTweetsConnectionString3 %>" 
            SelectCommand="SELECT * FROM [user_profile]"></asp:SqlDataSource>
        <br />
        <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="False" 
            DataKeyNames="tag" DataSourceID="SqlDataSource3">
            <Columns>
                <asp:BoundField DataField="tag" HeaderText="tag" ReadOnly="True" 
                    SortExpression="tag" />
                <asp:BoundField DataField="ttl" HeaderText="ttl" SortExpression="ttl" />
            </Columns>
        </asp:GridView>
        <asp:SqlDataSource ID="SqlDataSource3" runat="server" 
            ConnectionString="<%$ ConnectionStrings:LBTweetsConnectionString4 %>" 
            SelectCommand="SELECT * FROM [tag_to_ttl]"></asp:SqlDataSource>
    
    </div>
    </form>
</body>
</html>
