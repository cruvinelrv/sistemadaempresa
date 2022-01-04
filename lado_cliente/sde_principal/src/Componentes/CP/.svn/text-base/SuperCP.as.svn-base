package Componentes.CP
{
	import Core.Alerta.AlertaSistema;
	
	import com.hillelcoren.components.AdvancedAutoComplete;
	import com.hillelcoren.utils.StringUtils;
	
	import flash.events.Event;

	public class SuperCP extends AdvancedAutoComplete
	{
		public var camposPesquisa:Array = null;
		
		public function SuperCP()
		{
			super();
			
			width = 300;
			
			setStyle("backgroundColor", 0x3B5998 );
			setStyle("themeColor", 0x3B5998 );
			setStyle("verticalAlign", "middle" );
			styleName = "padding3";
			
			matchType = MATCH_ANY_PART;
			allowMultipleSelection = false;
			showBrowseButton = true;
			enableClearIcon = true;
			browseLabel = "Pesquisar";
			
			filterFunction = myFilterFunction;
		}
		
		private function myFilterFunction( item:Object, searchStr:String ):Boolean
		{
			if (searchStr.length == 0)
			{
				return true;
			}
			
			var stringCampos:String = "";
			if (camposPesquisa==null)
				stringCampos = labelFunction( item );
			else
				for each(var campo:String in camposPesquisa)
				{
					var s:* = item[campo];
					if (s!=null)
						stringCampos+=s+" ";
				}
			
			return StringUtils.contains( stringCampos, searchStr );
		}
		
	}
}