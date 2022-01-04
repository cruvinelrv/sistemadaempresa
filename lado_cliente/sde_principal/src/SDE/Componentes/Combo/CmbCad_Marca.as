package SDE.Componentes.Combo
{
    import Core.App;
    import SDE.Entidade.Cad_Marca;
    import mx.controls.ComboBox;
    public final class CmbCad_Marca extends ComboBox
    {
        public function CmbCad_Marca()
        {
            super();
            dataProvider = App.single.cache.arrayCad_Marca;
        }
        public function setIdentificador(identificador:Number):void
        {
            if (identificador==0)
                selectedIndex = 0;
            else
                selectedItem = App.single.cache.getCad_Marca(identificador);
        }
        public function getAs():Cad_Marca
        {
            return selectedItem as Cad_Marca;
        }
    }
}
