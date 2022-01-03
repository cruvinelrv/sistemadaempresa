<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ImprimeRelCaixaCorp.aspx.cs" Inherits="SDE.code.ImprimeRelCaixaCorp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>SDE Sistema de Gestão</title>
    <link href="htmlcss/imprimeRelCaixa.css" rel="Stylesheet" type="text/css" />
</head>
<body id="body" runat="server">
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" />
    
    <asp:Literal ID="Literal1" runat="server" />
    
    <asp:Timer ID="Timer1" runat="server" OnTick="Timer1_Tick" />
    </form>
</body>
</html>
