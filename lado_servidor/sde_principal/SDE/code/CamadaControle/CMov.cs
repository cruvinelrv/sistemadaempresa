using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using SDE.Entidade;
using SDE.Enumerador;
using Db4objects.Db4o.Query;
using Db4objects.Db4o;

namespace SDE.CamadaControle
{
    public partial class CMov: SuperControle
    {
       
        #region NFE

        public string gerarChave(string codUF, string dtEmissao,
            string cnpj, string modelo, string serie, string numDoc, string codNumerico
            )
        {
            int digVerificador = gerarDV(codUF, dtEmissao, cnpj, modelo, serie, numDoc, codNumerico);

            cnpj = Utils.apenasNumeros(cnpj);

            string valor = codUF + dtEmissao + cnpj + modelo + serie + numDoc + codNumerico + digVerificador;

            return valor;
        }

        public MovNFE LoadUltimaNFE()
        {
            IQuery qry = db.Query();
            qry.Constrain(typeof(MovNFE));
            qry.Descend("id").OrderDescending();
            IList todas = qry.Execute();
            if (todas.Count == 0)
                return null;
            return (MovNFE)todas[0];
        }

        private int gerarDV(string codUF, string dtEmissao, string cnpj, string modelo, string serie, string numDoc, string codNumerico)
        {
            string valor = codUF + dtEmissao + cnpj + modelo + serie + numDoc + codNumerico;

            int cont = 4;
            int valorTotal = 0;
            foreach (char c in valor)
            {
                int dig = int.Parse(c.ToString());
                valorTotal += dig * cont;
                if (cont != 2)
                    cont--;
                else
                    cont = 9;

            }
            int resto = valorTotal % 11;
            return 11 - resto;
        }

        public MovNFE LoadNFE(int idMov)
        {
            IList<MovNFE> rs = db.Query<MovNFE>(
                delegate(MovNFE nfe)
                {
                    return (nfe.idMov == idMov);
                }
            );
            return (rs.Count == 1) ? rs[0] : null;
        }

        public MovNFE LoadNFENumeroNota(int numeroNota)
        {
            IList<MovNFE> rs = db.Query<MovNFE>(
                delegate(MovNFE nfe)
                {
                    return (nfe.numeroNota == numeroNota);
                }
            );
            return (rs.Count == 1) ? rs[0] : null;
        }

        public MovNfeVeiculo LoadNFEVeiculo(int idMovNFE)
        {
            IList<MovNfeVeiculo> rs = db.Query<MovNfeVeiculo>(
                delegate(MovNfeVeiculo nfeVeiculo)
                {
                    return (nfeVeiculo.idMovNFE == idMovNFE);
                }
            );
            return (rs.Count == 1) ? rs[0] : null;
        }

        public void SalvaNFEVeiculo(MovNfeVeiculo nfeVeiculo)
        {
            db.Store(nfeVeiculo);
        }

        public MovNFE NovaNFE(Mov mov, MovNFE nfe)
        {
            //CorporacaoMax cMax = queryCMax();
            //nfe.id = ++cMax.idMovNfe;
            nfe.id = AppFacade.get.reaproveitamento.getIncremento(db, typeof(MovNFE), 0);
            mov.isNfePreenchida = true;
            //db.Store(cMax);
            db.Store(mov);
            db.Store(nfe);
            return nfe;
        }
        public void AtualizaNFE(Mov mov, MovNFE nfe)
        {           
            db.Store(nfe);
        }

        public void SalvaNFE_Enviada(int idMov)
        {
            Mov mov = Load(idMov);
            mov.isNfeEnviada = true;
            db.Store(mov);
        }

        #endregion

        #region Load

        public List<Mov> LoadMovEntrada_NumeroNota(int idCliente, int numNota, EMovResumo resumo)
        {
            IList<Mov> rs = db.Query<Mov>(
                delegate(Mov mov)
                {
                       return (mov.idCliente == idCliente && mov.numeroNF == numNota && mov.resumo == resumo);
                }
            );
        if (rs.Count > 0)
            return new List<Mov>(rs);
        else
            return null;
        }

