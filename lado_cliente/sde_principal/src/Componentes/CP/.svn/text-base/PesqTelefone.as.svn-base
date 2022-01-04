package Componentes.CP
{
	import Core.App;
	
	import SDE.Entidade.ClienteContato;
	import SDE.Enumerador.EContatoTipo;
	
	import mx.collections.ArrayCollection;
	
	public class PesqTelefone extends SuperCP
	{
		public function PesqTelefone()
		{
			super();
			camposPesquisa = [ClienteContato.campo_valor];
			browserFields = camposPesquisa;
			labelFunction = myLabelFunction;
		}
		
		public function myLabelFunction(cc:ClienteContato):String
		{
			if (cc.tipo == EContatoTipo.celular)
				return "Celular: " + cc.valor;
			else
				return "Telefone Fixo: " + cc.valor;
		}
		
		public function setCliente(idCliente:Number):ArrayCollection
		{
			var dp:ArrayCollection = new ArrayCollection();
			for each (var cc:ClienteContato in App.single.cache.arrayClienteContato)
			{
				if (cc.idCliente == idCliente)
					if (cc.tipo == EContatoTipo.celular || cc.tipo == EContatoTipo.fone_fixo)
						dp.addItem(cc);
			}
			prompt = "Selecione um telefone("+dp.length+")";
			return dp;
		}
	}
}