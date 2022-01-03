using System;
using System.Collections.Generic;
using System.Text;
using Db4objects.Db4o;
using SDE.Entidade;
using SDE.Enumerador;
using SDE.CamadaControle;
using System.Reflection;
using System.Threading;

namespace SDE.CamadaImportacao
{
    public class ImportaSDE : SuperImporta
    {



        /*
        public void importaMax(Max max)
        {
            IList<Max> rs = db0.Query<Max>();
            if (rs.Count == 0)
            {
                
            }


        }
        public void importaCMax(int bancoID, CorporacaoMax cmax)
        {

        }
        */

        public void IniciaSDE1()
        {
            Cliente cConsumidor = new Cliente()
            {
                id = 1,
                nome = "CLIENTE CONSUMIDOR",
                tipo = SDE.Enumerador.EPesTipo.Fisica,
                apelido_razsoc = "CLIENTE CONSUMIDOR",
                dtNasc = "01/01/2001",
                dtRegistro = "01/01/2001",
                cpf_cnpj = "00000000000"
            };


            CorporacaoMax cm = new CorporacaoMax() { idCliente = 1 };




            db.Store(cConsumidor);
            db.Store(cm);
        }

        public int IniciaCorp()
        {

            Max m = db0.Query<Max>()[0];
            //cria corp
            Corporacao corp = new Corporacao();
            corp.id = ++m.idCorporacao;
            corp.nome = "CORPORAÇÃO";
            setBancoID(corp.id);
            db.Store(corp);
            db.Store(new CorporacaoMax());


            //importa cliente consumidor
            CCliente cCliente = new CCliente();
            Cliente cli = new Cliente();
            cli = cCliente.Novo(cli);



            db0.Store(m);

            db.Commit();
            db0.Commit();

            return corp.id;
        }

        public void importa(int idCorp, Cliente cliente)
        {
            setBancoID(idCorp);

            bool pessoaJaExistia = true;

            //se pessoa já existir, carrega, se não, cria
            Cliente cDB = cCliente.LoadCpfCnpj(cliente.cpf_cnpj);

            if (cDB==null)
            {
                cDB = cCliente.Novo(cliente);
                if (cliente.__contatos != null)
                    cCliente.Atualizar(cDB.id, cliente.__contatos);
                if (cliente.__enderecos != null)
                    cCliente.Atualizar(cDB.id, cliente.__enderecos);
                if (cliente.__familiares != null)
                    cCliente.Atualizar(cDB.id, cliente.__familiares);
            }
        }






        public void importa(int idCorp, int idEmp, Item item)
        {
            setBancoID(idCorp);

            //sempre cria o item
            //ele "tenta" aproveitar iep e aliquotas
            //ou cria novos
            Item itemDB = cItem.Novo(item);



            





            ThreadStart ts = new ThreadStart(delegate() {


                item.id = itemDB.id;
                ItemEmp ie = cItem.LoadItemEmp(item.id, idEmp);

                ItemEmpPreco iep = cItem.LoadPreco(itemDB.id, idEmp);

                item.__ie.__preco.id = iep.id;//iep tem id própria
                item.__ie.__preco.idItem = itemDB.id;
                cItem.Atualiza(
                    iep,
                    item.__ie.__preco
                    );

                ItemEmpAliquotas a = item.__ie.__aliquotas;
                a.idEmp = ie.idEmp;
                a.idItem = ie.idItem;
                /*
                if (item.__estoques != null && item.__estoques.Count > 0)
                {
                    foreach (ItemEmpEstoque iee in item.__estoques)
                    {
                        iee.idEmp = ie.idEmp;
                        iee.idItem = itemDB.id;
                        if (cMov.LoadEstoque(iee.codBarras) != null)//não insere duplicado
                            iee.codBarras = "GERAR";
                        cMov.Novo(iee, ie.idEmp);
                    }
                }
                else//se nulo ou zero
                {
                    */
                ItemEmpEstoque iee = new ItemEmpEstoque();
                iee.idEmp = ie.idEmp;
                iee.identificador = "U:U";
                iee.idItem = itemDB.id;
                iee.codBarras = "GERAR";
                cMov.Novo(iee, ie.idEmp);
                //}
                SDE.CamadaServico.SuperServico.QTD_THREADS_FECHADAS++;

            });
            SDE.CamadaServico.SuperServico.QTD_THREADS_ABERTAS++;
            new Thread(ts).Start();
        }


