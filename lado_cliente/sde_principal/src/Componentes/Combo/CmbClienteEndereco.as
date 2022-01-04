package Componentes.Combo
{
	import Core.App;
	import Core.Utils.MeuFiltroWhere;
	
	import SDE.Entidade.ClienteEndereco;
	
	import mx.controls.ComboBox;

	public class CmbClienteEndereco extends ComboBox
	{
		private var dp:Array;
		
		public function CmbClienteEndereco()
		{
			super();
			dp = App.single.cache.arrayClienteEndereco;
			//dataProvider = null;
			//labelField = ;
			labelFunction = function(xxx:ClienteEndereco):String
			{
				ClienteEndereco.campo_fone
				return xxx.campo+": "+xxx.logradouro+", "+xxx.numero+" "+xxx.cidade+"-"+xxx.uf;
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
				filtro.andEquals(idCliente, ClienteEndereco.campo_idCliente);
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
		public function getValor():ClienteEndereco
		{
			return selectedItem as ClienteEndereco;
		}
		
	}
}