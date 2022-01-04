package SDE.Componentes.Combo
{
    import Core.App;
    import SDE.Entidade.OrdemServico_Executor;
    import mx.controls.ComboBox;
    public final class CmbOrdemServico_Executor extends ComboBox
    {
        public function CmbOrdemServico_Executor()
        {
            super();
            dataProvider = App.single.cache.arrayOrdemServico_Executor;
        }
        public function setIdentificador(identificador:Number):void
        {
            if (identificador==0)
                selectedIndex = 0;
            else
                selectedItem = App.single.cache.getOrdemServico_Executor(identificador);
        }
        public function getAs():OrdemServico_Executor
        {
            return selectedItem as OrdemServico_Executor;
        }
    }
}
