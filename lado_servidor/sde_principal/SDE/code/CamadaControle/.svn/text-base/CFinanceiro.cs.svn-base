using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using SDE.Entidade;
using Db4objects.Db4o.Query;
using Db4objects.Db4o;


namespace SDE.CamadaControle
{
    public class CFinanceiro : SuperControle
    {

        #region Tipo PAgamento / Tipo PAgamento Parcelas

        public Finan_TipoPagamento LoadTipoPagamento(int idTipoPAgamento)
        {
            IList<Finan_TipoPagamento> rs = db.Query<Finan_TipoPagamento>(
                delegate(Finan_TipoPagamento xxx)
                {
                    return (xxx.id == idTipoPAgamento );
                }
            );
            return (rs.Count == 1)? rs[0]: null;
        }

        public IList<Finan_TipoPagamento> PesquisaTipoPagamento(int idEmp)
        {
            IList<Finan_TipoPagamento> rs = db.Query<Finan_TipoPagamento>(
                delegate(Finan_TipoPagamento xxx)
                {
                    return (xxx.idEmp == idEmp);
                }
            );
            return rs;
        }

        public Finan_TipoPagamento NovoTipoPagamento(Finan_TipoPagamento ftpDados)
        {
            //CorporacaoMax cMax = queryCMax();
            Finan_TipoPagamento ftp = new Finan_TipoPagamento();
            Utils.copiaCamposBasicos(ftpDados, ftp);
            //ftp.id = ++cMax.idFinTipoPagamento;
            ftp.id = AppFacade.get.reaproveitamento.getIncremento(db, typeof(Finan_TipoPagamento), 0);

            //db.Store(cMax);
            db.Store(ftp);
            return ftp;
        }

        public void AtualizaTipoPagamento(Finan_TipoPagamento ftpDados ,Finan_TipoPagamento ftp)
        {
            
            
            Utils.copiaCamposBasicos(ftpDados, ftp);
            db.Store(ftp);
        }

        //PARTE TIPO PAGAMENTO PARCELAS
        /*
        public Finan_TipoPagamento_Parcela NovoTipoPagamentoParcela(Finan_TipoPagamento_Parcela ftpParcelaDados)
        {
            CorporacaoMax cMax = queryCMax();
            Finan_TipoPagamento_Parcela ftpParcela = new Finan_TipoPagamento_Parcela();
            Utils.copiaCamposBasicos(ftpParcelaDados, ftpParcela);
            ftpParcela.id = ++cMax.idFinTipoPagamentoParcela;

            db.Store(cMax);
            db.Store(ftpParcela);
            return ftpParcela;
        }
        */

        public void AtualizaTipoPagamentoParcela(Finan_TipoPagamento_Parcela ftpParcelaDados,
            Finan_TipoPagamento_Parcela ftpParcela)
        {
            Utils.copiaCamposBasicos(ftpParcelaDados, ftpParcela);
            db.Store(ftpParcela);

        }


        public IList<Finan_TipoPagamento_Parcela> PesquisaTipoPagamentoParcelas(int idTipoPagamento)
        {
            IList<Finan_TipoPagamento_Parcela> rs = db.Query<Finan_TipoPagamento_Parcela>(
                 delegate(Finan_TipoPagamento_Parcela xxx)
                 {
                     return (xxx.idTipoPagamento == idTipoPagamento);
                 }
             );
            return rs;
        }

        #endregion
    }
}
