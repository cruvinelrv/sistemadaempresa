package SDE.Componentes.Combo
{
    import Core.App;
    import SDE.Entidade.ItemEmpEstoque;
    import mx.controls.ComboBox;
    public final class CmbItemEmpEstoque extends ComboBox
    {
        public function CmbItemEmpEstoque()
        {
            super();
            dataProvider = App.single.cache.arrayItemEmpEstoque;
        }
        public function setIdentificador(identificador:Number):void
        {
            if (identificador==0)
                selectedIndex = 0;
            else
                selectedItem = App.single.cache.getItemEmpEstoque(identificador);
        }
        public function getAs():ItemEmpEstoque
        {
            return selectedItem as ItemEmpEstoque;
        }
    }
}
