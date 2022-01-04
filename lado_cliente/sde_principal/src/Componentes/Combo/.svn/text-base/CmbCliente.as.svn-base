package Componentes.Combo
{
	import Core.App;
	import Core.Utils.MeuFiltroWhere;
	
	import SDE.Entidade.Cliente;
	
	import mx.controls.ComboBox;

	public class CmbCliente extends ComboBox
	{
		private var dp:Array;
		
		public function CmbCliente()
		{
			super();
			dp = App.single.cache.arrayCliente;
			dataProvider = dp;
			labelField = Cliente.campo_nome;
		}
		
		private var _tipo:String;
		[Inspectable(category="SisEmpresa", enumeration="Todos,Funcionarios", defaultValue="Todos")]
		public function set tipo(v:String):void
		{
			_tipo = v;
			if (v=="Todos")
				dataProvider = dp;
			else if (v=="Funcionarios")
			{
				//
				var filtro:MeuFiltroWhere = new MeuFiltroWhere(dp);
				filtro.andEquals(true, Cliente.campo_ehFuncionario);
				dataProvider = filtro.getResultadoArraySimples();
				//
			}
		}
		public function get tipo():String
		{
			return _tipo;
		}
		
		public function setValorId(idXXX:Number):void
		{
			if (idXXX==0)
			{
				selectedIndex = 0;
				return;
			}
			selectedItem
				= new MeuFiltroWhere(dp)
				.andEquals(idXXX)
				.getResultadoArraySimples()[0];
		}
		public function getValor():Cliente
		{
			return selectedItem as Cliente;
		}
		
	}
}