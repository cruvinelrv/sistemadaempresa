
import Core.Alerta.AlertaSistema;
import Core.App;
import Core.Utils.Formatadores;

import SDE.Entidade.Cliente;
import SDE.Entidade.Finan_Conta;
import SDE.Entidade.Finan_Titulo;
import SDE.Entidade.Finan_TituloItem;
import SDE.Enumerador.EContaTipo;
import SDE.Enumerador.ETipoTitulo;
import SDE.Enumerador.ETituloSituacao;

import flash.events.Event;

import mx.collections.ArrayCollection;
import mx.controls.dataGridClasses.DataGridColumn;
import mx.utils.ObjectUtil;

	[Bindable] private var dpLancamentoCaixa:ArrayCollection = new ArrayCollection();
	private var arrayTitulosItemPagar:Array;
	private var finanConta:Finan_Conta;
	
	private function createBaixa():void{
		dgTituloItens.addEventListener('baixarChecked', baixarChecked);
		atualizaTituloItemPagar();
	}
	
	private function fn_valorPagamento(obj:Object, dgc:DataGridColumn):String{return Formatadores.unica.formataValor(obj.finanTituloItem.valorCobrado, true);}
	
	private function atualizaTituloItemPagar(pesquisa:String=""):void{
		arrayTitulosItemPagar = [];
		for each (var ft:Finan_Titulo in App.single.cache.arrayFinan_Titulo){
			if (ft.tipo == ETipoTitulo.titulo_a_pagar && ft.situacao == ETituloSituacao.em_aberto){
				for each (var fti:Finan_TituloItem in App.single.cache.arrayFinan_TituloItem){
					if (fti.idTitulo == ft.id && fti.situacao == ETituloSituacao.em_aberto && (fti.identificador.search(pesquisa) > -1)){
						arrayTitulosItemPagar.push(fti);
					}
				}
			}
		}
	}
	
	private function btnLimparData_Click():void{
		dtfDataInicio.selectedDate = null;
		dtfDataLimite.selectedDate = null;
	}
	
	private function chkbContaOrigem_Change():void{
		if (chkbContaOrigem.selected)
			currentState = "stateContaCaixaBanco";
		else
			currentState = null;
	}
	
	private function baixarChecked(ev:Event):void{
		ev.target.data.baixar = (ev.target.data.baixar) ? false : true;
	}
	
	private function btnPesquisar_Click():void{
		var dataInicio:Date;
		var dataLimite:Date;
		var dtPagamento:Date = null;
		var arrayBusca:Array = [];
		
		atualizaTituloItemPagar(txtFinanTituloItem.text);
		
		if (dtfDataInicio.selectedDate)
			dataInicio = dtfDataInicio.selectedDate;
		else
			dataInicio = null;
		if (dtfDataLimite.selectedDate)
			dataLimite = dtfDataLimite.selectedDate;
		else
			dataLimite = null;
			
		if (dataInicio && dataLimite){
			for each (var zzz:Finan_TituloItem in arrayTitulosItemPagar){
				dtPagamento = Formatadores.unica.stringToDate(zzz.dtPagamento);
				if ((ObjectUtil.dateCompare(dataInicio, dtPagamento) <= 0) && (ObjectUtil.dateCompare(dataLimite, dtPagamento) >= 0))
					arrayBusca.push(zzz);
			}
		}
		else if (dataInicio){
			for each (var xxx:Finan_TituloItem in arrayTitulosItemPagar){
				dtPagamento = Formatadores.unica.stringToDate(xxx.dtPagamento);
				if (ObjectUtil.dateCompare(dataInicio, dtPagamento) <= 0)
					arrayBusca.push(xxx);
			}
		}
		else if (dataLimite){
			for each (var yyy:Finan_TituloItem in arrayTitulosItemPagar){
				dtPagamento = Formatadores.unica.stringToDate(yyy.dtPagamento);
				if (ObjectUtil.dateCompare(dataLimite, dtPagamento) >= 0)
					arrayBusca.push(yyy);
			}
		}
		else
			for each (var jjj:Finan_TituloItem in arrayTitulosItemPagar)
				arrayBusca.push(jjj);
		
		var arrayBuscaFornecedor:Array = [];
		if (cpFornecedorBax.selectedItem)
			arrayBuscaFornecedor.push(cpFornecedorBax.selectedItem);
		else
			for each (var f:Cliente in cpFornecedorBax.dataProvider)
				arrayBuscaFornecedor.push(f);
		
		var arrayAprovados:Array = [];
		for each (var fornecedor:Cliente in arrayBuscaFornecedor){
			for each (var nnn:Finan_TituloItem in arrayBusca){
				if (fornecedor.id != App.single.cache.getFinan_Titulo(nnn.idTitulo).idClienteAReceber)
					continue;
				arrayAprovados.push(nnn);
			}
		}
		
		dpLancamentoCaixa.removeAll();
		for each (var finanTituloItem:Finan_TituloItem in arrayAprovados){
			var obj:Object = new Object();
			obj.finanTituloItem = finanTituloItem.clone();
			obj.cod = finanTituloItem.id;
			obj.numeroTitulo = finanTituloItem.identificador;
			obj.dataVencimento = finanTituloItem.dtPagamento;
			obj.valorCobrado = finanTituloItem.valorCobrado;
			obj.baixar = false;
			
			for each (var c:Cliente in App.single.cache.arrayCliente){
				if (c.id != App.single.cache.getFinan_Titulo(finanTituloItem.idTitulo).idClienteAReceber)
					continue;
				obj.fornecedor = c.nome;
				break;
			}
			dpLancamentoCaixa.addItem(obj);
		}
	}
	
	private function cpFinanConta_Change():void{
		if (cpFinanConta.selectedItem){
			if ((cpFinanConta.selectedItem as Finan_Conta).tipo == EContaTipo.Banco){
				currentState = "stateContaBancoDetalhe";
				lblBanco.text = (cpFinanConta.selectedItem as Finan_Conta).banco;
				lblAgencia.text = (cpFinanConta.selectedItem as Finan_Conta).ag;
				lblConta.text = (cpFinanConta.selectedItem as Finan_Conta).conta;
				finanConta = cpFinanConta.selectedItem as Finan_Conta;
			}
			else
				currentState = "stateContaCaixaBanco";
		}
		else
			currentState = null;
	}
	
	private function btnBaixa_Click():void{
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
				arrayFinanTituloItem.push(App.single.cache.getFinan_TituloItem(obj.finanTituloItem.id).clone());
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
		
		App.single.n.modificacoes.Finan_ContasPagar_Baixa(arrayFinanTituloItem, finanConta, cmbTipoPagamento.selectedLabel,
			idCentroCusto, idTipoLancamento,
			function():void{
				AlertaSistema.mensagem("Título(s) baixado(s)");
				atualizaTituloItemPagar();
				limpaTelaBaixa();
			}
		);
	}
	
	private function limpaTelaBaixa():void{
		cpFornecedorBax.selectedItems.removeAll();
		cpFornecedorBax.textInput.text = "";
		txtFinanTituloItem.text = "";
		btnLimparData_Click();
		chkbContaOrigem.selected = false;
		cmbTipoPagamento.selectedIndex = 0;
		if (cmbCentroCusto)
			cmbCentroCusto.selectedIndex = 0;
		if (cmbTipoLancamento)
			cmbTipoLancamento.selectedIndex = 0;
		if (cpFinanConta)
			cpFinanConta.selectedItems.removeAll();
		currentState = null;
		dpLancamentoCaixa.removeAll();
	}