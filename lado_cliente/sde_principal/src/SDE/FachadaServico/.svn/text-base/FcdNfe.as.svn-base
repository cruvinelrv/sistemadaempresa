package SDE.FachadaServico
{
    import mx.rpc.events.ResultEvent;
    import SDE.Entidade.MovNFE;
    import SDE.CamadaServico.SNfe;
    
    public final class FcdNfe
    {
        private static var _:FcdNfe;
        public static function get unica():FcdNfe
        {
            if (_==null) _=new FcdNfe();
                return _;
        }
        //ev.result    //CS=MovNFE    //AS=MovNFE
        public function LoadIdMov(idMov:Number, fRetorno:Function=null):void
        {
            SNfe.unica.LoadIdMov(idMov, fRetorno);
        }
        //ev.result    //CS=MovNFE    //AS=MovNFE
        public function LoadNumeroNota(numeroNota:Number, fRetorno:Function=null):void
        {
            SNfe.unica.LoadNumeroNota(numeroNota, fRetorno);
        }
        //ev.result    //CS=Boolean    //AS=Boolean
        public function VerificaExisteNFE(numeroNota:Number, fRetorno:Function=null):void
        {
            SNfe.unica.VerificaExisteNFE(numeroNota, fRetorno);
        }
        //ev.result    //CS=Void    //AS=Void
        public function SalvaMovNFE(idMov:Number, movnfeDados:MovNFE, fRetorno:Function=null):void
        {
            SNfe.unica.SalvaMovNFE(idMov, movnfeDados, fRetorno);
        }
        //ev.result    //CS=Void    //AS=Void
        public function DefineMovNFE_Enviada(idMov:Number, fRetorno:Function=null):void
        {
            SNfe.unica.DefineMovNFE_Enviada(idMov, fRetorno);
        }
        //ev.result    //CS=String    //AS=String
        public function GetUltimaInfoAdicional(fRetorno:Function=null):void
        {
            SNfe.unica.GetUltimaInfoAdicional(fRetorno);
        }
        //ev.result    //CS=Int32    //AS=Number
        public function GetProximoNumeroNFE(fRetorno:Function=null):void
        {
            SNfe.unica.GetProximoNumeroNFE(fRetorno);
        }
        //ev.result    //CS=String[]    //AS=Array
        public function GerarXml(idMov:Number, fRetorno:Function=null):void
        {
            SNfe.unica.GerarXml(idMov, fRetorno);
        }
        //ev.result    //CS=String[]    //AS=Array
        public function GerarTXT(idMov:Number, fRetorno:Function=null):void
        {
            SNfe.unica.GerarTXT(idMov, fRetorno);
        }
    }
}
