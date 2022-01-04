package SDE.Componentes.Combo
{
    import Core.App;
    import SDE.Entidade.Finan_GrupoTipoPagamento;
    import mx.controls.ComboBox;
    public final class CmbFinan_GrupoTipoPagamento extends ComboBox
    {
        public function CmbFinan_GrupoTipoPagamento()
        {
            super();
            dataProvider = App.single.cache.arrayFinan_GrupoTipoPagamento;
        }
        public function setIdentificador(identificador:Number):void
        {
            if (identificador==0)
                selectedIndex = 0;
            else
                selectedItem = App.single.cache.getFinan_GrupoTipoPagamento(identificador);
        }
        public function getAs():Finan_GrupoTipoPagamento
        {
            return selectedItem as Finan_GrupoTipoPagamento;
        }
    }
}
