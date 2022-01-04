import Core.App;
import Core.Utils.Formatadores;

import SDE.Entidade.Cx_Lancamento;
import SDE.Entidade.Mov;
import SDE.Entidade.MovItem;
import SDE.Entidade.Orcamento_Lancamento;
import SDE.Enumerador.EMovTipo;

import flash.events.Event;

import mx.managers.PopUpManager;
	
	
	private function usuario_abre_popup_importa_mov():void
	{
		//marcos ou flaviano?
		//esse código estava tirando a filtragem do usuario e forçando o tipo venda
		//popup_importar_venda.cmbTipoMov.visible = false;
		//popup_importar_venda.cmbTipoMov.selectedItem = EMovTipo.saida_venda;
		//marcos ou flaviano?
		//thiago
		//o que estou fazendo é limitar as opções 
		var dpCmb:Array = [EMovTipo.saida_venda, EMovTipo.outros_orcamento, EMovTipo.saida_cancel, EMovTipo.outros_pedido];
		popup_importar_venda.cmbTipoMov.dataProvider = dpCmb;
		//thiago
		//obs: ambas as formas estao incorretas, pois violam o encapsulamento do componente 'popup_importar_venda'
		
		popup_importar_venda.lbTipoMov.visible = false;
		popup_importar_venda.retornaMov = true;
		popup_importar_venda.dgcRetorna.visible = true;
		PopUpManager.addPopUp(popup_importar_venda, this, true);
		PopUpManager.centerPopUp(popup_importar_venda);
		popup_importar_venda.filtraBuscaMov();
	}
	
	private function usuario_retorna_popup_importa_mov(ev:Event):void
	{
		var movRet:Mov = ev.target.data;
		usuario_fecha_popup_importa_mov();
		
		isImportadoOS = false;
		
		sistema_limpar_carrinho(false);
		
		cpCliente.selectedItem = cache.getCliente(movRet.idCliente).clone();
		cliente_selecionado = cache.getCliente(movRet.idCliente).clone();
		change_cpCliente();
		
		cpVendedor.selectedItem = cache.getCliente(movRet.idClienteFuncionarioVendedor);
		mov.idClienteFuncionarioVendedor = movRet.idClienteFuncionarioVendedor;
		
		for each (var mi:MovItem in App.single.cache.arrayMovItem)
		{
			if (mi.idMov == movRet.id)
				arraycItensCarrinho.addItem(mi.clone());
		}
		
		if (movRet.tipo == EMovTipo.outros_orcamento){
			for each (var ol:Orcamento_Lancamento in App.single.cache.arrayOrcamento_Lancamento){
				if (ol.idTransacao == movRet.idTransacao){
					var cl:Cx_Lancamento = new Cx_Lancamento();
					cl.idClientePagador = ol.idClientePagador
					cl.idClienteRecebedor = ol.idClienteRecebedor;
					cl.idGrupoTipoPagamento = ol.idGrupoTipoPagamento;
					cl.idMovMapa = ol.idMovMapa;
					cl.idPortador = ol.idPortador;
					cl.idTipoPagamento = ol.idTipoPagamento;
					cl.dtPagamento = ol.dtPagamento;
					cl.nome = ol.nome;
					cl.observacoes = ol.observacoes;
					cl.tipoLancamentoDoCaixa = ol.tipoLancamentoDoCaixa;
					cl.tipoPagamento_nome = ol.tipoPagamento_nome;
					cl.tipoPagamento_pctComissao = ol.tipoPagamento_pctComissao;
					cl.txJuroAtraso = ol.txJuroAtraso;
					cl.txJuroParcelamento = ol.txJuroParcelamento;
					cl.txMultaAtraso = ol.txMultaAtraso;
					cl.valorCobrado = ol.valorCobrado;
					cl.valorOriginal = ol.valorOriginal;
					cl.valorRecebido = ol.valorRecebido;
					arraycLancamentosCaixa.addItem(cl);
				}
			}
		}
		else
		{
			for each (var cxL:Cx_Lancamento in App.single.cache.arrayCx_Lancamento)
			{
				if (cxL.idTransacao == movRet.idTransacao)
					arraycLancamentosCaixa.addItem(cxL.clone());
			}
		}
		
		arraycItensCarrinhoOriginal.removeAll();
		for each (var miC:MovItem in arraycItensCarrinho)
			arraycItensCarrinhoOriginal.addItem(miC.clone());
		for each (var miO:MovItem in arraycItensCarrinhoOriginal)
		{
			if (miO.vlrDescUnit == 0)
			{
				miO.vlrUnitVendaFinal = miO.vlrUnitVendaInicial;
				miO.vlrUnitVendaFinalQtd = miO.vlrUnitVendaFinal * miO.qtd;
			}
		}
		sistema_calcula_valor_carrinho_importado();
		sistema_define_valor_falta_jogar_para_titulos();
		sistema_verifica_desconto_importacao();
	}
	
	private function sistema_calcula_valor_carrinho_importado():void
	{
		totalDesc = 0;
		indicador_itenscarrinho_valor_desconto = 0;
		indicador_itenscarrinho_valor_desconto_unit = 0;
		
		for each(var miD:MovItem in arraycItensCarrinho)
		{
			indicadoroculto_itenscarrinho_valor_desconto_maximo += miD.vlrDescMax * miD.qtd;
			indicador_itenscarrinho_valor_desconto_unit += miD.vlrDescUnit;
			
			totalDesc += miD.vlrDesc * miD.qtd;
		}
		indicadoroculto_itenscarrinho_valor_desconto_maximo = Formatadores.unica.currencyFormatter(indicadoroculto_itenscarrinho_valor_desconto_maximo);
		indicador_itenscarrinho_valor_desconto_unit = Formatadores.unica.currencyFormatter(indicador_itenscarrinho_valor_desconto_unit);
		totalDesc = Formatadores.unica.currencyFormatter(totalDesc);
		
		indicador_itenscarrinho_valor_bruto = 0;
		indicador_itenscarrinho_valor_liquido = 0;
		indicadoroculto_itenscarrinho_valor_desconto_maximo = 0;
		indicadoroculto_itenscarrinho_valor_comissao = 0;	
		indicador_itenscarrinho_qtd = 0;
		mov.vlrItensInicial = 0;
		mov.vlrItensFinal = 0;
		
		
		for each (var mi:MovItem in arraycItensCarrinho)
		{
			mi.vlrUnitVendaFinalQtd = Formatadores.unica.currencyFormatter(mi.vlrUnitVendaFinal * mi.qtd);
			mi.vlrComissaoPreco = Formatadores.unica.currencyFormatter(mi.vlrUnitVendaFinalQtd * mi.pctComissaoPreco / 100);
						
			mov.vlrItensInicial += Formatadores.unica.currencyFormatter(mi.vlrUnitVendaInicial * mi.qtd);
			mov.vlrItensFinal += Formatadores.unica.currencyFormatter(mi.vlrUnitVendaFinal * mi.qtd);
			
			indicadoroculto_itenscarrinho_valor_desconto_maximo += mi.vlrDescMax * mi.qtd;
			indicadoroculto_itenscarrinho_valor_comissao += mi.vlrComissaoPreco;
			indicador_itenscarrinho_qtd += mi.qtd;
		}
		
		mov.vlrTotal = Formatadores.unica.currencyFormatter(mov.vlrItensFinal + mov.vlrAcrescimo);
		indicador_itenscarrinho_valor_bruto = mov.vlrItensInicial;
		indicador_itenscarrinho_valor_liquido = mov.vlrItensFinal;
		
		if(indicador_itenscarrinho_valor_desconto < 0)
			lbDesconto.text="Acréscimo";
		
		if(indicador_itenscarrinho_valor_desconto > 0)
			lbDesconto.text="Desconto";
		
		indicador_itenscarrinho_valor_desconto = totalDesc;
	}
	
	private function sistema_verifica_desconto_importacao():void
	{
		var totalDescUnit:Number = 0;
		for each (var mi:MovItem in arraycItensCarrinho)
		{
			totalDescUnit += mi.vlrDescUnit * mi.qtd;
		}
		totalDescUnit = Formatadores.unica.currencyFormatter(totalDescUnit);
		
		if (totalDescUnit != indicador_itenscarrinho_valor_desconto)
			isDescontoLancado = true;
		else
			isDescontoLancado = false;
		
		desconto_emValor.value = 0;
		desconto_emPct.value = 0;
		desconto_final.value = 0;
		rbEmReais.selected = true;
	}
	
	private function usuario_fecha_popup_importa_mov():void
	{
		PopUpManager.removePopUp(popup_importar_venda);
	}