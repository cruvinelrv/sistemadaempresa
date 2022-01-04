package SDE.Componentes.Combo
{
    import Core.App;
    import SDE.Entidade.Cx_Lancamento;
    import mx.controls.ComboBox;
    public final class CmbCx_Lancamento extends ComboBox
    {
        public function CmbCx_Lancamento()
        {
            super();
            dataProvider = App.single.cache.arrayCx_Lancamento;
        }
        public function setIdentificador(identificador:Number):void
        {
            if (identificador==0)
                selectedIndex = 0;
            else
                selectedItem = App.single.cache.getCx_Lancamento(identificador);
        }
        public function getAs():Cx_Lancamento
        {
            return selectedItem as Cx_Lancamento;
        }
    }
}
