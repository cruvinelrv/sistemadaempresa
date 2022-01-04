package Core.Parametrizacao
{
	import Core.App;
	
	import SDE.Entidade.SdeConfig;
	
	public class GerenteParametrizacao
	{
		private var app:App;
		public function GerenteParametrizacao(app:App)
		{
			this.app=app;
		}
		
		/**
		 * THIAGO AGO/10
		 * Funcao App.single.ss.parametrizacao.sim
		 * para simplificar o uso de parametros booleanos
		 * */
		
		public function sim(variavel:String, idEmp:Number=0, idClienteFuncionarioLogado:Number=0):Boolean
		{
			return this.getParametro(variavel, idEmp, idClienteFuncionarioLogado)=="1";
		}
		
		
		public function getParametro(variavel:String, idEmp:Number=0, idClienteFuncionarioLogado:Number=0):*
		{
			var variaveis_todas:Array = App.single.cache.arraySdeConfig;
			var ret:* = null;
			
			//tipo...
			//corp:0
			//emp:1
			//do 2 ao 9 podemos por niveis intermediarios
			//usuario:10
			
			variaveis_todas.sortOn(SdeConfig.campo_tipo);
			for each(var c:SdeConfig in variaveis_todas)
			{
				if (c.variavel==variavel)
					ret = c.valor;
			}
			
			//a idéia é que ele retorne o ultimo
			
			return ret;
		}
	}
}











