// ActionScript file
import Core.Alerta.AlertaSistema;
import Core.App;
import Core.Sessao;

import SDE.CamadaServico.SMov;
import SDE.Entidade.Mov;
import SDE.Entidade.MovItem;
import SDE.Entidade.MovItemEstoque;
import SDE.Entidade.MovValor;
import SDE.Enumerador.EMovTipo;
import SDE.FachadaServico.FcdMov;
import SDE.Parametro.ParamLoadMov;

import flash.net.URLRequest;
import flash.net.URLVariables;
import flash.net.navigateToURL;

import mx.collections.ArrayCollection;
import mx.utils.Base64Encoder;

	[Bindable] private var arIEE:ArrayCollection = new ArrayCollection();
	
	private function FechaMovimentacao(resumo:String, tipo:String, impressao:String, geraEtiqueta:Boolean):void
	{
		/**INICIA VALIDAÇÃO*/
		var msg:String = "";
		if (listaMI.length == 0)
			msg += "Sem itens de entrada\n";
		if (txtNumNota.text == "")
			msg += "Insira o número da Nota Fiscal\n";
		if (fornecedor == null)
			msg += "Selecione de fornecedor\n";
			
		if (msg!="")
		{
			AlertaSistema.mensagem(msg);
			return;
		}
		
		FcdMov.unica.LoadMovEntrada_NumeroNota(
			fornecedor.id,  Number(txtNumNota.text), resumo,
			function(ret:Array):void
			{
				var existeNota:Boolean = false;
				if (ret != null)
				{
					for each (var aaa:Mov in ret)
					{
						if (aaa.tipo != EMovTipo.entrada_cancel)
							existeNota = true;
					}
					if (existeNota)
					{
						var s:String = "Já existe uma nota de N° '"+txtNumNota.text+"' do fornecedor no sistema";
						AlertaSistema.mensagem(s);
						return;
					}
				}
				
				/**FIM VALIDAÇÃO*/
				
				var mov:Mov = new Mov();
				mov.__mItens = [];
				mov.idClienteFuncionarioLogado = Sessao.unica.idClienteFuncionarioLogado;
				mov.idEmp = Sessao.unica.idEmp;
				mov.idCliente = fornecedor.id;
				mov.cliente_nome = fornecedor.nome;
				if (transportador)
					mov.idClienteTransportador = transportador.id;
				mov.numeroNF = Number(txtNumNota.text);
		
				for each (var o:Object in listaMI)
				{
					var mi:MovItem = o.movItem;
					mov.vlrItensInicial += mi.qtd * mi.vlrUnitCompra;
					mov.vlrItensFinal += mi.qtd * mi.vlrUnitCusto;
					mov.__mItens.push(mi);
				}
				mov.vlrTotal = nsValorTotalProdutos.value + nsValorFrete.value + nsValorSubstituicaoTributaria.value;
				mov.vlrFrete = nsValorFrete.value;
				mov.vlrSubstituicaoTributaria = nsValorSubstituicaoTributaria.value;
				mov.vlrArredondamentoNota = nsValorArredondamentoNota.value;
				
				var mv:MovValor = new MovValor();
				mv.valor = mov.vlrItensFinal;
				mov.__mValores = [mv];
				
				mov.dtNF = dfDataEmissao.text;
				mov.dtEntSai = dfDataEmtrada.text;
				
				mov.resumo = resumo;
				mov.tipo = tipo;
				mov.impressao = impressao;
				
				alteraPrecoItens();
				
				App.single.ss.nuvens.modificacoes.NovaEntrada(mov,arrayIEP,
					function(retorno:Number):void
					{
						mov.id = retorno;
						
						if (geraEtiqueta)
						{
							var pl:ParamLoadMov = new ParamLoadMov();
							pl.movItens = true;
							pl.itens = true;
							SMov.unica.Load(mov.id, pl,
								function(retorno:Mov):void
								{
									var url:URLRequest = new URLRequest("Imprime.swf");
									var vars:URLVariables = new URLVariables();
									url.data = vars;
									vars.idCorp = Sessao.unica.idCorp;
									vars.idEmp = Sessao.unica.idEmp;
									vars.tipo = "movimentacao";//cmbRelatorios.selectedLabel;
									vars.tipo_impressao = "etiqueta";
									vars.etiqueta = "modelo01";//getModelo();//cmbEtiquetaModelo.selectedLabel;
									vars.saltar = 0;
									
									var enc:Base64Encoder = new Base64Encoder();
									enc.encodeUTFBytes("corp"+vars.idCorp);
									vars.hash = enc.toString();
									
									vars.idMov = mov.id;
									
									var obj:Object;
									for each (var mi:MovItem in retorno.__mItens)
									{
										for each (var mie:MovItemEstoque in mi.__mIEstoques)
										{
											obj= new Object();
											obj.nome = mi.__item.nome;
											obj.qtd = mie.qtd;
											obj.idIEE = mie.idIEE;
											arIEE.addItem(obj);
										}
									}
									
									vars.total_pares = arIEE.length;
									for (var i:int=0; i < arIEE.length; i++)
									{
										var o:Object  = arIEE[i];
										vars["lista"+i] = o.idIEE+","+o.qtd;
									}
									navigateToURL(url, "_blank");
								}
							);
						}
						limpaTela();
					}
				);
			}
		);
	}
	
	private function getModelo():String
	{
		return cmbEtiquetaModelo.selectedLabel;
	}