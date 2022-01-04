package SDE.Componentes.Combo
{
    import Core.App;
    import SDE.Entidade.OrdemServico_Item;
    import mx.controls.ComboBox;
    public final class CmbOrdemServico_Item extends ComboBox
    {
        public function CmbOrdemServico_Item()
        {
            super();
            dataProvider = App.single.cache.arrayOrdemServico_Item;
        }
        public function setIdentificador(identificador:Number):void
        {
            if (identificador==0)
                selectedIndex = 0;
            else
                selectedItem = App.single.cache.getOrdemServico_Item(identificador);
        }
        public function getAs():OrdemServico_Item
        {
            return selectedItem as OrdemServico_Item;
        }
    }
}
