
	import Core.Alerta.AlertaSistema;
	import Core.FoldPopup.FoldPopup;
	import Core.FoldPopup.GerenteFoldPopup;
	
	import SDE.Entidade.Item;
	import SDE.FachadaServico.FcdItem;
	import SDE.Parametro.ParamFiltroItem;
	import SDE.Parametro.ParamLoadItem;
	
	import flash.events.Event;
	import flash.events.KeyboardEvent;
	import flash.ui.Keyboard;
	
	import mx.events.FlexEvent;
	
	private var fcd:FcdItem = new FcdItem();
	private var retornoAutomatico:Boolean = true;
	
	private var fRetorno1:Function = null;
	private var paramFiltro:ParamFiltroItem;
	private var paramLoad:ParamLoadItem;
	
	private function get gerente():GerenteFoldPopup
	{
		return FoldPopup.gerente;
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
		gerente.fecha();
		fRetorno1([gridRet.selectedItem.item]);
    }
	private function txt_KEnter():void
	{
		if (paramFiltro==null)
			paramFiltro=new ParamFiltroItem();
		retornoAutomatico = false;
		paramFiltro.texto=txt.text;
		Pesquisa();
	}
	
	public function setParametros(paramFiltro:ParamFiltroItem, paramLoad:ParamLoadItem, fRetorno1:Function):void
	{
		this.retornoAutomatico=true;
		this.paramFiltro = paramFiltro;
		this.paramLoad = paramLoad;
		this.fRetorno1=fRetorno1;
		this.addEventListener(FlexEvent.INITIALIZE, _create_add_filtro);
	}
	
	private function _create_add_filtro(e:FlexEvent):void
	{
		ckbProduto.selected = this.paramFiltro.produto;
		ckbServico.selected = this.paramFiltro.servico;
		
		if (this.paramFiltro.produto || this. paramFiltro.servico)
		{
			ckbProduto.enabled = false;
			ckbServico.enabled = false;
		}
	}
	
	public function Pesquisa():void
    {
    	paramFiltro.produto = ckbProduto.selected;
    	paramFiltro.servico = ckbServico.selected;
    	
    	/*
    	if (ckbProduto.selected)
			paramFiltro.tipo = EItemTipo.produto;
		else if (ckbServico.selected)
			paramFiltro.tipo = EItemTipo.servico;
		else
			paramFiltro.tipo = EItemTipo.nao_informado;
			*/
		
    	paramLoad.precos=true;
    	
    	fcd.Pesquisa(1,  paramFiltro, paramLoad,
			function(retorno:Array):void
			{
				if (retorno.length==0 && retornoAutomatico)
					fRetorno1(null);
				else if (retorno.length==1 && retornoAutomatico)
				{
					gerente.fecha();
					fRetorno1(retorno);
					//gerenciador.fecha();
				}
				else
				{
					FoldPopup.gerente.mostra();
					
					var ar:Array = [];
					for each(var item:Item in retorno)
					{
						var o:Object = {};
						o.item = item;
						o.idItem = item.id;
						o.nome = item.nome
						o.rfUnica = item.rfUnica;
						o.rfAuxiliar = item.rfAuxiliar;
						o.secao = item.secao;
						/*
						o.grupo = item.grupo;
						o.subgrupo = item.subgrupo;
						/**/
						o.marca = item.marca;
						
						o.venda = item.__ie.__preco.venda;
						//o.modelo = item.modelo;
						o.unidMed = item.unidMed;
						o.tipo = item.tipo;
						ar.push(o);
					}
					
					gridRet.dataProvider = ar;
				}	
			}
		);
    }