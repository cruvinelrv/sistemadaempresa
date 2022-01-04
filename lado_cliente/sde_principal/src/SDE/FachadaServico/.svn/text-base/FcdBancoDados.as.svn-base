package SDE.FachadaServico
{
    import mx.rpc.events.ResultEvent;
    import SDE.CamadaServico.SBancoDados;
    
    public final class FcdBancoDados
    {
        private static var _:FcdBancoDados;
        public static function get unica():FcdBancoDados
        {
            if (_==null) _=new FcdBancoDados();
                return _;
        }
        //ev.result    //CS=Void    //AS=Void
        public function MetodoCorretivo(fRetorno:Function=null):void
        {
            SBancoDados.unica.MetodoCorretivo(fRetorno);
        }
        //ev.result    //CS=Void    //AS=Void
        public function GeraExcecao(index:Number, texto:String, fRetorno:Function=null):void
        {
            SBancoDados.unica.GeraExcecao(index, texto, fRetorno);
        }
        //ev.result    //CS=Objeto    //AS=Objeto
        public function Load(index:Number, uuID:Number, fRetorno:Function=null):void
        {
            SBancoDados.unica.Load(index, uuID, fRetorno);
        }
        //ev.result    //CS=IList    //AS=Array
        public function ListaClasses(fRetorno:Function=null):void
        {
            SBancoDados.unica.ListaClasses(fRetorno);
        }
        //ev.result    //CS=IList    //AS=Array
        public function Lista(index:Number, tipo:String, fRetorno:Function=null):void
        {
            SBancoDados.unica.Lista(index, tipo, fRetorno);
        }
        //ev.result    //CS=Void    //AS=Void
        public function EditaCampo(index:Number, uuID:Number, campo:String, valor:Object, fRetorno:Function=null):void
        {
            SBancoDados.unica.EditaCampo(index, uuID, campo, valor, fRetorno);
        }
        //ev.result    //CS=Void    //AS=Void
        public function Deleta(index:Number, uuID:Number, fRetorno:Function=null):void
        {
            SBancoDados.unica.Deleta(index, uuID, fRetorno);
        }
        //ev.result    //CS=Void    //AS=Void
        public function removeTodasInstanciasClasse(index:Number, classe:String, fRetorno:Function=null):void
        {
            SBancoDados.unica.removeTodasInstanciasClasse(index, classe, fRetorno);
        }
    }
}
