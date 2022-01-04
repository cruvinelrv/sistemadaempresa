package SDE.FachadaServico
{
    import mx.rpc.events.ResultEvent;
    import SDE.Parametro.ParamLoadItem;
    import SDE.Parametro.ParamFiltroItem;
    import SDE.Entidade.Item;
    import SDE.CamadaServico.SItem;
    
    public final class FcdItem
    {
        private static var _:FcdItem;
        public static function get unica():FcdItem
        {
            if (_==null) _=new FcdItem();
                return _;
        }
        //ev.result    //CS=Item    //AS=Item
        public function Load(idEmpresa:Number, idItem:Number, pl:ParamLoadItem, fRetorno:Function=null):void
        {
            SItem.unica.Load(idEmpresa, idItem, pl, fRetorno);
        }
        //ev.result    //CS=IList<Item>    //AS=Array
        public function Pesquisa(idEmpresa:Number, pf:ParamFiltroItem, pl:ParamLoadItem, fRetorno:Function=null):void
        {
            SItem.unica.Pesquisa(idEmpresa, pf, pl, fRetorno);
        }
        //ev.result    //CS=Item    //AS=Item
        public function Novo(idEmpresa:Number, item:Item, codBarras:String, fRetorno:Function=null):void
        {
            SItem.unica.Novo(idEmpresa, item, codBarras, fRetorno);
        }
        //ev.result    //CS=Item    //AS=Item
        public function Atualizar(idEmpresa:Number, itemDados:Item, retorna:Boolean, fRetorno:Function=null):void
        {
            SItem.unica.Atualizar(idEmpresa, itemDados, retorna, fRetorno);
        }
    }
}
