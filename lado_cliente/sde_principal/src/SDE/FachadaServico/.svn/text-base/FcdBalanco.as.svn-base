package SDE.FachadaServico
{
    import mx.rpc.events.ResultEvent;
    import SDE.Entidade.BalancoItem;
    import SDE.CamadaServico.SBalanco;
    
    public final class FcdBalanco
    {
        private static var _:FcdBalanco;
        public static function get unica():FcdBalanco
        {
            if (_==null) _=new FcdBalanco();
                return _;
        }
        //ev.result    //CS=Void    //AS=Void
        public function Finaliza(idBalanco:Number, fRetorno:Function=null):void
        {
            SBalanco.unica.Finaliza(idBalanco, fRetorno);
        }
        //ev.result    //CS=IList<Balanco>    //AS=Array
        public function Lista(fRetorno:Function=null):void
        {
            SBalanco.unica.Lista(fRetorno);
        }
        //ev.result    //CS=Balanco    //AS=Balanco
        public function Load(idBalanco:Number, fRetorno:Function=null):void
        {
            SBalanco.unica.Load(idBalanco, fRetorno);
        }
        //ev.result    //CS=BalancoItem    //AS=BalancoItem
        public function Registra(biDados:BalancoItem, fRetorno:Function=null):void
        {
            SBalanco.unica.Registra(biDados, fRetorno);
        }
        //ev.result    //CS=Void    //AS=Void
        public function Remove(idBalancoItem:Number, fRetorno:Function=null):void
        {
            SBalanco.unica.Remove(idBalancoItem, fRetorno);
        }
    }
}
