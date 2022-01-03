using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.Net;
using Db4objects.Db4o.Query;
using SDE.Entidade;
using SDE.Enumerador;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System.IO;
using System.Text;
using SDE.PDF;

namespace SDE.CamadaNuvem
{
    public partial class NuvemModificacoes
    {
        public string EnviaEmail(int idCorp, int idEmp, int idMov, int idCliente, string fromMailAddressData, string fromNameData, string fromPasswordData, string toMailAddressData, string toNameData, string subjectData, string messageData, string urlData)
        {
            //verifica se cliente tem email cadastrado, se não, cadastra

            defineCorp(idCorp);

            IQuery query = db.Query();
            query.Constrain(typeof(ClienteContato));
            query.Descend("idCliente").Constrain(idCliente);

            bool existe = false;

            foreach (ClienteContato cc in query.Execute())
                if (cc.tipo == EContatoTipo.email)
                    if (cc.valor == toMailAddressData)
                        existe = true;

            if (!existe)
            {
                lock (db.Ext().Lock())
                {
                    try
                    {
                        ClienteContato cc = new ClienteContato();
                        cc.id = AppFacade.get.reaproveitamento.getIncremento(db, typeof(ClienteContato), 0);
                        cc.idCliente = idCliente;
                        cc.tipo = EContatoTipo.email;
                        cc.campo = "EMAIL";
                        cc.valor = toMailAddressData;
                        cc.obs = "GERADO PELO SISTEMA";
                        dbStore(db, cc);
                        dbCommit(db);
                    }
                    catch (Exception ex)
                    {
                        dbRollback(ex, db);
                        throw;
                    }
                }
            }

            const int PORT = 587;

            SmtpClient client = new SmtpClient();

            string smtp = null;
            if (fromMailAddressData.Split('@').GetValue(1).ToString() == "gmail.com")
            {
                smtp = "smtp.gmail.com";
                client.EnableSsl = true;
            }
            else if (fromMailAddressData.Split('@').GetValue(1).ToString() == "ymail.com"
                || fromMailAddressData.Split('@').GetValue(1).ToString() == "yahoo.com.br")
                smtp = "smtp.mail.yahoo.com.br";
            else if (fromMailAddressData.Split('@').GetValue(1).ToString() == "airflux.com.br")
                smtp = "mail.airflux.com.br";
            else if (fromMailAddressData.Split('@').GetValue(1).ToString() == "uol.com.br")
                smtp = "smtp.uol.com.br";
            else
                return "Email do remetente não suportado";

            messageData = messageData.Replace('\r', '\n');

            messageData += "\n\n\nOrçamento em anexo.";

            MailAddress fromMailAddress = new MailAddress(fromMailAddressData, fromNameData);
            MailAddress toMailAddress = new MailAddress(toMailAddressData, toNameData);

            MailMessage mail = new MailMessage(fromMailAddress, toMailAddress);
            mail.Subject = subjectData + " - " + idMov;

            NetworkCredential credential = new NetworkCredential(fromMailAddress.Address, fromPasswordData);

            client.Host = smtp;
            client.Port = PORT;
            client.Credentials = credential;

            ConstrutorPDF.relatorioMov(idCorp, idEmp, idMov);
            String filePath = @"C:\Inetpub\wwwroot\ftp\documentos\" + idCorp + @"\mov.pdf";
            StreamReader streamReader = new StreamReader(filePath);
            Attachment anexo = new Attachment(streamReader.BaseStream, "Orçamento-" + idMov + ".pdf");
            mail.Attachments.Add(anexo);
            mail.Body = messageData;

            try
            {
                client.Send(mail);
            }
            catch (Exception ex)
            {
                throw new Exception("ERRO DE ENVIO:\n" + ex);
            }
            return "Mensagem Enviada";
        }
    }
}