        public List<Mov> LoadMovNumeroNota(int idCliente, int numNota, EMovResumo resumo)
        {
            IList<Mov> rs = db.Query<Mov>(
                delegate(Mov m)
                {
                    //entrada
                    if (resumo == EMovResumo.entrada)
                        return (m.idCliente == idCliente && m.numeroNF == numNota && m.resumo == resumo);
                    //saida
                    else
                        return (m.numeroNF == numNota && m.resumo == resumo);
                }
            );
            if (rs.Count > 0)
                return new List<Mov>(rs);
            else
                return null;
        }

        public Mov Load(int idMov)
        {
            IList<Mov> rs = db.Query<Mov>(
                  delegate(Mov mov)
                  {
                      return (mov.id == idMov);
                  }
            );
            return (rs.Count == 1) ? rs[0] : null;
        }

        public List<MovItem> LoadMovItens(int idMov)
        {
            IList<MovItem> rs = db.Query<MovItem>(
                  delegate(MovItem mi)
                  {
                      return (mi.idMov == idMov);
                  }
            );
            return new List<MovItem>(rs);
        }

        public List<MovValor> LoadMovValores(int idMov)
        {
            IList<MovValor> rs = db.Query<MovValor>(
                delegate(MovValor mv)
                {
                    return (mv.idMov == idMov);
                }
            );
            return new List<MovValor>(rs);
        }

        public List<MovItemEstoque> ListMovItemEstoque(int idMovItem)
        {
            IList<MovItemEstoque> rs = db.Query<MovItemEstoque>(
                delegate(MovItemEstoque mie)
                {
                    return (mie.idMovItem == idMovItem);
                }
            );
            return new List<MovItemEstoque>(rs);
        }

        #endregion

        #region Pesquisa

        public List<Mov> Pesquisa(long dtInicial, long dtFinal, string tipos)
        {
            Predicate<Mov> pesq;
            if (tipos == null)
                pesq = delegate(Mov mov)
                {
                    return (mov.dtMovTicks >= dtInicial &&
                        mov.dtMovTicks <= dtFinal);
                };
            else
                pesq = delegate(Mov mov)
                {
                    return (mov.dtMovTicks >= dtInicial &&
                        mov.dtMovTicks <= dtFinal &&
                        tipos.Contains(mov.tipo.ToString()));
                };

            IList<Mov> rs = db.Query<Mov>( pesq );
            return new List<Mov>(rs);
        }

        #endregion

        #region Movimentações

        //Salva Venda.....
        public int Venda(Mov mov)
        {
            //CorporacaoMax cMax = queryCMax();
            //mov.id = ++cMax.idMov;
            mov.id = AppFacade.get.reaproveitamento.getIncremento(db, typeof(Mov), 0);
            mov.dthrMovEmissao = Utils.getAgoraString();
            mov.dtMovTicks = DateTime.Parse(mov.dthrMovEmissao).Ticks;
            foreach (MovItem mi in mov.__mItens)
            {
                //mi.id = ++cMax.idMovItem;
                mi.id = AppFacade.get.reaproveitamento.getIncremento(db, typeof(MovItem), 0);
                mi.idMov = mov.id;
                foreach (MovItemEstoque mie in mi.__mIEstoques)
                {
                    ItemEmpEstoque iee = LoadEstoque(mie.idIEE);
                    iee.qtd -= mie.qtd;
                    mie.idMovItem = mi.id;
                    Utils.filtraCampos(mie);
                    db.Store(iee);
                    db.Store(mie);
                }
                Utils.filtraCampos(mi);
                db.Store(mi);
            }
            if(mov.__mValores !=  null)
                foreach (MovValor mv in mov.__mValores)
                {
                    //mv.id = ++cMax.idMovValor;
                    mv.id = AppFacade.get.reaproveitamento.getIncremento(db, typeof(MovValor), 0);
                    mv.idMov = mov.id;
                    db.Store(mv);
                }
            Utils.filtraCampos(mov);
            db.Store(mov);
            //db.Store(cMax);
            return mov.id;
        }

