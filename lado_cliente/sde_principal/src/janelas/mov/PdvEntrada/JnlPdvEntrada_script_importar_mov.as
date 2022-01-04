import Componentes.PopUpPesquisa.PopPesquisa_Vendas;

import Core.App;

import SDE.Entidade.Cx_Lancamento;
import SDE.Entidade.Mov;
import SDE.Entidade.MovItem;
import SDE.Enumerador.EMovTipo;

import flash.events.Event;

import mx.managers.PopUpManager;

public var popPesqVendas:PopPesquisa_Vendas = new PopPesquisa_Vendas();
	
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
			
		sistema_limpar_carrinho(false);
				
		for each (var mi:MovItem in App.single.cache.arrayMovItem)
		{
			if (mi.idMov == movRet.id)
			{
				sistema_coloca_item_importa(mi);
			}	
		}
		
		for each (var cxL:Cx_Lancamento in App.single.cache.arrayCx_Lancamento)
		{
			if (cxL.idTransacao == movRet.idTransacao)
			{
				arraycLancamentosCaixa.addItem(cxL.clone());
			}
		}
	}
	
	private function usuario_fecha_popup_importa_mov():void
	{
		PopUpManager.removePopUp(popup_importar_venda);
	}