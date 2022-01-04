
import Core.App;
import Core.Sessao;

import SDE.Entidade.Item;
import SDE.Entidade.ItemEmpPreco;
import SDE.Enumerador.EItemTipo;

import img.Imagens;

import mx.controls.Alert;

	private function ShowNovo():void
	{
		txtCodUnicoCad.text = "GERAR";
		cmbSecaoCad.dataProvider = secoes;
	}
	
	private function CancelaNovo():void
	{
		LimpaConsulta();
		MudaTela(telaConsulta);
	}
	
	private function SalvaNovo():void
	{
		var msg:String = "";
		if (txtCodUnicoCad.text == "")
			msg += "Digite o código único\n";
		if (txtDescricaoCad.text == "")
			msg += "Digite a descrição do serviço\n";
		if (nsPrecoServicoCad.value == 0)
			msg  += "Digite o preço do serviço";
		if (msg != "")
		{
			Alert.show(msg, "Mensagem SDE", 4, null, null, Imagens.unica.icn_32_deleta);
			return;
		}
		
		var itemServicoNovo:Item = new Item();
		itemServicoNovo.tipo = EItemTipo.servico;
		itemServicoNovo.nome = txtDescricaoCad.text;
		itemServicoNovo.rfUnica = txtCodUnicoCad.text;
		itemServicoNovo.unidMed = cmbUnidMedCad.selectedLabel;
		
		itemServicoNovo.idSecao = cmbSecaoCad.selectedItem.id;
		itemServicoNovo.secao = cmbSecaoCad.selectedItem.secao;
		itemServicoNovo.grupo = cmbSecaoCad.selectedItem.grupo;
		itemServicoNovo.subgrupo = cmbSecaoCad.selectedItem.subgrupo;
		
		var itemEmpPrecoServicoNovo:ItemEmpPreco = new ItemEmpPreco();
		itemEmpPrecoServicoNovo.idEmp = Sessao.unica.idEmp;
		itemEmpPrecoServicoNovo.custo = nsPrecoCustoCad.value;
		itemEmpPrecoServicoNovo.compra = nsPrecoCustoCad.value;
		itemEmpPrecoServicoNovo.venda = nsPrecoServicoCad.value;
		itemEmpPrecoServicoNovo.descontoMaximo = nsDescontoMaximoCad.value;
		
		App.single.mod.ItemNovo(itemServicoNovo, itemEmpPrecoServicoNovo,
			function (idItem:Number):void
			{
				LimpaCampos();
				ImportaItemEditando(App.single.cache.getItem(idItem).clone());
				MudaTela(telaEdicao);
			}
		);
	}
	
	private function LimpaCampos():void
	{
		txtDescricaoCad.text = "";
		txtCodUnicoCad.text = "GERAR";
		cmbUnidMedCad.selectedIndex = 0;
		cmbSecaoCad.selectedIndex = 0;
		nsPrecoCustoCad.value = 0;
		nsPrecoServicoCad.value = 0;
		nsDescontoMaximoCad.value = 0;
	}