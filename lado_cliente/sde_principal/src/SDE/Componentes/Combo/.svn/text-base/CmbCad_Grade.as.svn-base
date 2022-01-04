package SDE.Componentes.Combo
{
    import Core.App;
    import SDE.Entidade.Cad_Grade;
    import mx.controls.ComboBox;
    public final class CmbCad_Grade extends ComboBox
    {
        public function CmbCad_Grade()
        {
            super();
            dataProvider = App.single.cache.arrayCad_Grade;
        }
        public function setIdentificador(identificador:Number):void
        {
            if (identificador==0)
                selectedIndex = 0;
            else
                selectedItem = App.single.cache.getCad_Grade(identificador);
        }
        public function getAs():Cad_Grade
        {
            return selectedItem as Cad_Grade;
        }
    }
}
