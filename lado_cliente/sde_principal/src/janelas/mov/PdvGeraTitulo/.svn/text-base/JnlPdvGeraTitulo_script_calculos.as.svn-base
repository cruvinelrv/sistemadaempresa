	import Core.Utils.Formatadores;
	
	import SDE.Entidade.MovItem;
	
	import mx.controls.Alert;

	[Bindable] private var indicador_itenscarrinho_qtd:Number = 0;
	[Bindable] private var indicador_itenscarrinho_valor_bruto:Number = 0;
	[Bindable] private var indicador_itenscarrinho_valor_desconto:Number = 0;
	[Bindable] private var indicador_itenscarrinho_valor_desconto_pct:Number = 0;
	[Bindable] private var indicador_itenscarrinho_valor_desconto_unit:Number = 0;
	[Bindable] private var indicador_itenscarrinho_valor_juro:Number = 0;
	[Bindable] private var indicador_itenscarrinho_valor_liquido:Number = 0;
	
	[Bindable] private var indicador_venda_valor_final:Number = 0;
	[Bindable] private var indicador_venda_valor_pagamento_indefinido:Number = 0;
	[Bindable] private var indicador_venda_valor_juro_parcelamento:Number = 0;
	
	[Bindable] private var indicadoroculto_itenscarrinho_valor_desconto_maximo:Number = 0;
	[Bindable] private var indicadoroculto_itenscarrinho_valor_comissao:Number = 0;
	
	[Bindable] private var desconto_Valor:Number = 0;
	[Bindable] private var totalDesc:Number = 0;
	
	[Bindable] private var isDescontoLancado:Boolean = false;
	
	private function sistema_calcular_valor_carrinho():void
	{		
		totalDesc = 0;
		arraycItensCarrinho.removeAll();
		usuario_reseta_titulos();
		
		for each (var mClone:MovItem in arraycItensCarrinhoOriginal)
			arraycItensCarrinho.addItem(mClone.clone());
		
		btnSalvaDesc.enabled = false;
		
		indicador_itenscarrinho_valor_desconto = 0;
		indicador_itenscarrinho_valor_desconto_unit = 0;
		
		var desconto_a_distribuir:Number = desconto_emValor.value;
		
		for each(var m:MovItem in arraycItensCarrinho)
		{
			m.vlrDesc = 0;
		}
		
		for each(var miInd:MovItem in arraycItensCarrinho)
		{
			indicadoroculto_itenscarrinho_valor_desconto_maximo += miInd.vlrDescMax * miInd.qtd;
		}
		indicadoroculto_itenscarrinho_valor_desconto_maximo = Formatadores.unica.currencyFormatter(indicadoroculto_itenscarrinho_valor_desconto_maximo);
		
		var desconto_distribuido:Number = 0;
		for each(var miD:MovItem in arraycItensCarrinho)
		{
			if(Formatadores.unica.currencyFormatter(desconto_a_distribuir / miD.qtd) > Formatadores.unica.currencyFormatter(miD.vlrDescMax - miD.vlrDescUnit))
			{
				miD.vlrDesc = Formatadores.unica.currencyFormatter(miD.vlrDescMax - miD.vlrDescUnit);
				desconto_a_distribuir = Formatadores.unica.currencyFormatter(desconto_a_distribuir - miD.vlrDesc * miD.qtd);
				miD.vlrUnitVendaFinal = Formatadores.unica.currencyFormatter(miD.vlrUnitVendaFinal - miD.vlrDesc);
				miD.vlrUnitVendaFinalQtd = Formatadores.unica.currencyFormatter(miD.vlrUnitVendaFinal * miD.qtd);
				totalDesc += miD.vlrDesc * miD.qtd + miD.vlrDescUnit * miD.qtd;
				
				indicador_itenscarrinho_valor_desconto_unit += miD.vlrDescUnit;
				
			}
			else
			{
				miD.vlrDesc = Formatadores.unica.currencyFormatter((desconto_a_distribuir > miD.vlrDescMax * miD.qtd) ? miD.vlrDescMax : desconto_a_distribuir / miD.qtd);
				desconto_a_distribuir -= Formatadores.unica.currencyFormatter(miD.vlrDesc * miD.qtd);
				miD.vlrUnitVendaFinal = miD.vlrUnitVendaFinal - miD.vlrDesc;
				miD.vlrUnitVendaFinalQtd = Formatadores.unica.currencyFormatter(miD.vlrUnitVendaFinal * miD.qtd);
				totalDesc += miD.vlrDesc * miD.qtd + miD.vlrDescUnit * miD.qtd;
				indicador_itenscarrinho_valor_desconto_unit += miD.vlrDescUnit * miD.qtd;
			}
		}
		totalDesc = Formatadores.unica.currencyFormatter(totalDesc);
		indicador_itenscarrinho_valor_desconto_unit = Formatadores.unica.currencyFormatter(indicador_itenscarrinho_valor_desconto_unit);
		
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
		sistema_define_valor_falta_jogar_para_titulos();		
	}
	
	private function sistema_calcular_valor_abaDesconto():void
	{
		var valorLancado:Number = 0;
		var valorMaximo:Number = 0;
		var habilitarSalvarDesconto:Boolean = true;
		
		if (rbEmReais.selected)
		{
			valorLancado = Formatadores.unica.currencyFormatter(desconto_emValor.value);
			valorMaximo = Formatadores.unica.currencyFormatter(indicadoroculto_itenscarrinho_valor_desconto_maximo - indicador_itenscarrinho_valor_desconto);
			
			if (valorLancado > valorMaximo)
			{
				Alert.show("O Valor de desconto informado ultrapassa ao Máximo de Desconto permitido para esta venda.\nSaldo de Desconto: "+Formatadores.unica.formataValor(indicadoroculto_itenscarrinho_valor_desconto_maximo - indicador_itenscarrinho_valor_desconto,true), "Mensagem SDE", 4, null, null, Imagens.unica.icn_32_alerta);																													    								
				desconto_emValor.value = 0;
				desconto_emPct.value = 0;
				desconto_emValor.value = valorMaximo;
				habilitarSalvarDesconto = false;
			}
			else
			{
				desconto_emValor.value = valorLancado;
				desconto_emPct.value = ((indicadoroculto_itenscarrinho_valor_desconto_maximo-indicador_itenscarrinho_valor_desconto)*100)/(indicador_itenscarrinho_valor_bruto-indicador_itenscarrinho_valor_desconto_unit);
				desconto_final.value = Formatadores.unica.currencyFormatter(indicador_itenscarrinho_valor_bruto-(indicador_itenscarrinho_valor_desconto_unit + desconto_emValor.value));
			}
		}
		
		else if (rbEmPercent.selected)
		{
			valorLancado = Number(Formatadores.unica.formataValor(desconto_emPct.value, false));
			valorMaximo = Number(Formatadores.unica.formataValor(((indicadoroculto_itenscarrinho_valor_desconto_maximo - indicador_itenscarrinho_valor_desconto) * 100) / (indicador_itenscarrinho_valor_bruto - indicador_itenscarrinho_valor_desconto_unit), false));
			
			if (valorLancado > valorMaximo)
			{
				Alert.show("O Percentual de desconto informado ultrapassa ao Máximo de Desconto permitido para esta venda.\nSaldo de Desconto: "+Formatadores.unica.formataPorcentagem(((indicadoroculto_itenscarrinho_valor_desconto_maximo - indicador_itenscarrinho_valor_desconto) * 100) / (indicador_itenscarrinho_valor_bruto - indicador_itenscarrinho_valor_desconto_unit)), "Mensagem SDE", 4, null, null, Imagens.unica.icn_32_alerta);
				desconto_emValor.value = 0;
				desconto_emPct.value = 0;
				desconto_emPct.value = valorMaximo;
				habilitarSalvarDesconto = false;
			}
			else
			{
				desconto_emPct.value = valorLancado;
				desconto_emValor.value = Formatadores.unica.currencyFormatter(((indicador_itenscarrinho_valor_bruto-indicador_itenscarrinho_valor_desconto_unit)*desconto_emPct.value)/100);
				desconto_final.value = Formatadores.unica.currencyFormatter((indicador_itenscarrinho_valor_bruto-indicador_itenscarrinho_valor_desconto_unit) - desconto_emValor.value);
			}
		}
		
		else if (rbVlrFinal.selected)
		{
			valorLancado = Formatadores.unica.currencyFormatter(desconto_final.value);
			valorMaximo = Formatadores.unica.currencyFormatter(mov.vlrItensInicial - indicadoroculto_itenscarrinho_valor_desconto_maximo);
			
			if (valorLancado < valorMaximo)
			{
				Alert.show("O Valor de desconto informado ultrapassa ao Máximo de Desconto permitido para esta venda", "Mensagem SDE", 4, null, null, Imagens.unica.icn_32_alerta);
				desconto_emValor.value = 0;
				desconto_emPct.value = 0;
				desconto_final.value = valorMaximo;
				habilitarSalvarDesconto = false;
			}
			else
			{
				desconto_final.value = valorLancado;
				desconto_emValor.value = Formatadores.unica.currencyFormatter(mov.vlrItensInicial - (desconto_final.value + indicador_itenscarrinho_valor_desconto));
				desconto_emPct.value = Formatadores.unica.currencyFormatter((desconto_emValor.value * 100) / (mov.vlrItensInicial - indicador_itenscarrinho_valor_desconto));
			}
		}
		
		btnSalvaDesc.enabled = habilitarSalvarDesconto;
	}
	
	private function calculoValorFinal(valor:Number):void
	{
		mov.vlrTotal = Formatadores.unica.currencyFormatter(valor);
		mov.vlrAcrescimo = Formatadores.unica.currencyFormatter(mov.vlrItensFinal - valor);
	}