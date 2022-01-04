// ActionScript file
import SDE.Entidade.Item;
import SDE.Entidade.ItemEmpEstoque;
import SDE.FachadaServico.FcdRelatorio;

import impressoes.etiquetas.EtiquetaFabricaBodies;

import org.doc.PaperFormat;
import org.print.Body;
import org.print.Report;

	private function desenhaEtiqueta():void
	{	
		var rpt:Report = new Report();
		var fBuscou:Function = 
			function(retorno:Array):void
			{
				var listaEtiqueta:Array = [];
				
				var saltar:Number =parameters.saltar;
				for (var k:int = 0; k<saltar; k++)
				{
					listaEtiqueta.push(null);
				}
				
				//quantidade pela movimentação
				if(parameters.tipo == "movimentacao")
				{
					for each(var it1:Item in retorno){
						listaEtiqueta.push(it1);
					}
				}
				else if(parameters.tipo == "produto")
				{
				//Quantidade pela lista de itens
					for each(var it:Item in retorno){
						for(var i:int = 0; i<listaID.length; i++)
						{
							var id:int = listaID[i];
							var iee: ItemEmpEstoque = it.__estoques[0];
							if( iee.id == id)
							{
								var qtd:int = listaQtd[i];
								for(var j:int = 0; j<qtd; j++)
								{
									listaEtiqueta.push(it);
								}
								break;
							}
						}
					}	
				}
				
				//Etiqueta
				var body:Body = EtiquetaFabricaBodies.unica.getBody(parameters.etiqueta, listaEtiqueta);
				rpt.addChild(body);
				doc = new Document( rpt, body.dados, PaperFormat.A4);
			}	
			
		var listaID:Array = null;
		var listaQtd:Array = null;
		if(parameters.tipo == "movimentacao"){
			FcdRelatorio.unica.EtiquetasIdMov(parameters.idCorp, parameters.idEmp, parameters.idMov, fBuscou);
		}
		else if (parameters.tipo == "produto")
		{
			listaID = new Array;
			listaQtd = new Array;
			parameters.total_pares;
			for (var i:int=0; i < parameters.total_pares; i++)
			{		
				var s:String = parameters["lista"+i];
			 	var a:Array =  s.split(",");			 	
			 	listaID.push( a[0]);
			 	listaQtd.push ( a[1]);				
			}
			FcdRelatorio.unica.Etiquetas(parameters.idCorp, parameters.idEmp, listaID,fBuscou );
		}
	}
	

	private function desenhaRelatorio():void
	{
		var rpt:Report = new Report();
		var head:RelatorioCabecalho = new RelatorioCabecalho();
		var tipoRel:String = "";
		
		//cabecalho do relatorio
		var fCab:Function =
			function(retorno:Empresa):void
			{					
				var cabecalho:ModeloCabecalho = new ModeloCabecalho();
				cabecalho.empresa = retorno.__cliente.nome;
				cabecalho.cpf_cnpj = Funcoes.MascaraCPF(retorno.__cliente.cpf_cnpj);
				cabecalho.relatorio = tipoRel;
				
				head.data = cabecalho;
			}
		FcdRelatorio.unica.LoadCabecalho(parameters.idCorp, parameters.idEmp, fCab);
		//rodape
		//corpo do relatorio
		var fBuscou:Function =
			function(retorno:Array):void
			{
				var body:Body = RelatorioFabricaBodies.unica.getBody( parameters.relatorio, retorno);
				if(parameters.relatorio != "nfe")
				{
					rpt.addChild( head );
				}
				rpt.addChild( body );				
				doc = new Document( rpt, body.dados, PaperFormat.A4);
			}
		//tipo do relatorio
		if(parameters.relatorio == "movimentacao")
		{
			tipoRel = "Relatório de Movimentação";
			var pfMov:ParamFiltroMov = new ParamFiltroMov();
			pfMov.idMov = parameters.idMov;
			FcdRelatorio.unica.RelMovId( parameters.idCorp, pfMov, fBuscou);
		}
		else if(parameters.relatorio == "movDiario")
		{
			tipoRel = "Relatório Diário de Movimentações";	
			var pfMovD:ParamFiltroMov = new ParamFiltroMov();
			pfMovD.dtInicial = parameters.dtInicial;
			pfMovD.dtFinal =  parameters.dtFinal;
			pfMovD.tipos = parameters.tipos;
			FcdRelatorio.unica.RelMovDiario(parameters.idCorp, pfMovD, fBuscou);
		}
		else if(parameters.relatorio == "movResumo")
		{
			tipoRel = "Relatório Diário de Movimentações";	
			var pfMovR:ParamFiltroMov = new ParamFiltroMov();
			pfMovR.dtInicial = parameters.dtInicial;
			pfMovR.dtFinal = parameters.dtFinal;
			pfMovR.tipos = parameters.tipos;
			FcdRelatorio.unica.RelMovResumo(parameters.idCorp, pfMovR, fBuscou);
		}
		else if(parameters.relatorio == "pessoas")
		{
			tipoRel = "Relatório de Clientes";
			var pfCliente: ParamFiltroCliente = new ParamFiltroCliente();
			pfCliente.tipo = EPesTipo.nao_informado;
			if(parameters.tipos == "fornecedor")
				pfCliente.fornecedor = true;
			if(parameters.tipos == "funcionario")
				pfCliente.funcionario = true;
			if(parameters.tipos == "transportador")
				pfCliente.transportador = true;
			if(parameters.tipos == "parceiro")
				pfCliente.parceiro = true;
			FcdRelatorio.unica.RelClientes(parameters.idCorp, pfCliente, fBuscou);
		}
		else if(parameters.relatorio == "estoques")
		{
			tipoRel = "Relatório de Estoques";
			var pfItem: ParamFiltroItem = new ParamFiltroItem();
			FcdRelatorio.unica.RelEstoque(parameters.idCorp, parameters.idEmp, pfItem, fBuscou);
		}
		else if(parameters.relatorio == "nfe")
		{
			FcdRelatorio.unica.RelNFE(parameters.idCorp, parameters.idEmp, parameters.idMov, fBuscou);
		}


	
	}