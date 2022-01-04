package Core.Utils
{
	import SDE.Enumerador.EDirecao;
	import SDE.Enumerador.EPlanoContaTipo;
	import SDE.Outros.PlanoContaTipo;
	
	[Bindable]
	public final class FlyWeightOutros
	{
		private static var _:FlyWeightOutros;
		public static function get unica():FlyWeightOutros
		{
			if (_==null)
				_ = new FlyWeightOutros();
			return _;
		}
		public function FlyWeightOutros()
		{
			dictTiposPlanoConta[EPlanoContaTipo.Im.toString()] = new PlanoContaTipo(
				{direcao:EDirecao.nenhum,tipo:EPlanoContaTipo.Im,nome:"Imobilizado"}
			);
			dictTiposPlanoConta[EPlanoContaTipo.DF.toString()] = new PlanoContaTipo(
				{direcao:EDirecao.saida,tipo:EPlanoContaTipo.DF,nome:"Despesa Fixa"}
			);
			dictTiposPlanoConta[EPlanoContaTipo.DV.toString()] = new PlanoContaTipo(
				{direcao:EDirecao.saida,tipo:EPlanoContaTipo.DV,nome:"Despesa Variável"}
			);
			dictTiposPlanoConta[EPlanoContaTipo.RF.toString()] = new PlanoContaTipo(
				{direcao:EDirecao.entrada,tipo:EPlanoContaTipo.RF,nome:"Receita Fixa"}
			);
			dictTiposPlanoConta[EPlanoContaTipo.RV.toString()] = new PlanoContaTipo(
				{direcao:EDirecao.entrada,tipo:EPlanoContaTipo.RV,nome:"Receita Variável"}
			);
			
			
			//reinsere
			
			for each (var xxx:PlanoContaTipo in dictTiposPlanoConta)
			{
				listTiposPlanoConta.push(xxx);
			}
			
			
			
		}
		
		
		public var listTiposPlanoConta:Array = [];
		public var dictTiposPlanoConta:Array = [];
		
		
		
		private var _EPlanoContasTipos:Array = ['Im','RF','RV','DF','DV'];
		
		public function EPlanoContaTipo_ToNumber(tipo:String):Number
		{
			return _EPlanoContasTipos.indexOf(tipo)+1;
		}
		public function EPlanoContaTipo_FromNumber(tipo:Number):String
		{
			return _EPlanoContasTipos[tipo-1];
		}
		
		
		
		
		
		
		
		
		
		
		
		
		
		
		
		
	}
}