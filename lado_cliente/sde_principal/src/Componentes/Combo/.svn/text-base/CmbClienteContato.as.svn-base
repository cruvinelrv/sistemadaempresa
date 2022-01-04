package Componentes.Combo
{
	import Core.App;
	import Core.Utils.MeuFiltroWhere;
	
	import SDE.Entidade.ClienteContato;
	
	import mx.controls.ComboBox;

	public class CmbClienteContato extends ComboBox
	{
		private var dp:Array;
		
		public function CmbClienteContato()
		{
			super();
			dp = App.single.cache.arrayClienteContato;
			//dataProvider = null;
			//labelField = ;
			labelFunction = function(xxx:ClienteContato):String
			{
				return xxx.campo+": "+xxx.valor+", "+xxx.obs;
			}
		}
		
		private var _idCliente:Number;
		public function set idCliente(xxx:Number):void
		{
			_idCliente = xxx;
			if (xxx==0)
				dataProvider = null;
			else //if (xxx>0)
			{
				//
				var filtro:MeuFiltroWhere = new MeuFiltroWhere(dp);
				filtro.andEquals(idCliente, ClienteContato.campo_idCliente);
				dataProvider = filtro.getResultadoArraySimples();
				//
			}
		}
		public function get idCliente():Number
		{
			return _idCliente;
		}
		/*
		public function setValorId(idCliente:Number, idClienteEndereco:Number):void
		{
			this.idCliente = idCliente; 
			if (idClienteEndereco==0)
			{
				selectedIndex = -1;
				return;
			}
			selectedItem
				= new MeuFiltroWhere(dp)
				.andEquals(idClienteEndereco)
				.getResultadoArraySimples()[0];
		}
		*/
		public function getValor():ClienteContato
		{
			return selectedItem as ClienteContato;
		}
		
	}
}