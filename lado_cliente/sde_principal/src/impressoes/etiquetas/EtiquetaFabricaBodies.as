package impressoes.etiquetas
{
	import impressoes.etiquetas.corpos.Body3Colunas;
	import impressoes.etiquetas.corpos.Body3ColunasValusa;
	
	import mx.controls.Alert;
	
	import org.print.Body;
	
	public class EtiquetaFabricaBodies
	{
		public function EtiquetaFabricaBodies()
		{
		}
				
		public function getBody(tipo:String, dados:Array):Body
		{
			var body:Body = null;
			if (tipo == "sofistique")
			{
				body = new Body3Colunas();
			}
			else if( tipo == "modelo01")
			{
				body = new Body3ColunasValusa();
			}
			
			body.dados = body.converteDados( dados );
			body["desenha"]();
			return body;
		}
		
		private static var _:EtiquetaFabricaBodies;
		public static function get unica():EtiquetaFabricaBodies
		{
			if (_==null)
				_ = new EtiquetaFabricaBodies();
			return _;
		}

	}
}