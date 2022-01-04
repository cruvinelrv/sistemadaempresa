
import Core.Alerta.AlertaSistema;
import Core.App;
import Core.Utils.Formatadores;
import Core.Utils.MeuFiltroWhere;

import SDE.Entidade.Cliente;
import SDE.Entidade.Finan_Conta;
import SDE.Entidade.Finan_TipoPagamento;
import SDE.Entidade.Finan_Titulo;
import SDE.Entidade.Finan_TituloItem;
import SDE.Entidade.Mov;
import SDE.Enumerador.EContaTipo;
import SDE.Enumerador.ETituloSituacao;

import img.Imagens;

import mx.collections.ArrayCollection;
import mx.collections.Sort;
import mx.collections.SortField;
import mx.controls.Alert;
import mx.controls.dataGridClasses.DataGridColumn;
import mx.core.Application;
import mx.core.Container;
import mx.managers.PopUpManager;
import mx.utils.ObjectUtil;

import pesquisas.PesquisaCliente;
import pesquisas.PesquisaTituloItem;

	
	[Bindable] private var dpLancamentoCaixa:ArrayCollection = new ArrayCollection();
	[Bindable] private var dpCartoes:ArrayCollection;
	
	private var finanTipoPagamentoSelecionado:Finan_TipoPagamento;
	
	private var dataInicio:Date;
	private var dataLimite:Date;
	
	private var finanConta:Finan_Conta;
	
	private function createBaixa():void{
	}
	
	private function adicionaPopup(popup:Container):void
	{
		PopUpManager.addPopUp(popup, Application.application.gerenteJanelas, true);
		PopUpManager.centerPopUp(popup);
	}
	
	private function removePopup(popup:Container):void
	{
		PopUpManager.removePopUp(popup);
	}
	
	private function fn_valorPagamento(obj:Object, dgc:DataGridColumn):String{return Formatadores.unica.formataValor(obj.finanTituloItem.valorCobrado, true);}
	private function fn_valorDesconto(obj:Object, dgc:DataGridColumn):String{return Formatadores.unica.formataValor(obj.valorDesconto, true);}
	private function fn_baixar(obj:Object, dgc:DataGridColumn):String{return (obj.baixar ?"Sim":"Não");}
	
	private function btnLimparData_Click():void{
		dtfDataInicio.selectedDate = null;
		dtfDataLimite.selectedDate = null;
	}
	
	private function chkbContaDestino_Change():void{
		if (chkbContaDestino.selected)
			currentState = "stateContaCaixaBanco";
		else
			currentState = null;
	}
	
	private function realizaBusca():void{
		if (dtfDataInicio.selectedDate)
			dataInicio = dtfDataInicio.selectedDate;
		else
			dataInicio = null;
		
		if (dtfDataLimite.selectedDate)
			dataLimite = dtfDataLimite.selectedDate;
		else
			dataLimite = null;
			
		dpLancamentoCaixa = new ArrayCollection();
		var arrayClientes:Array = PesquisaCliente.pesquisar(cpCliente.text);
		var arrayTituloItens:Array = PesquisaTituloItem.pesquisar(cpFinTituloItem.text);
		var dtPagamento:Date = null;
		var arrayBusca:Array = [];
		if (dataInicio && dataLimite){
			for each (var zzz:Finan_TituloItem in arrayTituloItens){
				dtPagamento = Formatadores.unica.stringToDate(zzz.dtPagamento);
				if ((ObjectUtil.dateCompare(dataInicio, dtPagamento) <= 0) && (ObjectUtil.dateCompare(dataLimite, dtPagamento) >= 0))
					arrayBusca.push(zzz);
			}
		}
		else if (dataInicio){
			for each (var xxx:Finan_TituloItem in arrayTituloItens){
				dtPagamento = Formatadores.unica.stringToDate(xxx.dtPagamento);
				if (ObjectUtil.dateCompare(dataInicio, dtPagamento) <= 0)
					arrayBusca.push(xxx);
			}
		}
		else if (dataLimite){
			for each (var yyy:Finan_TituloItem in arrayTituloItens){
				dtPagamento = Formatadores.unica.stringToDate(yyy.dtPagamento);
				if (ObjectUtil.dateCompare(dataLimite, dtPagamento) >= 0)
					arrayBusca.push(yyy);
			}
		}
		else
			for each (var jjj:Finan_TituloItem in arrayTituloItens)
				arrayBusca.push(jjj);
		
		var indexList:Array = [];
		var arrayAprovados:Array = [];
		for each (var kkk:Finan_TituloItem in arrayBusca){
			for each (var mov:Mov in App.single.cache.arrayMov){
				var ft:Finan_Titulo = App.single.cache.getFinan_Titulo(kkk.idTitulo);
				if (mov.idTransacao !=  ft.idTransacao)
					continue;
				var contains:Boolean = false;
				for each (var cli:Cliente in arrayClientes){
					if (cli.id != mov.idCliente)
						continue;
					contains = true;
					break;
				}
				if (contains)
					if (kkk.situacao == ETituloSituacao.em_aberto)
						arrayAprovados.push(kkk);
				break;
			}
		}
		
		dpLancamentoCaixa.removeAll();
		for each (var finanTituloItem:Finan_TituloItem in arrayAprovados){
			var obj:Object = new Object();
			obj.finanTituloItem = finanTituloItem.clone();
			obj.cod = finanTituloItem.id;
			obj.numeroTitulo = finanTituloItem.identificador;
			obj.dataVencimento = finanTituloItem.dtPagamento;
			obj.data = Formatadores.unica.stringToDate(finanTituloItem.dtPagamento);
			obj.valorCobrado = finanTituloItem.valorCobrado;
			obj.valorDesconto = finanTituloItem.descontoVlr;
			obj.baixar = false;
			
			for each (var m:Mov in App.single.cache.arrayMov){
				if (m.idTransacao != App.single.cache.getFinan_Titulo(finanTituloItem.idTitulo).idTransacao)
					continue;
				obj.cliente = App.single.cache.getCliente(m.idCliente).nome;
				break;
			}
			dpLancamentoCaixa.addItem(obj);
		}
		var sort:Sort = new Sort();
		sort.fields = [new SortField("data")];
		dpLancamentoCaixa.sort = sort;
		dpLancamentoCaixa.refresh();
	}
	
	private function cpFinanConta_Change():void
	{
		if (cpFinanConta.selectedItem != null)
		{
			if ((cpFinanConta.selectedItem as Finan_Conta).tipo == EContaTipo.Banco)
			{
				currentState = "stateContaBancoDetalhe";
				lblBanco.text = (cpFinanConta.selectedItem as Finan_Conta).banco;
				lblAgencia.text = (cpFinanConta.selectedItem as Finan_Conta).ag;
				lblConta.text = (cpFinanConta.selectedItem as Finan_Conta).conta;
			}
			else
				currentState = "stateContaCaixaBanco";
			finanConta = (cpFinanConta.selectedItem as Finan_Conta).clone();
		}
		else
			currentState = null;
	}
	
	private function cmbTipoPagamento_Change():void
	{
		finanTipoPagamentoSelecionado = null;
		var tipoPagamentoSelecionatoStr:String = cmbTipoPagamento.selectedLabel;
		if (tipoPagamentoSelecionatoStr == "DINHEIRO" || tipoPagamentoSelecionatoStr == "CHEQUE A VISTA")
		{
			for each (var xxx:Finan_TipoPagamento in App.single.cache.arrayFinan_TipoPagamento)
			{
				if (xxx.grupoTipoPagamento_nome != tipoPagamentoSelecionatoStr && xxx.nome != tipoPagamentoSelecionatoStr)
					continue;
				finanTipoPagamentoSelecionado = xxx;
				break;
			}
			if (!finanTipoPagamentoSelecionado)
			{
				AlertaSistema.mensagem("Tipo Pagamento "+tipoPagamentoSelecionatoStr+" não cadastrado. Rralize o cadastro do mesmo para prosseguir.");
				return;
			}
		}
		else if (tipoPagamentoSelecionatoStr == "CARTAO CREDITO" || tipoPagamentoSelecionatoStr == "CARTAO DEBITO")
		{
			var filtro:MeuFiltroWhere =
				new MeuFiltroWhere (App.single.cache.arrayFinan_TipoPagamento)
					.andEquals(tipoPagamentoSelecionatoStr, "grupoTipoPagamento_nome");
			var retorno:Array = filtro.getResultadoArraySimples();
			
			if (retorno.length == 1)
				finanTipoPagamentoSelecionado = retorno[0];
			else if (retorno.length > 1)
			{
				dpCartoes = new ArrayCollection();
				dpCartoes.source = retorno;
				adicionaPopup(popupTipoPagamentoCartao);
			}
			else
				AlertaSistema.mensagem("Tipo Pagamento "+tipoPagamentoSelecionatoStr+" não cadastrado. Rralize o cadastro do mesmo para prosseguir.");
		}
	}
	
	private function cartaoSelecionado():void
	{
		if (!dgCartoes.selectedItem)
		{
			Alert.show("Selecione um cartão.", "Alerta SDE", 4, null, null, Imagens.unica.icn_32_alerta);
			return;
		}
		
		finanTipoPagamentoSelecionado = dgCartoes.selectedItem as Finan_TipoPagamento;
		removePopup(popupTipoPagamentoCartao);
	}
	
	private function limpaTela():void{
		cpCliente.text = "";
		//cpFinTituloItem.selectedItems.removeAll();
		cpFinTituloItem.text = "";
		btnLimparData_Click();
		chkbContaDestino.selected = false;
		cmbTipoPagamento.selectedIndex = 0;
		if (cmbCentroCusto)
			cmbCentroCusto.selectedIndex = 0;
		if (cmbTipoLancamento)
			cmbTipoLancamento.selectedIndex = 0;
		currentState = null;
		dpLancamentoCaixa.removeAll();
		dpCartoes.removeAll();
		finanTipoPagamentoSelecionado = null;
	}
	
	private function btnBaixar_Click():void{
		
		if (!finanTipoPagamentoSelecionado)
		{
			Alert.show("Selecione um tipo de pagamento.", "Alerta SDE", 4, null, null, Imagens.unica.icn_32_alerta);
			return;
		}
		
		if (currentState != null){
			if (cmbCentroCusto.selectedItem == null){
				AlertaSistema.mensagem("Selecione um Centro de Custo");
				return;
			}
			if (cmbTipoLancamento.selectedItem == null || cmbTipoLancamento.getAs().nomeTipoLancamento == "-"){
				AlertaSistema.mensagem("Selecione um Tipo de Conta");
				return;
			}
		}
		
		var arrayFinanTituloItem:Array = [];
		for each (var obj:Object in dgTituloItens.dataProvider){
			if (obj.baixar)
			{
				var finanTituloItem:Finan_TituloItem = App.single.cache.getFinan_TituloItem(obj.finanTituloItem.id).clone();
				finanTituloItem.descontoVlr = obj.valorDesconto;
				arrayFinanTituloItem.push(finanTituloItem);
			}
		}
		
		if (arrayFinanTituloItem.length == 0){
			AlertaSistema.mensagem("Nenhum titulo foi selecionado.");
			return;
		}
		
		var idCentroCusto:Number;
		var idTipoLancamento:Number;
		if (currentState == null)
			finanConta = null;
		else{
			idCentroCusto = cmbCentroCusto.getAs().id;
			idTipoLancamento = cmbTipoLancamento.getAs().id;
		}
		
		App.single.n.modificacoes.Finan_TituloBaixa(arrayFinanTituloItem, finanConta, finanTipoPagamentoSelecionado,
			idCentroCusto, idTipoLancamento,
			function():void{
				AlertaSistema.mensagem("Título(s) baixado(s)");
				//cpFinTituloItem.prompt = "Selecione um Lançamento (" + (cpFinTituloItem.dataProvider.length) +")";
				limpaTela();
			}
		);
	}