package SDE.Componentes.Combo
{
    import Core.App;
    import SDE.Entidade.ContadorOperacao;
    import mx.controls.ComboBox;
    public final class CmbContadorOperacao extends ComboBox
    {
        public function CmbContadorOperacao()
        {
            super();
            dataProvider = App.single.cache.arrayContadorOperacao;
        }
        public function setIdentificador(identificador:Number):void
        {
            if (identificador==0)
                selectedIndex = 0;
            else
                selectedItem = App.single.cache.getContadorOperacao(identificador);
        }
        public function getAs():ContadorOperacao
        {
            return selectedItem as ContadorOperacao;
        }
    }
}
