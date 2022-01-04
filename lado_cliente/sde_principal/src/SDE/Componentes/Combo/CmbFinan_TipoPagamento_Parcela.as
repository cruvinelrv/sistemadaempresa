package SDE.Componentes.Combo
{
    import Core.App;
    import SDE.Entidade.Finan_TipoPagamento_Parcela;
    import mx.controls.ComboBox;
    public final class CmbFinan_TipoPagamento_Parcela extends ComboBox
    {
        public function CmbFinan_TipoPagamento_Parcela()
        {
            super();
            dataProvider = App.single.cache.arrayFinan_TipoPagamento_Parcela;
        }
        public function setIdentificador(identificador:Number):void
        {
            if (identificador==0)
                selectedIndex = 0;
            else
                selectedItem = App.single.cache.getFinan_TipoPagamento_Parcela(identificador);
        }
        public function getAs():Finan_TipoPagamento_Parcela
        {
            return selectedItem as Finan_TipoPagamento_Parcela;
        }
    }
}
