import Core.Alerta.AlertaSistema;
import Core.App;
import Core.Sessao;
import Core.Utils.Formatadores;

import SDE.Entidade.Cx_Diario;
import SDE.Enumerador.ECxDiarioSituacao;

import mx.collections.ArrayCollection;
import mx.collections.Sort;
import mx.collections.SortField;
import mx.formatters.CurrencyFormatter;
	
	private var saldo:Number;
	private var saldoDiaAnterior:Number;
	private var millisecondsPerDay:int = 1000 * 60 * 60 * 24;
	
	private var saldoAnterior:Number;
	
	private function create():void
	{
		dtfAbertura.selectableRange = {
			rangeStart: new Date(2009,0,1),
			rangeEnd: new Date()
		}
		
		verificaExitenciaAbertura();
		saldoAnterior = getValorUtimoDiaMovimentado();
		lblSaldoAnterior.text = Formatadores.unica.formataValor(saldoAnterior, true);
		nsSaldo_ValueCommit();
	}
	
	private function dtfAbertura_Change():void
	{
		verificaExitenciaAbertura();
		saldoAnterior = getValorUtimoDiaMovimentado();
		lblSaldoAnterior.text = Formatadores.unica.formataValor(saldoAnterior, true);
		nsSaldo_ValueCommit();	
	}
	
	private function paraFormatoMoeda(valor:Number):Number
	{
		var cf:CurrencyFormatter = new CurrencyFormatter;
		cf.precision = 2;
		cf.useThousandsSeparator = false;
		cf.currencySymbol = "";
		var str:String = cf.format(valor);
		return Number(str);
	}
	
	private function getValorUtimoDiaMovimentado():Number
	{
		var cxD:Cx_Diario = null;
		var ac:ArrayCollection = new ArrayCollection();
		var dataSelecionada:Date = dtfAbertura.selectedDate;
		for each (var xxx:Cx_Diario in App.single.cache.arrayCx_Diario)
		{
			if (Formatadores.unica.stringToDate(xxx.data).getTime() < dataSelecionada.getTime() && xxx.idEmp == Sessao.unica.idEmp)
			{
				var obj:Object = new Object();
				obj.cxD = xxx;
				obj.data = Formatadores.unica.stringToDate(xxx.data);
				ac.addItem(obj);
			}
		}
		if (ac.length == 0)
			return 0;
		
		var sort:Sort = new Sort();
		sort.fields = [new SortField("data")];
		ac.sort = sort;
		ac.refresh();
		cxD = ac.getItemAt(ac.length - 1).cxD as Cx_Diario;
		return (cxD.valorAbertura + cxD.totalEntradas) - cxD.totalSaidas;
	}
	
	private function nsSaldo_ValueCommit():void{
		saldo = saldoAnterior + nsSaldo.value;
		lblsaldoTotal.text = Formatadores.unica.formataValor(saldo, true);
	}
	
	private function verificaExitenciaAbertura():void
	{
		/**
		 * 0 == não aberto
		 * 1 == aberto pelo sistema
		 * 2 == aberto
		 * */
		var aberto:Number = 0;
		for each (var cxD:Cx_Diario in App.single.cache.arrayCx_Diario)
		{
			if (cxD.data != Formatadores.unica.formataData(dtfAbertura.selectedDate) || cxD.idEmp != App.single.idEmp)
				continue;
			if (cxD.situacao == ECxDiarioSituacao.aberto_pelo_sistema)
				aberto = 1;
			else
				aberto = 2;
			break;
		}
		
		if (aberto == 1)
		{
			currentState = "stateAberturaDiaRealizadaPeloSistema";
			lblValorAberturaSistema.text = Formatadores.unica.formataValor(getValorUtimoDiaMovimentado(), true);
		}
		else if (aberto == 2)
			currentState = "stateAberturaDiaRealizada";
		else
			currentState = null;
	}
	
	private function btnConfirmar_Click():void{
		for each (var cxD:Cx_Diario in App.single.cache.arrayCx_Diario){
			if (cxD.data != Formatadores.unica.formataData(dtfAbertura.selectedDate) || cxD.situacao != ECxDiarioSituacao.aberto || cxD.idEmp != App.single.idEmp)
				continue;
			AlertaSistema.mensagem("A abertura de caixa na data '"+cxD.data+"' já foi realizada");
			return;
		}
		
		if (dtfAbertura.selectedDate == null)
		{
			AlertaSistema.mensagem("Informe a data para a abertura de caixa");
			dtfAbertura.setFocus();
			return;
		}
		
		saldo = paraFormatoMoeda(saldo);
		
		App.single.n.modificacoes.Caixa_Abertura(Formatadores.unica.formataData(dtfAbertura.selectedDate), saldo, false,
			function ():void
			{
				AlertaSistema.mensagem("Abertura de caixa realizada");
				limpaTela();
				verificaExitenciaAbertura();
			}
		);
	}
	
	private function limpaTela():void{
		dtfAbertura.selectedDate = new Date();
		nsSaldo.value = 0;
		dtfAbertura.setFocus();
	}