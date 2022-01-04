
import Core.Alerta.AlertaSistema;
import Core.App;
import Core.Load.LoadSistema;

import SDE.CamadaServico.SMov;
import SDE.Entidade.MovValor;
import SDE.Parametro.ParamLoadMov;

import mx.utils.Base64Encoder;

	[Bindable] private var arIEE:ArrayCollection = new ArrayCollection();
	
	private function confirmaAlteracao(resumo:String, tipo:String, impressao:String, geraEtiqueta:Boolean):void
	{
		if (listaMovItem.length == 0)
		{
			AlertaSistema.mensagem("Sem itens de entrada");
			return;
		}
		
		movAlterada.__mItens = [];
		movAlterada.idClienteFuncionarioLogado = Sessao.unica.idClienteFuncionarioLogado;
		movAlterada.idEmp = Sessao.unica.idEmp;
		movAlterada.idCliente = fornecedor.id;
		movAlterada.cliente_nome = fornecedor.nome;
		if (transportador)
			movAlterada.idClienteTransportador = transportador.id;
		movAlterada.numeroNF = Number(lblNumeroNota.text);
		
		for each(var o:Object in listaMovItem)
		{
			var mi:MovItem = o.movItem;
			movAlterada.vlrItensInicial += mi.qtd * mi.vlrUnitCompra;
			movAlterada.vlrItensFinal += mi.qtd * mi.vlrUnitCusto;
			movAlterada.__mItens.push(mi);
		}
		movAlterada.vlrTotal = nsValorTotalProdutos.value + nsValorFrete.value + nsValorSubstituicaoTributaria.value;
		movAlterada.vlrFrete = nsValorFrete.value;
		movAlterada.vlrSubstituicaoTributaria = nsValorSubstituicaoTributaria.value;
		movAlterada.vlrArredondamentoNota = nsValorArredondamentoNota.value;
		
		var mv:MovValor = new MovValor();
		mv.valor = movAlterada.vlrItensFinal;
		movAlterada.__mValores = [mv];
		
		movAlterada.dtNF = dfDataEmissao.text;
		movAlterada.dtEntSai = dfDataEmtrada.text;
		
		movAlterada.resumo = resumo;
		movAlterada.tipo = tipo;
		movAlterada.impressao = impressao;
		
		alteraPrecoItens();
		
		App.single.ss.nuvens.modificacoes.AlteraEntrada(movOriginal, movAlterada, arrayIEP,
			function(retorno:Number):void
			{
				movAlterada.id = retorno;
				
				if (geraEtiqueta)
				{
					var pl:ParamLoadMov = new ParamLoadMov();
						pl.movItens = true;
						pl.itens = true;
						SMov.unica.Load(movAlterada.id, pl,
							function(retorno:Mov):void
							{
								var url:URLRequest = new URLRequest("Imprime.swf");
								var vars:URLVariables = new URLVariables();
								url.data = vars;
								vars.idCorp = Sessao.unica.idCorp;
								vars.idEmp = Sessao.unica.idEmp;
								vars.tipo = "movAlteradaimentacao";//cmbRelatorios.selectedLabel;
								vars.tipo_impressao = "etiqueta";
								vars.etiqueta = "modelo01";//getModelo();//cmbEtiquetaModelo.selectedLabel;
								vars.saltar = 0;
								
								var enc:Base64Encoder = new Base64Encoder();
								enc.encodeUTFBytes("corp"+vars.idCorp);
								vars.hash = enc.toString();
								
								vars.idMov = movAlterada.id;
								
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