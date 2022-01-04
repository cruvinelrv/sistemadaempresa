// ActionScript file
import Core.Alerta.AlertaSistema;
import Core.App;
import Core.Sessao;
import Core.Utils.Funcoes;
import Core.Utils.MyArrayUtils;

import SDE.Entidade.Cargo;
import SDE.Entidade.Cliente;
import SDE.Entidade.ClienteContato;
import SDE.Entidade.ClienteEndereco;
import SDE.Entidade.ClienteFamiliar;
import SDE.Entidade.ClienteVeiculo;
import SDE.Entidade.IBGEMunicipio;
import SDE.Enumerador.EPesTipo;
import SDE.Nuvens;

import flash.events.Event;
import flash.media.Camera;

import mx.collections.ArrayCollection;
import mx.core.Application;
import mx.core.Container;
import mx.events.ValidationResultEvent;
import mx.managers.PopUpManager;
import mx.validators.ValidationResult;
	
	private var ss:Sessao;
	private var n:Nuvens;
	
	[Bindable] private var _c:Cliente = null;
	[Bindable] private var isCpfValido:Boolean = false;
	[Bindable] private var arraycContatos:ArrayCollection = new ArrayCollection();
	[Bindable] private var arraycEnderecos:ArrayCollection = new ArrayCollection();
	[Bindable] private var arraycAmigos:ArrayCollection = new ArrayCollection();
	[Bindable] private var arraycVeiculos:ArrayCollection = new ArrayCollection();
	private var arrayc_em_edicao:ArrayCollection = null;
	
	[Bindable] private var objetoOriginal:* = null;
	[Bindable] private var objetoEditando:* = null;
	
	private var popup_aberto:Container = null;
	
	private var tipoVeiculoTexto:Array = null;
	private var tipoContatoTexto:Array = null;
	
	private var ibge_municipios:Array;
	[Bindable] private var ibge_estados:Array;
	[Bindable] private var ibge_municipios_filtrados:ArrayCollection = new ArrayCollection();
	[Bindable] private var enderecoEditando:ClienteEndereco= null;
	private var dictEstados:Array;
	private var dictMunicipios:Array;
	
	private var validacoes:Array = [];//MasterTextInput[]
	
	private function set cliente_selecionado(v:Cliente):void
	{
		arraycContatos.removeAll();
		arraycEnderecos.removeAll();
		arraycAmigos.removeAll();
		arraycVeiculos.removeAll();
		
		_c = v;//.clone();
		//faz a tela andar dependendo se está nulo ou não
		this.currentState = (!v) ? "state1" : "state2";
		if (v)
		{
			for each (var cc:ClienteContato in n.cache.arrayClienteContato)
			{
				if (cc.idCliente == v.id)
					arraycContatos.addItem(cc.clone());
			}
			for each (var ce:ClienteEndereco in n.cache.arrayClienteEndereco)
			{
				if (ce.idCliente == v.id)
					arraycEnderecos.addItem(ce.clone());
			}
			for each (var cf:ClienteFamiliar in n.cache.arrayClienteFamiliar)
			{
				if (cf.idCliente == v.id)
					arraycAmigos.addItem(cf.clone());
			}
			for each (var cv:ClienteVeiculo in n.cache.arrayClienteVeiculo)
			{
				if (cv.idCliente == v.id)
					arraycVeiculos.addItem(cv.clone());
			}
			
			//this.currentState = (_c.tipo==EPesTipo.Fisica) ? null : "Juridica";
		
			AlertaSistema.mensagem( "C: "+_c.id, true);
			
			var s:String = (_c.tipo==EPesTipo.Fisica) ? "CPF" : "CNPJ";
			lblTitulo.text = s+': '+Funcoes.MascaraCPF(_c.cpf_cnpj) +' Tipo: '+_c.tipo;
			
			binding_dados(_c);//assim não precisamos popular manualmente
			//vs.selectedIndex = 0; //seleciona a aba Observação, abaixo
			//esse método 'registra' todas as validações do sistema
			registraValidacoes();
			
			/* if (!_c.ehFuncionario){
				abaFuncionario.label = "";
				abaFuncionario.enabled = false;
			}
			else{
				abaFuncionario.label = "Funcionário";
				abaFuncionario.enabled = true;
			} */
			
			mouseEh(vbFuncionario, MouseEvent.MOUSE_OUT, _c.ehFuncionario);
			mouseEh(vbParceiro, MouseEvent.MOUSE_OUT, _c.ehParceiro);
			mouseEh(vbFornecedor, MouseEvent.MOUSE_OUT, _c.ehFornecedor);
			mouseEh(vbTransportador, MouseEvent.MOUSE_OUT, _c.ehTransportador);
		}
	}
	private function get cliente_selecionado():Cliente
	{
		return _c;
	}
	
	private function init():void
	{
		ss = Application.application.sessao;
		n = ss.nuvens;
		cliente_selecionado = null;
		
		abaContatos_grid.addEventListener('usuario_edita_contato', usuario_edita_contato);
		abaEnderecos_grid.addEventListener('usuario_edita_endereco', usuario_edita_endereco);
		abaAmigos_grid.addEventListener('usuario_edita_amigo', usuario_edita_amigo);
		abaVeiculos_grid.addEventListener('usuario_edita_veiculo', usuario_edita_veiculo);
		
		abaContatos_grid.addEventListener('usuario_remove_contato', usuario_remove_contato);
		abaEnderecos_grid.addEventListener('usuario_remove_endereco', usuario_remove_endereco);
		abaAmigos_grid.addEventListener('usuario_remove_amigo', usuario_remove_amigo);
		abaVeiculos_grid.addEventListener('usuario_remove_veiculo', usuario_remove_veiculo);
		
		escondePopUp(abaContatos_popup);
		escondePopUp(abaEnderecos_popup);
		escondePopUp(abaAmigos_popup);
		escondePopUp(abaVeiculos_popup);
		
		tipoContatoTexto = [];
		tipoContatoTexto['fone_fixo'] = 'Fone Fixo';
		tipoContatoTexto['celular'] = 'Celular';
		tipoContatoTexto['email'] = 'E-mail';
		tipoContatoTexto['msn'] = 'MSN';
		tipoContatoTexto['skype'] = 'Skype';
		tipoContatoTexto['fax'] = 'Fax';
		tipoContatoTexto['ddg'] = 'DDG';
		tipoContatoTexto['homepage'] = 'Pagina Web';
		
		tipoVeiculoTexto = [];
		tipoVeiculoTexto['automovel'] = 'Automóvel';
		tipoVeiculoTexto['caminhao'] = 'Caminhão';
		tipoVeiculoTexto['motocicleta'] = 'Motocicleta';
		tipoVeiculoTexto['reboque'] = 'Reboque';
		tipoVeiculoTexto['semi_reboque'] = 'Semi-reboque';
		tipoVeiculoTexto['maquina_agricola'] = 'Máquina Agrícola';
		
		ibge_municipios = App.single.cache.arrayIBGEMunicipio;
		ibge_estados = App.single.cache.arrayIBGEEstado;
		dictEstados = MyArrayUtils.asDictionary(ibge_estados,"codigo");
		dictMunicipios = MyArrayUtils.asDictionary(ibge_municipios,"codigo");
		
		resetar();
	}
	
	private function fn_ComboTipoVeiculo_Label(tipoVeiculo:String):String
	{
		return tipoVeiculoTexto[tipoVeiculo];
	}
	
	private function fn_ComboTipoContato_Label(tipoContato:String):String
	{
		return tipoContatoTexto[tipoContato];
	}
	
	private function escondePopUp(p:Container):void
	{
		p.parent.removeChild(p);
	}
	
	private function resetar():void
	{
		/*
		gridBalancos.dataProvider = n.cache.listBalanco();
		this.currentState='state1';
		*/
	}
	
	private function gridBalancos_ev_abrir(ev:Event):void
	{
		/*
		var id:Number = ev.target.data.id;
		//AlertaSistema.mensagem("id: "+id);
		sistema_abre_balanco(id);
		*/
	}
	
	private function gridBalancoItens_ev_remover(ev:Event):void
	{
		/*
		var id:Number = ev.target.data.id;
		AlertaSistema.mensagem("removido: "+id);
		n.modificacoes.Balanco_Remove(
			id, 
			function():void
			{
				preencheBalancoItens();
			}
		);
		*/
	}
	
	
	
	
	
	
	
	
	
	private function trata_cpCliente_change():void
	{
		var xxx:Cliente = cpCliente.selectedItem;
		cliente_selecionado = (xxx) ? xxx.clone() : null;
		if (xxx)
			vs.selectedChild = abaDados;
	}
	
	
	private function Criar():void
	{
		//verifica se os campos foram preenchidos
		var msg:String = '';
		if (txtNome.text == '')
			msg += 'Informe o Nome\n';
		if (txtApelRazao.text == '')
			msg += 'Informe ' + (rbFis.selected) ? 'o Apelido':'a Razão';
		if (msg != '')
		{
			AlertaSistema.mensagem(msg);
			return;
		}
		
		//verifica se o CPF ou CNPJ é válido
		var cpf:String = txtCPF.text;
		/*
		var valido:Boolean =
			(rbFis.selected)
			? Funcoes.validaCpf(cpf)
			: Funcoes.validaCnpj(cpf);
			*/
		//se CPF ou CNPJ for inválido processo é interrompido
		if (!validaCPF())
		{
			AlertaSistema.mensagem((rbFis.selected)?'CPF Inválido':'CNPJ inválido');
			return;
		}
		
		var cNovo:Cliente = new Cliente();
		cNovo.nome = txtNome.text;
		cNovo.apelido_razsoc = txtApelRazao.text;
		cNovo.cpf_cnpj = Funcoes.LimpaCPF(txtCPF.text);
		cNovo.tipo = (rbFis.selected) ? EPesTipo.Fisica : EPesTipo.Juridica;
		//Salva nova pessoa e direciona para cadastro de cliente
		
		n.modificacoes.Cliente_NovoAltera(cNovo,
			function(retorno:Cliente):void
			{
				cliente_selecionado = retorno;
			}
		);
		limpaCadastro();
	}
	
	
	private function validaCPF():Boolean
	{
		isCpfValido = false;
		var cpf:String = txtCPF.text;
		if (cpf.length == 0)
			return false;
		
		if (rbFis.selected)
			isCpfValido = Funcoes.validaCpf(cpf);
		else
			isCpfValido = Funcoes.validaCnpj(cpf);
			
		
		if (!isCpfValido)
			return false;
		
		for each(var xxx:Cliente in App.single.cache.arrayCliente)
		{
			if (xxx.cpf_cnpj==cpf)
			{
				AlertaSistema.mensagem("cliente "+xxx.nome+" já cadastrado");
				txtCPF.text="";
				txtCPF.setFocus();
				//cpCliente2.text = xxx.cpf_cnpj;
				break;
			}
		}
		return isCpfValido;
	}
	
	
	/* private function cmbCargo_Change():void
	{
		var cargo:Cargo = cmbCargo.getValor().clone();
		
		boundMontanteTotalCheckBox.selected = cargo.calculaMontanteTotal;
		boundMaoDeObraCheckBox.selected = cargo.calculaMaoDeObra;
		boundMaoDeObraGeralCheckBox.selected = cargo.calculaMaoDeObraGeral;
		boundMaoDeObraGarantiaCheckBox.selected = cargo.calculaMaoDeObraGarantia;
		boundMaoDeObraGeralGarantiaCheckBox.selected = cargo.calculaMaoDeObraGeralGarantia;
		boundProdutosEmGarantiaCheckBox.selected = cargo.calculaProdutosEmGarantia;
		boundProdutosCheckBox.selected = cargo.calculaProdutos;
		
		boundMontanteTotalNumericStepper.value = cargo.comissaoMontanteTotal;
		boundMaoDeObraNumericStepper.value = cargo.comissaoMaoDeObra;
		boundMaoDeObraGeralNumericStepper.value = cargo.comissaoMaoDeObraGeral;
		boundMaoDeObraGarantiaNumericStepper.value = cargo.comissaoMaoDeObraGarantia;
		boundMaoDeObraGeralGarantiaNumericStepper.value = cargo.comissaoMaoDeObraGeralGarantia;
		boundProdutosEmGarantiaNumericStepper.value = cargo.comissaoProdutosEmGarantia;
		boundProdutosNumericStepper.value = cargo.comissaoProdutos;
	} */
	
	private function btnDesfazer_click(voltar:Boolean):void
	{
		if(voltar)
			_voltar();
		//
	}
	
	private function btnSalvar_click(voltar:Boolean, aba:Container):void
	{
		if (aba==abaDados)
		{
			Sessao.unica.nuvens.modificacoes.Cliente_NovoAltera(_c,
				function():void
				{
					AlertaSistema.mensagem("Salvei Cliente!", true);
				}
			);
		}
		else if (aba==abaContatos)
		{
			Sessao.unica.nuvens.modificacoes.Cliente_Genericos_Salva(ClienteContato.CLASSE, arraycContatos.source,
				function():void
				{
					AlertaSistema.mensagem("Salvei Contatos!", true);
					cliente_selecionado = cliente_selecionado;
				}
			);
		}
		else if (aba==abaEnderecos)
		{
			Sessao.unica.nuvens.modificacoes.Cliente_Genericos_Salva(ClienteEndereco.CLASSE, arraycEnderecos.source,
				function():void
				{
					AlertaSistema.mensagem("Salvei Endereco!", true);
					cliente_selecionado = cliente_selecionado;
				}
			);
		}
		else if (aba==abaAmigos)
		{
			Sessao.unica.nuvens.modificacoes.Cliente_Genericos_Salva(ClienteFamiliar.CLASSE, arraycAmigos.source,
				function():void
				{
					AlertaSistema.mensagem("Salvei Autorizado!", true);
					cliente_selecionado = cliente_selecionado;
				}
			);
		}
		else if (aba==abaVeiculos)
		{
			Sessao.unica.nuvens.modificacoes.Cliente_Genericos_Salva(ClienteVeiculo.CLASSE, arraycVeiculos.source,
				function():void
				{
					AlertaSistema.mensagem("Salvei Veiculo!", true);
					cliente_selecionado = cliente_selecionado;
				}
			);
		}
		/* else if (aba==abaFuncionario)
		{
			atualizaCargo();
			Sessao.unica.nuvens.modificacoes.ClienteFuncionario_SalvaDados(cliente_selecionado,
				function():void
				{
					AlertaSistema.mensagem("Salvar Comissão");
					cliente_selecionado = cliente_selecionado;
				}
			);
		} */
		if(voltar)
			_voltar();
	}
	
	private function _voltar():void
	{
		cpCliente.selectedItem = null;
		cliente_selecionado = null;
	}
	private function limpaCadastro():void
	{
		rbFis.selected = true;
		txtCPF.text = '';
		txtNome.text = '';
		txtApelRazao.text = '';
	}
	
	
	
	/**DADOS*/
	
	private function binding_dados(cliente:Cliente):void
	{
		Funcoes.myBind(boundObsTextInput, "text", cliente, "obs");
		Funcoes.myBind(boundNomeTextInput, "text", cliente, "nome");
		Funcoes.myBind(boundApelidoTextInput, "text", cliente, "apelido_razsoc");
		Funcoes.myBind(boundDtNascTextInput, "text", cliente, "dtNasc");
		
		if (cliente.tipo ==EPesTipo.Fisica)
		{
			Funcoes.myBind(boundRGTextInput, "text", cliente, "rg");
			Funcoes.myBind(boundUFCombo, "selectedItem", cliente, "rgUF");
		}
	}
	
	/**FIM DADOS*/
	
	/**COMISSÃO*/
	
	/* private function binding_comissao(cliente:Cliente):void
	{
		preencheCargo();
		
		Funcoes.myBind(boundMontanteTotalCheckBox, "selected", cliente, "calculaMontanteTotal");
		Funcoes.myBind(boundMaoDeObraCheckBox, "selected", cliente, "calculaMaoDeObra");
		Funcoes.myBind(boundMaoDeObraGeralCheckBox, "selected", cliente, "calculaMaoDeObraGeral");
		Funcoes.myBind(boundMaoDeObraGarantiaCheckBox, "selected", cliente, "calculaMaoDeObraGarantia");
		Funcoes.myBind(boundMaoDeObraGeralGarantiaCheckBox, "selected", cliente, "calculaMaoDeObraGeralGarantia");
		Funcoes.myBind(boundProdutosEmGarantiaCheckBox, "selected", cliente, "calculaProdutosEmGarantia");
		Funcoes.myBind(boundProdutosCheckBox, "selected", cliente, "calculaProdutos");
		
		Funcoes.myBind(boundMontanteTotalNumericStepper, "value", cliente, "comissaoMontanteTotal");
		Funcoes.myBind(boundMaoDeObraNumericStepper, "value", cliente, "comissaoMaoDeObra");
		Funcoes.myBind(boundMaoDeObraGeralNumericStepper, "value", cliente, "comissaoMaoDeObraGeral");
		Funcoes.myBind(boundMaoDeObraGarantiaNumericStepper, "value", cliente, "comissaoMaoDeObraGarantia");
		Funcoes.myBind(boundMaoDeObraGeralGarantiaNumericStepper, "value", cliente, "comissaoMaoDeObraGeralGarantia");
		Funcoes.myBind(boundProdutosEmGarantiaNumericStepper, "value", cliente, "comissaoProdutosEmGarantia");
		Funcoes.myBind(boundProdutosNumericStepper, "value", cliente, "comissaoProdutos");
	}
	
	private function preencheCargo():void
	{
		var dictCargo:Array = MyArrayUtils.asDictionary(App.single.cache.arrayCargo);
		
		if (cliente_selecionado.idCargo > 0)
			cmbCargo.selectedItem = dictCargo[cliente_selecionado.idCargo];
		else
			cmbCargo.selectedIndex = 0;
	}
	
	private function atualizaCargo():void
	{
		cliente_selecionado.idCargo = cmbCargo.getValor().id;
	} */
	
	/**FIM COMISSÃO*/
	
	/**CONTATO*/
	
	private function usuario_adiciona_contato():void
	{
		//define a referencia da lista
		arrayc_em_edicao = arraycContatos;
		objetoOriginal = new ClienteContato();
		objetoEditando = objetoOriginal;
		binding_contato(objetoEditando);
		popup_Abre(abaContatos_popup);
	}
	private function usuario_edita_contato(ev:Event):void
	{
		//define a referencia da lista
		arrayc_em_edicao = arraycContatos;
		objetoOriginal = ev.target.data;
		objetoEditando = objetoOriginal.clone();
		binding_contato(objetoEditando);
		popup_Abre(abaContatos_popup);
	}
	private function usuario_remove_contato(ev:Event):void
	{
		var contato:ClienteContato = ev.target.data;
		var pos:int = arraycContatos.getItemIndex(contato);
		if (contato.id == 0)
			arraycContatos.removeItemAt(pos);
		else
			contato.isDeletado = !contato.isDeletado;
	}
	private function binding_contato(contato:ClienteContato):void
	{
		Funcoes.myBind(abaContatos_boundDescricaoTextInput, "text", contato, "campo");
		Funcoes.myBind(abaContatos_boundContatoTextInput, "text", contato, "valor");
		Funcoes.myBind(abaContatos_boundObsTextInput, "text", contato, "obs");
		Funcoes.myBind(abaContatos_boundTipoComboBox, "selectedItem", contato, "tipo");
	}
	
	/**FIM CONTATO*/
	
	/**ENDEREÇO*/
	
	private function usuario_adiciona_endereco():void
	{
		//define a referencia da lista
		arrayc_em_edicao = arraycEnderecos;
		objetoOriginal = new ClienteEndereco();
		objetoEditando = objetoOriginal;
		binding_endereco(objetoEditando);
		popup_Abre(abaEnderecos_popup);
	}
	private function usuario_edita_endereco(ev:Event):void
	{
		//define a referencia da lista
		arrayc_em_edicao = arraycEnderecos;
		objetoOriginal = ev.target.data;
		objetoEditando = objetoOriginal.clone();
		binding_endereco(objetoEditando);
		//
		preencheCidadeSeVazio();
		cmbUF.selectedItem = dictEstados[objetoEditando.ufIBGE];
		filtra_municipios();
		cmbMunicipio.selectedItem = dictMunicipios[objetoEditando.cidadeIBGE];
		//
		popup_Abre(abaEnderecos_popup);
	}
	private function usuario_remove_endereco(ev:Event):void
	{
		var endereco:ClienteEndereco= ev.target.data;
		var pos:int = arraycEnderecos.getItemIndex(endereco);
		if (endereco.id == 0)
			arraycEnderecos.removeItemAt(pos);
		else
			endereco.isDeletado = !endereco.isDeletado;
	}
	private function binding_endereco(endereco:ClienteEndereco):void
	{
		//Por questões de migração essa verificação se fez necessária
		if (!endereco.campo)
			endereco.campo = "";
		
		Funcoes.myBind(abaEndereco_boundNomeTextInput, "text", endereco, "campo");
		Funcoes.myBind(abaEndereco_boundLogradouroTextInput, "text", endereco, "logradouro");
		Funcoes.myBind(abaEndereco_boundNumTextInput, "text", endereco, "numero");
		Funcoes.myBind(abaEndereco_boundComplTextInput, "text", endereco, "complemento");
		Funcoes.myBind(abaEndereco_boundBairroTextInput, "text", endereco, "bairro");
		Funcoes.myBind(abaEndereco_boundCEPTextInput, "text", endereco, "cep");
		Funcoes.myBind(abaEndereco_boundTipoComboBox, "selectedItem", endereco, "tipo");
		Funcoes.myBind(abaEndereco_boundInscEstTextInput, "text", endereco, "inscr");
		Funcoes.myBind(abaEndereco_boundInscMunTextInput, "text", endereco, "inscrMun");
		Funcoes.myBind(abaEndereco_boundObsTextInput, "text", endereco, "obs");
	}
	
	/**FIM ENDEREÇO*/
	
	/**AMIGO*/
	
	private function usuario_adiciona_amigo():void
	{
		//define a referencia da lista
		arrayc_em_edicao = arraycAmigos;
		objetoOriginal = new ClienteFamiliar();
		objetoEditando = objetoOriginal;
		binding_amigo(objetoEditando);
		popup_Abre(abaAmigos_popup);
	}
	private function usuario_edita_amigo(ev:Event):void
	{
		//define a referencia da lista
		arrayc_em_edicao = arraycAmigos;
		objetoOriginal = ev.target.data;
		objetoEditando = objetoOriginal.clone();
		binding_amigo(objetoEditando);
		popup_Abre(abaAmigos_popup);
	}
	private function usuario_remove_amigo(ev:Event):void
	{
		var amigo:ClienteFamiliar= ev.target.data;
		var pos:int = arraycAmigos.getItemIndex(amigo);
		if (amigo.id == 0)
			arraycAmigos.removeItemAt(pos);
		else
			amigo.isDeletado = !amigo.isDeletado;
	}
	private function binding_amigo(amigo:ClienteFamiliar):void
	{
		Funcoes.myBind(abaAmigos_boundDescricaoTextInput, "text", amigo, "key");
		Funcoes.myBind(abaAmigos_boundNomeTextInput, "text", amigo, "nome");
		Funcoes.myBind(abaAmigos_boundTelefoneTextInput, "text", amigo, "fone");
		Funcoes.myBind(abaAmigos_boundDataNascTextInput, "text", amigo, "data");
		Funcoes.myBind(abaAmigos_boundDependenteCheckBox, "selected", amigo, "ehDependente");
		Funcoes.myBind(abaAmigos_boundAutorizadoCheckBox, "selected", amigo, "ehAutorizado");
		Funcoes.myBind(abaAmigos_boundObsTextInput, "text", amigo, "obs"); 
	}
	
	/**FIM AMIGO*/
	
	/**VEICULO*/
	
	private function usuario_adiciona_veiculo():void
	{
		//define a referencia da lista
		arrayc_em_edicao = arraycVeiculos;
		objetoOriginal = new ClienteVeiculo();
		objetoEditando = objetoOriginal;
		binding_veiculo(objetoEditando);
		popup_Abre(abaVeiculos_popup);
	}
	private function usuario_edita_veiculo(ev:Event):void
	{
		//define a referencia da lista
		arrayc_em_edicao = arraycVeiculos;
		objetoOriginal = ev.target.data;
		objetoEditando = objetoOriginal.clone();
		binding_veiculo(objetoEditando);
		popup_Abre(abaVeiculos_popup);
	}
	private function usuario_remove_veiculo(ev:Event):void
	{
		var veiculo:ClienteVeiculo= ev.target.data;
		var pos:int = arraycVeiculos.getItemIndex(veiculo);
		if (veiculo.id == 0)
			arraycVeiculos.removeItemAt(pos);
		else
			veiculo.isDeletado = !veiculo.isDeletado;
	}
	private function binding_veiculo(veiculo:ClienteVeiculo):void
	{
		Funcoes.myBind(abaVeiculos_boundNovoTextInput, "text", veiculo, "nome");
		Funcoes.myBind(abaVeiculos_boundTipoComboBox, "selectedItem", veiculo, "tipo");
		Funcoes.myBind(abaVeiculos_boundPlacaMaskedTextInput, "text", veiculo, "placaNumero");
		Funcoes.myBind(abaVeiculos_boundUFComboBox, "text", veiculo, "placaUF");
		Funcoes.myBind(abaVeiculos_boundRNTCTextInput, "text", veiculo, "regNacTranspCarga");
		Funcoes.myBind(abaVeiculos_boundChassiTextInput, "text", veiculo, "chassi");
		Funcoes.myBind(abaVeiculos_boundNumSerieMotorTextInput, "text", veiculo, "numSerieMotor");
		Funcoes.myBind(abaVeiculos_boundFranquiaTextInput, "text", veiculo, "franquia");
		Funcoes.myBind(abaVeiculos_boundAnoNumericStepper, "value", veiculo, "ano");
		Funcoes.myBind(abaVeiculos_boundMarcaTextInput, "text", veiculo, "marca");
		Funcoes.myBind(abaVeiculos_boundModeloTextInput, "text", veiculo, "modelo");
		Funcoes.myBind(abaVeiculos_boundFIPENumericStepper, "value", veiculo, "valorFIPE");
	}
	
	/**FIM VEICULO*/
	
	private function popup_Abre(p:Container):void
	{
		popup_aberto = p;
		PopUpManager.addPopUp(p, Application.application.gerenteJanelas, true);
		PopUpManager.centerPopUp(p);
	}
	private function popup_Fechar():void
	{
		PopUpManager.removePopUp(popup_aberto);
	}
	
	
	
	private function popup_Salvar():void
	{
		if (popup_aberto == abaEnderecos_popup)
		{
			objetoEditando.uf = cmbUF.selectedItem.sigla;
			objetoEditando.ufIBGE = cmbUF.selectedItem.codigo;
			objetoEditando.cidade = cmbMunicipio.selectedItem.nome;
			objetoEditando.cidadeIBGE = cmbMunicipio.selectedItem.codigo;
		}
		
		//aproveita a referencia da lista
		if (arrayc_em_edicao.contains(objetoOriginal))
			objetoOriginal.injeta(objetoEditando);
		else
		{
			objetoEditando.idCliente = _c.id
			arrayc_em_edicao.addItem(objetoEditando.clone());
		}
		
		
		popup_Fechar();
	}
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	/**************/
	private function preencheCidadeSeVazio():void
	{
		if (objetoEditando.cidadeIBGE.length<5)
		{
			objetoEditando.ufIBGE = "52";
			objetoEditando.cidadeIBGE = "5218805";
		}
	}
	
	private function filtra_municipios():void
	{
		ibge_municipios_filtrados.removeAll();
		var codUF:String = String(cmbUF.selectedItem.codigo);
		
		for each (var mun:IBGEMunicipio in ibge_municipios)
		{
			if (mun.codigoEstado==codUF)
				ibge_municipios_filtrados.addItem( mun );
		}/* 
		for (var i:int=0; i<ibge_municipios_filtrados.length; i++)
			if (ibge_municipios_filtrados[i].codigo == objetoEditando.cidadeIBGE)
			{
				cmbMunicipio.selectedItem = ibge_municipios_filtrados[i];
			} */
	}
	
	
	
	
	private function funcionarioChecked():void
	{
		/* if (!_c.ehFuncionario){
			abaFuncionario.label = "";
			abaFuncionario.enabled = false;
		}
		else{
			abaFuncionario.label = "Funcionário";
			abaFuncionario.enabled = true;
		} */
		
		/*
		if (!ehUsuario()){
			tabPermissoes.label = "";
			tabPermissoes.enabled = false;
		}
		else{
			tabPermissoes.label = "Permissões";
			tabPermissoes.enabled = true;
		}
		*/
	}
	
	private function ehUsuario():Boolean
	{
		if (_c.loginSenha == "" && _c.loginUsuario == "")
			return false;
		else
			return true;
	}
	
	private function mouseEh(target:Object, evType:String, eh:Boolean):void
	{
		if (evType==MouseEvent.MOUSE_OUT)
			target.alpha = (eh) ? 1 : .4;
		else if (evType==MouseEvent.MOUSE_OVER || evType==MouseEvent.CLICK)
			target.alpha = (eh) ? 1 : .7;
	}
	private function registraValidacoes():void
	{
		validacoes.splice(0, validacoes.length);
		registraValidacao(boundDtNascTextInput);
		
		//registraValidacao(boundDtNascTextInput);
		//registraValidacao(boundDtNascTextInput);
		//registraValidacao(boundDtNascTextInput);
		//registraValidacao(boundDtNascTextInput);
	}
	private function registraValidacao(elemento:MasterTextInput):void
	{
		validacoes.push(elemento);
	}
	private function valida():String
	{
		for each (var elemento:MasterTextInput in validacoes)
		{
			var valido:ValidationResultEvent = elemento.currentValidator.validate(null, true);//não mudar parametros

			for each (var validResult:ValidationResult in valido.results)
				return validResult.errorMessage;
		}
		return null;
	}
	
	