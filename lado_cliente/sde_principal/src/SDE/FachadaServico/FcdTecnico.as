package SDE.FachadaServico
{
    import mx.rpc.events.ResultEvent;
    import SDE.CamadaServico.STecnico;
    
    public final class FcdTecnico
    {
        private static var _:FcdTecnico;
        public static function get unica():FcdTecnico
        {
            if (_==null) _=new FcdTecnico();
                return _;
        }
        //ev.result    //CS=Void    //AS=Void
        public function ClientesRemoveTodos(fRetorno:Function=null):void
        {
            STecnico.unica.ClientesRemoveTodos(fRetorno);
        }
        //ev.result    //CS=Void    //AS=Void
        public function ClientesResetaTodos(fRetorno:Function=null):void
        {
            STecnico.unica.ClientesResetaTodos(fRetorno);
        }
        //ev.result    //CS=Void    //AS=Void
        public function ItemRemoverTudo(fRetorno:Function=null):void
        {
            STecnico.unica.ItemRemoverTudo(fRetorno);
        }
        //ev.result    //CS=Void    //AS=Void
        public function ItemRedefineCodUnicoComIdItem(absolutamenteTodos:Boolean, fRetorno:Function=null):void
        {
            STecnico.unica.ItemRedefineCodUnicoComIdItem(absolutamenteTodos, fRetorno);
        }
        //ev.result    //CS=Void    //AS=Void
        public function EstoqueCorrecaoGradeBarras(fRetorno:Function=null):void
        {
            STecnico.unica.EstoqueCorrecaoGradeBarras(fRetorno);
        }
        //ev.result    //CS=Void    //AS=Void
        public function ResetaEmpListas(fRetorno:Function=null):void
        {
            STecnico.unica.ResetaEmpListas(fRetorno);
        }
    }
}
