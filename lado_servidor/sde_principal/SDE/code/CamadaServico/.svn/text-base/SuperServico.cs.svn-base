using SDE.CamadaControle;
using System.Threading;
using System.Text;
using System;
using Db4objects.Db4o;

namespace SDE.CamadaServico
{
    public class SuperServico
    {
        public SuperServico()
        {
            banco = AppFacade.get.conexaoBanco;
            db0 = banco.get(0, GerenteConectividadeBancoDados.TipoBanco.cadastros);
        }
        protected void setBancoID(int idCorp)
        {
            this.idCorp = idCorp;
            System.Web.HttpContext.Current.Items["idCorp"] = idCorp;
            db = banco.get(idCorp, GerenteConectividadeBancoDados.TipoBanco.cadastros);
        }
        protected GerenteConectividadeBancoDados banco = null;
        protected IObjectContainer db = null;
        protected IObjectContainer db0 = null;

        int _idCorp;
        protected int idCorp
        {
            get { return _idCorp; }
            private set { _idCorp = value; }
        }
        protected Exception anotaErro(Exception ex)
        {
            AppFacade.get.conexaoBanco.geraLog(idCorp, ex);
            return new Exception(string.Format("[erro anotado] {0}", ex.Message + "\n "+ ex.StackTrace), ex);
        }




        //private CAtualizacao _cAtualizacao;
        //protected CAtualizacao cAtualizacao { get { if (_cAtualizacao == null)_cAtualizacao = new CAtualizacao(idCorp); return _cAtualizacao; } }

        
        private CFinanceiro _cFinanceiro;
        protected CFinanceiro cFinanceiro { get { if (_cFinanceiro == null)_cFinanceiro = new CFinanceiro(); return _cFinanceiro; } }
        
        private CTecnico _cTecnico;
        protected CTecnico cTecnico { get { if (_cTecnico == null)_cTecnico = new CTecnico(); return _cTecnico; } }
        
        private CItem _cItem;
        protected CItem cItem { get { if (_cItem == null)_cItem = new CItem(); return _cItem; } }

        private CEmp _cEmp;
        protected CEmp cEmp
        {
            get { if (_cEmp == null)_cEmp = new CEmp(); return _cEmp; }
            set { _cEmp = null; }
        }

        private CCliente _cCliente;
        protected CCliente cCliente { get { if (_cCliente == null)_cCliente = new CCliente(); return _cCliente; } }

        
        private CCorp _cCorp;
        protected CCorp cCorp { get { if (_cCorp == null)_cCorp = new CCorp(); return _cCorp; } }

        private CBalanco _cBalanco;
        protected CBalanco cBalanco { get { if (_cBalanco == null)_cBalanco = new CBalanco(); return _cBalanco; } }

        private CMov _cMov;
        protected CMov cMov { get { if (_cMov == null)_cMov = new CMov(); return _cMov; } }
        


    }
}
