package SDE.Componentes.Combo
{
    import Core.App;
    import SDE.Entidade.BalancoItem;
    import mx.controls.ComboBox;
    public final class CmbBalancoItem extends ComboBox
    {
        public function CmbBalancoItem()
        {
            super();
            dataProvider = App.single.cache.arrayBalancoItem;
        }
        public function setIdentificador(identificador:Number):void
        {
            if (identificador==0)
                selectedIndex = 0;
            else
                selectedItem = App.single.cache.getBalancoItem(identificador);
        }
        public function getAs():BalancoItem
        {
            return selectedItem as BalancoItem;
        }
    }
}
