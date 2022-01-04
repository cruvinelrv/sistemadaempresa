package SDE.Componentes.Combo
{
    import Core.App;
    import SDE.Entidade.ClienteFuncionarioComissionamento;
    import mx.controls.ComboBox;
    public final class CmbClienteFuncionarioComissionamento extends ComboBox
    {
        public function CmbClienteFuncionarioComissionamento()
        {
            super();
            dataProvider = App.single.cache.arrayClienteFuncionarioComissionamento;
        }
        public function setIdentificador(identificador:Number):void
        {
            if (identificador==0)
                selectedIndex = 0;
            else
                selectedItem = App.single.cache.getClienteFuncionarioComissionamento(identificador);
        }
        public function getAs():ClienteFuncionarioComissionamento
        {
            return selectedItem as ClienteFuncionarioComissionamento;
        }
    }
}
