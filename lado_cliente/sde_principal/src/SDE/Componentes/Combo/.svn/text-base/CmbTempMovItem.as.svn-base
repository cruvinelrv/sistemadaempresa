package SDE.Componentes.Combo
{
    import Core.App;
    import SDE.Entidade.TempMovItem;
    import mx.controls.ComboBox;
    public final class CmbTempMovItem extends ComboBox
    {
        public function CmbTempMovItem()
        {
            super();
            dataProvider = App.single.cache.arrayTempMovItem;
        }
        public function setIdentificador(identificador:Number):void
        {
            if (identificador==0)
                selectedIndex = 0;
            else
                selectedItem = App.single.cache.getTempMovItem(identificador);
        }
        public function getAs():TempMovItem
        {
            return selectedItem as TempMovItem;
        }
    }
}
