import Core.Alerta.AlertaSistema;
import Core.App;
import Core.Utils.Formatadores;
import Core.Utils.Funcoes;

import SDE.Entidade.Cx_Lancamento;
import SDE.Entidade.Finan_Conta;
import SDE.Entidade.Finan_Lancamento;
import SDE.Enumerador.EContaTipo;

import mx.controls.Alert;


	private function cpFinanConta_Change():void{
		if (cpFinanConta.selectedItem){
			if ((cpFinanConta.selectedItem as Finan_Conta).tipo == EContaTipo.Banco){
				currentState = "stateContaBancoDetalhe";
				lblBanco.text = (cpFinanConta.selectedItem as Finan_Conta).banco;
				lblAgencia.text = (cpFinanConta.selectedItem as Finan_Conta).ag;
				lblConta.text = (cpFinanConta.selectedItem as Finan_Conta).conta;
			}
			else
				currentState = null;
		}
		else
			currentState = null;
	}
	
	private function btnLancarRetirada_Click():void{
		var saldoAtualCaixa:Number = Funcoes.getValorSaldoAtualCaixa();
		if (saldoAtualCaixa < nsValorRetirada.value){
			Alert.show("A transferência não pôde ser realizada, saldo de caixa insuficiente.\nSaldo atual:"
			+ Formatadores.unica.formataValor(saldoAtualCaixa, true), "Alerta SDE", 4, null, null,
			Imagens.unica.icn_32_deleta);
			return;
		}
		if (!cmbCentroCusto.getAs()){
			AlertaSistema.mensagem("Selecione um Centro de Custo");
			return;
		}
		if (!cmbTipoLancamento.getAs() || cmbTipoLancamento.getAs().nomeTipoLancamento == "-"){
			AlertaSistema.mensagem("Selecione um Tipo de Conta");
			return;
		}
		if (nsValorRetirada.value == 0){
			AlertaSistema.mensagem("Informe o valor da retirada");
			return;
		}
		if (nsValorRetirada.value <0){
			AlertaSistema.mensagem("Valor da retirada não pode ser negativo");
			return;
		}
		/*
		if (!cpFinanConta.selectedItem){
			AlertaSistema.mensagem("Selecione a conta de destino da retirada");
			return;
		}
		*/
		var idFinanConta:Number = 0;
		if (cpFinanConta.selectedItem){
			idFinanConta = (cpFinanConta.selectedItem as Finan_Conta).id;
		}
		
		var cxL:Cx_Lancamento = new Cx_Lancamento();
		cxL.valorCobrado = nsValorRetirada.value;
		cxL.valorOriginal = nsValorRetirada.value;
		cxL.observacoes = txtHistContaCaixa.text;
		
		var finanLancamento:Finan_Lancamento = new Finan_Lancamento();
		finanLancamento.idCentroCusto = cmbCentroCusto.getAs().id;
		finanLancamento.idTipoLancamento = cmbTipoLancamento.getAs().id;
		finanLancamento.tipoLancamento_nome = cmbTipoLancamento.getAs().nomeTipoLancamento;
		finanLancamento.idContaDestino = idFinanConta;
		finanLancamento.valorBruto = nsValorRetirada.value;
		finanLancamento.valorLancado = nsValorRetirada.value;
		
		App.single.n.modificacoes.Caixa_TransferenciaConta(cxL, finanLancamento,
			function ():void{
				AlertaSistema.mensagem("Retirada Realizada");
				limpaTela();
			}
		);
	}
	
	private function limpaTela():void{
		cmbCentroCusto.selectedIndex = 0;
		cmbTipoLancamento.selectedIndex = 0;
		nsValorRetirada.value = 0;
		cpFinanConta.selectedItems.removeAll();
		txtHistContaCaixa.text = "";
		currentState = null;
	}