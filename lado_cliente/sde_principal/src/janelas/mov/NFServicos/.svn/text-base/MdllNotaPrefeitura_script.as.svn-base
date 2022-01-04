// ActionScript file
import Core.Alerta.AlertaSistema;
import Core.Ev.EvRetornaArray;
import Core.Sessao;
import Core.Utils.Constantes;
import Core.Utils.Formatadores;

import SDE.Entidade.CFOP;
import SDE.Entidade.Cliente;
import SDE.Entidade.ClienteEndereco;
import SDE.Entidade.Item;
import SDE.Entidade.ItemEmpEstoque;
import SDE.Entidade.ItemEmpPreco;
import SDE.Entidade.Mov;
import SDE.Entidade.MovItem;
import SDE.Entidade.MovItemEstoque;
import SDE.FachadaServico.FcdMov;

import flash.events.Event;
import flash.net.URLRequest;
import flash.net.navigateToURL;

import mx.core.Application;
import mx.formatters.DateFormatter;
import mx.managers.PopUpManager;


	[Bindable] private var atualItem:Item = null;
	[Bindable] private var atualIEP:ItemEmpPreco = null;
	[Bindable] private var atualIEE:ItemEmpEstoque = null;
	[Bindable] private var cliente:Cliente = null;
	[Bindable] private var vendedor:Cliente = null;
	[Bindable] private var funcionario:Cliente = null;
	[Bindable] private var cfop:CFOP = null;
	
	[Bindable] private var listaMI:Array = [];
	
	private var ttUnitI:Number = 0;
	private var ttUnitF1:Number = 0;
	private var ttUnitF2:Number = 0;
	
	[Bindable] private var idMovImportada:Number = 0;
	private var tipoMovImportada:String = '';
	
	[Bindable] private var isCpfValido:Boolean = false;
	
	private var resumo:String = '';
	private var tipo:String = '';
	private var impressao:String = '';
	private var tipoNfs:String = '';
	
	private var rRequisicao:URLRequest;
	private const URL_DMS:String = "http://www.rioverde.go.gov.br/dms/dmsresponsavel.html";
	
	private var mov:Mov = new Mov();
	
	private function create():void
	{
		
		criaCmbData(3);
		
		cpCliente.addEventListener(EvRetornaArray.RETORNO,
			function(ev:EvRetornaArray):void
			{
				if (ev.retorno!=null)
				{
					cliente = ev.retorno[0];
					cmbEndereco.dataProvider = cliente.__enderecos;
					mov.cliente_cpf = cliente.cpf_cnpj;
					mov.cliente_nome = (cliente.apelido_razsoc=="")?cliente.nome:cliente.apelido_razsoc;
					change_cmbEndereco();
				}
			}
		);
		cpVendedor.addEventListener(EvRetornaArray.RETORNO,
			function(ev:EvRetornaArray):void
			{
				if (ev.retorno!=null)
					vendedor = ev.retorno[0];
			}
		);
		/* cpFuncionario.addEventListener(EvRetornaArray.RETORNO,
			function(ev:EvRetornaArray):void
			{
				if (ev.retorno!=null)
					funcionario = ev.retorno[0];
			}
		); */
		/* cpCfop.addEventListener(EvRetornaArray.RETORNO,
			function(ev:EvRetornaArray):void
			{
				if(ev.retorno!=null)
					cfop = ev.retorno[0];
			}
		); */
		grid.addEventListener('deleteRow',
			function(ev:Event):void
			{
				var obj:Object = ev.target.data;
		    	//remove da grid
		    	var pos:int = listaMI.indexOf(obj);
		    	listaMI.splice(pos, 1);
		    	//atualiza o dataprovider
		    	grid.dataProvider = listaMI;
		    	gridNFSe.dataProvider = grid.dataProvider;
		    	exibeTotaisPinta();
			}
		);
		gridNFSe.addEventListener('tomadorChecked',
			function(ev:Event):void
			{
				gridNFSe.selectedItem.recolhidoPeloTomador =
					(gridNFSe.selectedItem.recolhidoPeloTomador) ? false : true;
			}
		);
		
		popupDadosCliente.parent.removeChild(popupDadosCliente);
		
		limpaTela();
		exibeTotaisPinta();
	}
	
	private function change_cpCFOP():void
	{
		/* cfop = cpCFOP.selectedItem; AQUI*/
	}
	
	private function change_cmbEndereco():void
	{
		this.mov.cliente_endereco_faturamento = cmbEndereco.selectedLabel;
	}
	
	private function mostraPopupDadosCliente():void
	{
		txtClienteNome.text = this.mov.cliente_nome;
		rbFis.selected = (this.cliente.tipo == "Fisica");
		txtCPFCNPJ.text = this.mov.cliente_cpf;
		txtClienteEndereco.text = this.mov.cliente_endereco_faturamento;
		
		PopUpManager.addPopUp(popupDadosCliente, Application.application.gerenteJanelas, true);
		PopUpManager.centerPopUp(popupDadosCliente);
	}
	
	private function fechaPopupDadosCliente():void
	{
		PopUpManager.removePopUp(popupDadosCliente);
	}
	
	private function limpaTela():void
	{
		cpVendedor.pesquisaInternaID( Sessao.unica.idClienteFuncionarioLogado );
		cpCliente.pesquisaInternaCPF( Constantes.unica.cpf_cliente_consumidor );
		/* cpCFOP.selectedItems.removeAll(); AQUI*/
		nsDescontoFinal.value = 0;
		nsDescontoFinalpct.value = 0;
		vs.selectedIndex = 0;
		idMovImportada=0;
		cfop = null;
		funcionario = null;
		txtNumNota.text = '';
		txtSerieNota.text = '';
		txtDataEmissao.text = '';
		nsISSQN.value = 0;
		nsINSS.value = 0;
		ckbNoMun.selected = true;
		ckbForaMun.selected = false;
		txtFatura.text = '';
		txtObs.text = '';
		tipoMovImportada='';
		criaCmbData(3);
		cmbTipoServico.selectedIndex = 0;
		cmbTipoDeclaracao.selectedIndex = 0;
		cmbSituacao.selectedIndex = 0;
		nsAliquota.value = 0;
		limpaAtual();
		//txtBarras.setFocus();
	}
	
	private function criaCmbData(variacao:int):void
	{
		var ar:Array = [];
		var fmt:DateFormatter = new DateFormatter();
		for (var i:int=-variacao; i < variacao; i++)
		{
			var d:Date = new Date();
			d.month+=i;
			fmt.formatString = "MM / YYYY";
			ar.push(fmt.format(d));
		}
		cmbDataReferencia.dataProvider = ar;
		cmbDataReferencia.selectedIndex = variacao;
	}
	
	private function recolhidoTomador_click(ehRecolhido:Boolean):void
	{
		for each (var obj:Object in listaMI)
		{
			obj.recolhidoPeloTomador = ehRecolhido;
		}
		grid.dataProvider = listaMI;
		gridNFSe.dataProvider = grid.dataProvider;
	}
	
	private function txtBarrasKDown(ev:KeyboardEvent):void
	{
		if (ev.keyCode!=Keyboard.ENTER)
			return;
		if (txtBarras.text.length<3)
			return;
		
		//pesquisa Cod Barras
		FcdMov.unica.LoadItemEstoque(
			Sessao.unica.idEmp,
			txtBarras.text,
			function(retorno:Array):void
			{
				if (retorno==null)
				{
					AlertaSistema.mensagem("Não encontrado");
					txtBarras.setSelection(0 , txtBarras.text.length);
					return;
				}
				atualItem = retorno[0];
				atualIEE = retorno[1];
				atualIEP = retorno[2];
				lblItemAtual.text = atualItem.nome+", "+atualIEE.identificador;
				
				cmbUM.selectedItem = atualItem.unidMed;
				nsQtd.value=1;
				nsVlr.value = atualIEP.venda;
				
				txtBarras.text='';
				nsQtd.setFocus();
			}
		);
		
	}
	
	private function retornaEstoque(ev:EvRetornaArray):void
	{
		if (ev.retorno == null)
		{
			AlertaSistema.mensagem("Item não encontrado");
			return;
		}
		atualItem	= ev.retorno[0];
		atualIEE	= ev.retorno[1];
		atualIEP	= ev.retorno[2];
		lblItemAtual.text = atualItem.nome+", "+atualIEE.identificador;
		
		cmbUM.selectedItem = atualItem.unidMed;
		nsQtd.value=1;
		nsVlr.value = atualIEP.venda;
		
		txtBarras.text='';
		nsQtd.setFocus();
	}
	
	private function retornaMov(ev:EvRetornaArray):void
	{
		var mov:Mov = ev.retorno[0];
		
		listaMI.splice(0, listaMI.length);
		//grid.dataProvider = listaMI;
		
		idMovImportada = mov.id;
		tipoMovImportada=mov.tipo;
		//cbCancelarImportada.selected=false;
		
		for each (var mi:MovItem in mov.__mItens)
		{
			/*
			mi.qtd;
			mi.vlrUnitVendaInicial;
			mi.vlrUnitVendaFinal;
			*/
			
			lanca_inner( mi );
		}
		
		limpaAtual();
		
	}
	
	private function lblfn_endereco(end:ClienteEndereco):String
	{
		return end.logradouro +", "+ end.numero;
	}
	
	
	
	
	
	
	/** getters e setters */
	
	private var acrescimo:Number = 0;
	
	private function get unit():Number {return nsVlr.value;}
	private function get qtd():Number {return nsQtd.value;}
	private function get total():Number {return nsVlrTot.value;}
	
	private function set unit(v:Number):void {nsVlr.value=v;}
	private function set qtd(v:Number):void {nsQtd.value=v;}
	private function set total(v:Number):void {nsVlrTot.value=v;}
	
	public function altereiQtdUnit():void
	{
		total = qtd*unit;
		_altereiValor();
	}
	public function altereiTotal():void
	{
		qtd = nsQtd.value;
		unit = total/qtd;
		_altereiValor();
	}
	private function _altereiValor():void
	{
		acrescimo = 0;
		lblAcrescimo.text="";
		try
		{
			
			acrescimo = (unit - atualIEP.venda) * qtd;
			if (acrescimo>0)
				lblAcrescimo.text = "Acrescimo: "+Formatadores.unica.formataValor(acrescimo, true);
			else if (acrescimo<0)
				lblAcrescimo.text = "Desconto: "+Formatadores.unica.formataValor(-acrescimo, true);
			
		}
		catch(err:Error)
		{
		}
	}
	
	
	
	private function nsQtdKDown(ev:KeyboardEvent):void
	{
		if (ev.keyCode!=Keyboard.ENTER)
			return;
		lancarAtual();
	}
	private function lancarAtual():void
	{
		if (atualItem==null)
		{
			AlertaSistema.mensagem("Selecione um Produto");
			return;
		}
		if (qtd==0)
		{
			AlertaSistema.mensagem("Quantidade não pode ser zero");
			return;
		}
		if (unit==0)
		{
			AlertaSistema.mensagem("Valor não pode ser zero");
			return;
		}
		
		
		//lanca_inner(   parametros   );
		
		
		
		
		//define aliq
		//var iea:ItemEmpAliq = new ItemEmpAliq(); //atualItem.__alSaiDentro;//atualItem.__alSaiFora;
		//mid
		var mie:MovItemEstoque = new MovItemEstoque();
		mie.idIEE = atualIEE.id;
		//mie.__iee = atualIEE;
		mie.identificador = atualIEE.identificador;
		mie.qtd = qtd;
		//mie.idEmp = Sessao.unica.idEmp;
		
		
		//Cria Mov Item
		var mi:MovItem = new MovItem();
		mi.idItem = atualItem.id;
		mi.__item = atualItem;
		mi.__mIEstoques = [mie];
		
		mi.qtd = qtd;
		mi.idClienteFuncionario = Sessao.unica.idClienteFuncionarioLogado;
		
		
		//mi.vlr...
		
		//valores reais
		//compra original, custo original, venda original, venda digitado pelo usuário
		mi.vlrUnitCompra = atualIEP.compra;
		mi.vlrUnitCusto = atualIEP.custo;
		mi.vlrUnitVendaInicial = atualIEP.venda;
		mi.vlrUnitVendaFinal = unit;
		
		
		lanca_inner(   mi   );
		
		/** Limpar */
		limpaAtual();
	}
	
	private function lanca_inner( miLancado:MovItem ):void
	{
		
		miLancado.id=0;
		miLancado.idMov=0;
		miLancado.idClienteFuncionario = vendedor.id;
		
		//joga na lista para o grid
		var o:Object = {};
		o.movItem = miLancado;
		o.qtd = miLancado.qtd;
		o.unidMed = miLancado.__item.unidMed;
		o.nome = miLancado.__item.nome;
		o.unit = miLancado.vlrUnitVendaFinal;
		o.total = miLancado.qtd * miLancado.vlrUnitVendaFinal;
		o.recolhidoPeloTomador = false;
		listaMI.push(o);
		grid.dataProvider = listaMI;
		gridNFSe.dataProvider = grid.dataProvider;
		
	}
	
	private function limpaAtual():void
	{
		atualItem= null;
		atualIEE = null;
		atualIEP = null;
		lblItemAtual.text="";
		cmbUM.selectedIndex = 0;
		txtBarras.text="";
		txtBarras.setFocus();
		unit=0;
		qtd=0;
		exibeTotaisPinta();
	}
	
	private function exibeTotaisPinta():void
	{
		//var ttQtd:Number = 0;
		ttUnitI = 0;
		ttUnitF1 = 0;
		ttUnitF2 = 0;
		
		for each(var o:Object in listaMI)
		{
			var mi:MovItem = o.movItem;
			//ttQtd   += mi.qtd;
			ttUnitI += mi.qtd * mi.vlrUnitVendaInicial;
			ttUnitF1 += mi.qtd * mi.vlrUnitVendaFinal;
		}
		
		nsDescontoFinal.maximum = ttUnitF1;//não deixa dar desconto além
		ttUnitF2 = ttUnitF1 - nsDescontoFinal.value;
		
		txtMostraTotalBru.text = Formatadores.unica.formataValor(ttUnitI, true);
		txtMostraTotalLiq1.text = Formatadores.unica.formataValor(ttUnitF1, true);
		txtMostraTotalLiq2.text = Formatadores.unica.formataValor(ttUnitF2, true);
		txtMostraTotalAcr.text = Formatadores.unica.formataValor(ttUnitF2-ttUnitI, true);
		
		//não podemos exibir mensagens em caso de valor zerado
		//pois esse método é chamado de vários lugares
		//só cria seletores para valor > 0
		seletorEspecies.limpar( ttUnitF2 );
		
		boxFechamento1.enabled = (listaMI.length>0);
		boxFechamento2.enabled = boxFechamento1.enabled;
		grid.dataProvider = listaMI;
		gridNFSe.dataProvider = grid.dataProvider;

	}
	
	private function keyDownDescontoFinal(ev:KeyboardEvent):void
	{
		if (ev.keyCode==Keyboard.ENTER)
			focusOutDescontoFinal(ev);
	}
	private function focusOutDescontoFinal(ev:Event):void
	{
		AlertaSistema.mensagem(ev.currentTarget.name);
		
		if (ev.currentTarget == nsDescontoFinal)
		{
			nsDescontoFinalpct.value = 100 * nsDescontoFinal.value / ttUnitF1;
		}
		else if (ev.currentTarget == nsDescontoFinalpct)
		{
			nsDescontoFinal.value = nsDescontoFinalpct.value * ttUnitF1 / 100;
		}
		exibeTotaisPinta();
	}
	
	private function fn_abreUrlDms(url:String):void
	{
		rRequisicao = new URLRequest(url);
		navigateToURL(rRequisicao, '_blank');
	}
	
	
	
	
	
	
	
	
	