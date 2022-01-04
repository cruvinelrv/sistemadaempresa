
import Core.Alerta.AlertaSistema;
import Core.App;

import SDE.Entidade.Cx_Lancamento;


	private function btnLancarEntrada_Click():void{
		if (nsValorEntrada.value == 0){
			AlertaSistema.mensagem("Valor da entrada deve ser maior que 0 (zero)");
			return;
		}
		
		var cxL:Cx_Lancamento = new Cx_Lancamento();
		cxL.valorRecebido = nsValorEntrada.value;
		cxL.valorOriginal = nsValorEntrada.value;
		cxL.observacoes = txtHistContaCaixa.text;
		
		App.single.n.modificacoes.Caixa_Entrada(cxL,
			function():void{
				AlertaSistema.mensagem("Entrada realizada");
				limpaTela();
			}
		);
	}
	
	private function limpaTela():void{
		nsValorEntrada.value = 0;
		txtHistContaCaixa.text = "";
		nsValorEntrada.setFocus();
	}