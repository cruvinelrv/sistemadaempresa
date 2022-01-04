package SDE.Componentes.Combo
{
    import Core.App;
    import SDE.Entidade.ClienteFamiliar;
    import mx.controls.ComboBox;
    public final class CmbClienteFamiliar extends ComboBox
    {
        public function CmbClienteFamiliar()
        {
            super();
            dataProvider = App.single.cache.arrayClienteFamiliar;
        }
        public function setIdentificador(identificador:Number):void
        {
            if (identificador==0)
                selectedIndex = 0;
            else
                selectedItem = App.single.cache.getClienteFamiliar(identificador);
        }
        public function getAs():ClienteFamiliar
        {
            return selectedItem as ClienteFamiliar;
        }
    }
}
