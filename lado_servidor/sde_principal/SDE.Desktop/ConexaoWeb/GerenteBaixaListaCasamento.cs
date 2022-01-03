using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net;
using System.Diagnostics;

namespace SDE.Desktop.ConexaoWeb
{
    class GerenteBaixaListaCasamento
    {
        public void baixaListaCasamento(int idCorp)
        {
            string diretorio = @"C:\Documentos SDE\Lista Casamento\";
            string nomeArquivo = "Lista Casamento";

            if (!Directory.Exists(diretorio))
                Directory.CreateDirectory(diretorio);

            WebClient webClient = new WebClient();
            
            webClient.Credentials = new NetworkCredential("Administrator", Program.SENHA_FTP);
            byte[] dadosArquivo = webClient.DownloadData(Program.SDE_PRODUCAO + idCorp + @"/listaCasamento.pdf");
            FileStream arquivo = File.Create(diretorio + "\\" + nomeArquivo + ".pdf");
            arquivo.Write(dadosArquivo, 0, dadosArquivo.Length);
            arquivo.Close();
            Process.Start(diretorio + "\\" + nomeArquivo + ".pdf");
        }
    }
}
