using System;
using System.Collections.Generic;
using SDE.Entidade;
using Db4objects.Db4o;
using Db4objects.Db4o.Query;


namespace SDE.CamadaControle
{
    public class CCorp : SuperControle
    {/*
        public CorporacaoListas Listas()
        {
            CorporacaoListas corpListas = db.Query<CorporacaoListas>()[0];
            if (corpListas != null)
                db.Activate(corpListas, 10);
            return corpListas;
        }

        */
        public CFOP LoadCFOP(string codigoCFOP)
        {
            IList<CFOP> rs = db0.Query<CFOP>(
                delegate(CFOP cfop)
                {
                    return (cfop.codigo == codigoCFOP);
                }
            );
            return rs[0];
        }
        /*

        public void AtualizaMarcas(List<Marca> marcas, List<Marca> marcasDados)
        {
            //percorre a Lista de Grades
            for (int i = 0; i < marcasDados.Count; i++)
            {
                //verifica se há uma nova grade
                if (marcas.Count <= i)
                    marcas.Add(marcasDados[i]);
                else if (marcasDados[i].filhos != null)
                    //percorre as Lista de Grades
                    for (int j = 0; j < marcasDados[i].filhos.Count; j++)
                        //verifica se há uma nova grade
                        if (marcas[i].filhos.Count <= j)
                            marcas[i].filhos.Add(marcasDados[i].filhos[j]);
            }
            db.Ext().Configure().UpdateDepth(3);
            db.Store(marcas);
        }

        public void AtualizaSecoes(List<Categoria> secoes, List<Categoria> secoesDados)
        {
            //percorre Lista de Seção             
            for (int i = 0; i < secoesDados.Count; i++)
            {
                //verifica se há uma nova secao
                if (secoes.Count <= i)
                    secoes.Add(secoesDados[i]);
                else if (secoesDados[i].filhos != null)
                    //percorre Lista de Grupo 
                    for (int j = 0; j < secoesDados[i].filhos.Count; j++)
                        //verifica se há um novo Grupo
                        if (secoes[i].filhos.Count <= j)
                            secoes[i].filhos.Add(secoesDados[i].filhos[j]);
                        else
                        {
                            //percorre Lista de Sub-Grupos  
                            for (int k = 0; k < secoesDados[i].filhos[j].filhos.Count; k++)
                                //verifica se há um novo Sub-Grupo
                                if (secoes[i].filhos[j].filhos.Count <= k)
                                    secoes[i].filhos[j].filhos.Add(secoesDados[i].filhos[j].filhos[k]);
                        }
            }
            db.Ext().Configure().UpdateDepth(10);
            db.Store(secoes);
        }

        public void AtualizaGrade(List<Grade> grades, List<Grade> gradesDados)
        {
            //percorre a Lista de Grades
            for (int i = 0; i < gradesDados.Count; i++)
            {
                //verifica se há uma nova grade
                if (grades.Count <= i)
                    grades.Add(gradesDados[i]);
                else if (gradesDados[i].filhos != null)
                    //percorre as Lista de Grades
                    for (int j = 0; j < gradesDados[i].filhos.Count; j++)
                        //verifica se há uma nova grade
                        if (grades[i].filhos.Count <= j)
                            grades[i].filhos.Add(gradesDados[i].filhos[j]);
            }
            db.Ext().Configure().UpdateDepth(5);
            db.Store(grades);
        }
        */
    }
}
