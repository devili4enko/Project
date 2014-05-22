<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PageFlow.aspx.cs" Inherits="Project.WebForms.PageFlow" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Label runat="server" ID="TestLable" EnableViewState="false" ></asp:Label>0
        <asp:Button runat="server" ID="TestBtn" Text="Button" OnClick="TestBtn_Click" />
    
    </div>
    </form>
</body>
</html>
