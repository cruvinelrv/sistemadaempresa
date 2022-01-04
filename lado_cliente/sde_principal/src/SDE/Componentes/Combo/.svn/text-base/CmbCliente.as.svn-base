package SDE.Componentes.Combo
{
    import Core.App;
    import SDE.Entidade.Cliente;
    import mx.controls.ComboBox;
    public final class CmbCliente extends ComboBox
    {
        public function CmbCliente()
        {
            super();
            dataProvider = App.single.cache.arrayCliente;
        }
        public function setIdentificador(identificador:Number):void
        {
            if (identificador==0)
                selectedIndex = 0;
            else
                selectedItem = App.single.cache.getCliente(identificador);
        }
        public function getAs():Cliente
        {
            return selectedItem as Cliente;
        }
    }
}
