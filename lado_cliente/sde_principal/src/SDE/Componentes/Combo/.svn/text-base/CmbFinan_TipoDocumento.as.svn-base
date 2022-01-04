package SDE.Componentes.Combo
{
    import Core.App;
    import SDE.Entidade.Finan_TipoDocumento;
    import mx.controls.ComboBox;
    public final class CmbFinan_TipoDocumento extends ComboBox
    {
        public function CmbFinan_TipoDocumento()
        {
            super();
            dataProvider = App.single.cache.arrayFinan_TipoDocumento;
        }
        public function setIdentificador(identificador:Number):void
        {
            if (identificador==0)
                selectedIndex = 0;
            else
                selectedItem = App.single.cache.getFinan_TipoDocumento(identificador);
        }
        public function getAs():Finan_TipoDocumento
        {
            return selectedItem as Finan_TipoDocumento;
        }
    }
}
