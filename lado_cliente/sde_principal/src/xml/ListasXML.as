package xml
{
	import Core.Alerta.AlertaSistema;
	
	import mx.collections.ArrayCollection;
	import mx.rpc.events.ResultEvent;
	import mx.rpc.http.HTTPService;
	
	public final class ListasXML
	{
		//private var _novidades:ArrayCollection=null;
		private var _municipios:ArrayCollection=null;
		private var _cfops:ArrayCollection=null;
		
		/*
		public function Novidades(fCallBack:Function):void
		{
			if (_novidades!=null&&_novidades.length>0)
				fCallBack(this._novidades);
			else
			{
				var http:HTTPService = new HTTPService();
				http.url = http.rootURL.replace("index.swf","xml/novidades.xml");
				http.addEventListener(ResultEvent.RESULT,
					function(ev:ResultEvent):void
					{
						this._novidades = ev.result.novidades.mes;
						this.Novidades(fCallBack);//reinvoca para forçar a busca
					}
				);
				http.send();
			}
		}
		*/
		public function Municipios(fCallBack:Function):void
		{
			if (this._municipios!=null&&this._municipios.length>0)
				fCallBack(this._municipios.source);
			else
			{
				var http:HTTPService = new HTTPService();
				http.url = http.rootURL.replace("index.swf","xml/municipios.xml");
				http.addEventListener(ResultEvent.RESULT,
					function(ev:ResultEvent):void
					{
						this._municipios = ev.result.tabela.uf;
						if (this._municipios!=null&&this._municipios.length>0)
							fCallBack(this._municipios.source);
						else
							Municipios(fCallBack);//reinvoca para forçar a busca
					}
				);
				http.send();
				AlertaSistema.mensagem("buscando municipios IBGE");
			}
		}
		
		public function CFOP(fCallBack:Function):void
		{
			if (this._cfops!=null&&this._cfops.length>0)
				fCallBack(this._cfops.source);
			else
			{
				var http:HTTPService = new HTTPService();
				http.url = http.rootURL.replace("index.swf","xml/tbl_cfop.xml");
				http.addEventListener(ResultEvent.RESULT,
					function(ev:ResultEvent):void
					{
						this._cfops = ev.result.tabela.c;
						if (this._cfops!=null&&this._cfops.length>0)
							fCallBack(this._cfops.source);
						else
							CFOP(fCallBack);//reinvoca para forçar a busca
					}
				);
				http.send();
				AlertaSistema.mensagem("buscando lista CFOP");
			}
		}
	}
}