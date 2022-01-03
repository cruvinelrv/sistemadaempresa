using System;
using System.Collections.Generic;
using SDE.Outros;
using SDE.Enumerador;

namespace SDE.Constantes
{
    public class Variaveis_SdeConfig
    {
        //Thiago
        //esses valores não devem ser modificados
        //esses valores não devem ser modificados
        //esses valores não devem ser modificados
        //esses valores não devem ser modificados
        //esses valores não devem ser modificados
        //esses valores não devem ser modificados
        //esses valores não devem ser modificados

        public static string[] MENU_CAD_CLIENTE = new string[] { "Menu.Cadastro.Cliente", "0" };
        public static string[] MENU_CAD_CLIENTE2 = new string[] { "Menu.Cadastro.Cliente ", "1" };
        public static string[] MENU_CAD_FUNCIONARIO = new string[] { "Menu.Cadastro.Funcionário", "0" };
		public static string[] MENU_CAD_PRODUTO = new string[] { "Menu.Cadastro.Produto", "0"};
        public static string[] MENU_CAD_PRODUTO2 = new string[] { "Menu.Cadastro.Produto2", "0" };
        public static string[] MENU_CAD_SERVICO = new string[] { "Menu.Cadastro.Servico", "0"};
        public static string[] MENU_CAD_CARGO = new string[] { "Menu.Cadastro.Cargo", "0" };
        public static string[] MENU_CAD_OUTROS = new string[] { "Menu.Cadastro.Outros", "0" };

        public static string[] MENU_EST_ENTRADASIMPLES = new string[] {"Menu.Estoque.Entrada Simples", "0"};
        public static string[] MENU_EST_ENTRADACOMPLETA = new string[] { "Menu.Estoque.Entrada Completa", "0" };
        public static string[] MENU_EST_ALTERACAOENTRADA = new string[] { "Menu.Estoque.Alteração de Entrada", "0" };
        public static string[] MENU_EST_BALANCO = new string[] {"Menu.Estoque.Balanço", "0"};
        public static string[] MENU_EST_AJUSTEESTOQUE = new string[] {"Menu.Estoque.Ajuste de Estoque", "0"};
        public static string[] MENU_EST_CANCELAMENTO = new string[] {"Menu.Estoque.Cancelamento", "0"};

        public static string[] MENU_MOV_PDV = new string[] {"Menu.Movimentacoes.PDV", "0"};
        public static string[] MENU_MOV_PDVFINAN = new string[] {"Menu.Movimentacoes.PDV Titulos", "0"};
        public static string[] MENU_MOV_NFEENTRADA = new string[] {"Menu.Movimentacoes.NFE Entrada", "0"};
		public static string[] MENU_MOV_NFEENTRADARETORNO = new string[] {"Menu.Movimentacoes.NFE Entrada/Retorno", "0"};
        public static string[] MENU_MOV_NFESAIDA = new string[] {"Menu.Movimentacoes.NFE Saida", "0"};
        //public static string[] MENU_MOV_EMISSAONFE = new string[] {"Menu.Movimentacoes.Emissao NFE", "0"};
        public static string[] MENU_MOV_IMPRESSAONFE = new string[] {"Menu.Movimentacoes.NFE Impressao", "0"};
        public static string[] MENU_MOV_EMISSAONFS = new string[] {"Menu.Movimentacoes.Emissão NFS", "0"};

        public static string[] MENU_OS_CADASTROOS = new string[] {"Menu.Ordem de Serviço.Cadastro Ordem Servico", "0"};
        public static string[] MENU_OS_PAINELOS = new string[] {"Menu.Ordem de Serviço.Painel Ordem Servico", "0"};

        public static string[] MENU_CANCEL_MOV = new string[] { "Menu.Cancelamentos.Movimentação", "0" };

        public static string[] MENU_FIN_CADASTROS = new string[] {"Menu.Financeiro.Cadastros", "0"};
        public static string[] MENU_FIN_CONTASRECEBER = new string[] {"Menu.Financeiro.Contas a Receber", "0"};
        public static string[] MENU_FIN_CONTASPAGAR = new string[] { "Menu.Financeiro.Contas a Pagar", "0" };
        public static string[] MENU_FIN_TRANSFERENCIA = new string[] {"Menu.Financeiro.Transferencia Entre Contas", "0"};
        public static string[] MENU_FIN_DEBITOSCREDITOSAVISTA = new string[] { "Menu.Financeiro.Débitos / Créditos a Vista", "0" };
        public static string[] MENU_FIN_RECEBIMENTOROTA = new string[] { "Menu.Financeiro.Recebimento da Rota", "0" };
        public static string[] MENU_FIN_CONTROLECHEQUESRECEBER = new string[] { "Menu.Financeiro.Controle de Cheques a Receber", "0" };
        public static string[] MENU_FIN_DUPLICATA = new string[] { "Menu.Financeiro.Duplicata", "0" };
        //public static string[] MENU_FIN_CAIXA = new string[] { "Menu.Financeiro.Caixa", "1" };

