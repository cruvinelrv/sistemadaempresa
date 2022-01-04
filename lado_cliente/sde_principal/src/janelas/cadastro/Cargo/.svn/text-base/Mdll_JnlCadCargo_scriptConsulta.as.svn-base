
import Core.Alerta.AlertaSistema;

import SDE.Entidade.Cargo;

import mx.collections.ArrayCollection;

	[Bindable] private var dpCargo:ArrayCollection = new ArrayCollection();
	
	private function create():void{
		cpCargo_KeyDown();
		popupNovoCargo.parent.removeChild(popupNovoCargo);
	}
	
	private function cpCargo_KeyDown():void{
		if (cpCargo.dropDown)
			cpCargo.dropDown.visible = false;
		dpCargo = cpCargo.dataProvider as ArrayCollection;
	}
	
	private function btnSelecionar_Click():void{
		if (!dgCargo.selectedItem){
			AlertaSistema.mensagem("Selecione um cargo");
		}
		populaCargo((dgCargo.selectedItem as Cargo).clone());
		mudaTela(telaTab);
		vs.selectedIndex = 0;
		//mudaTela(telaNovoAltera);
	}