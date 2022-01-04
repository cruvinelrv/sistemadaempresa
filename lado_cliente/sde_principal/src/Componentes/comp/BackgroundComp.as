package Componentes.comp
{
	import SDE.Enumerador.EMovTipo;
	
	import flash.display.Graphics;
	
	import mx.controls.DataGrid;
	import mx.controls.Label;
	import mx.controls.dataGridClasses.DataGridListData;
	
	[Style(name="backgroundColor", type="uint", format="Color", inherit="no")]
    public class BackgroundComp extends Label
    {
        override protected function updateDisplayList(unscaledWidth:Number, unscaledHeight:Number):void
		{
		    super.updateDisplayList(unscaledWidth, unscaledHeight);
		 	
		    var g:Graphics = graphics;
		    g.clear();
		    var grid1:DataGrid = DataGrid(DataGridListData(listData).owner);
		    if (grid1.isItemSelected(data) || grid1.isItemHighlighted(data))
		    	return;
		    if (data.tipo == EMovTipo.ambos_cancel || data.tipo == EMovTipo.entrada_cancel || data.tipo == EMovTipo.outros_cancel || data.tipo == EMovTipo.saida_cancel) 
            {
		        g.beginFill( 0xFF9999 );
		        g.drawRect(0, 0, unscaledWidth, unscaledHeight);
		        g.endFill();
	    	}
		}
    }
}

	/* OUTRA FORMA DE FAZER
	[Style(name="backgroundColor", type="uint", format="Color", inherit="no")]
    public class BackgroundComp extends Label {

        override protected function updateDisplayList(unscaledWidth:Number, unscaledHeight:Number):void
	{
	     super.updateDisplayList(unscaledWidth, unscaledHeight);
	 		
	     var g:Graphics = graphics;
	     g.clear();
	     var grid1:DataGrid = DataGrid(DataGridListData(listData).owner);
	     if (grid1.isItemSelected(data) || grid1.isItemHighlighted(data))
	         return;
	     if (data[DataGridListData(listData).dataField] > 15) 
             {
	         g.beginFill(0xCC0033);
	         g.drawRect(0, 0, unscaledWidth, unscaledHeight);
	         g.endFill();
	     }
	}
    }
    */