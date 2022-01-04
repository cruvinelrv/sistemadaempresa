package SDE.Componentes.Combo
{
    import Core.App;
    import SDE.Entidade.ItemEmpPreco;
    import mx.controls.ComboBox;
    public final class CmbItemEmpPreco extends ComboBox
    {
        public function CmbItemEmpPreco()
        {
            super();
            dataProvider = App.single.cache.arrayItemEmpPreco;
        }
        public function setIdentificador(identificador:Number):void
        {
            if (identificador==0)
                selectedIndex = 0;
            else
                selectedItem = App.single.cache.getItemEmpPreco(identificador);
        }
        public function getAs():ItemEmpPreco
        {
            return selectedItem as ItemEmpPreco;
        }
    }
}
