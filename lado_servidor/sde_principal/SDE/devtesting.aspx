<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="devtesting.aspx.cs" Inherits="SDE.devtesting" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>SDE SISTEMA</title>
    <meta http-equiv="Content-Type"
        content="text/html;charset=UTF-8" />
        <style>
            *{ border:none; margin: 0; padding:0;
               font-size:14px;
               font-family:Roman;
               font-weight:bold;
            }
            .etiqueta
            {
            	width:88.9mm;
            	height:39.1mm;
            	margin-left:0mm;
            	margin-right:0mm;
            	margin-top:-1.5mm;
            	margin-bottom:0mm;
            	border: solid 1px;
            }
        </style>
    <link href="htmlcss/imprime.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Literal ID="Literal1" runat="server"></asp:Literal>
    </div>
    </form>
</body>
</html>
