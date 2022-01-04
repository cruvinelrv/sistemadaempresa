package SDE.CamadaServico
{
    import mx.rpc.events.ResultEvent;
    import Core.Sessao;
    import Core.ConexaoServidor.MyRemoteObject;
    
    public final class STecnico
    {
        private static var _:STecnico;
        public static function get unica():STecnico
        {
            if (_==null) _=new STecnico();
                return _;
        }
        private var ro:MyRemoteObject = new MyRemoteObject('SDE.CamadaServico.STecnico');
        //ev.result    //CS=Void    //AS=Void
        public function ClientesRemoveTodos(fRetorno:Function=null):void
        {
            ro.Invoca('ClientesRemoveTodos', [Sessao.unica.idCorp], fRetorno);
        }
        //ev.result    //CS=Void    //AS=Void
        public function ClientesResetaTodos(fRetorno:Function=null):void
        {
            ro.Invoca('ClientesResetaTodos', [Sessao.unica.idCorp], fRetorno);
        }
        //ev.result    //CS=Void    //AS=Void
        public function ItemRemoverTudo(fRetorno:Function=null):void
        {
            ro.Invoca('ItemRemoverTudo', [Sessao.unica.idCorp], fRetorno);
        }
        //ev.result    //CS=Void    //AS=Void
        public function ItemRedefineCodUnicoComIdItem(absolutamenteTodos:Boolean, fRetorno:Function=null):void
        {
            ro.Invoca('ItemRedefineCodUnicoComIdItem', [Sessao.unica.idCorp, absolutamenteTodos], fRetorno);
        }
        //ev.result    //CS=Void    //AS=Void
        public function EstoqueCorrecaoGradeBarras(fRetorno:Function=null):void
        {
            ro.Invoca('EstoqueCorrecaoGradeBarras', [Sessao.unica.idCorp], fRetorno);
        }
        //ev.result    //CS=Void    //AS=Void
        public function ResetaEmpListas(fRetorno:Function=null):void
        {
            ro.Invoca('ResetaEmpListas', [Sessao.unica.idCorp, Sessao.unica.idEmp], fRetorno);
        }
    }
}
