<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CadastrarEmpresa.aspx.cs" Inherits="SDE.Web.CadastrarEmpresa" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <style type="text/css">
        div.campos span { margin: 5px; font-weight:bold; font-size: large; }
        div.campos input { padding: 3px; }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div class="campos">
        <p><asp:Label Text= "Nome Fantasia:" Width="4cm" runat="server"></asp:Label>  <asp:TextBox ID="txtFant" Width="10cm" runat="server"></asp:TextBox></p>
        <p><asp:Label Text= "Razão Social:" Width="4cm" runat="server"></asp:Label>   <asp:TextBox ID="txtRazao" Width="10cm" runat="server"></asp:TextBox></p>
        <p><asp:Label Text= "CNPJ:" Width="4cm" runat="server"></asp:Label>  <asp:TextBox ID="txtCnpj" Width="10cm" runat="server"></asp:TextBox></p>
        <p><asp:Label Text= "Login:" Width="4cm" runat="server"></asp:Label>  <asp:TextBox ID="txtLogin" Width="10cm" runat="server"></asp:TextBox></p>
        
        <p><asp:Label ID="lblResposta" Text="" runat="server"></asp:Label></p>
    </div>
    <hr />
    <h1>Cadastro</h1>
    <asp:Button ID="Button1" runat="server" Text="Cadastrar"  OnClick="btn_Criar_Click"/><br />
    <hr />
    <h1>Inclusão</h1>
    
    <asp:ListBox ID="lstbxEmpresas" runat="server" Height="400px" Width="250px" Font-Size="Large"></asp:ListBox>
    <asp:Button ID="Button2" runat="server" Text="Inserir"  OnClick="btn_Incluir_Click" /><br />
    
    
    </form>
</body>
</html>
