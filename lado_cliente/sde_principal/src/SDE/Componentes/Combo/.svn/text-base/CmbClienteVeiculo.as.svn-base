package SDE.Componentes.Combo
{
    import Core.App;
    import SDE.Entidade.ClienteVeiculo;
    import mx.controls.ComboBox;
    public final class CmbClienteVeiculo extends ComboBox
    {
        public function CmbClienteVeiculo()
        {
            super();
            dataProvider = App.single.cache.arrayClienteVeiculo;
        }
        public function setIdentificador(identificador:Number):void
        {
            if (identificador==0)
                selectedIndex = 0;
            else
                selectedItem = App.single.cache.getClienteVeiculo(identificador);
        }
        public function getAs():ClienteVeiculo
        {
            return selectedItem as ClienteVeiculo;
        }
    }
}
