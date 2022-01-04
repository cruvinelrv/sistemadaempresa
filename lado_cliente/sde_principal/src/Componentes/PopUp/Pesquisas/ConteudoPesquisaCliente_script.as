
	import Core.FoldPopup.FoldPopup;
	import Core.FoldPopup.GerenteFoldPopup;
	
	import SDE.Entidade.Cliente;
	import SDE.Enumerador.EPesTipo;
	import SDE.FachadaServico.FcdCliente;
	import SDE.Parametro.ParamFiltroCliente;
	import SDE.Parametro.ParamLoadCliente;
	
	import flash.events.KeyboardEvent;
	import flash.ui.Keyboard;
	
	import mx.events.FlexEvent;
	
	private var fcd:FcdCliente = new FcdCliente();
	private var retornoAutomatico:Boolean = true;
	
	private var fRetorno1:Function = null;
	private var paramFiltro:ParamFiltroCliente;
	private var paramLoad:ParamLoadCliente;
	
	
	
	
	private function create():void
	{
		doBinding();
	}
	private function doBinding():void
	{
		/*
		Funcoes.myBind( cbFuncionario, "selected", paramFiltro, "funcionario" );
		Funcoes.myBind( cbFornecedor, "selected", paramFiltro, "fornecedor" );
		*/
	}
	/*
	private function checkbox_Click(ev:Event):void
	{
		
		if (ev.currentTarget == cbFisica && cbFisica.selected)
			cbJuridica.selected=false;
		if (ev.currentTarget == cbJuridica && cbJuridica.selected)
			cbFisica.selected=false;
		
		cbMasculino.enabled = cbFisica.selected;
		cbFeminino.enabled = cbFisica.selected;
		
		if (!cbFisica.selected)
		{
			cbMasculino.selected = false;
			cbFeminino.selected = false;
		}
		
	}
	/**/
	
	
	
	
	
	
	
	private function get gerente():GerenteFoldPopup
	{
		return FoldPopup.gerente;
	}
	
	private function txt_KEnter():void
	{
		if (paramFiltro==null)
			paramFiltro=new ParamFiltroCliente();
		/*
		AlertaSistema.mensagem( paramFiltro.fornecedor+'' );
		AlertaSistema.mensagem( paramFiltro.funcionario+'' );
		*/
		//já que vc pesquisou com txt_Enter, então você está com a janela ativa
		retornoAutomatico = false;
		paramFiltro.texto=txt.text;
		Pesquisa();
	}
	
	private function grid_kdown(ev:KeyboardEvent):void
	{
		if (ev.keyCode==Keyboard.ENTER)
			grid_dclick();
	}
	
    private function grid_dclick():void
    {
    	if (gridRet.selectedItem==null)
    		return;
		gerente.fecha(true);
		fRetorno1([gridRet.selectedItem.cliente]);
    }
	
	public function setParametros(paramFiltro:ParamFiltroCliente, paramLoad:ParamLoadCliente, fRetorno1:Function):void
	{
		this.retornoAutomatico=true;
		this.paramFiltro = paramFiltro;
		this.paramLoad = paramLoad;
		this.fRetorno1=fRetorno1;
		this.addEventListener(FlexEvent.CREATION_COMPLETE, _create_add_filtro);
	}
	private function _create_add_filtro(e:FlexEvent):void
	{
		ckbParceiro.selected = this.paramFiltro.parceiro;
		ckbFornecedor.selected = this.paramFiltro.fornecedor;
		ckbFuncionario.selected = this.paramFiltro.funcionario;
		ckbTransportador.selected = this.paramFiltro.transportador;
		ckbFisica.selected = (this.paramFiltro.tipo==EPesTipo.Fisica);
		ckbJuridica.selected = (this.paramFiltro.tipo==EPesTipo.Juridica);
		
		ckbParceiro.enabled = !this.paramFiltro.parceiro;
		ckbFornecedor.enabled = !this.paramFiltro.fornecedor;
		ckbFuncionario.enabled = !this.paramFiltro.funcionario;
		ckbTransportador.enabled = !this.paramFiltro.transportador;
		if (this.paramFiltro.tipo!=EPesTipo.nao_informado)
		{
			ckbFisica.enabled = false;
			ckbJuridica.enabled = false;
		}
	}
	
	public function Pesquisa():void
    {
		paramFiltro.parceiro = ckbParceiro.selected;
		paramFiltro.fornecedor = ckbFornecedor.selected;
		paramFiltro.funcionario = ckbFuncionario.selected;
		paramFiltro.transportador = ckbTransportador.selected;
		if (ckbFisica.selected)
			paramFiltro.tipo = EPesTipo.Fisica;
		else if (ckbJuridica.selected)
			paramFiltro.tipo = EPesTipo.Juridica
		else
			paramFiltro.tipo = EPesTipo.nao_informado;
		
    	gridRet.dataProvider=null;
    	fcd.Pesquisa(paramFiltro, paramLoad,
			function(retorno:Array):void
			{
				if (retorno.length==0 && retornoAutomatico)
					fRetorno1(null);
				else if (retorno.length==1 && retornoAutomatico)
				{
					gerente.fecha(true);
					fRetorno1(retorno);
					//gerenciador.fecha();
				}
				else
				{
					FoldPopup.gerente.mostra();
					
					var ar:Array = [];
					for each(var c:Cliente in retorno)
					{
						var o:Object = {};
						o.cliente = c;
						o.idCliente = c.id;
						o.nome = c.nome;
						o.apelido = c.apelido_razsoc;
						o.cpf = c.cpf_cnpj;
						o.tipo = c.tipo;
						o.dtNasc = c.dtNasc;
						ar.push(o);
					}
					gridRet.dataProvider = ar;
				}	
			}
		);
    }