import Core.Alerta.AlertaSistema;
import Core.App;
import Core.Utils.Formatadores;

import SDE.Entidade.Cx_Lancamento;
import SDE.Entidade.Finan_TipoPagamento;

import mx.controls.Alert;
	
[Bindable]private var movGeraCarne:Boolean = false;
	
	private function usuario_reseta_titulos():void
	{
		sistema_limpar_lancamentos();
		sistema_define_valor_falta_jogar_para_titulos();
		nsVlrFatiaTitulo.setFocus();
		movGeraCarne=false;
	}
	
	private function sistema_define_valor_falta_jogar_para_titulos():void
	{
		lancamento_em_edicao = null;
		
		AlertaSistema.mensagem('avaliando quanto falta jogar em parcelas...',true);
		
		var somatorio_valor_original:Number=0;
		indicador_venda_valor_final = 0;
		for each (var t:Cx_Lancamento in arraycLancamentosCaixa)
		{
			somatorio_valor_original += t.valorOriginal;
			indicador_venda_valor_final += t.valorCobrado;
		}
		
		indicador_venda_valor_pagamento_indefinido
			= indicador_itenscarrinho_valor_liquido
			- somatorio_valor_original;
		
		indicador_venda_valor_juro_parcelamento = indicador_venda_valor_final - somatorio_valor_original;
		if (indicador_venda_valor_juro_parcelamento<0)
			indicador_venda_valor_juro_parcelamento=0;
		nsVlrFatiaTitulo.value = Formatadores.unica.currencyFormatter(indicador_venda_valor_pagamento_indefinido);
		cmbTiposPgto.setIdentificador(0);
	}
	
	private function usuario_lanca_fatia_pagamento():void
	{
		if(!(nsVlrFatiaTitulo.value > Formatadores.unica.currencyFormatter(indicador_venda_valor_pagamento_indefinido)))
		{
			var ftp:Finan_TipoPagamento = cmbTiposPgto.getAs();
			
			var cxL:Cx_Lancamento = new Cx_Lancamento();
			cxL.valorOriginal = nsVlrFatiaTitulo.value;
			cxL.valorCobrado = 0;
			
			cxL.idTipoPagamento = ftp.id;
			cxL.idClientePagador = cliente_selecionado.id;
			cxL.tipoPagamento_nome = ftp.nome;
			cxL.tipoPagamento_geraContasPagar = ftp.geraContasPagar;
			cxL.tipoPagamento_geraContasReceber = ftp.geraContasReceber;
			cxL.tipoPagamento_pctComissao = ftp.pctComissao;
			cxL.idPortador = ftp.idPortador;
			cxL.idGrupoTipoPagamento = ftp.idGrupoTipoPagamento;
			cxL.grupoTipoPagamento_nome = ftp.grupoTipoPagamento_nome;
			cxL.txJuroParcelamento = ftp.txJuroParcelamento;
			cxL.txMultaAtraso = ftp.txMultaAtraso;
			
			cxL.dtPagamento = Formatadores.unica.formataData(new Date());
			
			lancamento_em_edicao = cxL;
			
			if (!ftp.ehPrazo)
			{
				if(arraycLancamentosCaixa!=null && arraycLancamentosCaixa.length>0)
				{
					for each(var alc:Cx_Lancamento in arraycLancamentosCaixa)
					{
						if(alc.tipoPagamento_nome==cxL.tipoPagamento_nome)
						{
							alc.valorOriginal=cxL.valorOriginal;
							alc.valorCobrado=cxL.valorOriginal;
							sistema_define_valor_falta_jogar_para_titulos();
							nsVlrFatiaTitulo.setFocus();
							break;
						}
						else
						{
							cxL.valorCobrado = cxL.valorOriginal;
							cxL.valorRecebido = cxL.valorOriginal;
							arraycLancamentosCaixa.addItem(cxL);
							sistema_define_valor_falta_jogar_para_titulos();
							nsVlrFatiaTitulo.setFocus();	
						}
					}
				}
				else
				{
					cxL.valorCobrado = cxL.valorOriginal;
					cxL.valorRecebido = cxL.valorOriginal;
					arraycLancamentosCaixa.addItem(cxL);
					sistema_define_valor_falta_jogar_para_titulos();
					nsVlrFatiaTitulo.setFocus();	
				}
			}
			else
				sistema_abre_popup_titulo(ftp);
				
				
				indicador_venda_valor_juro_parcelamento = cxL.txJuroParcelamento;
		}
		else
			{
				nsVlrFatiaTitulo.value = Formatadores.unica.currencyFormatter(indicador_venda_valor_pagamento_indefinido);
			}
			
	}
	
	private function sistema_abre_popup_titulo(ftp:Finan_TipoPagamento):void
	{		
		popupTituloDetalhe_dt.selectedDate = new Date();
		popupTituloDetalhe_obs.text = "";
		popupTituloDetalhe_juroParcela.value = ftp.txJuroParcelamento;
		popupTituloDetalhe_periodo.value = ftp.periodo;
		popupTituloDetalhe_qtdParcelas.value = ftp.qtdParcelas;
		popupTituloDetalhe_juroParcela.enabled = ftp.podeAlterarJuroParcela;
		popupTituloDetalhe_periodo.enabled = ftp.podeAlterarPeriodo;
		popupTituloDetalhe_qtdParcelas.enabled = ftp.podeAlterarQtdParcelas;
		
		if (!ftp.podeAlterarPeriodo && ftp.qtdParcelas==0)
			popupTituloDetalhe_qtdParcelas.enabled=true;
		
		
		PopUpManager.addPopUp(popupTituloDetalhe, Application.application.gerenteJanelas, true);
		PopUpManager.centerPopUp(popupTituloDetalhe);
		popupTituloDetalhe_qtdParcelas.setFocus();
	}
	
	private function usuario_no_popup_escolhe_detalhes_titulo():void
	{
		if (popupTituloDetalhe_qtdParcelas.value==0)
		{
			Alert.show("digite a quantidade de parcelas","Aviso",Alert.YES,null,
					function(ev2:CloseEvent):void
		            {
	                  popupTituloDetalhe_qtdParcelas.setFocus();
		            }
				);
			
			return;
		}
		
		var dt:Date = popupTituloDetalhe_dt.selectedDate;
		
		var qtdParcelas:Number = popupTituloDetalhe_qtdParcelas.value;
		var periodo:Number = popupTituloDetalhe_periodo.value;
		var txJurosParcela:Number = Formatadores.unica.currencyFormatter(popupTituloDetalhe_juroParcela.value);
		
		if(popupTituloDetalhe_obs.text != null)		
			lancamento_em_edicao.observacoes = popupTituloDetalhe_obs.text;
		
		var total:Number = lancamento_em_edicao.valorOriginal;
		var contabilizado:Number = 0;
		var valor_parcela:Number = Math.round(total/qtdParcelas*100)/100;
		
		AlertaSistema.mensagem( "total da forma: "+total+" pct: "+qtdParcelas+" |  parcela: "+valor_parcela, true);
		
		for (var i:int = 0; i<qtdParcelas; i++)
		{
			var t:Cx_Lancamento = lancamento_em_edicao.clone();
			
			if (periodo == 0)
				dt.month += 1;
			else
				dt.date += periodo;
			
			t.dtPagamento = Formatadores.unica.formataData(dt);
			t.txJuroParcelamento = txJurosParcela;
			
			t.valorOriginal = valor_parcela;
			contabilizado += valor_parcela;
			
			AlertaSistema.mensagem( "contab: "+contabilizado, true );
			if (i+1==qtdParcelas)
				t.valorOriginal += total-contabilizado;
			
			t.valorCobrado = t.valorOriginal *(100 + t.txJuroParcelamento)/100;
			
			arraycLancamentosCaixa.addItem(t);
		}
		sistema_define_valor_falta_jogar_para_titulos();
		indicador_itenscarrinho_valor_juro = indicador_venda_valor_juro_parcelamento;
		usuario_fecha_popup_titulo();
		
	}
	
	private function usuario_fecha_popup_titulo():void
	{
		PopUpManager.removePopUp(popupTituloDetalhe);
		nsVlrFatiaTitulo.setFocus();
	}
	
	private function fn_trata_dtGridTitulosGerados_delete(ev:Event):void
	{
    	var pos:int = arraycLancamentosCaixa.getItemIndex(ev.target.data);
		arraycLancamentosCaixa.removeItemAt(pos);
		for each(var xxx:Cx_Lancamento in arraycLancamentosCaixa)
		{
			var tp:Finan_TipoPagamento = App.single.cache.getFinan_TipoPagamento(xxx.idTipoPagamento);
		}
		sistema_define_valor_falta_jogar_para_titulos();
		nsVlrFatiaTitulo.setFocus();
		
		movGeraCarne=false;
	}