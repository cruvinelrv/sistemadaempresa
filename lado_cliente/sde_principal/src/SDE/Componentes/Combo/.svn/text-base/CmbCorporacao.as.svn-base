package SDE.Componentes.Combo
{
    import Core.App;
    import SDE.Entidade.Corporacao;
    import mx.controls.ComboBox;
    public final class CmbCorporacao extends ComboBox
    {
        public function CmbCorporacao()
        {
            super();
            dataProvider = App.single.cache.arrayCorporacao;
        }
        public function setIdentificador(identificador:Number):void
        {
            if (identificador==0)
                selectedIndex = 0;
            else
                selectedItem = App.single.cache.getCorporacao(identificador);
        }
        public function getAs():Corporacao
        {
            return selectedItem as Corporacao;
        }
    }
}
