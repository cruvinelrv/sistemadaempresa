package Core.Utils
{
	import Core.Alerta.AlertaSistema;
	
	
	public class MeuFiltroWhere
	{
		private var _campoPK:String;
		private var _espacoAmostral:Array = [];
		private var _condicoes:Array = [];
		
		
		private var OPERADOR_IGUAL:String = "==";
		private var OPERADOR_MAIOR:String = ">";
		
		
		/**
		 * Lembre-se que o array retornado é um dicionário
		 * 
		 * */
		public function getResultadoDict():Array
		{
			var retorno:Array = [];
			for each(var itemSource:Object in _espacoAmostral)
			{
				for each(var itemWhere:Array in _condicoes)
				{
					var campo:String = String(itemWhere[0]);
					var operador:Object = Object(itemWhere[1]);
					var valorComparativo:Object = Object(itemWhere[2]);
					
					if (itemSource[campo]==valorComparativo)
					{
						var valorPK:Object = itemSource[_campoPK];
						retorno[String(_campoPK+valorPK)] = itemSource;
						//hack de array:
						//chaves numéricas, geram itens nulos no array
						//chaves de texto, não alteram a largura
						retorno.length++; 
						
						//retorno.push(itemSource);
					}
				}
			}
			return retorno;
		}
		public function getResultadoArraySimples():Array
		{
			var retorno:Array = [];
			for each(var amostra:Object in _espacoAmostral)
			{
				var valido:Boolean = false;
				
				//for each(var condicoes:Array in _condicoes)
				for (var iCondicao:int=0; iCondicao<_condicoes.length; iCondicao++)
				{
					var condicao:Array = _condicoes[iCondicao];
					var campo:String = String(condicao[0]);
					var operador:Object = Object(condicao[1]);
					var valorComparativo:Object = Object(condicao[2]);
					var amostra_campo:Object = amostra[campo];
					
					var v1:Object = amostra_campo;
					var v2:Object = valorComparativo;
					
					if (operador==OPERADOR_IGUAL)
						valido = (v1==v2);
					else if(operador==OPERADOR_MAIOR)
						valido = (v1>v2);
					
					if (iCondicao>0)
						AlertaSistema.mensagem( campo+" | "+ v1+" "+operador+" "+v2+" é "+valido, true);
					
					if (!valido)
						break;
				}
				if (valido)
					retorno.push(amostra);
			}
			return retorno;
		}
		
		public function MeuFiltroWhere(source:Array,campoPK:String="id")
		{
			_espacoAmostral = source;
			_campoPK = campoPK;
		}
		public function And(campo:String, valor:Object):MeuFiltroWhere
		{
			_condicoes.push([campo, OPERADOR_IGUAL, valor]);
			return this;
		}
		public function andEquals(valor:Object,campo:String="id"):MeuFiltroWhere
		{
			_condicoes.push([campo, OPERADOR_IGUAL, valor]);
			return this;
		}
		public function andGreater(valor:Number,campo:String):MeuFiltroWhere
		{
			_condicoes.push([campo, OPERADOR_MAIOR, valor]);
			return this;
		}
	}
}