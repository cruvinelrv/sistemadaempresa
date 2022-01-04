package SDE.FachadaServico
{
    import mx.rpc.events.ResultEvent;
    import SDE.CamadaServico.SCorp;
    
    public final class FcdCorp
    {
        private static var _:FcdCorp;
        public static function get unica():FcdCorp
        {
            if (_==null) _=new FcdCorp();
                return _;
        }
        //ev.result    //CS=CFOP    //AS=CFOP
        public function Load_CFOP(codigoCFOP:String, fRetorno:Function=null):void
        {
            SCorp.unica.Load_CFOP(codigoCFOP, fRetorno);
        }
        //ev.result    //CS=Empresa    //AS=Empresa
        public function LoadEmpresaCompleta(fRetorno:Function=null):void
        {
            SCorp.unica.LoadEmpresaCompleta(fRetorno);
        }
        //ev.result    //CS=Empresa    //AS=Empresa
        public function LoadEmpresa(fRetorno:Function=null):void
        {
            SCorp.unica.LoadEmpresa(fRetorno);
        }
    }
}
