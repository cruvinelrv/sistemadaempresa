package SDE.Componentes.Combo
{
    import Core.App;
    import SDE.Entidade.TempMovNfeVeiculo;
    import mx.controls.ComboBox;
    public final class CmbTempMovNfeVeiculo extends ComboBox
    {
        public function CmbTempMovNfeVeiculo()
        {
            super();
            dataProvider = App.single.cache.arrayTempMovNfeVeiculo;
        }
        public function setIdentificador(identificador:Number):void
        {
            if (identificador==0)
                selectedIndex = 0;
            else
                selectedItem = App.single.cache.getTempMovNfeVeiculo(identificador);
        }
        public function getAs():TempMovNfeVeiculo
        {
            return selectedItem as TempMovNfeVeiculo;
        }
    }
}
