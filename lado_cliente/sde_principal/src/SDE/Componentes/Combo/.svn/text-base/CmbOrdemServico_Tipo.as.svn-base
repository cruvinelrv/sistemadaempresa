package SDE.Componentes.Combo
{
    import Core.App;
    import SDE.Entidade.OrdemServico_Tipo;
    import mx.controls.ComboBox;
    public final class CmbOrdemServico_Tipo extends ComboBox
    {
        public function CmbOrdemServico_Tipo()
        {
            super();
            dataProvider = App.single.cache.arrayOrdemServico_Tipo;
        }
        public function setIdentificador(identificador:Number):void
        {
            if (identificador==0)
                selectedIndex = 0;
            else
                selectedItem = App.single.cache.getOrdemServico_Tipo(identificador);
        }
        public function getAs():OrdemServico_Tipo
        {
            return selectedItem as OrdemServico_Tipo;
        }
    }
}
