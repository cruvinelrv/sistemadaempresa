package SDE.Componentes.Combo
{
    import Core.App;
    import SDE.Entidade.ContadorTransacao;
    import mx.controls.ComboBox;
    public final class CmbContadorTransacao extends ComboBox
    {
        public function CmbContadorTransacao()
        {
            super();
            dataProvider = App.single.cache.arrayContadorTransacao;
        }
        public function setIdentificador(identificador:Number):void
        {
            if (identificador==0)
                selectedIndex = 0;
            else
                selectedItem = App.single.cache.getContadorTransacao(identificador);
        }
        public function getAs():ContadorTransacao
        {
            return selectedItem as ContadorTransacao;
        }
    }
}
