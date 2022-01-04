package Componentes.CP
{
	import Core.App;
	
	import SDE.Entidade.Cliente;
	import SDE.Entidade.Finan_Conta;
	import SDE.Entidade.Finan_TipoDocumento;
	import SDE.Entidade.Finan_Titulo;
	import SDE.Enumerador.EContaTipo;
	import SDE.Enumerador.ETipoTitulo;
	
	import mx.collections.ArrayCollection;
	
	public class PesqFinanTitulo extends SuperCP
	{
		public function PesqFinanTitulo()
		{
			//TODO: implement function
			super();
		}
		
		private function myLabelFunction_Cheque(obj:Object):String
		{
			return obj.id +" - "+ obj.cliente +" - "+ obj.numCheque +" - "+ obj.dtPagamento;
		}
		
		public function somenteCheque():ArrayCollection
		{
			var dp:ArrayCollection = new ArrayCollection();
			for each (var titulo:Finan_Titulo in App.single.cache.arrayFinan_Titulo)
			{
				var finanTipoDocumento:Finan_TipoDocumento = new Finan_TipoDocumento();
				if (titulo.tipo == ETipoTitulo.cheque_a_receber)
					finanTipoDocumento = App.single.cache.getFinan_TipoDocumento(titulo.idTipoDocumento);
				else
					continue;
				
				if (finanTipoDocumento.nome == "CHEQUE")
				{
					if (!titulo.isBaixado && !titulo.isAlterado && !titulo.isDevolvido2)
					{
						var cli:Cliente = App.single.cache.getCliente(titulo.idClienteAPagar);
						var obj:Object = new Object();
						obj.id = titulo.id;
						obj.dtPagamento = titulo.dtPagamento;
						obj.cliente =  (cli.nome == "")?cli.apelido_razsoc:cli.nome;//App.single.cache.getCliente(titulo.idClienteAPagar).nome;
						obj.numCheque = titulo.numCheque;
						obj.valor = titulo.valorCobrado;
						dp.addItem(obj);
					}
				} 
			}
			
			camposPesquisa = ["id", "dtPagamento", "cliente", "numCheque"];
			browserFields = camposPesquisa;
		    labelFunction = myLabelFunction_Cheque;
		    
		    return dp;
		}
		
		public function somenteCheque_Compensacao():ArrayCollection
		{
			var dp:ArrayCollection = new ArrayCollection();
			for each (var titulo:Finan_Titulo in App.single.cache.arrayFinan_Titulo)
			{
				if (titulo.tipo == ETipoTitulo.cheque_a_receber)
					var finanTipoDocumento:Finan_TipoDocumento= App.single.cache.getFinan_TipoDocumento(titulo.idTipoDocumento);
				else
					continue;
				
				if (finanTipoDocumento.nome == "CHEQUE")
				{
					if (titulo.isBaixado && !titulo.isAlterado && !titulo.isDevolvido2 && titulo.idContaDestino != 0)
					{
						var finanConta:Finan_Conta = App.single.cache.getFinan_Conta(titulo.idContaDestino);
						if (finanConta.tipo == EContaTipo.Banco)
						{
							var cli:Cliente = App.single.cache.getCliente(titulo.idClienteAPagar);
							var obj:Object = new Object();
							obj.id = titulo.id;
							obj.dtPagamento = titulo.dtPagamento;
							obj.cliente =  (cli.nome == "")?cli.apelido_razsoc:cli.nome; //App.single.cache.getCliente(titulo.idClienteAPagar).nome;
							obj.numCheque = titulo.numCheque;
							obj.valor = titulo.valorCobrado;
							dp.addItem(obj);
						}
					}
				}
			}
			camposPesquisa = ["id", "dtPagamento", "cliente", "numCheque"];
			browserFields = camposPesquisa;
		    labelFunction = myLabelFunction_Cheque;
			
			return dp;
		}
	}
}