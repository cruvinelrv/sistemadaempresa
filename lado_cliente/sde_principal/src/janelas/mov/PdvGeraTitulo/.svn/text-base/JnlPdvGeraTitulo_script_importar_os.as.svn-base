	import Core.Alerta.AlertaSistema;
	import Core.App;
	import Core.Utils.MeuFiltroWhere;
	
	import SDE.Entidade.Item;
	import SDE.Entidade.ItemEmpAliquotas;
	import SDE.Entidade.ItemEmpPreco;
	import SDE.Entidade.MovItem;
	import SDE.Entidade.OrdemServico;
	import SDE.Entidade.OrdemServico_Item;
	import SDE.Enumerador.EItemTipo;
	
	import flash.events.Event;
	
	import mx.managers.PopUpManager;
	
	[Bindable]private var isImportadoOS:Boolean = false;
	private var idTransacaoOS:Number;
	private var idOperacaoOS:Number;
	private var ordemServicoImportada:OrdemServico = null;
		
	private function usuario_abre_popup_importa_os():void
	{
		PopUpManager.addPopUp(popup_importar_ordemServico, this, true);
		PopUpManager.centerPopUp(popup_importar_ordemServico);
	}
	
	private function usuario_retorna_popup_importa_os(ev:Event):void
	{
		var item:Item;
		var itemPreco:ItemEmpPreco;
		var itemAliquotas:ItemEmpAliquotas;
		var osRet:OrdemServico = ev.target.data;
		ordemServicoImportada = osRet;
		usuario_fecha_popup_importa_os();
		
		sistema_limpar_carrinho(false);
		
		isImportadoOS = true;
		idTransacaoOS = osRet.idTransacao;
		idOperacaoOS = osRet.idOperacao;
		
		mov.idOrdemServico = osRet.id;
		
		cpCliente.selectedItem = cache.getCliente(osRet.idCliente).clone();
		cliente_selecionado = cache.getCliente(osRet.idCliente).clone();
		change_cpCliente();
		
		var mi:MovItem;
		for each (var osi:OrdemServico_Item in App.single.cache.arrayOrdemServico_Item)
		{
			if (osi.idOrdemServico == osRet.id)
			{
				AlertaSistema.mensagem("Item OS para item nova Mov " + osi.item_nome, true);
				
				mi = new MovItem();
				mi.idItem = osi.idItem;
				mi.idIEE = osi.idIEE;
				
				item = new Item(App.single.cache.getItem(mi.idItem));
				itemPreco = new ItemEmpPreco(App.single.cache.getItemEmpPreco(osi.idIEP));
				
				mi.nomeCompl = osi.nomeCompl;
				mi.item_nome = osi.item_nome;
				mi.estoque_identificador = osi.estoque_identificador;
				mi.rf_unica = osi.rf_unica;
				mi.rf_auxiliar = osi.rf_auxiliar;
				mi.unid_med = osi.unid_med;
				
				var filtro:MeuFiltroWhere =
					new MeuFiltroWhere(cache.arrayItemEmpAliquotas)
						.andEquals(item.id,ItemEmpAliquotas.campo_idItem)
						.andEquals(ss.idEmp,ItemEmpAliquotas.campo_idEmp);
				impostos_para_carrinho = filtro.getResultadoArraySimples()[0];
				
				mi.icmsAliqPadrao = impostos_para_carrinho.icmsAliqPadrao_SD;
				mi.icmsAliq = impostos_para_carrinho.icmsAliq_SD;
				mi.icmsCst = impostos_para_carrinho.icmsCST_SD;
				
				if (item.tipo == EItemTipo.produto)
					mi.cfop = 5102;
				else if (item.tipo == EItemTipo.servico)
					mi.cfop = 6949;
				
				mi.qtd = osi.qtd;
				mi.vlrDescMax = ((itemPreco.descontoMaximo*itemPreco.venda)/100)*nsQtd.value;
				mi.vlrUnitVendaFinal = osi.vlrUnitVendaFinal;
				mi.vlrUnitVendaFinalQtd = osi.vlrUnitVendaFinalQtd;
				mi.vlrUnitVendaInicial = osi.vlrUnitVendaInicial;
				sistema_coloca_item_carrinho(mi);
			}
		}
	}
	
	private function usuario_fecha_popup_importa_os():void
	{
		PopUpManager.removePopUp(popup_importar_ordemServico);
	}