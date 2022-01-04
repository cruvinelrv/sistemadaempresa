// ActionScript file
import Core.Alerta.AlertaSistema;
import Core.App;
import Core.Ev.EvRetornaArray;
import Core.Sessao;
import Core.Utils.MyArrayUtils;

import SDE.CamadaServico.SNfe;
import SDE.Entidade.Cliente;
import SDE.Entidade.ClienteEndereco;
import SDE.Entidade.ClienteVeiculo;
import SDE.Entidade.MovNFE;
import SDE.Entidade.MovNfeVeiculo;
import SDE.Enumerador.EMovResumo;
import SDE.Enumerador.ENfeAmbiente;
import SDE.Enumerador.ENfeFinalidade;
import SDE.Enumerador.ENfeFormaPgto;
import SDE.Enumerador.ENfeTipoTransporte;
import SDE.FachadaServico.FcdCliente;
import SDE.FachadaServico.FcdCorp;
import SDE.FachadaServico.FcdNfe;
import SDE.Parametro.ParamLoadCliente;

import mx.controls.Alert;
import mx.formatters.DateFormatter;
	
	[Bindable] private var clienteNome:String;
	
	private function show2():void
	{
		//preencher comboBox da nfe		
		cmbFinalidade.dataProvider = ["escolha...", "NFE Normal","NFE Complementar"," NFE Ajuste"];
		cmbFormaPgto.dataProvider  = ["escolha...", "À vista", "À Prazo", "Outros"];	
		cmbModalidade.dataProvider = ["escolha...", "Pago pelo Emitente","Pago pelo Destinatário"];
		
		//preencher combobox dos veiculos
		cmbVeicOperacao.dataProvider = ['01-Venda concessionária','02-Faturamento Direto','03-Venda Direta','00-Outros'];
		cmbVeicCombustivel.dataProvider  =['01-Álcool','02-Gasolina','03-Diesel'];
		cmbVeicTipo.dataProvider  = ['02-Ciclomotor','03-Motoneta','04-Motocicleta','05-Triciclo','17-Caminhão Trator','21-Quadriciclo']
		cmbVeicPintura.dataProvider  =['01-Sólida','02-Metálica'];
		cmbVeicCondVIN.dataProvider = ['01-Importado','02-Nacional'];
		cmbVeicCond.dataProvider = ['01-Acabado'];
		cmbVeicEspecie.dataProvider = ['01-Passageiro','02-Carga'],'03-Misto','04-Corrida','05-Tração','06-Coleção','07-Especial';
		//parametro
		plTransp = new ParamLoadCliente();
		plTransp.enderecos = true;
		plTransp.veiculos = true;
		pfTransp = new ParamFiltroCliente();
		
		//prencher caixa pesquisa
		cpTransporte.pLoad = plTransp;
		cpTransporte.pFiltro = pfTransp;	
		cpTransporteReboque.pLoad = plTransp;
		cpTransporteReboque.pFiltro = pfTransp;
		//preenche caixa de pesquisa
		cpMov.pLoad =  plMov;
		
		//prencher valores clientes
		PreencheValoresCliente();
		PreencheValoresEmpresa();
		
		SNfe.unica.GetUltimaInfoAdicional(
			function(retorno:String):void{
				txtInfAdicional.text = retorno;
			}
		);
		
	}
	
	private function retornaTransporte( ev:EvRetornaArray  ):void
	{
		if(ev.retorno == null)
		{
			AlertaSistema.mensagem("Não encontrado");
			transportadora = null;
			cmbEndTransp.dataProvider = new Array();
			cmbVeiculo.dataProvider = new Array();			
			return;
		}
		transportadora = ev.retorno[0];		
		lblTransp.text = transportadora.nome;
		
		var cliEnd: ClienteEndereco = new ClienteEndereco();
		cliEnd.logradouro = 'escolha';
		cmbEndTransp.dataProvider = [cliEnd].concat(transportadora.__enderecos);
		
		var veic: ClienteVeiculo = new ClienteVeiculo();
		veic.nome = 'escolha...';
		cmbVeiculo.dataProvider = [veic].concat(transportadora.__veiculos);
		//cmbReboque.dataProvider = transportadora.__veiculos;		
	}
	
	private function retornaTransporteReboque( ev:EvRetornaArray  ):void
	{
		if(ev.retorno == null)
		{
			AlertaSistema.mensagem("Não encontrado");
			transportadoraReboque = null;			
			cmbReboque.dataProvider = new Array();			
			return;
		}
		transportadoraReboque = ev.retorno[0];		
		lblTranspReboque.text = transportadoraReboque.nome;
				
		var veic: ClienteVeiculo = new ClienteVeiculo();
		veic.nome = 'escolha...';
		cmbReboque.dataProvider = [veic].concat(transportadoraReboque.__veiculos);		
	}
	
	private function PreencheValoresCliente():void
	{
		cmbEndCliente.dataProvider = null;
		
		AlertaSistema.mensagem("Vou Buscar Cliente "+mov.idCliente, true);
		var plCliente:ParamLoadCliente = new ParamLoadCliente();
		plCliente.enderecos = true;
		
		FcdCliente.unica.Load(mov.idCliente, plCliente, 
			function(retorno:Cliente):void
			{
				cliente = retorno;
				if(cliente == null)
				{
					AlertaSistema.mensagem("Cliente "+mov.idCliente+" não encontrado");
					return;
				}			
				//var p:Pessoa = cliente.__pessoa;
				clienteNome = cliente.nome;
				cmbEndCliente.dataProvider = cliente.__enderecos;
				validaEndCliente();
			}
		);
	}
	private function PreencheValoresEmpresa():void
	{
		FcdCorp.unica.LoadEmpresaCompleta(
			function (retorno:Empresa):void
			{
				empresa = retorno;
				if(empresa == null){
					Alert.show("Empresa nula");
					
					Alert.show("corp:" +Sessao.unica.idCorp + " emp:"+Sessao.unica.idEmp);
					return;
				}
				
				//var p:Pessoa = empresa.__cliente.__pessoa;
				lblEmpresa.text =  empresa.__cliente.nome;
				cmbEndEmpresa.dataProvider =  empresa.__cliente.__enderecos;
				validaEndEmpresa();
				
			}
		);
	}
	
	private function SalvaNFE():void
	{			
		if( ! Validacao() )
		{
			return;
		}
		
		var ceEmpresa:ClienteEndereco =  cmbEndEmpresa.selectedItem as ClienteEndereco;
		var ceCliente:ClienteEndereco =  cmbEndCliente.selectedItem as ClienteEndereco;
		var ceTransp:ClienteEndereco = cmbEndTransp.selectedItem as ClienteEndereco;
		
		var veicTransp:ClienteVeiculo = cmbVeiculo.selectedItem as ClienteVeiculo;
		var reboqueTRansp:ClienteVeiculo = cmbReboque.selectedItem as ClienteVeiculo;
		
		nfe = new MovNFE();
		//finalidade
		if(cmbFinalidade.selectedIndex == 1){
			nfe.finalidadeNFE = ENfeFinalidade.normal;
		}
		if(cmbFinalidade.selectedIndex == 2){
			nfe.finalidadeNFE = ENfeFinalidade.complementar;
		}
		if(cmbFinalidade.selectedIndex == 3){
			nfe.finalidadeNFE = ENfeFinalidade.ajuste;
		}
		//forma pagamento		
		if( cmbFormaPgto.selectedIndex == 1){
			nfe.formaPgtoNFE =  ENfeFormaPgto.vista;
		}
		if (cmbFormaPgto.selectedIndex == 2){
			nfe.formaPgtoNFE = ENfeFormaPgto.prazo;	
		}
		if (cmbFormaPgto.selectedIndex == 3){
			nfe.formaPgtoNFE = ENfeFormaPgto.outros;
		}
		
		//dados da nota
		nfe.ambienteNFE = ENfeAmbiente.homologacao;
		nfe.idMov = idMovNFE;
		
		//
		if(mov.cfop == null || mov.cfop.length < 4)
		{
			if(mov.resumo == EMovResumo.saida){
				mov.cfop = "5102";
			}
			else
			{
				mov.cfop = "1102";
			}
		}
		nfe.cfop = MyArrayUtils.getItemByField(App.single.cache.arrayCFOP, "codigo", mov.cfop).descricao;
		//formatar data
		var fmt:DateFormatter = new DateFormatter();
		fmt.formatString="DD/MM/YYYY";
		
		nfe.dtSaiEnt = fmt.format( dtf.selectedDate );
		
		
		nfe.numeroNota =  parseInt( txtNumero.text );
		nfe.serieNota = parseInt(txtSerie.text);
		//Dados emitente/destinatario
		nfe.idCliente = cliente.id;
		nfe.idEnderecoCliente = ceCliente.id;
		nfe.idEmp = empresa.id;
		nfe.idEnderecoEmp =  ceEmpresa.id;		
		//endereco de retirada e entrega
		if(mov.resumo == EMovResumo.saida)
		{
			nfe.idEnderecoEntrega = ceCliente.id;
			nfe.idEnderecoRetirada = ceEmpresa.id;
			nfe.clienteIE
				= (cmbClienteSituacaoInscrEst.selectedIndex==0)
				? ceCliente.inscr
				: 'ISENTO';
			if (nfe.clienteIE.length <3)
				nfe.clienteIE = 'ISENTO';
		}
		else if(mov.resumo == EMovResumo.entrada)
		{
			nfe.idEnderecoEntrega = ceEmpresa.id;
			nfe.idEnderecoRetirada = ceCliente.id;
		}
		
		//Dados de Transporte;
		if(cmbModalidade.selectedIndex == 1){
			nfe.tipoTranspNFE = ENfeTipoTransporte.emitente;
		}
		else{
			nfe.tipoTranspNFE = ENfeTipoTransporte.destinatario;	
		}
		//verificação transportadora
		if(transportadora != null){
			nfe.idClienteTransp = transportadora.id;
		}
		else{
			nfe.idClienteTransp = 0;
		}
		//verificação endereco transportadora
		if(ceTransp != null){
			nfe.idEnderecoTransp = ceTransp.id;
		}
		else{
			nfe.idEnderecoTransp = 0;
		}
		//verificação veiculo transportadora
		if(veicTransp != null){
			nfe.idVeiculo = veicTransp.id;
		}
		else{
			nfe.idVeiculo = 0;
		}				
		//verificação de reboque
		if(reboqueTRansp != null){
			nfe.idReboque = reboqueTRansp.id;
		}
		else{
			nfe.idReboque = 0;
		}
		
		//volume
		nfe.volQuantidade = nsQtd.value;
		nfe.volMarca = txtVolMarca.text;
		nfe.volEspecie = txtVolEspecie.text;
		nfe.volNumeracao = txtVolNum.text;
		nfe.volPesoBruto = nsVolPesoB.value;
		nfe.volPesoLiquido = nsVolPesoL.value;
		nfe.infoAdicional = txtInfAdicional.text.replace('\r','').replace('\n','');
		nfe.fatura = txtFaturamento.text.replace('\r','').replace('\n','');
		
		//movNFEVeiculos
		if(ckbVeiculo.selected)
		{
			nfe.ehVendaVeiculo = true;
			var nfeVeiculo: MovNfeVeiculo = new MovNfeVeiculo();
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
			nfe.__nfeVeiculo = nfeVeiculo;
			
		}
		
		FcdNfe.unica.SalvaMovNFE(idMovNFE, nfe,
			function():void
			{
				Alert.show("Salvei NFE");
				fechaPopupNovo();
				
			}
		);
	}
	
	//validação Endereco
	public function ValidaEndereco(obj:Object):Boolean
	{		
		var valido:Boolean = true;
		if (obj == null)
		{
			return true;
		}
		var ce:ClienteEndereco = obj as ClienteEndereco;
		
		if(ce.id == 0)
		{
			return true;
		}
		//validacao CEP
		if(ce.ufIBGE.length != 2 || ce.cidadeIBGE.length != 7 ){
			AlertaSistema.mensagem("Atualize o Endereço");
			valido = false;
		}
		
		return valido;		
	}
	
	//VAlidação veiculos
	public  function ValidaVeiculo(obj:Object):Boolean
	{
		var valido:Boolean = true;
		if (obj == null)
		{
			return true;
		}
		
		var cv:ClienteVeiculo =  obj as ClienteVeiculo;
		if(cv.id == 0){
			return true;
		}
		
		//Placa
		if (cv.placaNumero == null || cv.placaNumero.length <7)
		{
			AlertaSistema.mensagem("Placa Veículo Inválida.");
			valido = false;
		}
		//RNTC
		if ( cv.regNacTranspCarga == null || cv.regNacTranspCarga.length == 0)
		{
			AlertaSistema.mensagem("RNTC Inválido");
			valido = false;
		}
		//placaUF
		if ( cv.placaUF == null || cv.placaUF.length != 2){
			AlertaSistema.mensagem("UF da placa Inválido");
			valido = false;
		}
		return valido;
	}
	
	public function Validacao():Boolean
	{
		var valido:Boolean = true;
		
		//validação da nota
		if(txtNumero.text == '' || txtSerie.text == '' || 
		  cmbFinalidade.selectedIndex < 1  || cmbFormaPgto.selectedIndex < 1 )
		{
			AlertaSistema.mensagem("Preencha campos referente a nota");
			valido = false;
		}
		//validação endereco cliente/remetente
		if( !( ValidaEndereco(cmbEndEmpresa.selectedItem) 
			|| ValidaEndereco(cmbEndCliente.selectedItem) ) )
		{
			valido = false;
		}
		//validação do transportador
		if(transportadora != null){
			if( cmbEndTransp.selectedIndex >= 1 )
			{
				valido = ValidaEndereco(cmbEndTransp.selectedItem);
			}
			else
			{
				valido = false;
				AlertaSistema.mensagem("Escolha endereço do transportador.");
			}			
		}
		return valido;
	}
	