        //Salva movimentação de emissão de NF de Serviços
        public int Servico(Mov mov)
        {
            //CorporacaoMax cMax = queryCMax();
            //mov.id = ++cMax.idMov;
            mov.id = AppFacade.get.reaproveitamento.getIncremento(db, typeof(Mov), 0);
            mov.dthrMovEmissao = Utils.getAgoraString();
            mov.dtMovTicks = DateTime.Parse(mov.dthrMovEmissao).Ticks;
            foreach (MovItem mi in mov.__mItens)
            {
                //mi.id = ++cMax.idMovItem;
                mi.id = AppFacade.get.reaproveitamento.getIncremento(db, typeof(MovItem), 0);
                mi.idMov = mov.id;
                foreach (MovItemEstoque mie in mi.__mIEstoques)
                {
                    mie.idMovItem = mi.id;
                    Utils.filtraCampos(mie);
                    db.Store(mie);
                }
                Utils.filtraCampos(mi);
                db.Store(mi);
            }
            if (mov.__mValores != null)
                foreach (MovValor mv in mov.__mValores)
                {
                    //mv.id = ++cMax.idMovValor;
                    mv.id = AppFacade.get.reaproveitamento.getIncremento(db, typeof(MovValor), 0);
                    mv.idMov = mov.id;
                    db.Store(mv);
                }
            Utils.filtraCampos(mov);
            db.Store(mov);
            //db.Store(cMax);
            return mov.id;
        }

        //Salva Orçamento...
        public int Orcamento(Mov mov)
        {
            //CorporacaoMax cMax = queryCMax();
            //mov.id = ++cMax.idMov;
            mov.id = AppFacade.get.reaproveitamento.getIncremento(db, typeof(Mov), 0);
            mov.dthrMovEmissao = Utils.getAgoraString();
            mov.dtMovTicks = DateTime.Parse(mov.dthrMovEmissao).Ticks;
            foreach (MovItem mi in mov.__mItens)
            {
                //mi.id = ++cMax.idMovItem;
                mi.id = AppFacade.get.reaproveitamento.getIncremento(db, typeof(MovItem), 0);
                mi.idMov = mov.id;
                foreach (MovItemEstoque mie in mi.__mIEstoques)
                {
                    //codigo orçamento com reserva
                    if(mov.tipo == EMovTipo.outros_reserva){
                        ItemEmpEstoque iee = LoadEstoque(mie.idIEE);
                        iee.qtdReserva += mie.qtd;
                        db.Store(iee);
                    }
                    mie.idMovItem = mi.id;
                    Utils.filtraCampos(mie);
                    db.Store(mie);
                }
                Utils.filtraCampos(mi);
                db.Store(mi);
            }
            if(mov.__mValores != null)
                foreach (MovValor mv in mov.__mValores)
                {
                    //mv.id = ++cMax.idMovValor;
                    mv.id = AppFacade.get.reaproveitamento.getIncremento(db, typeof(MovValor), 0);
                    mv.idMov = mov.id;
                    db.Store(mv);
                }
            Utils.filtraCampos(mov);
            db.Store(mov);
            //db.Store(cMax);
            return mov.id;
        }

        /*
    //Salva movimentação de Balanço
    public void Balanco(Balanco balanco, IList<BalancoItem> bItens)
    {
        //CorporacaoMax cMax = queryCMax();
        Mov mov = new Mov();
        //mov.id = ++cMax.idMov;
        mov.id = AppFacade.get.reaproveitamento.getIncremento(db, typeof(Mov), 0);
        mov.dthrMovEmissao = Utils.getAgoraString();
        mov.dtMovTicks = Utils.DateTimeParseBR(mov.dthrMovEmissao).Ticks;
        mov.impressao = EMovImpressao.sem_impressao;
        mov.tipo = EMovTipo.ambos_balan;
        mov.resumo = EMovResumo.ambos;
        mov.idCliente = 1;
        mov.idClienteFuncionarioLogado = 1;
        foreach (BalancoItem bi in bItens)
        {
            ItemEmpEstoque iee = LoadEstoque(bi.idIEE);

            MovItem mi = new MovItem();
            //mi.id = ++cMax.idMovItem;
            mi.id = AppFacade.get.reaproveitamento.getIncremento(db, typeof(MovItem), 0);
            mi.idMov = mov.id;
            mi.idItem = bi.idItem;
            mi.qtd = bi.qtd;
            mi.saldoAtual = bi.qtd - iee.qtd;
            db.Store(mi);

            MovItemEstoque mie = new MovItemEstoque();
            mie.idMovItem = mi.id;
            mie.idIEE = bi.idIEE;
            mie.qtd = bi.qtd;
            db.Store(mie);
 
            iee.qtd = bi.qtd;
            db.Store(iee);
        }
        //db.Store(cMax);
        db.Store(mov);
    }
        */

