package SDE.CamadaServico
{
    import mx.rpc.events.ResultEvent;
    import Core.Sessao;
    import Core.ConexaoServidor.MyRemoteObject;
    import SDE.Entidade.BalancoItem;
    
    public final class SBalanco
    {
        private static var _:SBalanco;
        public static function get unica():SBalanco
        {
            if (_==null) _=new SBalanco();
                return _;
        }
        private var ro:MyRemoteObject = new MyRemoteObject('SDE.CamadaServico.SBalanco');
        //ev.result    //CS=Void    //AS=Void
        public function Finaliza(idBalanco:Number, fRetorno:Function=null):void
        {
            ro.Invoca('Finaliza', [Sessao.unica.idCorp, idBalanco], fRetorno);
        }
        //ev.result    //CS=IList<Balanco>    //AS=Array
        public function Lista(fRetorno:Function=null):void
        {
            ro.Invoca('Lista', [Sessao.unica.idCorp], fRetorno);
        }
        //ev.result    //CS=Balanco    //AS=Balanco
        public function Load(idBalanco:Number, fRetorno:Function=null):void
        {
            ro.Invoca('Load', [Sessao.unica.idCorp, idBalanco], fRetorno);
        }
        //ev.result    //CS=BalancoItem    //AS=BalancoItem
        public function Registra(biDados:BalancoItem, fRetorno:Function=null):void
        {
            ro.Invoca('Registra', [Sessao.unica.idCorp, biDados], fRetorno);
        }
        //ev.result    //CS=Void    //AS=Void
        public function Remove(idBalancoItem:Number, fRetorno:Function=null):void
        {
            ro.Invoca('Remove', [Sessao.unica.idCorp, idBalancoItem], fRetorno);
        }
    }
}
