package impressoes.relatorios.modelos
{
	public class ModeloNFE
	{
		public function ModeloNFE()
		{
		}
		
		//dadso da nfe
		public var itens:Array;
		public var mMov:ModeloMov = null;
		
		public var cli:ModeloCliente = null;
		public var cliEmp:ModeloCliente = null;
		
		public var numero:String = "";
		public var serie:String = "";
		public var chaveAcesso:String = "";
		public var infAdicional:String = "";
		public var fatura:String = "";
		//dados trasnporte
		public var cliTranp:ModeloCliente = null;
		public var natOperacao:String = "";
		public var tipoMov:String = "0";
		public var tipoTransp:String = "";		
		public var placa:String = "";
		public var placaUF:String = "";
		public var rntc:String = "";
		//dados volume
		public var volQtd:String ="0";
		public var volEspecie:String ="";
		public var volMarca:String ="";
		public var volNumero:String ="0";
		public var volPesoB:String ="0";
		public var volPesoL:String ="0";
		
	}
}