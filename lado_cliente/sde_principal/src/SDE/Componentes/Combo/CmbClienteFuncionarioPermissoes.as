package SDE.Componentes.Combo
{
    import Core.App;
    import SDE.Entidade.ClienteFuncionarioPermissoes;
    import mx.controls.ComboBox;
    public final class CmbClienteFuncionarioPermissoes extends ComboBox
    {
        public function CmbClienteFuncionarioPermissoes()
        {
            super();
            dataProvider = App.single.cache.arrayClienteFuncionarioPermissoes;
        }
        public function setIdentificador(identificador:Number):void
        {
            if (identificador==0)
                selectedIndex = 0;
            else
                selectedItem = App.single.cache.getClienteFuncionarioPermissoes(identificador);
        }
        public function getAs():ClienteFuncionarioPermissoes
        {
            return selectedItem as ClienteFuncionarioPermissoes;
        }
    }
}
