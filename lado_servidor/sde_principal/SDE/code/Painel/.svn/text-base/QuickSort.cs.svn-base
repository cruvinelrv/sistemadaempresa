using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

namespace SDE.code.Utils
{
    public class QuickSort
    {
        public static void Ordenar(int[] vetor)
        {
            Ordenar(vetor, 0, vetor.Length - 1);
        }

        private static void Ordenar(int[] vetor, int inicio, int fim)
        {
            if (inicio < fim)
            {
                int posicaoPivo = Separar(vetor, inicio, fim);
                Ordenar(vetor, inicio, posicaoPivo - 1);
                Ordenar(vetor, posicaoPivo + 1, fim);
            }
        }

        private static int Separar(int[] vetor, int inicio, int fim)
        {
            int pivo = vetor[inicio];
            int i = inicio + 1, f = fim;
            while (i <= f)
            {
                if (vetor[i] <= pivo)
                    i++;
                else if (pivo < vetor[f])
                    f--;
                else
                {
                    int troca = vetor[i];
                    vetor[i] = vetor[f];
                    vetor[f] = troca;
                    i++;
                    f--;
                }
            }
            vetor[inicio] = vetor[f];
            vetor[f] = pivo;
            return f;
        }
    }
}
