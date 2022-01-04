	// ActionScript file
	import Core.Alerta.AlertaSistema;
	import Core.Utils.Formatadores;
	
	import SDE.CamadaServico.SNfe;
	import SDE.Entidade.Item;
	import SDE.Entidade.Mov;
	import SDE.Entidade.MovItem;
	import SDE.Entidade.MovItemEstoque;
	import SDE.Enumerador.EMovResumo;
	import SDE.Enumerador.EMovTipo;
	import SDE.FachadaServico.FcdMov;
	import SDE.FachadaServico.FcdNfe;
	import SDE.Parametro.ParamLoadMov;
	
	import mx.controls.Alert;
	import mx.core.Application;
	import mx.events.CloseEvent;
	
	private function show1():void
	{
	}
	
	private function retornaMov(retorno:Mov):void
	{
		if (retorno==null)
		{
			AlertaSistema.mensagem( "Movimentação não encontrada" );
			return;
		}
		this.mov =  retorno;
		btnGerar.enabled = (mov.isNfePreenchida)? true: false;		
		idMovNFE = mov.id;
		
		PreencheValoresMov();
	}
	
	private function cancelaPesquisaMov():void
	{
		//AlertaSistema.mensagem( "Você deve pesquisar uma movimentação" );
	}
	
	private function btnEdita_Click():void
	{
		this.selectedIndex = 1;
		
		FcdNfe.unica.GetProximoNumeroNFE(
			function(retorno:int):void
			{
				txtNumero.text = retorno.toString();
			}
		);
	}
	
	private function fechaPopupNovo():void
	{
		
		this.selectedIndex = 0;
	}

	private function btnGerarTXT():void
	{
		SNfe.unica.GerarTXT(mov.id,
			function(r:Array):void
			{
				Application.application.gerenteConexaoDesktop.escreveArquivoNFE(r[0],r[1]);
				AlertaSistema.mensagem( "Nota Salva!\nCódigo da Venda: "+mov.id+"\nChave de Acesso: "+r[1] );
			}
		);
		
		/*
		if (!Sessao.unica.desktopExecutando)
		{
			SoqueteCentral.unica.confereConexao();
			AlertaSistema.mensagem("Por favor, verifique que seu agente SDE esteja em execução e clique novamente nesse botão");
		}
		else
		{
			SoqueteEnvio.unica.enviaNFE(mov.id);
			AlertaSistema.mensagem("O arquivo foi salvo com sucesso em sua máquina");
		}
		*/
		
	}
	
	private function fn_retorno_envio_nfe(parametros:Array):void
	{
		Alert.show(parametros[0]+"\nDesejar Imprimir esta Nota Fiscal em papel?","",Alert.YES+Alert.NO, null,
			function(e:CloseEvent):void
			{
				/*
				if (e.detail==Alert.YES)
					btnImprime_Click();
					*/
			}
		);
	}
	
	//função publica que pode ser invocada de fora da classe
	public function BuscaMov(idMov:Number):void
	{
		plMov = new ParamLoadMov();
		plMov.movItens =  true;
		plMov.movValores = true;
		plMov.itens = true;
		
		FcdMov.unica.Load(idMov, plMov,
			function(retorno:Mov):void
			{
				idMovNFE = retorno.id;
				AlertaSistema.mensagem("carreguei mov, idCli: "+retorno.idCliente);
				btnGerar.enabled = (retorno.isNfePreenchida)? true: false;
				PreencheValoresMov();
			}
		);
	}
	
	private function PreencheValoresMov():void
	{
		if(mov == null)
			return;
		btnEdita.enabled = !mov.isNfePreenchida;	
		btnEdita.enabled=true;
		
		var contador:int = 0;
		var vlrNota:Number = 0;
		var vlrItens:Number = 0;
		
		var vlrICMS:Number = 0;
		var vlrICMSSubst: Number = 0;
		var vlrIPI:Number = 0;
		var vlrPIS:Number = 0;
		var vlrCOFINS:Number = 0;
		var vlrDesc:Number = 0;
		var vlrFrete:Number = 0;
		var vlrSeguro:Number = 0;
		
		//preeencher Array de itens
		arMI = new Array();
		for each(var mi:MovItem in mov.__mItens)
		{
			var it:Item = mi.__item;
			for each(var mie: MovItemEstoque in mi.__mIEstoques)
			{
				contador++;
				var obj:Object = {};
				obj.id = mie.idIEE;
				obj.contador = contador;
				obj.cfop = cfop;
				obj.grade = mie.identificador;
				obj.nome = it.nome;
				obj.qtd = Formatadores.unica.formataDecimal( mie.qtd, 2 );
				
				var vlrUnit:Number = 0;
				var vlrTotalProduto:Number = 0;
				if(mov.resumo == EMovResumo.entrada) {
					//verificar codigo
					if(mov.tipo == EMovTipo.entrada_compra)
						vlrUnit = mi.vlrUnitCompra;
					if(mov.tipo == EMovTipo.entrada_devolucao)
						vlrUnit =  mi.vlrUnitVendaFinal;
				}
				else {
					vlrUnit = mi.vlrUnitVendaFinal;
				}
				vlrTotalProduto =  mie.qtd * vlrUnit;					
				obj.vlrUnit = Formatadores.unica.formataValor( vlrUnit, true );
				obj.vlrTotal = Formatadores.unica.formataValor( vlrTotalProduto, true );								
				arMI.push(obj);
				
				//calculo valores totais
				switch (mi.icmsCst)
                {
                    case "000": //tributado normal
                        vlrICMS += mi.vlrICMS;
                        break;
                    case "020": //tributado com reducao
                        vlrICMS += mi.vlrICMS;
                        break;
                    case "040"://isento                           
                        break;
                    case "060"://substituicao tributaria
                    	vlrICMSSubst += mi.vlrIcmsSubstTrib;
                        break;
                }
				
				if(mi.ipiCst == "00" || mi.ipiCst == "49" || mi.ipiCst == "50" || mi.ipiCst == "99")
				{
					vlrIPI += mi.vlrIPI
				}
				if(mi.pisCst == "01" || mi.pisCst == "02" || mi.pisCst == "03")
				{
					vlrPIS += mi.vlrPis;
				}
				if(mi.cofinsCst == "01" || mi.cofinsCst == "02" || mi.cofinsCst == "03")
				{
					vlrCOFINS += mi.vlrCofins;
				}
				
				vlrItens += vlrTotalProduto;
				vlrSeguro += mi.vlrSeguro;
				vlrDesc += mi.vlrUnitVendaFinal - mi.vlrUnitVendaInicial;
				vlrFrete += mi.vlrFrete;
			}	
		}
		
		lblVlrIcms.text = Formatadores.unica.formataValor( vlrICMS, true );
		lblVlrIcmsst.text = Formatadores.unica.formataValor( vlrICMSSubst, true );
		
		lblVlrIPI.text = Formatadores.unica.formataValor( vlrIPI, true );
		lblVlrPis.text = Formatadores.unica.formataValor( vlrPIS, true );
		lblVlrCofins.text = Formatadores.unica.formataValor( vlrCOFINS, true );
		
		lblVlrSeguro.text = Formatadores.unica.formataValor( vlrSeguro, true );
		lblVlrFrete.text = Formatadores.unica.formataValor( vlrFrete, true );
		lblVlrDesc.text = Formatadores.unica.formataValor( vlrDesc, true );
		lblVlrProd.text = Formatadores.unica.formataValor( vlrItens, true );
		lblVlrTotal.text = Formatadores.unica.formataValor( mov.vlrTotal, true );
								
		grid.dataProvider = arMI;
	}
	
