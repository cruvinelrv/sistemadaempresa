package SDE.FachadaServico
{
    import mx.rpc.events.ResultEvent;
    import SDE.Parametro.ParamFiltroMov;
    import SDE.Parametro.ParamFiltroCliente;
    import SDE.Parametro.ParamFiltroItem;
    import SDE.CamadaServico.SRelatorio;
    
    public final class FcdRelatorio
    {
        private static var _:FcdRelatorio;
        public static function get unica():FcdRelatorio
        {
            if (_==null) _=new FcdRelatorio();
                return _;
        }
        //ev.result    //CS=Empresa    //AS=Empresa
        public function LoadCabecalho(idCorporacao:Number, idEmpresa:Number, fRetorno:Function=null):void
        {
            SRelatorio.unica.LoadCabecalho(idCorporacao, idEmpresa, fRetorno);
        }
        //ev.result    //CS=IList<Mov>    //AS=Array
        public function RelNFE(idCorporacao:Number, idEmpresa:Number, idMov:Number, fRetorno:Function=null):void
        {
            SRelatorio.unica.RelNFE(idCorporacao, idEmpresa, idMov, fRetorno);
        }
        //ev.result    //CS=IList<Mov>    //AS=Array
        public function RelMovResumo(idCorporacao:Number, pf:ParamFiltroMov, fRetorno:Function=null):void
        {
            SRelatorio.unica.RelMovResumo(idCorporacao, pf, fRetorno);
        }
        //ev.result    //CS=IList<Mov>    //AS=Array
        public function RelMovId(idCorporacao:Number, pf:ParamFiltroMov, fRetorno:Function=null):void
        {
            SRelatorio.unica.RelMovId(idCorporacao, pf, fRetorno);
        }
        //ev.result    //CS=IList<Mov>    //AS=Array
        public function RelMovDiario(idCorporacao:Number, pf:ParamFiltroMov, fRetorno:Function=null):void
        {
            SRelatorio.unica.RelMovDiario(idCorporacao, pf, fRetorno);
        }
        //ev.result    //CS=IList<Cliente>    //AS=Array
        public function RelClientes(idCorporacao:Number, pf:ParamFiltroCliente, fRetorno:Function=null):void
        {
            SRelatorio.unica.RelClientes(idCorporacao, pf, fRetorno);
        }
        //ev.result    //CS=IList<Item>    //AS=Array
        public function RelEstoque(idCorporacao:Number, idEmpresa:Number, pf:ParamFiltroItem, fRetorno:Function=null):void
        {
            SRelatorio.unica.RelEstoque(idCorporacao, idEmpresa, pf, fRetorno);
        }
        //ev.result    //CS=IList<Item>    //AS=Array
        public function Etiquetas(idCorporacao:Number, idEmpresa:Number, listaIdEstoque:Array, fRetorno:Function=null):void
        {
            SRelatorio.unica.Etiquetas(idCorporacao, idEmpresa, listaIdEstoque, fRetorno);
        }
        //ev.result    //CS=IList<Item>    //AS=Array
        public function EtiquetasIdMov(idCorporacao:Number, idEmpresa:Number, idMov:Number, fRetorno:Function=null):void
        {
            SRelatorio.unica.EtiquetasIdMov(idCorporacao, idEmpresa, idMov, fRetorno);
        }
    }
}
