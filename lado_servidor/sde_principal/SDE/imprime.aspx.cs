using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using SDE.CamadaServico;
using SDE.Entidade;
using SDE.Parametro;
using SDE.Enumerador;
using Db4objects.Db4o;
using Db4objects.Db4o.Query;
using System.Globalization;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using SDE.CamadaNuvem;
using SDE.RelatoriosPaginaWeb;

namespace SDE.Web
{
    public partial class imprime : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            
            body.Attributes["class"] = "carregando";
            Timer1.Interval = 1000;
            Timer1.Enabled = true;

            Page.EnableViewState = false;
        }

        protected void Timer1_Tick(object sender, EventArgs e)
        {
            Timer1.Enabled = false;
            escreveRelatorio();
            body.Attributes["class"] = "";
        }

        private void escreveRelatorio()
        {
            try
            {
                int idCorp = int.Parse(Request.QueryString["idCorp"]);
                int idEmp = int.Parse(Request.QueryString["idEmp"]);

                //string hashCorp = Request.QueryString["hash"];
                //Request.QueryString[""]
                string tipoImpressao = Request.QueryString["tipoImpressao"];

                StringBuilder sb = new StringBuilder();

                switch (tipoImpressao)
                {
                    case "relatorio":
                        ERelatorio relatorio = GetRelatorio(Request.QueryString["relatorio"]);

                        switch (relatorio)
                        {
                            case ERelatorio.estoque:
                                RelEstoque relEstoque = new RelEstoque();
                                sb = relEstoque.GeraRelEstoque(idCorp, idEmp, Convert.ToBoolean(Request.QueryString["exibeGrade"]), Convert.ToBoolean(Request.QueryString["exibeEstoqueZerado"]));
                                break;
                            case ERelatorio.lista_preco:
                                RelListaPreco relListaPreco = new RelListaPreco();
                                sb = relListaPreco.GeraRelListaPreco(idCorp, idEmp, Convert.ToInt32(Request.QueryString["idMarca"]));
                                break;
                            case ERelatorio.listagem_balanco:
                                RelListagemBalanco relListagemBalanco = new RelListagemBalanco();
                                sb = relListagemBalanco.GeraRelListagemBalanco(idCorp, idEmp, Request.QueryString["campoOrdenacao"]);
                                break;
                            case ERelatorio.espelho_mov:
                                RelEspelhoMov relEspelhoMov = new RelEspelhoMov();
                                sb = relEspelhoMov.GeraRelEspelhoMov(idCorp, idEmp, Request.QueryString["dtInicial"], Request.QueryString["dtFinal"], Convert.ToInt32(Request.QueryString["idCliente"]), Convert.ToInt32(Request.QueryString["idVendedor"]), Convert.ToInt32(Request.QueryString["idItem"]), Convert.ToInt32(Request.QueryString["idMov"]),
                                    Convert.ToBoolean(Request.QueryString["entrada_compra"]), Convert.ToBoolean(Request.QueryString["saida_venda"]), Convert.ToBoolean(Request.QueryString["outros_orcamento"]), Convert.ToBoolean(Request.QueryString["entrada_cancel"]), Convert.ToBoolean(Request.QueryString["saida_cancel"]), Convert.ToBoolean(Request.QueryString["ambos_ajuste_estoque"]), Convert.ToBoolean(Request.QueryString["ambos_balan"]), Convert.ToBoolean(Request.QueryString["outros_reserva"]), Convert.ToBoolean(Request.QueryString["outros_pedido"]),
                                    Convert.ToBoolean(Request.QueryString["sem_impressao"]), Convert.ToBoolean(Request.QueryString["nfe_produto"]), Convert.ToBoolean(Request.QueryString["nf_formulario"]), Convert.ToBoolean(Request.QueryString["cupom_fiscal"]), Convert.ToBoolean(Request.QueryString["orcamento"]), Convert.ToBoolean(Request.QueryString["reserva"]), Convert.ToBoolean(Request.QueryString["pedido"]));
                                break;
                            case ERelatorio.caixa:
                                RelCaixa relCaixa = new RelCaixa();
                                sb = relCaixa.GeraRelCaixa(idCorp, idEmp, Request.QueryString["dtCaixa"], Convert.ToBoolean(Request.QueryString["corporativo"]));
                                break;
                            case ERelatorio.pis_cofins:
                                RelPisCofins relPisCofins = new RelPisCofins();
                                sb = relPisCofins.GeraRelPisCofins(idCorp, idEmp, Request.QueryString["dtInicial"], Request.QueryString["dtFinal"]);
                                break;
                            case ERelatorio.extrato_conta_corrente_caixa:
                                RelExtratoContaCorerenteCaixa relExtratoContaCorrenteCaixa = new RelExtratoContaCorerenteCaixa();
                                sb = relExtratoContaCorrenteCaixa.GeraRelExtratoContaCorrenteCaixa(idCorp, idEmp, Convert.ToInt32(Request.QueryString["idConta"]), Convert.ToInt32(Request.QueryString["idCentroCusto"]), Convert.ToInt32(Request.QueryString["idPlanoConta"]), Request.QueryString["dtInicial"], Request.QueryString["dtFinal"]);
                                break;
                            case ERelatorio.titulos_receber_pagar:
                                RelTitulosReceberPagar relTitulosReceberPagar = new RelTitulosReceberPagar();
                                sb = relTitulosReceberPagar.GeraRelTitulosReceberPagar(idCorp, idEmp, Request.QueryString["dtInicial"], Request.QueryString["dtFinal"], Convert.ToInt32(Request.QueryString["idCliente"]), Convert.ToInt32(Request.QueryString["idPortador"]),
                                    Convert.ToBoolean(Request.QueryString["titulo_a_pagar"]), Convert.ToBoolean(Request.QueryString["titulo_a_receber"]), Convert.ToBoolean(Request.QueryString["em_aberto"]), Convert.ToBoolean(Request.QueryString["lancado"]));
                                break;
                            case ERelatorio.cheque:
                                RelCheques relCheques = new RelCheques();
                                sb = relCheques.GeraRelCheques(idCorp, idEmp, Request.QueryString["dtInicial"], Request.QueryString["dtFinal"], Convert.ToBoolean(Request.QueryString["cheques_a_receber"]), Convert.ToBoolean(Request.QueryString["cheques_baixados"]), Convert.ToBoolean(Request.QueryString["cheques_compensados"]), Convert.ToBoolean(Request.QueryString["cheques_devolvidos"]));
                                break;
                            case ERelatorio.comissionamento:
                                RelComissionamentoDinamico relComissionamentoDinamico = new RelComissionamentoDinamico();
                                sb = relComissionamentoDinamico.GeraRelComissionamentoDinamico(idCorp, idEmp, Request.QueryString["dtInicial"], Request.QueryString["dtFinal"], Convert.ToInt32(Request.QueryString["idFuncionario"]), Convert.ToBoolean(Request.QueryString["exibeMovimentacao"]));
                                break;
                            case ERelatorio.produtos_vendidos_periodo:
                                RelProdutosVendidosPeriodo relProdutosVendidosPeriodo = new RelProdutosVendidosPeriodo();
                                sb = relProdutosVendidosPeriodo.GeraRelProdutosVendidosPeriodo(idCorp, idEmp, Convert.ToInt32(Request.QueryString["idItem"]), Request.QueryString["dtInicial"], Request.QueryString["dtFinal"]);
                                break;

                            case ERelatorio.lista_prod_tributo:
                                RelListaProdutoTributacao relListaProdutoTributacao = new RelListaProdutoTributacao();
                                sb = relListaProdutoTributacao.GeraRelListaProdutoTributacao(idCorp, idEmp, Convert.ToInt32(Request.QueryString["idMarca"]));
                                break;
                            case ERelatorio.verificacao_balanco:
                                RelVerificacaoBalanco relVerificacaoBalanco = new RelVerificacaoBalanco();
                                sb = relVerificacaoBalanco.GeraRelVerificacaoBalanco(idCorp, idEmp, Convert.ToInt32(Request.QueryString["idBalanco"]), Convert.ToBoolean(Request.QueryString["somenteDivergencias"]));
                                break;
                                //impressão de verificação de balanço.teste
                                

                        }
                        break;
                    default:
                        break;
                }

                anota("form1 =");
                Literal1.Text = sb.ToString();

                anota("form1 =;");
            }
            catch (Exception)
            {
                Response.Write("<h1>Ocorreu um erro durante a construção do relatório.</h1>");
                //Vinicius 03out10 para ver o erro do relatorio
             //   throw; // new Exception(ex.ToString());
            }
            anota("acabou metodo");
        }

        private ERelatorio GetRelatorio(string relatorio)
        {
            return (ERelatorio)Enum.Parse(typeof(ERelatorio), relatorio);
        }


        //private string APPEND = @"C:\Inetpub\Dados\lalale.txt";
        //private long ticks, dif;
        private void anota(string s)
        {
            /*
            dif = DateTime.Now.Ticks - ticks;
            System.IO.File.AppendAllText(APPEND, DateTime.Now.TimeOfDay.ToString() + " - " + s + Environment.NewLine);
            ticks = DateTime.Now.Ticks;
             * */
        }
    }
}
