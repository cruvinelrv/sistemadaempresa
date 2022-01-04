package impressoes.relatorios
{
	import org.print.Body;
	
	import impressoes.relatorios.corpos.BodyClientes;
	import impressoes.relatorios.corpos.BodyEstoques;
	import impressoes.relatorios.corpos.BodyMovsDetalhes;
	import impressoes.relatorios.corpos.BodyMovsResumo;
	import impressoes.relatorios.corpos.BodyNFE;
	
	public final class RelatorioFabricaBodies
	{
		public function getBody(tipo:String, dadosNaoConvertidos:Array):Body
		{
			var body:Body = null;
			if (tipo=="movimentacao" || tipo=="movDiario")
				body = new BodyMovsDetalhes();
			else if(tipo == "movResumo")
				body = new BodyMovsResumo();
			else if(tipo == "pessoas")
				body = new BodyClientes();
			else if(tipo == "estoques")
				body = new BodyEstoques();
			else if(tipo == "nfe")
				body = new BodyNFE();	
				
			body.dados = body.converteDados( dadosNaoConvertidos );
			return body;
		}
		
		private static var _:RelatorioFabricaBodies;
		public static function get unica():RelatorioFabricaBodies
		{
			if (_==null)
				_ = new RelatorioFabricaBodies();
			return _;
		}
		public function RelatorioFabricaBodies()
		{
		}
	}
}