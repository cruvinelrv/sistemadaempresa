package Core.ConexaoDesktop
{
	import Core.Alerta.AlertaSistema;
	import Core.Sessao;
	
	import flash.events.EventDispatcher;
	
	public class GerenteConexaoDesktop extends EventDispatcher
	{
		[Bindable] private var conn:ConnDesktop
		public function GerenteConexaoDesktop(receiverweb:String,receiverproxy:String)
		{
			conn = new ConnDesktop( this, receiverweb, receiverproxy );
			//conn.addEventListener(ConnDesktop.RetornoTEF, fn_trata_RetornoTEF);
		}
		/*
		public function proxyEstaAberto():Boolean
		{
			return conn.proxyEstaAberto;
		}
		private function fn_trata_RetornoTEF(ev:EventoGenerico):void
		{
			Alert.show("retorno tratado em gerente: "+ev.number);
		}
		*/
		public function pingaProxy():void
		{
			conn.proxyPinga();
		}
		public function escreveArquivoTEF(conteudo:String, complemento:String, fRetorno:Function=null):void
		{
			conn.proxyEnvia("web_chama_desktop_enviatef", conteudo, complemento);
			if (fRetorno!=null)
				conn.addEventListener(ConnDesktop.RetornoTEF, fRetorno);
		}
		public function escreveArquivoNFE(conteudo:String, chaveAcessoNFE:String):void
		{
			conn.proxyEnvia("web_chama_desktop_envianfe", conteudo, chaveAcessoNFE);
		}
		public function escreveArquivoNFExml(conteudo:String, chaveAcessoNFE:String):void
		{
			conn.proxyEnvia("web_chama_desktop_envianfe_xml", conteudo, chaveAcessoNFE);
		}
		public function imprimeDanfe(nomePdf:String):void
		{
			conn.proxyEnvia("web_chama_desktop_imprime_danfe", nomePdf);
		}
		public function imprimeEtiquetas(idCorp:Number):void
		{
			conn.proxyEnvia("web_chama_desktop_imprime_etiquetas", idCorp);
		}
		public function iniciaProcesso(executavel:String, parametros:String="0"):void
		{
			conn.proxyEnvia("web_chama_desktop_processo", executavel, parametros);
		}
		public function exibeNotaPrefeitura(idMov:Number):void
		{
			var url:String = "\"http://sde.sistemadaempresa.com.br/notaprefeitura.aspx?idCorp="+Sessao.unica.idCorp+"&idMov="+idMov+"\"";
			this.iniciaProcesso("explorer", url);
		}
		public function enviaNotaPrefeitura(idMov:Number, conteudo:String):void
		{
			conn.proxyEnvia("web_chama_desktop_envianfe_prefeitura", idMov, conteudo);
		}
		public function imprimeMovPdf(idCorp:Number, idEmp:Number, idMov:Number, tipo:String):void
		{
			conn.proxyEnvia("web_chama_desktop_constroipdf", idCorp, idEmp, idMov, tipo);
		}
		public function baixaListaCasamento(idCorp:Number):void
		{
			conn.proxyEnvia("web_chama_desktop_baixa_listaCasamento", idCorp);
		}
		public function baixaDuplicata(idCorp:Number, idEmp:Number, numeroTitulo:String, tipoDocumento:String):void
		{
			conn.proxyEnvia("web_chama_desktop_baixa_duplicata", idCorp, idEmp, numeroTitulo, tipoDocumento);
		}
		public function baixaInventario(idCorp:Number, dataInventario:String, tipoDocumento:String):void
		{
			conn.proxyEnvia("web_chama_desktop_baixa_inventario", idCorp, dataInventario, tipoDocumento);
		}
		public function baixaCarne(idCorp:Number):void
		{
			conn.proxyEnvia("web_chama_desktop_baixa_carne", idCorp);
		}
		public function baixaRelatorioCliente(idCorp:Number, idEmp:Number):void
		{
			conn.proxyEnvia("web_chama_desktop_baixa_relCliente", idCorp, idEmp);
		}
		public function baixaRelatorioParcialBalanco(idCorp:Number):void
		{
			conn.proxyEnvia("web_chama_desktop_baixa_relParcialBalanco", idCorp);
		}
		public function baixaRelatorioOrdemServico(idCorp:Number, idOrdemServico:Number):void
		{
			conn.proxyEnvia("web_chama_desktop_baixa_relOrdemServico", idCorp, idOrdemServico);
		}
		public function baixaRelatorio(idCorp:Number, relatorio:String, nomeRelatorio:String):void
		{
			conn.proxyEnvia("web_chama_desktop_baixa_relatorio", idCorp, relatorio, nomeRelatorio);
		}
		public function exibeNotaFiscalFormulario():void
		{
			var url:String = null;
			if (Sessao.unica.idCorp == 47)
			{
				url = "http://sistemadaempresa.com.br/sde/notavivati.aspx";
				url = "\""+url+"\"";
				this.iniciaProcesso("explorer", url);
			}
			else if (Sessao.unica.idCorp == 43)
			{
				url = "http://sistemadaempresa.com.br/sde/notaagritom.aspx";
				url = "\""+url+"\"";
				this.iniciaProcesso("explorer", url);
			}
			else if (Sessao.unica.idCorp == 33)
			{
				url = "http://sistemadaempresa.com.br/sde/notatotalaco.aspx";
				url = "\""+url+"\"";
				this.iniciaProcesso("explorer", url); 
			}
			else if (Sessao.unica.idCorp == 6)
			{
				url = "http://sistemadaempresa.com.br/sde/notacasadafazenda.aspx";
				url = "\""+url+"\"";
				this.iniciaProcesso("explorer", url);
			}
			else
			{
				AlertaSistema.mensagem("Esta empresa não possui impressão de Nota Fiscal");
			}
		}
	}
}