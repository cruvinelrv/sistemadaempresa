using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using SDE.Entidade;
using SDE.Enumerador;

using Db4objects.Db4o.Query;
using Db4objects.Db4o;

namespace SDE.CamadaControle
{
    public class CTecnico : SuperControle
    {









        public void ClienteRemoverTudo()
        {/*
            Empresa emp = null;
            foreach (Empresa yyy in db.Query<Empresa>())
            {
                emp = yyy;
                emp.idClienteAdmin = 1;
                db.Store(emp);
            }

            foreach (Cliente cli in db.Query<Cliente>())
            {
                if (cli.id == 1 ||                  //consumidor
                    cli.id == emp.idCliente ||      //a propria empresa
                    cli.id == emp.idClienteAdmin)   //funcionario admin
                    continue;

                db.Delete(cli);
            }

            Console.Beep();
            foreach (Object xxx in db.Query<ClienteContato>())
                db.Delete(xxx);
            foreach (Object xxx in db.Query<ClienteEndereco>())
                db.Delete(xxx);
            foreach (Object xxx in db.Query<ClienteFamiliar>())
                db.Delete(xxx);

            Console.Beep();

            CorporacaoMax cmax = queryCMax();
            cmax.idCliente = 0;
            cmax.idClienteBancario = 0;
            cmax.idClienteContato = 0;
            cmax.idClienteEndereco = 0;
            cmax.idClienteFamiliar = 0;
            cmax.idClientePropriedade = 0;

            //select *
            IQuery query = db.Query();
            //from +Cliente+
            query.Constrain(typeof(Cliente));
            //order by +id+ asc
            query.Descend("id").OrderAscending();
            IList clientesRestantes = query.Execute();

            foreach (Cliente c in clientesRestantes)
            {
                ++cmax.idCliente;
                c.id = cmax.idCliente;
                db.Store(c);
            }

            db.Store(cmax);
            db.Commit();
            Console.Beep(1000,300);*/
        }

        public void ItemRemoverTudo()
        {
            /*
            foreach (Item xxx in db.Query<Item>())
                db.Delete(xxx);
            foreach (ItemEmp xxx in db.Query<ItemEmp>())
                db.Delete(xxx);
            foreach (ItemEmpEstoque xxx in db.Query<ItemEmpEstoque>())
                db.Delete(xxx);
            foreach (ItemEmpPreco xxx in db.Query<ItemEmpPreco>())
                db.Delete(xxx);
            foreach (Mov xxx in db.Query<Mov>())
                db.Delete(xxx);
            foreach (MovItem xxx in db.Query<MovItem>())
                db.Delete(xxx);
            foreach (MovItemEstoque xxx in db.Query<MovItemEstoque>())
                db.Delete(xxx);
            foreach (MovValor xxx in db.Query<MovValor>())
                db.Delete(xxx);
            foreach (Balanco xxx in db.Query<Balanco>())
                db.Delete(xxx);
            foreach (BalancoItem xxx in db.Query<BalancoItem>())
                db.Delete(xxx);

            CorporacaoMax cmax = queryCMax();
            cmax.idBalancoItem = 0;
            cmax.idBalancoItem = 0;
            cmax.idIEE = 0;
            cmax.idIEP = 0;
            cmax.idItem = 0;
            cmax.idMIE = 0;
            cmax.idMov = 0;
            cmax.idMovItem = 0;
            cmax.idMovValor = 0;

            db.Store(cmax);
             * */
        }

        public void ItemRedefineCodUnicoComIdItem(bool absolutamenteTodos)
        {
            /*
            foreach (Item xxx in db.Query<Item>())
            {
                //se TODOS ou PRECISAR
                if (absolutamenteTodos || (xxx.rfUnica == null || xxx.rfUnica.StartsWith("#")))
                {
                    xxx.rfUnica = string.Concat("#", xxx.id);
                    db.Store(xxx);
                }
            }*/
        }
        public void EstoqueCorrecaoGradeBarras()
        {/*
            foreach (ItemEmpEstoque xxx in db.Query<ItemEmpEstoque>())
            {
                if ((xxx.identificador == null || xxx.identificador.Length < 3))
                {
                    xxx.identificador = "U:U";
                    db.Store(xxx);
                }
                if ((xxx.codBarras == null || xxx.codBarras.Length < 3))
                {
                    xxx.codBarras = _GeraCodigoBarra(xxx.id);
                    db.Store(xxx);
                }
            }*/
        }
        private string _GeraCodigoBarra(int idIEE)
        {
            return string.Concat("B", idIEE.ToString().PadLeft(6, '0'));
        }




