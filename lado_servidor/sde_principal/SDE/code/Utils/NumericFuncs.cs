using System;

/// <summary>
/// Classe para convers�o de valores inteiros e monet�rios por extenso
/// </summary>
public class ConverteValorPorExtenso
{
	/*Escreve n�meros de 0 a 999 por extenso (esta � a base para ler valores maiores
	 * por que depois � s� cuidar dos "sufixos" mil, milh�o, etc)!*/
	private static string CentenaPorExtenso(int pValor)
	{
		string ret = "";
		if((pValor > 999) || (pValor < 0))
		{
			throw(new Exception("Valor fora da faixa admitida"));
		}
		else
		{
			/*Arrays com as descri��es das unidades b�sicas*/
			string[] ArrUnidades = new string[] {"","UM","DOIS","TR�S","QUATRO","CINCO","SEIS","SETE","OITO","NOVE","DEZ","ONZE","DOZE","TREZE","QUATORZE","QUINZE","DEZESSEIS","DEZESSETE","DEZOITO","DEZENOVE"};
			string[] ArrDezenas  = new string[] {"","","VINTE","TRINTA","QUARENTA","CINQUENTA","SESSENTA","SETENTA","OITENTA","NOVENTA"};
			string[] ArrCentenas = new string[] {"","CENTO","DUZENTOS","TREZENTOS","QUATROCENTOS","QUINHENTOS","SEISCENTOS","SETECENTOS","OITOCENTOS","NOVECENTOS"};
			/*Fim da declara��o dos arrays*/

			string StrValor = pValor.ToString().PadLeft(3,'0'); //Adiciona zeros � esquerda para ler SEMPRE 3 algarismos (centena, dezena e unidade)
			/*Se o valor for menor do que 20, basta ler o array de unidades*/
			if(pValor < 20)
			{
				ret = ((pValor == 0)?"ZERO":ArrUnidades[pValor]);
			}
			else 
			{
				/*Caso seja menor que 100, l� somente a casa da dezena e unidade*/
				if(pValor < 100)
				{
					ret += ArrDezenas[Int32.Parse(StrValor.Substring(1,1))] + (StrValor.Substring(2,1).Equals("0")?"":" E " + ArrUnidades[Int32.Parse(StrValor.Substring(2,1))]);
				}
				else /*L� centena, dezena e unidade*/
				{
					string centena = ((pValor == 100)?"CEM":ArrCentenas[Int32.Parse(StrValor.Substring(0,1))]); //Centena
					string dezena = CentenaPorExtenso(Int32.Parse(StrValor.Substring(1,2))); //Dezena e unidade (0 a 99) - Como a l�gica j� est� pronta, chamo a rotina recursivamente
					/*Monta a string final*/
					ret = centena + (dezena.Equals("ZERO")?"":" E " + dezena);
				}
			}
		}
		return ret;
	}

	/*Fun��o que converte valor em valor por extenso, agora contemplando milhares!*/
	public static string ValorPorExtenso(long pValor)
	{
		string ret = "";
		
		if(pValor > long.MaxValue)
		{
			throw(new ArgumentOutOfRangeException("pValor","Valor fora da faixa admitida: de 0 at� " + long.MaxValue.ToString()));
		}
		else
		{
			if(pValor == 0)
			{
				ret = "ZERO";
			}
			else
			{
				/*Array com as defini��es dos sufixos de milhar*/
				string[] ArrMilharSingular = new string[] {"","MIL","MILH�O","BILH�O","TRILH�O"};
				string[] ArrMilharPlural = new string[] {"","MIL","MILH�ES","BILH�ES","TRILH�ES","QUATRILH�ES","QUINTRILH�ES"};
				/*Fim da declara��o dos arrays*/

				/*Acrescenta zeros � string, para fechar grupos de 3 d�gitos por milhar*/
				string StrVal = pValor.ToString();
				int QtdDig = StrVal.Length;
				while( (QtdDig % 3) != 0 )
				{
					QtdDig++;
					StrVal = StrVal.PadLeft(QtdDig,'0');
				}
				/*Fim da sub-rotina*/

				/*Conta a quantidade de milhares*/
				int QtdMilhares = (StrVal.Length / 3);

				/*Separa cada milhar em um array*/
				string[] ArrCadaMilhar = new string[QtdMilhares]; //Definitivo, com os �ndices corretos
				string[] ArrCadaMilharExtenso = new string[QtdMilhares]; //Definitivo, com os �ndices corretos
				int aux = (QtdMilhares - 1); //Para guardar as milhaes "de tr�s para frente", para o �ndice da milhar casar com o �ndice do sufixo
				for(int i = 0; i < QtdMilhares; i++)
				{
					ArrCadaMilhar[aux] = StrVal.Substring(i * 3,3);
					aux--;
				}
				/*Fim da separa��o*/

				/*Agora, l� cada milhar por extenso*/
				for(int i = 0; i < QtdMilhares; i++)
				{
					ArrCadaMilharExtenso[i] = ((Int32.Parse(ArrCadaMilhar[i]) == 0)?"":CentenaPorExtenso(Int32.Parse(ArrCadaMilhar[i])) + " " + ((Int32.Parse(ArrCadaMilhar[i]) > 1)?ArrMilharPlural[i]:ArrMilharSingular[i]));
				}
		
				aux = (QtdMilhares - 1);
				for(int i = 0; i < QtdMilhares; i++)
				{
					ret += ArrCadaMilharExtenso[aux];
					if(!ArrCadaMilharExtenso[aux].Equals(""))
					{
						ret += " E ";
					}
					aux--;
				}
				/*Retira a �ltima v�rgula*/
				ret = ret.Substring(0,ret.Trim().Length - 1);
			}
		}
		return ret;
	}

	/*Fun��o que converte moeda por extenso*/
	public static string MoedaPorExtenso(double pValor)
	{
		string ret = "";
		string StrValor = pValor.ToString("f3").Replace(",","."); //Padroniza o separador de decimal para "."
		/*Separa a parte inteira da parte decimal*/
		string ParteInteira = StrValor.Substring(0,StrValor.IndexOf("."));
		string ParteDecimal = StrValor.Substring(StrValor.IndexOf(".") + 1,2);

		/*Tratamento do range*/
		if(Int64.Parse(ParteInteira) > long.MaxValue)
		{
			throw(new ArgumentOutOfRangeException("pValor","Valor fora da faixa admitida: de 0 at� " + long.MaxValue.ToString()));
		}
		else
		{
			/*Valores por Extenso*/
			string ParteInteiraExtenso = ValorPorExtenso(Int64.Parse(ParteInteira));
			string ParteDecimalExtenso = ValorPorExtenso(Int64.Parse(ParteDecimal));

			/*Monta a string de retorno*/
			if(Int64.Parse(ParteInteira) > 2)
			{
				ret = ParteInteiraExtenso + " REAIS ";
			}
			else if(Int64.Parse(ParteInteira) > 0)
			{
				ret = ParteInteiraExtenso + " REAL ";
			}
			if(Int64.Parse(ParteDecimal) > 2)
			{
				ret += ((Int64.Parse(ParteInteira) > 0)?" E ":"") + ParteDecimalExtenso + " CENTAVOS";
			}
			else if(Int64.Parse(ParteDecimal) > 0)
			{
				ret += ((Int64.Parse(ParteInteira) > 0)?" E ":"") + ParteDecimalExtenso + " CENTAVO";
			}
		}
		return ret;
	}
}
