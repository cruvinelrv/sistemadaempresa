import Core.App;

import SDE.Entidade.Cx_Lancamento;
import SDE.Entidade.Mov;
import SDE.Entidade.MovItem;
import SDE.Entidade.Orcamento_Lancamento;
import SDE.Enumerador.EMovTipo;

import flash.events.Event;

import mx.managers.PopUpManager;
	
	private function usuario_abre_popup_importa_mov():void
	{
		popup_importar_venda.cmbTipoMov.visible = false;
		popup_importar_venda.lbTipoMov.visible = false;
		popup_importar_venda.retornaMov = true;
		popup_importar_venda.dgcRetorna.visible = true;
		PopUpManager.addPopUp(popup_importar_venda, this, true);
		PopUpManager.centerPopUp(popup_importar_venda);
		popup_importar_venda.cmbTipoMov.selectedItem = EMovTipo.saida_venda;
		popup_importar_venda.filtraBuscaMov();
	}
	
	private function usuario_retorna_popup_importa_mov(ev:Event):void
	{
		var movRet:Mov = ev.target.data;
		usuario_fecha_popup_importa_mov();
		
		isImportadoOS = false;
		
		sistema_limpar_carrinho(false);
		
		cpCliente.selectedItem = cache.getCliente(movRet.idCliente).clone();
		cliente_selecionado = cache.getCliente(movRet.idCliente).clone();
		change_cpCliente();
		
		cpVendedor.selectedItem = cache.getCliente(movRet.idClienteFuncionarioVendedor);
		mov.idClienteFuncionarioVendedor = movRet.idClienteFuncionarioVendedor;
		
		for each (var mi:MovItem in App.single.cache.arrayMovItem)
		{
			if (mi.idMov == movRet.id)
				sistema_coloca_item_carrinho(mi);
		}
		
		if (movRet.tipo == EMovTipo.outros_orcamento){
			for each (var ol:Orcamento_Lancamento in App.single.cache.arrayOrcamento_Lancamento){
				if (ol.idTransacao == movRet.idTransacao){
				var cl:Cx_Lancamento = new Cx_Lancamento();
				cl.idClientePagador = ol.idClientePagador
				cl.idClienteRecebedor = ol.idClienteRecebedor;
				cl.idGrupoTipoPagamento = ol.idGrupoTipoPagamento;
				cl.idMovMapa = ol.idMovMapa;
				cl.idPortador = ol.idPortador;
				cl.idTipoPagamento = ol.idTipoPagamento;
				cl.dtPagamento = ol.dtPagamento;
				cl.nome = ol.nome;
				cl.observacoes = ol.observacoes;
				cl.tipoLancamentoDoCaixa = ol.tipoLancamentoDoCaixa;
				cl.tipoPagamento_nome = ol.tipoPagamento_nome;
				cl.tipoPagamento_pctComissao = ol.tipoPagamento_pctComissao;
				cl.txJuroAtraso = ol.txJuroAtraso;
				cl.txJuroParcelamento = ol.txJuroParcelamento;
				cl.txMultaAtraso = ol.txMultaAtraso;
				cl.valorCobrado = ol.valorCobrado;
				cl.valorOriginal = ol.valorOriginal;
				cl.valorRecebido = ol.valorRecebido;
				arraycLancamentosCaixa.addItem(cl);
				}
			}
		}
		else
		{
			for each (var cxL:Cx_Lancamento in App.single.cache.arrayCx_Lancamento)
			{
				if (cxL.idTransacao == movRet.idTransacao)
					arraycLancamentosCaixa.addItem(cxL.clone());
			}
		}
		
		sistema_define_valor_falta_jogar_para_titulos();
	}
	
	private function usuario_fecha_popup_importa_mov():void
	{
		PopUpManager.removePopUp(popup_importar_venda);
	}