package SDE.Componentes.Combo
{
    import Core.App;
    import SDE.Entidade.Cargo;
    import mx.controls.ComboBox;
    public final class CmbCargo extends ComboBox
    {
        public function CmbCargo()
        {
            super();
            dataProvider = App.single.cache.arrayCargo;
        }
        public function setIdentificador(identificador:Number):void
        {
            if (identificador==0)
                selectedIndex = 0;
            else
                selectedItem = App.single.cache.getCargo(identificador);
        }
        public function getAs():Cargo
        {
            return selectedItem as Cargo;
        }
    }
}
