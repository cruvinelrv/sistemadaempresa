package SDE.Componentes.Combo
{
    import Core.App;
    import SDE.Entidade.Finan_Portador;
    import mx.controls.ComboBox;
    public final class CmbFinan_Portador extends ComboBox
    {
        public function CmbFinan_Portador()
        {
            super();
            dataProvider = App.single.cache.arrayFinan_Portador;
        }
        public function setIdentificador(identificador:Number):void
        {
            if (identificador==0)
                selectedIndex = 0;
            else
                selectedItem = App.single.cache.getFinan_Portador(identificador);
        }
        public function getAs():Finan_Portador
        {
            return selectedItem as Finan_Portador;
        }
    }
}
