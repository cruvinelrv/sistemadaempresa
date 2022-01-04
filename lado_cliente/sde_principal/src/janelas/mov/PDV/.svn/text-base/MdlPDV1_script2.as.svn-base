import Core.Alerta.AlertaSistema;
import Core.App;
import Core.ConexaoDesktop.ConnDesktop;
import Core.Ev.EvRetornaArray;
import Core.Sessao;

import SDE.CamadaNuvem.NuvemCache;
import SDE.CamadaServico.SNfe;
import SDE.Constantes.Variaveis_SdeConfig;
import SDE.Entidade.Cliente;
import SDE.Entidade.Mov;
import SDE.Entidade.MovItem;
import SDE.Entidade.MovItemEstoque;
import SDE.Entidade.MovValor;
import SDE.Entidade.SdeConfig;
import SDE.Enumerador.EMovImpressao;
import SDE.Enumerador.EMovResumo;
import SDE.Enumerador.EMovTipo;
import SDE.Enumerador.EValorEspecie;
import SDE.FachadaServico.FcdMov;

import flash.net.URLRequest;
import flash.net.URLRequestMethod;
import flash.net.URLVariables;
import flash.net.navigateToURL;

import janelas.mov.PDV.template.PopTEF;

import mx.controls.Alert;
import mx.core.Application;
import mx.events.CloseEvent;
import mx.formatters.CurrencyFormatter;
import mx.managers.PopUpManager;
import mx.utils.Base64Encoder;

