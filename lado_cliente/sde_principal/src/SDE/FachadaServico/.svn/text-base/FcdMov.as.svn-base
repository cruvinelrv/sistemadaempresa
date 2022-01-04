package SDE.FachadaServico
{
    import mx.rpc.events.ResultEvent;
    import SDE.Parametro.ParamLoadMov;
    import SDE.Parametro.ParamFiltroMov;
    import SDE.Entidade.Mov;
    import SDE.CamadaServico.SMov;
    
    public final class FcdMov
    {
        private static var _:FcdMov;
        public static function get unica():FcdMov
        {
            if (_==null) _=new FcdMov();
                return _;
        }
        //ev.result    //CS=IList<Mov>    //AS=Array
        public function LoadMovEntrada_NumeroNota(idCliente:Number, numNota:Number, resumo:String, fRetorno:Function=null):void
        {
            SMov.unica.LoadMovEntrada_NumeroNota(idCliente, numNota, resumo, fRetorno);
        }
        //ev.result    //CS=IList<Mov>    //AS=Array
        public function LoadMovNumeroNota(idCliente:Number, numNota:Number, resumo:String, fRetorno:Function=null):void
        {
            SMov.unica.LoadMovNumeroNota(idCliente, numNota, resumo, fRetorno);
        }
        //ev.result    //CS=Object[]    //AS=Array
        public function LoadItemEstoque(idEmpresa:Number, barras:String, fRetorno:Function=null):void
        {
            SMov.unica.LoadItemEstoque(idEmpresa, barras, fRetorno);
        }
        //ev.result    //CS=Mov    //AS=Mov
        public function Load(idMov:Number, pl:ParamLoadMov, fRetorno:Function=null):void
        {
            SMov.unica.Load(idMov, pl, fRetorno);
        }
        //ev.result    //CS=IList<Mov>    //AS=Array
        public function Pesquisa(pl:ParamLoadMov, pf:ParamFiltroMov, fRetorno:Function=null):void
        {
            SMov.unica.Pesquisa(pl, pf, fRetorno);
        }
        //ev.result    //CS=Int32    //AS=Number
        public function NovaMovEntrada(mov:Mov, fRetorno:Function=null):void
        {
            SMov.unica.NovaMovEntrada(mov, fRetorno);
        }
        //ev.result    //CS=Int32    //AS=Number
        public function NovaMovEntradaDevolucao(mov:Mov, fRetorno:Function=null):void
        {
            SMov.unica.NovaMovEntradaDevolucao(mov, fRetorno);
        }
        //ev.result    //CS=Int32    //AS=Number
        public function NovaMovPDV(mov:Mov, fRetorno:Function=null):void
        {
            SMov.unica.NovaMovPDV(mov, fRetorno);
        }
        //ev.result    //CS=Int32    //AS=Number
        public function NovaMovServico(mov:Mov, fRetorno:Function=null):void
        {
            SMov.unica.NovaMovServico(mov, fRetorno);
        }
        //ev.result    //CS=String    //AS=String
        public function GeraDms(idMov:Number, fRetorno:Function=null):void
        {
            SMov.unica.GeraDms(idMov, fRetorno);
        }
        //ev.result    //CS=Void    //AS=Void
        public function CancelaMov(idMov:Number, fRetorno:Function=null):void
        {
            SMov.unica.CancelaMov(idMov, fRetorno);
        }
        //ev.result    //CS=Void    //AS=Void
        public function CalculaImpostos(mov:Mov, movItens:Array, fRetorno:Function=null):void
        {
            SMov.unica.CalculaImpostos(mov, movItens, fRetorno);
        }
    }
}
