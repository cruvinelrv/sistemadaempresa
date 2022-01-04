// ActionScript file - JnlPdvEntrada
	//Imports-----
	import Core.App;
	import Core.Sessao;
	import Core.Utils.MeuFiltroWhere;
	
	import SDE.CamadaNuvem.NuvemCache;
	import SDE.Entidade.CFOP;
	import SDE.Entidade.Cliente;
	import SDE.Entidade.Empresa;
	import SDE.Entidade.Item;
	import SDE.Entidade.ItemEmpAliquotas;
	import SDE.Entidade.ItemEmpEstoque;
	import SDE.Entidade.ItemEmpPreco;
	import SDE.Entidade.Mov;
	import SDE.Entidade.MovItem;
	
	import com.flexpernambuco.controls.MasterTextInput;
	
	import flash.events.KeyboardEvent;
	import flash.ui.Keyboard;
	
	import img.Imagens;
	
	import mx.collections.ArrayCollection;
	import mx.controls.Alert;
	import mx.core.Application;
	import mx.core.ClassFactory;
	import mx.core.Container;
	import mx.managers.IFocusManagerComponent;
	
	//Variáveis-----
	private var cfop1204:CFOP;
	private var cfopPadrao:CFOP;
	private var cfop_selecionado:CFOP;
	
	[Bindable]private var clienteEmpresa:Cliente;
	private var clienteFuncionarioLogado:Cliente;
	
	private var cache:NuvemCache;
	private var ss:Sessao;
	
	private var foco_vai_para:IFocusManagerComponent;
	private var icmsCSTTexto:Array = null;
	private var mov:Mov;
	private var mi:MovItem;
	private var emp:Empresa;
	
	[Bindable] private var arraycItensImporta:ArrayCollection;
	[Bindable] private var arraycItensCarrinho:ArrayCollection;
	[Bindable] private var arraycLancamentosCaixa:ArrayCollection;
	[Bindable] private var arraycEstoques:ArrayCollection;
	[Bindable] private var mostraImportarMov:Boolean;
	[Bindable] private var cliente_selecionado:Cliente;
	[Bindable] private var vendedor_selecionado:Cliente;
	[Bindable] private var item_para_carrinho:Item;
	[Bindable] private var preco_para_carrinho:ItemEmpPreco;
	[Bindable] private var estoque_para_carrinho:ItemEmpEstoque;
	[Bindable] private var impostos_para_carrinho:ItemEmpAliquotas;
	[Bindable] private var indicador_itenscarrinho_qtd:Number = 0;
	[Bindable] private var indicador_venda_valor_final:Number = 0;
	[Bindable] private var icmsCSTCodigo:Array = ['000','010','020','030','040','041','050','051','060','070','090'];
	[Bindable] private var mtiCFOP:ClassFactory;

	//Funções-----
	private function init():void
	{
		mtiCFOP = new ClassFactory(MasterTextInput);
				mtiCFOP.properties =
				{
					height:20,
					maxChars:4
				};
				
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
	}
	
	private function create():void
	{
		ss = App.single.ss;
		cache = App.single.cache;
		
		mov = new Mov();

		removePopup(popupDadosCliente);
		removePopup(popupEstoque);
		removePopup(popup_importar_venda);
		removePopup(popupImpostos);
		removePopup(popupConfirmaArquivo);
		//removePopup(popupConfirmaImpressao);
		
		arraycEstoques = new ArrayCollection();
		arraycItensImporta = new ArrayCollection();
		arraycItensCarrinho = new ArrayCollection();
		arraycLancamentosCaixa = new ArrayCollection();

		cfop1204 = new MeuFiltroWhere(cache.arrayCFOP).And(CFOP.campo_codigo,"1204").getResultadoArraySimples()[0];
		cpCFOP.selectedItem = cfop1204;
		cfop_selecionado = cfop1204;
		
		emp = App.single.cache.getEmpresa(ss.idEmp);
		
		clienteEmpresa = cache.getCliente(emp.idCliente).clone();
		
		cmbClienteContato.idCliente = emp.idCliente;
		
		cmbClienteEndereco_Faturamento.idCliente = emp.idCliente;
		cmbClienteEndereco_Cobranca.idCliente = emp.idCliente;
		cmbClienteEndereco_Entrega.idCliente = emp.idCliente;
		cmbEndereco_change();
		
		cliente_selecionado = clienteEmpresa;
		cliente_selecionado.id = clienteEmpresa.id;
		clienteFuncionarioLogado = cache.getCliente(ss.idClienteFuncionarioLogado);
		
		dtGridMI.addEventListener("deleteRow", fn_trata_dtGridMI_delete);
		dtGridMIcfop.addEventListener("deleteRow", fn_trata_dtGridMIcfop_delete);
		
		/*if (Application.application.gerenteConexaoDesktop==null)
		{
			etapa2_btn_nfe.enabled=false;
			etapa2_btn_nf.enabled=false;
		}
		
		etapa3.enabled = false;
		
		sistema_resetar_tela();*/
	}
	
	//Funções PDV-----
	private function proximoFoco(ev:KeyboardEvent):Boolean
	{
		if (ev.keyCode == Keyboard.ENTER)
			return true;
		else
			return false;
	}
	
	private function removePopup(p:Container):void
	{
		p.parent.removeChild(p);
	}
	
	private function sistema_resetar_tela():void
	{
		vs.selectedChild=etapa1;
		mov = new Mov();
		sistema_limpa_item(true);
		sistema_limpar_carrinho(true);
		sistema_limpar_complementoNF();
		sistema_calcular_valor_carrinho();
		arraycItensCarrinho.removeAll();
		cpTransportador.selectedItems.removeAll();
	}
	
	//Funções Carrinho-----
	private function usuario_lanca_item_para_carrinho():void
	{
		mi = new MovItem();
		
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
		
		mi.cfop = Number((cpCFOP.selectedItem as CFOP).codigo);
		
		mi.icmsAliqPadrao = nsAliq.value;
		mi.icmsAliq = nsReduzidaPara.value;
		mi.icmsCst = cmbCST.selectedItem as String;
		
		mi.cofinsAliq = impostos_para_carrinho.cofinsAliqPadrao;
		mi.cofinsCst = impostos_para_carrinho.cofinsCST;
		mi.pisAliq = impostos_para_carrinho.pisAliqPadrao;
		mi.pisCst = impostos_para_carrinho.pisCST;
		mi.ipiAliq = impostos_para_carrinho.ipiAliqPadrao;
		
		sistema_coloca_item_carrinho(mi);

		cpItem.txtPesquisaBox.text = "";
		foco_vai_para.setFocus();
	}
	
	private function usuario_escolheu_item_para_carrinho():void
	{
		sistema_limpa_item(false);
		foco_vai_para = cpItem.txtPesquisaBox;
		
		if (cpItem.itemSelecionado==null)
		{
			sistema_limpa_item(true);
			return;
		}
		
		item_para_carrinho = cpItem.itemSelecionado;
		
		var filtro:MeuFiltroWhere =
			new MeuFiltroWhere(cache.arrayItemEmpEstoque)
			.andEquals(item_para_carrinho.id,ItemEmpEstoque.campo_idItem)
			.andEquals(ss.idEmp, ItemEmpEstoque.campo_idEmp);
		
		arraycEstoques.removeAll();
		
		var estoques:Array = filtro.getResultadoArraySimples();
		
		if (estoques.length == 1)
		{
			sistema_define_estoque(estoques[0]);
		}
		else
		{
			abrepopup_estoques();
		}
		arraycEstoques.source = estoques;
	}
	
	private function importar_item_para_carrinho():void
	{
		for each (var movI:MovItem in dtGridMIcfop.dataProvider)
		{
			arraycItensCarrinho.addItem(movI);
		}
		sistema_limpa_importar();
	}
	
	private function sistema_coloca_item_importa(mi:MovItem):void
	{
		arraycItensImporta.addItem(mi);
	}
	
	private function sistema_coloca_item_carrinho(mi:MovItem):void
	{
		arraycItensCarrinho.addItem(mi);
		sistema_limpa_item(true);
		sistema_calcular_valor_carrinho();
	}
	
	private function usuario_escolheu_estoque_para_carrinho():void
	{
		foco_vai_para = cpEstoque;
		if (cpEstoque.selectedItem==null)
		{
			sistema_limpa_item(true);
			return;
		}
		
		var estoque:ItemEmpEstoque = cpEstoque.selectedItem;
		item_para_carrinho = cache.getItem( estoque.idItem );
		
		sistema_define_estoque(estoque);
	}
	
	private function cpItem_AtualizaSelecionado():void
	{
		cpItem.txtPesquisaBox.text = item_para_carrinho.nome;
	}
	
	private function usuario_define_quantidade():void
	{
		var filtro:MeuFiltroWhere =
			new MeuFiltroWhere(cache.arrayItemEmpPreco)
				.andEquals(item_para_carrinho.id,ItemEmpPreco.campo_idItem)
				.andEquals(ss.idEmp,ItemEmpPreco.campo_idEmp);
		var precos:Array = filtro.getResultadoArraySimples();
		preco_para_carrinho = precos[0];
		
		if(nsQtd.value > 0)
		{
			nsVlrUnit.value = preco_para_carrinho.venda;
			nsVlrTotal.value = nsVlrUnit.value * nsQtd.value;
			nsVlrUnit.setFocus();
		}
		else
		{
			Alert.show("A Quantidade informada deve ser superior a ZERO","Alerta SDE",4,null,null,img.Imagens.unica.icn_32_alerta);
		}
		
	}
	
	private function fn_trata_dtGridMIcfop_delete(ev:Event):void
	{
    	var pos:int = arraycItensImporta.getItemIndex(ev.target.data);
		arraycItensImporta.removeItemAt(pos);
	}
	
	private function fn_trata_dtGridMI_delete(ev:Event):void
	{
    	var pos:int = arraycItensCarrinho.getItemIndex(ev.target.data);
		arraycItensCarrinho.removeItemAt(pos);
		sistema_calcular_valor_carrinho();
	}
	
	private function sistema_limpa_item(limpaCPesqItem:Boolean):void
	{
		item_para_carrinho=null;
		estoque_para_carrinho=null;
		preco_para_carrinho=null;
		
		if (limpaCPesqItem)
			cpItem.itemSelecionado = null;
			
		cpEstoque.selectedItem=null;
		
		arraycEstoques.removeAll();
		
		nsQtd.value = 0;
		nsVlrFrete.value = 0;
		nsVlrSeguro.value = 0;
		nsVlrUnit.value = 0;
		nsVlrTotal.value = 0;
	}
	
	private function sistema_limpa_importar():void
	{
		arraycItensImporta.removeAll();
	}
	
	private function sistema_limpar_carrinho(limpaCliente:Boolean):void
	{
		arraycItensCarrinho.removeAll();
	}
	
	private function sistema_calcular_valor_carrinho():void
	{
		indicador_venda_valor_final = 0;
		indicador_itenscarrinho_qtd = 0;
		
		for each (var movI:MovItem in arraycItensCarrinho)
		{
			movI.vlrUnitVendaFinalQtd = movI.vlrUnitVendaFinal * movI.qtd;
			mov.vlrItensFinal = movI.vlrUnitVendaFinalQtd;
			indicador_venda_valor_final += mov.vlrItensFinal;
			indicador_itenscarrinho_qtd += movI.qtd;
		}
		mov.vlrTotal = indicador_venda_valor_final;
	}
	
	//Funções Impostos-----
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
	
	private function cpCFOP_Change():void
	{
		cfop_selecionado = cpCFOP.selectedItem as CFOP;
	}
	
	private function fn_ComboCST_ICMS_Label(cst_icms:String):String
	{
		return cst_icms+" | "+icmsCSTTexto[cst_icms];
	}
	
	//Funções Estoques-----
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
			
		var filtro:MeuFiltroWhere =
			new MeuFiltroWhere(cache.arrayItemEmpAliquotas)
				.andEquals(item_para_carrinho.id,ItemEmpAliquotas.campo_idItem)
				.andEquals(ss.idEmp,ItemEmpAliquotas.campo_idEmp);
		impostos_para_carrinho = filtro.getResultadoArraySimples()[0];
		
		usuario_seleciona_icms(true);
		
		AlertaSistema.mensagem("escreve imposto", true);
		
		nsQtd.value = 1;
		nsQtd.setFocus();
		cpItem_AtualizaSelecionado();
	}
// ActionScript file - JnlPdvEntrada