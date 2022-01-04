package SDE.CamadaServico
{
    import mx.rpc.events.ResultEvent;
    import Core.Sessao;
    import Core.ConexaoServidor.MyRemoteObject;
    import SDE.Entidade.MovNFE;
    
    public final class SNfe
    {
        private static var _:SNfe;
        public static function get unica():SNfe
        {
            if (_==null) _=new SNfe();
                return _;
        }
        private var ro:MyRemoteObject = new MyRemoteObject('SDE.CamadaServico.SNfe');
        //ev.result    //CS=MovNFE    //AS=MovNFE
        public function LoadIdMov(idMov:Number, fRetorno:Function=null):void
        {
            ro.Invoca('LoadIdMov', [Sessao.unica.idCorp, idMov], fRetorno);
        }
        //ev.result    //CS=MovNFE    //AS=MovNFE
        public function LoadNumeroNota(numeroNota:Number, fRetorno:Function=null):void
        {
            ro.Invoca('LoadNumeroNota', [Sessao.unica.idCorp, numeroNota], fRetorno);
        }
        //ev.result    //CS=Boolean    //AS=Boolean
        public function VerificaExisteNFE(numeroNota:Number, fRetorno:Function=null):void
        {
            ro.Invoca('VerificaExisteNFE', [Sessao.unica.idCorp, numeroNota], fRetorno);
        }
        //ev.result    //CS=Void    //AS=Void
        public function SalvaMovNFE(idMov:Number, movnfeDados:MovNFE, fRetorno:Function=null):void
        {
            ro.Invoca('SalvaMovNFE', [Sessao.unica.idCorp, idMov, movnfeDados], fRetorno);
        }
        //ev.result    //CS=Void    //AS=Void
        public function DefineMovNFE_Enviada(idMov:Number, fRetorno:Function=null):void
        {
            ro.Invoca('DefineMovNFE_Enviada', [Sessao.unica.idCorp, idMov], fRetorno);
        }
        //ev.result    //CS=String    //AS=String
        public function GetUltimaInfoAdicional(fRetorno:Function=null):void
        {
            ro.Invoca('GetUltimaInfoAdicional', [Sessao.unica.idCorp], fRetorno);
        }
        //ev.result    //CS=Int32    //AS=Number
        public function GetProximoNumeroNFE(fRetorno:Function=null):void
        {
            ro.Invoca('GetProximoNumeroNFE', [Sessao.unica.idCorp], fRetorno);
        }
        //ev.result    //CS=String[]    //AS=Array
        public function GerarXml(idMov:Number, fRetorno:Function=null):void
        {
            ro.Invoca('GerarXml', [Sessao.unica.idCorp, idMov], fRetorno);
        }
        //ev.result    //CS=String[]    //AS=Array
        public function GerarTXT(idMov:Number, fRetorno:Function=null):void
        {
            ro.Invoca('GerarTXT', [Sessao.unica.idCorp, idMov], fRetorno);
        }
    }
}
