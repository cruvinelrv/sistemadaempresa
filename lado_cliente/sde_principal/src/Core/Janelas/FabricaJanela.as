package Core.Janelas
{
	import Core.Alerta.AlertaSistema;
	
	import SDE.Constantes.Variaveis_SdeConfig;
	
	import janelas.OrdemServico.CadastroOS.JnlCadastroOrdemServico;
	import janelas.OrdemServico.PainelOS.JanelaPainelOS1;
	import janelas.atendimento.ListaPresente.JnlAtendimentoListaPresentes;
	import janelas.cadastro.Cargo2.JnlCadCargo2;
	import janelas.cadastro.Cliente1.JanelaCadCliente;
	import janelas.cadastro.Cliente2.JnlCadCliente;
	import janelas.cadastro.Funcionario.JnlCadFuncionario;
	import janelas.cadastro.ItemProduto1.JanelaCadItemProduto;
	import janelas.cadastro.ItemProduto2.JnlCadItemProduto;
	import janelas.cadastro.ItemServico2.JnlCadItemServico;
	import janelas.cadastro.Outros1.JanelaCadOutros;
	import janelas.caixa.Abertura.JnlCaixa_Abertura;
	import janelas.caixa.Entrada.JnlCaixa_Entrada;
	import janelas.caixa.RetiradaParaPagamento.JnlCaixa_RetiradaParaPagamento;
	import janelas.caixa.TransferenciaParaConta.JnlCaixa_TransferenciaParaConta;
	import janelas.financeiro.FinCadastros.JanelaFinCadastros;
	import janelas.financeiro.FinCheque.JnlFinControleChequesReceber;
	import janelas.financeiro.FinContasPagar.JnlFinContasPagar;
	import janelas.financeiro.FinContasReceber.JnlFinContasReceber;
	import janelas.financeiro.FinDuplicata.JnlFinDuplicata;
	import janelas.financeiro.FinNovoLancamento.JnlFinDebCred;
	import janelas.financeiro.FinNovoLancamento.JnlFinLancamentoNovo;
	import janelas.financeiro.FinNovoLancamento.JnlFinRecebimentoRota;
	import janelas.gestao.AjustePreco.JnlGestaoAjustePreco;
	import janelas.mov.AjusteEstoque.JnlAjusteEstoque1;
	import janelas.mov.Balanco2.JnlBalanco;
	import janelas.mov.Cancel1.JanelaCancel1;
	import janelas.mov.EntradaCompleta.JanelaEntradaCompleta;
	import janelas.mov.EntradaSimples.JanelaEntradaSimples;
	import janelas.mov.EntradaSimples.JnlAlteracaoEntradaSimples;
	import janelas.mov.ImpressaoNFE1.JanelaImpressaoNFE1;
	import janelas.mov.NFServicos.JanelaNotaPrefeitura;
	import janelas.mov.PDV.JanelaPDV1;
	import janelas.mov.PDV2.JanelaPDV2;
	import janelas.mov.PDV4.JanelaPDV4;
	import janelas.mov.PdvEntrada.JnlPdvEntrada;
	import janelas.mov.PdvGeraTitulo.JnlPdvGeraTitulo;
	import janelas.relatorios.etiquetas1.JanelaEtiquetas1;
	import janelas.relatorios.etiquetas1.JnlEtiquetasLista;
	import janelas.relatorios.etiquetas1.JnlEtiquetasMov;
	import janelas.relatorios.relatorios1.JanelaRelatoriosReimpressao;
	import janelas.relatorios.relatorios1.JnlRelatorios;
	import janelas.tecnico.Parametrizacao.JnlTecParametrizacao;
	import janelas.tecnico.Tecnico.JnlTecPrincipal;
	import janelas.tecnico.Usuario.JnlTecUsuario;
	
	import mx.core.Application;
	
	public final class FabricaJanela
	{
		private var referencias:Array =
			[
				{variavel:Variaveis_SdeConfig.MENU_CAD_CLIENTE, classe:JanelaCadCliente},
				{variavel:Variaveis_SdeConfig.MENU_CAD_CLIENTE2, classe:JnlCadCliente},
				{variavel:Variaveis_SdeConfig.MENU_CAD_FUNCIONARIO, classe:JnlCadFuncionario},
				{variavel:Variaveis_SdeConfig.MENU_CAD_PRODUTO, classe:JanelaCadItemProduto},
				{variavel:Variaveis_SdeConfig.MENU_CAD_PRODUTO2, classe:JnlCadItemProduto},
				{variavel:Variaveis_SdeConfig.MENU_CAD_SERVICO, classe:JnlCadItemServico},
				{variavel:Variaveis_SdeConfig.MENU_CAD_CARGO, classe:JnlCadCargo2},
				{variavel:Variaveis_SdeConfig.MENU_CAD_OUTROS, classe:JanelaCadOutros},
				{variavel:Variaveis_SdeConfig.MENU_EST_ENTRADASIMPLES, classe:JanelaEntradaSimples},
				{variavel:Variaveis_SdeConfig.MENU_EST_ENTRADACOMPLETA, classe:JanelaEntradaCompleta},
				{variavel:Variaveis_SdeConfig.MENU_EST_ALTERACAOENTRADA, classe:JnlAlteracaoEntradaSimples},
				//{variavel:Variaveis_SdeConfig.MENU_EST_BALANCO, classe:JanelaBalanco1},
				{variavel:Variaveis_SdeConfig.MENU_EST_BALANCO, classe:JnlBalanco},
				{variavel:Variaveis_SdeConfig.MENU_EST_AJUSTEESTOQUE, classe:JnlAjusteEstoque1},
				{variavel:Variaveis_SdeConfig.MENU_EST_CANCELAMENTO, classe:JanelaCancel1},
				{variavel:Variaveis_SdeConfig.MENU_MOV_PDV, classe:JanelaPDV1},
				{variavel:Variaveis_SdeConfig.MENU_MOV_PDVFINAN, classe:JnlPdvGeraTitulo},
				{variavel:Variaveis_SdeConfig.MENU_MOV_IMPRESSAONFE, classe:JanelaImpressaoNFE1},
				{variavel:Variaveis_SdeConfig.MENU_MOV_EMISSAONFS, classe:JanelaNotaPrefeitura},
				{variavel:Variaveis_SdeConfig.MENU_MOV_NFEENTRADA, classe:JanelaPDV4},
				{variavel:Variaveis_SdeConfig.MENU_MOV_NFEENTRADARETORNO, classe:JnlPdvEntrada},
				{variavel:Variaveis_SdeConfig.MENU_MOV_NFESAIDA, classe:JanelaPDV2},
				{variavel:Variaveis_SdeConfig.MENU_OS_CADASTROOS, classe:JnlCadastroOrdemServico},
				{variavel:Variaveis_SdeConfig.MENU_CANCEL_MOV, classe:JanelaCancel1},
				//{variavel:Variaveis_SdeConfig.MENU_IMP_RELATORIOS, classe:JanelaRelatorios1},
				//{variavel:Variaveis_SdeConfig.MENU_IMP_ETIQUETAS, classe:JanelaEtiquetas1},
				{variavel:Variaveis_SdeConfig.MENU_REL_IMPRESSAO, classe:JnlRelatorios},
				{variavel:Variaveis_SdeConfig.MENU_REL_REIMPRESSAO, classe:JanelaRelatoriosReimpressao},
				{variavel:Variaveis_SdeConfig.MENU_ETIQ_IMPRESSAO, classe:JanelaEtiquetas1},
				{variavel:Variaveis_SdeConfig.MENU_ETIQ_IMPRESSAOMOV, classe:JnlEtiquetasMov},
				{variavel:Variaveis_SdeConfig.MENU_ETIQ_IMPRESSAOLISTA, classe:JnlEtiquetasLista},
				{variavel:Variaveis_SdeConfig.MENU_FIN_CADASTROS, classe:JanelaFinCadastros},
				{variavel:Variaveis_SdeConfig.MENU_FIN_CONTASRECEBER, classe:JnlFinContasReceber},
				{variavel:Variaveis_SdeConfig.MENU_FIN_CONTASPAGAR, classe:JnlFinContasPagar},
				{variavel:Variaveis_SdeConfig.MENU_FIN_CONTROLECHEQUESRECEBER, classe:JnlFinControleChequesReceber},
				{variavel:Variaveis_SdeConfig.MENU_FIN_DUPLICATA, classe:JnlFinDuplicata},
				{variavel:Variaveis_SdeConfig.MENU_CAIXA_RETIRADA, classe:JnlCaixa_RetiradaParaPagamento},
				{variavel:Variaveis_SdeConfig.MENU_CAIXA_ABERTURA, classe:JnlCaixa_Abertura},
				{variavel:Variaveis_SdeConfig.MENU_CAIXA_ENTRADA, classe:JnlCaixa_Entrada},
				{variavel:Variaveis_SdeConfig.MENU_CAIXA_TRANSFERENCIACONTA, classe:JnlCaixa_TransferenciaParaConta},
				{variavel:Variaveis_SdeConfig.MENU_ATENDIMENTO_LISTAPRESENTES, classe:JnlAtendimentoListaPresentes},
				{variavel:Variaveis_SdeConfig.MENU_TEC_PARAM, classe:JnlTecParametrizacao},
				{variavel:Variaveis_SdeConfig.MENU_TEC_PRINCIPAL, classe:JnlTecPrincipal},
				{variavel:Variaveis_SdeConfig.MENU_TEC_LOGIN, classe:JnlTecUsuario},
				{variavel:Variaveis_SdeConfig.MENU_OS_CADASTROOS, classe:JnlCadastroOrdemServico},
				{variavel:Variaveis_SdeConfig.MENU_OS_PAINELOS, classe:JanelaPainelOS1},
				{variavel:Variaveis_SdeConfig.MENU_FIN_TRANSFERENCIA, classe:JnlFinLancamentoNovo},
				{variavel:Variaveis_SdeConfig.MENU_FIN_DEBITOSCREDITOSAVISTA, classe:JnlFinDebCred},
				{variavel:Variaveis_SdeConfig.MENU_FIN_RECEBIMENTOROTA, classe:JnlFinRecebimentoRota},
				{variavel:Variaveis_SdeConfig.MENU_GESTAO_AJUSTEPRECO, classe:JnlGestaoAjustePreco}
			];
		
		public function criaMenu(variavel:String):*
		{
			for each(var o:* in referencias)
			{
				if (variavel==o.variavel)
					return o;
			}
			if (Application.application.sessao.modoTecnico)
				AlertaSistema.mensagem("desenvolvedor, registre o menu:\n"+variavel);
		}
		
	}
}