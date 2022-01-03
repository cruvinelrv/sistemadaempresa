<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="imprime.aspx.cs" Inherits="SDE.Web.imprime" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>SDE SISTEMAS DE GESTAO - 64-3621-4579</title>
    <link href="htmlcss/imprime.css" rel="stylesheet" type="text/css" />
</head>
<body id="body" runat="server">
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" />
    
    <asp:Literal ID="Literal1" runat="server"></asp:Literal>
    
    <asp:Timer ID="Timer1" runat="server" ontick="Timer1_Tick">
    </asp:Timer>
    </form>
</body>
</html>
