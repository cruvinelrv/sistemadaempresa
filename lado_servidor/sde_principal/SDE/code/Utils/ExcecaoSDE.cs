using System;
using System.Collections.Generic;
using System.Text;

public class ExcecaoSDE : Exception
{
    public ExcecaoSDE(string mensagem):base(mensagem)
    {
        //sem código aqui
    }
}
