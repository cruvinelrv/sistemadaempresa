package SDE.Componentes.Combo
{
    import Core.App;
    import SDE.Entidade.Balanco;
    import mx.controls.ComboBox;
    public final class CmbBalanco extends ComboBox
    {
        public function CmbBalanco()
        {
            super();
            dataProvider = App.single.cache.arrayBalanco;
        }
        public function setIdentificador(identificador:Number):void
        {
            if (identificador==0)
                selectedIndex = 0;
            else
                selectedItem = App.single.cache.getBalanco(identificador);
        }
        public function getAs():Balanco
        {
            return selectedItem as Balanco;
        }
    }
}
