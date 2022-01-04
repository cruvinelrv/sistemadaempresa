package Componentes.CaixaPesquisa.CP
{
	import Componentes.CaixaPesquisa.SuperCaixaPesquisaTxt;
	import Componentes.PopUp.Pesquisas.ConteudoPesquisaCliente;
	
	import Core.FoldPopup.FoldPopup;
	
	import SDE.Entidade.Cliente;
	import SDE.FachadaServico.FcdCliente;
	import SDE.Parametro.ParamFiltroCliente;
	import SDE.Parametro.ParamLoadCliente;
	
	public final class CPesqCliente extends SuperCaixaPesquisaTxt
	{
		public function CPesqCliente()
		{
			var cor:uint = 0x66aa66;
			
			super(cor);
		}
		
		public function set btnLabel(v:String):void
		{
			btn.label = v;
		}
		
		public var pFiltro:ParamFiltroCliente = new ParamFiltroCliente();
		public var pLoad:ParamLoadCliente = new ParamLoadCliente();
		
		override protected function definePesquisa():void
		{
			var conteudo:ConteudoPesquisaCliente = new ConteudoPesquisaCliente();
			conteudo.setParametros(pFiltro, pLoad, fRetorno);
			FoldPopup.gerente.setConteudo(conteudo, fCancela);
			pFiltro.texto = txt.text;
		}
		
		public function pesquisaInternaCPF(cpf:String):void
		{
			var fcd:FcdCliente = new FcdCliente();
			var pf:ParamFiltroCliente = new ParamFiltroCliente();
			pf.cpf = cpf;
			fcd.Pesquisa(pf, null,
				function(retorno:Array):void
				{
					if (retorno.length>1)
						retorno.splice(1, retorno.length-1);
					//array com apenas um resultado
					fRetorno(retorno);
				}
			);
		}
		
		public function pesquisaInternaID(idCliente:Number):void
		{
			var fcd:FcdCliente = new FcdCliente();
			fcd.Load(idCliente, null,
				function(retorno:Cliente):void
				{
					fRetorno([retorno]);
				}
			);
			
		}
		
	}
}