        public static string[] MENU_CAIXA_RETIRADA = new string[] { "Menu.Caixa.Retirada", "0" };
        public static string[] MENU_CAIXA_ABERTURA = new string[] { "Menu.Caixa.Abertura", "0" };
        public static string[] MENU_CAIXA_ENTRADA = new string[] { "Menu.Caixa.Entrada", "0" };
        public static string[] MENU_CAIXA_TRANSFERENCIACONTA = new string[] { "Menu.Caixa.Transferência Para Conta", "0" };

        public static string[] MENU_ATENDIMENTO_LISTACASAMENTO = new string[] { "Menu.Atendimento.Lista de Casamento", "0" };
        public static string[] MENU_ATENDIMENTO_LISTAPRESENTES = new string[] { "Menu.Atendimento.Lista de Presentes", "0" };

        //public static string[] MENU_IMP_RELATORIOS = new string[] {"Menu.Impressões.Relatorios", "0"};
        //public static string[] MENU_IMP_ETIQUETAS = new string[] {"Menu.Impressões.Etiquetas", "0"};

        public static string[] MENU_REL_IMPRESSAO = new string[] { "Menu.Relatórios.Impressão", "0" };
        public static string[] MENU_REL_REIMPRESSAO = new string[] { "Menu.Relatórios.Reimpressão", "0" };
        public static string[] MENU_ETIQ_IMPRESSAO = new string[] { "Menu.Etiquetas.Impressão", "0" };
        public static string[] MENU_ETIQ_IMPRESSAOMOV = new string[] { "Menu.Etiquetas.Impressão por Movimentação", "0" };
        public static string[] MENU_ETIQ_IMPRESSAOLISTA = new string[] { "Menu.Etiquetas.Impressão por Item", "0" };

        public static string[] MENU_TEC_PARAM  = new string[] {"Menu.Tecnico.Parametrizacao", "0"};
        public static string[] MENU_TEC_PRINCIPAL = new string[] {"Menu.Tecnico.Principal", "0"};
		public static string[] MENU_TEC_LOGIN = new string[] { "Menu.Tecnico.Login", "0" };

        //public static string[] EMPRESA_VENDAESTOQUENEGATIVO = new string[] {"Empresa.VendaEstoqueNegativo", "0"};
        public static string[] CORPORACAO_CFOPPADRAO = new string[] {"Corporacao.CFOP Padrao", "5102"};
        public static string[] EMPRESA_ALIQUOTASECF = new string[] { "Empresa.Aliquotas ECF", "17" };
        public static string[] EMPRESA_IMPOSTOS_SIMPLESNACIONAL = new string[] { "Empresa.Impostos.Simples Nacional", "0" };
        
        public static string[] FINANCEIRO_CONTAS_CAPITALTOTAL = new string[] { "Financeiro.Contas.Capital Total", "1" };
        public static string[] FINANCEIRO_CONTAS_ADMINISTRATIVO = new string[] { "Financeiro.Contas.Administrativo", "2" };
        //public static string[] FINANCEIRO_CONTAS_RECEITAVENDA = new string[] { "Financeiro.Contas.Receita Venda", "3" }; 

        public static string[] FINANCEIRO_TIPOSLANCAMENTO_MANUAL = new string[] { "Financeiro.Tipos de Lancamento.Manual", "1" };
        public static string[] FINANCEIRO_TIPOSLANCAMENTO_RECEITAVENDA = new string[] { "Financeiro.Tipos de Lancamento.Receita Venda", "2" };

        public static string[] RELATORIO_ABREIMPRESSAO_IE = new string[] { "Relatório.Abre Impressão.IE", "0" };
        public static string[] EMPRESA_VENDEVEICULOS = new string[] { "Empresa.Vende Veículos", "0" };
        public static string[] EMPRESA_VENDECOMBUSTIVEL = new string[] { "Empresa.Vende Combustível", "0" };

        public static string[] EMPRESA_NFE_AMBIENTE = new string[] { "Empresa.NFe.Ambiente", "0" }; // 1:produção 0:homologação
        public static string[] EMPRESA_PDV_NFEXML = new string[] { "Empresa.PDV. NFe XML", "0" }; // 0:transmite NFe em arquivo txt 1:transmite NFe em arquivo xml

        public static string[] EMPRESA_EMAIL_ENDERECO = new string[] { "Empresa.Email.Endereco", "" };
        public static string[] EMPRESA_EAMIL_SENHA = new string[] { "Empresa.Email.Senha", "" };

        public static string[] EMPRESA_ETIQUETA_PORTACOM = new string[] { "Empresa.Etiqueta.Porta COM", "COM1" };

        public static string[] MENU_GESTAO_AJUSTEPRECO = new string[] { "Menu.Gestão.Ajuste de Preço", "0" };
        public static string[] EMPRESA_ENDERECO_MAC = new string[] { "Empresa.Endereço.MAC", "" };
    }
}