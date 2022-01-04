package janelas.cadastro.Funcionario
{
	import Core.App;
	import Core.Sessao;
	
	import SDE.Entidade.Cargo;
	import SDE.Entidade.CargoComissionamento;
	import SDE.Entidade.CargoPermissao;
	import SDE.Entidade.Cliente;
	import SDE.Entidade.ClienteFuncionarioComissionamento;
	import SDE.Entidade.ClienteFuncionarioPermissao;
	import SDE.Entidade.ClienteFuncionarioUsuario;
	import SDE.Entidade.SdeConfig;
	
	import com.hillelcoren.utils.StringUtils;
	
	import img.Imagens;
	
	import janelas.cadastro.Funcionario.Form.Edicao.Funcionario_FormEdicao;
	import janelas.cadastro.Funcionario.Form.Pesquisa.Funcionario_FormPesquisa;
	
	import mx.collections.ArrayCollection;
	import mx.containers.ViewStack;
	import mx.controls.Alert;
	import mx.core.Container;
	
	import pesquisas.PesquisaFuncionario;
	
	public class FuncionarioScript
	{
		public function FuncionarioScript()
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
		
		public function defineState(comissionado:Boolean, acessaSistema:Boolean):String
		{
			if (comissionado && acessaSistema)
				return "stateFuncionarioComissionadoAcessaSistema";
	 		else if (comissionado)
	 			return "stateFuncionarioComissionado";
	 		else if (acessaSistema)
	 			return "stateFuncionarioAcessaSistema";
	 		else
	 			return null;
		 }
		 
		 /**
		 * FUNÇÕES TELA PESQUISA
		 * */
		 public function limpaFormPesquisa(formPesquisa:Funcionario_FormPesquisa):void
		 {
		 	formPesquisa.dpFuncionario.removeAll();
		 	formPesquisa.cpFuncionario.text = "";
		 }
		 
		 public function limpaFormEdicao(formEdicao:Funcionario_FormEdicao):void
		 {
		 	formEdicao.lblFuncionario.text = "";
		 	formEdicao.cmbCargo.selectedIndex = 0;
		 	formEdicao.ckbFuncionarioComissionado.selected = false;
		 	
		 	formEdicao.ckbMaoDeObra.selected = false;
		 	formEdicao.ckbMaoDeObraGarantia.selected = false;
		 	formEdicao.ckbMaoDeObraGeral.selected = false;
		 	formEdicao.ckbMaoDeObraGeralEmGarantia.selected = false;
		 	formEdicao.ckbProdutos.selected = false;
		 	formEdicao.ckbProdutosEmGarantia.selected = false;
		 	formEdicao.ckbMontanteTotal.selected = false;
		 	
		 	formEdicao.nsMaoDeObra.value = 0;
		 	formEdicao.nsMaoDeObraGarantia.value = 0;
		 	formEdicao.nsMaoDeObraGeral.value = 0;
		 	formEdicao.nsMaoDeObraGeralEmGarantia.value = 0;
		 	formEdicao.nsProdutos.value = 0;
		 	formEdicao.nsProdutosEmGarantia.value = 0;
		 	formEdicao.nsMontanteTotal.value = 0;
		 	
		 	formEdicao.dpPermissoesSistema.removeAll();
		 	formEdicao.idClienteFuncionario = 0;
		 	formEdicao.idClienteFuncionarioComissionamento = 0;
		 }
		 
		 public function buscaFuncionario(strBusca:String):ArrayCollection
		 {
		 	var arrayResultadoBusca:Array = PesquisaFuncionario.pesquisar(strBusca);
		 	var arrayCollectionRetorno:ArrayCollection = new ArrayCollection();
		 	
		 	var usuarioTecnico:Boolean;
		 	for each (var funcionario:Cliente in arrayResultadoBusca)
		 	{
		 		//verifica se o acesso do usuário é tecnico, se for o laço é descontinuado. os usuários técnicos não serão apresentados ao cliente final
		 		usuarioTecnico = false;
		 		if (funcionario.usuarioSistema)
		 			for each (var yyy:ClienteFuncionarioUsuario in App.single.cache.arrayClienteFuncionarioUsuario)
		 			{
		 				if (yyy.idCliente != funcionario.id || yyy.idEmp != Sessao.unica.idEmp)
		 					continue;
	 					usuarioTecnico = yyy.usuarioTecnico;
	 					break;
		 			}
	 			if (usuarioTecnico)
	 				continue;
		 		
		 		var cargo:Cargo = null;
		 		for each (var xxx:Cargo in App.single.cache.arrayCargo)
		 		{
		 			if (funcionario.idCargo != xxx.id || xxx.idEmp != Sessao.unica.idEmp)
		 				continue;
	 				cargo = xxx;
	 				break;
		 		}
		 		
		 		var obj:Object = new Object();
		 		obj.cargo = cargo;
		 		obj.cod = funcionario.id;
		 		obj.nome = funcionario.nome;
		 		obj.nomeCargo = (cargo)?cargo.nomeCargo:"NÃO DEFINIDO";
		 		obj.comissionado = funcionario.comissionado;
		 		obj.acessaSistema = funcionario.usuarioSistema;
		 		arrayCollectionRetorno.addItem(obj);
		 	}
		 	
		 	return arrayCollectionRetorno;
		 }
		 
		 /**
		 * FUNÇÕES TELA EDIÇÃO
		 * */
		 public function carregaConfiguracoesCargo(cargo:Cargo, mdlPai:JnlCadFuncionario):void
		 {
		 	if (!cargo || cargo.id == 0)
		 	{
		 		Alert.show("Selecione um cargo para carregar suas configurações", "Mensagem SDE", 4, null, null, Imagens.unica.icn_32_info);
		 		return;
		 	}
		 	
		 	var clienteFuncionario:Cliente = App.single.cache.getCliente(mdlPai.formEdicao.idClienteFuncionario).clone();
		 	var cargoComissionamento:CargoComissionamento;
		 	var clienteFuncionarioUsuario:ClienteFuncionarioUsuario;
		 	
		 	//se cargo é comissionado, copia suas configurações para o usuário
		 	if (cargo.comissionado)
		 	{
		 		for each (var xxx:CargoComissionamento in App.single.cache.arrayCargoComissionamento)
		 		{
		 			if (xxx.idCargo != cargo.id || xxx.idEmp != Sessao.unica.idEmp)
		 				continue;
	 				cargoComissionamento = xxx;
	 				break;
		 		}
		 		
		 		mdlPai.formEdicao.ckbMaoDeObra.selected = cargoComissionamento.calculaMaoDeObra;
		 		mdlPai.formEdicao.ckbMaoDeObraGarantia.selected = cargoComissionamento.calculaMaoDeObraGarantia;
		 		mdlPai.formEdicao.ckbMaoDeObraGeral.selected = cargoComissionamento.calculaMaoDeObraGeral;
		 		mdlPai.formEdicao.ckbMaoDeObraGeralEmGarantia.selected = cargoComissionamento.calculaMaoDeObraGeralGarantia;
		 		mdlPai.formEdicao.ckbProdutos.selected = cargoComissionamento.calculaProdutos;
		 		mdlPai.formEdicao.ckbProdutosEmGarantia.selected = cargoComissionamento.calculaProdutosEmGarantia;
		 		mdlPai.formEdicao.ckbMontanteTotal.selected = cargoComissionamento.calculaMontanteTotal;
		 		
		 		mdlPai.formEdicao.nsMaoDeObra.value = cargoComissionamento.comissaoMaoDeObra;
		 		mdlPai.formEdicao.nsMaoDeObraGarantia.value = cargoComissionamento.comissaoMaoDeObraGarantia;
		 		mdlPai.formEdicao.nsMaoDeObraGeral.value = cargoComissionamento.comissaoMaoDeObraGeral;
		 		mdlPai.formEdicao.nsMaoDeObraGeralEmGarantia.value = cargoComissionamento.comissaoMaoDeObraGeralGarantia;
		 		mdlPai.formEdicao.nsProdutos.value = cargoComissionamento.comissaoProdutos;
		 		mdlPai.formEdicao.nsProdutosEmGarantia.value = cargoComissionamento.comissaoProdutosEmGarantia;
		 		mdlPai.formEdicao.nsMontanteTotal.value = cargoComissionamento.comissaoMontanteTotal;
		 	}
		 	mdlPai.formEdicao.ckbFuncionarioComissionado.selected = cargo.comissionado;
		 	
		 	//se o cargo acessa o sistema ou se usuario tem acesso ao sistema, copia suas configurações para o usuário
		 	mdlPai.formEdicao.dpPermissoesSistema.removeAll();
		 	if (clienteFuncionario.usuarioSistema)
		 	{
		 		for each (var cfu:ClienteFuncionarioUsuario in App.single.cache.arrayClienteFuncionarioUsuario)
		 		{
		 			if (cfu.idCliente != clienteFuncionario.id || cfu.idEmp != Sessao.unica.idEmp)
		 				continue;
	 				clienteFuncionarioUsuario = cfu;
	 				break;
		 		}
		 		
		 		var obj:Object;
		 		var arrayMenusConfCargo:Array = [];
	 			for each (var cp:CargoPermissao in App.single.cache.arrayCargoPermissao.sortOn(CargoPermissao.campo_variavel))
	 			{
	 				if (cp.idCargo != cargo.id || cp.idEmp != Sessao.unica.idEmp)
	 					continue;
 					arrayMenusConfCargo.push(cp);
	 			}
		 		
		 		var menus:ArrayCollection = new ArrayCollection();
		 		var menuExiste:Boolean;
		 		for each (var sdeConfig:SdeConfig in App.single.cache.arraySdeConfig.sortOn(SdeConfig.campo_variavel))
		 		{
		 			var id:Number = 0;
	 				for each (var cfp:ClienteFuncionarioPermissao in App.single.cache.arrayClienteFuncionarioPermissao)
	 				{
	 					if (cfp.idEmp != Sessao.unica.idEmp || cfp.variavel != sdeConfig.variavel)
	 						continue;
 						id = cfp.id;
 						break;
	 				}
		 			
		 			menuExiste = false;
		 			for each (var yyy:CargoPermissao in arrayMenusConfCargo)
		 			{
		 				if (yyy.variavel != sdeConfig.variavel || sdeConfig.valor != "1")
		 					continue;
	 					obj = new Object();
	 					obj.cod = id;
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
 						obj.cod = id;
 						obj.menu = sdeConfig.variavel;
 						obj.permitido = false;
 						menus.addItem(obj);
		 			}
		 		}
		 		mdlPai.formEdicao.dpPermissoesSistema = menus;
		 	}
		 	
		 	mdlPai.formEdicao.currentState = defineState(cargo.comissionado, clienteFuncionario.usuarioSistema);
		 }
		 
		 public function carregaFuncionarioSelecionado(obj:Object, mdlPai:JnlCadFuncionario):void
		 {
		 	if (!obj)
		 	{
		 		Alert.show("Selecione um funcionário na tabela para prosseguir", "Mensagem SDE", 4, null, null, Imagens.unica.icn_32_info);
		 		return;
		 	}
		 	
		 	mdlPai.formEdicao.idClienteFuncionario = obj.cod;
		 	
		 	var cargo:Cargo = obj.cargo;
		 	var funcionario:Cliente = App.single.cache.getCliente(obj.cod).clone();
		 	
		 	mdlPai.formEdicao.lblFuncionario.text = funcionario.nome;
		 	mdlPai.formEdicao.cmbCargo.selectedItem = cargo;
		 	
		 	//Se funcionário é comissionado carrega dados do comissionamento
		 	if (funcionario.comissionado)
		 	{
		 		var funcionarioComissionamento:ClienteFuncionarioComissionamento;
		 		for each (var fc:ClienteFuncionarioComissionamento in App.single.cache.arrayClienteFuncionarioComissionamento)
		 		{
		 			if (fc.idCliente != funcionario.id || fc.idEmp != Sessao.unica.idEmp)
		 				continue;
	 				funcionarioComissionamento = fc;
	 				break;
		 		}
		 		
		 		if (!funcionarioComissionamento)
		 		{
		 			Alert.show("As configurações de comissionamento do funcionário não foram encontradas.\nID:" + funcionario.id, "Erro SDE", 4, null, null, Imagens.unica.icn_32_deleta);
		 			return;
		 		}
		 		
		 		mdlPai.formEdicao.idClienteFuncionarioComissionamento = funcionarioComissionamento.id;
		 		
		 		mdlPai.formEdicao.ckbMaoDeObra.selected = funcionarioComissionamento.calculaMaoDeObra;
		 		mdlPai.formEdicao.ckbMaoDeObraGarantia.selected = funcionarioComissionamento.calculaMaoDeObraGarantia;
		 		mdlPai.formEdicao.ckbMaoDeObraGeral.selected = funcionarioComissionamento.calculaMaoDeObraGeral;
		 		mdlPai.formEdicao.ckbMaoDeObraGeralEmGarantia.selected = funcionarioComissionamento.calculaMaoDeObraGeralGarantia;
		 		mdlPai.formEdicao.ckbProdutos.selected = funcionarioComissionamento.calculaProdutos;
		 		mdlPai.formEdicao.ckbProdutosEmGarantia.selected = funcionarioComissionamento.calculaProdutosEmGarantia;
		 		mdlPai.formEdicao.ckbMontanteTotal.selected = funcionarioComissionamento.calculaMontanteTotal;
		 		
		 		mdlPai.formEdicao.nsMaoDeObra.value = funcionarioComissionamento.comissaoMaoDeObra;
		 		mdlPai.formEdicao.nsMaoDeObraGarantia.value = funcionarioComissionamento.comissaoMaoDeObraGarantia;
		 		mdlPai.formEdicao.nsMaoDeObraGeral.value = funcionarioComissionamento.comissaoMaoDeObraGeral;
		 		mdlPai.formEdicao.nsMaoDeObraGeralEmGarantia.value = funcionarioComissionamento.comissaoMaoDeObraGeralGarantia;
		 		mdlPai.formEdicao.nsProdutos.value = funcionarioComissionamento.comissaoProdutos;
		 		mdlPai.formEdicao.nsProdutosEmGarantia.value = funcionarioComissionamento.comissaoProdutosEmGarantia;
		 		mdlPai.formEdicao.nsMontanteTotal.value = funcionarioComissionamento.comissaoMontanteTotal;
		 	}
	 		mdlPai.formEdicao.ckbFuncionarioComissionado.selected = funcionario.comissionado;
		 	
		 	//se funcionário é usuário do sistema carrega dados do acesso
		 	if (funcionario.usuarioSistema)
		 	{
		 		var arrayMenus:Array = [];
		 		for each (var xxx:ClienteFuncionarioPermissao in App.single.cache.arrayClienteFuncionarioPermissao.sortOn(ClienteFuncionarioPermissao.campo_variavel))
		 		{
		 			for each (var zzz:ClienteFuncionarioUsuario in App.single.cache.arrayClienteFuncionarioUsuario)
		 			{
		 				if (zzz.idCliente != funcionario.id || xxx.idClienteFuncionarioUsuario != zzz.id || zzz.idEmp != Sessao.unica.idEmp)
		 					continue;
	 					if (zzz.idCliente == funcionario.id && xxx.idEmp == Sessao.unica.idEmp)
		 					arrayMenus.push(xxx);
		 				break;
		 			}
		 		}
		 		
		 		var menus:ArrayCollection = new ArrayCollection();
		 		var menuExiste:Boolean;
		 		var obj:Object;
		 		for each (var sdeConfig:SdeConfig in App.single.cache.arraySdeConfig.sortOn(SdeConfig.campo_variavel))
		 		{
		 			menuExiste = false;
		 			for each (var yyy:ClienteFuncionarioPermissao in arrayMenus)
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
		 		
		 		mdlPai.formEdicao.dpPermissoesSistema = menus;
		 	}
		 	else
		 		mdlPai.formEdicao.dpPermissoesSistema.removeAll();
		 	
		 	mdlPai.formEdicao.currentState = defineState(funcionario.comissionado, funcionario.usuarioSistema);
		 	
		 	mudaTela(mdlPai.vs, mdlPai.formEdicao);
		 	limpaFormPesquisa(mdlPai.formPesquisa);
		 }
		 
		 public function salvaFuncionario(mdlPai:JnlCadFuncionario, mdlFuncionario:Funcionario_FormEdicao):void
		 {
		 	//VALIDAÇÃO DOS DADOS INSERIDOS NO CADASTRO
		 	if (!(mdlFuncionario.cmbCargo.selectedItem as Cargo) || (mdlFuncionario.cmbCargo.selectedItem as Cargo).id == 0)
		 	{
		 		Alert.show("Selecione um cargo", "Mensagem SDE", 4, null, null, Imagens.unica.icn_32_alerta);
		 		mdlFuncionario.cmbCargo.setFocus();
		 		return;
		 	}
		 	
		 	var cargo:Cargo = App.single.cache.getCargo((mdlFuncionario.cmbCargo.selectedItem as Cargo).id).clone();
		 	
		 	var clienteFuncionario:Cliente = new Cliente();
		 	if (mdlFuncionario.idClienteFuncionario != 0)
		 		clienteFuncionario =  App.single.cache.getCliente(mdlFuncionario.idClienteFuncionario).clone();
	 		else
	 		{
	 			Alert.show("Ocorreu um problema com a referência ");
	 		}
	 		
	 		clienteFuncionario.idCargo = cargo.id;
	 		
	 		var clienteFuncionarioComissionamento:ClienteFuncionarioComissionamento;
	 		if (mdlFuncionario.ckbFuncionarioComissionado.selected)
	 		{
	 			clienteFuncionarioComissionamento = new ClienteFuncionarioComissionamento();
	 			if (mdlFuncionario.idClienteFuncionarioComissionamento != 0)
	 				clienteFuncionarioComissionamento = App.single.cache.getClienteFuncionarioComissionamento(mdlFuncionario.idClienteFuncionarioComissionamento).clone();
 				clienteFuncionario.comissionado = true;
 				
 				clienteFuncionarioComissionamento.calculaMontanteTotal = mdlFuncionario.ckbMontanteTotal.selected;
		 		clienteFuncionarioComissionamento.calculaMaoDeObra = mdlFuncionario.ckbMaoDeObra.selected;
		 		clienteFuncionarioComissionamento.calculaMaoDeObraGeral = mdlFuncionario.ckbMaoDeObraGeral.selected;
		 		clienteFuncionarioComissionamento.calculaMaoDeObraGarantia = mdlFuncionario.ckbMaoDeObraGarantia.selected;
		 		clienteFuncionarioComissionamento.calculaMaoDeObraGeralGarantia = mdlFuncionario.ckbMaoDeObraGeralEmGarantia.selected;
		 		clienteFuncionarioComissionamento.calculaProdutos = mdlFuncionario.ckbProdutos.selected;
		 		clienteFuncionarioComissionamento.calculaProdutosEmGarantia = mdlFuncionario.ckbProdutosEmGarantia.selected;
		 		
		 		clienteFuncionarioComissionamento.comissaoMontanteTotal = (mdlFuncionario.ckbMontanteTotal.selected)?mdlFuncionario.nsMontanteTotal.value:0;
		 		clienteFuncionarioComissionamento.comissaoMaoDeObra = (mdlFuncionario.ckbMaoDeObra.selected)?mdlFuncionario.nsMaoDeObra.value:0;
		 		clienteFuncionarioComissionamento.comissaoMaoDeObraGeral = (mdlFuncionario.ckbMaoDeObraGeral.selected)?mdlFuncionario.nsMaoDeObraGeral.value:0;
		 		clienteFuncionarioComissionamento.comissaoMaoDeObraGarantia = (mdlFuncionario.ckbMaoDeObraGarantia.selected)?mdlFuncionario.nsMaoDeObraGarantia.value:0;
		 		clienteFuncionarioComissionamento.comissaoMaoDeObraGeralGarantia = (mdlFuncionario.ckbMaoDeObraGeralEmGarantia.selected)?mdlFuncionario.nsMaoDeObraGeralEmGarantia.value:0;
		 		clienteFuncionarioComissionamento.comissaoProdutos = (mdlFuncionario.ckbProdutos.selected)?mdlFuncionario.nsProdutos.value:0;
		 		clienteFuncionarioComissionamento.comissaoProdutosEmGarantia = (mdlFuncionario.ckbProdutosEmGarantia.selected)?mdlFuncionario.nsProdutosEmGarantia.value:0;
	 		}
	 		else
	 			clienteFuncionario.comissionado = false;
 			
 			var arrayClienteFuncionarioPermissoes:Array = [];
 			if (clienteFuncionario.usuarioSistema)
 			{
 				for each (var obj:Object in mdlFuncionario.dpPermissoesSistema)
 				{
 					var clienteFuncionarioPermissao:ClienteFuncionarioPermissao = new ClienteFuncionarioPermissao();
 					if (obj.cod != 0)
 						clienteFuncionarioPermissao = App.single.cache.getClienteFuncionarioPermissao(obj.cod).clone();
 					clienteFuncionarioPermissao.variavel = obj.menu;
 					clienteFuncionarioPermissao.valor = (obj.permitido)?"1":"0";
 					arrayClienteFuncionarioPermissoes.push(clienteFuncionarioPermissao);
 				}
 			}
 			
 			App.single.n.modificacoes.ClienteFuncionario_Atualiza(clienteFuncionario, clienteFuncionarioComissionamento, arrayClienteFuncionarioPermissoes,
 				function():void
 				{
 					limpaFormEdicao(mdlFuncionario);
 					mudaTela(mdlPai.vs, mdlPai.formPesquisa);
 				}
 			);
		 }
	}
}