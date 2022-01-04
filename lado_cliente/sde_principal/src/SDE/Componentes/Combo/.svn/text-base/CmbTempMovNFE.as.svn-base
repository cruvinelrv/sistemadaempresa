package SDE.Componentes.Combo
{
    import Core.App;
    import SDE.Entidade.TempMovNFE;
    import mx.controls.ComboBox;
    public final class CmbTempMovNFE extends ComboBox
    {
        public function CmbTempMovNFE()
        {
            super();
            dataProvider = App.single.cache.arrayTempMovNFE;
        }
        public function setIdentificador(identificador:Number):void
        {
            if (identificador==0)
                selectedIndex = 0;
            else
                selectedItem = App.single.cache.getTempMovNFE(identificador);
        }
        public function getAs():TempMovNFE
        {
            return selectedItem as TempMovNFE;
        }
    }
}
