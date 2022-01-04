package SDE.Componentes.Combo
{
    import Core.App;
    import SDE.Entidade.Cx_Diario;
    import mx.controls.ComboBox;
    public final class CmbCx_Diario extends ComboBox
    {
        public function CmbCx_Diario()
        {
            super();
            dataProvider = App.single.cache.arrayCx_Diario;
        }
        public function setIdentificador(identificador:Number):void
        {
            if (identificador==0)
                selectedIndex = 0;
            else
                selectedItem = App.single.cache.getCx_Diario(identificador);
        }
        public function getAs():Cx_Diario
        {
            return selectedItem as Cx_Diario;
        }
    }
}
