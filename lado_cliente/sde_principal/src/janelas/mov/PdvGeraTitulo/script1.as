	import Core.Alerta.AlertaSistema;
	import Core.App;
	import Core.Sessao;
	import Core.Utils.Formatadores;
	import Core.Utils.MeuFiltroWhere;
	import Core.Utils.MyArrayUtils;
	
	import SDE.CamadaNuvem.NuvemCache;
	import SDE.Constantes.Variaveis_SdeConfig;
	import SDE.Entidade.CFOP;
	import SDE.Entidade.Cliente;
	import SDE.Entidade.Cx_Lancamento;
	import SDE.Entidade.Item;
	import SDE.Entidade.ItemEmpAliquotas;
	import SDE.Entidade.ItemEmpEstoque;
	import SDE.Entidade.ItemEmpPreco;
	import SDE.Entidade.Mov;
	import SDE.Entidade.MovItem;
	import SDE.Enumerador.EItemTipo;
	
	import flash.events.Event;
	import flash.events.KeyboardEvent;
	import flash.ui.Keyboard;
	
	import img.Imagens;
	
	import mx.collections.ArrayCollection;
	import mx.controls.Alert;
	import mx.core.Application;
	import mx.core.Container;
	import mx.events.ValidationResultEvent;
	import mx.managers.IFocusManagerComponent;
	import mx.managers.PopUpManager;
	
	import pesquisas.PesquisaItem;
	import pesquisas.PesquisaItemEmpAliquota;
	import pesquisas.PesquisaItemEmpEstoque;
	
	private var cfop5102:CFOP;
	private var cfopPadrao:CFOP;
	private var cfop_selecionado:CFOP;
	private var clienteConsumidor:Cliente;
	private var clienteFuncionarioLogado:Cliente;
	
	[Bindable] private var tiposPgto:Array;
	
	[Bindable]private var tipo_nfe:Boolean = false;

	private var tiposDocDICT:Array;
	[Bindable] private var arraycItensImporta:ArrayCollection;
	[Bindable] private var arraycItensCarrinho:ArrayCollection;
	[Bindable] private var arraycItensCarrinhoOriginal:ArrayCollection;
	[Bindable] private var arraycLancamentosCaixa:ArrayCollection;
	[Bindable] private var arraycEstoques:ArrayCollection;
	private var cache:NuvemCache;
	private var ss:Sessao;
	
	
	private var desconto_no_total:Boolean = false;
	
	[Bindable] private var mostraImportarOS:Boolean;
	
	[Bindable] private var cliente_selecionado:Cliente;
	[Bindable] private var vendedor_selecionado:Cliente;
	[Bindable] private var item_para_carrinho:Item;
	[Bindable] private var preco_para_carrinho:ItemEmpPreco;
	[Bindable] private var estoque_para_carrinho:ItemEmpEstoque;
	[Bindable] private var impostos_para_carrinho:ItemEmpAliquotas;
	
	private var lancamento_em_edicao:Cx_Lancamento;
	
	private var foco_vai_para:IFocusManagerComponent;
	
	
	
	public static const milisegundosPorDia:int = 1000 * 60 * 60 * 24;
	
	private var mov:Mov;
	
	[Bindable] private var orcamentoEmail:Boolean = false;
	
	
	[Bindable] private var icmsCSTCodigo:Array = ['000','010','020','030','040','041','050','051','060','070','090'];
	private var icmsCSTTexto:Array = null;
	[Bindable] private var mtiCFOP:ClassFactory;
	
	[Bindable] public var numCarac:String = "200";
	[Bindable] public var descVlrFinal:Boolean = false;
	[Bindable] public var booFinalizacao:Boolean = false;
	[Bindable] public var booLancar:Boolean = true;
	
	private function botoesOcultos(ev:KeyboardEvent):void
	{
		if (ev.ctrlKey && ev.shiftKey && ev.keyCode==Keyboard.ENTER)
		{
			etapa3_btn_reserva.visible = !etapa3_btn_reserva.visible;
			etapa3_btn_vendasimples.visible = !etapa3_btn_vendasimples.visible;
		}
	}
	
	private function fn_ComboCST_ICMS_Label(cst_icms:String):String
	{
		return cst_icms+" | "+icmsCSTTexto[cst_icms];
	}
	
	private function remove_desconto():void
	{
		desc_valorbruto.text = "0.00";
		desconto_emValor.value = 0;
		desconto_emPct.value = 0;
		desconto_final.value = 0;
		indicador_itenscarrinho_valor_desconto = 0;
		sistema_calcular_valor_carrinho();
	}
	
	private function limpa_aba_desconto():void
	{
		desc_valorbruto.text = "0.00";
		desconto_emValor.value = 0;
		desconto_emPct.value = 0;
		desconto_final.value = 0;
		indicador_itenscarrinho_valor_desconto = 0;
		sistema_calcular_valor_carrinho();
		sistema_calcular_valor_abaDesconto();
	}
	
	private function valida_data():Boolean
	{
		var vre:ValidationResultEvent = validadorData.validate();
		return (vre.type=="valid")?true:false;
	}
		
	private function init():void
	{
		icmsCSTTexto = [];
		icmsCSTTexto['000'] = 'Tributada integralmente';
		icmsCSTTexto['010'] = 'Tributada e com cobran??a do ICMS por substitui????o tribut??ria';
		icmsCSTTexto['020'] = 'Com redu????o de base de c??lculo';
		icmsCSTTexto['030'] = 'Isenta ou n??o tributada e com cobran??a do ICMS por substitui????o tribut??ria';
		icmsCSTTexto['040'] = 'Isenta';
		icmsCSTTexto['041'] = 'N??o tributada';
		icmsCSTTexto['050'] = 'Suspens??o';
		icmsCSTTexto['051'] = 'Diferimento';
		icmsCSTTexto['060'] = 'ICMS cobrado anteriormente por substitui????o tribut??ria';
		icmsCSTTexto['070'] = 'Com redu????o de base de c??lculo e cobran??a do ICMS por substitui????o tribut??ria';
		icmsCSTTexto['090'] = 'Outras';
	}
	
	private function create():void
	{
		ss = App.single.ss;
		cache = App.single.cache;
		
		removePopup(popupDadosCliente);
		removePopup(popupEstoque);
		removePopup(popupTituloDetalhe);
		removePopup(popupTEF);
		removePopup(popup_importar_venda);
		removePopup(popup_importar_ordemServico);
		removePopup(popupImpostos);
		removePopup(popupConfirmaArquivo);
		removePopup(popupConfirmaImpressao);
		removePopup(popupEnviaEmailOrcamento);
		removePopup(popupEmiteOrcamento);
		removePopup(vboxDadosOrcamento);
		removePopup(popupDataPedido);
		removePopup(popupDescFinal);
		clienteConsumidor = cache.getCliente(1);
		
		arraycEstoques = new ArrayCollection();
		arraycItensCarrinho = new ArrayCollection();
		arraycItensCarrinhoOriginal = new ArrayCollection();
		arraycLancamentosCaixa = new ArrayCollection();
		tiposPgto = cache.arrayFinan_TipoPagamento;
		tiposDocDICT = MyArrayUtils.asDictionary(cache.arrayFinan_TipoDocumento);
		
		var strCfopPadrao:String = ss.parametrizacao.getParametro(Variaveis_SdeConfig.CORPORACAO_CFOPPADRAO);
		if (strCfopPadrao != "0" && strCfopPadrao != null)
		{
			cfopPadrao = new MeuFiltroWhere(cache.arrayCFOP).And(CFOP.campo_codigo, strCfopPadrao).getResultadoArraySimples()[0];
			cpCFOP.selectedItem = cfopPadrao;
			cfop_selecionado = cfopPadrao;
		}
		else
		{
			cfop5102 = new MeuFiltroWhere(cache.arrayCFOP).And(CFOP.campo_codigo,"5102").getResultadoArraySimples()[0];
			cpCFOP.selectedItem = cfop5102;
			cfop_selecionado = cfop5102;
		}
		
		clienteConsumidor = cache.getCliente(1);
		clienteFuncionarioLogado = cache.getCliente(ss.idClienteFuncionarioLogado);
		cpVendedor.selectedItem = clienteFuncionarioLogado;
		vendedor_selecionado = clienteFuncionarioLogado;
		
		
		dtGridMI.addEventListener("deleteRow", fn_trata_dtGridMI_delete);
		dtGridTitulosGerados.addEventListener("deleteRow", fn_trata_dtGridTitulosGerados_delete);
		
		if (Application.application.gerenteConexaoDesktop==null)
		{
			etapa3_btn_tef.enabled=false;
			etapa3_btn_nfe.enabled=false;
			etapa3_btn_nf.enabled=false;
		}
		
		etapa4.enabled = false;
		
		//mostraImportarOS = App.single.ss.parametrizacao.getParametro(Variaveis_SdeConfig.MENU_OS_CADASTROOS) == '1';
		mostraImportarOS = App.single.ss.parametrizacao.sim(Variaveis_SdeConfig.MENU_OS_CADASTROOS);
		
		sistema_resetar_tela();
		descVlrFinal = false;
	}
	
	private function removePopup(p:Container):void
	{
		p.parent.removeChild(p);
	}
	
	private function sistema_limpa_importacao_os():void
	{
		mov.idOrdemServico = 0;
		isImportadoOS = false;
		ordemServicoImportada = null;
		
		sistema_resetar_tela();
	}
	
	private function sistema_resetar_tela():void
	{
		vs.selectedChild=etapa1;
		etapa3_botoes_escondidos=0;
		mov = new Mov();
		sistema_limpa_item(true);
		sistema_limpar_carrinho(true);
		sistema_limpar_lancamentos();
		sistema_limpar_complementoNF();
		sistema_calcular_valor_carrinho();
		sistema_limpar_popupEmiteOrcamento();
		sistema_limpar_popupEnviaEmailOrcamento();
		indicador_itenscarrinho_qtd = 0;
		indicador_itenscarrinho_valor_bruto = 0;
		indicador_itenscarrinho_valor_desconto = 0;
		indicador_itenscarrinho_valor_desconto_pct = 0;
		indicador_itenscarrinho_valor_juro = 0;
		indicador_itenscarrinho_valor_liquido = 0;
		indicador_venda_valor_final = 0;
		indicador_venda_valor_juro_parcelamento = 0;
		indicador_venda_valor_pagamento_indefinido = 0;
		indicadoroculto_itenscarrinho_valor_comissao = 0;
		indicadoroculto_itenscarrinho_valor_desconto_maximo = 0;
		nsVlrTotal.value = 0;
		nsDescFinal_valor.value = 0;
		
		clienteConsumidor = cache.getCliente(1);
		clienteFuncionarioLogado = cache.getCliente(ss.idClienteFuncionarioLogado);
		cpVendedor.selectedItem = clienteFuncionarioLogado;
		vendedor_selecionado = clienteFuncionarioLogado;
			
		txtFaturaDuplicata.text = ""
		txtDadosAdicionais.text = "";
		
		if (cfopPadrao){
			cpCFOP.selectedItem = cfopPadrao;
			cfop_selecionado = cfopPadrao;
		}
		else{
			cpCFOP.selectedItem = cfop5102;
			cfop_selecionado = cfop5102;
		}
		
		isEnviaEmail = false;
		isImportadoOS = false;
		isNF = false;
		isNFe = false;
		isDescontoLancado = false;
		descVlrFinal = false;
		tabNav.selectedIndex = 0;
	}
	
	private function sistema_limpa_item(limpaCPesqItem:Boolean):void
	{
		item_para_carrinho=null;
		estoque_para_carrinho=null;
		preco_para_carrinho=null;
		
		if (limpaCPesqItem)
			cpItem.itemSelecionado = null;
		
		cpEstoque.text = "";
				
		arraycEstoques.removeAll();
		
		nsQtd.value = 0;
		nsVlrFrete.value = 0;
		nsVlrSeguro.value = 0;
		nsVlrUnit.value = 0;
		nsVlrTotal.value = 0;
	}
	
	private function sistema_limpar_carrinho(limpaCliente:Boolean):void
	{
		arraycItensCarrinho.removeAll();
		arraycItensCarrinhoOriginal.removeAll();
		cpCliente.selectedItem = clienteConsumidor;
		cliente_selecionado = clienteConsumidor;
		cpVendedor.selectedItem = clienteFuncionarioLogado;
		nsVlrTotal.value = 0;
		
		change_cpCliente();
	}
	
	private function sistema_limpa_importar():void
	{
		arraycItensImporta.removeAll();
	}
	
	private function sistema_limpar_lancamentos():void
	{
		arraycLancamentosCaixa.removeAll();
		indicador_itenscarrinho_valor_juro = 0;
	}
	
	private function sistema_limpar_popupEmiteOrcamento():void
	{
		sistema_limpar_vboxDadosOrcamento();
	}
	
	private function sistema_limpar_popupEnviaEmailOrcamento():void
	{
		cpEmail.selectedItems.removeAll();
		sistema_limpar_vboxDadosOrcamento();
		vsEmail.selectedIndex = 0;
		txtMenssagemEmail.text = "";
	}
	
	private function sistema_limpar_vboxDadosOrcamento():void
	{
		popupEmiteOrcamento_lblPagamento.text = "";
		popupEmiteOrcamento_txtValidadeDias.text = "";
		popupEmiteOrcamento_lblValidade.text = "";
		popupEmiteOrcamento_txtEntrega.text = "";
		popupEmiteOrcamento_txtFrete.text = "";
		popupEmiteOrcamento_txtImpostos.text = "";
	}
	
	private function iniciaDadosCliente():void
	{
		popupEmiteOrcamento_lblCliente.text = cliente_selecionado.nome;
		popupEmiteOrcamento_lblVendedor.text = cpVendedor.selectedItem.nome;
		
		var strPagamento:String = "";
		if (arraycLancamentosCaixa.length == 1)
			popupEmiteOrcamento_lblPagamento.text = arraycLancamentosCaixa.getItemAt(0).tipoPagamento_nome;
		else
		{
			var cont:Number = 1;
			for each (var cxL:Cx_Lancamento in arraycLancamentosCaixa)
			{
				if (cont == arraycLancamentosCaixa.length)
					strPagamento += cxL.tipoPagamento_nome;
				else
					strPagamento += (cxL.tipoPagamento_nome + ", ");
				cont++;
			}
			popupEmiteOrcamento_lblPagamento.text = strPagamento;
		}
	}
	
	private function usuario_abre_popupEmiteOrcamento():void
	{
		orcamentoEmail = false;
		iniciaDadosCliente();
		popupEmiteOrcamento_placeHolder.addChild(vboxDadosOrcamento);
		PopUpManager.addPopUp(popupEmiteOrcamento, Application.application.gerenteJanelas, true);
		PopUpManager.centerPopUp(popupEmiteOrcamento);
	}
	
	private function popupEmiteOrcamento_txtValidadeDias_Change():void
	{
		if (popupEmiteOrcamento_txtValidadeDias.text == "")
			return;
		popupEmiteOrcamento_lblValidade.text = "Valido at??: " + Formatadores.unica.formataData(new Date(new Date().getTime() + (Number(popupEmiteOrcamento_txtValidadeDias.text) * milisegundosPorDia))).toString();
	}
	
	private function usuario_abre_popup_enviaEmail():void
	{
		orcamentoEmail = true;
		iniciaDadosCliente();
		
		cpEmail.dataProvider = cpEmail.setCliente(cliente_selecionado.id);
		
		if (ss.parametrizacao.getParametro(Variaveis_SdeConfig.EMPRESA_EMAIL_ENDERECO) == "")
		{
			AlertaSistema.mensagem("N??o existe uma conta de Email cadastrada no sistema para envio de email's, entre em contato com o suporte para o cadastramento");
			return;
		}
		
		popupEmiteOrcamento_placeHolder.addChild(vboxDadosOrcamento);
		popupEnviaEmailOrcamento_placeHolder.addChild(vboxDadosOrcamento);
		
		txtDeEmail.text = ss.parametrizacao.getParametro(Variaveis_SdeConfig.EMPRESA_EMAIL_ENDERECO);
		txtDeNome.text = cache.getCliente(cache.getEmpresa(ss.idEmp).idCliente).nome;
		txtAssuntoEmail.text = 'OR??AMENTO ' + cache.getCliente(cache.getEmpresa(ss.idEmp).idCliente).nome;
		
		txtParaNome.text = cliente_selecionado.nome;
		
		PopUpManager.addPopUp(popupEnviaEmailOrcamento, Application.application.gerenteJanelas, true);
		PopUpManager.centerPopUp(popupEnviaEmailOrcamento);
	}
	
	private function usuario_abre_popupDescFinal():void
	{
		PopUpManager.addPopUp(popupDescFinal, Application.application.gerenteJanelas, true);
		PopUpManager.centerPopUp(popupDescFinal);
	}
	
	private function usuario_fecha_popupDescFinal():void
	{
		PopUpManager.removePopUp(popupDescFinal);
	}
	
	private function usuario_abre_popup_impostos():void
	{
		PopUpManager.addPopUp(popupImpostos, Application.application.gerenteJanelas, true);
		PopUpManager.centerPopUp(popupImpostos);
	}
	
	private function usuario_seleciona_icms(detro_ou_fora:Boolean):void
	{
		if (detro_ou_fora)
		{
			nsAliq.value = impostos_para_carrinho.icmsAliqPadrao_SD;
			nsReduzidaPara.value = impostos_para_carrinho.icmsAliq_SD;
			cmbCST.selectedItem = impostos_para_carrinho.icmsCST_SD;
		}
		else
		{
			nsAliq.value = impostos_para_carrinho.icmsAliqPadrao_SF;
			nsReduzidaPara.value = impostos_para_carrinho.icmsAliq_SF;
			cmbCST.selectedItem = impostos_para_carrinho.icmsCST_SF;
		}
	}
	
	private function usuario_confirma_impostos():void
	{
		usuario_fecha_popup_impostos();
	}
	
	private function usuario_fecha_popup_impostos():void
	{
		PopUpManager.removePopUp(popupImpostos);
	}
	
	private function cpEstoque_KeyDown(event:KeyboardEvent):void
	{
		if (event.keyCode != Keyboard.ENTER)
			return;
		
		usuario_escolheu_estoque_para_carrinho();
	}
	
	private function usuario_escolheu_estoque_para_carrinho():void
	{
		foco_vai_para = cpEstoque.txtPesquisa;
		
		var estoques:Array = PesquisaItemEmpEstoque.pegarArrayPorCodigoBarras(Sessao.unica.idEmp, cpEstoque.text);
		
		if (estoques.length == 1)
		{
			sistema_define_estoque(estoques[0] as ItemEmpEstoque);
			item_para_carrinho = PesquisaItem.pegar(estoque_para_carrinho.idItem);
		}
		else if (estoques.length > 1)
		{
			arraycEstoques.removeAll();
			arraycEstoques.source = estoques;
			abrepopup_estoques();
		}
		else if (estoques.length == 0 && item_para_carrinho.tipo == EItemTipo.servico)
		{
			sistema_define_estoque(null);
		}
		else
		{
			Alert.show("Estoque n??o encontrado para o produto", "Mensagem SDE", 4, null, null, Imagens.unica.icn_32_deleta);
			return;
		}
	}
	
 	private function usuario_escolheu_item_para_carrinho():void
	{
		sistema_limpa_item(false);
		foco_vai_para = cpItem.txtPesquisaBox;
		
		if (cpItem.itemSelecionado == null)
		{
			sistema_limpa_item(true);
			return;
		}
		
		item_para_carrinho = cpItem.itemSelecionado.clone();
		var estoques:Array = PesquisaItemEmpEstoque.pegarArrayPorItem(Sessao.unica.idEmp, item_para_carrinho.id);
		
		if (estoques.length == 1)
		{
			sistema_define_estoque(estoques[0] as ItemEmpEstoque);
		}
		else if (estoques.length > 1)
		{
			arraycEstoques.removeAll();
			arraycEstoques.source = estoques;
			abrepopup_estoques();
		}
		else if (estoques.length == 0 && item_para_carrinho.tipo == EItemTipo.servico)
		{
			sistema_define_estoque(null);
		}
		else
		{
			Alert.show("Estoque n??o encontrado para o produto. Consulte o suporte para maiores informa????es.", "Mensagem SDE", 4, null, null, Imagens.unica.icn_32_deleta);
			return;
		}
	}
	
	private function abrepopup_estoques():void
	{
		PopUpManager.addPopUp(popupEstoque, Application.application.gerenteJanelas, true);
		PopUpManager.centerPopUp(popupEstoque);
		dtGridEstoques.selectedIndex=0;
		dtGridEstoques.setFocus();
	}
	
	private function usuario_fecha_popup_estoques():void
	{
		PopUpManager.removePopUp(popupEstoque);
		sistema_limpa_item(true);
	}
	
	private function usuario_escolheu_estoque():void
	{
		PopUpManager.removePopUp(popupEstoque);
		sistema_define_estoque(dtGridEstoques.selectedItem as ItemEmpEstoque);
	}
	
	private function sistema_define_estoque(estoque:ItemEmpEstoque):void
	{
		if (estoque)
			estoque_para_carrinho = estoque;
		
		if (!item_para_carrinho)
			item_para_carrinho = PesquisaItem.pegar(estoque_para_carrinho.idItem);
		
		impostos_para_carrinho = PesquisaItemEmpAliquota.pegar(Sessao.unica.idEmp, item_para_carrinho.id);
		usuario_seleciona_icms(true);
		
		AlertaSistema.mensagem("escreve imposto", true);
		
		nsQtd.value = 1;
		nsQtd.setFocus();
		cpItem_AtualizaSelecionado();
	}
	
	private function usuario_define_quantidade():void
	{
		var filtro:MeuFiltroWhere =
			new MeuFiltroWhere(cache.arrayItemEmpPreco)
				.andEquals(item_para_carrinho.id,ItemEmpPreco.campo_idItem)
				.andEquals(ss.idEmp,ItemEmpPreco.campo_idEmp);
		
		var precos:Array = filtro.getResultadoArraySimples();
		
		preco_para_carrinho = precos[0];
		nsVlrUnit.value = preco_para_carrinho.venda;
		nsVlrTotal.value = nsVlrUnit.value * nsQtd.value;
		
		nsVlrUnit.setFocus();
	}
	
	private function usuario_lanca_item_para_carrinho():void
	{
		var mi:MovItem = new MovItem();
		var descDado:Number = Formatadores.unica.currencyFormatter(preco_para_carrinho.venda - nsVlrUnit.value);
		mi.vlrDesc = 0;
		
		mi.vlrDescMax = Formatadores.unica.currencyFormatter((preco_para_carrinho.descontoMaximo*preco_para_carrinho.venda)/100);
		
		if (descDado > mi.vlrDescMax)
		{
			Alert.show("O Desconto Concedido ultrapassa o valor do Desconto M??ximo para este ??tem", "Mensagem SDE", 4, null, null, Imagens.unica.icn_32_alerta);
			return;
		}
		if (descDado != 0)	
			mi.vlrDesc = Formatadores.unica.currencyFormatter((preco_para_carrinho.venda - nsVlrUnit.value)*nsQtd.value);		
				
		mi.idClienteFuncionario = cpVendedor.selectedItem.id;
		mi.idItem = item_para_carrinho.id;
		mi.item_nome = item_para_carrinho.nome;
		
		if (item_para_carrinho.tipo == EItemTipo.produto)
		{
			mi.idIEE = estoque_para_carrinho.id;
			mi.estoque_identificador = estoque_para_carrinho.identificador;
		}
		
		mi.qtd = nsQtd.value;
		mi.vlrSeguro = nsVlrSeguro.value;
		mi.vlrFrete = nsVlrFrete.value;
		mi.vlrUnitCusto = preco_para_carrinho.custo;
		mi.vlrUnitCompra = preco_para_carrinho.custo;
		mi.rf_unica = item_para_carrinho.rfUnica;
		mi.rf_auxiliar = item_para_carrinho.rfAuxiliar;
		mi.unid_med = item_para_carrinho.unidMed;
		mi.vlrUnitVendaInicial = preco_para_carrinho.venda;
		mi.vlrUnitVendaFinal = nsVlrUnit.value;
		mi.vlrDescUnit = Formatadores.unica.currencyFormatter(preco_para_carrinho.venda - nsVlrUnit.value);
		mi.cfop = Number(cfop_selecionado.codigo);
		mi.icmsAliqPadrao = nsAliq.value;
		mi.icmsAliq = nsReduzidaPara.value;
		mi.icmsCst = cmbCST.selectedItem as String;
		mi.cofinsAliq = impostos_para_carrinho.cofinsAliqPadrao;
		mi.cofinsCst = impostos_para_carrinho.cofinsCST;
		mi.pisAliq = impostos_para_carrinho.pisAliqPadrao;
		mi.pisCst = impostos_para_carrinho.pisCST;
		mi.ipiAliq = impostos_para_carrinho.ipiAliqPadrao;
		mi.pctComissaoPreco = preco_para_carrinho.pctComissao;
		
		sistema_coloca_item_carrinho(mi);
		cpItem.txtPesquisaBox.text = "";
		foco_vai_para.setFocus();	
	}
	
	private function sistema_coloca_item_carrinho(mi:MovItem):void
	{
		var valorDescontoAnterior:Number = indicador_itenscarrinho_valor_desconto - indicador_itenscarrinho_valor_desconto_unit;
		
		desc_valorbruto.text = "0.00";
		desconto_emValor.value = 0;
		desconto_emPct.value = 0;
		desconto_final.value = 0;
		indicador_itenscarrinho_valor_desconto = 0;
		sistema_calcular_valor_carrinho();
		
		arraycItensCarrinho.addItem(mi);
		arraycItensCarrinhoOriginal.removeAll();
		for each (var mi:MovItem in arraycItensCarrinho)
			arraycItensCarrinhoOriginal.addItem(mi.clone());
		sistema_limpa_item(true);
		desconto_emValor.value = valorDescontoAnterior;
		sistema_calcular_valor_carrinho();
	}
	
	private function fn_trata_dtGridMI_delete(ev:Event):void
	{
		if (isImportadoOS)
		{
			Alert.show("N??o ?? permitida altera????o de itens da OS no fechamento. Para alter??-la, v?? ao menu de cadastro de Ordem de Servi??o e altere a OS desejada.", "Mensagem SDE", 4, null, null, Imagens.unica.icn_32_alerta);
			return;
		}
		
    	var pos:int = arraycItensCarrinho.getItemIndex(ev.target.data);
		arraycItensCarrinho.removeItemAt(pos);
		arraycItensCarrinhoOriginal.removeItemAt(pos);
		limpa_aba_desconto();
	}
	
	private function cpItem_AtualizaSelecionado():void
	{
		cpItem.txtPesquisaBox.text = item_para_carrinho.nome;
	}	
	
	private function cpCFOP_Change():void
	{
		cfop_selecionado = cpCFOP.selectedItem as CFOP;
	}
	
	private function proximoFoco(ev:KeyboardEvent):Boolean
	{
		if (ev.keyCode == Keyboard.ENTER)
			return true;
		else
			return false;
	}
	
	private function proximoFoco_(ev:KeyboardEvent, container:Container):void
	{
		if (ev.keyCode == Keyboard.ENTER || ev.keyCode == Keyboard.TAB)
			container.setFocus();
	}
	
	private function geraCarne(IdMov:Number):void
	{
		if (ss.idCorp == 10)
		{
			ss.nuvens.modificacoes.GeraCarne(IdMov,
				function():void{
					if(Application.application.gerenteConexaoDesktop)
					{
						Application.application.gerenteConexaoDesktop.baixaCarne(App.single.idCorp);
					}
					else
					{
						AlertaSistema.mensagem("?? necess??rio estar conectado ao SDE Desktop para a Impress??o do carn??");
					}
				}
			);
		}
	}
	
	private function caracteres():void
	{
		//txtDadosAdicionais.text = "teste";
		numCarac = (200-txtDadosAdicionais.length).toString();
	}
	private function msgSimples():void
	{
		txtDadosAdicionais.text = "permite aproveitamento de credito icms no valor de correspondente a aliquota de 3.82 nos termos do art.23 da lc 12306";
		txtDadosAdicionais.text.toUpperCase();
	}
	private function limpaMsg():void
	{
		txtDadosAdicionais.text = "";
	}
	private function lancarValorFinal():void
	{
		var valores:Number;
		valores = 0;
		if(nsDescFinal_valor.value == indicador_venda_valor_final)
		{
			descVlrFinal = true;
			booFinalizacao = true;
			booLancar = false;
		}
		else
			if(nsDescFinal_valor.value > indicador_venda_valor_final)
			{
				descVlrFinal = true;
				Alert.show("O Valor Final da Venda n??o deve ser superior aa Valor Original da Venda!", "Mensagem SDE", 4, null, null, Imagens.unica.icn_32_deleta);
				nsDescFinal_valor.value = indicador_venda_valor_final;
				booFinalizacao = true;
				booLancar = true;
			}
			else
				if(nsDescFinal_valor.value > 0)
				{		
					valores = indicador_venda_valor_final - nsDescFinal_valor.value ;
					indicador_itenscarrinho_valor_desconto += valores;
					indicador_venda_valor_final = nsDescFinal_valor.value;
					descVlrFinal = true;
					booFinalizacao = false;
					booLancar = false;
				}
				else
					if(nsDescFinal_valor.value == 0)
					{
						descVlrFinal = true;
						Alert.show("O Valor Final da Venda deve ser superior a ZERO!", "Mensagem SDE", 4, null, null, Imagens.unica.icn_32_deleta);
						nsDescFinal_valor.value = indicador_venda_valor_final;
						booFinalizacao = true;
					}
	}
	
	private function removerValorFinal():void
	{
		indicador_itenscarrinho_valor_desconto = mov.vlrItensInicial - mov.vlrItensFinal;
		nsDescFinal_valor.value = 0;
		indicador_venda_valor_final = mov.vlrTotal;
		descVlrFinal = true;
		booFinalizacao = true;
		booLancar = true;
	}
	