/*
	private function CmbEspecieLabel(lbl:String):String
	{
		AlertaSistema.mensagem(lbl);
		lbl = lbl.split("_").join(" ");
		return lbl;
	}
*/
	private function FechaMovimentacao(resumo:String, tipo:String, impressao:String):void
	{
		if (listaMI.length==0)
		{
			AlertaSistema.mensagem('não há itens');
			return;
		}
		
		if (cbCancelarImportada.selected && idMovImportada>0)
		{
			if  (tipoMovImportada==EMovTipo.outros_reserva||tipoMovImportada==EMovTipo.outros_orcamento)
			{
				FcdMov.unica.CancelaMov(idMovImportada);
				idMovImportada=0;
			}
		}
		
		var mov:Mov = new Mov();
		mov.__mItens = [];
		mov.idCliente = cliente.id;
		mov.idClienteFuncionarioLogado = Sessao.unica.idClienteFuncionarioLogado;
		mov.idEmp = Sessao.unica.idEmp;
		mov.idClienteParceiro = 0;
		mov.idClienteTransportador = 0;
		//mov.idCaixa
		//mov.idCaixaDia
		
		mov.__cli = cliente;
		mov.__cliFuncionario = vendedor;
		
		//MV e MI
		mov.__mValores = seletorEspecies.getMV();
		for each(var o:Object in listaMI)
		{
			var mi:MovItem = o.movItem;
			mov.vlrItensInicial += mi.qtd * mi.vlrUnitVendaInicial;
			mov.vlrItensFinal   += mi.qtd * mi.vlrUnitVendaFinal;
			mov.__mItens.push(mi);
		}
		
		mov.vlrAcrescimo = -nsDescontoFinal.value;
		mov.vlrTotal = mov.vlrItensFinal + mov.vlrAcrescimo;
		
		//
		mov.resumo = resumo;
		mov.tipo = tipo;
		mov.impressao = impressao;
		
		
		/*
				VALIDA APENAS    UM    ÚNICO CONDICIONAL
		*/
		
		if (mov.__mValores.length>1)
		{
			var mvCondicional:MovValor = null;
			for each (var mv:MovValor in mov.__mValores)
			{
				if (mv.especie==EValorEspecie.reserva)
					mvCondicional = mv;
			}
			if (mvCondicional!=null)
			{
				mvCondicional.valor = mov.vlrTotal;
				mov.__mValores = [mvCondicional];
				AlertaSistema.mensagem( "O valor total foi transferido para Orcamento!", false, 15000 );
			}
		}
		
		//se é um condicional
		if (mov.__mValores.length==1 && mov.__mValores[0].especie==EValorEspecie.reserva)
		{
			mov.resumo = EMovResumo.outros;
			mov.tipo = EMovTipo.outros_reserva;
			mov.impressao = EMovImpressao.reserva;
		}
		
		AlertaSistema.mensagem( "tipo: " +mov.tipo, true );
		AlertaSistema.mensagem( "impressao: " +mov.impressao, true );
		
		
		var fFechaMov:Function =
			function(fAntesFechar:Function=null):void
			{
				
				
				for each (var mi:MovItem in mov.__mItens)
				{
					for each (var mie:MovItemEstoque in mi.__mIEstoques)
					{
						mie.__iee=null;
					}
					mi.__item=null;
				}
				mov.__cli=null;
				mov.__cliFuncionario = null;
				mov.__cliParceiro=null;
				mov.__cliTransp=null;
				//Funcoes.filtraCampos(mov, Mov.getCampos());
				
				
				FcdMov.unica.NovaMovPDV(
					mov,
					function(retorno:Number):void
					{
						
						mov.id = retorno;
						
						Alert.show( "Deseja imprimir uma página com a movimentação?", "imprimir?", Alert.YES+Alert.NO, null,
							function(ev2:CloseEvent):void
				            {
				                if(ev2.detail == Alert.YES)
				                {
				                    //mdiCanvas.windowManager.executeDefaultBehavior(queuedEvent);
				                    
				                    var url:URLRequest = new URLRequest("Imprime.swf");
				                    url.method = URLRequestMethod.GET;
				                    
				                    var vars:URLVariables = new URLVariables();
				                    vars.idMov = mov.id;
				                    vars.idCorp = Sessao.unica.idCorp;
				                    vars.idEmp = Sessao.unica.idEmp;
				                    
				                    vars.tipo_impressao = "relatorio";
				                    vars.relatorio = "movimentacao";
				                    
									var enc:Base64Encoder = new Base64Encoder();
									enc.encodeUTFBytes("corp"+vars.idCorp);
									vars.hash = enc.toString();
				                    
				                    url.data = vars;
				                    navigateToURL(url, "_blank");
				                    
				                }
				            }
						);
						
						if (fAntesFechar!=null)
							fAntesFechar();
						
						AlertaSistema.mensagem( "Concluído com Sucesso!" );
						listaMI.splice(0, listaMI.length);
						limpaTela();
						exibeTotaisPinta();
						
					}
				);
			}
		
		//fechamentos da venda
		switch (mov.tipo)
		{
			case EMovTipo.saida_venda:
			case EMovTipo.outros_orcamento:
			case EMovTipo.outros_reserva:
				
				
				
				switch(mov.impressao)
				{
					case EMovImpressao.sem_impressao:
					case EMovImpressao.orcamento:
					case EMovImpressao.reserva:
						
						
						fFechaMov();
						
						
						break;
					case EMovImpressao.nfe_produto:
						
						
						fFechaMov(
							function():void
							{
								SNfe.unica.GerarTXT(mov.id,
									function(r:Array):void
									{
										Application.application.gerenteConexaoDesktop.escreveArquivoNFE(r[0],r[1]);
									}
								);
								AlertaSistema.mensagem( "enviei nfe da mov: "+mov.id );
							}
						);
						
						
						break;
					case EMovImpressao.cupom_fiscal:
						
						if (!Application.application.ProxyEstaAberto)
						{
						//	Alert.show("O Software Conector da Impressoa Fiscal não foi encontrado");
						}
						else
						{
							
							var pTEF:PopTEF = new PopTEF();
							
							PopUpManager.addPopUp( pTEF, this, true );
							PopUpManager.centerPopUp(pTEF);
							/*
							SoqueteRecebimento.unica.RegistraRetornoMultiTEF(
								pTEF.RetornoMultiTEF
							);
							*/
							Application.application.gerenteConexaoDesktop.addEventListener(
								ConnDesktop.RetornoTEF,
								pTEF.RetornoMultiTEF
							);
							
							pTEF.addEventListener(EvRetornaArray.RETORNO,
								function(ev:EvRetornaArray):void
								{
									AlertaSistema.mensagem( "Concluído com Sucesso!" );
									mov.cooTEF = ev.retorno[0];
									PopUpManager.removePopUp(pTEF);
									fFechaMov();
								}
							);
							/*
							pTEF.addEventListener(Evento.CANCELA,
								function():void
								{
									Application.application.gerenteConexaoDesktop.removeEventListener(
										ConnDesktop.RetornoTEF,
										pTEF.RetornoMultiTEF
									);
									//SoqueteRecebimento.unica.RegistraRetornoMultiTEF( null );
								}
							);
							*/
							var linhasMultiTEF:String = escreveArquivoMultiTEF(mov);
							var compl:String = "vendedor: "+App.single.cache.getCliente(mov.idClienteFuncionarioVendedor).nome.split(" ")[0]; //mov.__cliFuncionario.nome.split(" ")[0];
							//SoqueteEnvio.unica.enviaMultiTEF(linhasMultiTEF, compl);
							Application.application.gerenteConexaoDesktop.escreveArquivoTEF(linhasMultiTEF, compl, pTEF.RetornoMultiTEF);
						}
						
						break;
					default:
						AlertaSistema.mensagem( "Impressão "+mov.impressao+" ainda não suportado" );
						return;
				}
				
				
				
				
				//_FechaMovVenda(mov, fFechaMov);
				
				//_FechaMovOrcamento(mov);
				//fFechaMov();
				//AlertaSistema.mensagem( "Concluído com Sucesso! " );
				
				
				break;
				/*
			case EMovTipo.saida_condi:
				
				_FechaMovCondicional(mov);
				
				return;
				break;
				*/
			default:
				Alert.show('tipo de finalização desconhecido');
				return;
				break;
		}
		/*
		listaMI.splice(0, listaMI.length);
		limpaTela();
		exibeTotais();
		pintaTela();
		*/
	}
	
	private function escreveArquivoMultiTEF(mov:Mov):String
	{
		/*
		pessoa:Pessoa, itens:Array, endereco:String,
						  vendedor:String, //numeroVenda:String, aliquota:String,
						  valorDesconto:Number
		*/
		
		var arRetorno:Array = [];
		var formaPgtoMULTITEF:String="01";
		
		var cache:NuvemCache = App.single.cache;
		
		var arraySequencia:Array = ['01','02','03','04','05','06','07','08'];
		var arrayAliquotas:Array = [17];
		
		for each(var sdeC:SdeConfig in cache.arraySdeConfig)
		{
			if (sdeC.variavel==Variaveis_SdeConfig.EMPRESA_ALIQUOTASECF)
				arrayAliquotas = sdeC.valor.split('|');
		}
		
		//sdeConfig.valor = arrayAliquotas.join('|');
		
		var cli:Cliente = mov.__cli;
		var vendedorNome:String = App.single.cache.getCliente(mov.idClienteFuncionarioVendedor).nome.split(" ")[0]; //mov.__cliFuncionario.nome.split(" ")[0];
		var endereco:String = "";
		
		var tipoModificador:String = "a";
		var vlrDesconto:Number = mov.vlrTotal - mov.vlrItensInicial;
		
		if (vlrDesconto < 0)
		{
			tipoModificador = "d";
			vlrDesconto = -vlrDesconto;
		}
		
		var s:String = "0"+
			alinhaString(cli.cpf_cnpj,14)+
			alinhaString(cli.nome,40)+
			alinhaString(endereco,47)+
			formaPgtoMULTITEF+
			tipoModificador+
			alinhaNumero(vlrDesconto, 8, 2)+
			alinhaString(vendedorNome, 10);
		//arRetorno.push(s);
		
		for each (var mi:MovItem in mov.__mItens)
		{
			var numAliq:String = "01";
			
			//
			for (var i:int = 0; i < arrayAliquotas.length; i++)
			{
				//AlertaSistema.mensagem( mi.icmsAliq+" <- reg: "+arraySequencia[i]+" aliq: "+arrayAliquotas[i] );
				
				var al:Number = arrayAliquotas[i];
				if (al == mi.icmsAliq)
				{
					//AlertaSistema.mensagem( "achei!" );
					numAliq = arraySequencia[i];
				}
			}
			
			s += "\r\n1"+
				numAliq+
				alinhaNumero(mi.__item.id, 13, 0)+
				alinhaString(mi.__item.nome.substr(0,29)+" ", 30)+
				alinhaNumero(mi.vlrUnitVendaInicial, 9, 2)+
				alinhaNumero(mi.qtd, 8, 3);		
			arRetorno.push(s);
		}
		//return arRetorno;
		return s;
	}
	//Função Alinha Numeros
	private function alinhaNumero(valor:Number, larguraTotal:int, qtdCasasDecimais:int):String 
	{
		//Preenche com casas Decimais
		var fmt:CurrencyFormatter = new CurrencyFormatter();
		fmt.precision = qtdCasasDecimais;
		fmt.currencySymbol = "";
		fmt.useThousandsSeparator = false;
		
		var ret:String = fmt.format(valor).replace(".", ",");
		//eu quero um tamanho menor que os dados, então corta a sobra
		if(larguraTotal < ret.length)
			ret = ret.substr(0, larguraTotal);
		//enquanto "a largura da string de retorno" for menor que "o tamanho que eu quero chegar", ponha um zero
		while(larguraTotal > ret.length)
			ret = "0"+ret;
		return ret;
	}
	//Função Alinha String
	private function alinhaString( valor:String, larguraTotal:int): String
	{
		var ret:String = (valor==null) ? "" : valor;
		//eu quero um tamanho menor que os dados, então corta a sobra
		if(larguraTotal < ret.length)
			ret = ret.substr(0, larguraTotal);
		//enquanto "a largura da string de retorno" for menor que "o tamanho que eu quero chegar", ponha um espaço
		while(larguraTotal > ret.length)
			ret +=" ";
		return ret;
	}
	
	
	
	
	
	
	
	
	
	
	
	/**/
	
	
	
	
	
	
	
	
	
	
	
	
	