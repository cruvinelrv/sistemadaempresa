package SDE.Componentes.Combo
{
    import Core.App;
    import SDE.Entidade.Finan_TituloItem;
    import mx.controls.ComboBox;
    public final class CmbFinan_TituloItem extends ComboBox
    {
        public function CmbFinan_TituloItem()
        {
            super();
            dataProvider = App.single.cache.arrayFinan_TituloItem;
        }
        public function setIdentificador(identificador:Number):void
        {
            if (identificador==0)
                selectedIndex = 0;
            else
                selectedItem = App.single.cache.getFinan_TituloItem(identificador);
        }
        public function getAs():Finan_TituloItem
        {
            return selectedItem as Finan_TituloItem;
        }
    }
}
