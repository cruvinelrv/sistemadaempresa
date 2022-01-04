package SDE.Componentes.Combo
{
    import Core.App;
    import SDE.Entidade.OrdemServico;
    import mx.controls.ComboBox;
    public final class CmbOrdemServico extends ComboBox
    {
        public function CmbOrdemServico()
        {
            super();
            dataProvider = App.single.cache.arrayOrdemServico;
        }
        public function setIdentificador(identificador:Number):void
        {
            if (identificador==0)
                selectedIndex = 0;
            else
                selectedItem = App.single.cache.getOrdemServico(identificador);
        }
        public function getAs():OrdemServico
        {
            return selectedItem as OrdemServico;
        }
    }
}
