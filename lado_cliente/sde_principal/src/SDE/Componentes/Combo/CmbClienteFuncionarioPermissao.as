package SDE.Componentes.Combo
{
    import Core.App;
    import SDE.Entidade.ClienteFuncionarioPermissao;
    import mx.controls.ComboBox;
    public final class CmbClienteFuncionarioPermissao extends ComboBox
    {
        public function CmbClienteFuncionarioPermissao()
        {
            super();
            dataProvider = App.single.cache.arrayClienteFuncionarioPermissao;
        }
        public function setIdentificador(identificador:Number):void
        {
            if (identificador==0)
                selectedIndex = 0;
            else
                selectedItem = App.single.cache.getClienteFuncionarioPermissao(identificador);
        }
        public function getAs():ClienteFuncionarioPermissao
        {
            return selectedItem as ClienteFuncionarioPermissao;
        }
    }
}
