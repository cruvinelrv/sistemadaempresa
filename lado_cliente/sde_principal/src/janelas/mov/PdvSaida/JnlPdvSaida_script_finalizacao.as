	import Core.Alerta.AlertaSistema;
	import Core.App;
	import Core.Ev.EventoGenerico;
	import Core.Sessao;
	
	import SDE.Constantes.Variaveis_SdeConfig;
	import SDE.Entidade.Cliente;
	import SDE.Entidade.ClienteEndereco;
	import SDE.Entidade.Cx_Lancamento;
	import SDE.Entidade.Finan_TipoPagamento;
	import SDE.Entidade.Item;
	import SDE.Entidade.ItemEmpEstoque;
	import SDE.Entidade.Mov;
	import SDE.Entidade.MovItem;
	import SDE.Entidade.SdeConfig;
	import SDE.Enumerador.EItemTipo;
	import SDE.Enumerador.EMovImpressao;
	import SDE.Enumerador.EMovResumo;
	import SDE.Enumerador.EMovTipo;
	import SDE.Enumerador.EPesTipo;
	
	import img.Imagens;
	
	import mx.collections.ArrayCollection;
	import mx.collections.Sort;
	import mx.collections.SortField;
	import mx.controls.Alert;
	import mx.core.Application;
	import mx.formatters.CurrencyFormatter;
	import mx.managers.PopUpManager;
	import mx.utils.Base64Encoder;
	
	private var _etapa3_botoes_escondidos:Number = 0;
	private var isEnviaEmail:Boolean = false;
	private function get etapa3_botoes_escondidos():Number{return _etapa3_botoes_escondidos;}
	private function set etapa3_botoes_escondidos(v:Number):void
	{
		_etapa3_botoes_escondidos=v;
		
		etapa3_btn_vendasimples.visible = (v==2);
		etapa3_btn_reserva.visible = (v==2);
	}
	
	private var etapa3_tipoMov:Number = 0;
	private var tipoMov:String = "";
	
	private var nomePdfDanfe:String = "";
	
		
	private function usuario_invoca_tef():void
	{
		tipoMov = "Venda";
		if (Application.application.gerenteConexaoDesktop==null)
		{
			AlertaSistema.mensagem( "Equipamentos não detectados" );
		}
		
		if (!verificaEstoque())
			return;
		
		var nomeVendedor:String = cpVendedor.selectedItem.nome.split(" ")[0];
		var compl:String = escreveArquivoPRZTEF();
		var linhasMultiTEF:String = escreveArquivoMultiTEF(nomeVendedor);
		
		Application.application.gerenteConexaoDesktop.escreveArquivoTEF(linhasMultiTEF, compl,
		
			function(ev:EventoGenerico):void
			{
				AlertaSistema.mensagem( "define coo: "+ ev.number, false );
				popupTEF_coo.text = ev.number.toString();
			}
		
		);
		
		PopUpManager.addPopUp(popupTEF, Application.application.gerenteJanelas, true);
		PopUpManager.centerPopUp( popupTEF );
		
	}
	
	
	private function usuario_popup_tef_cancela():void
	{
		PopUpManager.removePopUp(popupTEF);
	}
	
	private function usuario_popup_tef_salva():void
	{
		mov.cooTEF = Number(popupTEF_coo.text);
		mov.resumo = EMovResumo.saida;
		mov.impressao = EMovImpressao.cupom_fiscal;
		mov.tipo = EMovTipo.saida_venda;
		
		PopUpManager.removePopUp(popupTEF);
		
		sistema_salva_venda();
	}
	
	private function usuario_salva_venda_com_nf():void
	{
		if (!verificaEstoque())
			return;
			
		if (cmbClienteEndereco_Faturamento.selectedItem == null)
		{
			AlertaSistema.mensagem("O cliente não possui endereço, favor incluir");
			return;
		}
		mov.idClienteEndereco = (cmbClienteEndereco_Faturamento.selectedItem as ClienteEndereco).id;
		
		tipoMov = "Venda";
		if (Application.application.gerenteConexaoDesktop==null)
		{
			AlertaSistema.mensagem( "Equipamentos não detectados" );
			return;
		}
		
		mov.tipo = EMovTipo.saida_venda;
		mov.resumo = EMovResumo.saida;
		if (isNF)
			mov.impressao = EMovImpressao.nf_formulario;
		if (isNFe)
			mov.impressao = EMovImpressao.nfe_produto;
			
		if (!Validacao())
		{
			AlertaSistema.mensagem('Dados Inválidos:\nVerifique as informações inseridas.');
			return;
		}
		sistema_popula_mov_nf();
		
		
		if (mov.impressao == EMovImpressao.nf_formulario)
		{
			mov.idCliente = cliente_selecionado.id
			mov.idEmp = ss.idEmp;
			mov.idClienteFuncionarioLogado = ss.idClienteFuncionarioLogado;
			mov.idClienteFuncionarioVendedor = (mov.idClienteFuncionarioVendedor==0)?ss.idClienteFuncionarioLogado:mov.idClienteFuncionarioVendedor;
			
			if (!desconto_no_total)
			for each (var mi:MovItem in arraycItensCarrinho)
				mi.vlrDesc = mi.vlrUnitVendaInicial * mi.qtd - mi.vlrUnitVendaFinalQtd;
			
			App.single.n.modificacoes.Temp_Salva(mov, mov_nf, arraycItensCarrinho.source,
				function():void
				{
					Application.application.gerenteConexaoDesktop.exibeNotaFiscalFormulario();
				}
			);
			
			PopUpManager.addPopUp(popupConfirmaImpressao, Application.application.gerenteJanelas, true);
			PopUpManager.centerPopUp(popupConfirmaImpressao);	
		}
		if (mov.impressao == EMovImpressao.nfe_produto)
		{
			if (!desconto_no_total)
				for each (var xxx:MovItem in arraycItensCarrinho)
					xxx.vlrDesc = xxx.vlrUnitVendaInicial * xxx.qtd - xxx.vlrUnitVendaFinalQtd;
			
			var tipoNFE:String = Sessao.unica.parametrizacao.getParametro(Variaveis_SdeConfig.EMPRESA_PDV_NFEXML);
			
				if(tipoNFE == "0")
				{
					App.single.n.modificacoes.GerarTXT(mov, mov_nf, null, arraycItensCarrinho.source,
						function(retorno:Array):void
						{
							Application.application.gerenteConexaoDesktop.escreveArquivoNFE(retorno[0],retorno[1]);
							lblMensagemPopupConfirmaArquivoMessage.text = "A NFe foi autorizada?";
							lblMensagemPopupConfirmaArquivoFile.text = retorno[1]+"-nfe.txt";
						}
					);
				}
					
				if(tipoNFE == "1")
				{
					App.single.n.modificacoes.GerarXml(mov, mov_nf, null, arraycItensCarrinho.source,
						function(retorno:Array):void
						{
							Application.application.gerenteConexaoDesktop.escreveArquivoNFExml(retorno[0], retorno[1]);
							lblMensagemPopupConfirmaArquivoMessage.text = "Deseja Imprimir o DANFe?";
							nomePdfDanfe = retorno[1] + ".pdf";
						}
					);
				}
				
			PopUpManager.addPopUp(popupConfirmaArquivo, Application.application.gerenteJanelas, true);
			PopUpManager.centerPopUp(popupConfirmaArquivo);
		}
	}
	
	private function sistema_salva_venda():void
	{
		etapa3_tipoMov = 1;
		sistema_salva_mov("Salvo!");
	}
	
	private function usuario_salva_orcamento():void
	{
		tipoMov = "Orçamento";
		mov.resumo = EMovResumo.outros;
		mov.tipo = EMovTipo.outros_orcamento;
		mov.impressao = EMovImpressao.orcamento;
		mov.pagamento = popupEmiteOrcamento_lblPagamento.text;
		mov.entrega = popupEmiteOrcamento_txtEntrega.text;
		mov.frete = popupEmiteOrcamento_txtFrete.text;
		mov.impostos = popupEmiteOrcamento_txtImpostos.text;
		mov.validadeDias = Number(popupEmiteOrcamento_txtValidadeDias.text);
		etapa3_tipoMov = 2;
		sistema_salva_mov("Orçamento Salvo");
	}
	
	private function usuario_salva_reserva():void
	{
		tipoMov = "Reserva de Estoque";
		if (!verificaEstoque())
			return;
		mov.resumo = EMovResumo.outros;
		mov.tipo = EMovTipo.outros_reserva;
		mov.impressao = EMovImpressao.reserva;
		etapa3_tipoMov = 3;
		sistema_salva_mov("Reserva Salva!");
	}
	
	private function usuario_salva_pedido():void
	{
		PopUpManager.addPopUp(popupDataPedido, Application.application.gerenteJanelas, true);
		PopUpManager.centerPopUp(popupDataPedido);
		
		popupDataPedido_data.setFocus();
	}
	
	private function usuario_salva_baixa():void
	{
		if(!valida_data())
		{
			Alert.show("Insira uma data válida.\nFormato: dd/mm/aaaa", "Alerta SDE", 4, null, null, Imagens.unica.icn_32_alerta);
			return;
		}
		
		PopUpManager.removePopUp(popupDataPedido);
		
		var df:DateFormatter = new DateFormatter();
		df.formatString = "JJ:NN:SS";
		mov.dthrMovEmissao = popupDataPedido_data.text +" "+ df.format(new Date());
		
		tipoMov = "Baixa";
		if (!verificaEstoque())
			return;
		mov.resumo = EMovResumo.saida;
		mov.tipo = EMovTipo.outros_pedido;
		mov.impressao = EMovImpressao.pedido;
		etapa3_tipoMov = 3;
		sistema_salva_mov("OK!");
	}

	private function sistema_salva_mov(msg:String):void
	{
		for each (var cxLan:Cx_Lancamento in arraycLancamentosCaixa)
		{
		    var ftp:Finan_TipoPagamento = App.single.cache.getFinan_TipoPagamento(cxLan.idTipoPagamento);
		    if(!ftp.imprimeCarne)
		    	continue;
	    	else
		    {
		       movGeraCarne=true;
		       break;
		    }
		}
		for each (var cxL:Cx_Lancamento in arraycLancamentosCaixa)
		{
			var finanTipoPagamento:Finan_TipoPagamento = App.single.cache.getFinan_TipoPagamento(cxL.idTipoPagamento);
			if (finanTipoPagamento.geraContasReceber && cliente_selecionado.id == 1)
			{
				AlertaSistema.mensagem("Não é permitido gerar contas a receber para CLIENTE CONSUMIDOR");
				return;
			}
		}
		
		mov.idCliente = cliente_selecionado.id;
		mov.idEmp = ss.idEmp;
		mov.idClienteEndereco = Number(cmbClienteEndereco_Faturamento.id);
		mov.idClienteFuncionarioLogado = ss.idClienteFuncionarioLogado;
		mov.idClienteFuncionarioVendedor = (mov.idClienteFuncionarioVendedor==0)?ss.idClienteFuncionarioLogado:mov.idClienteFuncionarioVendedor;
		
		var autorizadaSemEndereco:Boolean = true;
		for each (var cxL1:Cx_Lancamento in arraycLancamentosCaixa)
		{
			var finanTipoPagamento1:Finan_TipoPagamento = App.single.cache.getFinan_TipoPagamento(cxL1.idTipoPagamento);
			if (finanTipoPagamento1.geraContasReceber)
			{
				autorizadaSemEndereco = false;
				continue;
			}
		}
		
		
		if (mov.tipo == EMovTipo.outros_orcamento && mov.impressao == EMovImpressao.orcamento)
		{
			if (cmbClienteEndereco_Faturamento.selectedItem != null)
				mov.idClienteEndereco = (cmbClienteEndereco_Faturamento.selectedItem as ClienteEndereco).id;
		}
		else if (mov.tipo == EMovTipo.saida_venda && mov.impressao == EMovImpressao.cupom_fiscal)
		{
			if (cmbClienteEndereco_Faturamento.selectedItem != null)
				mov.idClienteEndereco = (cmbClienteEndereco_Faturamento.selectedItem as ClienteEndereco).id;
		}
		else if (autorizadaSemEndereco)
		{
			if (cmbClienteEndereco_Faturamento.selectedItem != null)
				mov.idClienteEndereco = (cmbClienteEndereco_Faturamento.selectedItem as ClienteEndereco).id;
		}
		else
		{
			if (cmbClienteEndereco_Faturamento.selectedItem == null)
			{
				if (EMovTipo.outros_pedido != mov.tipo)
				{
				   AlertaSistema.mensagem("O cliente não possui endereço, favor incluir");
				   return;
				}
			}
			else
				mov.idClienteEndereco = (cmbClienteEndereco_Faturamento.selectedItem as ClienteEndereco).id;
		}
		
		if (!desconto_no_total)
			for each (var mi:MovItem in arraycItensCarrinho)
				mi.vlrDesc = mi.vlrUnitVendaInicial * mi.qtd - mi.vlrUnitVendaFinalQtd;
		
		if (isEnviaEmail && mov.tipo == EMovTipo.outros_orcamento)
			mov.isEmailEnviado = true;
		
		if (isImportadoOS){
			mov.idTransacao = idTransacaoOS;
			mov.idOperacao = idOperacaoOS;
		}
		
		Application.application.sessao.nuvens.modificacoes.Mov_Salva(//ivocação
			mov, arraycItensCarrinho.source, arraycLancamentosCaixa.source, ordemServicoImportada,
			function(retornoIdMovMapa:Number):void
			{
				var ss:Sessao = Application.application.sessao;
       
				var enc:Base64Encoder = new Base64Encoder();
				enc.encodeUTFBytes("corp"+ss.idCorp);
                var hash:String = enc.toString();
				
				if (isEnviaEmail)
				{
					var senha:String = ss.parametrizacao.getParametro(Variaveis_SdeConfig.EMPRESA_EAMIL_SENHA);
					
					if (cpEmail.text == "" && cpEmail.searchText == "")
					{
						AlertaSistema.mensagem("Digite o endereço de email");
						return;
					}
					
					var link:String = "http://sistemadaempresa.com.br/sde/imprime.aspx";
                	link+="?idCorp="	+ss.idCorp;
                    link+="&idEmp="	+ss.idEmp;
                    link+="&hash="	+hash;
                    link+="&tipo_impressao=relatorio";
                    link+="&relatorio=movimentacao";
                    link+="&idMov="+retornoIdMovMapa;
					
					var paraEmail:String;
					if (cpEmail.text != "")
						paraEmail = cpEmail.text;
					else if (cpEmail.searchText != "")
						paraEmail = cpEmail.searchText;
					
					ss.nuvens.modificacoes.EnviaEmail(retornoIdMovMapa, mov.idCliente, txtDeEmail.text, txtDeNome.text, senha,
						paraEmail, txtParaNome.text, txtAssuntoEmail.text, txtMenssagemEmail.text, link,
						function(retorno:String):void {AlertaSistema.mensagem(retorno)});
					
					
					removePopup(popupEnviaEmailOrcamento);
				}				
				
				if (mov.impressao == EMovImpressao.nf_formulario || mov.impressao == EMovImpressao.nfe_produto)
				{
					mov_nf.idMov = retornoIdMovMapa;
					App.single.ss.nuvens.modificacoes.SalvaMovNFE(retornoIdMovMapa, mov_nf,
						function():void
						{
							AlertaSistema.mensagem("Salvei MovNFE");
						}
					);
				}
				
				Alert.show( "Deseja imprimir uma página com a movimentação?", "imprimir?", Alert.YES+Alert.NO, null,
					function(ev2:CloseEvent):void
		            {
	                    
		                if(ev2.detail == Alert.YES)
		                {
		                	ss.nuvens.modificacoes.MovEscrevePdf(retornoIdMovMapa,
		                		function():void{
		                			if (Application.application.gerenteConexaoDesktop){
				                		Application.application.gerenteConexaoDesktop.imprimeMovPdf(ss.idCorp, ss.idEmp, retornoIdMovMapa, tipoMov);
				                	}
				                	else{
				                		AlertaSistema.mensagem("É necessário estar conectado ao SDE Desktop para a impressão");
				                	}
		                		}
	                		);
		                }
						AlertaSistema.mensagem(msg);
		            }
				);
				if(movGeraCarne==true && mov.tipo == EMovTipo.saida_venda)
				{
					geraCarne(retornoIdMovMapa);
				}
				sistema_resetar_tela();
			}
		);
		
	}
	
	private function escreveArquivoPRZTEF():String
	{
		var cli:Cliente = cliente_selecionado;
		var end:ClienteEndereco = new ClienteEndereco();
		if (cmbClienteEndereco_Faturamento.selectedItem as ClienteEndereco)
			end = App.single.cache.getClienteEndereco((cmbClienteEndereco_Faturamento.selectedItem as ClienteEndereco).id);
		var vendedor:Cliente = vendedor_selecionado;
		
		var movimentacoes:ArrayCollection = App.single.cache.arraycMov;
		var sort:Sort = new Sort();
		sort.fields = [new SortField("id")];
		movimentacoes.sort = sort;
		movimentacoes.refresh();
		var ultimaMovId:String = (movimentacoes.getItemAt(movimentacoes.length - 1) as Mov).id.toString();
		
		var strRetorno:String = alinhaString("Nº" + ultimaMovId, 35) +
			((cli.tipo == EPesTipo.Fisica) ? alinhaString(cli.nome, 35) : alinhaString(cli.apelido_razsoc, 35)) +
			alinhaString(end.cidade +" - "+ end.uf, 35) +
			alinhaString(end.logradouro, 35) +
			alinhaString(cli.cpf_cnpj + " " + ((cli.tipo == EPesTipo.Fisica) ? "RG.:"+cli.rg : "I.E.:"+end.inscr), 35) +
			alinhaString("Vendedor.: " + vendedor.id +"-"+ vendedor.nome, 35); 
		
		return strRetorno;
	}
	
	private function escreveArquivoMultiTEF(nomeVendedor:String):String
	{
		var arRetorno:Array = [];
		var formaPgtoMULTITEF:String="01";
		
		var arraySequencia:Array = ['01','02','03','04','05','06','07','08'];
		var arrayAliquotas:Array = [17];
		
		for each(var sdeC:SdeConfig in cache.arraySdeConfig)
		{
			if (sdeC.variavel==Variaveis_SdeConfig.EMPRESA_ALIQUOTASECF)
				arrayAliquotas = sdeC.valor.split('|');
		}
		
		var cli:Cliente = cliente_selecionado;
		var endereco:String = (popupDadosCliente_endereco_faturamento.text == "") ? cmbClienteEndereco_Faturamento.text : popupDadosCliente_endereco_faturamento.text;
		
		var isAcrescimo:Boolean = false;
		var tipoModificador:String = "a";
		var vlrDesconto:Number = mov.vlrTotal - mov.vlrItensInicial;
		
		if (vlrDesconto < 0)
		{
			tipoModificador = "d";
			vlrDesconto = -vlrDesconto;
		}
		else if (vlrDesconto > 0)
		{
			vlrDesconto = 0;
			isAcrescimo = true;
		}
		
		var s:String = "0"+
			alinhaString(cli.cpf_cnpj,14)+
			alinhaString(cli.nome,40)+
			alinhaString(endereco,47)+
			formaPgtoMULTITEF+
			tipoModificador+
			alinhaNumero(vlrDesconto, 8, 2)+
			alinhaString(nomeVendedor, 10);
		
		for each (var mi:MovItem in arraycItensCarrinho)
		{
			var item:Item = App.single.cache.getItem(mi.idItem);
			if (item.tipo == EItemTipo.servico)
				continue;
			
			var numAliq:String = "01";
			
			if (mi.icmsCst != "060" && mi.icmsCst != "040")
			{
				for (var i:int = 0; i < arrayAliquotas.length; i++)
				{
					var al:Number = arrayAliquotas[i];
					if (al == mi.icmsAliq)
					{
						numAliq = arraySequencia[i];
					}
				}
			}
			else if (mi.icmsCst == "060")
				numAliq = "FF";
			else if (mi.icmsCst == "040")
				numAliq = "II";
			
			var valorProduto:Number = (isAcrescimo) ? mi.vlrUnitVendaFinal : mi.vlrUnitVendaInicial;
			
			s += "\r\n1"+
				numAliq+
				alinhaNumero(mi.idItem, 13, 0)+
				alinhaString(mi.item_nome.substr(0,29)+" ", 30)+
				alinhaNumero(valorProduto, 9, 2) +
				alinhaNumero(mi.qtd, 8, 3);
			arRetorno.push(s);
		}
		return s;
	}
	
	private function alinhaNumero(valor:Number, larguraTotal:int, qtdCasasDecimais:int):String 
	{
		var fmt:CurrencyFormatter = new CurrencyFormatter();
		fmt.precision = qtdCasasDecimais;
		fmt.currencySymbol = "";
		fmt.useThousandsSeparator = false;
		
		var ret:String = fmt.format(valor).replace(".", ",");
		if(larguraTotal < ret.length)
			ret = ret.substr(0, larguraTotal);
		while(larguraTotal > ret.length)
			ret = "0"+ret;
		return ret;
	}
	
	private function alinhaString( valor:String, larguraTotal:int): String
	{
		var ret:String = (valor==null) ? "" : valor;
		if(larguraTotal < ret.length)
			ret = ret.substr(0, larguraTotal);
		while(larguraTotal > ret.length)
			ret +=" ";
		return ret;
	}
	
	private function verificaEstoque():Boolean
	{
		var itensSemEstoque:ArrayCollection = new ArrayCollection();
		var somenteServico:Boolean = true;
		
		for each (var mi:MovItem in arraycItensCarrinho)
		{
			if (App.single.cache.getItem(mi.idItem).tipo == EItemTipo.produto)
			{
				somenteServico = false;
				var iee:ItemEmpEstoque = cache.getItemEmpEstoque(mi.idIEE);
				if (iee.qtd < mi.qtd)
					itensSemEstoque.addItem(iee);
			}
		}
		
		if (itensSemEstoque.length > 0 && !somenteServico)
		{
			AlertaSistema.mensagem('Venda não pode ser concluída, itens com estoque insuficiente:\n'+escreveItensSemEstoque(itensSemEstoque));
			return false;	
		}
		else
			return true;
	}
	
	private function escreveItensSemEstoque(itensSemEstoque:ArrayCollection):String
	{
		var retorno:String = '';
		for each (var iee:ItemEmpEstoque in itensSemEstoque)
			retorno += cache.getItem(iee.idItem).nome+'\n';
			
		return retorno;
	}
	
	private function mostraValorFinal():void
	{
		gridItemFinal.visible=true;
		vs.selectedChild = etapa3;
	}
	
	private function ocultaValorFinal():void
	{
		gridItemFinal.visible=false;
		vs.selectedChild = etapa2;
	}