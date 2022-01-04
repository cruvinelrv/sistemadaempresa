// ActionScript file
import Core.Alerta.AlertaSistema;
import Core.App;
import Core.Sessao;
import Core.Utils.Formatadores;
import Core.Utils.MeuFiltroWhere;

import SDE.Constantes.Variaveis_SdeConfig;
import SDE.Entidade.Balanco;
import SDE.Entidade.BalancoItem;
import SDE.Entidade.Item;
import SDE.Entidade.ItemEmpEstoque;
import SDE.Entidade.ItemEmpPreco;
import SDE.Enumerador.EBalancoSituacao;
import SDE.Nuvens;

import flash.events.Event;
import flash.net.URLRequest;

import mx.collections.ArrayCollection;
import mx.controls.Alert;
import mx.controls.dataGridClasses.DataGridColumn;
import mx.core.Application;
import mx.events.CloseEvent;
import mx.managers.IFocusManagerComponent;
import mx.managers.PopUpManager;
	
	private var ss:Sessao;
	private var n:Nuvens;
	[Bindable] private var arraycBalancoItens:ArrayCollection = new ArrayCollection();
	[Bindable] private var arraycEstoques:ArrayCollection = new ArrayCollection();
	
	[Bindable] private var arrayBalancoItens:Array = [];
	
	[Bindable] private var item_selecionado:Item = null; 
	[Bindable] private var estoque_lancado:ItemEmpEstoque = null
	
	private var foco_vai_para:IFocusManagerComponent;
	
	[Bindable] public var balanco_selecionado:Balanco = null;
	
	[Bindable] public var valorTotalCusto:Number;
	[Bindable] public var valorTotalVenda:Number;
	
	private var idBalanco:Number = 0;
	
	[Bindable] private var balancoItens:Array;
	
	private function init():void
	{
		ss = Application.application.sessao;
		n = ss.nuvens;
		//layout
		popupEstoque.parent.removeChild(popupEstoque);
		popupRelatorioVerificacaoBalanco.parent.removeChild(popupRelatorioVerificacaoBalanco);
		//eventos
		gridBalancos.addEventListener("abrir", gridBalancos_ev_abrir);
		gridBalancos.addEventListener('concluir', gridBalancos_ev_concluir);
		gridBalancos.addEventListener('cancelar', gridBalancos_ev_cancelar);
		gridBalancos.addEventListener('relatorio', gridBalancos_ev_relatorio);
		
		gridBalancoItens.addEventListener("remover", gridBalancoItens_ev_remover);
		//
		resetar();
	}
	private function create():void
	{
	}
	
	private function gridBalancos_labelfunction(xxx:Balanco, coluna:DataGridColumn):String
	{
		if (coluna==colunaBalancoSituacao)
		{
			if (xxx.situacao==EBalancoSituacao.cancelado)
				return 'Cancelado';
			if (xxx.situacao==EBalancoSituacao.efetuado)
				return 'Concluído';
			if (xxx.situacao==EBalancoSituacao.em_andamento)
				return 'Em Andamento';
		}
		if (coluna==colunaBalancoNome)
		{
			return "Balanco "+xxx.id+", iniciado em "+xxx.dthrInicio;
		}
		
		return "";
	}
	
	private function resetar():void
	{
		gridBalancos.dataProvider = n.cache.cloneBalanco;
		this.currentState='state1';
	}
	
	private function gridBalancos_ev_abrir(ev:Event):void
	{
		var id:Number = ev.target.data.id;
		//AlertaSistema.mensagem("id: "+id);
		sistema_abre_balanco(id);
	}
	
	private function gridBalancos_ev_concluir(ev:Event):void
	{
		n.modificacoes.Balanco_Fecha(ev.target.data.id, true,
			function():void
			{
				resetar();
				//AlertaSistema.mensagem("Conclui");
			}
		);
	}
	
	private function gridBalancos_ev_cancelar(ev:Event):void
	{
		n.modificacoes.Balanco_Fecha(ev.target.data.id, false,
			function():void
			{
				resetar();
				//AlertaSistema.mensagem("Cancelei");
			}
		);
	}
	
	private function gridBalancos_ev_relatorio(ev:Event):void
	{
		idBalanco = ev.target.data.id;
		this.currentState='state3';
	}
	
	private function gridBalancoItens_ev_remover(ev:Event):void
	{
		var id:Number = ev.target.data.id;
		//AlertaSistema.mensagem("removido: "+id);
		n.modificacoes.Balanco_Remove(
			id, 
			function():void
			{
				preencheBalancoItens();
			}
		);
	}
	
	private function usuario_cria_balanco():void
	{
		//não permite usuário abrir dois balanços
		for each (var xxx:Balanco in n.cache.arrayBalanco)
		{
			if (xxx.situacao==EBalancoSituacao.em_andamento)
			{
				sistema_abre_balanco(xxx.id);
				return;
			}
		}
		
		
		n.modificacoes.Balanco_Abre(
			function(retorno:Number):void
			{
				sistema_abre_balanco(retorno);
				//resetar();
			}
		);
	}
	
	private function sistema_abre_balanco(idBalanco:Number):void
	{
		this.balanco_selecionado = App.single.cache.getBalanco(idBalanco);
		this.currentState='state2';
		preencheBalancoItens();
		cpItem.txtPesquisaBox.setFocus();
	}
	
	private function preencheBalancoItens():void
	{
		arraycBalancoItens.removeAll();
		for each (var bi:BalancoItem in n.cache.cloneBalancoItem)
		{
			if (bi.idBalanco==balanco_selecionado.id)
			{
				arraycBalancoItens.addItem(bi);
			}
		}
	}
	
	
	
	private function usuario_escolheu_item():void
	{
		/*
		if (cpItem.selectedItem==null)
			return;
		item_selecionado = cpItem.selectedItem;
		foco_vai_para = cpItem;
		*/
		
		if (cpItem.itemSelecionado == null)
			return;
		item_selecionado = cpItem.itemSelecionado;
		foco_vai_para = cpItem.txtPesquisaBox;
		
		var filtro:MeuFiltroWhere =
			new MeuFiltroWhere(n.cache.arrayItemEmpEstoque)
			.andEquals(item_selecionado.id,ItemEmpEstoque.campo_idItem);
			//.andEquals(ss.idEmp, ItemEmpEstoque.campo_idEmp);
			//.andGreater(0, ItemEmpEstoque.campo_qtd);
		
		arraycEstoques.removeAll();
		var estoques:Array = filtro.getResultadoArraySimples();
		/*
		if (estoques.length == 0)
		{
			AlertaSistema.mensagem("você precisa lançar quantidade de estoque para '"+item_selecionado.nome+"'");
			sistema_limpa_item();
			return;
		}else */
		if (estoques.length == 1)
		{
			sistema_lanca_estoque(estoques[0]);
		}
		else
		{
			sistema_abrepopup_estoques();
		}
		
		arraycEstoques.source = estoques;
		
		
		//AlertaSistema.mensagem(cpItem.selectedItem.id);
	}
	
	private function usuario_escolheu_estoque():void
	{
		if (cpEstoque.selectedItem==null)
			return;
		foco_vai_para = cpEstoque;
		sistema_lanca_estoque(cpEstoque.selectedItem);
	}
	
	
	
	private function sistema_abrepopup_estoques():void
	{
		PopUpManager.addPopUp(popupEstoque, Application.application.gerenteJanelas, true);
		PopUpManager.centerPopUp(popupEstoque);
		dtGridEstoques.selectedIndex=0;
		dtGridEstoques.setFocus();
	}
	
	private function popupEstoque_usuario_escolheu_estoque():void
	{
		usuario_fecha_popup_estoques();
		var estoque:ItemEmpEstoque = dtGridEstoques.selectedItem as ItemEmpEstoque;
		item_selecionado = n.cache.getItem(estoque.idItem);
		sistema_lanca_estoque(estoque);
	}
	
	private function usuario_fecha_popup_estoques():void
	{
		PopUpManager.removePopUp(popupEstoque);
	}
	
	
	
	private function sistema_lanca_estoque(estoque:ItemEmpEstoque):void
	{
		AlertaSistema.mensagem("lancando estoque "+estoque.identificador);
		estoque_lancado=estoque;
		nsQtd.setFocus();
	}
	
	private function usuario_define_quantidade():void
	{
		for each (var bi:BalancoItem in arraycBalancoItens)
		{
			if (bi.idItem != item_selecionado.id)
				continue;
				
			Alert.show("Estem item já foi lançado,\nDeseja lançar novamente?", null,Alert.YES | Alert.NO, null, close_event_handler);
			return;
	        break;
		}
		
		sistema_lanca_item();
	}
	
	private function sistema_lanca_item():void
	{
		var iep:ItemEmpPreco;
		for each (var xxx:ItemEmpPreco in App.single.cache.arrayItemEmpPreco){
			if (xxx.idItem != item_selecionado.id)
				continue;
			iep = xxx.clone();
			break;
		}
		
		var bi:BalancoItem = new BalancoItem();
		bi.idBalanco = balanco_selecionado.id;
		bi.idOperacao = balanco_selecionado.idOperacao;
		bi.idTransacao = balanco_selecionado.idTransacao;
		bi.idItem = item_selecionado.id;
		bi.idIEE = estoque_lancado.id;
		bi.item_nome = item_selecionado.nome;
		bi.rfUnica = item_selecionado.rfUnica;
		bi.rfAuxiliar = item_selecionado.rfAuxiliar;
		bi.estoque_identificador = estoque_lancado.identificador;
		bi.qtdAnterior = estoque_lancado.qtd;
		bi.qtdLancada = nsQtd.value;
		bi.custo = iep.custo;
		bi.compra = iep.compra;
		bi.venda = iep.venda;
		
		n.modificacoes.Balanco_Lanca(bi, preencheBalancoItens);
			/*
			function():void
			{
				preencheBalancoItens();
			}
			*/
		
		cpItem.itemSelecionado = null;
		cpItem.txtPesquisaBox.text = "";
		cpEstoque.selectedItem = null;
		nsQtd.value = 0;
		estoque_lancado = null;
		item_selecionado = null;
		foco_vai_para.setFocus();
	}
	
	private function close_event_handler(ev:CloseEvent):void
	{
		if (ev.detail == Alert.YES)
			sistema_lanca_item();
	}
	
	private function btnAtualizarValores_Click():void{
		valorTotalCusto = 0;
		valorTotalVenda = 0;
		for each (var bi:BalancoItem in n.cache.cloneBalancoItem){
			var iep:ItemEmpPreco = App.single.cache.getItemEmpPreco(bi.idItem);
			valorTotalCusto += iep.custo * bi.qtdLancada;
			valorTotalVenda += iep.venda * bi.qtdLancada;
		}
		lblPrecoTotalCusto.text = Formatadores.unica.formataValor(valorTotalCusto, true);
		lblPrecoTotalVenda.text = Formatadores.unica.formataValor(valorTotalVenda, true);
	}
	
	private function btnRelatorioParcial_Click():void{
		App.single.ss.nuvens.modificacoes.RelatorioBalanco_Parcial(balanco_selecionado.id,
			function():void{
				if (Application.application.gerenteConexaoDesktop){
					Application.application.gerenteConexaoDesktop.baixaRelatorioParcialBalanco(App.single.idCorp);
				}
				else{
					AlertaSistema.mensagem("É necessário estar conectado ao SDE Desktop para a impressão");
				}
			}
		);
	}
	
	/**RELATÓRIOS*/
	
	private function btnItensBalanco_click():void
	{
		var imprimirNoIE:String = App.single.ss.parametrizacao.getParametro(Variaveis_SdeConfig.RELATORIO_ABREIMPRESSAO_IE);
		var urlStr:String;
		var url:URLRequest = new URLRequest();
		var vars:URLVariables = new URLVariables();
		
		url.data = vars;
		vars.idCorp = Sessao.unica.idCorp;
		vars.idEmp = Sessao.unica.idEmp;
		vars.tipo_impressao = "relatorio";
		vars.relatorio = "Itens Balanço";
		
		if (imprimirNoIE == "1" && Application.application.gerenteConexaoDesktop!=null)
		{
			urlStr = "http://sistemadaempresa.com.br/sde/imprime.aspx";
			urlStr+="?idCorp="+Sessao.unica.idCorp;
			urlStr+="&idEmp="+Sessao.unica.idEmp;
			urlStr+="&tipo_impressao=relatorio";
			urlStr+="&relatorio=Itens Balanço";
			urlStr+="&idBalanco="+idBalanco;
			
			urlStr = "\""+urlStr+"\"";
			
			Application.application.gerenteConexaoDesktop.iniciaProcesso("explorer",urlStr);
			return; 
		}
		else
		{
			url.url = "imprime.aspx";
			vars.idBalanco = idBalanco;
		}
		
		navigateToURL(url, "_blank");
	}
	
	private function btnEstoqueNaData():void
	{
		var imprimirNoIE:String = App.single.ss.parametrizacao.getParametro(Variaveis_SdeConfig.RELATORIO_ABREIMPRESSAO_IE);
		var urlStr:String;
		var url:URLRequest = new URLRequest();
		var vars:URLVariables = new URLVariables();
		
		url.data = vars;
		vars.idCorp = Sessao.unica.idCorp;
		vars.idEmp = Sessao.unica.idEmp;
		vars.tipo_impressao = "relatorio";
		vars.relatorio = "Estoque na Data";
		
		if (imprimirNoIE == "1" && Application.application.gerenteConexaoDesktop!=null)
		{
			urlStr = "http://sistemadaempresa.com.br/sde/imprime.aspx";
			urlStr+="?idCorp="+Sessao.unica.idCorp;
			urlStr+="&idEmp="+Sessao.unica.idEmp;
			urlStr+="&tipo_impressao=relatorio";
			urlStr+="&relatorio=Estoque na Data";
			urlStr+="&idBalanco="+idBalanco;
			
			urlStr = "\""+urlStr+"\"";
			
			Application.application.gerenteConexaoDesktop.iniciaProcesso("explorer",urlStr);
			return; 
		}
		else
		{
			url.url = "imprime.aspx";
			vars.idBalanco = idBalanco;
		}
		
		navigateToURL(url, "_blank");
	}
	
	private function btnVerificacaoBalanco():void
	{
		PopUpManager.addPopUp(popupRelatorioVerificacaoBalanco, Application.application.gerenteJanelas, true);
		PopUpManager.centerPopUp(popupRelatorioVerificacaoBalanco);
	}
	
	private function relVerificacaoBalanco():void
	{
		var imprimirNoIE:String = App.single.ss.parametrizacao.getParametro(Variaveis_SdeConfig.RELATORIO_ABREIMPRESSAO_IE);
		var urlStr:String;
		var url:URLRequest = new URLRequest();
		var vars:URLVariables = new URLVariables();
		
		url.data = vars;
		vars.idCorp = Sessao.unica.idCorp;
		vars.idEmp = Sessao.unica.idEmp;
		vars.tipo_impressao = "relatorio";
		vars.relatorio = "Verificação de Balanço";
		
		if (imprimirNoIE == "1" && Application.application.gerenteConexaoDesktop!=null)
		{
			urlStr = "http://sistemadaempresa.com.br/sde/imprime.aspx";
			urlStr+="?idCorp="+Sessao.unica.idCorp;
			urlStr+="&idEmp="+Sessao.unica.idEmp;
			urlStr+="&tipo_impressao=relatorio";
			urlStr+="&relatorio=Verificação de Balanço";
			urlStr+="&idBalanco="+idBalanco;
			urlStr+="&somenteDivergencias="+chkbSomenteDivergencias.selected;
			
			urlStr = "\""+urlStr+"\"";
			
			Application.application.gerenteConexaoDesktop.iniciaProcesso("explorer",urlStr);
			return; 
		}
		else
		{
			url.url = "imprime.aspx";
			vars.idBalanco = idBalanco;
			vars.somenteDivergencias = chkbSomenteDivergencias.selected;
		}
		
		navigateToURL(url, "_blank");
		
		popupRelatorioVerificacaoBalanco.parent.removeChild(popupRelatorioVerificacaoBalanco);
	}
	
	