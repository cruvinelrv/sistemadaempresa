package SDE.Componentes.Combo
{
    import Core.App;
    import SDE.Entidade.Finan_TipoLancamento;
    import mx.controls.ComboBox;
    public final class CmbFinan_TipoLancamento extends ComboBox
    {
        public function CmbFinan_TipoLancamento()
        {
            super();
            dataProvider = App.single.cache.arrayFinan_TipoLancamento;
        }
        public function setIdentificador(identificador:Number):void
        {
            if (identificador==0)
                selectedIndex = 0;
            else
                selectedItem = App.single.cache.getFinan_TipoLancamento(identificador);
        }
        public function getAs():Finan_TipoLancamento
        {
            return selectedItem as Finan_TipoLancamento;
        }
    }
}