        public int Entrada(Mov mov)
        {
            //CorporacaoMax cMax = queryCMax();
            //mov.id = ++cMax.idMov;
            mov.id = AppFacade.get.reaproveitamento.getIncremento(db, typeof(Mov), 0);
            mov.dthrMovEmissao = Utils.getAgoraString();
            mov.dtMovTicks = DateTime.Parse(mov.dthrMovEmissao).Ticks;

            foreach (MovItem mi in mov.__mItens)
            {
                //mi.id = ++cMax.idMovItem;
                //mi.id = AppFacade.get.reaproveitamento.getIncremento(db, typeof(MovItem), 0);
                mi.idMov = mov.id;
                foreach (MovItemEstoque mie in mi.__mIEstoques)
                {
                    MovItem miNovo = new MovItem();
                    Utils.copiaCamposBasicos(mi, miNovo);
                    miNovo.id = AppFacade.get.reaproveitamento.getIncremento(db, typeof(MovItem), 0);
                    
                    mie.id = AppFacade.get.reaproveitamento.getIncremento(db, typeof(MovItemEstoque), 0);
                    ItemEmpEstoque iee = LoadEstoque(mov.idEmp, mi.idItem, mie.identificador);
                    if (iee.custo == 0)
                        iee.custo = mi.vlrUnitCusto;
                    iee.qtd += mie.qtd;
                    iee.lote = mie.lote;
                    iee.dtFab = mie.dtFab;
                    iee.dtVal = mie.dtVal;
                    
                    mie.idIEE = iee.id;
                    mie.idMovItem = mi.id;
                    Utils.filtraCampos(mie);

                    miNovo.idIEE = iee.id;
                    miNovo.qtd = mie.qtd;
                    miNovo.estoque_identificador = mie.identificador;
                    Utils.filtraCampos(miNovo);

                    db.Store(mie);
                    db.Store(iee);
                    db.Store(miNovo);
                }
                //Utils.filtraCampos(mi);
                //db.Store(mi);
            }
            if (mov.__mValores != null)
            {
                foreach (MovValor mv in mov.__mValores)
                {
                    //mv.id = ++cMax.idMovValor;
                    mv.id = AppFacade.get.reaproveitamento.getIncremento(db, typeof(MovValor), 0);
                    mv.idMov = mov.id;
                    db.Store(mv);
                }
            }
            Utils.filtraCampos(mov);
            db.Store(mov);
            //db.Store(cMax);
            return mov.id;

        }
        
        #endregion

        #region Cancela movimentação

        public void CancelaSaida(Mov movDados, Mov mov)
        {
            //CorporacaoMax max = queryCMax();
            //mov.id = ++max.idMov;
            mov.id = AppFacade.get.reaproveitamento.getIncremento(db, typeof(Mov), 0);
            mov.idMovCancelada = movDados.id;
            movDados.idMovCanceladora = mov.id;
            foreach (MovItem mi in mov.__mItens)
            {
                //mi.id = ++max.idMovItem;
                mov.id = AppFacade.get.reaproveitamento.getIncremento(db, typeof(MovItem), 0);
                mi.idMov = mov.id;
                mi.qtd =  - mi.qtd;
                mi.__mIEstoques = new List<MovItemEstoque>();
                mi.__mIEstoques = ListMovItemEstoque(mi.id);
                foreach (MovItemEstoque mie in mi.__mIEstoques)
                {
                    mie.idMovItem = mi.id;
                    mie.qtd =  - mie.qtd;
                    //alteracao de estoque
                    ItemEmpEstoque iee = LoadEstoque(mie.idIEE);
                    iee.qtd -= mie.qtd;
                    Utils.filtraCampos(mie);
                    db.Store(mie);
                    db.Store(iee);
                }
                Utils.filtraCampos(mi);
                db.Store(mi);
            }
            Utils.filtraCampos(mov);
            Utils.filtraCampos(movDados);
            //db.Store(max);
            db.Store(mov);
            db.Store(movDados);
        }

