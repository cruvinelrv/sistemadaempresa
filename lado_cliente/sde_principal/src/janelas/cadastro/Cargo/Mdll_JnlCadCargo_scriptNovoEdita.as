
import Core.Alerta.AlertaSistema;
import Core.App;
import Core.Sessao;
import Core.Utils.MyArrayUtils;

import SDE.Constantes.Variaveis_SdeConfig;
import SDE.Entidade.Cargo;
import SDE.Entidade.CargoPermissoes;
import SDE.Entidade.SdeConfig;

import com.hillelcoren.utils.StringUtils;

import mx.collections.ArrayCollection;
import mx.controls.dataGridClasses.DataGridColumn;
import mx.core.Application;
import mx.managers.PopUpManager;
	
	
	private var cargo_selecionado:Cargo = null;
	
	[Bindable] private var parametros:Array;
	[Bindable] private var parametros_filtados:ArrayCollection = new ArrayCollection();
	
	private function fn_permissao(cp:CargoPermissoes, dgc:DataGridColumn):String{return (cp.valor == "1") ? "Sim" : "Não";}
	
	private function populaCargo(cargo:Cargo):void{
		this.cargo_selecionado = new Cargo();
		this.cargo_selecionado = cargo;
		
		txtDescCargo.text = this.cargo_selecionado.nomeCargo;
		chkbMontanteTotal.selected = this.cargo_selecionado.calculaMontanteTotal;
		chkbMaoDeObra.selected = this.cargo_selecionado.calculaMaoDeObra;
		chkbMaoDeObraGarantia.selected = this.cargo_selecionado.calculaMaoDeObraGarantia;
		chkbProdutosEmGarantia.selected = this.cargo_selecionado.calculaProdutosEmGarantia;
		chkbProdutos.selected = this.cargo_selecionado.calculaProdutos;
		nsMontanteTotal.value = this.cargo_selecionado.comissaoMontanteTotal;
		nsMaoDeObra.value = this.cargo_selecionado.comissaoMaoDeObra;
		nsMaoDeObraGarantia.value = this.cargo_selecionado.comissaoMaoDeObraGarantia;
		nsProdutosEmGarantia.value = this.cargo_selecionado.comissaoProdutosEmGarantia;
		nsProdutos.value = this.cargo_selecionado.comissaoProdutos;
		
		verifica_novas_telas();
		
		var configuracoes:Array = App.single.cache.arraySdeConfig.sortOn(SdeConfig.campo_variavel);
		parametros = [];
		
		for each (var sdeConfig:SdeConfig in configuracoes)
		{
			if (!StringUtils.beginsWith(sdeConfig.variavel, "Menu"))
				continue;
			if (sdeConfig.valor == "0")
				continue;
			
			for each (var cargoPermissoes:CargoPermissoes in App.single.cache.arrayCargoPermissao)
			{	
				if (cargoPermissoes.idCargo != cargo.id)
					continue;
				
				if (sdeConfig.variavel != cargoPermissoes.variavel)
					continue;
				parametros.push(cargoPermissoes);
				break;
			}
		}
		
		parametros_filtados.removeAll();
		parametros_filtados.addAll( new ArrayCollection(parametros) );
	}
	
	private function verifica_novas_telas():void
	{
		//var camposTodos_String:Array = Variaveis_SdeConfig.getCampos();
		var camposMenus:Array = MyArrayUtils.cloneArrayEntidades(App.single.cache.arrayCargoPermissao);
		var camposNovos:Array = [];
		
		for each (var s:String in Variaveis_SdeConfig.getCampos())
		{
			if(!StringUtils.beginsWith(s, "Menu"))
				continue;
			
			//if (!verifica_acesso_empresa(s))
				//continue;
			
			var existe:Boolean = false;
			var c:CargoPermissoes;
			for each (c in camposMenus)
			{
				if (c.variavel == s && c.idEmp == App.single.idEmp && c.idCargo == cargo_selecionado.id)
					existe=true;
			}
			if(!existe)
			{
				c = new CargoPermissoes();
				c.idEmp = App.single.idEmp;
				c.idCargo = cargo_selecionado.id;
				c.variavel = s;
				c.valor = "0";
				c.prioridade = 3;//1==corp,2==emp,3==cargo,10==usuario-logado
				camposNovos.push(c);
				AlertaSistema.mensagem("criando configuração: "+s, true);
			}
		}
		
		if (camposNovos.length > 0)
		{
			Application.application.sessao.nuvens.modificacoes.CargoPermissoes_NovosAtualiza(
				camposNovos,
				function(r:Array):void
				{
					AlertaSistema.mensagem(camposNovos.length+ " novas configurações foram criadas");
				}
			);
		}
	}
	
	private function verifica_acesso_empresa(menu:String):Boolean
	{
		var retorno:Boolean = false;
		for each (var sdeConfig:SdeConfig in App.single.cache.arraySdeConfig)
		{
			if (menu != sdeConfig.variavel)
				continue;
			if (sdeConfig.valor == "1")
				retorno = true;
			break;
		}
		return retorno;
	}
	
	private function btnNovoCargo_Click():void
	{
		PopUpManager.addPopUp(popupNovoCargo, Application.application.gerenteJanelas, true);
		PopUpManager.centerPopUp(popupNovoCargo);
		popupNovoCargo_txtDescCargo.setFocus();
	}
	
	private function btnSalvarNovoCargo_Click():void
	{
		if (popupNovoCargo_txtDescCargo.text == "")
		{
			AlertaSistema.mensagem("Digite a Descrição do Cargo.");
			return;
		}
		
		var xxx:Cargo = new Cargo();
		xxx.nomeCargo = popupNovoCargo_txtDescCargo.text;
		
		Sessao.unica.nuvens.modificacoes.Cargo_NovoAltera(xxx,
			function (retorno:Number):void
			{
				cargo_selecionado = new Cargo();
				cargo_selecionado.id = retorno;
				verifica_novas_telas();
				popupNovoCargo.parent.removeChild(popupNovoCargo);
				popupNovoCargo_txtDescCargo.text = "";
				dpCargo = App.single.cache.arraycCargo;
			}
		);
	}
	
	private function btnSalvar_Click():void
	{
		this.cargo_selecionado.nomeCargo = txtDescCargo.text;	
		
		this.cargo_selecionado.nomeCargo = txtDescCargo.text;
		this.cargo_selecionado.calculaMontanteTotal = chkbMontanteTotal.selected;
		this.cargo_selecionado.calculaMaoDeObra = chkbMaoDeObra.selected;
		this.cargo_selecionado.calculaMaoDeObraGeral = chkbMaoDeObraGeral.selected;
		this.cargo_selecionado.calculaMaoDeObraGarantia = chkbMaoDeObraGarantia.selected;
		this.cargo_selecionado.calculaMaoDeObraGeralGarantia = chkbMaoDeObraGeralEmGarantia.selected;
		this.cargo_selecionado.calculaProdutosEmGarantia = chkbProdutosEmGarantia.selected;
		this.cargo_selecionado.calculaProdutos = chkbProdutos.selected;
		this.cargo_selecionado.comissaoMontanteTotal = nsMontanteTotal.value;
		this.cargo_selecionado.comissaoMaoDeObra = nsMaoDeObra.value;
		this.cargo_selecionado.comissaoMaoDeObraGeral = nsMaoDeObraGeral.value;
		this.cargo_selecionado.comissaoMaoDeObraGarantia = nsMaoDeObraGarantia.value;
		this.cargo_selecionado.comissaoMaoDeObraGeralGarantia = nsMaoDeObraGeralEmGarantia.value;
		this.cargo_selecionado.comissaoProdutosEmGarantia = nsProdutosEmGarantia.value;
		this.cargo_selecionado.comissaoProdutos = nsProdutos.value;
		
		var permissoes:ArrayCollection = dgPermissoes.dataProvider as ArrayCollection;
		
		Sessao.unica.nuvens.modificacoes.Cargo_NovoAltera(cargo_selecionado,
			function ():void{
				Sessao.unica.nuvens.modificacoes.CargoPermissoes_NovosAtualiza(permissoes.source);
				AlertaSistema.mensagem("Cargo salvo", true);
				mudaTela(telaPesquisa);
				limpaTelaCadastro();
				dpCargo = App.single.cache.arraycCargo;
			}
		);
	}
	
	private function limpaTelaCadastro():void{
		txtDescCargo.text = "";
		chkbMontanteTotal.selected = false;
		chkbMaoDeObra.selected = false;
		chkbMaoDeObraGeral.selected = false;
		chkbMaoDeObraGarantia.selected = false;
		chkbMaoDeObraGeralEmGarantia.selected = false;
		chkbProdutosEmGarantia.selected = false;
		chkbProdutos.selected = false;
		nsMontanteTotal.value = 0;
		nsMaoDeObra.value = 0;
		nsMaoDeObraGeral.value = 0;
		nsMaoDeObraGarantia.value = 0;
		nsMaoDeObraGeralEmGarantia.value = 0;
		nsProdutosEmGarantia.value = 0;
		nsProdutos.value = 0;
		cargo_selecionado = null;
		cpCargo_KeyDown();
	}