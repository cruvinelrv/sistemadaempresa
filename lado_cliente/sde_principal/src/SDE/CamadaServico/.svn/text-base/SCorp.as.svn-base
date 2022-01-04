package SDE.CamadaServico
{
    import mx.rpc.events.ResultEvent;
    import Core.Sessao;
    import Core.ConexaoServidor.MyRemoteObject;
    
    public final class SCorp
    {
        private static var _:SCorp;
        public static function get unica():SCorp
        {
            if (_==null) _=new SCorp();
                return _;
        }
        private var ro:MyRemoteObject = new MyRemoteObject('SDE.CamadaServico.SCorp');
        //ev.result    //CS=CFOP    //AS=CFOP
        public function Load_CFOP(codigoCFOP:String, fRetorno:Function=null):void
        {
            ro.Invoca('Load_CFOP', [codigoCFOP], fRetorno);
        }
        //ev.result    //CS=Empresa    //AS=Empresa
        public function LoadEmpresaCompleta(fRetorno:Function=null):void
        {
            ro.Invoca('LoadEmpresaCompleta', [Sessao.unica.idCorp, Sessao.unica.idEmp], fRetorno);
        }
        //ev.result    //CS=Empresa    //AS=Empresa
        public function LoadEmpresa(fRetorno:Function=null):void
        {
            ro.Invoca('LoadEmpresa', [Sessao.unica.idCorp, Sessao.unica.idEmp], fRetorno);
        }
    }
}
