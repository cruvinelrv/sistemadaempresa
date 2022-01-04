package pesquisas
{
	import Core.App;
	
	import SDE.Entidade.Cliente;
	
	public class PesquisaCliente
	{
		public function PesquisaCliente()
		{
		}
		
		public static function pesquisar(searchStr:String):Array
		{
			var arrayClientes:Array = [];
			var source:Array = App.single.cache.arrayCliente;
			for each (var cliente:Cliente in source){
				if (!cliente.ehInativo){
					var arrayStringPesquisas:Array = searchStr.split(' ');
					var arrayValorPesquisas:Array = [
						cliente.id.toString(), cliente.nome, cliente.apelido_razsoc,
						cliente.cpf_cnpj 
						];
					var contador:Number = 0;
					for each (var strPesq:String in arrayStringPesquisas){
						for each (var strValor:String in arrayValorPesquisas){
							if (strValor == null)
								continue;
							if (strValor.search(strPesq.toUpperCase()) > -1){
								contador++;
								break;
							}
						}
					}
 					if (contador == arrayStringPesquisas.length)
						arrayClientes.push(cliente.clone());
				}
			}
			
			return arrayClientes;
		}
		
		public static function pegar(idCliente:Number):Cliente
		{
			var retorno:Cliente = null;
			var source:Array = App.single.cache.arrayCliente;
			for each (var cliente:Cliente in source)
			{
				if (idCliente != cliente.id)
					continue;
				retorno = cliente.clone();
				break;
			}
			return retorno;
		}

	}
}