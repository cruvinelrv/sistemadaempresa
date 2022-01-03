<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="notaprefeitura.aspx.cs" Inherits="SDE.Web.notaprefeitura" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>SDE SISTEMA</title>
    <meta http-equiv="Content-Type"
        content="text/html;charset=UTF-8" />
    <style type="text/css">
        *{ border:none; margin: 0; padding:0;
           font-family:Roman;
           }
        .pagina
        {
        	width: 21cm;
        	height: 28cm;
        }
        .cab_det
        {
        	position:relative;
        	width:8.5cm;
        	height:3.8cm;
        	top:0.3cm;
        	left:12.5cm;
        }
        .cod_op
        {
            position:relative;
            float:left;
            width:2cm;
            top:1.1cm;
            left:0.3cm;
        }
        .natureza_op
        {
        	position:relative;
        	float:left;
        	width:5.4cm;
        	overflow:auto;
        	font-size:9px;
        	top:1.1cm;
        	left:1cm;
        }
        .local_no_municipio
        {
        	position:absolute;
        	width:1cm;
        	top:1.6cm;
        	left:3.5cm;
        }
        .local_fora_municipio
        {
        	position:absolute;
        	width:1cm;
        	top:1.6cm;
        	left:6cm;
        }
        .data_emissao
        {
        	position:relative;
        	float:left;
        	width:6cm;
        	top:2.2cm;
        	left:0.5cm;
        }
        .dados_cliente
        {
        	position:relative;
        	top:0cm;
        	right:0.3cm;
        	left:0.3cm;
        	width:20.3cm;
        	height:4cm;
        }
        .cliente
        {
        	position:relative;
        	float:left;
        	width:14cm;
        	top:0.4cm;
        	left:1.8cm;
        }
        .cod
        {
        	position:relative;
        	float:left;
        	width:3cm;
        	top:0.4cm;
        	left:4cm;
        }
        .endereco
        {
        	position:relative;
        	float:left;
        	width:10cm;
        	top:0.6cm;
        	left:1.8cm;
        }
        .setor
        {
        	position:relative;
        	float:left;
        	width:5cm;
        	top:0.6cm;
        	left:4cm;
        }
        .cidade
        {
        	position:relative;
        	float:left;
        	width:11cm;
        	top:0.8cm;
        	left:1.8cm;
        }
        .uf
        {
        	position:relative;
        	float:left;
        	width:2cm;
        	top:0.8cm;
        	left:3.8cm;
        }
        .cep
        {
        	position:relative;
        	float:left;
        	width:3cm;
        	top:0.8cm;
        	left:5cm;
        }
        .cnpj_cpf
        {
        	position:relative;
        	float:left;
        	width:6cm;
        	top:1cm;
        	left:1.8cm;
        }
        .inscr_est
        {
        	position:relative;
        	float:left;
        	width:4cm;
        	top:1cm;
        	left:4cm;
        }
        .inscr_mun
        {
        	position:relative;
        	float:left;
        	width:4cm;
        	top:1cm;
        	left:7.5cm;
        }
        .fatura
        {
        	position:relative;
        	float:left;
        	width:18cm;
        	top:-0.6cm;
        	left:0.6cm;
        }
        .rodape
        {
        	position:relative;
        	bottom:-0.6cm;
        	right:-0.6cm;
        	left:0.6cm;
        	width:20.3cm;
        	height:3cm;
        }
        .total
        {
        	position:relative;
        	float:right;
        	width:3cm;
        	font-size:10px;
        	top:-0.3cm;
        	right:0.2cm;
        }
        .obs
        {
        	position:relative;
        	float:left;
        	width:10.5cm;
        	height:2cm;
        	top:0.5cm;
        	left:0.2cm;
        }
        TABLE.produtos_lista
        {
        	position: relative;
        	height:7.6cm;
        	top:0.3cm;
        	left:0.6cm;
        }
        TABLE.produtos_lista TD.col1
        {
        	width: 2.2cm;
        	font-size:10px;
        }
        TABLE.produtos_lista TD.col2
        {
        	width: 0.7cm;
        	font-size:10px;
        }
        TABLE.produtos_lista TD.col3
        {
        	width: 11.7cm;
        	font-size:10px;
        }
        TABLE.produtos_lista TD.col4
        {
        	width: 2.5cm;
        	font-size:10px;
        }
        TABLE.produtos_lista TD.col5
        {
        	width: 3.5cm;
        	font-size:10px;
        }
        
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div class="pagina">
        <div class="cab_det">
            <p id="pCodOp" runat="server" class="cod_op"></p>
            <p id="pNaturezaOp" runat="server" class="natureza_op"></p>
            <p id="pLocalNoMunicipio" runat="server" class="local_no_municipio"></p>
            <p id="pLocalForaMunicipio" runat="server" class="local_fora_municipio"></p>
            <p id="pDataEmissao" runat="server" class="data_emissao"></p>
        </div>
        <div class="dados_cliente">
            <p id="pCliente" runat="server" class="cliente"></p>
            <p id="pCod" runat="server" class="cod"></p>
            <p id="pEndereco" runat="server" class="endereco"></p>
            <p id="pSetor" runat="server" class="setor"></p>
            <p id="pCidade" runat="server" class="cidade"></p>
            <p id="pUf" runat="server" class="uf"></p>
            <p id="pCep" runat="server" class="cep"></p>
            <p id="pCnpjCpf" runat="server" class="cnpj_cpf"></p>
            <p id="pInscrEst" runat="server" class="inscr_est"></p>
            <p id="pInscrMun" runat="server" class="inscr_mun"></p>
        </div>
        <p id="pFatura" runat="server" class="fatura"></p>
        <asp:Table ID="tServicos" runat="server" CssClass="produtos_lista" />

        <div class="rodape">
            <p id="pTotal" runat="server" class="total"></p>
            <p id="pObs" runat="server" class="obs"></p>
        </div>
    </div>
    </form>
</body>
</html>