import Core.App;
import Core.Sessao;
import Core.Utils.Formatadores;

import SDE.Entidade.Cliente;
import SDE.Entidade.Item;
import SDE.Entidade.ItemEmpPreco;
import SDE.Entidade.OrdemServico;
import SDE.Entidade.OrdemServico_Item;
import SDE.Enumerador.EOrdemServicoStatus;
import SDE.Enumerador.EPesTipo;

import flash.events.Event;

import img.Imagens;

import mx.controls.Alert;
import mx.core.Application;
import mx.core.Container;
import mx.core.IFlexDisplayObject;
import mx.managers.PopUpManager;

import pesquisas.PesquisaCliente;
import pesquisas.PesquisaItem;
import pesquisas.PesquisaItemEmpPreco;
import pesquisas.PesquisaListaCasamento;

	private function mudaTela(container:Container):void
	{
		vs.selectedChild = container; 
	}
	
	private function mostraPopup(iFlexDisplayObject:IFlexDisplayObject):void
	{
		PopUpManager.addPopUp(iFlexDisplayObject, Application.application.gerenteJanelas, true);
		PopUpManager.centerPopUp(iFlexDisplayObject);
	}
	
	private function removePopup(iFlexDisplayObject:IFlexDisplayObject):void
	{
		PopUpManager.removePopUp(iFlexDisplayObject);
	}
	
	private function pesquisaLista(searchStr:String):void
	{
		var resultadoPesquisa:Array = PesquisaListaCasamento.pesquisar(searchStr);
		arraycLista.removeAll();
		for each (var lista:OrdemServico in resultadoPesquisa)
		{
			if (lista.status == EOrdemServicoStatus.em_andamento)
				arraycLista.addItem(lista);
		}
		arraycLista.sort = sortLista;
		arraycLista.refresh();
	}
	
	private function selecionaLista(lista:OrdemServico):void
	{
		if (!lista)
		{
			Alert.show("Selecione uma lista para visualisar seus itens.", "Mensagem SDE", 4, null, null, Imagens.unica.icn_32_alerta);
			return;
		}
		
		cpListaCasamento.text = "";
		arraycLista.removeAll();
		
		listaSelecionada = lista.clone();
		clienteSelecionado = PesquisaCliente.pegar(listaSelecionada.idCliente);
		
		mudaTela(telaLista);
		preencheListaItens();
		cpItem.txtPesquisa.setFocus();
	}
	
	private function pesquisaCliente(searchStr:String):void
	{
		var resultadoPesquisa:Array = PesquisaCliente.pesquisar(searchStr);
		arraycCliente.removeAll();
		for each (var cliente:Cliente in resultadoPesquisa)
			if (cliente.tipo == EPesTipo.Fisica && cliente.cpf_cnpj != "00000000000")
				arraycCliente.addItem(cliente);
		arraycCliente.sort = sortCliente;
		arraycCliente.refresh();
	}
	
	private function selecionaClienteCriaLista(cliente:Cliente, dataEvento:Date):void
	{
		if (!cliente)
		{
			Alert.show("Selecione um cliente para criar uma lista de casamento.", "Mensagem SDE", 4, null, null, Imagens.unica.icn_32_alerta);
			return;
		}
		
		App.single.n.modificacoes.ListaCasamento_Abre(cliente.clone().id, Formatadores.unica.formataData(dataEvento),
			function(idLista:Number):void
			{
				listaSelecionada = PesquisaListaCasamento.pegar(idLista);
				clienteSelecionado = PesquisaCliente.pegar(listaSelecionada.idCliente);
			}
		);
		
		mudaTela(telaLista);
		cpItem.txtPesquisa.setFocus();
		removePopup(popupNovaLista);
	}
	
	//******************************************************************************************************************
	
	private function preencheListaItens():void
	{
		arraycListaItem.removeAll();
		for each (var listaItem:OrdemServico_Item in App.single.cache.arrayOrdemServico_Item)
			if (listaItem.idOrdemServico == listaSelecionada.id)
				arraycListaItem.addItem(listaItem.clone());
		arraycListaItem.sort = sortListaItem;
		arraycListaItem.refresh();
	}
	
	private function pesquisaItem(searchStr:String):void
	{
		var resultadoPesquisa:Array = PesquisaItem.pesquisar(searchStr);
		
		if (resultadoPesquisa.length == 1)
		{
			incluiItem(resultadoPesquisa[0] as Item);
		}
		else
		{
			arraycItem.removeAll();
			for each (var it:Item in resultadoPesquisa)
			{
				var iep:ItemEmpPreco = PesquisaItemEmpPreco.pegar(Sessao.unica.idEmp, it.id);
				var obj:Object = new Object();
				obj.nome = it.nome;
				obj.venda = iep.venda;
				obj.item = it;
				arraycItem.addItem(obj);
			}
			arraycItem.sort = sortItem;
			arraycItem.refresh();
			cpItemPopup.text = cpItem.text;
			cpItemPopup.txtPesquisa.setFocus();
			mostraPopup(popupPesquisaItem);
		}
	}
	
	private function pesquisaItemPopup(searchStr:String):void
	{
		var resultadoPesquisa:Array = PesquisaItem.pesquisar(searchStr);
		
		arraycItem.removeAll();
		for each (var it:Item in resultadoPesquisa)
		{
			var iep:ItemEmpPreco = PesquisaItemEmpPreco.pegar(Sessao.unica.idEmp, it.id);
			var obj:Object = new Object();
			obj.nome = it.nome;
			obj.venda = iep.venda;
			obj.item = it;
			arraycItem.addItem(obj);
		}
		arraycItem.sort = sortItem;
		arraycItem.refresh();
	}
	
	private function itemPopupSelecionado(item:Item):void
	{
		if (!item)
		{
			Alert.show("Selecione um item na grid para prosseguir", "Mensagem SDE", 4, null, null, Imagens.unica.icn_32_alerta);
			return;
		}
		
		incluiItem(item);
		cpItem.text = "";
		cpItemPopup.text = "";
		cpItem.txtPesquisa.setFocus();
	}
	
	private function verificaItemLista(idItem:Number):Boolean
	{
		var retorno:Boolean = false;
		for each (var listaItem:OrdemServico_Item in arraycListaItem)
		{
			if (listaItem.idItem != idItem)
				continue;
			retorno = true;
			break;
		}
		return retorno;
	}
	
	private function incluiItem(item:Item):void
	{
		if (verificaItemLista(item.id))
		{
			Alert.show("Este item já foi incluído na lista", "Mensagem SDE", 4, null, null, Imagens.unica.icn_32_info);
			return;
		}
		
		var itemEmpPreco:ItemEmpPreco = PesquisaItemEmpPreco.pegar(Sessao.unica.idEmp, item.id);
		var listaItem:OrdemServico_Item = new OrdemServico_Item();
		listaItem.idOrdemServico = listaSelecionada.id;
		listaItem.idItem = item.id;
		listaItem.idIEP = itemEmpPreco.id;
		listaItem.item_nome = item.nome;
		listaItem.vlrUnitVendaInicial = itemEmpPreco.venda;
		
		App.single.n.modificacoes.ListaCasamento_LancaItem(listaItem,
			function():void
			{
				preencheListaItens();
				removePopup(popupPesquisaItem);
				cpItem.text = "";
				cpItem.txtPesquisa.setFocus();
			}
		);
	}
	
	private function removeItem(event:Event):void
	{
		var id:Number = event.target.data.id;
		App.single.n.modificacoes.ListaCasamento_RemoveItem(id, preencheListaItens);
	}
	
	private function imprimeLista():void
	{
		if (Sessao.unica.idCorp == 76)
		{
			App.single.n.modificacoes.ListaCasamento_Imprime(listaSelecionada.id,
				function():void
				{
					Application.application.gerenteConexaoDesktop.baixaListaCasamento(Sessao.unica.idCorp);
				}
			);
		}
		else
		{
			Alert.show("Impressão não suportada para esta empresa.", "Mensagem SDE", 4, null, null, Imagens.unica.icn_32_info);
		}
	}