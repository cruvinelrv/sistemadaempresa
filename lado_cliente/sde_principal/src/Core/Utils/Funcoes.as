package Core.Utils
{
	import Core.App;
	import Core.Sessao;
	
	import SDE.Entidade.Cx_Diario;
	
	import flash.display.DisplayObject;
	import flash.events.KeyboardEvent;
	import flash.ui.Keyboard;
	
	import mx.binding.utils.BindingUtils;
	import mx.collections.ArrayCollection;
	import mx.collections.Sort;
	import mx.collections.SortField;
	import mx.events.FocusRequestDirection;
	import mx.managers.IFocusManager;
	import mx.managers.IFocusManagerComponent;
	
	public final class Funcoes
	{
		/**
		 * 
		 * 		EXEMPLO
		 * 		
				for each (var o:Object in main.getChildren())
				{
					if (o is IFocusManagerComponent && o is DisplayObject)
					{
						var o2:DisplayObject = o as DisplayObject;
						Funcoes.InterceptaTeclas(this.focusManager, o2);
					}
				}
		 * 
		 * 
		 * */
		
		public static function InterceptaTeclas(focusManager:IFocusManager, elemento:DisplayObject):void
		{
			if (!elemento is IFocusManagerComponent)
				return;
			/*
			if (!sobrepoeEventos && elemento.hasEventListener(KeyboardEvent.KEY_DOWN))
				return;
			*/
			//AlertaSistema.mensagem("Interceptando "+elemento.name);
			
			elemento.addEventListener(KeyboardEvent.KEY_DOWN,
				function(evt:KeyboardEvent):void
				{
					if(evt.keyCode == Keyboard.ENTER)
					{
						/*
						if (evt.target.className == "Button")
							(evt.target as Button).dispatchEve(new KeyboardEvent(KeyboardEvent.KEY_DOWN, true, false, Keyboard.ENTER));
						else
						*/
			       		focusManager.moveFocus(FocusRequestDirection.FORWARD);
			  		} 
					else if(evt.keyCode == Keyboard.ESCAPE)
			       		focusManager.moveFocus(FocusRequestDirection.BACKWARD);
				}
			);
		}
		
		public static function myBind(obj1:Object, prop1:String, obj2:Object, prop2:String):void
		{
			BindingUtils.bindProperty(obj1, prop1, obj2, prop2);
			BindingUtils.bindProperty(obj2, prop2, obj1, prop1);
		}
		
		public static function LimpaCPF(cpf:String):String
		{
			return cpf.replace(".","").replace(".","").replace("/","").replace("-","");
		}
		public static function MascaraCPF(cpf:String):String
		{
			if (cpf.length==11)
			{
				//93642652115
				cpf = cpf.substr(0, 3)+"."+cpf.substr(3, 3)+"."+cpf.substr(6, 3)+"-"+cpf.substr(9, 2);
			}
			else if (cpf.length==14)
			{
				cpf = cpf.substr(0, 2)+"."+cpf.substr(2, 3)+"."+cpf.substr(5, 3)+"/"+cpf.substr(8, 4)+"-"+cpf.substr(12, 2);
			}
			return cpf;
		}
		
		
		public static function filtraCampos(o:*, campos:Array):void
		{
			for each(var campo:String in campos)
			{
				if (campo.indexOf('__')>-1)
					o[campo]=null;
			}
		}
		
		
		public static function validaCpf(cpf:String):Boolean
		{
            var multiplicador1:Array =[10, 9, 8, 7, 6, 5, 4, 3, 2];
            var multiplicador2:Array =[11, 10, 9, 8, 7, 6, 5, 4, 3, 2];
            var tempCpf: String;
            var digito: String;
            var soma: int;
            var resto: int;
            cpf = LimpaCPF(cpf);
            
            var s:String = cpf.replace(cpf.charAt(0),"");
            if (s=="")
                return false;
            
            if (cpf.length != 11)
                return false;
            
            var arTemp:Array = [];
            for (var iPos:int; iPos < cpf.length; iPos++)
            {
            	var ch:String = cpf.charAt(iPos);
            	if (arTemp[ch]==null)
            		arTemp[ch] = 0;
            	else
            		arTemp[ch]++;
            	if (arTemp[ch] > 8)
            		return false;
            }
            
            tempCpf = cpf.substring(0, 9);
            soma = 0;
            for (var i:int = 0; i < 9; i++)
                soma += (parseInt(tempCpf.charAt(i))) * multiplicador1[i];

            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
                
            digito = resto.toString();
            tempCpf = tempCpf + digito;
            soma = 0;
            for (var j:int = 0; j < 10; j++)
                soma += parseInt(tempCpf.charAt(j)) * multiplicador2[j];

            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = digito + resto.toString();
            var digito2:String = cpf.substr(9,2);
            return (digito2 == digito);
  		}

		public static function validaCnpj(cnpj: String):Boolean
		{
            var multiplicador1:Array = [5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2];
            var multiplicador2:Array = [6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2];
            var soma: int;
            var resto: int;
            var digito: String;
            var tempCnpj: String;
            
            cnpj = LimpaCPF(cnpj);
            
            var s:String = cnpj.replace(cnpj.charAt(0),"");
            if (s=="")
                return false;
            
            if (cnpj.length != 14)
                return false;
            
            tempCnpj = cnpj.substring(0, 12);
            soma = 0;
            for (var i:int = 0; i < 12; i++)
                soma += parseInt(tempCnpj.charAt(i)) * multiplicador1[i];
            
            resto = (soma % 11);
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = resto.toString();
            tempCnpj = tempCnpj + digito;
            soma = 0;
            for (var j:int = 0; j < 13; j++)
                soma += parseInt(tempCnpj.charAt(j)) * multiplicador2[j];

            resto = (soma % 11);
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = digito + resto.toString();
            var digito2:String = cnpj.substr(12,2);
            return (digito2 == digito);
		}
		public static function numeroCaracteresEtiqueta(idCorp:Number):Number
		{
			if (idCorp == 56)
				return 15;
			else if (idCorp == 5)
				return 0;
			else if (idCorp == 44)
				return 18;
			else if (idCorp == 64)
				return 33;
			else
				return 0;
		}
		
		public static function getValorSaldoAtualCaixa():Number
		{
			var cxD:Cx_Diario = null;
			var ac:ArrayCollection = new ArrayCollection();
			var dataSelecionada:Date = new Date();
			for each (var xxx:Cx_Diario in App.single.cache.arrayCx_Diario)
			{
				if (xxx.idEmp == Sessao.unica.idEmp)
				{
					if (Formatadores.unica.stringToDate(xxx.data).getTime() < dataSelecionada.getTime())
					{
						var obj:Object = new Object();
						obj.cxD = xxx;
						obj.data = Formatadores.unica.stringToDate(xxx.data);
						ac.addItem(obj);
					}
				}
			}
			if (ac.length == 0)
				return 0;
			
			var sort:Sort = new Sort();
			sort.fields = [new SortField("data")];
			ac.sort = sort;
			ac.refresh();
			cxD = ac.getItemAt(ac.length - 1).cxD as Cx_Diario;
			return (cxD.valorAbertura + cxD.totalEntradas) - cxD.totalSaidas;
		}
	}
}