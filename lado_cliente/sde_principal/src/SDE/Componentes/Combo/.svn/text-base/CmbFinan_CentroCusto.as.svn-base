package SDE.Componentes.Combo
{
    import Core.App;
    import SDE.Entidade.Finan_CentroCusto;
    import mx.controls.ComboBox;
    public final class CmbFinan_CentroCusto extends ComboBox
    {
        public function CmbFinan_CentroCusto()
        {
            super();
            dataProvider = App.single.cache.arrayFinan_CentroCusto;
        }
        public function setIdentificador(identificador:Number):void
        {
            if (identificador==0)
                selectedIndex = 0;
            else
                selectedItem = App.single.cache.getFinan_CentroCusto(identificador);
        }
        public function getAs():Finan_CentroCusto
        {
            return selectedItem as Finan_CentroCusto;
        }
    }
}
