package SDE.Componentes.Combo
{
    import Core.App;
    import SDE.Entidade.Finan_Titulo;
    import mx.controls.ComboBox;
    public final class CmbFinan_Titulo extends ComboBox
    {
        public function CmbFinan_Titulo()
        {
            super();
            dataProvider = App.single.cache.arrayFinan_Titulo;
        }
        public function setIdentificador(identificador:Number):void
        {
            if (identificador==0)
                selectedIndex = 0;
            else
                selectedItem = App.single.cache.getFinan_Titulo(identificador);
        }
        public function getAs():Finan_Titulo
        {
            return selectedItem as Finan_Titulo;
        }
    }
}
