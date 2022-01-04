import Core.Alerta.AlertaSistema;
import Core.App;
import Core.Sessao;
import Core.Utils.Funcoes;
import Core.Utils.MyArrayUtils;

import SDE.Entidade.Item;
import SDE.Entidade.ItemEmpAliquotas;
import SDE.Entidade.ItemEmpPreco;

import flash.events.Event;

import mx.collections.ArrayCollection;
import mx.core.Application;
import mx.managers.PopUpManager;


	private var itemEdita:Item;
	private var iepEdita:ItemEmpPreco;
	private var ieaEdita:ItemEmpAliquotas;
	
	[Bindable] private var icmsCSTCodigo:Array = ['000','010','020','030','040','041','050','051','060','070','090'];
	private var icmsCSTTexto:Array;
	
	[Bindable] private var pis_cofinsCSTCodigo:Array = ['01','02','03','04','05','06','07','08','09','99'];
	private var pis_cofinsCSTTexto:Array;
	
	[Bindable] private var ipiCSTCodigo:Array = ['00','01','02','03','04','05','49','50','51','52','53','54','55','99'];
	private var ipiCSTTexto:Array;
	
	private var tipoIdentTextoEdi:Array;
	private var tipoCalculoTexto:Array;
	private var origemTexto:Array;
	
	[Bindable] private var dpLocacao:ArrayCollection;
	
	private var isEditando:Boolean;
	private var objetoOriginal:Object;
	
	private function iniciaEdita():void{
		tipoIdentTextoEdi = [];
		tipoIdentTextoEdi['grade'] = 'Grade';
		tipoIdentTextoEdi['identificador'] = 'Identificador';
		tipoIdentTextoEdi['lote'] = 'Lote';
		
		icmsCSTTexto = [];
		icmsCSTTexto['000'] = 'Tributada integralmente';
		icmsCSTTexto['010'] = 'Tributada e com cobrança do ICMS por substituição tributária';
		icmsCSTTexto['020'] = 'Com redução de base de cálculo';
		icmsCSTTexto['030'] = 'Isenta ou não tributada e com cobrança do ICMS por substituição tributária';
		icmsCSTTexto['040'] = 'Isenta';
		icmsCSTTexto['041'] = 'Não tributada';
		icmsCSTTexto['050'] = 'Suspensão';
		icmsCSTTexto['051'] = 'Diferimento';
		icmsCSTTexto['060'] = 'ICMS cobrado anteriormente por substituição tributária';
		icmsCSTTexto['070'] = 'Com redução de base de cálculo e cobrança do ICMS por substituição tributária';
		icmsCSTTexto['090'] = 'Outras';
		
		pis_cofinsCSTTexto = [];
		pis_cofinsCSTTexto['01'] = 'Operação Tributável - Base de Cálculo = Valor da Operação Alíquota Normal (Cumulativo/Não Cumulativo)';
		pis_cofinsCSTTexto['02'] = 'Operação Tributável - Base de Calculo = Valor da Operação (Alíquota Diferenciada)';
		pis_cofinsCSTTexto['03'] = 'Operação Tributável - Base de Calculo = Quantidade Vendida x Alíquota por Unidade de Produto';
		pis_cofinsCSTTexto['04'] = 'Operação Tributável - Tributação Monofásica - (Alíquota Zero)';
		pis_cofinsCSTTexto['05'] = 'Operação Tributável (substituição tributária)';
		pis_cofinsCSTTexto['06'] = 'Operação Tributável – Alíquota zero';
		pis_cofinsCSTTexto['07'] = 'Operação Isenta da contribuição';
		pis_cofinsCSTTexto['08'] = 'Operação Sem Incidência da contribuição';
		pis_cofinsCSTTexto['09'] = 'Operação com suspensão da contribuição';
		pis_cofinsCSTTexto['99'] = 'Outras Operações';
		
		ipiCSTTexto = [];
		ipiCSTTexto['00'] = 'Entrada com recuperação de crédito';
		ipiCSTTexto['01'] = 'Entrada tributada com alíquota zero';
		ipiCSTTexto['02'] = 'Entrada isenta';
		ipiCSTTexto['03'] = 'Entrada não-tributada';
		ipiCSTTexto['04'] = 'Entrada imune';
		ipiCSTTexto['05'] = 'Entrada com suspensão';
		ipiCSTTexto['49'] = 'Outras entradas';
		ipiCSTTexto['50'] = 'Saída tributada';
		ipiCSTTexto['51'] = 'Saída tributada com alíquota zero';
		ipiCSTTexto['52'] = 'Saída isenta';
		ipiCSTTexto['53'] = 'Saída não-tributada';
		ipiCSTTexto['54'] = 'Saída imune';
		ipiCSTTexto['55'] = 'Saída com suspensão';
		ipiCSTTexto['99'] = 'Outras saídas';
		
		tipoCalculoTexto = [];
		tipoCalculoTexto['percentual'] = 'Percentual';
		tipoCalculoTexto['valor_fixo'] = 'Valor Fixo';
		
		origemTexto = [];
		origemTexto['nacional'] = 'Nacional';
		origemTexto['internacional'] = 'Estrangeira - Importação direta';
		origemTexto['internacional_mi'] = 'Estrangeira - Adquirida no mercado interno';	
	}
	
	private function createEdita():void{
		cmbEdiSecao.dataProvider = secoes;
		cmbEdiMarca.dataProvider = marcas;
	}
	
	private function fn_ComboTipoIdentEdi_Label(tipoIdent:String):String{
		return tipoIdentTextoEdi[tipoIdent];
	}
	
	private function fn_ComboCST_ICMS_Label(cst_icms:String):String{
		return cst_icms+" | "+icmsCSTTexto[cst_icms];
	}
	
	private function fn_ComboPIS_COFINS_Label(cst_pis_cofins:String):String{
		return cst_pis_cofins+" | "+pis_cofinsCSTTexto[cst_pis_cofins];
	}
	
	private function fn_ComboCST_IPI_Label(cst_ipi:String):String{
		return cst_ipi+" | "+ipiCSTTexto[cst_ipi];
	}
	
	private function fn_ComboTipoCalculo_Label(tipoCalculo:String):String{
		return tipoCalculoTexto[tipoCalculo];
	}
	
	private function fn_ComboOrigem_Label(origem:String):String{
		return origemTexto[origem];
	}
	
	private function calculaProjecaoPrecoVenda():void{
		if (nsEdiNovaMargemLucro.value > 0)
			nsEdiProPrecoVenda.value = ((nsEdiCusto.value / 100) * nsEdiNovaMargemLucro.value) + nsEdiCusto.value;
	}
	
	private function calculaProjecaoMargemLucro():void{
		var diferenca:Number = nsEdiNovoPrecoVenda.value - nsEdiCusto.value;
		diferenca = diferenca * 100;
		nsEdiProMargemLucro.value = diferenca / nsEdiCusto.value;
	}
	
	private function btnConfirmaMargemLucro():void{
		nsEdiMargemLucro.value = nsEdiNovaMargemLucro.value;
		nsEdiVenda.value = nsEdiProPrecoVenda.value;
		limpaEdicaoPrecos();
	}
	
	private function btnConfirmaPrecoVenda():void{
		nsEdiVenda.value = nsEdiNovoPrecoVenda.value;
		nsEdiAtac.value = nsEdiNovoPrecoAtac.value;
		nsEdiMargemLucro.value = nsEdiProMargemLucro.value;
		limpaEdicaoPrecos();
	}
	
	private function limpaEdicaoPrecos():void{
		nsEdiNovaMargemLucro.value = 0;
		nsEdiProPrecoVenda.value = 0;
		nsEdiNovoPrecoVenda.value = 0;
		nsEdiProMargemLucro.value = 0;
		nsEdiNovoPrecoAtac.value = 0;
	}
	
	public function importaItemSelecionado(idItem:Number):void{
		
		itemEdita = App.single.cache.getItem(idItem).clone();
		
		for each (var iep:ItemEmpPreco in App.single.cache.arrayItemEmpPreco){
			if (iep.idItem != itemEdita.id || iep.idEmp != Sessao.unica.idEmp)
				continue;
			iepEdita = iep.clone();
			break;
		}
		
		for each (var iea:ItemEmpAliquotas in App.single.cache.arrayItemEmpAliquotas){
			if (iea.idItem != itemEdita.id || iea.idEmp != Sessao.unica.idEmp)
				continue;
			ieaEdita = iea.clone();
			break;
		}
		
		lblEdiTitulo.text = "Código do Item: " + itemEdita.id;
		
		preencheSecaoMarca();
		carregaLocacao();
		doBinding();
		
		vsProduto.selectedIndex = 0;
	}
	
	private function doBinding():void{
		/**ITEM*/
		/**ESTE BIND FOI REMOVIDO POR APRESENTAR PROBLEMAS, AS PROPRIEDades NÃO ESTÃO MAIS LIGADAS*/
		
		//update: este data binding foi mantido, pois não apresenta problemas
		Funcoes.myBind(txtEdiNome, "text", itemEdita, "nome");
		Funcoes.myBind(txtEdiCodUnico, "text", itemEdita, "rfUnica");
		Funcoes.myBind(txtEdiCodAux, "text", itemEdita, "rfAuxiliar");
		Funcoes.myBind(cmbEdiUn, "selectedItem", itemEdita, "unidMed");
		Funcoes.myBind(cmbEdiTipoIdent, "selectedItem", itemEdita, "tipoIdent");
		Funcoes.myBind(nsEdiRfPeso, "value", itemEdita, "rfPeso");
		Funcoes.myBind(ckbEdiDesuso, "selected", itemEdita, "desuso");
		Funcoes.myBind(txtEdiObs, "text", itemEdita, "complAplic");
		Funcoes.myBind(txtNomeEtiqueta, "text", itemEdita, "nomeEtiqueta");
		
		/*
		txtEdiNome.text = itemEdita.nome;
		txtEdiCodUnico.text = itemEdita.rfUnica;
		txtEdiCodAux.text = itemEdita.rfAuxiliar;
		cmbEdiUn.selectedItem = itemEdita.unidMed;
		cmbEdiTipoIdent.selectedItem = itemEdita.tipoIdent;
		nsEdiRfPeso.value = itemEdita.rfPeso;
		ckbEdiDesuso.selected = itemEdita.desuso;
		txtEdiObs.text = itemEdita.complAplic;
		txtNomeEtiqueta.text = itemEdita.nomeEtiqueta;
		*/
		
		/**PREÇOS*/
		/**ESTE BIND FOI REMOVIDO POR APRESENTAR PROBLEMAS, AS PROPRIEDades NÃO ESTÃO MAIS LIGADAS*/
		/*
		Funcoes.myBind(nsEdiDescontoMaximo, "value", iepEdita, "descontoMaximo");
		Funcoes.myBind(nsEdiComissao, "value", iepEdita, "pctComissao");
		Funcoes.myBind(nsEdiCompra, "value", iepEdita, "compra");
		Funcoes.myBind(nsEdiCusto, "value", iepEdita, "custo");
		Funcoes.myBind(nsEdiMargemLucro, "value", iepEdita, "margemLucro");
		Funcoes.myBind(nsEdiVenda, "value", iepEdita, "venda");
		*/
		nsEdiDescontoMaximo.value = iepEdita.descontoMaximo;
		nsEdiComissao.value = iepEdita.pctComissao;
		nsEdiCompra.value = iepEdita.compra;
		nsEdiCusto.value = iepEdita.custo;
		nsEdiMargemLucro.value = iepEdita.margemLucro;
		nsEdiVenda.value = iepEdita.venda;
		nsEdiAtac.value = iepEdita.atacado;
		
		/**TRIBUTAÇÃO*/
		/**ESTE BIND FOI REMOVIDO POR APRESENTAR PROBLEMAS, AS PROPRIEDades NÃO ESTÃO MAIS LIGADAS*/
		/*
		Funcoes.myBind(nsEdiICMSPadraoED, "value", ieaEdita, "icmsAliqPadrao_ED");
		Funcoes.myBind(nsEdiICMSReduzidoED, "value", ieaEdita, "icmsAliq_ED");
		Funcoes.myBind(cmbEdiCSTED, "selectedItem", ieaEdita, "icmsCST_ED");
		Funcoes.myBind(txtEdiObsED, "text", ieaEdita, "icmsObs_ED");
		*/
		nsEdiICMSPadraoED.value = ieaEdita.icmsAliqPadrao_ED;
		nsEdiICMSReduzidoED.value = ieaEdita.icmsAliq_ED;
		cmbEdiCSTED.selectedItem = ieaEdita.icmsCST_ED;
		txtEdiObsED.text = ieaEdita.icmsObs_ED;
		
		
		/** ENTRADA FORA */
		/**ESTE BIND FOI REMOVIDO POR APRESENTAR PROBLEMAS, AS PROPRIEDades NÃO ESTÃO MAIS LIGADAS*/
		/*
		Funcoes.myBind(nsEdiICMSPadraoEF, "value", ieaEdita, "icmsAliqPadrao_EF");
		Funcoes.myBind(nsEdiICMSReduzidoEF, "value", ieaEdita, "icmsAliq_EF");
		Funcoes.myBind(cmbEdiCSTEF, "selectedItem", ieaEdita, "icmsCST_EF");
		Funcoes.myBind(txtEdiObsEF, "text", ieaEdita, "icmsObs_EF");
		*/
		nsEdiICMSPadraoEF.value = ieaEdita.icmsAliqPadrao_EF;
		nsEdiICMSReduzidoEF.value = ieaEdita.icmsAliq_EF;
		cmbEdiCSTEF.selectedItem = ieaEdita.icmsCST_EF;
		txtEdiObsEF.text = ieaEdita.icmsObs_EF;
		
		/** SAIDA DENTRO */
		/**ESTE BIND FOI REMOVIDO POR APRESENTAR PROBLEMAS, AS PROPRIEDades NÃO ESTÃO MAIS LIGADAS*/
		/*
		Funcoes.myBind(nsEdiICMSPadraoSD, "value", ieaEdita, "icmsAliqPadrao_SD");
		Funcoes.myBind(nsEdiICMSReduzidoSD, "value", ieaEdita, "icmsAliq_SD");
		Funcoes.myBind(cmbEdiCSTSD, "selectedItem", ieaEdita, "icmsCST_SD");
		Funcoes.myBind(txtEdiObsSD, "text", ieaEdita, "icmsObs_SD");
		*/
		nsEdiICMSPadraoSD.value = ieaEdita.icmsAliqPadrao_SD;
		nsEdiICMSReduzidoSD.value = ieaEdita.icmsAliq_SD;
		cmbEdiCSTSD.selectedItem = ieaEdita.icmsCST_SD;
		txtEdiObsSD.text = ieaEdita.icmsObs_SD;
		
		/** SAIDA FORA */
		/**ESTE BIND FOI REMOVIDO POR APRESENTAR PROBLEMAS, AS PROPRIEDades NÃO ESTÃO MAIS LIGADAS*/
		/*
		Funcoes.myBind(nsEdiICMSPadraoSF, "value", ieaEdita, "icmsAliqPadrao_SF");
		Funcoes.myBind(nsEdiICMSReduzidoSF, "value", ieaEdita, "icmsAliq_SF");
		Funcoes.myBind(cmbEdiCSTSF, "selectedItem", ieaEdita, "icmsCST_SF");
		Funcoes.myBind(txtEdiObsSF, "text", ieaEdita, "icmsObs_SF");
		*/
		nsEdiICMSPadraoSF.value = ieaEdita.icmsAliqPadrao_SF;
		nsEdiICMSReduzidoSF.value = ieaEdita.icmsAliq_SF;
		cmbEdiCSTSF.selectedItem = ieaEdita.icmsCST_SF;
		txtEdiObsSF.text = ieaEdita.icmsObs_SF;
		
		/** PIS */
		/**ESTE BIND FOI REMOVIDO POR APRESENTAR PROBLEMAS, AS PROPRIEDades NÃO ESTÃO MAIS LIGADAS*/
		/*
		Funcoes.myBind(cmbEdiCSTPIS, "selectedItem", ieaEdita, "pisCST");
		Funcoes.myBind(nsEdiAliqPadraoCSTPIS, "value", ieaEdita, "pisAliqPadrao");
		Funcoes.myBind(nsEdiAliqReduzidaCSTPIS, "value", ieaEdita, "pisAliq");
		*/
		cmbEdiCSTPIS.selectedItem = ieaEdita.pisCST;
		nsEdiAliqPadraoCSTPIS.value = ieaEdita.pisAliqPadrao;
		nsEdiAliqReduzidaCSTPIS.value = ieaEdita.pisAliq;
		
		/** COFINS */
		/**ESTE BIND FOI REMOVIDO POR APRESENTAR PROBLEMAS, AS PROPRIEDades NÃO ESTÃO MAIS LIGADAS*/
		/*
		Funcoes.myBind(cmbEdiCSTCOFINS, "selectedItem", ieaEdita, "cofinsCST");
		Funcoes.myBind(nsEdiAliqPadraoCSTCOFINS, "value", ieaEdita, "cofinsAliqPadrao");
		Funcoes.myBind(nsEdiAliqReduzidaCSTCOFINS, "value", ieaEdita, "cofinsAliq");
		*/
		cmbEdiCSTCOFINS.selectedItem = ieaEdita.cofinsCST;
		nsEdiAliqPadraoCSTCOFINS.value = ieaEdita.cofinsAliqPadrao;
		nsEdiAliqReduzidaCSTCOFINS.value = ieaEdita.cofinsAliq;
		
		/** IPI */
		/**ESTE BIND FOI REMOVIDO POR APRESENTAR PROBLEMAS, AS PROPRIEDades NÃO ESTÃO MAIS LIGADAS*/
		/*
		Funcoes.myBind(cmbEdiCSTIPI, "selectedItem", ieaEdita, "ipiCST");
		Funcoes.myBind(txtEdiCNPJ, "text", ieaEdita, "ipiCNPJ");
		Funcoes.myBind(txtEdiClasseEnquadramento, "text", ieaEdita, "ipiClasseEnquad");
		Funcoes.myBind(txtEdiCodigoEnquadramento, "text", ieaEdita, "ipiCodEnquad");
		Funcoes.myBind(txtEdiCodigoSeloControle, "text", ieaEdita, "ipiCodSelo");
		Funcoes.myBind(cmbEdiTipoCauculo, "selectedItem", ieaEdita, "ipiTipoCalculo");
		Funcoes.myBind(nsEdiAliquotaPadraoIPI, "value", ieaEdita, "ipiAliqPadrao");
		Funcoes.myBind(nsEdiAliquotaReduzidaIPI, "value", ieaEdita, "ipiAliq");
		*/
		cmbEdiCSTIPI.selectedItem = ieaEdita.ipiCST;
		txtEdiCNPJ.text = (ieaEdita.ipiCNPJ == null) ? "" : ieaEdita.ipiCNPJ ;
		txtEdiClasseEnquadramento.text = ieaEdita.ipiClasseEnquad;
		txtEdiCodigoEnquadramento.text = ieaEdita.ipiCodEnquad;
		txtEdiCodigoSeloControle.text = ieaEdita.ipiCodSelo;
		cmbEdiTipoCauculo.selectedItem = ieaEdita.ipiTipoCalculo;
		nsEdiAliquotaPadraoIPI.value = ieaEdita.ipiAliqPadrao;
		nsEdiAliquotaReduzidaIPI.value = ieaEdita.ipiAliq;
		
		/** OUTROS */
		/**ESTE BIND FOI REMOVIDO POR APRESENTAR PROBLEMAS, AS PROPRIEDades NÃO ESTÃO MAIS LIGADAS*/
		/*
		Core.Utils.Funcoes.myBind(cmbEdiOrigem, "selectedItem", itemEdita, "origem");
		Core.Utils.Funcoes.myBind(txtEdiClassFiscal, "text", itemEdita, "classificacaoFiscal");
		*/
		cmbEdiOrigem.selectedItem = itemEdita.origem;
		txtEdiClassFiscal.text = itemEdita.classificacaoFiscal;
	}
	
	private function preencheSecaoMarca():void{
		var dictSecoes:Array = MyArrayUtils.asDictionary(secoes);
		var dictMarcas:Array = MyArrayUtils.asDictionary(marcas);
		
		if (itemEdita.idSecao > 0)
			cmbEdiSecao.selectedItem = dictSecoes[itemEdita.idSecao];
		else
			cmbEdiSecao.selectedIndex = 0;
		if (itemEdita.idMarca > 0)
			cmbEdiMarca.selectedItem = dictMarcas[itemEdita.idMarca];
		else
			cmbEdiMarca.selectedIndex = 0;
	}
	
	private function atualizaSecaoMarca():void{
		itemEdita.idSecao = cmbEdiSecao.selectedItem.id;
		itemEdita.grupo = cmbEdiSecao.selectedItem.grupo;
		itemEdita.subgrupo = cmbEdiSecao.selectedItem.subgrupo;
		itemEdita.secao = cmbEdiSecao.selectedItem.secao;
		
		itemEdita.idMarca = cmbEdiMarca.selectedItem.id;
		itemEdita.modelo = cmbEdiMarca.selectedItem.modelo;
		itemEdita.marca = cmbEdiMarca.selectedItem.marca;
	}
	
	/**INICIO CODIGO LOCAÇÃO*/
	
	private function abrePopupLocacao():void{
		PopUpManager.addPopUp(popupLocacao, Application.application.gerenteJanelas, true);
		PopUpManager.centerPopUp(popupLocacao);
		txtEdiLocacao.setFocus();
	}
	
	private function carregaLocacao():void{
		if (dpLocacao == null)
			dpLocacao = new ArrayCollection();
		else
			dpLocacao.removeAll();
		for (var i:Number = 0 ; i < 10 ; i++){
			var campo:String = "locacao" + (i + 1).toString();
			
			if (itemEdita[campo] != null && itemEdita[campo] != ""){
				var obj:Object = new Object();
				obj.campo = campo;
				obj.valor = itemEdita[campo];
				dpLocacao.addItem(obj);
			}
		}
	}
	
	private function btnAdicionaLocacao_Click():void{
		if (dpLocacao.length == 10 || dpLocacao.length > 10)
		{
			AlertaSistema.mensagem("Quantidade máxima de locações atingida");
			return;
		}
		isEditando = false;
		abrePopupLocacao();
	}
	
	private function btnSalvarLocal_Click():void{
		if (isEditando)
			editaLocal();
		else
			adicionaLocal();
	}
	
	private function adicionaLocal():void{
		if (txtEdiLocacao.text == ""){
			AlertaSistema.mensagem("Digite a locação");
			return;
		}
		
		if (dpLocacao == null)
			dpLocacao = new ArrayCollection();
		
		var campo:String = "locacao" + (dpLocacao.length + 1).toString();
		itemEdita[campo] = txtEdiLocacao.text;
		
		var obj:Object = new Object();
		obj.campo = campo;
		obj.valor = itemEdita[campo];
		dpLocacao.addItem(obj);
		
		popupLocacao.parent.removeChild(popupLocacao);
		
		txtEdiLocacao.text = "";
	}
	
	private function editaLocal():void{
		for each (var obj:Object in dpLocacao){
			if (obj.campo == objetoOriginal.campo){
				obj.valor = txtEdiLocacao.text;
				itemEdita[obj.campo] = txtEdiLocacao.text;
			}
		}
		
		gridLocacao.dataProvider = dpLocacao;
		
		popupLocacao.parent.removeChild(popupLocacao);
		txtEdiLocacao.text = "";
	}
	
	private function editRowHandler(ev:Event):void{
		objetoOriginal = new Object();
		objetoOriginal.campo = ev.target.data.campo;
		objetoOriginal.valor = ev.target.data.valor;
		abrePopupLocacao();
		txtEdiLocacao.text = objetoOriginal.valor;
		isEditando = true;
	}
	
	private function deleteRowHandler(ev:Event):void{
		var index:int = dpLocacao.getItemIndex(ev.target.data);
		dpLocacao.removeItemAt(index);
		
		var i:Number;
		var campo:String;
		
		for (i = 0 ; i < 10 ; i++){
			campo = "locacao" + (i + 1).toString();
			itemEdita[campo] = null;
		}
		
		for (i = 0; i < dpLocacao.length ; i++){
			campo = "locacao" + (i + 1).toString();
			itemEdita[campo] = dpLocacao.getItemAt(i).valor;
		}
		
		var novoDp:ArrayCollection = new ArrayCollection();
		for (i = 0 ; i < dpLocacao.length ; i++){
			campo = "locacao" + (i + 1).toString();
			
			if (itemEdita[campo] != null){
				var obj:Object = new Object();
				obj.campo = campo;
				obj.valor = itemEdita[campo];
				novoDp.addItem(obj);
			}
		}
		dpLocacao = novoDp;
	}
	
	/**FIM CODIGO LOCAÇÃO*/
	
	private function btnNaoSalvar_Click():void{
		mudaTela(telaConsulta);
	}
	
	private function calculaPrecoIPICUSTO():void
	{
		var calcIPI:Number;
		var retorno:Number;
		
		calcIPI = nsCadPrecoCompra.value + (nsCadPrecoCompra.value * nsCadIPIperc.value)/100;	
		retorno = calcIPI + (calcIPI * nsCadOutrosCustos.value)/100;
	
		nsCadPrecoCusto.value = retorno;	
	}
	
	private function calculaPrecoIPICUSTO2():void
	{
		var calcIPI2:Number;
		var retorno2:Number;
		
		calcIPI2 = nsEdiCompra.value + (nsEdiCompra.value * nsEdiIPIperc.value)/100;	
		retorno2 = calcIPI2 + (calcIPI2 * nsEdiOutrosCustos.value)/100;
	
		nsEdiCusto.value = retorno2;	
	}
	
	private function btnSalvaAlteracao_Click(continuar:Boolean):void{
		var msg:String = "";
		if (txtEdiCodUnico.text == '')
			msg += "Digite uma REFERENCIA\n";
		if (txtEdiNome.text == '')
			msg += "Digite uma DESCRIÇÃO\n";		
		if (nsEdiCusto.value == 0)
			msg += "Digite um PREÇO DE CUSTO";
		/* if (nsEdiVenda.value == 0)
			msg += "Digite um PREÇO DE VENDA"; */
		if (msg != ""){
			AlertaSistema.mensagem(msg);
			return;
		}
		
		/*
		itemEdita.nome = txtEdiNome.text;
		itemEdita.rfUnica = txtEdiCodUnico.text;
		itemEdita.rfAuxiliar = txtEdiCodAux.text;
		itemEdita.unidMed = cmbEdiUn.selectedItem;
		itemEdita.tipoIdent = cmbEdiTipoIdent.selectedItem;
		itemEdita.rfPeso = nsEdiRfPeso.value;
		itemEdita.desuso = ckbEdiDesuso.selected;
		itemEdita.complAplic = txtEdiObs.text;
		itemEdita.nomeEtiqueta = txtNomeEtiqueta.text;
		*/
		
		iepEdita.descontoMaximo = nsEdiDescontoMaximo.value;
		iepEdita.pctComissao = nsEdiComissao.value;
		iepEdita.compra = nsEdiCompra.value;
		iepEdita.custo = nsEdiCusto.value;
		iepEdita.margemLucro = nsEdiMargemLucro.value;
		iepEdita.venda = nsEdiVenda.value;
		iepEdita.atacado = nsEdiAtac.value;
		
		ieaEdita.icmsAliqPadrao_ED = nsEdiICMSPadraoED.value;
		ieaEdita.icmsAliq_ED = nsEdiICMSReduzidoED.value;
		ieaEdita.icmsCST_ED = cmbEdiCSTED.selectedItem.toString();
		ieaEdita.icmsObs_ED = txtEdiObsED.text;
		ieaEdita.icmsAliqPadrao_EF = nsEdiICMSPadraoEF.value;
		ieaEdita.icmsAliq_EF = nsEdiICMSReduzidoEF.value;
		ieaEdita.icmsCST_EF = cmbEdiCSTEF.selectedItem.toString();
		ieaEdita.icmsObs_EF = txtEdiObsEF.text;
		ieaEdita.icmsAliqPadrao_SD = nsEdiICMSPadraoSD.value;
		ieaEdita.icmsAliq_SD = nsEdiICMSReduzidoSD.value;
		ieaEdita.icmsCST_SD = cmbEdiCSTSD.selectedItem.toString();
		ieaEdita.icmsObs_SD = txtEdiObsSD.text;
		ieaEdita.icmsAliqPadrao_SF = nsEdiICMSPadraoSF.value;
		ieaEdita.icmsAliq_SF = nsEdiICMSReduzidoSF.value;
		ieaEdita.icmsCST_SF = cmbEdiCSTSF.selectedItem.toString();
		ieaEdita.icmsObs_SF = txtEdiObsSF.text;
		ieaEdita.pisCST = cmbEdiCSTPIS.selectedItem.toString();
		ieaEdita.pisAliqPadrao = nsEdiAliqPadraoCSTPIS.value;
		ieaEdita.pisAliq = nsEdiAliqReduzidaCSTPIS.value;
		ieaEdita.cofinsCST = cmbEdiCSTCOFINS.selectedItem.toString();
		ieaEdita.cofinsAliqPadrao = nsEdiAliqPadraoCSTCOFINS.value;
		ieaEdita.cofinsAliq = nsEdiAliqReduzidaCSTCOFINS.value;
		ieaEdita.ipiCST = cmbEdiCSTIPI.selectedItem.toString();
		ieaEdita.ipiCNPJ = txtEdiCNPJ.text;
		ieaEdita.ipiClasseEnquad = txtEdiClasseEnquadramento.text;
		ieaEdita.ipiCodEnquad = txtEdiCodigoEnquadramento.text;
		ieaEdita.ipiCodSelo = txtEdiCodigoSeloControle.text;
		ieaEdita.ipiTipoCalculo = cmbEdiTipoCauculo.selectedItem.toString();
		ieaEdita.ipiAliqPadrao = nsEdiAliquotaPadraoIPI.value;
		ieaEdita.ipiAliq = nsEdiAliquotaReduzidaIPI.value;
		itemEdita.origem = cmbEdiOrigem.selectedItem.toString();
		itemEdita.classificacaoFiscal = txtEdiClassFiscal.text;
		
		atualizaSecaoMarca();
		
		App.single.n.modificacoes.ItemAtualiza(itemEdita, iepEdita, ieaEdita,
			function (idItem:Number):void{
				if (continuar)
					importaItemSelecionado(idItem);
				else{
					itemEdita = new Item();
					iepEdita = new ItemEmpPreco();
					ieaEdita = new ItemEmpAliquotas();
					mudaTela(telaConsulta);
				}
			}
		);
	}