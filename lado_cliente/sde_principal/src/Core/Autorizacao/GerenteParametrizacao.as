package Core.Autorizacao
{
	import Core.Sessao;
	
	import SDE.Entidade.SdeConfig;
	
	public class GerenteParametrizacao
	{
		private var ss:Sessao;
		public function GerenteParametrizacao(ss:Sessao)
		{
			this.ss=ss;
		}
		
		public function getAutorizacao(variavel:String):String
		{
			var variaveis_todas:Array = ss.nuvens.cache.SdeConfig;
			var ret:* = null;
			
			variaveis_todas.sortOn(SdeConfig.campo_tipo);
			for each(var c:SdeConfig in variaveis_todas)
			{
				if (c.variavel==variavel)
					ret = c.valor;
			}
			
			return ret;
		}
		
	}
}