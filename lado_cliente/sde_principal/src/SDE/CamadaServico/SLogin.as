package SDE.CamadaServico
{
    import mx.rpc.events.ResultEvent;
    import Core.Sessao;
    import Core.ConexaoServidor.MyRemoteObject;
    
    public final class SLogin
    {
        private static var _:SLogin;
        public static function get unica():SLogin
        {
            if (_==null) _=new SLogin();
                return _;
        }
        private var ro:MyRemoteObject = new MyRemoteObject('SDE.CamadaServico.SLogin');
        //ev.result    //CS=String    //AS=String
        public function ValidaVersao(versao:String, fRetorno:Function=null):void
        {
            ro.Invoca('ValidaVersao', [versao], fRetorno);
        }
        //ev.result    //CS=Int32[]    //AS=Number
        public function FazLogin2(emp:String, usu:String, sen:String, fRetorno:Function=null):void
        {
            ro.Invoca('FazLogin2', [emp, usu, sen], fRetorno);
        }
        //ev.result    //CS=Int32[]    //AS=Number
        public function FazLogin(empresa:String, usuario:String, senha:String, fRetorno:Function=null):void
        {
            ro.Invoca('FazLogin', [empresa, usuario, senha], fRetorno);
        }
        //ev.result    //CS=Void    //AS=Void
        public function SalvaSenha(corpId:Number, empId:Number, clienteId:Number, senha:String, fRetorno:Function=null):void
        {
            ro.Invoca('SalvaSenha', [corpId, empId, clienteId, senha], fRetorno);
        }
        //ev.result    //CS=Void    //AS=Void
        public function GuardaCEP(cep:String, tipo_logr:String, logr:String, bairro:String, cidade:String, uf:String, fRetorno:Function=null):void
        {
            ro.Invoca('GuardaCEP', [cep, tipo_logr, logr, bairro, cidade, uf], fRetorno);
        }
    }
}
