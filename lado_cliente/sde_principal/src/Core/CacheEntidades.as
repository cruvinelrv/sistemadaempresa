package Core
{
	import SDE.CamadaServico.SCliente;
	import SDE.Entidade.Cliente;
	import SDE.Parametro.ParamFiltroCliente;
	import SDE.Parametro.ParamLoadCliente;
	
	
	public final class CacheEntidades
	{
		
		private var fornecedores:Array;
		
		public function getFornecedores(fCallBack:Function):void
		{
			if (this.fornecedores)
				fCallBack(this.fornecedores);
			else
			{
				var pf:ParamFiltroCliente = new ParamFiltroCliente();
				pf.fornecedor = true;
				//var pl:ParamLoadCliente = new ParamLoadCliente();
				
				SCliente.unica.Pesquisa(
					pf, null,
					function(retorno:Array):void
					{
						this.fornecedores = retorno;
						fCallBack(this.fornecedores);
					}
				);
			}
			
		}
		
	}
}