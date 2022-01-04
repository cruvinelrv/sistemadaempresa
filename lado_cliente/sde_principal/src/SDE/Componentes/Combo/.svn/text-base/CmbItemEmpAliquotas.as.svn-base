package SDE.Componentes.Combo
{
    import Core.App;
    import SDE.Entidade.ItemEmpAliquotas;
    import mx.controls.ComboBox;
    public final class CmbItemEmpAliquotas extends ComboBox
    {
        public function CmbItemEmpAliquotas()
        {
            super();
            dataProvider = App.single.cache.arrayItemEmpAliquotas;
        }
        public function setIdentificador(identificador:Number):void
        {
            if (identificador==0)
                selectedIndex = 0;
            else
                selectedItem = App.single.cache.getItemEmpAliquotas(identificador);
        }
        public function getAs():ItemEmpAliquotas
        {
            return selectedItem as ItemEmpAliquotas;
        }
    }
}
