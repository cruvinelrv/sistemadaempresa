// ActionScript file
import Core.Alerta.AlertaSistema;
import Core.FoldPopup.FoldPopup;
import Core.FoldPopup.GerenteFoldPopup;
import Core.Sessao;

import SDE.Entidade.CFOP;

import flash.events.KeyboardEvent;
import flash.ui.Keyboard;

import mx.collections.ArrayCollection;

	private var retornoAutomatico:Boolean = true;
	private var fRetorno1:Function = null;
	
	private var listaFiltrada:ArrayCollection = new ArrayCollection();
	
	private function get gerente():GerenteFoldPopup
	{
		return FoldPopup.gerente;
	}
	
	private function create():void
	{
		Pesquisa();
	}
	
	private function txt_KEnter():void
	{
		retornoAutomatico = false;
		Pesquisa();
	}
	
	public function setParametros(fRetorno1:Function):void
	{
		this.fRetorno1 = fRetorno1;
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
		fRetorno1([gridRet.selectedItem]);
    }
    
    private function txt_keyUp():void
    {
    	var textoPesquisando:String = txt.text;
		var listaTodos:Array = Sessao.unica.nuvens.cache.arrayCFOP;
		
		//var MAX_RESULTS:int = 15;
		
		listaFiltrada.removeAll();
		for (var i:int = 1; i < listaTodos.length; i++)
		{
			var cfop:CFOP = listaTodos[i] as CFOP;
			if ((cfop.codigo.indexOf(textoPesquisando)>=0) || (cfop.descricao.indexOf(textoPesquisando)>=0))//contains  -- indexof('a')>0
				listaFiltrada.addItem(cfop);
		}
		gridRet.dataProvider=listaFiltrada;
    }
    
    public function Pesquisa():void
    {
    	txt_keyUp();
    }