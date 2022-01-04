package SDE.Componentes.Combo
{
    import Core.App;
    import SDE.Entidade.MovItem;
    import mx.controls.ComboBox;
    public final class CmbMovItem extends ComboBox
    {
        public function CmbMovItem()
        {
            super();
            dataProvider = App.single.cache.arrayMovItem;
        }
        public function setIdentificador(identificador:Number):void
        {
            if (identificador==0)
                selectedIndex = 0;
            else
                selectedItem = App.single.cache.getMovItem(identificador);
        }
        public function getAs():MovItem
        {
            return selectedItem as MovItem;
        }
    }
}
