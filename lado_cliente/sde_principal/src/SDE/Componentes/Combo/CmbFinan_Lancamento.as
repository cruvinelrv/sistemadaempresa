package SDE.Componentes.Combo
{
    import Core.App;
    import SDE.Entidade.Finan_Lancamento;
    import mx.controls.ComboBox;
    public final class CmbFinan_Lancamento extends ComboBox
    {
        public function CmbFinan_Lancamento()
        {
            super();
            dataProvider = App.single.cache.arrayFinan_Lancamento;
        }
        public function setIdentificador(identificador:Number):void
        {
            if (identificador==0)
                selectedIndex = 0;
            else
                selectedItem = App.single.cache.getFinan_Lancamento(identificador);
        }
        public function getAs():Finan_Lancamento
        {
            return selectedItem as Finan_Lancamento;
        }
    }
}
