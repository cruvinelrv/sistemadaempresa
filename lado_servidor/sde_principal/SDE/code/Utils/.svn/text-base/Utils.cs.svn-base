using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Globalization;

public class Utils
{

    public static bool testeWebOrb()
    {

        return true;

    }




    public static bool ValidaCNPJ(string cnpj)
    {
        if (cnpj.Length != 14)
            return false;

        int[] multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
        int[] multiplicador2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
        int soma;
        int resto;
        string digito;
        string tempCnpj;

        cnpj = cnpj.Trim();
        cnpj = cnpj.Replace(".", "").Replace("-", "").Replace("/", "");

        if (cnpj.Length != 14)
            return false;

        tempCnpj = cnpj.Substring(0, 12);

        soma = 0;
        for (int i = 0; i < 12; i++)
            soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];

        resto = (soma % 11);
        if (resto < 2)
            resto = 0;
        else
            resto = 11 - resto;

        digito = resto.ToString();

        tempCnpj = tempCnpj + digito;
        soma = 0;
        for (int i = 0; i < 13; i++)
            soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];

        resto = (soma % 11);
        if (resto < 2)
            resto = 0;
        else
            resto = 11 - resto;

        digito = digito + resto.ToString();

        return cnpj.EndsWith(digito);
    }

    public static bool ValidaCPF(string cpf)
    {
        if (cpf.Length != 11)
            return false;

        //cpf = Functions.SoNumero(cpf).PadLeft(11, '0');
        int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
        int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
        string tempCpf;
        string digito;
        int soma;
        int resto;


        string s = cpf;
        s = s.Replace(s[0].ToString(), "");
        if (s == "")
            return false;

        cpf = cpf.Trim();
        cpf = cpf.Replace(".", "").Replace("-", "");

        if (cpf.Length != 11)
            return false;

        tempCpf = cpf.Substring(0, 9);
        soma = 0;
        for (int i = 0; i < 9; i++)
            soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];

        resto = soma % 11;
        if (resto < 2)
            resto = 0;
        else
            resto = 11 - resto;

        digito = resto.ToString();

        tempCpf = tempCpf + digito;

        soma = 0;
        for (int i = 0; i < 10; i++)
            soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];

        resto = soma % 11;
        if (resto < 2)
            resto = 0;
        else
            resto = 11 - resto;

        digito = digito + resto.ToString();

        return cpf.EndsWith(digito);

    }

    public static string defineNumeroTituloItem(string prefixo, string numero, string parcela)
    {
        while (numero.Length < 7)
            numero = "0" + numero;

        return prefixo + numero + "-" + parcela;
    }

    public static string defineNumeroTitulo(string prefixo, string numero)
    {
        while (numero.Length < 7)
            numero = "0" + numero;

        return prefixo + numero;
    }




























    //valida InscricaoEstadual
    [System.Runtime.InteropServices.DllImport("DllInscE32.dll")]
    private static extern int ConsisteInscricaoEstadual(string ie, string uf);
    public static bool inscricaoValida(string insc, string UF)
    {
        int resposta = ConsisteInscricaoEstadual(insc, UF);
        return (resposta == 0);
    }

    public static bool ehNumero(string valor)
    {
        try { double.Parse(valor); return true; }
        catch (Exception) { return false; }
    }

    public static string apenasNumeros(string texto)
    {
        string r = "";
        if (texto != null)
            for (int i = 0; i < texto.Length; i++)
            {
                char c = texto[i];
                if (char.IsNumber(c))
                    r += c;
            }
        return r;
    }



    public static DateTime DateParseBR(string s)
    {
        /*
        string[] ss = s.Split('/');
        DateTime dt = new DateTime();
         * */
        return DateTime.ParseExact(s, "dd/MM/yyyy", null);
    }
    public static DateTime DateTimeParseBR(string s)
    {
        return DateTime.ParseExact(s, "dd/MM/yyyy HH:mm:ss", null);
    }
    public static string getHojeString()
    {
        return getDateString(DateTime.Now.Date);
    }
    public static string getDateString(DateTime dt)
    {
        return dt.ToString("dd/MM/yyyy");
    }
    public static string getAgoraString()
    {
        return getDateTimeString(DateTime.Now);
    }
    public static long getAgoraTicks()
    {
        return DateTime.Now.Ticks;
    }
    public static string getDateTimeString(DateTime dt)
    {
        return dt.ToString("dd/MM/yyyy HH:mm:ss");
    }

    public static void copiaCamposBasicos(object de, object para)
    {
        foreach (FieldInfo f in de.GetType().GetFields())
        {
            if (!f.Name.StartsWith("__"))//não copia se começar com "__"
            {
                object o = f.GetValue(de);
                f.SetValue(para, o);
            }
        }
    }



    public static void filtraCampos(object obj)
    {
        foreach (FieldInfo f in obj.GetType().GetFields())
        {
            if (f.Name.StartsWith("__"))
            {
                f.SetValue(obj, null);
            }
        }
    }

    public static bool verifica(string pesquisas,params string[] vValores)
    {
        string[] vPesquisas = pesquisas.Split(' ');
        int contador = 0;
        foreach (string pesq in vPesquisas)
        {
            foreach (string valor in vValores)
            {
                if (valor == null)
                    break;

                if (valor.Contains(pesq))
                {
                    contador++;
                    break;
                }
            }
            if (contador == vPesquisas.Length)
                return true;

        }
        return false;
    }


    public static void retiraCaracterEspecial(object obj)
    {
        foreach (FieldInfo f in obj.GetType().GetFields())
        {
            if ( f.FieldType != typeof(String))
                continue;
            if (f.GetValue(obj) == null)
                continue;

            string valor = f.GetValue(obj).ToString().ToUpper();
            valor = Regex.Replace(valor, "[ÁÀÂÃ]", "A");
            valor = Regex.Replace(valor, "[ÉÈÊ]", "E");
            valor = Regex.Replace(valor, "[ÍÌÎ]", "I");
            valor = Regex.Replace(valor, "[ÓÒÔÕ]", "O");
            valor = Regex.Replace(valor, "[ÚÙÛ]", "U");
            valor = Regex.Replace(valor, "[Ç]", "C");
            valor = Regex.Replace(valor, "[$&*?/~^]", " ");
            valor = Regex.Replace(valor, "['´`]", " ");
            valor = Regex.Replace(valor, "\r\n", " ");
            valor = Regex.Replace(valor, "[ªº]"," ");
            f.SetValue(obj, valor);
        }
    }

    public static void verificaCaracterEspecial(object obj)
    {
        foreach (FieldInfo f in obj.GetType().GetFields())
        {
            if (f.FieldType != typeof(String))
                continue;
            if (f.GetValue(obj) == null)
                continue;

            string frase = f.GetValue(obj).ToString().ToUpper();
            string fraseRetorno = "";
            string palavra = "";
            foreach (char space in frase)
            {
                if (space != ' ')
                    palavra += space;

                if (space == ' ')
                {
                    palavra = Regex.Replace(palavra, "R$", "");
                    palavra = Regex.Replace(palavra, "/", "");
                    palavra = Regex.Replace(palavra, "|", "");
                    palavra = Regex.Replace(palavra, "$", "");
                    palavra = Regex.Replace(palavra, "!", "");
                    palavra = Regex.Replace(palavra, "@", "");
                    palavra = Regex.Replace(palavra, "#", "");
                    palavra = Regex.Replace(palavra, "¨", "");
                    palavra = Regex.Replace(palavra, "&", "");
                    palavra = Regex.Replace(palavra, "*", "");
                    palavra = Regex.Replace(palavra, "º", "");
                    palavra = Regex.Replace(palavra, "£", "");
                    palavra = Regex.Replace(palavra, "¢", "");
                    palavra = Regex.Replace(palavra, "¬", "");

                    retiraCaracterEspecialPalavra(palavra);
                    fraseRetorno += palavra;
                    palavra = "";
                }
            }            
            f.SetValue(obj, fraseRetorno);
        }
    }

    public static void retiraCaracterEspecialPalavra(object obj)
    {
        foreach (FieldInfo f in obj.GetType().GetFields())
        {
            if (f.FieldType != typeof(String))
                continue;
            if (f.GetValue(obj) == null)
                continue;

            string valor = f.GetValue(obj).ToString().ToUpper();
            valor = Regex.Replace(valor, "[ÁÀÂÃ]", "A");
            valor = Regex.Replace(valor, "[ÉÈÊ]", "E");
            valor = Regex.Replace(valor, "[ÍÌÎ]", "I");
            valor = Regex.Replace(valor, "[ÓÒÔÕº]", "O");
            valor = Regex.Replace(valor, "[ÚÙÛÜ]", "U");
            valor = Regex.Replace(valor, "[Ç]", "C");
            f.SetValue(obj, valor);
        }
    }

    public static int numeroCaracteresEtiqueta(int idCorp)
    {
        if (idCorp == 56)
            return 15;
        else if (idCorp == 68)
            return 0;
        else if (idCorp == 44)
            return 18;
        else if (idCorp == 53)
            return 19;
        else if (idCorp == 20)
            return 52;
        else if (idCorp == 64)
            return 33;
        else if (idCorp == 76)
            return 26;
        else
            return 0;
    }

    /*
    public static List<FieldInfo> getFields(Type t)
    {
        List<FieldInfo> ls = new List<FieldInfo>();

        foreach (FieldInfo f in t.GetFields())
            if (tiposPrimitivos.Contains(f.FieldType))
                ls.Add(f);
        return ls;
    }
    */
    public static List<Type> tiposPrimitivos = new List<Type>() { typeof(string), typeof(int), typeof(bool), typeof(double), typeof(Enum) };

    public static string formatMoney(Decimal d, Boolean mostra_cifra)
    {
        if (mostra_cifra)
            return String.Format(CultureInfo.CreateSpecificCulture("pt-br"), "{0:C}", d);
        else
            return String.Format(CultureInfo.CreateSpecificCulture("pt-br"), "{0:N}", d);
    }

    public static Double formataQuantidade(Double quantidade)
    {
        return Convert.ToDouble(String.Format("{0:0.####}", quantidade));
    }

    public static string formata_cpf_cnpj(string cpf_cnpj)
    {
        StringBuilder dado = new StringBuilder();
        string mascara = "";
        string tipo = "";

        if (cpf_cnpj.Length == 11)
        {
            mascara = "###.###.###-##";
            tipo = "CPF";
        }
        else
        {
            mascara = "##.###.###/####-##";
            tipo = "CNPJ";
        }

        foreach (char c in cpf_cnpj)
        {
            if (Char.IsNumber(c))
                dado.Append(c);
        }

        int indMascara = mascara.Length;
        int indCampo = dado.Length;

        for (; indCampo > 0 && indMascara > 0; )
        {
            if (mascara[--indMascara] == '#')
                indCampo--;
        }

        StringBuilder saida = new StringBuilder();
        for (; indMascara < mascara.Length; indMascara++)
            saida.Append((mascara[indMascara] == '#') ? dado[indCampo++] : mascara[indMascara]);

        return tipo + ": " + saida.ToString();
    }

    public static DateTime StringToDateTime(string dataString)
    {
        if (dataString.Length == 10)
            return DateTime.Parse(dataString);
        else
            return DateTime.Parse(dataString.Substring(0, 10));
    }
}