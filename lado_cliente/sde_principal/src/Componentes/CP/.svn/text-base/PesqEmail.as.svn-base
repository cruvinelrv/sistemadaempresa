package Componentes.CP
{
	import Core.App;
	
	import SDE.Entidade.ClienteContato;
	import SDE.Enumerador.EContatoTipo;
	
	import mx.collections.ArrayCollection;
	
	public class PesqEmail extends SuperCP
	{
		public function PesqEmail()
		{
			super();
			camposPesquisa = ["clienteNome", "email"];
			browserFields = ["id", "idCliente", "clienteNome", "email"];
			labelFunction = myLabelFunction;
		}
		
		private function myLabelFunction(obj:Object):String
		{
			return obj.email;
		}
		
		public function setCliente(idCliente:Number):ArrayCollection
		{
			var dp:ArrayCollection = new ArrayCollection();
			for each (var cc:ClienteContato in App.single.cache.arrayClienteContato)
			{
				if (cc.tipo == EContatoTipo.email && cc.idCliente == idCliente)
				{
					var obj:Object = new Object();
					obj.id = cc.id;
					obj.idCliente = cc.idCliente;
					obj.clienteNome = App.single.cache.getCliente(cc.idCliente).nome;
					obj.email = cc.valor;
					dp.addItem(obj);
				}
			}
			prompt = "Selecione um e-mail("+dp.length+")"
			return dp;
		}
	}
}