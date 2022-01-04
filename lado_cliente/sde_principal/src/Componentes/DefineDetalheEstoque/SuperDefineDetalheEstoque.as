package Componentes.DefineDetalheEstoque
{
	import mx.containers.Box;

	public class SuperDefineDetalheEstoque extends Box
	{
		public function SuperDefineDetalheEstoque()
		{
			super();
			percentHeight = 100;
			percentWidth = 100;
		}
		
		[Bindable] public var strIdent:String;
		
		public function getResultado():String
		{
			return getResultado_impl();
		}
		
		protected function getResultado_impl():String
		{
			return new String();
		}
	}
}