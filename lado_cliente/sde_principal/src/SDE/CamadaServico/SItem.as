package SDE.CamadaServico
{
    import mx.rpc.events.ResultEvent;
    import Core.Sessao;
    import Core.ConexaoServidor.MyRemoteObject;
    import SDE.Parametro.ParamLoadItem;
    import SDE.Parametro.ParamFiltroItem;
    import SDE.Entidade.Item;
    
    public final class SItem
    {
        private static var _:SItem;
        public static function get unica():SItem
        {
            if (_==null) _=new SItem();
                return _;
        }
        private var ro:MyRemoteObject = new MyRemoteObject('SDE.CamadaServico.SItem');
        //ev.result    //CS=Item    //AS=Item
        public function Load(idEmpresa:Number, idItem:Number, pl:ParamLoadItem, fRetorno:Function=null):void
        {
            ro.Invoca('Load', [Sessao.unica.idCorp, idEmpresa, idItem, pl], fRetorno);
        }
        //ev.result    //CS=IList<Item>    //AS=Array
        public function Pesquisa(idEmpresa:Number, pf:ParamFiltroItem, pl:ParamLoadItem, fRetorno:Function=null):void
        {
            ro.Invoca('Pesquisa', [Sessao.unica.idCorp, idEmpresa, pf, pl], fRetorno);
        }
        //ev.result    //CS=Item    //AS=Item
        public function Novo(idEmpresa:Number, item:Item, codBarras:String, fRetorno:Function=null):void
        {
            ro.Invoca('Novo', [Sessao.unica.idCorp, idEmpresa, item, codBarras], fRetorno);
        }
        //ev.result    //CS=Item    //AS=Item
        public function Atualizar(idEmpresa:Number, itemDados:Item, retorna:Boolean, fRetorno:Function=null):void
        {
            ro.Invoca('Atualizar', [Sessao.unica.idCorp, idEmpresa, itemDados, retorna], fRetorno);
        }
    }
}
