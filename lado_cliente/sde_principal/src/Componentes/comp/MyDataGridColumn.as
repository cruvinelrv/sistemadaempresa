package Componentes.comp
{
	import Core.Utils.Formatadores;
	
	import flash.events.Event;
	
	import mx.controls.CheckBox;
	import mx.controls.dataGridClasses.DataGridColumn;
	import mx.core.ClassFactory;

	public class MyDataGridColumn extends DataGridColumn
	{
		public function MyDataGridColumn(columnName:String=null)
		{
			super(columnName);
		}
		private var _tipo:String;
		public function get tipo():String
		{
			return _tipo;
		}
		[Inspectable(category="SisEmpresa", enumeration="Nenhum,Dinheiro,SimNao,CpfCnpj,CheckBox", defaultValue="Nenhum")]
		public function set tipo(v:String):void
		{
			this.labelFunction = null;
			_tipo = v;
			if (tipo=="Dinheiro")
				this.labelFunction = this.lblfn_dinheiro;
			if (tipo=="SimNao")
				this.labelFunction = this.lblfn_simnao;
			if (tipo=="CpfCnpj")
				this.labelFunction = this.lblfn_cpfcnpj;
			if (tipo=="CheckBox")
			{
				this.itemRenderer = new ClassFactory(CheckBox);
			}
		}
		
		public var labelSim:String="SIM";
		public var labelNao:String="NÃO";
		//
		
		private function lblfn_dinheiro(data:Object, column:DataGridColumn):String
		{
			return Formatadores.unica.formataValor( data[dataField], true );
		}
		private function lblfn_simnao(data:Object, column:Object):String
		{
			return (data[dataField]) ? labelSim : labelNao;
		}
		private function lblfn_cpfcnpj (data:Object, column:DataGridColumn):String
		{
			return Formatadores.unica.formataCpfCnpj(data[dataField]);
		}
		private function get ir_checkbox():CheckBox
		{
			var c:CheckBox = new CheckBox();
			c.addEventListener(Event.CHANGE,
				function(ev:Event):void
				{
					//AlertaSistema.mensagem('data: '+data[dataField] +"\nev.data: "+ev.data[datafield]);
				}
			);
			return c;
		}
		
		
		
	}
}