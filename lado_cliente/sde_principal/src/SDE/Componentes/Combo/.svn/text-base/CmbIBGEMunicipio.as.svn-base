package SDE.Componentes.Combo
{
    import Core.App;
    import SDE.Entidade.IBGEMunicipio;
    import mx.controls.ComboBox;
    public final class CmbIBGEMunicipio extends ComboBox
    {
        public function CmbIBGEMunicipio()
        {
            super();
            dataProvider = App.single.cache.arrayIBGEMunicipio;
        }
        public function setIdentificador(identificador:Number):void
        {
            if (identificador==0)
                selectedIndex = 0;
            else
                selectedItem = App.single.cache.getIBGEMunicipio(identificador);
        }
        public function getAs():IBGEMunicipio
        {
            return selectedItem as IBGEMunicipio;
        }
    }
}
