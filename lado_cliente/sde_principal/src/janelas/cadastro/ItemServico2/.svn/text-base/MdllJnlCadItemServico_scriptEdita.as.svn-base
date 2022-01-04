import Core.App;
import Core.Sessao;
import Core.Utils.Funcoes;
import Core.Utils.MyArrayUtils;

import SDE.Entidade.Item;

import img.Imagens;

import mx.controls.Alert;

import pesquisas.PesquisaItemEmpAliquota;
import pesquisas.PesquisaItemEmpPreco;
	
	private function ImportaItemEditando(item:Item):void
	{
		itemEditando = item;
		itemEmpPrecoEditando = PesquisaItemEmpPreco.pegar(Sessao.unica.idEmp, itemEditando.id);
		itemEmpAliquotasEditando = PesquisaItemEmpAliquota.pegar(Sessao.unica.idEmp, itemEditando.id);
		
		cmbSecaoEdi.dataProvider = secoes;
		
		PreencheSecao();
		DoBinding();
		
		vsServico.selectedIndex = 0;
	}
	
	private function DoBinding():void
	{
		Funcoes.myBind(cmbUnidMedEdi, "selectedItem", itemEditando, "unidMed");
		Funcoes.myBind(txtDescricaoEdi, "text", itemEditando, "nome");
		Funcoes.myBind(txtCodUnicoEdi, "text", itemEditando, "rfUnica");
		Funcoes.myBind(ckbDesusoEdi, "selected", itemEditando, "desuso");
		Funcoes.myBind(txtObservacaoAplicacaoEdi, "text", itemEditando, "complAplic");
		
		Funcoes.myBind(nsPrecoCustoEdi, "value", itemEmpPrecoEditando, "custo");
		Funcoes.myBind(nsPrecoServicoEdi, "value", itemEmpPrecoEditando, "venda");
		Funcoes.myBind(nsDescontoMaximoEdi, "value", itemEmpPrecoEditando, "descontoMaximo");
		
		Funcoes.myBind(nsISSQNEdi, "value", itemEmpAliquotasEditando, "issqn");
		Funcoes.myBind(nsINSSEdi, "value", itemEmpAliquotasEditando, "inss");
	}
	
	private function CancelaEdicao():void
	{
		LimpaConsulta();
		MudaTela(telaConsulta);
	}
	
	private function SalvaEdicao(continuaEditando:Boolean):void
	{
		var msg:String = null;
		if (itemEditando.nome.length < 3)
			msg += "Digite uma DESCRIÇÃO\n";
		if (itemEditando.rfUnica.length < 2)
			msg += "Digite uma REFERÊNCIA\n";
		if (itemEmpPrecoEditando != null)
		{
			if (itemEmpPrecoEditando.venda == 0)
				msg += "digite o PREÇO DO SERVIÇO\n";
			if (itemEmpPrecoEditando.custo >= itemEmpPrecoEditando.venda)
				msg = "CUSTO deve ser menor que PREÇO DO SERVIÇO";
		}			
		
		if (msg != null)
		{
			Alert.show(msg, "Mensagem SDE", 4, null, null, Imagens.unica.icn_32_deleta);
			return;
		}
		
		AtualizaSecao();
		App.single.n.modificacoes.ItemAtualiza(itemEditando, itemEmpPrecoEditando, itemEmpAliquotasEditando,
			function():void
			{
				if (!continuaEditando)
				{
					LimpaConsulta();
					MudaTela(telaConsulta);
				}
			}
		);
	}
	
	private function PreencheSecao():void
	{
		if (itemEditando.idSecao == 0)
			return;
		
		var dictSecoes:Array = MyArrayUtils.asDictionary(secoes);
		cmbSecaoEdi.selectedItem = dictSecoes[itemEditando.idSecao];
	}
	
	private function AtualizaSecao():void
	{
		if (itemEditando == null)
			return;
			
		itemEditando.idSecao = cmbSecaoEdi.selectedItem.id;
		itemEditando.secao = cmbSecaoEdi.selectedItem.secao;
		itemEditando.grupo = cmbSecaoEdi.selectedItem.grupo;
		itemEditando.subgrupo = cmbSecaoEdi.selectedItem.subgrupo;
	}