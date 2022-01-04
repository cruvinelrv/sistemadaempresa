package SDE.CamadaServico
{
    import mx.rpc.events.ResultEvent;
    import Core.Sessao;
    import Core.ConexaoServidor.MyRemoteObject;
    import SDE.Parametro.ParamFiltroMov;
    import SDE.Parametro.ParamFiltroCliente;
    import SDE.Parametro.ParamFiltroItem;
    
    public final class SRelatorio
    {
        private static var _:SRelatorio;
        public static function get unica():SRelatorio
        {
            if (_==null) _=new SRelatorio();
                return _;
        }
        private var ro:MyRemoteObject = new MyRemoteObject('SDE.CamadaServico.SRelatorio');
        //ev.result    //CS=Empresa    //AS=Empresa
        public function LoadCabecalho(idCorporacao:Number, idEmpresa:Number, fRetorno:Function=null):void
        {
            ro.Invoca('LoadCabecalho', [idCorporacao, idEmpresa], fRetorno);
        }
        //ev.result    //CS=IList<Mov>    //AS=Array
        public function RelNFE(idCorporacao:Number, idEmpresa:Number, idMov:Number, fRetorno:Function=null):void
        {
            ro.Invoca('RelNFE', [idCorporacao, idEmpresa, idMov], fRetorno);
        }
        //ev.result    //CS=IList<Mov>    //AS=Array
        public function RelMovResumo(idCorporacao:Number, pf:ParamFiltroMov, fRetorno:Function=null):void
        {
            ro.Invoca('RelMovResumo', [idCorporacao, pf], fRetorno);
        }
        //ev.result    //CS=IList<Mov>    //AS=Array
        public function RelMovId(idCorporacao:Number, pf:ParamFiltroMov, fRetorno:Function=null):void
        {
            ro.Invoca('RelMovId', [idCorporacao, pf], fRetorno);
        }
        //ev.result    //CS=IList<Mov>    //AS=Array
        public function RelMovDiario(idCorporacao:Number, pf:ParamFiltroMov, fRetorno:Function=null):void
        {
            ro.Invoca('RelMovDiario', [idCorporacao, pf], fRetorno);
        }
        //ev.result    //CS=IList<Cliente>    //AS=Array
        public function RelClientes(idCorporacao:Number, pf:ParamFiltroCliente, fRetorno:Function=null):void
        {
            ro.Invoca('RelClientes', [idCorporacao, pf], fRetorno);
        }
        //ev.result    //CS=IList<Item>    //AS=Array
        public function RelEstoque(idCorporacao:Number, idEmpresa:Number, pf:ParamFiltroItem, fRetorno:Function=null):void
        {
            ro.Invoca('RelEstoque', [idCorporacao, idEmpresa, pf], fRetorno);
        }
        //ev.result    //CS=IList<Item>    //AS=Array
        public function Etiquetas(idCorporacao:Number, idEmpresa:Number, listaIdEstoque:Array, fRetorno:Function=null):void
        {
            ro.Invoca('Etiquetas', [idCorporacao, idEmpresa, listaIdEstoque], fRetorno);
        }
        //ev.result    //CS=IList<Item>    //AS=Array
        public function EtiquetasIdMov(idCorporacao:Number, idEmpresa:Number, idMov:Number, fRetorno:Function=null):void
        {
            ro.Invoca('EtiquetasIdMov', [idCorporacao, idEmpresa, idMov], fRetorno);
        }
    }
}
