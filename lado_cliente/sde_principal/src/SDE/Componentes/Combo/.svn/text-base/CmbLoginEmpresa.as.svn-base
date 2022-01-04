package SDE.Componentes.Combo
{
    import Core.App;
    import SDE.Entidade.LoginEmpresa;
    import mx.controls.ComboBox;
    public final class CmbLoginEmpresa extends ComboBox
    {
        public function CmbLoginEmpresa()
        {
            super();
            dataProvider = App.single.cache.arrayLoginEmpresa;
        }
        public function setIdentificador(identificador:Number):void
        {
            if (identificador==0)
                selectedIndex = 0;
            else
                selectedItem = App.single.cache.getLoginEmpresa(identificador);
        }
        public function getAs():LoginEmpresa
        {
            return selectedItem as LoginEmpresa;
        }
    }
}
