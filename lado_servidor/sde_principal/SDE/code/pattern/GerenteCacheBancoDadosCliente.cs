using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Web;
using SDE.Outros;

/// <summary>
/// Uma Instancia por corporacação dentro de AppFacade
/// </summary>
    public class GerenteCacheBancoDadosCliente
    {
        Dictionary<int, Dictionary<int, Atualizacao>> _cachesCorporacoes;
        Dictionary<int, int> _cachesCorporacoesUltimas;
        //Dictionary<int, int> ids_Sequenciais;





        private const int TEMPO_THREAD_LIMPAR = 30*1000;

        public GerenteCacheBancoDadosCliente(AppFacade appFacade)
        {
            this._cachesCorporacoes = new Dictionary<int, Dictionary<int, Atualizacao>>();
            this._cachesCorporacoesUltimas = new Dictionary<int, int>();
        }
        /*
        private void safeAccess_idCorp(int idCorp)
        {
            if (!cachesCorporacoes.ContainsKey(idCorp))
            {
                cachesCorporacoes.Add(idCorp, new Dictionary<int, Atualizacao>());
                cachesCorporacoesUltimas.Add(idCorp, 1);
            }
        }
        */
        private Dictionary<int, Atualizacao> getCacheByIdCorp(int idCorp)
        {
            if (!_cachesCorporacoes.ContainsKey(idCorp))
            {
                _cachesCorporacoes.Add(idCorp, new Dictionary<int, Atualizacao>());
                _cachesCorporacoesUltimas.Add(idCorp, 1);
            }
            return _cachesCorporacoes[idCorp];
        }

        private int getIdUltimaNotificacaoByIdCorp(int idCorp)
        {
            return _cachesCorporacoesUltimas[idCorp];
        }
        private void setIdUltimaNotificacaoByIdCorp(int idCorp, int idUltima)
        {
            _cachesCorporacoesUltimas[idCorp] = idUltima;
        }

        /*
        private void LimpaNotificacoesAntigas()
        {
        }
        */

        public List<Atualizacao> getAtualizacoes(int idCorp, int ultima)
        {
            Dictionary<int, Atualizacao> lsCachesTodasAtualizacoesDestaCorp = getCacheByIdCorp(idCorp);
            List<Atualizacao> ls = new List<Atualizacao>();

            //Console.Beep(1000, 300);
            if (ultima == 0)
            {
                //cachesCorporacoesUltimas.Keys.GetEnumerator().Current
                Dictionary<int, Atualizacao>.Enumerator en = lsCachesTodasAtualizacoesDestaCorp.GetEnumerator();
                en.MoveNext();
                ultima = en.Current.Key;
                //idUltimaNotificacao = cachesCorporacoesUltimas[idCorp];
            }
            else
                ultima++;

            //idUltimaNotificacao = 0;
            while (true)
            {
                //System.IO.File.AppendAllText("c:\\inetpub\\web\\lendo.txt", Environment.NewLine + DateTime.Now.ToString("HH:MM:ss") + " lendo item:" + idUltimaNotificacao + " | qtd total " + lsCachesTodasAtualizacoesDestaCorp.Count);

                if (lsCachesTodasAtualizacoesDestaCorp.Count == 0)
                    break;

                if (lsCachesTodasAtualizacoesDestaCorp.ContainsKey(ultima))
                {
                    /*
                    Dictionary<int, Atualizacao>.Enumerator enu = lsCachesTodasAtualizacoesDestaCorp.GetEnumerator();
                    enu.MoveNext();
                    throw new ExcecaoSDE("idUltimaNotificacao == " + idUltimaNotificacao + " | key real: " + enu.Current.Key);
                    */

                    Atualizacao at = lsCachesTodasAtualizacoesDestaCorp[ultima];
                    ls.Add(at);
                    ultima++;
                    //Console.Beep(2000, 300);
                }
                else
                {
                    //cachesCorporacoesUltimas[idCorp] = idUltimaNotificacao;
                    break;
                }
            }

            return ls;
        }

        public void LancaNotificacoes(int idCorp, ArrayList insercoes, ArrayList remocoes)
        {
            Dictionary<int, Atualizacao> lsCachesTodos = getCacheByIdCorp(idCorp);
            List<Atualizacao> lsAtualizacoesLancadasDuranteEstaRequisicao = new List<Atualizacao>();

            lock (lsCachesTodos)
            {
                int idUltimaNotificacao = getIdUltimaNotificacaoByIdCorp(idCorp);
                
                //System.IO.File.AppendAllText("c:\\inetpub\\web\\add.txt", Environment.NewLine + DateTime.Now.ToString("HH:MM:ss") + " inseri " + objetos.Length + " idUltimaNotificacao: " + idUltimaNotificacao + " totais de chaves: " + lsCachesTodos.Keys.Count);

                foreach (object obj in insercoes)
                {
                    Atualizacao at = new Atualizacao();
                    at.ehInsercao = true;
                    at.classe = obj.GetType().Name;
                    at.idObj = Convert.ToInt32(obj.GetType().GetField("id").GetValue(obj));
                    at.obj = obj;
                    at.idAtualizacao = ++idUltimaNotificacao;
                    //a primeira lista é APENAS dos itens que foram adicionados
                    //a primeira lista é DE TODOS os itens que foram já adicionados
                    lsAtualizacoesLancadasDuranteEstaRequisicao.Add(at);
                    lsCachesTodos[at.idAtualizacao] = at;
                    setIdUltimaNotificacaoByIdCorp(idCorp, idUltimaNotificacao);
                }
                foreach (object obj in remocoes)
                {
                    Atualizacao at = new Atualizacao();
                    at.ehInsercao = false;
                    at.classe = obj.GetType().Name;
                    at.idObj = Convert.ToInt32(obj.GetType().GetField("id").GetValue(obj));
                    at.obj = null;
                    at.idAtualizacao = ++idUltimaNotificacao;
                    //a primeira lista é APENAS dos itens que foram adicionados
                    //a primeira lista é DE TODOS os itens que foram já adicionados
                    lsAtualizacoesLancadasDuranteEstaRequisicao.Add(at);
                    lsCachesTodos[at.idAtualizacao] = at;
                    setIdUltimaNotificacaoByIdCorp(idCorp, idUltimaNotificacao);
                }
            }



            //ok, nesse momento os objetos já foram registrados
            //na thread abaixo, segue algorítmo para remover os mesmos



            ThreadStart ts = new ThreadStart(
                delegate()
                {
                    Thread.Sleep(TEMPO_THREAD_LIMPAR);

                    lock (lsCachesTodos)
                    {
                        foreach (Atualizacao at in lsAtualizacoesLancadasDuranteEstaRequisicao)
                        {
                            //beep deletando (o tempo já passou)
                            lsCachesTodos.Remove(at.idAtualizacao);
                        }
                    }
                }
                );
            new Thread(ts).Start();
        }

    }
