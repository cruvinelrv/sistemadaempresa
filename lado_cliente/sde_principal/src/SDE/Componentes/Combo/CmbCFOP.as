package SDE.Componentes.Combo
{
    import Core.App;
    import SDE.Entidade.CFOP;
    import mx.controls.ComboBox;
    public final class CmbCFOP extends ComboBox
    {
        public function CmbCFOP()
        {
            super();
            dataProvider = App.single.cache.arrayCFOP;
        }
        public function setIdentificador(identificador:Number):void
        {
            if (identificador==0)
                selectedIndex = 0;
            else
                selectedItem = App.single.cache.getCFOP(identificador);
        }
        public function getAs():CFOP
        {
            return selectedItem as CFOP;
        }
    }
}
