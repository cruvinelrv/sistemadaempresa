	import Core.Alerta.AlertaSistema;
	import Core.App;
	import Core.Sessao;
	
	import SDE.Entidade.ItemEmpEstoque;
	import SDE.Enumerador.EMovImpressao;
	import SDE.Enumerador.EMovResumo;
	import SDE.Enumerador.EMovTipo;
	
	import mx.collections.ArrayCollection;
	import mx.controls.Alert;
	import mx.core.Application;
	import mx.managers.PopUpManager;
	import mx.utils.Base64Encoder;
	
	private var etapa3_tipoMov:Number = 0;
	private var tipoMov:String = "";
	
	private var nomePdfDanfe:String = "";
	
	private function valida_nfe():void
	{
		
	}
				
	private function usuario_salva_entrada_com_nf():void
	{	
		mov.idCliente = cliente_selecionado.id;
		mov.idEmp = ss.idEmp;
		tipoMov = "Compra";
		/*if (Application.application.gerenteConexaoDesktop==null)
		{
			AlertaSistema.mensagem( "Equipamentos não detectados" );
			return;
		}*/
		
		mov.tipo = EMovTipo.entrada_devolucao;
		mov.resumo = EMovResumo.entrada;
		mov.impressao = EMovImpressao.nfe_produto;
					
		if (!Validacao())
		{
			AlertaSistema.mensagem('Dados Inválidos:\nVerifique as informações inseridas.');
			return;
		}
		sistema_popula_mov_nf();
		
		
		if (mov.impressao == EMovImpressao.nf_formulario)
		{
			mov.idCliente = cliente_selecionado.id;
			mov.idEmp = ss.idEmp;
			mov.idClienteFuncionarioLogado = ss.idClienteFuncionarioLogado;
			mov.idClienteFuncionarioVendedor = (mov.idClienteFuncionarioVendedor==0)?ss.idClienteFuncionarioLogado:mov.idClienteFuncionarioVendedor;
			
			App.single.n.modificacoes.Temp_Salva(mov, mov_nf, arraycItensCarrinho.source,
				function():void
				{
					Application.application.gerenteConexaoDesktop.exibeNotaFiscalFormulario();
				}
			);
			
			//PopUpManager.addPopUp(popupConfirmaImpressao, Application.application.gerenteJanelas, true);
			//PopUpManager.centerPopUp(popupConfirmaImpressao);	
		}
		if (mov.impressao == EMovImpressao.nfe_produto)
		{
			if(isNFe == true)
			{
				App.single.n.modificacoes.GerarXml(mov, mov_nf, null, arraycItensCarrinho.source,
				function(retorno:Array):void
					{
						Application.application.gerenteConexaoDesktop.escreveArquivoNFExml(retorno[0],retorno[1]);
						
						//Salvando Mov em todas requisições, isso será validado de acordo com retorno do xml
						
						lblMensagemPopupConfirmaArquivoMessage.text = "Deseja Imprimir o DANFe?";
						nomePdfDanfe = retorno[1]+".pdf";
					}
				);
			}
			
			PopUpManager.addPopUp(popupConfirmaArquivo, Application.application.gerenteJanelas, true);
			PopUpManager.centerPopUp(popupConfirmaArquivo);
		}
	}
	
	private function sistema_salva_saida():void
	{
		//sistema_salva_mov("Salvo!");
	}
	
	private function sistema_salva_mov(msg:String):void
	{
		/*for each (var cxL:Cx_Lancamento in arraycLancamentosCaixa)
		{
			var finanTipoPagamento:Finan_TipoPagamento = App.single.cache.getFinan_TipoPagamento(cxL.idTipoPagamento);
			if (finanTipoPagamento.geraContasReceber && cliente_selecionado.id == 1)
			{
				AlertaSistema.mensagem("Não é permitido gerar contas a receber para CLIENTE CONSUMIDOR");
				return;
			}
		}*/
		
		mov.idCliente = cliente_selecionado.id;
		mov.idEmp = ss.idEmp;
		//mov.idClienteEndereco = Number(cmbClienteEndereco_Faturamento.id);
		mov.idClienteFuncionarioLogado = ss.idClienteFuncionarioLogado;
		
		/*var autorizadaSemEndereco:Boolean = true;
		for each (var cxL1:Cx_Lancamento in arraycLancamentosCaixa)
		{
			var finanTipoPagamento1:Finan_TipoPagamento = App.single.cache.getFinan_TipoPagamento(cxL1.idTipoPagamento);
			if (finanTipoPagamento1.geraContasReceber)
			{
				autorizadaSemEndereco = false;
				continue;
			}
		}*/
		
		Application.application.sessao.nuvens.modificacoes.Mov_Salva_EntradaRetorno(//ivocação
			mov, arraycItensCarrinho.source, arraycLancamentosCaixa.source,
			function(retornoIdMovMapa:Number):void
			{
				var ss:Sessao = Application.application.sessao;
       
				var enc:Base64Encoder = new Base64Encoder();
				enc.encodeUTFBytes("corp"+ss.idCorp);
                var hash:String = enc.toString();
				
					mov_nf.idMov = retornoIdMovMapa;
					App.single.ss.nuvens.modificacoes.SalvaMovNFE(retornoIdMovMapa, mov_nf,
						function():void
						{
							AlertaSistema.mensagem("Salvei MovNFE");
						}
					);
				
				sistema_resetar_tela();
			}
		);
	}
		
	private function escreveItensSemEstoque(itensSemEstoque:ArrayCollection):String
	{
		var retorno:String = '';
		for each (var iee:ItemEmpEstoque in itensSemEstoque)
			retorno += cache.getItem(iee.idItem).nome+'\n';
			
		return retorno;
	}