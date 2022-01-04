package SDE.CamadaServico
{
    import mx.rpc.events.ResultEvent;
    import Core.Sessao;
    import Core.ConexaoServidor.MyRemoteObject;
    import SDE.Parametro.ParamLoadCliente;
    import SDE.Parametro.ParamFiltroCliente;
    
    public final class SCliente
    {
        private static var _:SCliente;
        public static function get unica():SCliente
        {
            if (_==null) _=new SCliente();
                return _;
        }
        private var ro:MyRemoteObject = new MyRemoteObject('SDE.CamadaServico.SCliente');
        //ev.result    //CS=ClienteEndereco    //AS=ClienteEndereco
        public function LoadEnderecoPorId(idEndereco:Number, fRetorno:Function=null):void
        {
            ro.Invoca('LoadEnderecoPorId', [idEndereco], fRetorno);
        }
        //ev.result    //CS=Cliente    //AS=Cliente
        public function LoadClienteCpfCnpj(cpf:String, fRetorno:Function=null):void
        {
            ro.Invoca('LoadClienteCpfCnpj', [Sessao.unica.idCorp, cpf], fRetorno);
        }
        //ev.result    //CS=Cliente    //AS=Cliente
        public function Load(idCliente:Number, pl:ParamLoadCliente, fRetorno:Function=null):void
        {
            ro.Invoca('Load', [Sessao.unica.idCorp, idCliente, pl], fRetorno);
        }
        //ev.result    //CS=IList<Cliente>    //AS=Array
        public function Pesquisa(pf:ParamFiltroCliente, pl:ParamLoadCliente, fRetorno:Function=null):void
        {
            ro.Invoca('Pesquisa', [Sessao.unica.idCorp, pf, pl], fRetorno);
        }
    }
}
