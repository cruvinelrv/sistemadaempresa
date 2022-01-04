package SDE.Componentes.Combo
{
    import Core.App;
    import SDE.Entidade.ItemEmp;
    import mx.controls.ComboBox;
    public final class CmbItemEmp extends ComboBox
    {
        public function CmbItemEmp()
        {
            super();
            dataProvider = App.single.cache.arrayItemEmp;
        }
        public function setIdentificador(identificador:Number):void
        {
            if (identificador==0)
                selectedIndex = 0;
            else
                selectedItem = App.single.cache.getItemEmp(identificador);
        }
        public function getAs():ItemEmp
        {
            return selectedItem as ItemEmp;
        }
    }
}
