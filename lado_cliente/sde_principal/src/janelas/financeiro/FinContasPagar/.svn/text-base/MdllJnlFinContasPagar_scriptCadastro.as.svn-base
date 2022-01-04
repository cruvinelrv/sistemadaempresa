
import Core.Alerta.AlertaSistema;
import Core.App;
import Core.Utils.Formatadores;

import SDE.Entidade.Finan_Titulo;
import SDE.Entidade.Finan_TituloItem;

import mx.collections.ArrayCollection;
import mx.controls.dataGridClasses.DataGridColumn;


	[Bindable] private var dpParcelas:ArrayCollection = new ArrayCollection();
	
	private function lblFunction_valorParcela(xxx:Object, dgc:DataGridColumn):String{
		return Formatadores.unica.formataValor(xxx.valorCobrado, true);
	}
	
	private function lblFnction_dataPagamento(xxx:Object, dgc:DataGridColumn):String{
		return Formatadores.unica.formataData(xxx.dtPagamento);
	}
	
	private function chkbPagamentoParcelado_Click():void{
		if (chkbPagamentoParcelado.selected)
			currentState = "statePagamentoParcelado";
		else
			currentState = null;
	}
	
	private function btnLimparParcelas_Click():void{
		if (dpParcelas.length > 0)
			dpParcelas.removeAll();
	}
	
	private function btnLancarParcelas_Click():void{
		btnLimparParcelas_Click();
		if (!dtfDataPagamento.selectedDate)
		{
			AlertaSistema.mensagem("Selecione uma data base.");
			return;
		}
		if (nsValor.value == 0)
		{
			AlertaSistema.mensagem("Valor deve ser maior que 0 (zero).");
			return;
		}
		if (nsNumeroParcelas.value == 0){
			AlertaSistema.mensagem("Número de parcelas deve ser maior que 0 (zero).");
			return;
		}
		
		var dt:Date = Formatadores.unica.stringToDate(dtfDataPagamento.text);
		var parcela:Number = 1;
		
		var numeroParcelas:Number = nsNumeroParcelas.value;
		var juros:Number = nsJuros.value;
		var periodo:Number = nsPeriodo.value;
		
		var valorTotal:Number = nsValor.value;
		var valorContabilizado:Number = 0;
		var valorParcela:Number = Math.round(valorTotal/numeroParcelas*100)/100;
		
		while (numeroParcelas > 0){
			if (periodo == 0)
				dt.month += 1;
			else
				dt.date += periodo;
				
			var strData:String = Formatadores.unica.formataData(dt);
			
			var obj:Object = new Object();
			obj.dtPagamento = Formatadores.unica.stringToDate(strData);
			obj.parcela = parcela;
			obj.valorCobrado = valorParcela * (100 + juros) / 100;
			obj.obs = txtObs.text;
			
			dpParcelas.addItem(obj);
			numeroParcelas--;
			parcela ++;
		}
	}
	
	private function btnConfirmar_Click():void{
		
		if (!cpFornecedorCad.selectedItem){
			AlertaSistema.mensagem("Selecione um fornecedor");
			return;
		}
		if (txtDescricao.text == ""){
			AlertaSistema.mensagem("Digite uma descição para o pagamento");
			return;
		}
		if (!dtfDataPagamento.selectedDate)
		{
			AlertaSistema.mensagem("Selecione uma data base.");
			return;
		}
		if (nsValor.value == 0)
		{
			AlertaSistema.mensagem("Valor deve ser maior que 0 (zero).");
			return;
		}
		
		var arrayFinanTitulo:Array = [];
		var finanTituloItem:Finan_TituloItem;
		if (chkbPagamentoParcelado.selected){
			if (dpParcelas.length == 0)
			{
				AlertaSistema.mensagem("É necessário lançar parcelas para pagamento parcelado.");
				return;
			}
			for each(var obj:Object in dpParcelas){
				finanTituloItem = new Finan_TituloItem();
				finanTituloItem.dtPagamento = Formatadores.unica.formataData(obj.dtPagamento);
				finanTituloItem.parcela = obj.parcela;
				finanTituloItem.valorCobrado = obj.valorCobrado;
				finanTituloItem.obs = obj.obs; 
				arrayFinanTitulo.push(finanTituloItem);
			}
		}
		else{
			finanTituloItem = new Finan_TituloItem();
			finanTituloItem.dtPagamento = Formatadores.unica.formataData(dtfDataPagamento.selectedDate);
			finanTituloItem.parcela = 1;
			finanTituloItem.valorCobrado = nsValor.value;
			finanTituloItem.obs = txtObs.text;
			
			arrayFinanTitulo.push(finanTituloItem);
		}
		
		var finanTitulo:Finan_Titulo = new Finan_Titulo();
		finanTitulo.idClienteAReceber = cpFornecedorCad.selectedItem.id;
		finanTitulo.descricao = txtDescricao.text;
		finanTitulo.obs = txtObs.text;
		if (chkbPagamentoParcelado.selected)
			finanTitulo.txJuroParcelamento = nsJuros.value;
		finanTitulo.valorOriginal = nsValor.value;
		var valorCobrado:Number = 0;
		for each (var fti:Finan_TituloItem in arrayFinanTitulo)
			valorCobrado += fti.valorCobrado;
		finanTitulo.valorCobrado = valorCobrado;
		
		App.single.n.modificacoes.Finan_ContasPagar_Novo(finanTitulo, arrayFinanTitulo,
			function ():void{
				AlertaSistema.mensagem("Conta a pagar cadastrada.");
				limpaTelaCadastro();
			}
		);
	}
	
	private function limpaTelaCadastro():void{
		cpFornecedorCad.selectedItems.removeAll();
		txtDescricao.text = "";
		dtfDataPagamento.selectedDate = null;
		nsValor.value = 0;
		if (nsNumeroParcelas) nsNumeroParcelas.value = 0;
		if (nsJuros) nsJuros.value = 0;
		if (nsPeriodo) nsPeriodo.value = 0;
		if (txtObs) txtObs.text = "";
		dpParcelas.removeAll();
		currentState = null;
		chkbPagamentoParcelado.selected = false;
	}