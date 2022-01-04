// ActionScript file
import Core.Alerta.AlertaSistema;
import Core.App;
import Core.Utils.MyArrayUtils;

import SDE.Constantes.Variaveis_SdeConfig;
import SDE.Entidade.Cliente;
import SDE.Entidade.ClienteEndereco;
import SDE.Entidade.ClienteVeiculo;
import SDE.Entidade.Empresa;
import SDE.Entidade.MovItem;
import SDE.Entidade.MovNFE;
import SDE.Entidade.MovNfeVeiculo;
import SDE.Enumerador.EMovResumo;
import SDE.Enumerador.ENfeAmbiente;
import SDE.Enumerador.ENfeFinalidade;
import SDE.Enumerador.ENfeFormaPgto;
import SDE.Enumerador.ENfeTipoTransporte;

import img.Imagens;

import mx.controls.Alert;
import mx.core.Application;
import mx.core.Container;
import mx.formatters.DateFormatter;
import mx.managers.PopUpManager;

	[Bindable] private var isEndEmpValido:Boolean = false;
	[Bindable] private var isEndCliValido:Boolean = false;
	
	[Bindable] private var isNF:Boolean = false;
	[Bindable] private var isNFe:Boolean = false;
	
	private var mov_nf:MovNFE = new MovNFE();
	
	private function popupConfirmaArquivoCloseHandler(cont:Container, imprimeDanfe:Boolean):void
	{
		sistema_salva_mov("Salva Movimentação com NF");
		
		if (imprimeDanfe)
			Application.application.gerenteConexaoDesktop.imprimeDanfe(nomePdfDanfe);
		
		PopUpManager.removePopUp(cont);
	}
	
	private function usuario_chama_notaFiscal():void
	{
		var proximoNumeroNF:Number = 0;
		
		ss.nuvens.modificacoes.numNFE(
			function(num:Number):void
			{
				txtNumNota.text = (++num).toString();
			}
		);
		
		if (!Application.application.gerenteConexaoDesktop)
		{
			Alert.show("É necessário que SDE Desktop esteja em execução para Emissão de NFe","Alerta SDE",4,null,null,Imagens.unica.icn_32_alerta);
			return;
		}
		etapa3.enabled = true;
		
		etapa3.label = '3. Complemento NF-e';
		
		if (ss.parametrizacao.getParametro(Variaveis_SdeConfig.EMPRESA_VENDEVEICULOS) == '1')
		{
			vendaVeiculos.enabled = true;
			vendaVeiculos.label = 'Venda de Veículos';
		}
		else
			vendaVeiculos.enabled = false;
		
		if (ss.parametrizacao.getParametro(Variaveis_SdeConfig.EMPRESA_VENDECOMBUSTIVEL) == '1')
		{
			vendaCombustivel.enabled = true;
			vendaCombustivel.label = 'Venda de Combustível';
		}
		else
			vendaCombustivel.enabled = false;
		
		vs.selectedChild = etapa3;
		
		txtSerieNota.text = '001';
		dtfDataSE.selectedDate = new Date();
		
		var date:Date = new Date();
		var df:DateFormatter = new DateFormatter();
		df.formatString = "HH:MM";
		txtHoraSaida.text = df.format(date);
		
		//preencher comboBox da nfe		
		cmbFinalidade.dataProvider = ["escolha...", "NFE Normal","NFE Complementar"," NFE Ajuste"];
		cmbPagamento.dataProvider  = ["escolha...", "À vista", "À Prazo", "Outros"];	
		cmbModalidade.dataProvider = ["escolha...", "Pago pelo Emitente","Pago pelo Destinatário"];
		
		//preencher combobox dos veiculos
		cmbVeicOperacao.dataProvider = ['01-Venda concessionária','02-Faturamento Direto','03-Venda Direta','00-Outros'];
		cmbVeicCombustivel.dataProvider  =['01-Álcool','02-Gasolina','03-Diesel'];
		cmbVeicTipo.dataProvider  = ['02-Ciclomotor','03-Motoneta','04-Motocicleta','05-Triciclo','17-Caminhão Trator','21-Quadriciclo']
		cmbVeicPintura.dataProvider  =['01-Sólida','02-Metálica'];
		cmbVeicCondVIN.dataProvider = ['01-Importado','02-Nacional'];
		cmbVeicCond.dataProvider = ['01-Acabado'];
		cmbVeicEspecie.dataProvider = ['01-Passageiro','02-Carga'],'03-Misto','04-Corrida','05-Tração','06-Coleção','07-Especial';
		preencheEmpresa();
	}
	
	/**PREENCHE EMPRESA*/
	private function preencheEmpresa():void
	{
		var emp:Empresa = cache.getEmpresa(ss.idEmp);
		var cli:Cliente = cache.getCliente(emp.idCliente);
		
		lblEmpresa.text = (cli.apelido_razsoc=='')?cli.nome:cli.apelido_razsoc;
		cmbEndEmpresa.dataProvider = preencheComBoxEndereco(cli.id);
		isEndEmpValido = validaEndereco(cmbEndEmpresa.selectedItem as ClienteEndereco);
	}
	/**FIM PREENCHE EMPRESA*/
	
		
	/**PREENCHE TRANSPORTADOR*/
	private function preencheTransportador():void
	{
		if (cpTransportador.selectedItem == null)
			return;
		
		var cli:Cliente = cpTransportador.selectedItem as Cliente;
		
		cmbEndTranportador.dataProvider = preencheComBoxEndereco(cli.id);
		cmbVeiTransportador.dataProvider = preencheComboBoxVeiculo(cli.id);
	}
	/**FIM PREENCHE TRANSPORTADOR*/
	
	/**PREENCHE REBOQUE*/
	private function preencheTransportadorReboque():void
	{
		if (cpTransportadorReboque.selectedItem == null)
			return;
			
		var cli:Cliente = cpTransportadorReboque.selectedItem as Cliente;
		
		cmbRebTranportadorReboque.dataProvider = preencheComboBoxVeiculo(cli.id);
	}
	/**FIM PREENCHE REBOQUE*/
	
	
	private function preencheComBoxEndereco(idCliente:Number):Array
	{
		var enderecos:Array = [];
		for each (var end:ClienteEndereco in cache.arrayClienteEndereco)
			if (end.idCliente == idCliente)
				enderecos.push(end);
		
		return enderecos;
	}
	private function preencheComboBoxVeiculo(idCliente:Number):Array
	{
		var veiculos:Array = [];
		for each (var vei:ClienteVeiculo in cache.arrayClienteVeiculo)
			if (vei.idCliente == idCliente)
				veiculos.push(vei);
				
		return veiculos;
	}
	private function validaEndereco(end:ClienteEndereco):Boolean
	{
		var valido:Boolean = true;
		if (end == null)
		{
			return true;
		}
		
		if(end.id == 0)
		{
			return true;
		}
		//validacao CEP
		if(end.ufIBGE.length != 2 || end.cidadeIBGE.length != 7 ){
			AlertaSistema.mensagem("Atualize o Endereço");
			valido = false;
		}
		
		return valido;
	}
	private function fn_lbl_endereco(end:ClienteEndereco):String
	{
		cmbPagamento.selectedIndex = 3;
		cmbFinalidade.selectedIndex = 1;
	
		if (end.inscr == null || end.inscr == '')
			return end.logradouro +', '+ end.cidade +'-'+ end.uf;
		else
			return end.logradouro +', '+ end.cidade +'-'+ end.uf +' | Insc. Est: '+ end.inscr;
	}
	private function fn_lbl_veiculo(vei:ClienteVeiculo):String
	{
		return vei.marca +' '+ vei.modelo +' Placa: '+ vei.placaNumero;
	}
	public function Validacao():Boolean
	{
		var valido:Boolean = true;
		
		//validação da nota
		if(txtNumNota.text == '' || txtSerieNota.text == '' || 
		  cmbPagamento.selectedIndex < 1)
		{
			AlertaSistema.mensagem("Preencha campos referente a nota");
			valido = false;
		}
		if (isNFe)
		{
			if (cmbFinalidade.selectedIndex < 1)
			{
				AlertaSistema.mensagem("Selecione a finalidade");
				valido = false;
			}
		}
		//validação endereco cliente/remetente
		if( !( validaEndereco(cmbEndEmpresa.selectedItem as ClienteEndereco) ) )
		{
			valido = false;
		}
		//validação do transportador
		if(cpTransportador.selectedItem != null)
		{
			if (!validaEndereco(cmbEndTranportador.selectedItem as ClienteEndereco))
			{
				valido = false;
				AlertaSistema.mensagem("Escolha endereço do transportador.");
			}
			/*
			if( cmbEndTranportador.selectedIndex >= 1 )
			{
				valido = validaEndereco(cmbEndTranportador.selectedItem as ClienteEndereco);
			}
			else
			{
				valido = false;
				AlertaSistema.mensagem("Escolha endereço do transportador.");
			}
			*/		
		}
		
		return valido;
	}
	
	
	
	
	private function sistema_popula_mov_nf():void
	{
		if (!Validacao())
		{
			AlertaSistema.mensagem('Dados Inválidos:\nVerifique as informações inseridas.');
			return;
		}
		
		var ceEmpresa:ClienteEndereco =  cmbEndEmpresa.selectedItem as ClienteEndereco;
		//var ceCliente:ClienteEndereco =  cmbEndCliente.selectedItem as ClienteEndereco;
		var ceTransp:ClienteEndereco = cmbEndTranportador.selectedItem as ClienteEndereco;
		
		var veicTransp:ClienteVeiculo = cmbVeiTransportador.selectedItem as ClienteVeiculo;
		var reboqueTRansp:ClienteVeiculo = cmbRebTranportadorReboque.selectedItem as ClienteVeiculo;
		
		mov_nf = new MovNFE();
		
		//forma pagamento
		if (cmbPagamento.selectedIndex == 1)
			mov_nf.formaPgtoNFE = ENfeFormaPgto.vista;
		if (cmbPagamento.selectedIndex == 2)
			mov_nf.formaPgtoNFE = ENfeFormaPgto.prazo;
		if (cmbPagamento.selectedIndex == 3)
			mov_nf.formaPgtoNFE = ENfeFormaPgto.outros;
			
		//dados da nota
		
		if (isNFe)
		{
			//finalidade
			if (cmbFinalidade.selectedIndex == 1)
				mov_nf.finalidadeNFE = ENfeFinalidade.normal;
			if (cmbFinalidade.selectedIndex == 2)
				mov_nf.finalidadeNFE = ENfeFinalidade.complementar;
			if (cmbFinalidade.selectedIndex == 3)
				mov_nf.finalidadeNFE = ENfeFinalidade.ajuste;
				
			//ambiente
			if (ss.parametrizacao.getParametro(Variaveis_SdeConfig.EMPRESA_NFE_AMBIENTE) == "0")
				mov_nf.ambienteNFE = ENfeAmbiente.homologacao;
			if (ss.parametrizacao.getParametro(Variaveis_SdeConfig.EMPRESA_NFE_AMBIENTE) == "1")
				mov_nf.ambienteNFE = ENfeAmbiente.producao;
		}
			
		
		if (mov.cfop == null || mov.cfop.length < 4)
		{
			mov.cfop = (arraycItensCarrinho.getItemAt(0) as MovItem).cfop.toString();
		}
		mov_nf.cfop = MyArrayUtils.getItemByField(App.single.cache.arrayCFOP, "codigo", mov.cfop).descricao;
		mov_nf.dtSaiEnt =dtfDataSE.text;
		mov_nf.horaSaida = txtHoraSaida.text;
		mov_nf.numeroNota = parseInt(txtNumNota.text);
		mov_nf.serieNota = parseInt(txtSerieNota.text);
		
		//dados emitente/destinatário
		mov_nf.idCliente = mov.idCliente;
		//mov_nf.idEnderecoCliente = ceCliente.id;
		mov_nf.idEmp = ss.idEmp;
		mov_nf.idEnderecoEmp = ceEmpresa.id;
		
		//endereco de retirada e entrega
		if (mov.resumo == EMovResumo.saida)
		{
		//	mov_nf.idEnderecoEntrega = ceCliente.id;
			mov_nf.idEnderecoRetirada = ceEmpresa.id;
		}
		else if (mov.resumo == EMovResumo.entrada)
		{
			mov_nf.idEnderecoEntrega = ceEmpresa.id;
		//	mov_nf.idEnderecoRetirada = ceCliente.id;
		}
		
		//dados de transporte
		if (cmbModalidade.selectedIndex == 1)
			mov_nf.tipoTranspNFE = ENfeTipoTransporte.emitente;
		if (cmbModalidade.selectedIndex == 2)
			mov_nf.tipoTranspNFE = ENfeTipoTransporte.destinatario;
			
		//verificacao transportadora
		if (cpTransportador.selectedItem != null)
			mov_nf.idClienteTransp = (cpTransportador.selectedItem as Cliente).id;
		else
			mov_nf.idClienteTransp = 0;
			
		//verificação endereço transportadora
		if (ceTransp != null)
			mov_nf.idEnderecoTransp = ceTransp.id;
		else
			mov_nf.idEnderecoTransp = 0;
			
		//verificacao veiculo transportadora
		if (veicTransp != null)
			mov_nf.idVeiculo = veicTransp.id;
		else
			mov_nf.idVeiculo = 0;
			
		//verificacao reboque
		if (reboqueTRansp != null)
			mov_nf.idReboque = reboqueTRansp.id;
		else
			mov_nf.idReboque = 0;
		
		//volume
		mov_nf.volQuantidade = nsQtdtransportado.value;
		mov_nf.volEspecie = txtEspecie.text;
		mov_nf.volMarca = txtMarca.text;
		mov_nf.volNumeracao = txtNumeracao.text;
		mov_nf.volPesoLiquido = nsPesoLiquido.value;
		mov_nf.volPesoBruto = nsPesoBruto.value;
		mov_nf.valorFrete = nsValorFrete.value;
		mov_nf.valorSeguro = nsValorSeguro.value;
		
		if(cmbPagamento.selectedLabel == "vista")
		{
			mov_nf.formaPgtoNFE = ENfeFormaPgto.vista;
		}
		
		if(cmbPagamento.selectedLabel == "prazo")
		{
			mov_nf.formaPgtoNFE = ENfeFormaPgto.prazo;
		}
		
		if(cmbPagamento.selectedLabel == "outros")
		{
			mov_nf.formaPgtoNFE = ENfeFormaPgto.outros;
		}
		
		mov_nf.fatura = txtFaturaDuplicata.text;
		mov_nf.valorOutrasDespesas = nsValorOutrasDespesas.value;
		mov_nf.infoAdicional = txtDadosAdicionais.text.replace('\r','').replace('\n','');
		
		//mov_nfEVeiculos
		if (ss.parametrizacao.getParametro(Variaveis_SdeConfig.EMPRESA_VENDEVEICULOS) == '1')
		{
			mov_nf.ehVendaVeiculo = true;
			var nfeVeiculo:MovNfeVeiculo = new MovNfeVeiculo();
			nfeVeiculo.anoFab = nsVeicAnoF.value;
			nfeVeiculo.anoModel = nsVeicAnoModelo.value;
			nfeVeiculo.chassi =  txtVeicChassi.text;
			nfeVeiculo.cm3 = nsVeicCM3.value;
			nfeVeiculo.cmkg = nsVeicCMKG.value;
			nfeVeiculo.codCor = nsVeicCodCor.value;
			nfeVeiculo.codMarcaModelo = parseInt(txtVeicCodModelo.text);
			nfeVeiculo.condicaoVeic = parseInt(cmbVeicCond.selectedLabel.substr(0,2));
			nfeVeiculo.condicaoVIN = parseInt(cmbVeicCondVIN.selectedLabel.substr(0,2));
			nfeVeiculo.desCor = txtVeicCor.text;
			nfeVeiculo.distanciaEixos = nsVeicDistEixos.value;
			nfeVeiculo.especie = parseInt(cmbVeicEspecie.selectedLabel.substr(0,2));
			nfeVeiculo.numeroMotor = txtVeicNumMotor.text;
			nfeVeiculo.pesoB = nsVeicPesoB.value;
			nfeVeiculo.pesoL =nsVeicPesoL.value;
			nfeVeiculo.potencia = nsVeicPotencia.value;
			nfeVeiculo.serie = txtVeicSerie.text;
			nfeVeiculo.tipoCombustivel = parseInt(cmbVeicCombustivel.selectedLabel.substr(0,2));
			nfeVeiculo.tipoOperacao  =parseInt(cmbVeicOperacao.selectedLabel.substr(0,2));
			nfeVeiculo.tipoPintura  = parseInt(cmbVeicPintura.selectedLabel.substr(0,2));
			nfeVeiculo.tipoVeiculo  =parseInt(cmbVeicTipo.selectedLabel.substr(0,2));
			mov_nf.__nfeVeiculo = nfeVeiculo;
		} 
	}
	
	private function sistema_limpar_complementoNF():void
	{
		limpa_notaFiscal_remetente();
		limpa_transporte_frete();
		limpa_vendaVeiculos();
		limpa_vendaCombustivel();
		
		isNF = false;
		isNFe = false;
		
		etapa3.enabled = false;
		etapa3.label = '';
		
		vendaVeiculos.enabled = false;
		vendaVeiculos.label = '';
		
		vendaCombustivel.enabled = false;
		vendaCombustivel.label = '';
		
		vsNf.selectedIndex = 0;
	}
	
	private function limpa_notaFiscal_remetente():void
	{
		txtNumNota.text = "";
		txtSerieNota.text = "";
		dtfDataSE.selectedDate = new Date();
		cmbPagamento.selectedIndex = 0;
		cmbFinalidade.selectedIndex = 0;
		lblEmpresa.text = "";
		cmbEndEmpresa.dataProvider = null;
		//lblCliente.text = "";
		//cmbEndCliente.dataProvider = null;
		//cmbClienteSituacaoInscrEst.selectedIndex = 0;
		txtDadosAdicionais.text = "";
	}
	private function limpa_transporte_frete():void
	{
		cpTransportador.selectedItem = null;
		cmbEndTranportador.dataProvider = null;
		cmbVeiTransportador.dataProvider = null;
		cpTransportadorReboque.selectedItem = null;
		cmbRebTranportadorReboque.dataProvider = null;
		cmbModalidade.selectedIndex = 0;
		//volume transportado
		nsQtdtransportado.value = 0;
		txtEspecie.text = "";
		txtMarca.text = "";
		txtNumeracao.text = "";
		nsPesoLiquido.value = 0;
		nsPesoBruto.value = 0;
	}
	private function limpa_vendaVeiculos():void
	{
		cmbVeicOperacao.selectedIndex = 0;
		txtVeicChassi.text = "";
		txtVeicNumMotor.text = "";
		txtVeicSerie.text = "001";
		nsVeicCMKG.value = 0;
		nsVeicPotencia.value = 0;
		nsVeicCM3.value = 0;
		nsVeicPesoL.value = 0;
		nsVeicPesoB.value = 0;
		cmbVeicTipo.selectedIndex = 0;
		nsVeicDistEixos.value = 0;
		txtVeicCodModelo.text = "";
		nsVeicAnoModelo.value = 1900;
		nsVeicAnoF.value = 1900;
		cmbVeicTipo.selectedIndex = 0;
		cmbVeicEspecie.selectedIndex = 0;
		cmbVeicPintura.selectedIndex = 0;
		nsVeicCodCor.value = 0;
		txtVeicCor.text = "";
		cmbVeicCondVIN.selectedIndex = 0;
		cmbVeicCond.selectedIndex = 0;
	}
	private function limpa_vendaCombustivel():void
	{
		txtCombANP.text = "";
		nsCombBaseCalculoICMS.value = 0;
		nsCombValorICMS.value = 0;
		nsCombBaseCalculoICMSST.value = 0;
		nsCombValorICMSST.value = 0;
	}