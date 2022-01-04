package Componentes.comp
{
	import mx.controls.DateField;

	public class DateFieldBR extends DateField
	{
		public function DateFieldBR()
		{
			super();
			dayNames = ['D', 'S', 'T', 'Q', 'Q', 'S', 'S'];
			monthNames = ['Jan','Fev','Mar','Abr','Mai','Jun','Jul','Aug','Set','Out','Nov','Dez'];
			formatString = "DD/MM/YYYY";
			/*
			 
		var hoje:Date = new Date();
		dtf1.selectableRange = {
			rangeStart : new Date(2009,0,1),
			rangeEnd : hoje
		};
		dtf1.selectedDate = hoje; 
			 */
		}
		
	}
}