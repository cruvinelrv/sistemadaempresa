import Core.Alerta.AlertaSistema;
import Core.Sessao;

import SDE.CamadaNuvem.NuvemModificacoes;
import SDE.Constantes.Variaveis_SdeConfig;
import SDE.Entidade.OrdemServico_Executor;
import SDE.Entidade.OrdemServico_Item;

import flash.net.URLRequest;
import flash.net.navigateToURL;

import img.Imagens;

import mx.controls.Alert;
import mx.core.Application;
import mx.utils.Base64Encoder;
	
	private function salvaOrdemServico(resumo:String, tipo:String, impressao:String):void
	{
		/**INICIA VALIDAÇÃO*/
		var msg:String = "";
		if (txtNumOS.text == "")
			msg += "Digite o número do serviço\n";
		if (dfDataInicio.text == "")
			msg += "Informe a data de início\n";
		if (dfDataPrevisao.text == "")
			msg += "Informe a data de previsão de conclusão\n";		
		if (txtDescricao.text == "")
			msg += "Insira uma descrição para a OS\n";
		
		if (ordemServico.cliente_contato == "" && ordemServico.cliente_cpf == "" &&
			ordemServico.cliente_endereco_cobranca == "" && ordemServico.cliente_nome == "")
			msg += "Pesquise ou insira os dados do cliente";
		
		if(msg != "")
		{
			AlertaSistema.mensagem(msg);
			return;
		}
		/**FIM VALIDAÇÃO*/
		
		ordemServico.veiculo = txtVeiculo.text;
		ordemServico.placa = txtPlaca.fullText;
		ordemServico.kilometragem = nsKilometragem.value.toString();
		ordemServico.numMotor = txtNumMotor.text;
		ordemServico.maquina = txtMaquina.text;
		ordemServico.implAgricola = txtImplAgricola.text;
		ordemServico.equipamento = txtEquipamento.text;
		ordemServico.numSerie = txtNumSerie.text;
		ordemServico.servico = txtServico.text;
		ordemServico.defeitoReclamado = txtDefeitoReclamado.text;
		ordemServico.defeiroConstatado = txtDefeitoConstatado.text;
		
		ordemServico.__oSItens = [];
		ordemServico.idCliente = cliente.id;
		ordemServico.idClienteFuncionarioLogado = Sessao.unica.idClienteFuncionarioLogado;
		ordemServico.idEmp = Sessao.unica.idEmp;
		
		ordemServico.numOS = txtNumOS.text;
		ordemServico.descricao = txtDescricao.text;
		ordemServico.obs = txtObservacoes.text;
		ordemServico.dthrInicio = dfDataInicio.text;
		ordemServico.dthrPrevisao = dfDataPrevisao.text;
		ordemServico.isContratado = rbComContrato.selected;
		
		for each (var obj:Object in listaOSI)
		{
			var osi:OrdemServico_Item = obj.ordemServicoItem;
			osi.__executores = [];
			ordemServico.vlrItensInicial += osi.qtd * osi.vlrUnitVendaInicial;
			ordemServico.vlrItensFinal += osi.qtd * osi.vlrUnitVendaFinal;
			osi.tipoItem = obj.tipo_sigla;
			
			for each (var ose:OrdemServico_Executor in obj.executores)
			{
				if (osi.__removaMe)
					ose.__removaMe = osi.__removaMe;
				ose.idOrdemServicoItem = osi.id;
				osi.__executores.push(ose);
			}
			
			ordemServico.__oSItens.push(osi);
		}
		//ordemServico.vlrAcrescimo = -nsDescontoVlr.value;
		ordemServico.vlrTotal = ordemServico.vlrItensFinal + ordemServico.vlrAcrescimo;
		
		AlertaSistema.mensagem("ID Tipo OS: " + ordemServico.idOrdemServicoTipo, true);
		
		Sessao.unica.nuvens.modificacoes.OrdemServico_NovoAtualizacao(ordemServico,
			function(retornoIdOrdemServico:Number):void
			{
				AlertaSistema.mensagem("Salvei", true);
				Application.application.sessao.nuvens.listagem.ListaDB(OrdemServico_Item.CLASSE);
				Application.application.sessao.nuvens.listagem.ListaDB(OrdemServico_Executor.CLASSE);
				
				Alert.show("Deseja imprimir uma página com a Ordem de Serviço?", "Imprimir?", Alert.YES+Alert.NO, null,
					function(ev:CloseEvent):void
					{
						if(ev.detail == Alert.YES)
						{
							if (!Application.application.gerenteConexaoDesktop)
							{
								Alert.show("É necessário estar conectado ao SDE Desktop para a impressão", "Mensagem SDE", 4, null, null, Imagens.unica.icn_32_alerta);
								return;
							}
							
							var nuvemModificacoes:NuvemModificacoes = new NuvemModificacoes();
							nuvemModificacoes.RelatorioOrdemServico(retornoIdOrdemServico,
								function():void
								{
									Application.application.gerenteConexaoDesktop.baixaRelatorioOrdemServico(Sessao.unica.idCorp, retornoIdOrdemServico);
								}
							);
							
							/*
							var ss:Sessao = Application.application.sessao;
		                    
							var enc:Base64Encoder = new Base64Encoder();
							enc.encodeUTFBytes("corp"+ss.idCorp);
		                    var hash:String = enc.toString();
		                    
		                    if (ss.parametrizacao.getParametro(Variaveis_SdeConfig.RELATORIO_ABREIMPRESSAO_IE) == "1"
		                    	&& Application.application.gerenteConexaoDesktop!=null)
		                    {
			                	var url:String = "http://sistemadaempresa.com.br/sde/imprime.aspx";
			                	url+="?idCorp="	+ss.idCorp;
			                    url+="&idEmp="	+ss.idEmp;
			                    url+="&hash="	+hash;
			                    url+="&tipo_impressao=relatorio";
			                    url+="&relatorio=Ordem Serviço";
			                    url+="&idOrdemServico="+retornoIdOrdemServico;
			                    
			                    url = "\""+url+"\"";
			                    Application.application.gerenteConexaoDesktop.iniciaProcesso("explorer", url);
			                    return;
		                    }
		                    
		                    var urlR:URLRequest = new URLRequest("imprime.aspx");
		                    urlR.method = URLRequestMethod.GET;
		                    
		                    var vars:URLVariables = new URLVariables();
		                    vars.idOrdemServico  = retornoIdOrdemServico;
		                    vars.idCorp = ss.idCorp;
		                    vars.idEmp  = ss.idEmp;
		                    
		                    vars.tipo_impressao = "relatorio";
		                    vars.relatorio = "Ordem Serviço";
		                    
							vars.hash = hash;
		                    
		                    urlR.data = vars;
		                    navigateToURL(urlR, "_blank");
		                    */
						}
					}
				
				);
				
				limpaTela();
				vs.selectedChild = etapa1;
			}
		);
	}