package SDE.Componentes.Combo
{
    import Core.App;
    import SDE.Entidade.Empresa;
    import mx.controls.ComboBox;
    public final class CmbEmpresa extends ComboBox
    {
        public function CmbEmpresa()
        {
            super();
            dataProvider = App.single.cache.arrayEmpresa;
        }
        public function setIdentificador(identificador:Number):void
        {
            if (identificador==0)
                selectedIndex = 0;
            else
                selectedItem = App.single.cache.getEmpresa(identificador);
        }
        public function getAs():Empresa
        {
            return selectedItem as Empresa;
        }
    }
}
