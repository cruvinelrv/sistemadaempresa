<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="notatotalaco.aspx.cs" Inherits="SDE.notatotalaco" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>SDE SISTEMA</title>
    <meta http-equiv="Content-Type"
        content="text/html;charset=UTF-8" />
    <style>
        *{ border:none; margin: 0; padding:0;
           font-size:11px;
           font-family:Roman;
           font-weight:bold;
        }
        .pagina
        {
        	width: 21.2cm;
        	height: 28cm;
        }
        .numero_nf
        {
            position:relative;
            float:right;
            top:0.5cm;
            right:1.5cm;
        }
        .numero_nf_rodape
        {
            position:relative;
            float:right;
            top:4.5cm;
            right:4cm;
        }
        .bloco1
        {
            position:relative;
            top:1.4cm;
            left:1.7cm;
            width:15.9cm;
            height:2cm;
        }
        .bloco2
        {
            position:relative;
            float:left;
            top:2.3cm;
            left:1.7cm;
            width:15.9cm;
            height:3cm;
        }
        .bloco3
        {
            position:relative;
            float:left;
            top:2.3cm;
            left:2cm;
            width:2.5cm;
            height:2cm;
        }
        .bloco4
        {
            position:relative;
            float:left;
            top:3.1cm;
            left:1.7cm;
            width:18.7cm;
            height:1.5cm;
        }
        .bloco5
        {
            position:relative;
            top:3.2cm;
            left:1.7cm;
            width:18.7cm;
            height:2cm;
        }
        .bloco6
        {
            position:relative;
            top:3.3cm;
            left:1.7cm;
            width:8.5cm;
            height:3cm;
        }
        
        
        /*<!---->*/
        
        
        .saida
        {
            position:absolute;
            top:0cm;
            left:12.3cm;
            width:0.5cm;
        }
        .entrada
        {
            position:absolute;
            top:0cm;
            left:14.1cm;
            width:0.3cm;
        }
        .natureza_op
        {
            position:relative;
            float:left;
            top:2.1cm;
            width:5.7cm;
        }
        .cfop
        {
            position:relative;
            float:left;
            top:2.1cm;
            width:1.3cm;
            overflow:hidden;
        }
        
        /*<!---->*/
        
        
        .nome_razSocial_dest_remet
        {
            position:relative;
            float:left;
            top:0.2cm;
            width:12cm;
            left:0cm;
        }
        .cnpj_cpf_dest_remet
        {
            position:relative;
            float:left;
            top:0.2cm;
            width:3.8cm;
        }
        
        .endereco_dest_remet
        {
            position:relative;
            float:left;
            top:0.5cm;
            width:9.4cm;
            left:0cm;
        }
        .bairro_distrito_dest_remet
        {
            position:relative;
            float:left;
            top:0.5cm;
            width:4cm;
        }
        .cep_dest_remet
        {
            position:relative;
            float:left;
            top:0.5cm;
            width:2.2cm;
        }
        
        .municipio_dest_remet
        {
            position:relative;
            float:left;
            top:0.8cm;
            width:8.7cm;
            left:0cm;
        }
        .fone_fax_dest_remet
        {
            position:relative;
            float:left;
            top:0.8cm;
            width:2.3cm;
        }
        .uf_dest_remet
        {
            position:relative;
            float:left;
            top:0.8cm;
            width:0.9cm;
        }
        .inscr_estadual_dest_remet
        {
            position:relative;
            float:left;
            top:0.8cm;
            width:3.8cm;
        }
        .fatura
        {
            position:relative;
            float:left;
            top:1.4cm;
            width:100%;
            font-size:13px;
        }
        
        /*<!---->*/
        
        .data_emissao
        {
            position:relative;
            left:0cm;
            width:2.5cm;
            top:0.2cm;
        }
        .data_saida_entrada
        {
            position:relative;
            left:0cm;
            width:2.5cm;
            top:0.5cm;
        }
        .hora_saida
        {
            position:relative;
            left:0cm;
            width:2.5cm;
            top:0.8cm
        }
        
        /*<!---->*/
        
        .espaco_bloco4
        {
            position:relative;
            float:left;
            top:0.2cm;
            width:7.1cm;
            left:0cm;
        }
        .base_calculo_icms_sub
        {
            position:relative;
            float:left;
            top:0.2cm;
            width:3.6cm;
        }
        .valor_icms_sub
        {
            position:relative;
            float:left;
            top:0.2cm;
            width:4cm;
        }
        .valor_total_produtos
        {
            position:relative;
            float:left;
            top:0.2cm;
            width:3.5cm;
            text-align:right;
        }
        
        .valor_frete
        {
            position:relative;
            float:left;
            top:0.5cm;
            width:3.5cm;
            left:0cm;
        }
        .valor_seguro
        {
            position:relative;
            float:left;
            top:0.5cm;
            width:3.6cm;
        }
        .outras_desp_acessorias
        {
            position:relative;
            float:left;
            top:0.5cm;
            width:7.6cm;
        }
        .valor_total_nota
        {
            position:relative;
            float:left;
            top:0.5cm;
            width:3.5cm;
            text-align:right;
        }
        
        /*<!---->*/
        
        .nome_razSocial_transp
        {
            position:relative;
            float:left;
            top:0.2cm;
            width:10.8cm;
            left:0cm;
        }
        .frete_por_conta_transp
        {
            position:relative;
            float:left;
            top:0.2cm;
            width:0.5cm;
        }
        .placa_veiculo_transp
        {
            position:relative;
            float:left;
            top:0.2cm;
            width:2.5cm;
        }
        .uf_veiculo_transp
        {
            position:relative;
            float:left;
            top:0.2cm;
            width:0.9cm;
        }
        .cnpj_cpf_transp
        {
            position:relative;
            float:left;
            top:0.2cm;
            width:3.7cm;
        }
        .endereco_transp
        {
            position:relative;
            float:left;
            top:0.5cm;
            width:8.7cm;
            left:0cm;
        }
        .municipio_transp
        {
            position:relative;
            float:left;
            top:0.5cm;
            width:5.1cm;
        }
        .uf_transp
        {
            position:relative;
            float:left;
            top:0.5cm;
            width:0.9cm;
        }
        .inscr_estadual_transp
        {
            position:relative;
            float:left;
            top:0.5cm;
            width:3.7cm;
        }
        
        .quantidade_transp
        {
            position:relative;
            float:left;
            top:0.8cm;
            width:1.8cm;
            left:0cm;
        }
        .especie_transp
        {
            position:relative;
            float:left;
            top:0.8cm;
            width:7cm;
        }
        .marca_transp
        {
            position:relative;
            float:left;
            top:0.8cm;
            width:3.3cm;
        }
        .numero_transp
        {
            position:relative;
            float:left;
            top:0.8cm;
            width:1.3cm;
        }
        .peso_bruto_transp
        {
            position:relative;
            float:left;
            top:0.8cm;
            width:2.5cm;
        }
        .peso_liquido_transp
        {
            position:relative;
            float:left;
            top:0.8cm;
            width:2.5cm;
        }
        
        /*<!---->*/
        
        .dados_adicionais
        {
            position:relative;
            float:left;
            top:0.4cm;
            width:8.5cm;
            height:2.5cm;
            left:0cm;
            overflow:hidden;
        }
        
        TABLE.produtos
        {
            position:relative;
            top:2.7cm;
            left:1.7cm;
            width:18.6cm;
            height:10.8cm;
        }
        TABLE.produtos TD.col1
        {
            width: 1.7cm;
            text-align:left;
        }
        TABLE.produtos TD.col2
        {
            width: 6.6cm;
            text-align:left;
        }
        TABLE.produtos TD.col3
        {
            width: 1.1cm;
            text-align:left;
        }
        TABLE.produtos TD.col4
        {
            width: 0.7cm;
            text-align:left;
        }
        TABLE.produtos TD.col5
        {
            width: 0.7cm;
            text-align:left;
        }
        TABLE.produtos TD.col6
        {
            width: 1.2cm;
            text-align:left;
        }
        TABLE.produtos TD.col7
        {
            width: 2.2cm;
            text-align:left;
        }
        TABLE.produtos TD.col8
        {
            width: 2.5cm;
            text-align:left;
        }
        TABLE.produtos TD.col9
        {
            width:0.7cm;
            text-align:left;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div class="pagina">
        
        <p id="pNumeroNf" runat="server" class="numero_nf" />
        <div class="bloco1">
            <p id="pSaida" runat="server" class="saida" />
            <p id="pEntrada" runat="server" class="entrada" />
            <p id="pNaturezaOp" runat="server" class="natureza_op" />
            <p id="pCFOP" runat="server" class="cfop" />
        </div>
        <div class="bloco2">
            <p id="pNomeRazSocialDestRemet" runat="server" class="nome_razSocial_dest_remet" />
            <p id="pCnpjCpfDestRemet" runat="server" class="cnpj_cpf_dest_remet" />
            <p id="pEnderecoDestRemet" runat="server" class="endereco_dest_remet" />
            <p id="pBairroDistritoDestRemet" runat="server" class="bairro_distrito_dest_remet" />
            <p id="pCepDestRemet" runat="server" class="cep_dest_remet" />
            <p id="pMunicipioDestRemet" runat="server" class="municipio_dest_remet" />
            <p id="pFoneFaxDestRemet" runat="server" class="fone_fax_dest_remet" />
            <p id="pUfDestRemet" runat="server" class="uf_dest_remet" />
            <p id="pInsrcEstadualDestRemet" runat="server" class="inscr_estadual_dest_remet" />
            <p id="pFatura" runat="server" class="fatura" />
        </div>
        <div class="bloco3">
            <p id="pDataEmissao" runat="server" class="data_emissao" />
            <p id="pDataSaidaEntrada" runat="server" class="data_saida_entrada" />
            <p id="pHoraSaida" runat="server" class="hora_saida" />
        </div>
        
        <asp:Table ID="tProdutos" runat="server" CssClass="produtos" />
        
        <div class="bloco4">
            <p id="pEspacoBloco4" runat="server" class="espaco_bloco4" />
            <p id="pBaseCalculoIcmsSub" runat="server" class="base_calculo_icms_sub" />
            <p id="pValorIcmsSub" runat="server" class="valor_icms_sub" />
            <p id="pValorTotaProdutos" runat="server" class="valor_total_produtos" />
            <p id="pValorFrete" runat="server" class="valor_frete" />
            <p id="pValorSeguro" runat="server" class="valor_seguro" />
            <p id="pOutrasDespAcessorias" runat="server" class="outras_desp_acessorias" />
            <p id="pValorToralNota" runat="server" class="valor_total_nota" />
        </div>
        
        <div class="bloco5">
            <p id="pNomeRazSocialTransp" runat="server" class="nome_razSocial_transp" />
            <p id="pFretePorContaTransp" runat="server" class="frete_por_conta_transp" />
            <p id="pPlacaVeiculoTransp" runat="server" class="placa_veiculo_transp" />
            <p id="pUfVeiculoTransp" runat="server" class="uf_veiculo_transp" />
            <p id="pCpfCnpjTransp" runat="server" class="cnpj_cpf_transp" />
            <p id="pEnderecoTransp" runat="server" class="endereco_transp" />
            <p id="pMunicipioTrans" runat="server" class="municipio_transp" />
            <p id="pUfTransp" runat="server" class="uf_transp" />
            <p id="pInsrEstadualTransp" runat="server" class="inscr_estadual_transp" />
            <p id="pQuantidadeTransp" runat="server" class="quantidade_transp" />
            <p id="pEspecieTransp" runat="server" class="especie_transp" />
            <p id="pMarcaTrans" runat="server" class="marca_transp" />
            <p id="pNumeroTransp" runat="server" class="numero_transp" />
            <p id="pPesoBrutoTransp" runat="server" class="peso_bruto_transp" />
            <p id="pPesoLiquidoTransp" runat="server" class="peso_liquido_transp" />
        </div>
        
        <div class="bloco6">
            <p id="pDadosAdicionais" runat="server" class="dados_adicionais" />
        </div>
        
        <p id="pNumeroNfRodape" runat="server" class="numero_nf_rodape" />
        
    </div>
    </form>
</body>
</html>
