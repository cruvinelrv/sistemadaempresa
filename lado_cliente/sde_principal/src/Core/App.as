package Core
{
	import Core.Janelas.GerenteJanelas;
	import Core.Parametrizacao.GerenteParametrizacao;
	
	import SDE.CamadaNuvem.NuvemCache;
	import SDE.CamadaNuvem.NuvemModificacoes;
	import SDE.Nuvens;
	
	import mx.core.Application;
	
	public class App
	{
		private static var _:App = new App();
		public static function get single():App{return App._;}
		
		public function App()
		{
			//if (App._)//!=null
			//	throw new Error("Uso Incorreto do AppFacade");
			gerParam = new GerenteParametrizacao(this);
			gerJan = Application.application.gerenteJanelas;
		}
		
		public var gerParam:GerenteParametrizacao = null;
		public var gerJan:GerenteJanelas = null;
		
		
		public function get ss():Sessao
		{
			return Sessao.unica;
		}
		public function get n():Nuvens
		{
			return ss.nuvens;
		}
		public function get cache():NuvemCache
		{
			return n.cache;
		}
		public function get mod():NuvemModificacoes
		{
			return n.modificacoes;
		}
		
		
		public function get idCorp():Number{return ss.idCorp;}
		public function get idEmp():Number{return ss.idEmp;}
		public function get idClienteFuncionarioLogado():Number{return ss.idClienteFuncionarioLogado;}
		
		
	}
}