package SDE.Componentes.Combo
{
    import Core.App;
    import SDE.Entidade.CargoComissionamento;
    import mx.controls.ComboBox;
    public final class CmbCargoComissionamento extends ComboBox
    {
        public function CmbCargoComissionamento()
        {
            super();
            dataProvider = App.single.cache.arrayCargoComissionamento;
        }
        public function setIdentificador(identificador:Number):void
        {
            if (identificador==0)
                selectedIndex = 0;
            else
                selectedItem = App.single.cache.getCargoComissionamento(identificador);
        }
        public function getAs():CargoComissionamento
        {
            return selectedItem as CargoComissionamento;
        }
    }
}
