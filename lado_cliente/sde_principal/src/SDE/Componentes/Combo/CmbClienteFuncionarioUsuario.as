package SDE.Componentes.Combo
{
    import Core.App;
    import SDE.Entidade.ClienteFuncionarioUsuario;
    import mx.controls.ComboBox;
    public final class CmbClienteFuncionarioUsuario extends ComboBox
    {
        public function CmbClienteFuncionarioUsuario()
        {
            super();
            dataProvider = App.single.cache.arrayClienteFuncionarioUsuario;
        }
        public function setIdentificador(identificador:Number):void
        {
            if (identificador==0)
                selectedIndex = 0;
            else
                selectedItem = App.single.cache.getClienteFuncionarioUsuario(identificador);
        }
        public function getAs():ClienteFuncionarioUsuario
        {
            return selectedItem as ClienteFuncionarioUsuario;
        }
    }
}
