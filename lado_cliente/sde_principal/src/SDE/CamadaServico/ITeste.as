package SDE.CamadaServico
{
    import mx.rpc.events.ResultEvent;
    import Core.Sessao;
    import Core.ConexaoServidor.MyRemoteObject;
    
    public final class ITeste
    {
        private static var _:ITeste;
        public static function get unica():ITeste
        {
            if (_==null) _=new ITeste();
                return _;
        }
        private var ro:MyRemoteObject = new MyRemoteObject('SDE.CamadaServico.ITeste');
        //ev.result    //CS=Void    //AS=Void
        public function metodo1(fRetorno:Function=null):void
        {
            ro.Invoca('metodo1', [], fRetorno);
        }
        //ev.result    //CS=String    //AS=String
        public function get_prop1(fRetorno:Function=null):void
        {
            ro.Invoca('get_prop1', [], fRetorno);
        }
        //ev.result    //CS=Void    //AS=Void
        public function set_prop1(value:String, fRetorno:Function=null):void
        {
            ro.Invoca('set_prop1', [value], fRetorno);
        }
        //ev.result    //CS=String    //AS=String
        public function get_prop2(fRetorno:Function=null):void
        {
            ro.Invoca('get_prop2', [], fRetorno);
        }
        //ev.result    //CS=Void    //AS=Void
        public function set_prop3(value:String, fRetorno:Function=null):void
        {
            ro.Invoca('set_prop3', [value], fRetorno);
        }
    }
}
