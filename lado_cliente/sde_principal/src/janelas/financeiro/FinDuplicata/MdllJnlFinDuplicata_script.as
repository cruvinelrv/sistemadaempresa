
import Core.Alerta.AlertaSistema;
import Core.App;
import Core.Utils.Formatadores;
import Core.Utils.Funcoes;

import SDE.Entidade.Cliente;
import SDE.Entidade.Finan_TituloItem;
import SDE.Entidade.Mov;
import SDE.Enumerador.ETituloSituacao;

import flash.events.Event;

import mx.collections.ArrayCollection;
import mx.core.Application;
import mx.utils.ObjectUtil;

	
	[Bindable] private var dpTituloDuplicata:ArrayCollection;
	[Bindable] private var tituloSelecionado:Finan_TituloItem;
	
	private var dataInicio:Date;
	private var dataLimite:Date;
	
	private function create():void{
		dgTituloItens.addEventListener('gerarDuplicataChecked', gerarDuplicataChecked);
	}
	
	private function btnLimparData():void{
		dtfDataInicio.selectedDate = null;
		dtfDataLimite.selectedDate = null;
	}
	
	private function cpCliente_KeyUp():void{
		if (cpCliente.dropDown)
			cpCliente.dropDown.visible = false;
		realizaBusca();
	} 
	
	private function cpFinTituloItem_KeyUp():void{
		if (cpFinTituloItem.dropDown)
			cpFinTituloItem.dropDown.visible = false;
		if (cpFinTituloItem.textInput.text.length < 7)
			return;
		realizaBusca();
	} 
	
	private function dtfDataInicio_ValueCommit():void{
		if (dtfDataInicio.selectedDate)
			dataInicio = dtfDataInicio.selectedDate;
		else
			dataInicio = null;
		realizaBusca();
	}
	
	private function dtfDataLimite_ValueCommit():void{
		if (dtfDataLimite.selectedDate)
			dataLimite = dtfDataLimite.selectedDate;
		else
			dataLimite = dtfDataLimite.selectedDate;
		realizaBusca();
	}
	
	private function realizaBusca():void{
		dpTituloDuplicata = new ArrayCollection();
		var dtPagamento:Date = null;
		var arrayBusca:Array = [];
		if (dataInicio && dataLimite){
			for each (var zzz:Finan_TituloItem in cpFinTituloItem.dataProvider){
				dtPagamento = Formatadores.unica.stringToDate(zzz.dtPagamento);
				if ((ObjectUtil.dateCompare(dataInicio, dtPagamento) <= 0) && (ObjectUtil.dateCompare(dataLimite, dtPagamento) >= 0))
					arrayBusca.push(zzz);
			}
		}
		else if (dataInicio){
			for each (var xxx:Finan_TituloItem in cpFinTituloItem.dataProvider){
				dtPagamento = Formatadores.unica.stringToDate(xxx.dtPagamento);
				if (ObjectUtil.dateCompare(dataInicio, dtPagamento) <= 0)
					arrayBusca.push(xxx);
			}
		}
		else if (dataLimite){
			for each (var yyy:Finan_TituloItem in cpFinTituloItem.dataProvider){
				dtPagamento = Formatadores.unica.stringToDate(yyy.dtPagamento);
				if (ObjectUtil.dateCompare(dataLimite, dtPagamento) >= 0)
					arrayBusca.push(yyy);
			}
		}
		else
			for each (var jjj:Finan_TituloItem in cpFinTituloItem.dataProvider)
				arrayBusca.push(jjj);
		
		var indexList:Array = [];
		var arrayAprovados:Array = [];
		dpTituloDuplicata.removeAll();
		for each (var finanTituloItem:Finan_TituloItem in arrayBusca){
			for each (var mov:Mov in App.single.cache.arrayMov){
				if (mov.idTransacao !=  App.single.cache.getFinan_Titulo(finanTituloItem.idTitulo).idTransacao)
					continue;
				var contains:Boolean = false;
				for each (var cli:Cliente in cpCliente.dataProvider){
					if (cli.id != mov.idCliente)
						continue;
					contains = true;
					break;
				}
				if (contains){
					var obj:Object = new Object();
					obj.finanTituloItem = finanTituloItem.clone();
					obj.cod = finanTituloItem.id;
					obj.numeroTitulo = finanTituloItem.identificador;
					obj.dataVencimento = finanTituloItem.dtPagamento;
					obj.valorCobrado = finanTituloItem.valorCobrado;
					obj.gerado = (finanTituloItem.situacao == ETituloSituacao.duplicata_impressa) ? true : false;
					
					for each (var m:Mov in App.single.cache.arrayMov){
						if (m.idTransacao != App.single.cache.getFinan_Titulo(finanTituloItem.idTitulo).idTransacao)
							continue;
						obj.cliente = App.single.cache.getCliente(m.idCliente).nome;
						break;
					}
					dpTituloDuplicata.addItem(obj);
				}
				//arrayAprovados.push(kkk);
				break;
			}
		}
	}
	
	private function gerarDuplicataChecked(ev:Event):void{
		ev.target.data.gerarDuplicata = (ev.target.data.gerarDuplicata) ? false : true;	
	}
	
	private function btnSelecionar_Click():void{
		tituloSelecionado = (dgTituloItens.selectedItem.finanTituloItem as Finan_TituloItem).clone();
		lblNumeroTitulo.text = tituloSelecionado.identificador;
		lblValorTitulo.text = tituloSelecionado.valorCobrado.toString();
		doBinding();
	}
	
	private function doBinding():void{
		Funcoes.myBind(nsDescontoPct, "value", tituloSelecionado, "descontoPct");
		Funcoes.myBind(nsDescontoVlr, "value", tituloSelecionado, "descontoVlr");
		Funcoes.myBind(txtCondEspeciais, "text", tituloSelecionado, "condEspeciais");
	}
	
	private function nsDescontoPct_Commited():void{
		nsDescontoVlr.value = (tituloSelecionado.valorCobrado * nsDescontoPct.value) / 100;
	}
	
	private function nsDescontoVlr_Commited():void{
		nsDescontoPct.value = (nsDescontoVlr.value * 100) / tituloSelecionado.valorCobrado;
	}
	
	private function btnGerarDuplicata_Click():void{
		tituloSelecionado.descontoValidade = dtfDescontoValidade.text;
		
		/*
		//solução temporária
		var listaTituloItem:Array = [];
		listaTituloItem.push(tituloSelecionado);
		*/
		
		App.single.n.modificacoes.Finan_DuplicataIpressa(tituloSelecionado,
			function(retotnoFinanTituloItem:Finan_TituloItem):void{
				var listaTituloItem:Array = [];
				listaTituloItem.push(retotnoFinanTituloItem);
				App.single.n.modificacoes.Finan_DuplicataEscrevePdf(listaTituloItem,
					function():void{
						if (Application.application.gerenteConexaoDesktop){
		            		Application.application.gerenteConexaoDesktop.baixaDuplicata(App.single.ss.idCorp, App.single.ss.idEmp, retotnoFinanTituloItem.identificador, "Duplicata");
		            	}
		            	else{
		            		AlertaSistema.mensagem("É necessário estar conectado ao SDE Desktop para a impressão");
		            	}
					}
				);
				limpaTela();
				btnLimparData();
				mudaTela(telaBusca);
				dpTituloDuplicata.removeAll();
			}
		);
	}
	
	private function limpaSelecionado():void{
		lblNumeroTitulo.text = "";
		lblValorTitulo.text = "";
		lblValorComDesconto.text = "";
		nsDescontoPct.value = 0;
		nsDescontoVlr.value = 0;
		dtfDescontoValidade.selectedDate = null;
		dtfDescontoValidade.text = "";
		txtCondEspeciais.text = "";
		tituloSelecionado = null;
	}
	
	private function limpaTela():void{
		limpaSelecionado();
		cpCliente.selectedItems.removeAll();
		cpCliente.textInput.text = "";
		cpFinTituloItem.selectedItems.removeAll();
		cpFinTituloItem.textInput.text = "";
	}