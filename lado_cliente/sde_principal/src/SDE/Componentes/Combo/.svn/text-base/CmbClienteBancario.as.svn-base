package SDE.Componentes.Combo
{
    import Core.App;
    import SDE.Entidade.ClienteBancario;
    import mx.controls.ComboBox;
    public final class CmbClienteBancario extends ComboBox
    {
        public function CmbClienteBancario()
        {
            super();
            dataProvider = App.single.cache.arrayClienteBancario;
        }
        public function setIdentificador(identificador:Number):void
        {
            if (identificador==0)
                selectedIndex = 0;
            else
                selectedItem = App.single.cache.getClienteBancario(identificador);
        }
        public function getAs():ClienteBancario
        {
            return selectedItem as ClienteBancario;
        }
    }
}
