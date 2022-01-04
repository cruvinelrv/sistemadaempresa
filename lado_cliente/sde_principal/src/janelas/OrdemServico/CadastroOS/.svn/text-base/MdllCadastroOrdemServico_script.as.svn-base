import Core.Alerta.AlertaSistema;
import Core.App;
import Core.Ev.EvRetornaArray;
import Core.Sessao;
import Core.Utils.Formatadores;
import Core.Utils.Funcoes;
import Core.Utils.MeuFiltroWhere;

import SDE.CamadaNuvem.NuvemCache;
import SDE.Entidade.Cliente;
import SDE.Entidade.ClienteContato;
import SDE.Entidade.ClienteEndereco;
import SDE.Entidade.Item;
import SDE.Entidade.ItemEmpEstoque;
import SDE.Entidade.ItemEmpPreco;
import SDE.Entidade.OrdemServico;
import SDE.Entidade.OrdemServico_Executor;
import SDE.Entidade.OrdemServico_Item;
import SDE.Entidade.OrdemServico_Tipo;
import SDE.Enumerador.EOrdemServicoStatus;

import flash.events.Event;
import flash.events.KeyboardEvent;
import flash.ui.Keyboard;

import img.Imagens;

import janelas.cadastro.ItemProduto2.JnlCadItemProduto;
import janelas.cadastro.Outros1.JanelaCadOutros;

