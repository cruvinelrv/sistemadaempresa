package SDE.Componentes.Combo
{
    import Core.App;
    import SDE.Entidade.Orcamento_Lancamento;
    import mx.controls.ComboBox;
    public final class CmbOrcamento_Lancamento extends ComboBox
    {
        public function CmbOrcamento_Lancamento()
        {
            super();
            dataProvider = App.single.cache.arrayOrcamento_Lancamento;
        }
        public function setIdentificador(identificador:Number):void
        {
            if (identificador==0)
                selectedIndex = 0;
            else
                selectedItem = App.single.cache.getOrcamento_Lancamento(identificador);
        }
        public function getAs():Orcamento_Lancamento
        {
            return selectedItem as Orcamento_Lancamento;
        }
    }
}
