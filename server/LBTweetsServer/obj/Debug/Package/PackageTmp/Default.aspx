<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="LBTweetsServer._Default" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
<a href="new_user.aspx"> New User </a> <br />
<a href="tweet_page.aspx"> Tweet </a> <br />
<a href="get_tweets.aspx"> Get Tweets </a> <br />
<a href="getAllTweets.aspx"> GetAllTweets </a> <br />
<a href="WebServices.asmx"> Web Services </a>
</asp:Content>
