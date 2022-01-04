package SDE.Componentes.Combo
{
    import Core.App;
    import SDE.Entidade.MovNfeVeiculo;
    import mx.controls.ComboBox;
    public final class CmbMovNfeVeiculo extends ComboBox
    {
        public function CmbMovNfeVeiculo()
        {
            super();
            dataProvider = App.single.cache.arrayMovNfeVeiculo;
        }
        public function setIdentificador(identificador:Number):void
        {
            if (identificador==0)
                selectedIndex = 0;
            else
                selectedItem = App.single.cache.getMovNfeVeiculo(identificador);
        }
        public function getAs():MovNfeVeiculo
        {
            return selectedItem as MovNfeVeiculo;
        }
    }
}
