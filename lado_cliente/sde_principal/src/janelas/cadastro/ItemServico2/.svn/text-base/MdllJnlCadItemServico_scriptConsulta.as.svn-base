
import Core.Sessao;

import SDE.Entidade.Item;
import SDE.Enumerador.EItemTipo;

import img.Imagens;

import mx.controls.Alert;

import pesquisas.PesquisaItem;
import pesquisas.PesquisaItemEmpPreco;

	private function PesquisarServico():void
	{
		var arrayResultadoPesquisaItem:Array = PesquisaItem.pesquisar(cpServicoPesquisa.text, false, true);
		
		dpServico.removeAll();
		for each(var item:Item in arrayResultadoPesquisaItem)
		{
			var obj:Object = new Object();
			obj.item = item.clone();
			obj.cod = item.id;
			obj.nome = item.nome;
			obj.codUnico = item.rfUnica;
			obj.unMed = item.unidMed;
			obj.valor = PesquisaItemEmpPreco.pegar(Sessao.unica.idEmp, item.id).venda;
			dpServico.addItem(obj);
		}
	}
	
	private function LimpaConsulta():void
	{
		dpServico.removeAll();
		cpServicoPesquisa.text = "";
		cpServicoPesquisa.setFocus();
	}
	
	private function ServicoSelecionado():void
	{
		if (!dgServicoPesquisa.selectedItem)
		{
			Alert.show("Selecione um item para prosseguir.", "Mensagem SDE", 4, null, null, Imagens.unica.icn_32_alerta);
			return;
		}
		
		ImportaItemEditando(dgServicoPesquisa.selectedItem.item);
		MudaTela(telaEdicao);
	}