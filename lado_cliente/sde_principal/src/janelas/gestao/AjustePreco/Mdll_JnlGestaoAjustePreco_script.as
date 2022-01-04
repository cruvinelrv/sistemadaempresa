
import Core.Alerta.AlertaSistema;
import Core.App;

import SDE.Entidade.Cad_Marca;
import SDE.Entidade.Cad_Secao;

import mx.core.Container;

	[Bindable] public var marcas:Array = null;
	[Bindable] public var secoes:Array = null;
	
	private function init():void{
		secoes = [];
		for each (var o:Cad_Secao in App.single.cache.arrayCad_Secao)
		{
			secoes.push(o);
			o.__orderBy = o.secao;
			if (o.grupo!="")
				o.__orderBy+=", "+o.grupo;
			if (o.subgrupo!="")
				o.__orderBy+=", "+o.subgrupo;
		}
		secoes.sortOn("__orderBy");
		marcas = [];
		for each (var o2:Cad_Marca in App.single.cache.arrayCad_Marca)
		{
			marcas.push(o2);
			o2.__orderBy = o2.marca;
			if (o2.modelo!="")
				o2.__orderBy+=", "+o2.modelo;
		}
		marcas.sortOn("__orderBy");
	}
	
	private function createMarca():void{
		cmbMarca.dataProvider = marcas;
	}
	
	private function createSecao():void{
		cmbSecao.dataProvider = secoes;
	}
	
	private function mudaTela(container:Container):void{
		this.selectedChild = container;
	}
	
	private function btnConfirmaAjusteMarca_Click():void{
		if (nsAjusteMarca.value == 0){
			AlertaSistema.mensagem("O valor deve ser diferente de 0");
			return;
		}
		
		App.single.n.modificacoes.ItemPrecoAjusteMarca((cmbMarca.selectedItem as Cad_Marca).id, nsAjusteMarca.value,
			function ():void{
				AlertaSistema.mensagem("Ajuste realizado.");
				limpaMarca();
			}
		);
	}
	
	private function btnConfirmaAjusteSecao():void{
		if (nsAjusteSecao.value == 0){
			AlertaSistema.mensagem("O valor deve ser diferente de 0");
			return;
		}
		
		App.single.n.modificacoes.ItemPrecoAjusteSecao((cmbSecao.selectedItem as Cad_Secao).id, nsAjusteSecao.value,
			function ():void{
				AlertaSistema.mensagem("Ajuste realizado.");
				limpaSecao();
			}
		);
	}
	
	private function btnVoltarDeMarca_Click():void{
		mudaTela(telaMenu);
		limpaMarca();
	}
	
	private function btnVoltaDeSecao_Click():void{
		mudaTela(telaMenu);
		limpaSecao();
	}
	
	private function limpaMarca():void{
		cmbMarca.selectedIndex = 0;
		nsAjusteMarca.value = 0;
	}
	
	private function limpaSecao():void{
		cmbSecao.selectedIndex = 0;
		nsAjusteSecao.value = 0;
	}