package SDE.Componentes.Combo
{
    import Core.App;
    import SDE.Entidade.Finan_Conta;
    import mx.controls.ComboBox;
    public final class CmbFinan_Conta extends ComboBox
    {
        public function CmbFinan_Conta()
        {
            super();
            dataProvider = App.single.cache.arrayFinan_Conta;
        }
        public function setIdentificador(identificador:Number):void
        {
            if (identificador==0)
                selectedIndex = 0;
            else
                selectedItem = App.single.cache.getFinan_Conta(identificador);
        }
        public function getAs():Finan_Conta
        {
            return selectedItem as Finan_Conta;
        }
    }
}
