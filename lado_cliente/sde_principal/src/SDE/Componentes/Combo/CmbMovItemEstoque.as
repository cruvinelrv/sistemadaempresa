package SDE.Componentes.Combo
{
    import Core.App;
    import SDE.Entidade.MovItemEstoque;
    import mx.controls.ComboBox;
    public final class CmbMovItemEstoque extends ComboBox
    {
        public function CmbMovItemEstoque()
        {
            super();
            dataProvider = App.single.cache.arrayMovItemEstoque;
        }
        public function setIdentificador(identificador:Number):void
        {
            if (identificador==0)
                selectedIndex = 0;
            else
                selectedItem = App.single.cache.getMovItemEstoque(identificador);
        }
        public function getAs():MovItemEstoque
        {
            return selectedItem as MovItemEstoque;
        }
    }
}