        public void CancelaEntrada(Mov movDados, Mov mov)
        {
            //CorporacaoMax max = queryCMax();
            //mov.id = ++max.idMov;
            mov.id = AppFacade.get.reaproveitamento.getIncremento(db, typeof(Mov), 0);
            mov.idMovCancelada = movDados.id;
            movDados.idMovCanceladora = mov.id;
            foreach (MovItem mi in mov.__mItens)
            {
                //mi.id = ++max.idMovItem;
                mi.id = AppFacade.get.reaproveitamento.getIncremento(db, typeof(MovItem), 0);
                mi.idMov = mov.id;
                mi.qtd = -mi.qtd;
                foreach (MovItemEstoque mie in mi.__mIEstoques)
                {
                    mie.idMovItem = mi.id;
                    mie.qtd = -mie.qtd;
                    //alteracao de estoque
                    ItemEmpEstoque iee = LoadEstoque(mie.idIEE);
                    iee.qtd += mie.qtd;
                    Utils.filtraCampos(mie);
                    db.Store(mie);
                    db.Store(iee);
                }
                Utils.filtraCampos(mi);
                db.Store(mi);
            }
            Utils.filtraCampos(mov);
            Utils.filtraCampos(movDados);
            //db.Store(max);
            db.Store(mov);
            db.Store(movDados);
        }

        public void CancelaReserva(Mov movDados, Mov mov)
        {
            //CorporacaoMax max = queryCMax();
            //mov.id = ++max.idMov;
            mov.id = AppFacade.get.reaproveitamento.getIncremento(db, typeof(Mov), 0);
            mov.idMovCancelada = movDados.id;
            mov.isReservaDevolvida = true;
            movDados.idMovCanceladora = mov.id;
            foreach (MovItem mi in mov.__mItens)
            {
                //mi.id = ++max.idMovItem;
                mi.id = AppFacade.get.reaproveitamento.getIncremento(db, typeof(MovItem), 0);
                mi.idMov = mov.id;
                mi.qtd = -mi.qtd;
                foreach (MovItemEstoque mie in mi.__mIEstoques)
                {
                    mie.idMovItem = mi.id;
                    mie.qtd = -mie.qtd;
                    //alteracao de estoque
                    ItemEmpEstoque iee = LoadEstoque(mie.idIEE);
                    iee.qtdReserva += mie.qtd;
                    Utils.filtraCampos(mie);
                    db.Store(mie);
                    db.Store(iee);
                }
                Utils.filtraCampos(mi);
                db.Store(mi);
            }
            Utils.filtraCampos(mov);
            Utils.filtraCampos(movDados);
            //db.Store(max);
            db.Store(mov);
            db.Store(movDados);
        }

        public void CancelaBalanco(Mov movDados, Mov mov)
        {
            //CorporacaoMax max = queryCMax();
            //mov.id = ++max.idMov;
            mov.id = AppFacade.get.reaproveitamento.getIncremento(db, typeof(Mov), 0);
            mov.idMovCancelada = movDados.id;
            mov.isReservaDevolvida = true;
            movDados.idMovCanceladora = mov.id;
            foreach (MovItem mi in mov.__mItens)
            {
                //mi.id = ++max.idMovItem;
                mi.id = AppFacade.get.reaproveitamento.getIncremento(db, typeof(MovItem), 0);
                mi.idMov = mov.id;
                mi.saldoAtual = - mi.saldoAtual;
                mi.qtd = - mi.saldoAtual;
                foreach (MovItemEstoque mie in mi.__mIEstoques)
                {
                    mie.idMovItem = mi.id;
                    mie.qtd = mi.saldoAtual;
                    //alteracao de estoque
                    ItemEmpEstoque iee = LoadEstoque(mie.idIEE);
                    iee.qtd += mie.qtd;
                    Utils.filtraCampos(mie);
                    db.Store(mie);
                    db.Store(iee);
                }
                Utils.filtraCampos(mi);
                db.Store(mi);
            }
            Utils.filtraCampos(mov);
            Utils.filtraCampos(movDados);
            //db.Store(max);
            db.Store(mov);
            db.Store(movDados);
        }

        #endregion

    }

}
