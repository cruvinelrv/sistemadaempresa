	import Core.Alerta.AlertaSistema;
	import Core.App;
	import Core.Sessao;
	import Core.Utils.Funcoes;
	
	import SDE.CamadaServico.SMov;
	import SDE.Entidade.ClienteEndereco;
	import SDE.Entidade.MovItem;
	import SDE.Entidade.MovItemEstoque;
	
	import mx.core.Application;
	
	private var mi:MovItem = null;
	private var msg:String = "";
	
	private function finaliza(resumo:String, tipo:String, impressao:String, tipoNfs:String):void
	{
		this.resumo = resumo;
		this.tipo = tipo;
		this.impressao = impressao;
		this.tipoNfs = tipoNfs;
		
		if (listaMI.length==0)
		{
			AlertaSistema.mensagem('Não há itens');
			return;
		}
		
		msg = "";
		
		if (cliente == null)
			msg += "Selecione um CLIENTE\n";
		if (txtNumNota.text == "")
			msg += "Digite o NÚMERO DA NOTA\n";
		if (cfop == null)
			msg += "Selecione o CFOP\n";
		
		if (this.tipoNfs == "formulario")
			if (listaMI.length > 9)
				msg += "A nota pode ter no máximo 9 itens\n";
		
		if (this.tipoNfs == "eletronica")
		{
			if (txtSerieNota.text == "")
				msg += "Digite o NÚMERO DE SÉRIE DA NOTA\n";
			if (cmbTipoServico.selectedIndex == 0)
				msg += "Selecione o tipo de serviço\n";
		}
		
		if(msg != "")
		{
			AlertaSistema.mensagem(msg);
			return;
		}
		
		if (cliente.id==1)
			mostraPopupDadosCliente();
		else
			FechaMovimentacao();
	}
	
	private function FechaMovimentacao():void
	{
		var clienteEndereco:ClienteEndereco = new ClienteEndereco(cmbEndereco.selectedItem);
		
		//var mov:Mov = new Mov();
		mov.__mItens = [];
		mov.idCliente = cliente.id;
		mov.idClienteFuncionarioLogado = Sessao.unica.idClienteFuncionarioLogado;
		mov.idEmp = Sessao.unica.idEmp;
		mov.cfop = cfop.codigo;
		mov.idClienteEndereco = clienteEndereco.id;
		mov.idClienteParceiro = 0;
		mov.idClienteTransportador = 0;
		
		mov.__cli = cliente;
		mov.__cliFuncionario = vendedor;
		
		mov.__mValores = seletorEspecies.getMV();
		
		mov.vlrAcrescimo = -nsDescontoFinal.value;
		mov.vlrTotal = mov.vlrItensFinal + mov.vlrAcrescimo;
		
		mov.resumo = this.resumo;
		mov.tipo = this.tipo;
		mov.impressao = this.impressao;
		
		mov.numeroNF = Number(txtNumNota.text);
		mov.serieNF = txtSerieNota.text;
		mov.dtNF = txtDataEmissao.text;
		
		if (tipoNfs == 'formulario')
		{
			for each(var of:Object in listaMI)
			{
				mi = of.movItem;
				mov.vlrItensInicial += mi.qtd * mi.vlrUnitVendaInicial;
				mov.vlrItensFinal   += mi.qtd * mi.vlrUnitVendaFinal;
				mov.__mItens.push(mi);
			}
			
			mov.retencaoISSQN = nsISSQN.value;
			mov.retencaoINSS = nsINSS.value;
			if (ckbNoMun.selected)
				mov.noMun = true;
			if (ckbForaMun.selected)
				mov.foraMun = true;
			mov.fatura = txtFatura.text;
			mov.obs = txtObs.text;
		}
		else if (tipoNfs == 'eletronica')
		{
			for each(var oe:Object in listaMI)
			{
				mi = oe.movItem;
				mov.vlrItensInicial += mi.qtd * mi.vlrUnitVendaInicial;
				mov.vlrItensFinal   += mi.qtd * mi.vlrUnitVendaFinal;
				
				mi.bcISS = nsAliquota.value;
				mi.recolhidoPeloTomador = oe.recolhidoPeloTomador;
				mov.__mItens.push(mi);
			}
			mov.mesReferencia = cmbDataReferencia.selectedLabel.substring(0,2);
			mov.anoReferencia = cmbDataReferencia.selectedLabel.substring(5,9);
			mov.tipoServicoDms = cmbTipoServico.selectedIndex.toString();
			mov.tipoDeclaracaoDms = cmbTipoDeclaracao.selectedIndex.toString();
			mov.situacaoDms = cmbSituacao.selectedIndex.toString();
		}
		
		//retirar variáveis temporarias (lixo)
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
		
		//AlertaSistema.mensagem("Salvei NFS");
		//return;
		
		SMov.unica.NovaMovServico(mov,
			function(retorno:Number):void
			{
				//AlertaSistema
				var idMov:Number = retorno;
				if (tipoNfs == 'formulario')
				{
					//SoqueteEnvio.unica.enviaNotaPrefeitura(mov.id);
					Application.application.gerenteConexaoDesktop.enviaNotaPrefeitura(mov.id);
					
				}
				else if (tipoNfs == 'eletronica')
				{
					SMov.unica.GeraDms(idMov,
						function(retorno:String):void
						{
							//SoqueteEnvio.unica.enviaDMS(mov.id);
							Application.application.gerenteConexaoDesktop.enviaNotaPrefeitura(idMov, retorno);
						}
					);
				}
				listaMI.splice(0, listaMI.length);
				limpaTela();
				exibeTotaisPinta();
			}
		);
	}
	
	private function salvaDadosCliente():void
	{
		if (txtCPFCNPJ.text == "00000000000")
			isCpfValido = true;
		else if (rbFis.selected)
			isCpfValido = Funcoes.validaCpf(txtCPFCNPJ.text);
		else
			isCpfValido = Funcoes.validaCnpj(txtCPFCNPJ.text);
			
		if(!isCpfValido)
		{
			AlertaSistema.mensagem("CPF/CNPJ inválido");
			return;
		}
		
		this.mov.cliente_nome = txtClienteNome.text;
		this.mov.cliente_cpf = Funcoes.LimpaCPF(txtCPFCNPJ.text);
		this.mov.cliente_endereco_faturamento = txtClienteEndereco.text;
		
		fechaPopupDadosCliente();
		FechaMovimentacao();
	}