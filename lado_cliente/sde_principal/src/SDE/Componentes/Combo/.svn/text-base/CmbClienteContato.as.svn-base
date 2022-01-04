package SDE.Componentes.Combo
{
    import Core.App;
    import SDE.Entidade.ClienteContato;
    import mx.controls.ComboBox;
    public final class CmbClienteContato extends ComboBox
    {
        public function CmbClienteContato()
        {
            super();
            dataProvider = App.single.cache.arrayClienteContato;
        }
        public function setIdentificador(identificador:Number):void
        {
            if (identificador==0)
                selectedIndex = 0;
            else
                selectedItem = App.single.cache.getClienteContato(identificador);
        }
        public function getAs():ClienteContato
        {
            return selectedItem as ClienteContato;
        }
    }
}
