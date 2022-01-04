package impressoes.relatorios.modelos
{
	import mx.charts.chartClasses.NumericAxis;
	public final class ModeloMov
	{
		public function ModeloMov()
		{
		}
		
        public var id:Number = 0;
        public var dthrMov:String = "";
        public var dtEntSai:String = "";
        public var funcionario:String = "";
        public var tipo:String = "";
        public var nota:String = "";
        
        public var vlrProduto: String = "";
        public var vlrTotal:String = "";
        public var vlrDesc: String = "";
        public var percDesc: String = "";
        
        //Calculo Impostos
        public var bcICMS:String="";
        public var vlrICMS:String="";
        public var bcICMSSubst:String="";
        public var vlrICMSSubst:String="";
        public var vlrFrete:String="";
        public var vlrSeguro:String="";
        public var vlrOutros:String="";
        public var vlrIPI:String="";
        
        public var cliente:ModeloCliente = null;
        public var itens:Array = null;
	}
}