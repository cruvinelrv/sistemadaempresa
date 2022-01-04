package Core
{
	import Core.Parametrizacao.GerenteParametrizacao;
	
	import SDE.Nuvens;
	
	import flash.events.EventDispatcher;
	
	import mx.core.Application;
	
	public final class Sessao extends EventDispatcher
	{
		//private static var _:Sessao = new Sessao();
		public static function get unica():Sessao{return Application.application.sessao;}
		
		
		
		//private var _nuvens:Nuvens = new Nuvens();
		//public function get nuvens():Nuvens{return _nuvens;}
		public var nuvens:Nuvens = new Nuvens();
		
		
		public function Sessao()
		{
			//parametrizacao = new GerenteParametrizacao(App.single);
		}
		//[Bindable]
		[Bindable] public var modoTecnico:Boolean = false;
		[Bindable] public var modo:String;
		[Bindable] public var versao:String;
		
		public function get parametrizacao():GerenteParametrizacao
		{
			return App.single.gerParam;
		}
		
		
		
		
		public function logar(idCorp:Number, idEmp:Number, idClienteFuncionarioLogado:Number):void
		{
			_idCorp = idCorp;
			_idEmp = idEmp;
			_idClienteFuncionarioLogado = idClienteFuncionarioLogado;
		}
		public function logOut():void
		{
			nuvens.Fecha();
			Application.application.sessao = new Sessao();
		}
		
		public function get isLogado():Boolean
		{
			return idClienteFuncionarioLogado>0;
		}
		
		
		
		
		
		private var _idCorp:Number=0;
		public function get idCorp():Number{return _idCorp;}
		private var _idEmp:Number=0;
		public function get idEmp():Number{return _idEmp;}
		private var _idClienteFuncionarioLogado:Number=0;
		public function get idClienteFuncionarioLogado():Number{return _idClienteFuncionarioLogado;}
		
		

	}
}