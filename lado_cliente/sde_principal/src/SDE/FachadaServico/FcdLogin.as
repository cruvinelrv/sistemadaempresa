package SDE.FachadaServico
{
    import mx.rpc.events.ResultEvent;
    import SDE.CamadaServico.SLogin;
    
    public final class FcdLogin
    {
        private static var _:FcdLogin;
        public static function get unica():FcdLogin
        {
            if (_==null) _=new FcdLogin();
                return _;
        }
        //ev.result    //CS=String    //AS=String
        public function ValidaVersao(versao:String, fRetorno:Function=null):void
        {
            SLogin.unica.ValidaVersao(versao, fRetorno);
        }
        //ev.result    //CS=Int32[]    //AS=Number
        public function FazLogin2(emp:String, usu:String, sen:String, fRetorno:Function=null):void
        {
            SLogin.unica.FazLogin2(emp, usu, sen, fRetorno);
        }
        //ev.result    //CS=Int32[]    //AS=Number
        public function FazLogin(empresa:String, usuario:String, senha:String, fRetorno:Function=null):void
        {
            SLogin.unica.FazLogin(empresa, usuario, senha, fRetorno);
        }
        //ev.result    //CS=Void    //AS=Void
        public function SalvaSenha(corpId:Number, empId:Number, clienteId:Number, senha:String, fRetorno:Function=null):void
        {
            SLogin.unica.SalvaSenha(corpId, empId, clienteId, senha, fRetorno);
        }
        //ev.result    //CS=Void    //AS=Void
        public function GuardaCEP(cep:String, tipo_logr:String, logr:String, bairro:String, cidade:String, uf:String, fRetorno:Function=null):void
        {
            SLogin.unica.GuardaCEP(cep, tipo_logr, logr, bairro, cidade, uf, fRetorno);
        }
    }
}
