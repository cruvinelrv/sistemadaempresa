using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net;
using System.Diagnostics;

namespace SDE.Desktop.ConexaoWeb
{
    class GerenteBaixaRelatorio
    {
        public void BaixaRelatorio(int idCorp, string relatorio, string nomeRelatorio)
        {
            string diretorio = "C:\\Documentos SDE\\Relatórios\\";
            if (!Directory.Exists(diretorio))
                Directory.CreateDirectory(diretorio);

            WebClient webClient = new WebClient();
            webClient.Credentials = new NetworkCredential("Administrator", Program.SENHA_FTP);
            byte[] dadosArquivo = webClient.DownloadData(Program.SDE_EMERGENCIA + idCorp + "/" + relatorio + ".pdf");

            FileStream arquivo = File.Create(diretorio + nomeRelatorio + ".pdf");
            arquivo.Write(dadosArquivo, 0, dadosArquivo.Length);
            arquivo.Close();
            Process.Start(diretorio + nomeRelatorio + ".pdf");
        }
    }
}
