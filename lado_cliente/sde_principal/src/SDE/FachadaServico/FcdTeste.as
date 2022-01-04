package SDE.FachadaServico
{
    import mx.rpc.events.ResultEvent;
    import SDE.CamadaServico.ITeste;
    
    public final class FcdTeste
    {
        private static var _:FcdTeste;
        public static function get unica():FcdTeste
        {
            if (_==null) _=new FcdTeste();
                return _;
        }
        //ev.result    //CS=Void    //AS=Void
        public function metodo1(fRetorno:Function=null):void
        {
            ITeste.unica.metodo1(fRetorno);
        }
        //ev.result    //CS=String    //AS=String
        public function get_prop1(fRetorno:Function=null):void
        {
            ITeste.unica.get_prop1(fRetorno);
        }
        //ev.result    //CS=Void    //AS=Void
        public function set_prop1(value:String, fRetorno:Function=null):void
        {
            ITeste.unica.set_prop1(value, fRetorno);
        }
        //ev.result    //CS=String    //AS=String
        public function get_prop2(fRetorno:Function=null):void
        {
            ITeste.unica.get_prop2(fRetorno);
        }
        //ev.result    //CS=Void    //AS=Void
        public function set_prop3(value:String, fRetorno:Function=null):void
        {
            ITeste.unica.set_prop3(value, fRetorno);
        }
    }
}
