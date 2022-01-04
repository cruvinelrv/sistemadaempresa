package pesquisas
{
	import Core.App;
	
	import SDE.Entidade.Cliente;
	import SDE.Entidade.ClienteFuncionarioUsuario;
	
	public class PesquisaUsuario
	{
		public function PesquisaUsuario()
		{
		}
		
		public static function pesquisar(searchStr:String):Array
		{
			var arrayUsuarios:Array = [];
			var source:Array = App.single.cache.arrayClienteFuncionarioUsuario;
			for each (var usuario:ClienteFuncionarioUsuario in source)
			{
				var arrayStringPesquisas:Array = searchStr.split(' ');
				var funcionario:Cliente = App.single.cache.getCliente(usuario.idCliente);
				var arrayValorPesquisas:Array = [usuario.id, usuario.login, funcionario.nome];
				var contador:Number = 0;
				for each (var strPesq:String in arrayStringPesquisas)
				{
					for each (var strValor:String in arrayValorPesquisas)
					{
						if (strValor == null)
							continue;
						if (strValor.search(strPesq.toUpperCase()) > -1)
						{
							contador++;
							break;
						}
					}
				}
				if (contador == arrayStringPesquisas.length)
					arrayUsuarios.push(usuario.clone());
			}
			return arrayUsuarios;
		}
	}
}