        public void ClientesResetaTodos()
        {/*
            IList<Cliente> clientes = db.Query<Cliente>();
            Empresa emp = db.Query<Empresa>()[0];
            foreach (Cliente cli in clientes)
            {
                if (cli.id == 1 ||                  //consumidor
                    cli.id == emp.idCliente ||      //a propria empresa
                    cli.id == emp.idClienteAdmin)   //funcionario admin
                    continue;
                db.Delete(cli);
            }

            clientes = db.Query<Cliente>(delegate(Cliente c) { return (c.id > 1); });
            int idCliente = 1;
            foreach (Cliente cliente in clientes)
            {
                cliente.id = ++idCliente;
                db.Store(cliente);
                if (cliente.tipo == EPesTipo.Fisica)
                    emp.idClienteAdmin = cliente.id;
                else
                    emp.idCliente = cliente.id;
            }
            db.Store(emp);

            CorporacaoMax cmax = queryCMax();
            cmax.idCliente = idCliente;
            db.Store(cmax);
            */
        }
        /*
        public void ResetaCorpListas()
        {
            foreach (Categoria xxx in db.Query<Categoria>())
            {
                if (xxx.filhos != null)
                    db.Delete(xxx.filhos);//deleta a 'instância da lista'
                db.Delete(xxx);
            }
            foreach (Marca xxx in db.Query<Marca>())
            {
                if (xxx.filhos != null)
                    db.Delete(xxx.filhos);//deleta a 'instância da lista'
                db.Delete(xxx);
            }
            foreach (Grade xxx in db.Query<Grade>())
            {
                if (xxx.filhos != null)
                    db.Delete(xxx.filhos);//deleta a 'instância da lista'
                db.Delete(xxx);
            }
            foreach (CorporacaoListas xxx in db.Query<CorporacaoListas>())
            {
                db.Delete(xxx.categorias);
                db.Delete(xxx.marcas);
                db.Delete(xxx.grades);
                db.Delete(xxx);
            }

            CorporacaoListas cLis = new CorporacaoListas()
            {
                categorias = new List<Categoria>()
                {
                    new Categoria() {
                        nome="GERAL",
                        filhos = new List<Categoria>()
                        {
                            new Categoria() {
                                nome="GERAL",
                                filhos = new List<Categoria>()
                                {
                                    new Categoria() { nome="GERAL" }
                                }
                            }
                        }
                    }
                },
                marcas = new List<Marca>()
                {
                    new Marca() {
                        nome="GENERICA",
                        filhos = new List<Marca>()
                        {
                            new Marca() { nome="GENERICO" }
                        }
                    }
                },
                grades = new List<Grade>()
                {
                    new Grade() {
                        nome="UNICA", rf="U",
                        filhos = new List<Grade>()
                        {
                            new Grade() { nome="UNICA", rf="U" }
                        }
                    }
                }
            };
            db.Store(cLis);
        }
        */
        public void ResetaEmpListas(int idEmp)
        {
            /*
            foreach (Finan_CentroCusto xxx in db.Query<Finan_CentroCusto>(delegate(Finan_CentroCusto xx) { return (xx.idEmp == idEmp); }))
            {
                db.Delete(xxx);
            }
            foreach (Finan_PlanoConta xxx in db.Query<Finan_PlanoConta>(delegate(Finan_PlanoConta xx) { return (xx.idEmp == idEmp); }))
            {
                db.Delete(xxx);
            }
            foreach (Finan_Conta xxx in db.Query<Finan_Conta>(delegate(Finan_Conta xx) { return (xx.idEmp == idEmp); }))
            {
                db.Delete(xxx);
            }
            foreach (Finan_Portador xxx in db.Query<Finan_Portador>(delegate(Finan_Portador xx) { return (xx.idEmp == idEmp); }))
            {
                db.Delete(xxx);
            }
            foreach (Finan_TipoDocumento xxx in db.Query<Finan_TipoDocumento>(delegate(Finan_TipoDocumento xx) { return (xx.idEmp == idEmp); }))
            {
                db.Delete(xxx);
            }
            /*
            foreach (EmpresaListas xxx in db.Query<EmpresaListas>(delegate(EmpresaListas xx) { return (xx.idEmp == idEmp); }))
            {
                db.Delete(xxx.centroCustos);
                db.Delete(xxx.planosConta);
                db.Delete(xxx.contas);
                db.Delete(xxx.portadores);
                db.Delete(xxx.tiposDocumento);
                db.Delete(xxx);
            }

            EmpresaListas eLis = new EmpresaListas()
            {
                idEmp = idEmp,
                centroCustos = new List<Finan_CentroCusto>()
                {
                    new Finan_CentroCusto() { idEmp=idEmp, id=1, nome="ADMINISTRATIVO" },
                    new Finan_CentroCusto() { idEmp=idEmp, id=2, nome="COMERCIAL" }
                },
                /*
                planosConta = new List<Finan_PlanoConta>()
                {
                    new Finan_PlanoConta() { idEmp=idEmp, tipo=EPlanoContaTipo.Im, cod="001.000.00", codCateg=1, codConta=0, nomeCateg="PESSOAL", nomeConta="-" },
                    new Finan_PlanoConta() { idEmp=idEmp, tipo=EPlanoContaTipo.Im, cod="002.000.00", codCateg=2, codConta=0, nomeCateg="MARKETING", nomeConta="-" },
                    new Finan_PlanoConta() { idEmp=idEmp, tipo=EPlanoContaTipo.Im, cod="003.000.00", codCateg=3, codConta=0, nomeCateg="FINANCEIRO", nomeConta="-" },
                    new Finan_PlanoConta() { idEmp=idEmp, tipo=EPlanoContaTipo.Im, cod="004.000.00", codCateg=4, codConta=0, nomeCateg="IMPOSTOS", nomeConta="-" },
                    new Finan_PlanoConta() { idEmp=idEmp, tipo=EPlanoContaTipo.Im, cod="005.000.00", codCateg=5, codConta=0, nomeCateg="COMERCIAL", nomeConta="-" },
                    new Finan_PlanoConta() { idEmp=idEmp, tipo=EPlanoContaTipo.RV, cod="005.001.00", codCateg=5, codConta=1, nomeCateg="COMERCIAL", nomeConta="VENDAS" },
                    new Finan_PlanoConta() { idEmp=idEmp, tipo=EPlanoContaTipo.DV, cod="005.002.00", codCateg=5, codConta=2, nomeCateg="COMERCIAL", nomeConta="COMPRAS" },
                    new Finan_PlanoConta() { idEmp=idEmp, tipo=EPlanoContaTipo.Im, cod="006.000.00", codCateg=6, codConta=0, nomeCateg="CREDITOS BANCARIOS", nomeConta="-" },
                    new Finan_PlanoConta() { idEmp=idEmp, tipo=EPlanoContaTipo.Im, cod="007.000.00", codCateg=7, codConta=0, nomeCateg="DEBITOS BANCARIOS", nomeConta="-" },
                    new Finan_PlanoConta() { idEmp=idEmp, tipo=EPlanoContaTipo.Im, cod="008.000.00", codCateg=8, codConta=0, nomeCateg="VEICULOS", nomeConta="-" },
                    new Finan_PlanoConta() { idEmp=idEmp, tipo=EPlanoContaTipo.Im, cod="009.000.00", codCateg=9, codConta=0, nomeCateg="OPERACIONAIS", nomeConta="-" },
                    new Finan_PlanoConta() { idEmp=idEmp, tipo=EPlanoContaTipo.Im, cod="010.000.00", codCateg=10, codConta=0, nomeCateg="PROLABORE", nomeConta="-" }
                },
                
                portadores = new List<Finan_Portador>()
                {
                    neFinan_Portador() { idEmp=idEmp, id=1, tipo=EPortadorTipo.Carteira, nome="CARTEIRA" }
                },
                tiposDocento = new List<Finan_TipoDocumento>()
                {
                    new Finan_TipoDocumento() { id=1, idEmp=idEmp, nome="NOTA FISCAL" }
                },
                contas = new List<Finan_Conta>()
                {
                    new Finan_Conta() { idEmp=1, id=1, tipo=EContaTipo.Caixa, nome="FINANCEIRO", dtSaldoInicial="01/01/1900" }
                },
                formas = new List<Finan_TipoPagamento>()
            };

            db.Store(eLis);
            */
        }

    }
}
