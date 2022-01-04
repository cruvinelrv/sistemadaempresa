package SDE.CamadaServico
{
    import mx.rpc.events.ResultEvent;
    import Core.Sessao;
    import Core.ConexaoServidor.MyRemoteObject;
    
    public final class SBancoDados
    {
        private static var _:SBancoDados;
        public static function get unica():SBancoDados
        {
            if (_==null) _=new SBancoDados();
                return _;
        }
        private var ro:MyRemoteObject = new MyRemoteObject('SDE.CamadaServico.SBancoDados');
        //ev.result    //CS=Void    //AS=Void
        public function MetodoCorretivo(fRetorno:Function=null):void
        {
            ro.Invoca('MetodoCorretivo', [], fRetorno);
        }
        //ev.result    //CS=Void    //AS=Void
        public function GeraExcecao(index:Number, texto:String, fRetorno:Function=null):void
        {
            ro.Invoca('GeraExcecao', [index, texto], fRetorno);
        }
        //ev.result    //CS=Objeto    //AS=Objeto
        public function Load(index:Number, uuID:Number, fRetorno:Function=null):void
        {
            ro.Invoca('Load', [index, uuID], fRetorno);
        }
        //ev.result    //CS=IList    //AS=Array
        public function ListaClasses(fRetorno:Function=null):void
        {
            ro.Invoca('ListaClasses', [], fRetorno);
        }
        //ev.result    //CS=IList    //AS=Array
        public function Lista(index:Number, tipo:String, fRetorno:Function=null):void
        {
            ro.Invoca('Lista', [index, tipo], fRetorno);
        }
        //ev.result    //CS=Void    //AS=Void
        public function EditaCampo(index:Number, uuID:Number, campo:String, valor:Object, fRetorno:Function=null):void
        {
            ro.Invoca('EditaCampo', [index, uuID, campo, valor], fRetorno);
        }
        //ev.result    //CS=Void    //AS=Void
        public function Deleta(index:Number, uuID:Number, fRetorno:Function=null):void
        {
            ro.Invoca('Deleta', [index, uuID], fRetorno);
        }
        //ev.result    //CS=Void    //AS=Void
        public function removeTodasInstanciasClasse(index:Number, classe:String, fRetorno:Function=null):void
        {
            ro.Invoca('removeTodasInstanciasClasse', [index, classe], fRetorno);
        }
    }
}
