using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using SDE.Outros;
using SDE.CamadaControle;
using SDE.Entidade;
using SDE.Parametro;
using Db4objects.Db4o;

namespace SDE.CamadaServico
{
    public class SFinanceiro : SuperServico
    {

        /*
        #region Tipo Pagamento / tipo Pagamento Parcelas
        public Finan_TipoPagamento novoTipoPagamento(int idCorp, int idEmp, Finan_TipoPagamento ftpDados)
        {
            setBancoID(idCorp);
            Finan_TipoPagamento ftp = null;
            lock (db.Ext().Lock())
            {
                try
                {
                    ftpDados.idEmp = idEmp;
                    ftp = cFinanceiro.NovoTipoPagamento(ftpDados);
                    //ftp.__parcelas = new List<Finan_TipoPagamento_Parcela>();

                    foreach (Finan_TipoPagamento_Parcela ftpParcelaDados in ftpDados.__parcelas)
                    {
                        ftpParcelaDados.idTipoPagamento = ftp.id;
                        ftpParcelaDados.taxaJuro = 0;
                        ftpParcelaDados.taxaMultaDiaria = 0;
                        Finan_TipoPagamento_Parcela ftpParcela = cFinanceiro.NovoTipoPagamentoParcela(ftpParcelaDados);
                        ftp.__parcelas.Add(ftpParcela);
                    }
                    db.Commit();
                    return ftp;
                }
                catch (Exception ex)
                {
                    throw anotaErro(ex);
                }
            }
        }

        public Finan_TipoPagamento AtualizaTipoPagamento(int idCorp, int idEmp, Finan_TipoPagamento ftpDados)
        {           
            setBancoID(idCorp);
            Finan_TipoPagamento ftp = null;
            lock (db.Ext().Lock())
            {
                try
                {
                    ftp = cFinanceiro.LoadTipoPagamento(ftpDados.id);
                    if (ftp == null)
                        throw new Exception(string.Format("Forma de Pagamento código {} - não Encontrada", ftpDados.id));
                    


                    cFinanceiro.AtualizaTipoPagamento(ftpDados, ftp);
                    IList<Finan_TipoPagamento_Parcela> parcelas = cFinanceiro.PesquisaTipoPagamentoParcelas(ftp.id);

                    foreach (Finan_TipoPagamento_Parcela ftpParcela in parcelas)
                        foreach (Finan_TipoPagamento_Parcela ftpParcelaDados in ftpDados.__parcelas)
                            if (ftpParcela.id == ftpParcelaDados.id)
                            {
                                cFinanceiro.AtualizaTipoPagamentoParcela(ftpParcelaDados, ftpParcela);
                                break;
                            }
                    db.Commit();
                    return ftp;
                }
                catch (Exception ex)
                {
                    throw anotaErro(ex);
                }
            }
            
        }

        public IList<Finan_TipoPagamento> PesquisaTipoPagamento(int idCorp, int idEmp, ParamFiltroTipoPagamento pf)
        {
            setBancoID(idCorp);
            IList<Finan_TipoPagamento> ret = cFinanceiro.PesquisaTipoPagamento(idEmp);
            foreach (Finan_TipoPagamento ftp in ret)
                ftp.__parcelas = cFinanceiro.PesquisaTipoPagamentoParcelas(ftp.id);
            return ret;
        }
        #endregion
        */



    }
}
