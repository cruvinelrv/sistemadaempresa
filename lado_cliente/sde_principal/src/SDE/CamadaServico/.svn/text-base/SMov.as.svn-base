package SDE.CamadaServico
{
    import mx.rpc.events.ResultEvent;
    import Core.Sessao;
    import Core.ConexaoServidor.MyRemoteObject;
    import SDE.Parametro.ParamLoadMov;
    import SDE.Parametro.ParamFiltroMov;
    import SDE.Entidade.Mov;
    
    public final class SMov
    {
        private static var _:SMov;
        public static function get unica():SMov
        {
            if (_==null) _=new SMov();
                return _;
        }
        private var ro:MyRemoteObject = new MyRemoteObject('SDE.CamadaServico.SMov');
        //ev.result    //CS=IList<Mov>    //AS=Array
        public function LoadMovEntrada_NumeroNota(idCliente:Number, numNota:Number, resumo:String, fRetorno:Function=null):void
        {
            ro.Invoca('LoadMovEntrada_NumeroNota', [Sessao.unica.idCorp, idCliente, numNota, resumo], fRetorno);
        }
        //ev.result    //CS=IList<Mov>    //AS=Array
        public function LoadMovNumeroNota(idCliente:Number, numNota:Number, resumo:String, fRetorno:Function=null):void
        {
            ro.Invoca('LoadMovNumeroNota', [Sessao.unica.idCorp, idCliente, numNota, resumo], fRetorno);
        }
        //ev.result    //CS=Object[]    //AS=Array
        public function LoadItemEstoque(idEmpresa:Number, barras:String, fRetorno:Function=null):void
        {
            ro.Invoca('LoadItemEstoque', [Sessao.unica.idCorp, idEmpresa, barras], fRetorno);
        }
        //ev.result    //CS=Mov    //AS=Mov
        public function Load(idMov:Number, pl:ParamLoadMov, fRetorno:Function=null):void
        {
            ro.Invoca('Load', [Sessao.unica.idCorp, idMov, pl], fRetorno);
        }
        //ev.result    //CS=IList<Mov>    //AS=Array
        public function Pesquisa(pl:ParamLoadMov, pf:ParamFiltroMov, fRetorno:Function=null):void
        {
            ro.Invoca('Pesquisa', [Sessao.unica.idCorp, pl, pf], fRetorno);
        }
        //ev.result    //CS=Int32    //AS=Number
        public function NovaMovEntrada(mov:Mov, fRetorno:Function=null):void
        {
            ro.Invoca('NovaMovEntrada', [Sessao.unica.idCorp, mov], fRetorno);
        }
        //ev.result    //CS=Int32    //AS=Number
        public function NovaMovEntradaDevolucao(mov:Mov, fRetorno:Function=null):void
        {
            ro.Invoca('NovaMovEntradaDevolucao', [Sessao.unica.idCorp, mov], fRetorno);
        }
        //ev.result    //CS=Int32    //AS=Number
        public function NovaMovPDV(mov:Mov, fRetorno:Function=null):void
        {
            ro.Invoca('NovaMovPDV', [Sessao.unica.idCorp, mov], fRetorno);
        }
        //ev.result    //CS=Int32    //AS=Number
        public function NovaMovServico(mov:Mov, fRetorno:Function=null):void
        {
            ro.Invoca('NovaMovServico', [Sessao.unica.idCorp, mov], fRetorno);
        }
        //ev.result    //CS=String    //AS=String
        public function GeraDms(idMov:Number, fRetorno:Function=null):void
        {
            ro.Invoca('GeraDms', [Sessao.unica.idCorp, Sessao.unica.idEmp, idMov], fRetorno);
        }
        //ev.result    //CS=Void    //AS=Void
        public function CancelaMov(idMov:Number, fRetorno:Function=null):void
        {
            ro.Invoca('CancelaMov', [Sessao.unica.idCorp, idMov, Sessao.unica.idClienteFuncionarioLogado], fRetorno);
        }
        //ev.result    //CS=Void    //AS=Void
        public function CalculaImpostos(mov:Mov, movItens:Array, fRetorno:Function=null):void
        {
            ro.Invoca('CalculaImpostos', [Sessao.unica.idEmp, mov, movItens], fRetorno);
        }
    }
}
