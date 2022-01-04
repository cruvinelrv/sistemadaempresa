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

import janelas.mov.ImpressaoNFE1.JanelaImpressaoNFE1;

import mx.controls.Alert;
import mx.core.Application;

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
				FcdMov.unica.NovaMovEntradaDevolucao(
					mov,
					function(retorno:Number):void
					{						
						mov.id = retorno;
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
			case EMovTipo.entrada_devolucao:
				switch(mov.impressao)
				{
					case EMovImpressao.nfe_produto:
						fFechaMov(
							function():void
							{	
								var janela:JanelaImpressaoNFE1 = new JanelaImpressaoNFE1(); 
								Application.application.gerenteJanelas.NovaJanela(janela, "Imprima sua Nota");
								janela.BuscaMov(mov.id);
							}
						);
						break;
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
	
	
	
	
	
	
	
	
	
	
	
	
	