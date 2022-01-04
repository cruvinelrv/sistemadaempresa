package SDE.FachadaServico
{
    import mx.rpc.events.ResultEvent;
    import SDE.Parametro.ParamLoadCliente;
    import SDE.Parametro.ParamFiltroCliente;
    import SDE.CamadaServico.SCliente;
    
    public final class FcdCliente
    {
        private static var _:FcdCliente;
        public static function get unica():FcdCliente
        {
            if (_==null) _=new FcdCliente();
                return _;
        }
        //ev.result    //CS=ClienteEndereco    //AS=ClienteEndereco
        public function LoadEnderecoPorId(idEndereco:Number, fRetorno:Function=null):void
        {
            SCliente.unica.LoadEnderecoPorId(idEndereco, fRetorno);
        }
        //ev.result    //CS=Cliente    //AS=Cliente
        public function LoadClienteCpfCnpj(cpf:String, fRetorno:Function=null):void
        {
            SCliente.unica.LoadClienteCpfCnpj(cpf, fRetorno);
        }
        //ev.result    //CS=Cliente    //AS=Cliente
        public function Load(idCliente:Number, pl:ParamLoadCliente, fRetorno:Function=null):void
        {
            SCliente.unica.Load(idCliente, pl, fRetorno);
        }
        //ev.result    //CS=IList<Cliente>    //AS=Array
        public function Pesquisa(pf:ParamFiltroCliente, pl:ParamLoadCliente, fRetorno:Function=null):void
        {
            SCliente.unica.Pesquisa(pf, pl, fRetorno);
        }
    }
}
