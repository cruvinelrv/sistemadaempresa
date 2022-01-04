package SDE.Componentes.Combo
{
    import Core.App;
    import SDE.Entidade.IBGEEstado;
    import mx.controls.ComboBox;
    public final class CmbIBGEEstado extends ComboBox
    {
        public function CmbIBGEEstado()
        {
            super();
            dataProvider = App.single.cache.arrayIBGEEstado;
        }
        public function setIdentificador(identificador:Number):void
        {
            if (identificador==0)
                selectedIndex = 0;
            else
                selectedItem = App.single.cache.getIBGEEstado(identificador);
        }
        public function getAs():IBGEEstado
        {
            return selectedItem as IBGEEstado;
        }
    }
}
