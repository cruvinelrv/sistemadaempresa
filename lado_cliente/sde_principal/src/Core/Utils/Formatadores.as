package Core.Utils
{
	import Core.Sessao;
	
	import SDE.Constantes.Variaveis_SdeConfig;
	
	import mx.controls.DateField;
	import mx.formatters.CurrencyFormatter;
	import mx.formatters.DateFormatter;
	import mx.formatters.NumberBaseRoundType;
	import mx.formatters.NumberFormatter;
	
	public final class Formatadores
	{
		private static var _:Formatadores;
		public static function get unica():Formatadores
		{
			if (_==null)
				_ = new Formatadores();
			return _;
		}
		
		private var fmtValor:CurrencyFormatter;
		private var fmtPorcentagem:CurrencyFormatter;
		private var fmtNum:NumberFormatter;
		private var fmtData:DateFormatter;
		
		public function formataValor(valor:Number, exibeCifra:Boolean):String
		{
			if (fmtValor == null)
			{
				fmtValor = new CurrencyFormatter();
				fmtValor.precision = 2;
				fmtValor.useThousandsSeparator=true;
				fmtValor.rounding = NumberBaseRoundType.NEAREST;
			}
			if (exibeCifra)
				fmtValor.currencySymbol="R$";
			else
				fmtValor.currencySymbol = "";
			
			return fmtValor.format(valor);
		}
		public function formataValor4(valor:Number):String
		{
			fmtValor = new CurrencyFormatter();
			fmtValor.precision = 4;
			fmtValor.useThousandsSeparator = true;
			fmtValor.currencySymbol="R$";
			fmtValor.rounding = NumberBaseRoundType.NEAREST;
			return fmtValor.format(valor);
		}
		public function formataPorcentagem(valor:Number):String
		{
			if (fmtPorcentagem==null)
			{
				fmtPorcentagem = new CurrencyFormatter();
				fmtPorcentagem.precision = 2;
				fmtPorcentagem.useThousandsSeparator = true;
				fmtPorcentagem.rounding = NumberBaseRoundType.NEAREST;
				fmtPorcentagem.currencySymbol = "%";
				fmtPorcentagem.alignSymbol = "right";
			}
			return fmtPorcentagem.format(valor);
		}
		public function formataDecimal(valor:Number, precisao:Number=2):String
		{
			if (fmtNum==null)
			{
				fmtNum = new NumberFormatter();
				fmtNum.precision = precisao;
				fmtNum.useThousandsSeparator=true;
			}
			return fmtNum.format(valor);
		}
		
		public function formataData(dt:Date):String
		{
			if (fmtData==null)
			{
				fmtData = new DateFormatter();
				fmtData.formatString="DD/MM/YYYY";
			}
			return fmtData.format(dt);
		}
		
		public function stringToDate(dateString:String):Date
		{
			return DateField.stringToDate(dateString, "DD/MM/YYYY");
		}
		
		public function currencyFormatter(valor:Number):Number
		{
			var cf:CurrencyFormatter = new CurrencyFormatter();
			cf.precision = 2;
			cf.currencySymbol = "";
			cf.rounding=NumberBaseRoundType.NEAREST;
			cf.useThousandsSeparator = false;	
			return Number(cf.format(valor));	
		}
		
		public function formataCpfCnpj(valor:String):String
		{
			if (valor.length == 11)
			{
				return valor.substr(0,3) + '.' + valor.substr(3,3) + '.' + valor.substr(6,3) + '-' + valor.substr(9,10);
			}
			else if (valor.length == 14)
			{
				return valor.substr(0,2) + '.' + valor.substr(2,3) + '.' + valor.substr(5,3) + '/' + valor.substr(8,4)+ '-' + valor.substr(12,13);
			}
			else
			{
				return valor.concat('*');
			}
		}
	}
}