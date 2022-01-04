package SDE.Componentes.Combo
{
    import Core.App;
    import SDE.Entidade.Finan_TipoPagamento;
    import mx.controls.ComboBox;
    public final class CmbFinan_TipoPagamento extends ComboBox
    {
        public function CmbFinan_TipoPagamento()
        {
            super();
            dataProvider = App.single.cache.arrayFinan_TipoPagamento;
        }
        public function setIdentificador(identificador:Number):void
        {
            if (identificador==0)
                selectedIndex = 0;
            else
                selectedItem = App.single.cache.getFinan_TipoPagamento(identificador);
        }
        public function getAs():Finan_TipoPagamento
        {
            return selectedItem as Finan_TipoPagamento;
        }
    }
}
