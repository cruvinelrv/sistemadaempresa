package SDE.Componentes.Combo
{
    import Core.App;
    import SDE.Entidade.TempMov;
    import mx.controls.ComboBox;
    public final class CmbTempMov extends ComboBox
    {
        public function CmbTempMov()
        {
            super();
            dataProvider = App.single.cache.arrayTempMov;
        }
        public function setIdentificador(identificador:Number):void
        {
            if (identificador==0)
                selectedIndex = 0;
            else
                selectedItem = App.single.cache.getTempMov(identificador);
        }
        public function getAs():TempMov
        {
            return selectedItem as TempMov;
        }
    }
}
