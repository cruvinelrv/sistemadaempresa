
import Core.App;
import Core.Sessao;

import SDE.CamadaNuvem.NuvemModificacoes;
import SDE.Enumerador.ERelatorio;

import flash.net.navigateToURL;

import img.Imagens;

import mx.controls.Alert;
import mx.core.Application;

	private var nuvemModificacoes:NuvemModificacoes = App.single.n.modificacoes;
	
	private function gerarRelListaTelefone(tipoImpressao:String):void
	{
		if (tipoImpressao == "PDF")
		{
			if (!Application.application.gerenteConexaoDesktop)
			{
				Alert.show("É necessário estar conectado ao SDE Desktop para exibição em PDF", "Mensagem SDE", 4, null, null, Imagens.unica.icn_32_alerta);
				return;
			}
			
			nuvemModificacoes.RelListaTelefone(
				function(retorno:Number):void
				{
					if (retorno == 0)
					{
						Alert.show("Ocorreu um erro durante a construção do relatório", "Mensagem SDE", 4, null, null, Imagens.unica.icn_32_info);
					}
					else
						Application.application.gerenteConexaoDesktop.baixaRelatorio(Sessao.unica.idCorp, ERelatorio.lista_telefone, "Lista de Telefone");
				}
			);
		}
		else if (tipoImpressao == "WEB")
		{
			Alert.show("Exibição em Página Web não disponível para este relatório", "Mensagem SDE", 4, null, null, Imagens.unica.icn_32_info);
		}
	}
	
	private function gerarRelFichaCliente(tipoImpressao:String):void
	{
		if (tipoImpressao == "PDF")
		{
			if (!Application.application.gerenteConexaoDesktop)
			{
				Alert.show("É necessário estar conectado ao SDE Desktop para exibição em PDF", "Mensagem SDE", 4, null, null, Imagens.unica.icn_32_alerta);
				return;
			}
			
			nuvemModificacoes.RelFichaCliente(
				function(retorno:Number):void
				{
					if (retorno == 0)
					{
						Alert.show("Ocorreu um erro durante a construção do relatório", "Mensagem SDE", 4, null, null, Imagens.unica.icn_32_info);
					}
					else
						Application.application.gerenteConexaoDesktop.baixaRelatorio(Sessao.unica.idCorp, ERelatorio.ficha_cliente, "Ficha de Clientes");
				}
			);
		}
		else if (tipoImpressao == "WEB")
		{
			Alert.show("Exibição em Página Web não disponível para este relatório", "Mensagem SDE", 4, null, null, Imagens.unica.icn_32_info);
		}
	}
	
	private function gerarRelEstoque(tipoImpressao:String):void
	{
		if (tipoImpressao == "PDF")
		{
			Alert.show("Exibição em PDF não disponível para este relatório", "Mensagem SDE", 4, null, null, Imagens.unica.icn_32_info);
		}
		else if (tipoImpressao == "WEB")
		{
			vars.relatorio = ERelatorio.estoque;
			vars.exibeGrade = rbRelEstoqueComGrade.selected;
			vars.exibeEstoqueZerado = ckbRelEstoqueExibeItensSemEstoque.selected;
			navigateToURL(url, "_blank");
			
			limpaUrl();
		}
	}
	
	private function gerarRelListaPrecos(tipoImpressao:String):void
	{
		var idMarca:Number = (cmbRelListaPrecosMarca.selectedIndex == 0) ? 0 : cmbRelListaPrecosMarca.selectedItem.id;
		
		if (tipoImpressao == "PDF")
		{
			if (!Application.application.gerenteConexaoDesktop)
			{
				Alert.show("É necessário estar conectado ao SDE Desktop para exibição em PDF", "Mensagem SDE", 4, null, null, Imagens.unica.icn_32_alerta);
				return;
			}
			
			nuvemModificacoes.RelListaPrecos(idMarca,
				function(retorno:Number):void
				{
					if (retorno == 0)
						Alert.show("Ocorreu um erro durante a construção do relatório", "Mensagem SDE", 4, null, null, Imagens.unica.icn_32_info);
					else
						Application.application.gerenteConexaoDesktop.baixaRelatorio(Sessao.unica.idCorp, ERelatorio.lista_preco, "Lista de Preços");
				}
			);
		}
		else if (tipoImpressao == "WEB")
		{
			vars.relatorio = ERelatorio.lista_preco;
			vars.idMarca = idMarca;
			navigateToURL(url, "_blank");
			
			limpaUrl();
		}
	}
	
	private function gerarRelListagemParaBalanco(tipoImpressao:String):void
	{
		var campoOrdenacao:String = (rbRelListagemBalancoNome.selected) ? "nome" : "id";
		
		if (tipoImpressao == "PDF")
		{
			if (!Application.application.gerenteConexaoDesktop)
			{
				Alert.show("É necessário estar conectado ao SDE Desktop para exibição em PDF", "Mensagem SDE", 4, null, null, Imagens.unica.icn_32_alerta);
				return;
			}
			
			nuvemModificacoes.RelListagemBalanco(campoOrdenacao,
				function(retorno:Number):void
				{
					if (retorno == 0)
						Alert.show("Ocorreu um erro durante a construção do relatório", "Mensagem SDE", 4, null, null, Imagens.unica.icn_32_info);
					else
						Application.application.gerenteConexaoDesktop.baixaRelatorio(Sessao.unica.idCorp, ERelatorio.listagem_balanco, "Listagem Para Balanço");
				}
			);
		}
		else if (tipoImpressao == "WEB")
		{
			vars.relatorio = ERelatorio.listagem_balanco;
			vars.campoOrdenacao = campoOrdenacao;
			navigateToURL(url, "_blank");
			
			limpaUrl();
		}
	}
	
	private function gerarRelEspelhoMovimentacoes(tipoImpressao:String):void
	{
		if (tipoImpressao == "PDF")
		{
			Alert.show("Exibição em PDF não disponível para este relatório", "Mensagem SDE", 4, null, null, Imagens.unica.icn_32_info);
		}
		else if (tipoImpressao == "WEB")
		{
			if (dfEspelhoMovDataInicial.text == "" || dfEspelhoMovDataFinal.text == "")
			{
				Alert.show("Selecionar as datas inicial e final para gerar o relatório.", "Mensagem SDE", 4, null, null, Imagens.unica.icn_32_alerta);
				return;
			}
			
			vars.relatorio = ERelatorio.espelho_mov;
			
			vars.dtInicial = dfEspelhoMovDataInicial.text;
			vars.dtFinal = dfEspelhoMovDataFinal.text;
			vars.idCliente = (clienteSelecionado) ? clienteSelecionado.id : "0";
			vars.idVendedor = (funcionarioSelecionado) ? funcionarioSelecionado.id : "0";
			vars.idItem = (itemSelecionado) ? itemSelecionado.id : "0";
			vars.idMov = (movSelecionada) ? movSelecionada.id: "0";
			
			vars.entrada_compra = ckbTipoMovCompra.selected;
			vars.saida_venda = ckbTipoMovVenda.selected;
			vars.outros_orcamento = ckbTipoMovOrcamento.selected;
			vars.entrada_cancel = ckbTipoMovCompraCancelada.selected;
			vars.saida_cancel = ckbTipoMovVendaCancelada.selected;
			vars.ambos_ajuste_estoque = ckbTipoMovAjusteEstoque.selected;
			vars.ambos_balan = ckbTipoMovBalanco.selected;
			if (currentState == "stateTipoMovOculta")
			{
				vars.outros_reserva = ckbTipoMovReserva.selected;
				vars.outros_pedido = ckbTipoMovPedido.selected;
			}
			else
			{
				vars.outros_reserva = false;
				vars.outros_pedido = false;
			}
			
			vars.sem_impressao = ckbTipoImpressaoSemImpressao.selected;
			vars.nfe_produto = ckbTipoImpressaoNfe.selected;
			vars.nf_formulario = ckbTipoImpressaoNf.selected;
			vars.cupom_fiscal = ckbTipoImpressaoCupomFiscal.selected;
			vars.orcamento = ckbTipoImpressaoOrcamento.selected;
			if (currentState == "stateTipoMovOculta")
			{
				vars.reserva = ckbTipoImpressaoReserva.selected;
				vars.pedido = ckbTipoImpressaoPedido.selected;
			}
			else
			{
				vars.reserva = false;
				vars.pedido = false;
			}
			
			navigateToURL(url, "_blank");
			
			limpaUrl();
		}
	}
	
	private function gerarRelCaixa(tipoImpressao:String):void
	{
		if (tipoImpressao == "PDF")
		{
			Alert.show("Exibição em PDF não disponível para este relatório", "Mensagem SDE", 4, null, null, Imagens.unica.icn_32_info);
		}
		else if (tipoImpressao == "WEB")
		{
			if (dfRelCaixa.text == "")
			{
				Alert.show("Selecionar a data do caixa para gerar o relatório.", "Mensagem SDE", 4, null, null, Imagens.unica.icn_32_alerta);
				return;
			}
			
			vars.relatorio = ERelatorio.caixa;
			vars.dtCaixa = dfRelCaixa.text;
			vars.corporativo = false;
			navigateToURL(url, "_blank");
			
			limpaUrl();
		}
	}
	
	private function gerarRelPisCofins(tipoImpressao:String):void
	{
		if (tipoImpressao == "PDF")
		{
			Alert.show("Exibição em PDF não disponível para este relatório", "Mensagem SDE", 4, null, null, Imagens.unica.icn_32_info);
		}
		else if (tipoImpressao == "WEB")
		{
			if (dfRelPisCofinsDataInicial.text == "" || dfRelPisCofinsDataFinal.text == "")
			{
				Alert.show("Selecionar as datas inicial e final para gerar o relatório.", "Mensagem SDE", 4, null, null, Imagens.unica.icn_32_alerta);
				return;
			}
			
			vars.relatorio = ERelatorio.pis_cofins;
			vars.dtInicial = dfRelPisCofinsDataInicial.text;
			vars.dtFinal = dfRelPisCofinsDataFinal.text;
			navigateToURL(url, "_blank");
			
			limpaUrl();
		}
	}
	
	private function gerarRelExtratoContaCorrenteCaixa(tipoImpressao:String):void
	{
		if (tipoImpressao == "PDF")
		{
			Alert.show("Exibição em PDF não disponível para este relatório", "Mensagem SDE", 4, null, null, Imagens.unica.icn_32_info);
		}
		else if (tipoImpressao == "WEB")
		{
			if (dfRelExtratoContaCorrenteCaixaDataInicial.text == "" || dfRelExtratoContaCorrenteCaixaDataFinal.text == "")
			{
				Alert.show("Selecionar as datas inicial e final para gerar o relatório.", "Mensagem SDE", 4, null, null, Imagens.unica.icn_32_alerta);
				return;
			}
			
			vars.relatorio = ERelatorio.extrato_conta_corrente_caixa;
			vars.dtInicial = dfRelExtratoContaCorrenteCaixaDataInicial.text;
			vars.dtFinal = dfRelExtratoContaCorrenteCaixaDataFinal.text;
			vars.idConta = (cmbRelExtratoContaCorrenteCaixaConta.selectedIndex == 0) ? 0 : cmbRelExtratoContaCorrenteCaixaConta.getAs().id;
			vars.idCentroCusto = (cmbRelExtratoContaCorrenteCaixaCentroCusto.selectedIndex == 0) ? 0 : cmbRelExtratoContaCorrenteCaixaCentroCusto.getAs().id;
			vars.idPlanoConta = (cmbRelExtratoContaCorrenteCaixaPlanoConta.selectedIndex == 0) ? 0 : cmbRelExtratoContaCorrenteCaixaPlanoConta.getAs().id;
			navigateToURL(url, "_blank");
			
			limpaUrl();
		}
	}
	
	private function gerarRelTitulosReceberPagar(tipoImpressao:String):void
	{
		if (tipoImpressao == "PDF")
		{
			Alert.show("Exibição em PDF não disponível para este relatório", "Mensagem SDE", 4, null, null, Imagens.unica.icn_32_info);
		}
		else if (tipoImpressao == "WEB")
		{
			if (dfRelTitulosReceberPagarDataInicial.text == "" || dfRelTitulosReceberPagarDataFinal.text == "")
			{
				Alert.show("Selecionar as datas inicial e final para gerar o relatório.", "Mensagem SDE", 4, null, null, Imagens.unica.icn_32_alerta);
				return;
			}
			
			vars.relatorio = ERelatorio.titulos_receber_pagar;
			vars.dtInicial = dfRelTitulosReceberPagarDataInicial.text;
			vars.dtFinal = dfRelTitulosReceberPagarDataFinal.text;
			vars.idCliente = (clienteSelecionado) ? clienteSelecionado.id : "0";
			vars.idPortador = (cmbRelTitulosReceberPagarPortador.selectedIndex == 0) ? 0 : cmbRelTitulosReceberPagarPortador.getAs().id;
			vars.titulo_a_pagar = ckbRelTitulosReceberPagarContasPagar.selected;
			vars.titulo_a_receber = ckbRelTitulosReceberPagarContasReceber.selected;
			vars.em_aberto = ckbRelTitulosReceberPagarEmAberto.selected;
			vars.lancado = ckbRelTitulosReceberPagarRecebidoPago.selected;
			navigateToURL(url, "_blank");
			
			limpaUrl();
		}
	}
	
	private function gerarRelCheques(tipoImpressao:String):void
	{
		if (tipoImpressao == "PDF")
		{
			Alert.show("Exibição em PDF não disponível para este relatório", "Mensagem SDE", 4, null, null, Imagens.unica.icn_32_info);
		}
		else if (tipoImpressao == "WEB")
		{
			if (dfRelChequeDataInicial.text == "" || dfRelChequeDataFinal.text == "")
			{
				Alert.show("Selecionar as datas inicial e final para gerar o relatório.", "Mensagem SDE", 4, null, null, Imagens.unica.icn_32_alerta);
				return;
			}
			
			vars.relatorio = ERelatorio.cheque;
			vars.dtInicial = dfRelChequeDataInicial.text;
			vars.dtFinal = dfRelChequeDataFinal.text;
			vars.cheques_a_receber = ckbdfRelChequeReceber.selected;
			vars.cheques_baixados = ckbdfRelChequeBaixados.selected;
			vars.cheques_compensados = ckbdfRelChequeCompensados.selected;
			vars.cheques_devolvidos = ckbdfRelChequeDevolvidos.selected;
			navigateToURL(url, "_blank");
			
			limpaUrl();
		}
	}
	
	private function gerarRelComissionamentoDinamico(tipoImpressao:String):void
	{
		if (tipoImpressao == "PDF")
		{
			Alert.show("Exibição em PDF não disponível para este relatório", "Mensagem SDE", 4, null, null, Imagens.unica.icn_32_info);
		}
		else if (tipoImpressao == "WEB")
		{
			if (dfRelComissionamentoDataInicial.text == "" || dfRelComissionamentoDataFinal.text == "")
			{
				Alert.show("Selecionar as datas inicial e final para gerar o relatório.", "Mensagem SDE", 4, null, null, Imagens.unica.icn_32_alerta);
				return;
			}
			
			vars.relatorio = ERelatorio.comissionamento;
			vars.dtInicial = dfRelComissionamentoDataInicial.text;
			vars.dtFinal = dfRelComissionamentoDataFinal.text;
			vars.idFuncionario = (funcionarioSelecionado) ? funcionarioSelecionado.id : "0";
			vars.exibeMovimentacao = ckbRelComissionamentoExibeMov.selected;
			navigateToURL(url, "_blank");
			
			limpaUrl();
		}
	}
	
	private function gerarRelProdutosVendidosPeriodo(tipoImpressao:String):void
	{
		if (tipoImpressao == "PDF")
		{
			Alert.show("Exibição em PDF não disponível para este relatório", "Mensagem SDE", 4, null, null, Imagens.unica.icn_32_info);
		}
		else if (tipoImpressao == "WEB")
		{
			if (dfRelProdutosVendidosPeriodoDataInicial.text == "" || dfRelProdutosVendidosPeriodoDataFinal.text == "")
			{
				Alert.show("Selecionar as datas inicial e final para gerar o relatório.", "Mensagem SDE", 4, null, null, Imagens.unica.icn_32_alerta);
				return;
			}
			
			vars.relatorio = ERelatorio.produtos_vendidos_periodo;
			vars.dtInicial = dfRelProdutosVendidosPeriodoDataInicial.text;
			vars.dtFinal = dfRelProdutosVendidosPeriodoDataFinal.text;
			vars.idItem = (itemSelecionado) ? itemSelecionado.id : "0";
			navigateToURL(url, "_blank");
			
			limpaUrl();
		}
	}
	
	private function gerarRelAgrodefesa(tipoImpressao:String):void
	{
		if (tipoImpressao == "PDF")
		{}
		else if (tipoImpressao == "WEB")
		{}
	}
	
	private function gerarRelInventario(tipoImpressao:String):void
	{
		if (tipoImpressao == "PDF")
		{}
		else if (tipoImpressao == "WEB")
		{}
	}
	private function gerarRelListaProdutoTributacao(tipoImpressao:String):void
	{
		var idMarca:Number = (cmbRelListaProdMarcaTrib.selectedIndex == 0) ? 0 : cmbRelListaProdMarcaTrib.selectedItem.id;
		
		if (tipoImpressao == "PDF")
		{
			if (!Application.application.gerenteConexaoDesktop)
			{
				Alert.show("É necessário estar conectado ao SDE Desktop para exibição em PDF", "Mensagem SDE", 4, null, null, Imagens.unica.icn_32_alerta);
				return;
			}
			
			nuvemModificacoes.RelListaProdutoTributacao(idMarca,
				function(retorno:Number):void
				{
					if (retorno == 0)
						Alert.show("Ocorreu um erro durante a construção do relatório", "Mensagem SDE", 4, null, null, Imagens.unica.icn_32_info);
					else
						Application.application.gerenteConexaoDesktop.baixaRelatorio(Sessao.unica.idCorp, ERelatorio.lista_prod_tributo, "Lista de Produtos com Tributação");
				}
			);
		}
		else if (tipoImpressao == "WEB")
		{
			vars.relatorio = ERelatorio.lista_prod_tributo;
			vars.idMarca = idMarca;
			navigateToURL(url, "_blank");
			
			limpaUrl();
		}
	}

	
	