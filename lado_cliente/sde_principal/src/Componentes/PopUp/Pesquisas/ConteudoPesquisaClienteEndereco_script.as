
	import Core.Alerta.AlertaSistema;
	import Core.FoldPopup.FoldPopup;
	import Core.FoldPopup.GerenteFoldPopup;
	
	import SDE.Entidade.Cliente;
	import SDE.FachadaServico.FcdCliente;
	import SDE.Parametro.ParamFiltroCliente;
	import SDE.Parametro.ParamLoadCliente;
	
	import flash.events.KeyboardEvent;
	import flash.ui.Keyboard;
	
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
	}
	
	public function Pesquisa():void
    {
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
						o.nome = c.__pessoa.nome;
						o.apelido = c.__pessoa.apelido_razsoc;
						o.cpf = c.__pessoa.cpf_cnpj;
						o.tipo = c.__pessoa.tipo;
						o.dtNasc = c.__pessoa.dtNasc;
						ar.push(o);
					}
					gridRet.dataProvider = ar;
				}	
			}
		);
    }