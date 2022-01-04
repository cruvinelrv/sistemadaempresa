package SDE.Componentes.Combo
{
    import Core.App;
    import SDE.Entidade.MovNFE;
    import mx.controls.ComboBox;
    public final class CmbMovNFE extends ComboBox
    {
        public function CmbMovNFE()
        {
            super();
            dataProvider = App.single.cache.arrayMovNFE;
        }
        public function setIdentificador(identificador:Number):void
        {
            if (identificador==0)
                selectedIndex = 0;
            else
                selectedItem = App.single.cache.getMovNFE(identificador);
        }
        public function getAs():MovNFE
        {
            return selectedItem as MovNFE;
        }
    }
}