        public void reassinalaCodBarra(int idCorp, List<Item> listaCerta)
        {
            setBancoID(idCorp);

            Dictionary<string, int> dicNomeId = new Dictionary<string, int>();
            Dictionary<int, ItemEmpEstoque> dicIdEstoque = new Dictionary<int, ItemEmpEstoque>();
            
            IList<Item> itensErrados = db.Query<Item>();
            IList<ItemEmpEstoque> estoquesErrados = db.Query<ItemEmpEstoque>();

            foreach (Item it in itensErrados)
            {
                if (it.nome!=null)
                dicNomeId[it.nome] = it.id;
            }

            foreach (ItemEmpEstoque iee in estoquesErrados)
                dicIdEstoque[iee.idItem] = iee;





            foreach (Item itCerto in listaCerta)
            {
                string barras = "GERAR";
                if (itCerto.__estoques != null && itCerto.__estoques.Count > 0)
                    barras = itCerto.__estoques[0].codBarras;

                if (itCerto.nome != null && dicNomeId.ContainsKey(itCerto.nome))
                {
                    int idItem = dicNomeId[itCerto.nome];

                    if (dicIdEstoque.ContainsKey(idItem))
                    {

                        ItemEmpEstoque iee = dicIdEstoque[idItem];
                        System.Web.HttpContext.Current.Response.Write(itCerto.nome + "<br/>");

                        if (barras == null || barras.Length < 3 || barras == "GERAR")
                        {
                            barras = string.Concat("B", iee.id.ToString().PadLeft(6, '0'));

                        }

                        iee.codBarras = barras;
                        db.Store(iee);
                    }
                }
            }

        }




        public void importa(int idCorp, Empresa emp)
        {
            /*
            //tudo que eu espero dentro de __pessoa é CPF
            setBancoID(idCorp);

            //se pessoa já existir, carrega, se não, cria
            Cliente cDB = cCliente.LoadCpfCnpj(emp.__cliente.cpf_cnpj);
            
            Cliente cDB = null;
            if (pDB == null)
            {

                pDB = cPessoa.Novo(emp.__cliente.__pessoa);
                cDB = cCliente.Novo(pDB.id);


                //throw new Exception( string.Format("Não encontrado a Pessoa da Empresa'{0}', preencha o CPF manualmente", emp.usuario) );
            }
            else if (pDB.tipo != SDE.Enumerador.EPesTipo.Juridica)
            {
                throw new Exception( string.Format("Pessoa '{0}' não é do tipos JURIDICO para ser registrado na empresa '{0}'", pDB.cpf_cnpj, emp.usuario) );
            }


            //agora pDB está preenchido
            //registro a pessoa na empresa
            emp.idCliente = cDB.id;

            //Cliente Admin
            pDB = cPessoa.Load(emp.__clienteAdmin.__pessoa.cpf_cnpj);
            if (pDB == null)
            {
                throw new Exception( string.Format("Não encontrado a Pessoa da Empresa'{0}', preencha o CPF manualmente", emp.usuario) );
            }
            else if (pDB.tipo != SDE.Enumerador.EPesTipo.Fisica)
            {
                throw new Exception( string.Format("Pessoa '{0}' não é do tipos FISICO para ser registrado como Admin na empresa '{0}'", pDB.cpf_cnpj, emp.usuario) );
            }

            //esse é outro Cliente, pessoa administradora do sistema
            cDB = cCliente.LoadPorIdPessoa(pDB.id);
            if (cDB == null)
            {
                cDB = cCliente.Novo(pDB.id);
            }

            //registro o cliente como admin da empresa
            emp.idClienteAdmin = cDB.id;




            CEmp cEmp = new CEmp();

            if (cEmp.ListaLogin(emp.usuario).Count>0)
            {
                throw new Exception( string.Format("A empresa '{0}' já existe", emp.usuario) );
            }

            emp = cEmp.NovaEmpresa(emp);



            //cria login "emp-usr admin admin"

            Login l = new Login()
            {
                idCliente=emp.idClienteAdmin,
                idCorp=idCorp, idEmp=emp.id,
                empresa=emp.usuario,
                usuario="ADMIN", senha="ADMIN"
            };
            l.telas = new LoginTelas()
            {
                cadCli=true, cadItem=true, cadOutros=true,
                estBal1=true, estPDV=true,
                finCad=true
            };

            l = cEmp.NovoLogin(idCorp, l);
            */
        }

        public void importa(int idCorp, CorporacaoListas clistas)
        {
            setBancoID(idCorp);
            db.Store(clistas);
        }
    }
}
