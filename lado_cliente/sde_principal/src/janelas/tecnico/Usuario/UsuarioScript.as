package janelas.tecnico.Usuario
{
	import Core.Alerta.AlertaSistema;
	import Core.App;
	import Core.Sessao;
	
	import SDE.Entidade.Cargo;
	import SDE.Entidade.CargoPermissao;
	import SDE.Entidade.Cliente;
	import SDE.Entidade.ClienteFuncionarioPermissao;
	import SDE.Entidade.ClienteFuncionarioUsuario;
	import SDE.Entidade.SdeConfig;
	
	import com.hillelcoren.utils.StringUtils;
	
	import img.Imagens;
	
	import janelas.tecnico.Usuario.Form.Cadastro.Usuario_FormCadastro;
	
	import mx.collections.ArrayCollection;
	import mx.containers.ViewStack;
	import mx.controls.Alert;
	import mx.core.Container;
	
	import pesquisas.PesquisaFuncionario;
	import pesquisas.PesquisaUsuario;
	
	public class UsuarioScript
	{
		public function UsuarioScript()
		{
		}
		
		/**
		 * FUNÇÕES GENÉRICAS
		 * */
		 public function mudaTela(vs:ViewStack, container:Container):void
		 {
		 	vs.selectedChild = container;
		 }
		 
		 /**
		 * FUNÇÕES TELA PESQUISA
		 * */
		 public function pesquisaUsuario(strBusca:String):ArrayCollection
		 {
		 	var arrayResultadoBusca:Array = PesquisaUsuario.pesquisar(strBusca);
		 	var arrayCollectionRetorno:ArrayCollection = new ArrayCollection();
		 	
		 	for each (var usuario:ClienteFuncionarioUsuario in arrayResultadoBusca)
		 	{
		 		var funcionario:Cliente = App.single.cache.getCliente(usuario.idCliente);
		 		var obj:Object = new Object();
		 		obj.cod = usuario.id;
		 		obj.nome = funcionario.nome;
		 		obj.login = usuario.login;
		 		obj.tecnico = usuario.usuarioTecnico;
		 		arrayCollectionRetorno.addItem(obj);
		 	}
		 	return arrayCollectionRetorno;
		 }		 
		 
		 /**
		 * FUNÇÕES TELA CADASTRO
		 * */
		 public function pesquisaFuncionario(strBusca:String):ArrayCollection
		 {
		 	var arrayResultadoBusca:Array = PesquisaFuncionario.pesquisar(strBusca);
		 	var arrayCollectionRetorno:ArrayCollection = new ArrayCollection();
		 	
		 	for each (var funcionario:Cliente in arrayResultadoBusca)
		 	{
		 		if (funcionario.usuarioSistema)
		 			continue;
		 		var cargo:Cargo;
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
		 		arrayCollectionRetorno.addItem(obj);
		 	}
		 	
		 	return arrayCollectionRetorno;
		 }
		 
		 public function carregaMenusDisponiveisEmpresa(mdlUsuario:Usuario_FormCadastro, ehTecnico:Boolean=false):ArrayCollection
		 {
		 	var arrayCollectionMenusRetorno:ArrayCollection = new ArrayCollection();
		 	var menus:Array  = App.single.cache.arraySdeConfig.sortOn(SdeConfig.campo_variavel);
		 	var menusAtuais:Array;
		 	if (mdlUsuario.dpPermissoesSistema)
		 		menusAtuais = mdlUsuario.dpPermissoesSistema.source;
	 		else
		 		menusAtuais = [];
		 	
		 	var menuExiste:Boolean;
		 	var obj:Object;
		 	for each (var sdeConfig:SdeConfig in menus)
		 	{
		 		menuExiste = false;
		 		for each (var yyy:Object in menusAtuais)
		 		{
		 			if (yyy.menu != sdeConfig.variavel || sdeConfig.valor != "1")
		 				continue;
	 				obj = new Object();
	 				obj.cod = yyy.cod;
	 				obj.menu = yyy.menu;
	 				obj.permitido = yyy.permitido;
	 				arrayCollectionMenusRetorno.addItem(obj);
	 				menuExiste = true;
	 				break;
		 		}
		 		
		 		if (!menuExiste)
		 		{
			 		if (!StringUtils.beginsWith(sdeConfig.variavel, "Menu"))
			 			continue;
		 			if (!ehTecnico)
		 				if (sdeConfig.valor == "0")
		 					continue;
	 				obj = new Object();
	 				obj.cod = 0;
	 				obj.menu = sdeConfig.variavel;
	 				obj.permitido = false;
	 				arrayCollectionMenusRetorno.addItem(obj);
 				}
		 	}
		 	return arrayCollectionMenusRetorno;
		 }
		 
		 public function carregaMenusConfCargo(idCargo:Number):ArrayCollection
		 {
		 	var cargo:Cargo = App.single.cache.getCargo(idCargo);
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
	 				obj.cod = 0;
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
		 	
		 	return menus;
		 }
		 
		 public function ckbUsuarioTecnico_Click(mdlUsuario:Usuario_FormCadastro, ehTecnico:Boolean):void
		 {
		 	if (ehTecnico)
		 		mdlUsuario.dpPermissoesSistema = carregaMenusDisponiveisEmpresa(mdlUsuario, true);
	 		else
	 			mdlUsuario.dpPermissoesSistema = carregaMenusDisponiveisEmpresa(mdlUsuario, false);
		 }
		 
		 public function usuarioSelecionado(obj:Object, mdlPai:JnlTecUsuario):void
		 {
		 	if (!obj)
		 	{
		 		Alert.show("Selecione um usuário para prosseguir.", "Mensagem SDE", 4, null, null, Imagens.unica.icn_32_info);
		 		return;
		 	}
		 	
		 	mdlPai.formCadastro.ehEditando = true;
		 	
		 	var usuario:ClienteFuncionarioUsuario = App.single.cache.getClienteFuncionarioUsuario(obj.cod);
		 	var funcionario:Cliente = App.single.cache.getCliente(usuario.idCliente);
		 	var cargo:Cargo = App.single.cache.getCargo(funcionario.idCargo);
		 	
		 	mdlPai.formCadastro.idClienteFuncionarioUsuario = usuario.id;
		 	mdlPai.formCadastro.idClienteFuncionario = funcionario.id;
		 	
		 	mdlPai.formCadastro.lblFuncionario.text = funcionario.nome;
		 	mdlPai.formCadastro.lblCargo.text = (cargo)?cargo.nomeCargo:"NÃO DEFINIDO";
		 	mdlPai.formCadastro.txtLogin.text = usuario.login;
		 	mdlPai.formCadastro.ckbUsuarioTecnico.selected = usuario.usuarioTecnico;
		 	
		 	var arrayMenus:Array = [];
		 	for each (var xxx:ClienteFuncionarioPermissao in App.single.cache.arrayClienteFuncionarioPermissao.sortOn(ClienteFuncionarioPermissao.campo_variavel))
		 	{
		 		if (xxx.idClienteFuncionarioUsuario == usuario.id && xxx.idEmp == Sessao.unica.idEmp)
		 			arrayMenus.push(xxx);
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
 					if (!usuario.usuarioTecnico)
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
		 	mudaTela(mdlPai.vs, mdlPai.formCadastro);
		 	mudaTela(mdlPai.formCadastro.vsUsuarioCadastro, mdlPai.formCadastro.configuracaoPermissoes);
		 }
		 
		 public function funcionarioSelecionado(obj:Object, mdlPai:JnlTecUsuario):void
		 {
		 	if (!obj)
		 	{
		 		Alert.show("Selecione um funcionário para prosseguir.", "Mensagem SDE", 4, null, null, Imagens.unica.icn_32_info);
		 		return;
		 	}
		 	
		 	mdlPai.formCadastro.ehEditando = false;
		 	mdlPai.formCadastro.idClienteFuncionario = obj.cod;
		 	
		 	mdlPai.formCadastro.idClienteFuncionarioUsuario = 0;
		 	mdlPai.formCadastro.idClienteFuncionario = obj.cod; /**TALVEZ EU NÃO PRECISE DISTO*/
		 	
		 	mdlPai.formCadastro.lblFuncionario.text = obj.nome;
		 	mdlPai.formCadastro.lblCargo.text = obj.nomeCargo;
		 	
		 	if (!obj.cargo)
		 		mdlPai.formCadastro.dpPermissoesSistema = carregaMenusDisponiveisEmpresa(mdlPai.formCadastro);
	 		else
	 			mdlPai.formCadastro.dpPermissoesSistema = carregaMenusConfCargo(obj.cargo.id);
		 	
		 	mudaTela(mdlPai.formCadastro.vsUsuarioCadastro, mdlPai.formCadastro.configuracaoPermissoes);
		 }
		 
		 public function salvaCadastroLogin(mdlPai:JnlTecUsuario, mdlUsuario:Usuario_FormCadastro):void
		 {
		 	//VALIDAÇÃO DOS DADOS INSERIDOS
		 	if (mdlUsuario.txtLogin.text == "")
		 	{
		 		Alert.show("Informe o login do usuário", "Alerta SDE", 4, null,
		 			function():void{mdlUsuario.txtLogin.setFocus();},
		 			Imagens.unica.icn_32_alerta);
	 			return;
		 	}
		 	
		 	var login:ClienteFuncionarioUsuario = new ClienteFuncionarioUsuario();
		 	if (mdlUsuario.idClienteFuncionarioUsuario != 0)
		 		login = App.single.cache.getClienteFuncionarioUsuario(mdlUsuario.idClienteFuncionarioUsuario);
	 		else
	 		{
	 			login.id = mdlUsuario.idClienteFuncionarioUsuario;
		 		login.idCliente = mdlUsuario.idClienteFuncionario;
		 	}
		 	
		 	login.login = mdlUsuario.txtLogin.text;
		 	login.usuarioTecnico = mdlUsuario.ckbUsuarioTecnico.selected;
		 	
		 	var arrayClienteFuncionarioPermissoes:Array = [];
		 	for each (var obj:Object in mdlUsuario.dgPermissoes.dataProvider)
		 	{
		 		var clienteFuncionarioPermissao:ClienteFuncionarioPermissao = new ClienteFuncionarioPermissao();
		 		if (obj.cod != 0)
		 			clienteFuncionarioPermissao = App.single.cache.getClienteFuncionarioPermissao(obj.cod);
	 			else
	 				clienteFuncionarioPermissao.id = obj.cod;
 				clienteFuncionarioPermissao.variavel = obj.menu;
 				clienteFuncionarioPermissao.valor = (obj.permitido)?"1":"0";
 				arrayClienteFuncionarioPermissoes.push(clienteFuncionarioPermissao);
		 	}
		 	
		 	App.single.n.modificacoes.Usuario_NovoAtualizacao(login, arrayClienteFuncionarioPermissoes,
		 		function():void
		 		{
		 			mudaTela(mdlPai.vs, mdlPai.formPesquisa);
		 			limpaCadastroUsuario(mdlUsuario);
		 			limpaPesquisaUsuario(mdlPai);
		 		}
		 	);
		 }
		 
		 public function limpaPesquisaUsuario(form:JnlTecUsuario):void
		 {
		 	form.formPesquisa.cpUsuario.text = "";
		 	form.formPesquisa.dpUsuariosSistema.removeAll();
		 }
		 
		 public function limpaPesquisaFuncionario(form:Usuario_FormCadastro):void
		 {
		 	form.cpFuncionario.text = "";
		 	form.dpFuncionario.removeAll();
		 }
		 
		 public function limpaCadastroUsuario(form:Usuario_FormCadastro):void
		 {
		 	form.lblFuncionario.text = "";
		 	form.lblCargo.text = "";
		 	form.txtLogin.text = "";
		 	form.ckbUsuarioTecnico.selected = false;
		 }
		 
		 public function cancelaCadastroEdicao(form:Usuario_FormCadastro, mdlPai:JnlTecUsuario):void
		 {
		 	limpaPesquisaFuncionario(form);
		 	limpaCadastroUsuario(form);
		 	form.ehEditando = false;
		 	limpaPesquisaUsuario(mdlPai);
		 	mudaTela(mdlPai.vs, mdlPai.formPesquisa);
		 	mudaTela(form.vsUsuarioCadastro, form.pesquisaFuncionario);
		 }
	}
}