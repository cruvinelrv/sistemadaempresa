package SDE.Componentes.Combo
{
    import Core.App;
    import SDE.Entidade.CargoPermissao;
    import mx.controls.ComboBox;
    public final class CmbCargoPermissao extends ComboBox
    {
        public function CmbCargoPermissao()
        {
            super();
            dataProvider = App.single.cache.arrayCargoPermissao;
        }
        public function setIdentificador(identificador:Number):void
        {
            if (identificador==0)
                selectedIndex = 0;
            else
                selectedItem = App.single.cache.getCargoPermissao(identificador);
        }
        public function getAs():CargoPermissao
        {
            return selectedItem as CargoPermissao;
        }
    }
}
