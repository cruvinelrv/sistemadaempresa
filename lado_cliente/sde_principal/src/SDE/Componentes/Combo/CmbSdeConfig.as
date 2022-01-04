package SDE.Componentes.Combo
{
    import Core.App;
    import SDE.Entidade.SdeConfig;
    import mx.controls.ComboBox;
    public final class CmbSdeConfig extends ComboBox
    {
        public function CmbSdeConfig()
        {
            super();
            dataProvider = App.single.cache.arraySdeConfig;
        }
        public function setIdentificador(identificador:Number):void
        {
            if (identificador==0)
                selectedIndex = 0;
            else
                selectedItem = App.single.cache.getSdeConfig(identificador);
        }
        public function getAs():SdeConfig
        {
            return selectedItem as SdeConfig;
        }
    }
}
