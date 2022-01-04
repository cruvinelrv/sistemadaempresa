package SDE.Componentes.Combo
{
    import Core.App;
    import SDE.Entidade.MovValor;
    import mx.controls.ComboBox;
    public final class CmbMovValor extends ComboBox
    {
        public function CmbMovValor()
        {
            super();
            dataProvider = App.single.cache.arrayMovValor;
        }
        public function setIdentificador(identificador:Number):void
        {
            if (identificador==0)
                selectedIndex = 0;
            else
                selectedItem = App.single.cache.getMovValor(identificador);
        }
        public function getAs():MovValor
        {
            return selectedItem as MovValor;
        }
    }
}
