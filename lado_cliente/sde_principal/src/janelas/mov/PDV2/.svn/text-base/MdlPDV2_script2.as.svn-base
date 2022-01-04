import Core.Alerta.AlertaSistema;
import Core.Janelas.FabricaJanela;
import Core.Sessao;

import SDE.Entidade.Mov;
import SDE.Entidade.MovItem;
import SDE.Entidade.MovItemEstoque;
import SDE.Entidade.MovValor;
import SDE.Enumerador.EMovImpressao;
import SDE.Enumerador.EMovResumo;
import SDE.Enumerador.EMovTipo;
import SDE.Enumerador.EValorEspecie;
import SDE.FachadaServico.FcdMov;

import flash.net.URLRequest;
import flash.net.URLRequestMethod;
import flash.net.URLVariables;
import flash.net.navigateToURL;

import janelas.mov.ImpressaoNFE1.JanelaImpressaoNFE1;
import janelas.mov.PDV.template.PopTEF;

import mx.controls.Alert;
import mx.core.Application;
import mx.events.CloseEvent;
import mx.formatters.CurrencyFormatter;
import mx.managers.PopUpManager;
import mx.utils.Base64Encoder;

	private function FechaMovimentacao(resumo:String, tipo:String, impressao:String):void
	{
		if (cliente.id==1)
		{
			AlertaSistema.mensagem('Você não pode gerar uma nota fiscal para Cliente Consumidor');
			return;
		}
		
		if (listaMI.length==0)
		{
			AlertaSistema.mensagem('não há itens');
			return;
		}
		/*
		if (cbCancelarImportada.selected && idMovImportada>0)
		{
			if  (tipoMovImportada==EMovTipo.outros_reserva||tipoMovImportada==EMovTipo.outros_orcamento)
			{
				//FcdMov.unica.CancelaMov(idMovImportada, Sessao.unica.idClienteFuncionarioLogado, null);
				idMovImportada=0;
			}
		}
		*/
		var mov:Mov = new Mov();
		mov.__mItens = [];
		mov.idCliente = cliente.id;
		mov.idClienteFuncionarioLogado = Sessao.unica.idClienteFuncionarioLogado;
		mov.idEmp = Sessao.unica.idEmp;
		mov.idClienteParceiro = 0;
		mov.idClienteTransportador = 0;
		
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
		
		//fechamentos da venda
		switch (mov.tipo)
		{
			case EMovTipo.saida_venda:
			case EMovTipo.outros_orcamento:
			case EMovTipo.outros_reserva:
				switch(mov.impressao)
				{
					/*
					case EMovImpressao.sem_impressao:
					case EMovImpressao.orcamento:
					case EMovImpressao.reserva:
						fFechaMov();
						break;
						*/
					case EMovImpressao.nfe_produto:
						
						
						
						
						
						
						for each (var xxx:MovItem in mov.__mItens)
						{
							for each (var yyy:MovItemEstoque in xxx.__mIEstoques)
							{
								yyy.__iee=null;
							}
							xxx.__item=null;
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
								
								
								var janela:JanelaImpressaoNFE1 = new JanelaImpressaoNFE1();
								Application.application.gerenteJanelas.NovaJanela(janela, "Imprima sua NFE");
								janela.BuscaMov(mov.id);
								
								
								AlertaSistema.mensagem( "Concluído com Sucesso!" );
								listaMI.splice(0, listaMI.length);
								limpaTela();
								exibeTotaisPinta();
								
							}
						);
						break;
						/*
					case EMovImpressao.cupom_fiscal:						
						if (!Sessao.unica.desktopExecutando)
						{
							Alert.show("O Software Conector da Impressoa Fiscal não foi encontrado");
						}
						else
						{							
							var pTEF:PopTEF = new PopTEF();
							
							PopUpManager.addPopUp( pTEF, this, true );
							PopUpManager.centerPopUp(pTEF);
							
							SoqueteRecebimento.unica.RegistraRetornoMultiTEF(
								pTEF.RetornoMultiTEF
							);
							
							pTEF.addEventListener(EvRetornaArray.RETORNO,
								function(ev:EvRetornaArray):void
								{
									AlertaSistema.mensagem( "Concluído com Sucesso! " );
									mov.cooTEF = ev.retorno[0];
									fFechaMov();
								}
							);
							
							pTEF.addEventListener(Evento.CANCELA,
								function():void
								{
									SoqueteRecebimento.unica.RegistraRetornoMultiTEF( null );
								}
							);
							
							var linhasMultiTEF:Array = escreveArquivoMultiTEF(mov);
							var compl:String = "vendedor: "+mov.__cliFuncionario.__pessoa.nome.split(" ")[0];
							SoqueteEnvio.unica.enviaMultiTEF(linhasMultiTEF, compl);
						}
						
						break;
						*/
					default:
						AlertaSistema.mensagem( "Impressão "+mov.impressao+" ainda não suportado" );
						return;
				}			
				break;
			default:
				Alert.show('tipo de finalização desconhecido');
				return;
				break;
		}
	}
	
	/**/
	
	
	
	
	
	
	
	
	
	
	
	
	