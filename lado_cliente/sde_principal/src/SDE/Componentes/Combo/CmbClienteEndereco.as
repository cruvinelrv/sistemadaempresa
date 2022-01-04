package SDE.Componentes.Combo
{
    import Core.App;
    import SDE.Entidade.ClienteEndereco;
    import mx.controls.ComboBox;
    public final class CmbClienteEndereco extends ComboBox
    {
        public function CmbClienteEndereco()
        {
            super();
            dataProvider = App.single.cache.arrayClienteEndereco;
        }
        public function setIdentificador(identificador:Number):void
        {
            if (identificador==0)
                selectedIndex = 0;
            else
                selectedItem = App.single.cache.getClienteEndereco(identificador);
        }
        public function getAs():ClienteEndereco
        {
            return selectedItem as ClienteEndereco;
        }
    }
}
