package SDE.Componentes.Combo
{
    import Core.App;
    import SDE.Entidade.CargoPermissoes;
    import mx.controls.ComboBox;
    public final class CmbCargoPermissoes extends ComboBox
    {
        public function CmbCargoPermissoes()
        {
            super();
            dataProvider = App.single.cache.arrayCargoPermissoes;
        }
        public function setIdentificador(identificador:Number):void
        {
            if (identificador==0)
                selectedIndex = 0;
            else
                selectedItem = App.single.cache.getCargoPermissoes(identificador);
        }
        public function getAs():CargoPermissoes
        {
            return selectedItem as CargoPermissoes;
        }
    }
}
