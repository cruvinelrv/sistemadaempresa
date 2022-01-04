package SDE.Componentes.Combo
{
    import Core.App;
    import SDE.Entidade.Cad_Secao;
    import mx.controls.ComboBox;
    public final class CmbCad_Secao extends ComboBox
    {
        public function CmbCad_Secao()
        {
            super();
            dataProvider = App.single.cache.arrayCad_Secao;
        }
        public function setIdentificador(identificador:Number):void
        {
            if (identificador==0)
                selectedIndex = 0;
            else
                selectedItem = App.single.cache.getCad_Secao(identificador);
        }
        public function getAs():Cad_Secao
        {
            return selectedItem as Cad_Secao;
        }
    }
}
