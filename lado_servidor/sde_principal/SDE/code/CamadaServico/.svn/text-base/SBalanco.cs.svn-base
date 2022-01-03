using System;
using System.Collections.Generic;
using SDE.Parametro;
using SDE.Entidade;
using SDE.Enumerador;

namespace SDE.CamadaServico
{
    public class SBalanco : SuperServico
    {

        public void Finaliza(int idCorp, int idBalanco)
        {
            /*
            setBancoID(idCorp);
            lock (db.Ext().Lock())
            {
                try
                {
                    Balanco balanco = cBalanco.Load(idBalancoItem);
                    if (balanco.fechado)
                        throw new Exception("Balanço já está fechado");
                    IList<BalancoItem> listaBI = cBalanco.Lista(idBalancoItem);
                    if (listaBI.Count == 0)
                        throw new Exception("Balanço sem Item");
                    cBalanco.Finaliza(balanco);
                    cMov.Balanco(balanco, listaBI);
                    db.Commit();
                }
                catch (Exception ex)
                {
                    db.Rollback();
                    throw anotaErro(ex);
                }
            }
             * */
        }

        public IList<Balanco> Lista(int idCorp)
        {
            return null;
            /*
            setBancoID(idCorp);
            try
            {
                IList<Balanco> rs = cBalanco.Lista();
                return rs;
            }
            catch (Exception ex)
            {
                throw anotaErro(ex);
            }
             * */
        }

        public Balanco Load(int idCorp, int idBalanco)
        {
            return null;
            /*
            setBancoID(idCorp);
            try
            {
                Balanco b = cBalanco.Load(idBalancoItem);
                b.__itens = cBalanco.Lista(idBalancoItem);
                return b;
            }
            catch (Exception ex)
            {
                throw anotaErro(ex);
            }
             * */
        }

        public BalancoItem Registra(int idCorp, BalancoItem biDados)
        {
            return null;
            /*
            setBancoID(idCorp);
            lock (db.Ext().Lock())
            {
                try
                {
                    //registra caso o balanço não exista
                    if (biDados.idBalancoItem == 0)
                        biDados.idBalancoItem = cBalanco.Novo();
                    //caso ele exista, devemos retornar nulo se ele já tiver sido fechado
                    else if (cBalanco.Load(biDados.idBalancoItem).fechado)
                        return null;

                    BalancoItem bi = cBalanco.LoadBalancoItem(biDados.idBalancoItem, biDados.idIEE);
                    //registra caso o item na exista no balanço
                    if (bi == null)
                        biDados = cBalanco.Registra(biDados);
                    //senao, atualiza o item existente
                    else
                    {
                        bi.qtd += biDados.qtd;
                        biDados = cBalanco.Registra(bi);
                    }
                    db.Commit();
                    return biDados;
                }
                catch (Exception ex)
                {
                    throw anotaErro(ex);
                }
            }
             * */
        }

        public void Remove(int idCorp, int idBalancoItem)
        {
            /*
            setBancoID(idCorp);
            try
            {
                cBalanco.Remove(idBalancoItem);
                db.Commit();
            }
            catch (Exception ex)
            {
                db.Rollback();
                throw anotaErro(ex);
            }
            */
        }


    }
}
