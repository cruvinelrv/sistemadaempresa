
import Core.Alerta.AlertaSistema;
import Core.App;
import Core.Utils.Formatadores;

import SDE.Entidade.Cad_Marca;
import SDE.Entidade.Cad_Secao;
import SDE.Entidade.Cliente;
import SDE.Entidade.Item;
import SDE.Entidade.ItemEmpPreco;
import SDE.Enumerador.EItemTipo;

import img.Imagens;

import mx.collections.ArrayCollection;
import mx.collections.Sort;
import mx.collections.SortField;
import mx.controls.Alert;
import mx.core.Application;
import mx.managers.PopUpManager;

	
	private var tipoIdentTextoCad:Array;
	
	private var precoCusto:Number = 0;
	private var margemLucro:Number = 0;
	private var precoVenda:Number = 0;
	private var precoCompra:Number = 0;
	
	private function iniciaNovo():void{
		tipoIdentTextoCad = [];
		tipoIdentTextoCad['grade'] = 'Grade';
		tipoIdentTextoCad['identificador'] = 'Identificador';
		tipoIdentTextoCad['lote'] = 'Lote';
	}
	
	private function createNovo():void{
		popupConfiguraPreco.parent.removeChild(popupConfiguraPreco);
		cmbCadSecao.dataProvider = secoes;
		cmbCadMarca.dataProvider = marcas;
		txtCadCodUnico.text = "GERAR";
	}
	
	private function fn_ComboTipoIdentCad_Label(tipoIdent:String):String{
		return tipoIdentTextoCad[tipoIdent];
	}
	
	private function btnCancela_Click():void{
		mudaTela(telaConsulta);
		limpaCadNovo();
	}
	
	private function btnSalva_Click():void{
		var msg:String = "";
		if (txtCadCodUnico.text == '')
			msg += "Digite uma REFERENCIA\n";
		if (txtCadDescricao.text == '')
			msg += "Digite uma DESCRIÇÃO\n";		
		if (precoCusto == 0)
			msg += "Informe o PREÇO DE CUSTO\n";
		/* if (margemLucro == 0)
			msg += "Informe a MARGEM DE LUCRO\n";
		if (precoVenda == 0)
			msg += "Informe o PREÇO DE VENDA"; */
		if (msg != ""){
			AlertaSistema.mensagem(msg);
			return;
		}
		
		var itemNovo:Item = new Item();
		itemNovo.tipo = EItemTipo.produto;
		itemNovo.nome = txtCadDescricao.text;
		itemNovo.rfUnica = txtCadCodUnico.text;
		itemNovo.rfAuxiliar = txtCadCodAuxiliar.text;

		itemNovo.idSecao = cmbCadSecao.selectedItem.id;
		itemNovo.idMarca = cmbCadMarca.selectedItem.id;
		itemNovo.secao = cmbCadSecao.selectedItem.secao;
		itemNovo.grupo = cmbCadSecao.selectedItem.grupo;
		itemNovo.subgrupo = cmbCadSecao.selectedItem.subgrupo;
		itemNovo.marca = cmbCadMarca.selectedItem.marca;
		itemNovo.modelo = cmbCadMarca.selectedItem.modelo;
		itemNovo.unidMed = cmbCadUn.selectedLabel;
		itemNovo.tipoIdent = cmbCadTipoIdent.selectedItem.toString();
		
		var iepNovo:ItemEmpPreco = new ItemEmpPreco();
		iepNovo.idEmp = App.single.ss.idEmp;
		iepNovo.custo = precoCusto;
		iepNovo.compra = precoCompra;
		iepNovo.margemLucro = margemLucro;
		iepNovo.venda = precoVenda;
		iepNovo.pctComissao = nsCadComissao.value;
		iepNovo.descontoMaximo = nsCadDescMaximo.value;
		
		App.single.n.modificacoes.ItemNovo(itemNovo, iepNovo,
			function (idItem:Number):void{
				mudaTela(telaEdita);
				limpaCadNovo();
				importaItemSelecionado(idItem);
			}
		);
	}
	
	private function btnConfigurarPreco_Click():void{
		PopUpManager.addPopUp(popupConfiguraPreco, Application.application.gerenteJanelas, true);
		PopUpManager.centerPopUp(popupConfiguraPreco);
		nsCadPrecoCusto.value = precoCusto;
	}
	
	private function calculaPrecoVenda():void{
		if (nsCadConfMargemLucro.value == 0)
			return;
		nsCadConfProPrecoVenda.value = ((nsCadPrecoCusto.value / 100) * nsCadConfMargemLucro.value) + nsCadPrecoCusto.value;
		btn_ConfirmaMargemLucro.setFocus();
	}
	
	private function calculaMargemLucro():void{
		var diferenca:Number = nsCadConfPrecoVenda.value - nsCadPrecoCusto.value;
		diferenca = diferenca * 100;
		nsCadConfProMargemLucro.value = diferenca / nsCadPrecoCusto.value;
		btn_ConfirmaPrecoVenda.setFocus();
	}
	
	private function btnConfirmaMargemLucro_Click():void
	{
		//Fazer esta validação, mas permitir que o usuário salve com qualquer valor que ele queira
		/* if (nsCadPrecoCusto.value == 0)
		{
			Alert.show("Preço de custo deve ser maior que 0 (zero)", "Mensagem SDE", 4, null, null, Imagens.unica.icn_32_alerta);
			return;
		}
		if (nsCadConfProPrecoVenda.value <= nsCadPrecoCusto.value)
		{
			Alert.show("O preço de venda deve ser maior que o preço de custo", "Mensagem SDE", 4, null, null, Imagens.unica.icn_32_alerta);
			return;
		} */
		
		precoCompra = nsCadPrecoCompra.value;
		precoCusto = nsCadPrecoCusto.value;
		precoVenda = nsCadConfProPrecoVenda.value;
		margemLucro = nsCadConfMargemLucro.value;
		
		txtCadPrecoCompra.text = Formatadores.unica.formataValor(precoCompra, true);
		txtCadPrecoCusto.text = Formatadores.unica.formataValor(precoCusto, true);
		txtCadPrecoVenda.text = Formatadores.unica.formataValor(precoVenda, true);
		
		PopUpManager.removePopUp(popupConfiguraPreco);
		
		limpaConfPreco();
	}
	
	private function btnConfirmaPrecoVenda_Click():void
	{
		//Fazer esta validação, mas permitir que o usuário salve com qualquer valor que ele queira
		/* if (nsCadPrecoCusto.value == 0)
		{
			Alert.show("Preço de custo deve ser maior que 0 (zero)", "Mensagem SDE", 4, null, null, Imagens.unica.icn_32_alerta);
			return;
		}
		if (nsCadConfPrecoVenda.value <= nsCadPrecoCusto.value)
		{
			Alert.show("O preço de venda deve ser maior que o preço de custo", "Mensagem SDE", 4, null, null, Imagens.unica.icn_32_alerta);
			return;
		} */
		
		precoCompra = nsCadPrecoCompra.value;
		precoCusto = nsCadPrecoCusto.value;
		precoVenda = nsCadConfPrecoVenda.value;
		margemLucro = nsCadConfProMargemLucro.value;
		
		txtCadPrecoCompra.text = Formatadores.unica.formataValor(precoCompra, true);
		txtCadPrecoCusto.text = Formatadores.unica.formataValor(precoCusto, true);
		txtCadPrecoVenda.text = Formatadores.unica.formataValor(precoVenda, true);
		
		PopUpManager.removePopUp(popupConfiguraPreco);
		
		limpaConfPreco();
	}
	
	private function geraCodigoUnico():String
	{
		var fornecedor:Cliente;
		if (cpFornecedor.selectedItem)
			fornecedor = cpFornecedor.selectedItem as Cliente;
		else
		{
			Alert.show("Selecione um fornecedor", "Alerta SDE", 4, null, null, Imagens.unica.icn_32_alerta);
			return "GERAR";
		}
		var secao:Cad_Secao = cmbCadSecao.selectedItem as Cad_Secao;
		var itens:ArrayCollection = App.single.cache.arraycItem;
		
		var sort:Sort = new Sort();
		sort.fields = [new SortField("id")];
		itens.sort = sort;
		itens.refresh();
		var item:Item = itens.getItemAt(itens.length - 1) as Item;
		
		return secao.id.toString()+fornecedor.id.toString()+(item.id+1).toString();
	}
	
	private function limpaConfPreco():void
	{
		nsCadPrecoCusto.value = 0;
		nsCadConfMargemLucro.value = 0;
		nsCadConfProPrecoVenda.value = 0;
		nsCadConfPrecoVenda.value = 0;
		nsCadConfProMargemLucro.value = 0;
	}
	
	private function limpaCadNovo():void{
		txtCadDescricao.text = "";
		txtCadCodUnico.text = "GERAR";
		txtCadCodAuxiliar.text = "";

		cmbCadSecao.selectedIndex = 0;
		cmbCadUn.selectedIndex = 0;
		cmbCadTipoIdent.selectedIndex = 0;
		nsCadPrecoCusto.value = 0;
		nsCadPrecoCompra.value = 0;
		nsCadComissao.value = 0;
		nsCadDescMaximo.value = 0;
		txtCadPrecoCusto.text = "";
		txtCadPrecoCompra.text = "";
		txtCadPrecoVenda.text = "";
		precoCusto = 0;
		margemLucro = 0;
		precoVenda = 0;
	}