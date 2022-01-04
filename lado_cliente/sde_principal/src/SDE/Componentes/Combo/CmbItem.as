package SDE.Componentes.Combo
{
    import Core.App;
    import SDE.Entidade.Item;
    import mx.controls.ComboBox;
    public final class CmbItem extends ComboBox
    {
        public function CmbItem()
        {
            super();
            dataProvider = App.single.cache.arrayItem;
        }
        public function setIdentificador(identificador:Number):void
        {
            if (identificador==0)
                selectedIndex = 0;
            else
                selectedItem = App.single.cache.getItem(identificador);
        }
        public function getAs():Item
        {
            return selectedItem as Item;
        }
    }
}
