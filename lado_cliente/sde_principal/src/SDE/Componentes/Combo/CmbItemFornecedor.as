package SDE.Componentes.Combo
{
    import Core.App;
    import SDE.Entidade.ItemFornecedor;
    import mx.controls.ComboBox;
    public final class CmbItemFornecedor extends ComboBox
    {
        public function CmbItemFornecedor()
        {
            super();
            dataProvider = App.single.cache.arrayItemFornecedor;
        }
        public function setIdentificador(identificador:Number):void
        {
            if (identificador==0)
                selectedIndex = 0;
            else
                selectedItem = App.single.cache.getItemFornecedor(identificador);
        }
        public function getAs():ItemFornecedor
        {
            return selectedItem as ItemFornecedor;
        }
    }
}
