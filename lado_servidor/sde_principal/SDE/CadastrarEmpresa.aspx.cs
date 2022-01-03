using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using SDE.Entidade;
using SDE.CamadaServico;
using SDE.Enumerador;
using SDE.CamadaControle;
using Db4objects.Db4o;
using System.Reflection;
using SDE.CamadaNuvem;

namespace SDE.Web
{
    public partial class CadastrarEmpresa : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
                return;

            GerenteConectividadeBancoDados bancos = AppFacade.get.conexaoBanco;
            IObjectContainer db0 = bancos.get(0, GerenteConectividadeBancoDados.TipoBanco.cadastros);
            
            lstbxEmpresas.Items.Clear();
            foreach (LoginEmpresa le in db0.Query<LoginEmpresa>())
	        {
                //text: 'empresa abc', value: idCorp
                lstbxEmpresas.Items.Add(
                    new ListItem(le.idCorp+". "+le.empresa, le.idCorp.ToString())
                    );
	        }
        }

        private bool valida()
        {
            txtFant.Text = txtFant.Text.ToUpper();
            txtLogin.Text = txtLogin.Text.ToUpper().Trim();
            txtRazao.Text = txtRazao.Text.ToUpper();

            //verificaco de campos
            string cnpj = txtCnpj.Text.Replace("/", "").Replace("-", "").Replace(".", "").Replace(",", "");
            if (txtLogin.Text.Length <= 0 ||
                txtFant.Text.Length <= 0 ||
                txtRazao.Text.Length <= 0)
            {
                lblResposta.Text = "Empresa não foi Cadastrada: CAMPOS VAZIOS";
                return false;
            }

            if (cnpj.Length < 14)
            {
                lblResposta.Text = "Empresa não foi Cadastrada: CNPJ INVÁLIDO";
                txtCnpj.Text = cnpj;
                return false;
            }

            foreach (ListItem li in lstbxEmpresas.Items)
            {
                if (li.Text == txtLogin.Text)
                {
                    lblResposta.Text = "Empresa não foi Cadastrada: CNPJ INVÁLIDO";
                    txtCnpj.Text = cnpj;
                    return false;
                }
            }

            return true;
        }

        protected void btn_Incluir_Click(object sender, EventArgs e)
        {
            if (!valida())
                return;
            //

            GerenteConectividadeBancoDados bancos = AppFacade.get.conexaoBanco;
            IObjectContainer db0 = bancos.get(0);

            int idCorp = int.Parse(lstbxEmpresas.SelectedValue);

            lock (db0.Ext().Lock())
            {
                //
                Max m = db0.Query<Max>()[0];
                IObjectContainer db = bancos.get(idCorp);
                //
                lock (db.Ext().Lock())
                {
                    try
                    {
                        //setBancoID(corp.id);
                        System.Web.HttpContext.Current.Items["idCorp"] = idCorp;

                        Cliente cEmpresa = new Cliente() { cpf_cnpj = txtCnpj.Text, nome = txtFant.Text, apelido_razsoc = txtRazao.Text, dtNasc = "01/01/1900", tipo = SDE.Enumerador.EPesTipo.Juridica };
                        cEmpresa.id = AppFacade.get.reaproveitamento.getIncremento(db, typeof(Cliente), 0);
                        db.Store(cEmpresa);

                        Empresa empresa = new Empresa() { idCliente = cEmpresa.id, idClienteAdmin = 1, usuario = txtLogin.Text };
                        empresa.id = AppFacade.get.reaproveitamento.getIncremento(db, typeof(Empresa), 0);
                        db.Store(empresa);

                        LoginEmpresa loginEmp = new LoginEmpresa() { id = ++m.idLoginEmpresa, empresa = txtLogin.Text, idCorp = idCorp, idEmp = empresa.id };
                        db0.Store(loginEmp);

                        lblResposta.Text = "Empresa Incluída com Sucesso";
                        txtCnpj.Text = "";
                        txtFant.Text = "";
                        txtLogin.Text = "";
                        txtRazao.Text = "";

                        db0.Store(m);
                        db0.Commit();
                        db.Commit();
                    }
                    catch (Exception ex)
                    {
                        db0.Rollback();
                        db.Rollback();
                        lblResposta.Text = "Empresa não foi Incluída: "
                            + ex.Message + "-" + ex.StackTrace;
                    }
                }//lock-db

            }//lock-db0

        }

        protected void btn_Criar_Click(object sender, EventArgs e)
        {
            //vamos usar os dados fornecidos pelo usuário, para
            //
            //criar um cliente consumidor
            //criar um "cliente" para representar a empresa
            //pegar uma nova idCorp (subsequente)
            //criar um login
            //salvar os dados

            if (!valida())
                return;

            GerenteConectividadeBancoDados bancos = AppFacade.get.conexaoBanco;
            IObjectContainer db0 = bancos.get(0, GerenteConectividadeBancoDados.TipoBanco.cadastros);

            lock (db0.Ext().Lock())
            {
                //
                Max m = db0.Query<Max>()[0];
                int proximoIdCorp = ++m.idCorporacao;
                IObjectContainer db = bancos.get(proximoIdCorp, GerenteConectividadeBancoDados.TipoBanco.cadastros);
                //
                lock (db.Ext().Lock())
                {
                    try
                    {
                        //setBancoID(corp.id);
                        System.Web.HttpContext.Current.Items["idCorp"] = proximoIdCorp;

                        //CorporacaoMax cmax = new CorporacaoMax() { idCliente = 2, idEmpresa=1 };
                        //db.Store(cmax);
                        AppFacade.get.reaproveitamento.setIncremento(db, typeof(Cliente), 2);
                        AppFacade.get.reaproveitamento.setIncremento(db, typeof(Empresa), 1);

                        //cria corp
                        Corporacao corp = new Corporacao() { id = proximoIdCorp, nome = "CORPORAÇÃO" };
                        db.Store(corp);
                        Empresa empresa = new Empresa() { id = 1, idCliente = 2, idClienteAdmin = 1, usuario = txtLogin.Text };
                        db.Store(empresa);

                        //cliente consumidor
                        Cliente cConsumidor = new Cliente() { id = 1, cpf_cnpj = "00000000000", nome = "CLIENTE CONSUMIDOR", apelido_razsoc = "CLIENTE CONSUMIDOR", dtNasc = "01/01/1900", tipo = SDE.Enumerador.EPesTipo.Fisica, loginUsuario = "ADMIN", loginSenha = "ADMIN" };
                        Cliente cEmpresa = new Cliente() { id = 2, cpf_cnpj = txtCnpj.Text, nome = txtFant.Text, apelido_razsoc = txtRazao.Text, dtNasc = "01/01/1900", tipo = SDE.Enumerador.EPesTipo.Juridica };
                        db.Store(cConsumidor);
                        db.Store(cEmpresa);

                        LoginEmpresa loginEmp = new LoginEmpresa() { id = ++m.idLoginEmpresa, empresa = txtLogin.Text,idCorp = proximoIdCorp, idEmp = 1 };
                        db0.Store(loginEmp);
                        
                        lblResposta.Text = "Empresa Cadastrada com Sucesso";
                        txtCnpj.Text = "";
                        txtFant.Text = "";
                        txtLogin.Text = "";
                        txtRazao.Text = "";

                        db0.Store(m);
                        db0.Commit();
                        db.Commit();

                        /*
                        //cria Plano de Contas padrão (RECEITAS / DESPESAS)
                        List<Finan_TipoLancamento> listaFinanTipoLancamento = new List<Finan_TipoLancamento>();
                        Finan_TipoLancamento receitas = new Finan_TipoLancamento()
                        {
                            idEmp = empresa.id,
                            idClienteFuncionarioLogado = cConsumidor.id,
                            nomeGrupoLancamento = "RECEITAS",
                            nomeTipoLancamento = "-"
                        };
                        listaFinanTipoLancamento.Add(receitas);
                        Finan_TipoLancamento despesas = new Finan_TipoLancamento()
                        {
                            idEmp = empresa.id,
                            idClienteFuncionarioLogado = cConsumidor.id,
                            nomeGrupoLancamento = "DESPESAS",
                            nomeTipoLancamento = "-"
                        };
                        listaFinanTipoLancamento.Add(despesas);
                        Finan_TipoLancamento contaSaldoInicial = new Finan_TipoLancamento()
                        {
                            idEmp = empresa.id,
                            idClienteFuncionarioLogado = cConsumidor.id,
                            nomeGrupoLancamento = "RECEITAS",
                            codigoGrupoLancamento = 1,
                            nomeTipoLancamento = "SALDO INICIAL"
                        };
                        NuvemModificacoes nuvem = new NuvemModificacoes();
                        nuvem.Finan_TipoLancamento_Novos(corp.id, listaFinanTipoLancamento);
                         * */
                    }
                    catch (Exception ex)
                    {
                        db0.Rollback();
                        db.Rollback();
                        lblResposta.Text = "Empresa não foi Cadastrada: "
                            +ex.Message+"-"+ex.StackTrace;
                    }
                }//lock-db

                //isso deve ficar fora do lock-db para não cairmos num dead-lock
                //o código a seguir, VAI gerar uma exceção que DEVE SER IGNORADA
                //pois se trata do sistema de notificações, e não teremos nenhum usuário logado nesse momento
                CamadaNuvem.NuvemModificacoes nMod = new SDE.CamadaNuvem.NuvemModificacoes();
                nMod.SdeConfig_Reseta(proximoIdCorp);
                nMod.Cad_Generico_Reseta(proximoIdCorp);
            }//lock-db0

        }
    }
}