import mx.collections.ArrayCollection;
import mx.containers.HBox;
import mx.controls.Alert;
import mx.controls.Label;
import mx.controls.dataGridClasses.DataGridColumn;
import mx.core.Application;
import mx.core.Container;
import mx.events.CloseEvent;
import mx.managers.PopUpManager;

	[Bindable] private var atualItem:Item = null;
	[Bindable] private var atualIEP:ItemEmpPreco = null;
	[Bindable] private var atualIEE:ItemEmpEstoque = null;
	[Bindable] private var cliente:Cliente = null;
	
	[Bindable] private var listaOSI:ArrayCollection = new ArrayCollection();
	[Bindable] private var listaExecutores:ArrayCollection = new ArrayCollection();
	
	[Bindable] private var vlrTotalBruto:Number = 0;
	[Bindable] private var vlrLiquidoSemDesconto:Number = 0;
	[Bindable] private var vlrLiquidoComDesconto:Number = 0;
	[Bindable] private var qtdEntradas:Number = 0;
	
	[Bindable] private var tipoOS:OrdemServico_Tipo;
	
	private var ordemServico:OrdemServico;
	
	private var cpf_cnjpValido:Boolean = false;
	[Bindable] private var ehEditando:Boolean;
	[Bindable] private var tipoCadastro:String;
	
	[Bindable] private var listaOrdemServicoAtualizacao:ArrayCollection = new ArrayCollection();
	[Bindable] private var arTiposOS:ArrayCollection = new ArrayCollection();
	[Bindable] private var arCamposTipoOS:ArrayCollection = new ArrayCollection();
	
	[Bindable] private var itemTipoArray:Array;
    [Bindable] private var arraycEstoques:ArrayCollection;
	
	private function preInitialize():void
	{
		itemTipoArray = [
			{label:'Serviço', sigla:'S'},
			{label:'Produto', sigla:'P'},
			{label:'Garantia', sigla:'G'},
			{label:'Serviço de Terceiros', sigla:'ST'},
			{label:'Produto de Terceiros', sigla:'PT'},
			{label:'Produto Utilizado', sigla:'PU'},
			{label:'Materia Prima', sigla:'MP'}
			];
	}
	
	private function create():void
	{
		grid.addEventListener('deleteRowItem',
			function (ev:Event):void
			{
				var obj:Object = ev.target.data;
				var osi:OrdemServico_Item = ev.target.data.ordemServicoItem;
				var pos:int = listaOSI.getItemIndex(obj);
				
				arraycEstoques = new ArrayCollection();			
				
				if (osi.id == 0)
					listaOSI.removeItemAt(pos);
				else
					osi.__removaMe = !osi.__removaMe;
				exibeTotaisPinta();
				exibeItensPinta();
			}
		);
		gridExecutores.addEventListener('deleteRowExecutor',
			function (ev:Event):void
			{
				var ose:OrdemServico_Executor = ev.target.data;
				var pos:int = listaExecutores.getItemIndex(ose);
				if (ose.id == 0)
					listaExecutores.removeItemAt(pos);
				else
					ose.__removaMe = !ose.__removaMe;
			}
		);
		grid.addEventListener('editRowItem',
			function (ev:Event):void
			{
				ehEditando = true;
				listaExecutores = new ArrayCollection();
				
				var obj:Object = ev.target.data;
				for each (var objTipoItem:Object in cmbTipoItem.dataProvider)
				{
					if (objTipoItem.sigla == obj.tipo_sigla)
						cmbTipoItem.selectedItem = objTipoItem;
				}
				
				listaExecutores = obj.executores;
				gridExecutores.dataProvider = listaExecutores;
				
				PopUpManager.addPopUp(popupEditaItem, Application.application.gerenteJanelas, true);
				PopUpManager.centerPopUp(popupEditaItem);
			}
		);
		
		for each (var campo:Container in camposTipoOS.getChildren())
		{
			arCamposTipoOS.addItem(campo);
		}
		
		rbSemContrato.selected = true;
		vs.selectedChild = etapa1;
		popupSelecionaTipoOS.parent.removeChild(popupSelecionaTipoOS);
		popupDadosCliente.parent.removeChild(popupDadosCliente);
		popupEditaItem.parent.removeChild(popupEditaItem);
		popupSelecionaOS.parent.removeChild(popupSelecionaOS);
		popupEstoque.parent.removeChild(popupEstoque);
		limpa();
	}
	
	private function lbl_fn_precoCusto(obj:Object,dgc:DataGridColumn):String
	{
		return Formatadores.unica.formataValor(obj.precoCusto,true);
	}
	
	private function novoProduto():void
	{
		Application.application.gerenteJanelas.NovaJanela(new JnlCadItemProduto(), "Cadastre seu item");
	}
	
	private function limpa():void
	{
		ordemServico = new OrdemServico();
	}
	
	private function abrepopup_estoques(estoque:Array):void
	{
		arraycEstoques = new ArrayCollection();
		for each (var iee:ItemEmpEstoque in estoque)
		{
			if(iee.qtd!=0)
			{
				var obj:Object = new Object();
				obj.item=atualItem.nome;
				obj.gradIdent=iee.identificador;
				obj.qdt=iee.qtd;
				obj.precoCusto=iee.custo;
				obj.iee=iee;
			
				arraycEstoques.addItem(obj);
			}
		}
		
		PopUpManager.addPopUp(popupEstoque, Application.application.gerenteJanelas, true);
		PopUpManager.centerPopUp(popupEstoque);
		dtGridEstoques.selectedIndex=0;
		dtGridEstoques.setFocus();
	}
	
	private function cpCliente_Change():void
	{
		
		atualItem=cpItem.itemSelecionado;
		var item:Item = cpItem.itemSelecionado;
		var cache:NuvemCache = App.single.cache;
		
		for each (var iep:ItemEmpPreco in App.single.cache.arrayItemEmpPreco)
		{
			if (iep.idItem != atualItem.id || iep.idEmp != Sessao.unica.idEmp)
				continue;
			
			atualIEP = iep;
			break;
		}
		
		var filtro:MeuFiltroWhere = new MeuFiltroWhere(cache.arrayItemEmpEstoque)
			.andEquals(item.id,ItemEmpEstoque.campo_idItem)
			.andEquals(Sessao.unica.idEmp,ItemEmpEstoque.campo_idEmp);
			
		var retornoItem:Array=filtro.getResultadoArraySimples();
		var iee:ItemEmpEstoque = retornoItem[0];
		
		if(retornoItem.length==0)
		{
			AlertaSistema.mensagem("Não existe estoque para este Ítem");
		}
		
		if(retornoItem.length==1)
		{
			atualIEE = retornoItem[0];
			nsVlrUnit.value = atualIEP.venda;
			nsQtd.value = 1;
			nsQtd.setFocus();
		}
		
		if(retornoItem.length>1)
		{
			abrepopup_estoques(retornoItem);
		}
	}
	
	private function chance_cpCliente():void
	{
		var xxx:Cliente = cpCliente.selectedItem;
		if (xxx == null)
		{
			this.cliente = null;
			return;
		}		
		cliente = xxx.clone();//clone aqui só é necessário quando formos alterar os dados
		var filtroContato:MeuFiltroWhere =
			new MeuFiltroWhere(App.single.cache.arrayClienteContato,"id")
			.And("idCliente",cliente.id);
		var filtroEndereco:MeuFiltroWhere =
			new MeuFiltroWhere(App.single.cache.arrayClienteEndereco,"id")
			.And("idCliente",cliente.id);
		var arContatos:Array = filtroContato.getResultadoArraySimples();
		var arEnderecos:Array = filtroEndereco.getResultadoArraySimples();
		cmbContato.dataProvider = arContatos;
		cmbEndereco.dataProvider = arEnderecos;
		this.ordemServico.cliente_nome = cpCliente.selectedItem.nome;
		this.ordemServico.cliente_cpf = cpCliente.selectedItem.cpf_cnpj;
		change_cmbContato();
		change_cmbEndereco();
	}
		
	private function change_cpExecutor():void
	{
		var ose:OrdemServico_Executor = new OrdemServico_Executor();
		ose.__executor = cpExecutor.selectedItem;
		ose.idClienteExecutor = ose.__executor.id;
		listaExecutores.addItem(ose); //= cpExecutor.selectedItems;
		cpExecutor.selectedItems.removeAll();
		gridExecutores.dataProvider = listaExecutores;
	}
	
	private function change_cmbTipoOS():void
	{
		tipoOS = cmbTiposOS.selectedItem as OrdemServico_Tipo;
		ordemServico.idOrdemServicoTipo  = tipoOS.id;
	}
	
	private function change_cmbContato():void
	{
		this.ordemServico.cliente_contato = cmbContato.selectedLabel;
	}
	
	private function change_cmbEndereco():void
	{
		this.ordemServico.cliente_endereco_cobranca = cmbEndereco.selectedLabel;
	}
	
	private function lblfn_cmbContato(xxx:ClienteContato):String
	{
		return xxx.campo+" - "+xxx.valor;
	}
	
	private function lblfn_cmbEndereco(xxx:ClienteEndereco):String
	{
		return xxx.logradouro +", "+ xxx.numero;
	}
	
	private function lblfn_cmbTipoOS(xxx:OrdemServico_Tipo):String
	{
		return xxx.nome;
	}
	
	private function mostraPopupSelecionaTipoOS():void
	{
		PopUpManager.addPopUp(popupSelecionaTipoOS, Application.application.gerenteJanelas, true);
		PopUpManager.centerPopUp(popupSelecionaTipoOS);
	}
	
	private function mostraPopupBuscaOS():void
	{
		var listaOrdemServico:ArrayCollection = new ArrayCollection()
		listaOrdemServico.source = App.single.cache.arrayOrdemServico;
		
		if (!listaOrdemServicoAtualizacao)
			listaOrdemServicoAtualizacao = new ArrayCollection();
		listaOrdemServicoAtualizacao.removeAll();
		
		if (this.tipoCadastro == 'alterar')
		{
			for each (var osa:OrdemServico in listaOrdemServico)
			{
				if (osa.status == EOrdemServicoStatus.em_andamento || osa.status == EOrdemServicoStatus.nao_iniciada)
					listaOrdemServicoAtualizacao.addItem(osa);
			}
		}
		else if (this.tipoCadastro == 'reabrir')
		{
			for each (var osr:OrdemServico in listaOrdemServico)
			{
				if (osr.status == EOrdemServicoStatus.finalizada)
					listaOrdemServicoAtualizacao.addItem(osr);
			}
		}
		
		
		gridOrdensServico.dataProvider = listaOrdemServicoAtualizacao;
		txtNumOSPesquisa.text = "";
		dfDataPesquisa.text = "";
		dfDataPesquisa.selectedDate = null;
		cpClientePesquisa.selectedItems.removeAll();
		
		PopUpManager.addPopUp(popupSelecionaOS, Application.application.gerenteJanelas, true);
		PopUpManager.centerPopUp(popupSelecionaOS);
	}
	
	private function mostraPopupDadosCliente():void
	{
		txtClienteNome.text = ordemServico.cliente_nome;
		txtClienteCpf_cnpj.text = ordemServico.cliente_cpf;
		txtClienteEndereco.text = ordemServico.cliente_endereco_cobranca;
		txtClienteContato.text = ordemServico.cliente_contato;
		
		PopUpManager.addPopUp(popupDadosCliente, Application.application.gerenteJanelas, true);
		PopUpManager.centerPopUp(popupDadosCliente);
	}
	
	private function mostraPopupEditaItem():void
	{
		listaExecutores = new ArrayCollection();
		PopUpManager.addPopUp(popupEditaItem, Application.application.gerenteJanelas, true);
		PopUpManager.centerPopUp(popupEditaItem);
		
		cmbTipoItem.selectedIndex = 0;
		btnConfirmaItem.setFocus();
	}
	
	private function fechaPopupSelecionaTipoOS():void
	{
		PopUpManager.removePopUp(popupSelecionaTipoOS);
		vs.selectedChild = etapa2;
		defineTipoOS();
		txtNumOS.text = defineNumeroOS();
		mkDate();
	}
	
	private function fechaPopupBuscaOS():void
	{
		PopUpManager.removePopUp(popupSelecionaOS);
	}

	private function fechaPopupClienteNaoCadastrado():void
	{
		PopUpManager.removePopUp(popupDadosCliente);
		validaCpf_cnpj(txtClienteCpf_cnpj.text);
	}
	
	private function fechaPopupEditaItem():void
	{
		PopUpManager.removePopUp(popupEditaItem);
	}
	
	private function mostraCadastroNovo():void
	{
		arTiposOS = App.single.cache.arraycOrdemServico_Tipo;
		if (arTiposOS.length == 0)
		{
			Alert.show("Não existem tipos de OS cadastradas no sistema, cadastre-as para utilizar a OS", "",4, null,
				function(ev:CloseEvent):void
				{
					Application.application.gerenteJanelas.NovaJanela(new JanelaCadOutros, "Cadastre o tipo de OS");
				}
			);
		}
		else
		{
			cmbTiposOS.dataProvider = arTiposOS;
			change_cmbTipoOS();
			mostraPopupSelecionaTipoOS();
		}
	}
	
	private function mostraCadastroAlterar():void
	{
		vs.selectedChild = etapa2;
		AlertaSistema.mensagem("Este cadastro será exibido preenchido para edição das informações da OS", true);
		populaCadastroEdicao(gridOrdensServico.selectedItem as OrdemServico);
		fechaPopupBuscaOS();
	}
	
	private function mostraCadastroReabrir():void
	{
		vs.selectedChild = etapa2;
		AlertaSistema.mensagem("Este cedastro será exibido com as informações originais da OS para reativação," + 
			" sendo que as informações podem ser alteradas", true);
		populaCadastroEdicao(gridOrdensServico.selectedItem as OrdemServico);
		fechaPopupBuscaOS();
	}
	
	private function defineNumeroOS():String
	{
		var listaOrdemServico:ArrayCollection = App.single.cache.arraycOrdemServico;
		var retorno:Number = 1;
		
		for each (var os:OrdemServico in listaOrdemServico)
		{
			if (Number(os.numOS) > retorno)
				retorno = Number(os.numOS);
		}
		return (++retorno).toString();
	}
	
	private function filtraBuscaOrdemServico(listaOrdemServico:ArrayCollection):void
	{
		var listaOrdemServicoFiltrada:ArrayCollection = new ArrayCollection(listaOrdemServico.source);
		var listaTemporaria:Array = [];
		if (cpClientePesquisa.selectedItem != null)
		{
			var cli:Cliente = cpClientePesquisa.selectedItem;
			listaTemporaria = [];
			for each (var os_porCliente:OrdemServico in listaOrdemServicoFiltrada)
			{
				if (os_porCliente.cliente_nome == cli.nome)
					listaTemporaria.push(os_porCliente);
			}
			listaOrdemServicoFiltrada = new ArrayCollection(listaTemporaria);
		}
		if (dfDataPesquisa.text != "")
		{
			listaTemporaria = [];
			for each (var os_porData:OrdemServico in listaOrdemServicoFiltrada)
			{
				var dataPesquisa:String = os_porData.dthrLancamento.substr(0,10);
				AlertaSistema.mensagem("Data: " + dataPesquisa, true);
				if (dataPesquisa == dfDataPesquisa.text)
					listaTemporaria.push(os_porData);
			}
			listaOrdemServicoFiltrada = new ArrayCollection(listaTemporaria);
		}
		if (txtNumOSPesquisa.text != "")
		{
			listaTemporaria = [];
			for each (var os_porNumero:OrdemServico in listaOrdemServicoFiltrada)
			{
				if (os_porNumero.numOS == txtNumOSPesquisa.text)
					listaTemporaria.push(os_porNumero);
			}
			listaOrdemServicoFiltrada = new ArrayCollection(listaTemporaria);
		}
		gridOrdensServico.dataProvider = listaOrdemServicoFiltrada;
	}
	
	private function limpaBuscaOrdemServico():void
	{
		cpClientePesquisa.selectedItems.removeAll();
		dfDataPesquisa.text = "";
		txtNumOSPesquisa.text = "";
		filtraBuscaOrdemServico(listaOrdemServicoAtualizacao);
	}
	
	private function populaCadastroEdicao(ordemServico:OrdemServico):void
	{
		//busca lista com os dados que serão necessários
		var listaOrdemServico_Tipo:ArrayCollection = App.single.cache.arraycOrdemServico_Tipo;
		var listaOrdemServicoItens:ArrayCollection = App.single.cache.arraycOrdemServico_Item;
		var listaExecutores:ArrayCollection = App.single.cache.arraycOrdemServico_Executor;
		var listaClientes:ArrayCollection = App.single.cache.arraycCliente;
		var listaItens:ArrayCollection = App.single.cache.arraycItem;
			
		AlertaSistema.mensagem("Executores: " + listaExecutores.length, true);
			
		//define qual o tipo de OS a ser usado de acordo com o cadastro inicial
		for each (var ost:OrdemServico_Tipo in listaOrdemServico_Tipo)
		{
			if (ost.id == ordemServico.idOrdemServicoTipo)
			{
				tipoOS = ost;
				defineTipoOS();
			}	
		}
		
		for each (var cli:Cliente in listaClientes)
		{
			if (ordemServico.idCliente != cli.id)
				continue;
			this.cliente = cli;
			break;
		}
		
		//popula dados da OS
		this.ordemServico.id = ordemServico.id;
		//this.ordemServico.idCliente = ordemServico.idCliente;
		this.ordemServico.idOrdemServicoTipo = ordemServico.idOrdemServicoTipo;
		this.ordemServico.dthrLancamento = ordemServico.dthrLancamento;
		
		this.ordemServico.status = ordemServico.status;
		if (ordemServico.status == EOrdemServicoStatus.finalizada)
		{
			txtNumOS.text = defineNumeroOS();
			dfDataInicio.selectedDate = new Date();
			dfDataPrevisao.selectedDate = new Date();
		}
		else
		{
			txtNumOS.text = ordemServico.numOS;
			dfDataInicio.text = ordemServico.dthrInicio;
			dfDataPrevisao.text = ordemServico.dthrPrevisao;
		}
		
		this.ordemServico.cliente_nome = ordemServico.cliente_nome;
		this.ordemServico.cliente_cpf = ordemServico.cliente_cpf;
		this.ordemServico.cliente_endereco_cobranca = ordemServico.cliente_endereco_cobranca;
		this.ordemServico.cliente_contato = ordemServico.cliente_contato;
		
		cpCliente.selectedItem=this.cliente;
		
		var filtroContato:MeuFiltroWhere =
			new MeuFiltroWhere(App.single.cache.arrayClienteContato,"id")
			.And("idCliente",cliente.id);
		var filtroEndereco:MeuFiltroWhere =
			new MeuFiltroWhere(App.single.cache.arrayClienteEndereco,"id")
			.And("idCliente",cliente.id);
		var arContatos:Array = filtroContato.getResultadoArraySimples();
		var arEnderecos:Array = filtroEndereco.getResultadoArraySimples();
		cmbContato.dataProvider = arContatos;
		cmbEndereco.dataProvider = arEnderecos;
		
		
					
		rbComContrato.selected = ordemServico.isContratado;
		txtDescricao.text = ordemServico.descricao;
		txtObservacoes.text = ordemServico.obs;
		
		txtVeiculo.text = ordemServico.veiculo;
		txtPlaca.text = ordemServico.placa;
		nsKilometragem.value = Number(ordemServico.kilometragem);
		txtNumMotor.text = ordemServico.numMotor;
		txtMaquina.text = ordemServico.maquina;
		txtImplAgricola.text = ordemServico.implAgricola;
		txtEquipamento.text = ordemServico.equipamento;
		txtNumSerie.text = ordemServico.numSerie;
		txtServico.text = ordemServico.servico;
		txtDefeitoReclamado.text = ordemServico.defeitoReclamado;
		txtDefeitoConstatado.text = ordemServico.defeiroConstatado;
		
		//popula itens e executores
		for each (var osi:OrdemServico_Item in listaOrdemServicoItens)
		{
			if (osi.idOrdemServico == ordemServico.id)
			{
				var obj:Object = new Object();
				this.listaExecutores = new ArrayCollection();
				
				for each (var item:Item in listaItens)
				{
					if (osi.idItem != item.id)
						continue;
					osi.__item = item;
					obj.nome = item.nome;
					obj.um = item.unidMed;
					break;
				}
				obj.ordemServicoItem = osi;
				obj.tipo_sigla = osi.tipoItem;
				obj.qtd = osi.qtd;
				obj.unit = osi.vlrUnitVendaFinal;
				obj.total = Math.round(osi.qtd * osi.vlrUnitVendaFinal*100)/100;
				obj.totalSemDesconto = Math.round(osi.qtd * osi.vlrUnitVendaInicial*100)/100;
				
				for each (var objTipoItem:Object in cmbTipoItem.dataProvider)
				{
					if (objTipoItem.sigla == obj.tipo_sigla)
						obj.tipo_label = objTipoItem.label;
				}
				
				for each (var ose:OrdemServico_Executor in listaExecutores)
				{
					if (ose.idOrdemServicoItem == osi.id)
					{
						for each (var exec:Cliente in listaClientes)
						{
							if (ose.idClienteExecutor != exec.id)
								continue;
							ose.__executor = exec;
							break;
						}
						this.listaExecutores.addItem(ose);
					}
				}
				
				obj.executores = this.listaExecutores;
				obj.qtdExecutores = this.listaExecutores.length;
				
				listaOSI.addItem(obj);
				grid.dataProvider = listaOSI;
				
				exibeItensPinta();
				exibeTotaisPinta();
			}
		}
		
	}
	
	//Define e organiza os campos que devem aparecer na OS de acordo com o tipo selecionado
	private function defineTipoOS():void
	{
		var linhaHBoxTemp:HBox = new HBox();
		linhaHBoxTemp.percentWidth = 100;
		
		camposTipoOS.removeAllChildren();
		
		for each (var campo:Container in arCamposTipoOS)
		{
			if (campo.visible)
			{
				if (linhaHBoxTemp.numChildren == 2)
				{
					var linhaHBox:HBox = new HBox();
					linhaHBox = linhaHBoxTemp;
					camposTipoOS.addChild(linhaHBox);
					linhaHBoxTemp = new HBox();
					linhaHBoxTemp.percentWidth = 100;
				}
				linhaHBoxTemp.addChild(campo);
			}
		}
		
		if (linhaHBoxTemp.numChildren > 0)
		{
			var linhaHBoxUnica:HBox = new HBox();
			linhaHBoxUnica = linhaHBoxTemp;
			camposTipoOS.addChild(linhaHBoxUnica);
		}
	}
	
	private function btnCancelar_click():void
	{
		limpaTela();
		vs.selectedChild = etapa1;
	}
	
	private function validaCpf_cnpj(cpf_cnpj:String):void
	{
		cpf_cnpj = Funcoes.LimpaCPF(cpf_cnpj);
		if (cpf_cnpj == '00000000000')
			cpf_cnjpValido = true;
		else if (cpf_cnpj.length == 11)
			cpf_cnjpValido = Funcoes.validaCpf(cpf_cnpj);
		else if (cpf_cnpj.length == 14)
			cpf_cnjpValido = Funcoes.validaCnpj(cpf_cnpj);
	}
		
	private function adicionaClienteMov():void
	{
		ordemServico.cliente_nome = txtClienteNome.text;
		ordemServico.cliente_cpf = txtClienteCpf_cnpj.text;
		ordemServico.cliente_endereco_cobranca = txtClienteEndereco.text;
		ordemServico.cliente_contato = txtClienteContato.text;
		fechaPopupClienteNaoCadastrado();
	}
	
	/***/
	
	private function get unit():Number {return nsVlrUnit.value;}
	private function get qtd():Number {return nsQtd.value;}
	private function get total():Number {return nsVlrTot.value;}
	
	private function set unit(v:Number):void {nsVlrUnit.value=v;}
	private function set qtd(v:Number):void {nsQtd.value=v;}
	private function set total(v:Number):void {nsVlrTot.value=v;}
	
	private function retornaEstoque(ev:EvRetornaArray):void
	{
		if (ev.retorno ==null)
		{
			AlertaSistema.mensagem("Item não encontrado");
			return;
		}
		
		atualItem = ev.retorno[0];
		atualIEE = ev.retorno[1];
		atualIEP = ev.retorno[2];
		
		nsVlrUnit.value = atualIEP.venda;
		
		nsQtd.value = 1;
		nsQtd.setFocus();
	}
	
	public function altereiQtdUnit():void
	{
		total = qtd*unit;
	}
	
	public function altereiTotal():void
	{
		qtd = nsQtd.value;
	}
	
	private function nsQtdKDown(ev:KeyboardEvent):void
	{
		if (ev.keyCode!=Keyboard.ENTER)
			return;
		lancarAtual();
	}
	
	private function lancarAtual():void
	{
		ehEditando = false;
		
		/**INICIO VALIDAÇÃO*/
		
		var msg:String = "";
		
		if (atualItem == null)
			msg += "Seleciona um Item\n";
		if (nsQtd.value == 0)
			msg += "Informe a Quantidade\n";
		if (nsVlrUnit.value == 0)
			msg += "Informe o Valor do item";
		
		if (!msg == "")
		{
			AlertaSistema.mensagem(msg);
			return;
		}
		
		/**FIM VALIDAÇÃO*/
		mostraPopupEditaItem();
	}
	
	private function lanca_inner(osiLancado:OrdemServico_Item):void
	{
		var executores:ArrayCollection = new ArrayCollection();
		
		for each (var xxx:OrdemServico_Executor in listaExecutores.source)
		{
			executores.addItem(new OrdemServico_Executor(xxx));
		}
		
		osiLancado.id = 0;
		//osiLancado.idMov = 0;
		
		var obj:Object = {};
		obj.ordemServicoItem = osiLancado;
		obj.nome = osiLancado.__item.nome;
		obj.um = osiLancado.__item.unidMed;
		obj.tipo_sigla = cmbTipoItem.selectedItem.sigla;
		obj.tipo_label = cmbTipoItem.selectedItem.label;
		obj.tipo_obj = cmbTipoItem.selectedItem;
		obj.executores = executores;
		obj.qtdExecutores = executores.length;
		obj.qtd = osiLancado.qtd;
		obj.unit = osiLancado.vlrUnitVendaFinal;
		obj.total = Math.round(osiLancado.qtd * osiLancado.vlrUnitVendaFinal*100)/100;
		obj.totalSemDesconto = Math.round(osiLancado.qtd * osiLancado.vlrUnitVendaInicial*100)/100;
		listaOSI.addItem(obj);
		grid.dataProvider = listaOSI;			
	}
	
	private function btnEditaItem_click():void
	{
		if (ehEditando)
		{
			grid.selectedItem.executores = listaExecutores;
			grid.selectedItem.qtdExecutores = listaExecutores.length;
			grid.selectedItem.tipo_sigla = cmbTipoItem.selectedItem.sigla;
			grid.selectedItem.tipo_label = cmbTipoItem.selectedItem.label;
			grid.selectedItem.tipo_obj = cmbTipoItem.selectedItem;
			
			grid.dataProvider = grid.dataProvider;
			gridExecutores.dataProvider = null;
		}
		else
		{
			//var mie:MovItemEstoque = new MovItemEstoque();
			//mie.idIEE = atualIEE.id;
			//mie.identificador = atualIEE.identificador;
			//mie.qtd = qtd;
			var osi:OrdemServico_Item = new OrdemServico_Item();
			osi.idItem = atualItem.id;
			osi.idIEE = atualIEE.id;
			osi.idIEP = atualIEP.id;
			osi.__item = atualItem;
			//osi.__oSIEstoques = [mie];
			osi.item_nome = atualItem.nome;
			osi.estoque_identificador = atualIEE.identificador;
			osi.rf_unica = atualItem.rfUnica;
			osi.rf_auxiliar = atualItem.rfAuxiliar;
			osi.unid_med = atualItem.unidMed;
			osi.qtd = qtd;
			osi.vlrUnitVendaInicial = atualIEP.venda;
			osi.vlrUnitVendaFinal = unit;
			osi.vlrUnitVendaFinalQtd = osi.vlrUnitVendaFinal * osi.qtd;
			
			lanca_inner(osi);
			limpaAtual();
			
			gridExecutores.dataProvider = null;
		}
			
		fechaPopupEditaItem();
		exibeItensPinta();
		cpItem.txtPesquisaBox.text='';
	}
	
	private function limpaAtual():void
	{
		atualItem = null;
		atualIEE = null;
		atualIEP = null;
		nsQtd.value = 0;
		nsVlrUnit.value = 0;
		nsVlrTot.value = 0;
		qtd = 0;
		unit = 0;
		total = 0;
		cpExecutor.selectedItems.removeAll();
		exibeTotaisPinta();
	}
	
	private function limpaTela():void
	{
		txtNumOS.text = '';
		dfDataInicio.text = '';
		dfDataPrevisao.text = '';
		cpCliente.selectedItems.removeAll();
		cmbContato.dataProvider = null;
		cmbEndereco.dataProvider = null;
		rbSemContrato.selected = true;
		txtDescricao.text = '';
		txtObservacoes.text = '';
		//nsDescontoPct.value = 0;
		//nsDescontoVlr.value = 0;
		//nsDescontoVlrTotal.value = 0;
		
		txtVeiculo.text = '';
		txtPlaca.text = '';
		nsKilometragem.value = 0;
		txtNumMotor.text = '';
		txtMaquina.text = '';
		txtImplAgricola.text = '';
		txtEquipamento.text = '';
		txtNumSerie.text = '';
		txtServico.text = '';
		txtDefeitoReclamado.text = '';
		txtDefeitoConstatado.text = '';
		
		limpaAtual();
		limpa();
		
		listaOSI.removeAll();
		listaExecutores.removeAll();
		itens.removeAllChildren();
		exibeTotaisPinta();
	}
	
	private function exibeItensPinta():void
	{
		itens.removeAllChildren();
		
		for each (var o:Object in listaOSI)
		{
			var hb:HBox = null;
			var lbValor:Label = null;
			
			hb = itens.getChildByName(o.tipo_sigla) as HBox;
			
			if (hb==null)
			{
				var lbTipo:Label = new Label();
				lbValor = new Label();
				hb = new HBox();
				hb.name = o.tipo_sigla;
				lbTipo.width = 250;
				lbValor.name = 'valor';
				
				lbTipo.text = "Total de " + o.tipo_label + ":";
				lbValor.text = Formatadores.unica.formataValor(o.totalSemDesconto,true);
				
				hb.addChild(lbTipo);
				hb.addChild(lbValor);
				itens.addChild(hb);
			}
			else
			{
				lbValor = hb.getChildByName('valor') as Label;
				var index:int = hb.getChildIndex(lbValor);
				var novoValor:Number = Number(lbValor.text.replace('R$',''));
				novoValor += Number(o.totalSemDesconto);
				lbValor.text = Formatadores.unica.formataValor(novoValor,true);
				
				hb.removeChildAt(index);
				hb.addChild(lbValor);
			}
		}
	}
		
	private function exibeTotaisPinta():void
	{
		vlrTotalBruto = 0;
		vlrLiquidoSemDesconto = 0;
		vlrLiquidoComDesconto = 0;
		
		for each (var o:Object in listaOSI)
		{
			var osi:OrdemServico_Item = o.ordemServicoItem;
			vlrTotalBruto += osi.vlrUnitVendaInicial * osi.qtd;
			vlrLiquidoSemDesconto += osi.qtd * osi.vlrUnitVendaInicial;
			vlrLiquidoComDesconto += osi.vlrUnitVendaFinal * osi.qtd;
		}
		
		//nsDescontoVlr.maximum = vlrLiquidoSemDesconto; //não deixa dar desconto além do valor total
		//nsDescontoVlrTotal.maximum = vlrLiquidoSemDesconto; //não deixa dar desconto além do valor total
		//vlrLiquidoComDesconto = vlrLiquidoSemDesconto - nsDescontoVlr.value;
		
		txtVlrBruto.text = Formatadores.unica.formataValor(vlrTotalBruto,true);
		lbVlrLiquidoSemDesconto.text = Formatadores.unica.formataValor(vlrLiquidoSemDesconto,true);
		txtVlrLiquidoComDesconto.text = Formatadores.unica.formataValor(vlrLiquidoComDesconto,true);
		txtVlrAcrDesc.text = Formatadores.unica.formataValor(vlrLiquidoComDesconto - vlrTotalBruto,true);
		
		//nsDescontoVlrTotal.value = vlrLiquidoComDesconto;
		
		qtdEntradas = listaOSI.length;
		grid.dataProvider = listaOSI;
	}
	
	private function usuario_escolheu_estoque():void
	{
		PopUpManager.removePopUp(popupEstoque);
		sistema_define_estoque(dtGridEstoques.selectedItem.iee as ItemEmpEstoque);
	}
	
	private function usuario_fecha_popup_estoques():void
	{
		PopUpManager.removePopUp(popupEstoque);
		//sistema_limpa_item(true);
	}
	
	private function usuario_fecha_popup_buscaOS():void
	{
		PopUpManager.removePopUp(popupSelecionaOS);
		//sistema_limpa_item(true);
	}
	
	private function sistema_define_estoque(estoque:ItemEmpEstoque):void
	{
		atualIEE = estoque;
		nsVlrUnit.value = atualIEP.venda;
		nsQtd.value = 1;
		nsQtd.setFocus();
	}
	
	private function dfDataInicio_valueCommit():void
	{
		if (!dfDataPrevisao)
			return;
				
		var data:Date = dfDataInicio.selectedDate;
		dfDataPrevisao.selectableRange =
		{
			rangeStart:dfDataInicio.selectedDate
		};
		
		if (dfDataInicio.selectedDate.getTime() > dfDataPrevisao.selectedDate.getTime())
			dfDataPrevisao.selectedDate = dfDataInicio.selectedDate;
	}
	
	/*
	private function keyDownDescontoFinal(ev:KeyboardEvent):void
	{
		if (ev.keyCode==Keyboard.ENTER)
			focusOutDescontoFinal(ev);
	}
	
	/*
	private function focusOutDescontoFinal(ev:Event):void
	{
		if (ev.currentTarget == nsDescontoVlr)
		{
			nsDescontoPct.value = 100 * nsDescontoVlr.value / vlrLiquidoSemDesconto;
			nsDescontoVlrTotal.value = vlrLiquidoSemDesconto - nsDescontoVlr.value;
		}
		else if (ev.currentTarget == nsDescontoPct)
		{
			nsDescontoVlr.value = nsDescontoPct.value * vlrLiquidoSemDesconto / 100;
			nsDescontoVlrTotal.value = vlrLiquidoSemDesconto - nsDescontoVlr.value;
		}
		else if (ev.currentTarget == nsDescontoVlrTotal)
		{
			nsDescontoVlr.value = vlrLiquidoSemDesconto - nsDescontoVlrTotal.value;
			nsDescontoPct.value = 100 * nsDescontoVlr.value / vlrLiquidoSemDesconto;
		}
		
		exibeTotaisPinta();
	}
	*/