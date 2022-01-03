using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace SDEConfiguracoes
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Verifica Login
        /// </summary>
        /// <param name="usuario">Nome do Usuario</param>
        /// <param name="senha">Senha do Ususario</param>
        /// <returns>bool</returns>
        public static bool login(string usuario, string senha)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("http://www.sistemadaempresa.com.br/programas/Desktop/SDE_DesktopUser.xml");
            XmlNodeList xUsuario = doc.GetElementsByTagName("usuario");
            XmlNodeList xSenha = doc.GetElementsByTagName("senha");


            int n = xUsuario.Count;
            int i = 0;
            bool resultado = false;
            while (i < n)
            {
                if ((xUsuario[i].InnerText == usuario) && (xSenha[i].InnerText == senha))
                {
                    resultado = true;
                    break;
                }
                else
                {
                    resultado = false;
                }

                i++;
            }
            return (resultado);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            if (login(txtUsuario.Text, txtSenha.Text) == true)
            {
                frmSDEConfig frmConfig = new frmSDEConfig();
                frmConfig.usuario = txtUsuario.Text;
                Hide();
                frmConfig.ShowDialog();

            }
            else
            {
                MessageBox.Show("Usuário e/ou Senha Incorreto(s)!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            Cursor.Current = Cursors.Default;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void txtSenha_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (login(txtUsuario.Text, txtSenha.Text) == true)
                {
                    frmSDEConfig frmConfig = new frmSDEConfig();
                    frmConfig.usuario = txtUsuario.Text;
                    Hide();
                    frmConfig.ShowDialog();

                }
                else
                {
                    MessageBox.Show("Usuário e/ou Senha Incorreto(s)!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }


    }
}
