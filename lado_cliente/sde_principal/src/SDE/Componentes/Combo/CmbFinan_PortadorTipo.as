package SDE.Componentes.Combo
{
    import Core.App;
    import SDE.Entidade.Finan_PortadorTipo;
    import mx.controls.ComboBox;
    public final class CmbFinan_PortadorTipo extends ComboBox
    {
        public function CmbFinan_PortadorTipo()
        {
            super();
            dataProvider = App.single.cache.arrayFinan_PortadorTipo;
        }
        public function setIdentificador(identificador:Number):void
        {
            if (identificador==0)
                selectedIndex = 0;
            else
                selectedItem = App.single.cache.getFinan_PortadorTipo(identificador);
        }
        public function getAs():Finan_PortadorTipo
        {
            return selectedItem as Finan_PortadorTipo;
        }
    }
}
