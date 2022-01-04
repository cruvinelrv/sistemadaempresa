package janelas.cadastro.Cargo2
{
	import Core.Alerta.AlertaSistema;
	import Core.App;
	import Core.Sessao;
	
	import SDE.Entidade.Cargo;
	import SDE.Entidade.CargoComissionamento;
	import SDE.Entidade.CargoPermissao;
	import SDE.Entidade.SdeConfig;
	
	import com.hillelcoren.utils.StringUtils;
	
	import img.Imagens;
	
	import janelas.cadastro.Cargo2.Form.Cadastro.Cargo_FormCadastro;
	import janelas.cadastro.Cargo2.Form.Pesquisa.Cargo_FormPesquisa;
	
	import mx.collections.ArrayCollection;
	import mx.containers.ViewStack;
	import mx.controls.Alert;
	import mx.core.Container;
	
	import pesquisas.PesquisaCargo;
	
	public class CargoScript
	{
		public function CargoScript()
		{
		}
		
		/**
		 * FUNÇÕES GENÉRICAS
		 * */
		public function mudaTela(vs:ViewStack, container:Container):void
		{
			vs.selectedChild = container;
		}
		
		public function removeComponente(container:Container):void
		{
			container.parent.removeChild(container);
		}
		
		public function ckbComissionadoAcessoSistema_Click(comissionado:Boolean, acessaSistema:Boolean):String
		{
			if (comissionado && acessaSistema)
				return "stateCargoComissionadoAcessaSistema";
	 		else if (comissionado)
	 			return "stateCargoComissionado";
	 		else if (acessaSistema)
	 			return "stateCargoAcessaSistema";
	 		else
	 			return null;
		 }
		
		/**
		 * FUNÇÕES TELA PESQUISA
		 * */
		 public function limpaFormPesquisa(form:Cargo_FormPesquisa):void
		 {
		 	form.dpCargo.removeAll();
		 	form.cpCargo.text = "";
		 }
		 
		public function buscaCargo(strBusca:String):ArrayCollection
		{
			var arrayResultadoBusca:Array = PesquisaCargo.pequisar(strBusca);
			var arrayCollectionRetorno:ArrayCollection = new ArrayCollection();
			
			for each (var cargo:Cargo in arrayResultadoBusca)
			{
				var obj:Object = new Object;
				obj.cod = cargo.id;
				obj.nomeCargo = cargo.nomeCargo;
				obj.comissionado = cargo.comissionado;
				obj.acessaSistema = cargo.acessaSistema;
				arrayCollectionRetorno.addItem(obj);
			}
			
			return arrayCollectionRetorno;
		}
		
		/**
		 * FUNÇÕES TELA CADASTRO
		 * */
		 
		 
		 public function abreNovoCadastro(mdlPai:JnlCadCargo2):void
		 {
		 	limpaFormPesquisa(mdlPai.formPesquisa);
		 	
		 	mdlPai.formCadastro.idCargo = 0;
		 	mdlPai.formCadastro.idCargoComissionamento = 0;
		 	
		 	mdlPai.formCadastro.ehEditando = false;
		 	mdlPai.formCadastro.dpPermissoesSistema = carregaMenusDisponiveisEmpresa();
		 	mudaTela(mdlPai.vs, mdlPai.formCadastro);
		 }
		 
		 public function carregaMenusDisponiveisEmpresa():ArrayCollection
		 {
		 	var arrayCollectionMenusRetorno:ArrayCollection = new ArrayCollection();
		 	var menus:Array  = App.single.cache.arraySdeConfig.sortOn(SdeConfig.campo_variavel);
		 	
		 	for each (var sdeConfig:SdeConfig in menus)
		 	{
		 		if (!StringUtils.beginsWith(sdeConfig.variavel, "Menu"))
		 			continue;
	 			if (sdeConfig.valor == "0")
	 				continue;
 				var obj:Object = new Object();
 				obj.cod = 0;
 				obj.menu = sdeConfig.variavel;
 				obj.permitido = false;
 				arrayCollectionMenusRetorno.addItem(obj);
		 	}
		 	return arrayCollectionMenusRetorno;
		 }
		 
		 /** FUNÇÃO RECEBE TODO O FORM DA TELA DE CADASTRO PARA PODER RECUPERAR
		 * TODAS AS INFORMAÇÕES NELA CONTIDA NO MOMENTO EM QUE O USUÁRIO CLICAR EM SALVAR
		 * */
		 public function salvaCadastroCargo(mdlPai:JnlCadCargo2, mdlCargo:Cargo_FormCadastro):void
		 {
		 	//VALIDAÇÃO DOS DADOS INSERIDOS NO CADASTRO
		 	if (mdlCargo.txtDescricaoCargo.text == "")
		 	{
		 		Alert.show("Informe a descrição do cargo", "Alerta SDE", 4, mdlCargo,
			 		function():void{mdlCargo.txtDescricaoCargo.setFocus();},
			 		Imagens.unica.icn_32_alerta);
		 		return;
		 	}
		 	
		 	var cargo:Cargo = new Cargo();
		 	if (mdlCargo.idCargo != 0)
		 		cargo = App.single.cache.getCargo(mdlCargo.idCargo).clone();
	 		else
	 			cargo.id = mdlCargo.idCargo;
		 	cargo.nomeCargo = mdlCargo.txtDescricaoCargo.text;
		 	
		 	var cargoComissionamento:CargoComissionamento;
		 	if (mdlCargo.ckbComissionado.selected)
		 	{
		 		cargoComissionamento = new CargoComissionamento();
		 		if (mdlCargo.idCargoComissionamento != 0)
		 			cargoComissionamento = App.single.cache.getCargoComissionamento(mdlCargo.idCargoComissionamento).clone();
	 			else
	 				cargoComissionamento.id = mdlCargo.idCargoComissionamento;
		 		cargo.comissionado = true;
		 		
		 		cargoComissionamento.calculaMontanteTotal = mdlCargo.ckbMontanteTotal.selected;
		 		cargoComissionamento.calculaMaoDeObra = mdlCargo.ckbMaoDeObra.selected;
		 		cargoComissionamento.calculaMaoDeObraGeral = mdlCargo.ckbMaoDeObraGeral.selected;
		 		cargoComissionamento.calculaMaoDeObraGarantia = mdlCargo.ckbMaoDeObraGarantia.selected;
		 		cargoComissionamento.calculaMaoDeObraGeralGarantia = mdlCargo.ckbMaoDeObraGeralEmGarantia.selected;
		 		cargoComissionamento.calculaProdutos = mdlCargo.ckbProdutos.selected;
		 		cargoComissionamento.calculaProdutosEmGarantia = mdlCargo.ckbProdutosEmGarantia.selected;
		 		
		 		cargoComissionamento.comissaoMontanteTotal = (mdlCargo.ckbMontanteTotal.selected)?mdlCargo.nsMontanteTotal.value:0;
		 		cargoComissionamento.comissaoMaoDeObra = (mdlCargo.ckbMaoDeObra.selected)?mdlCargo.nsMaoDeObra.value:0;
		 		cargoComissionamento.comissaoMaoDeObraGeral = (mdlCargo.ckbMaoDeObraGeral.selected)?mdlCargo.nsMaoDeObraGeral.value:0;
		 		cargoComissionamento.comissaoMaoDeObraGarantia = (mdlCargo.ckbMaoDeObraGarantia.selected)?mdlCargo.nsMaoDeObraGarantia.value:0;
		 		cargoComissionamento.comissaoMaoDeObraGeralGarantia = (mdlCargo.ckbMaoDeObraGeralEmGarantia.selected)?mdlCargo.nsMaoDeObraGeralEmGarantia.value:0;
		 		cargoComissionamento.comissaoProdutos = (mdlCargo.ckbProdutos.selected)?mdlCargo.nsProdutos.value:0;
		 		cargoComissionamento.comissaoProdutosEmGarantia = (mdlCargo.ckbProdutosEmGarantia.selected)?mdlCargo.nsProdutosEmGarantia.value:0;
		 	}
		 	else
		 		cargo.comissionado = false;
		 	
		 	var arrayCargoPermissoes:Array = [];
		 	if (mdlCargo.ckbAcessaSistema.selected)
		 	{
		 		cargo.acessaSistema = true;
		 		
		 		for each (var obj:Object in mdlCargo.dgPermissoes.dataProvider)
		 		{
		 			var cargoPermissao:CargoPermissao = new CargoPermissao();
		 			if (obj.cod != 0)
		 				cargoPermissao = App.single.cache.getCargoPermissao(obj.cod).clone();
	 				else
		 				cargoPermissao.id = obj.cod;
		 			cargoPermissao.variavel = obj.menu;
		 			cargoPermissao.valor = (obj.permitido)?"1":"0";
		 			arrayCargoPermissoes.push(cargoPermissao);
		 		}
		 	}
		 	else
		 		cargo.acessaSistema = false;
		 	
		 	App.single.n.modificacoes.Cargo_NovoAtualizacao(cargo, cargoComissionamento, arrayCargoPermissoes,
		 		function ():void
		 		{
		 			AlertaSistema.mensagem("Cargo Salvo");
		 			mudaTela(mdlPai.vs, mdlPai.formPesquisa);
		 			limpaFormCadastro(mdlCargo);
		 		}
		 	);
		 }
		 
		 public function limpaFormCadastro(form:Cargo_FormCadastro):void
		 {
		 	if (form.ckbMaoDeObra) form.ckbMaoDeObra.selected = false;
		 	if (form.ckbMaoDeObraGarantia) form.ckbMaoDeObraGarantia.selected = false;
		 	if (form.ckbMaoDeObraGeral) form.ckbMaoDeObraGeral.selected = false;
		 	if (form.ckbMaoDeObraGeralEmGarantia) form.ckbMaoDeObraGeralEmGarantia.selected = false;
		 	if (form.ckbMontanteTotal) form.ckbMontanteTotal.selected = false;
		 	if (form.ckbProdutos) form.ckbProdutos.selected = false;
		 	if (form.ckbProdutosEmGarantia) form.ckbProdutosEmGarantia.selected = false;
		 	
		 	if (form.nsMaoDeObra) form.nsMaoDeObra.value = 0;
		 	if (form.nsMaoDeObraGarantia) form.nsMaoDeObraGarantia.value = 0;
		 	if (form.nsMaoDeObraGeral) form.nsMaoDeObraGeral.value = 0;
		 	if (form.nsMaoDeObraGeralEmGarantia) form.nsMaoDeObraGeralEmGarantia.value = 0;
		 	if (form.nsMontanteTotal) form.nsMontanteTotal.value = 0;
		 	if (form.nsProdutos) form.nsProdutos.value = 0;
		 	if (form.nsProdutosEmGarantia) form.nsProdutosEmGarantia.value = 0; 
		 	
		 	form.txtDescricaoCargo.text = "";
		 	form.ckbComissionado.selected = false;
		 	form.ckbAcessaSistema.selected = false;
		 	
		 	form.idCargo = 0;
		 	form.idCargoComissionamento = 0;
		 	
		 	form.currentState = null;
		 }
		 
		/**
		 * FUNÇÕES TELA EDIÇÃO
		 * */
		 public function carregaCargoSelecionado(obj:Object, mdlPai:JnlCadCargo2):void
		 {
		 	if (!obj)
		 	{
		 		AlertaSistema.mensagem("Selecione um cargo na tabela para prosseguir");
		 		return;
		 	}

			var idCargo:Number = obj.cod;
					 	
		 	mdlPai.formCadastro.ehEditando = true;
		 	var cargo:Cargo = App.single.cache.getCargo(idCargo).clone();
		 	mdlPai.formCadastro.dpPermissoesSistema = new ArrayCollection();
		 	mdlPai.formCadastro.txtDescricaoCargo.text = cargo.nomeCargo;
		 	
		 	mdlPai.formCadastro.idCargo = cargo.id;
		 	
		 	//Se cargo é comissionado carrega dados do comissionamento
		 	if (cargo.comissionado)
		 	{
		 		var cargoComissionamento:CargoComissionamento;
			 	for each (var cc:CargoComissionamento in App.single.cache.arrayCargoComissionamento)
			 	{
			 		if (cc.idCargo != cargo.id || cc.idEmp != Sessao.unica.idEmp)
			 			continue;
		 			cargoComissionamento = cc;
		 			break;
			 	}
			 	
			 	if (!cargoComissionamento)
			 	{
			 		Alert.show("As configurações de comissionamento de cargo não foram encontradas.\nID:" + cargo.id, "Erro SDE", 4, null, null, Imagens.unica.icn_32_deleta);
			 		return;
			 	}
		 		
		 		mdlPai.formCadastro.ckbMaoDeObra.selected = cargoComissionamento.calculaMaoDeObra;
		 		mdlPai.formCadastro.ckbMaoDeObraGarantia.selected = cargoComissionamento.calculaMaoDeObraGarantia;
		 		mdlPai.formCadastro.ckbMaoDeObraGeral.selected = cargoComissionamento.calculaMaoDeObraGeral;
		 		mdlPai.formCadastro.ckbMaoDeObraGeralEmGarantia.selected = cargoComissionamento.calculaMaoDeObraGeralGarantia;
		 		mdlPai.formCadastro.ckbProdutos.selected = cargoComissionamento.calculaProdutos;
		 		mdlPai.formCadastro.ckbProdutosEmGarantia.selected = cargoComissionamento.calculaProdutosEmGarantia;
		 		mdlPai.formCadastro.ckbMontanteTotal.selected = cargoComissionamento.calculaMontanteTotal;
		 		
		 		mdlPai.formCadastro.nsMaoDeObra.value = cargoComissionamento.comissaoMaoDeObra;
		 		mdlPai.formCadastro.nsMaoDeObraGarantia.value = cargoComissionamento.comissaoMaoDeObraGarantia;
		 		mdlPai.formCadastro.nsMaoDeObraGeral.value = cargoComissionamento.comissaoMaoDeObraGeral;
		 		mdlPai.formCadastro.nsMaoDeObraGeralEmGarantia.value = cargoComissionamento.comissaoMaoDeObraGeralGarantia;
		 		mdlPai.formCadastro.nsProdutos.value = cargoComissionamento.comissaoProdutos;
		 		mdlPai.formCadastro.nsProdutosEmGarantia.value = cargoComissionamento.comissaoProdutosEmGarantia;
		 		mdlPai.formCadastro.nsMontanteTotal.value = cargoComissionamento.comissaoMontanteTotal;
		 		
		 		mdlPai.formCadastro.idCargoComissionamento = cargoComissionamento.id;
		 		
		 		mdlPai.formCadastro.ckbComissionado.selected = cargo.comissionado;
		 	}
		 	else
		 		mdlPai.formCadastro.idCargoComissionamento = 0;
		 	
		 	//Se cargo acessa sistema carrega dados do acesso
		 	if (cargo.acessaSistema)
		 	{
		 		var arrayMenus:Array = [];
			 	for each (var xxx:CargoPermissao in App.single.cache.arrayCargoPermissao.sortOn(CargoPermissao.campo_variavel))
			 	{
			 		if (xxx.idCargo == cargo.id && xxx.idEmp == Sessao.unica.idEmp)
			 			arrayMenus.push(xxx);
			 	}
			 	
			 	var menus:ArrayCollection = new ArrayCollection();
			 	var menuExiste:Boolean;
			 	var obj:Object;
			 	for each (var sdeConfig:SdeConfig in App.single.cache.arraySdeConfig.sortOn(SdeConfig.campo_variavel))
			 	{
			 		menuExiste = false;
			 		for each (var yyy:CargoPermissao in arrayMenus)
			 		{
			 			if (yyy.variavel != sdeConfig.variavel || sdeConfig.valor != "1")
			 				continue;
		 				obj = new Object();
		 				obj.cod = yyy.id;
		 				obj.menu = yyy.variavel;
 						obj.permitido = (yyy.valor=="0")?false:true;
 						menus.addItem(obj);
 						menuExiste = true;
 						break;
			 		}
			 		
			 		if (!menuExiste)
			 		{
			 			if (!StringUtils.beginsWith(sdeConfig.variavel, "Menu"))
				 			continue;
			 			if (sdeConfig.valor == "0")
			 				continue;
			 			obj = new Object();
			 			obj.cod = 0;
			 			obj.menu = sdeConfig.variavel;
			 			obj.permitido = false;
			 			menus.addItem(obj);
			 		}
			 	}
			 	
			 	mdlPai.formCadastro.dpPermissoesSistema = menus;
		 		mdlPai.formCadastro.ckbAcessaSistema.selected = cargo.acessaSistema;
		 	}
		 	else
		 		mdlPai.formCadastro.dpPermissoesSistema = carregaMenusDisponiveisEmpresa();
		 	
		 	mdlPai.formCadastro.currentState = ckbComissionadoAcessoSistema_Click(cargo.comissionado, cargo.acessaSistema);
		 	
		 	mudaTela(mdlPai.vs, mdlPai.formCadastro);
		 	limpaFormPesquisa(mdlPai.formPesquisa);
		 }
	}
}