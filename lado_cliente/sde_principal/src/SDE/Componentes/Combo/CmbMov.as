package SDE.Componentes.Combo
{
    import Core.App;
    import SDE.Entidade.Mov;
    import mx.controls.ComboBox;
    public final class CmbMov extends ComboBox
    {
        public function CmbMov()
        {
            super();
            dataProvider = App.single.cache.arrayMov;
        }
        public function setIdentificador(identificador:Number):void
        {
            if (identificador==0)
                selectedIndex = 0;
            else
                selectedItem = App.single.cache.getMov(identificador);
        }
        public function getAs():Mov
        {
            return selectedItem as Mov;
        }
    }
}
