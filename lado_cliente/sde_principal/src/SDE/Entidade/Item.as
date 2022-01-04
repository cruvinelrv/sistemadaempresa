package SDE.Entidade
{
    [Bindable]
    [RemoteClass(alias='SDE.Entidade.Item')]
    public final class Item
    {
        public static function get CLASSE():String{return 'Item';}
        public static function getCampos():Array{return['id','idSecao','idMarca','secao','grupo','subgrupo','marca','modelo','rfUnica','rfAuxiliar','nome','nomeEtiqueta','complAplic','obs','classificacaoFiscal','locacao1','locacao2','locacao3','locacao4','locacao5','locacao6','locacao7','locacao8','locacao9','locacao10','desuso','rfPeso','unidMed','tipo','origem','__ie','__listaIE','__movimentacoes','__fornecedores','__estoques','tipoIdent']};
        
        public static var campo_id:String = 'id';
        public static var campo_idSecao:String = 'idSecao';
        public static var campo_idMarca:String = 'idMarca';
        public static var campo_secao:String = 'secao';
        public static var campo_grupo:String = 'grupo';
        public static var campo_subgrupo:String = 'subgrupo';
        public static var campo_marca:String = 'marca';
        public static var campo_modelo:String = 'modelo';
        public static var campo_rfUnica:String = 'rfUnica';
        public static var campo_rfAuxiliar:String = 'rfAuxiliar';
        public static var campo_nome:String = 'nome';
        public static var campo_nomeEtiqueta:String = 'nomeEtiqueta';
        public static var campo_complAplic:String = 'complAplic';
        public static var campo_obs:String = 'obs';
        public static var campo_classificacaoFiscal:String = 'classificacaoFiscal';
        public static var campo_locacao1:String = 'locacao1';
        public static var campo_locacao2:String = 'locacao2';
        public static var campo_locacao3:String = 'locacao3';
        public static var campo_locacao4:String = 'locacao4';
        public static var campo_locacao5:String = 'locacao5';
        public static var campo_locacao6:String = 'locacao6';
        public static var campo_locacao7:String = 'locacao7';
        public static var campo_locacao8:String = 'locacao8';
        public static var campo_locacao9:String = 'locacao9';
        public static var campo_locacao10:String = 'locacao10';
        public static var campo_desuso:String = 'desuso';
        public static var campo_rfPeso:String = 'rfPeso';
        public static var campo_unidMed:String = 'unidMed';
        public static var campo_tipo:String = 'tipo';
        public static var campo_origem:String = 'origem';
        public static var campo___ie:String = '__ie';
        public static var campo___listaIE:String = '__listaIE';
        public static var campo___movimentacoes:String = '__movimentacoes';
        public static var campo___fornecedores:String = '__fornecedores';
        public static var campo___estoques:String = '__estoques';
        public static var campo_tipoIdent:String = 'tipoIdent';
        public function Item(obj:Object=null){if (obj==null)return;for each(var campo:String in getCampos())this[campo]=obj[campo];}        public function injeta(o:*):void{for each (var campo:String in Item.getCampos()){this[campo]=o[campo];}}
        public function clone():Item{return new Item(this);}
        public function toString():String
        {
            return '[Item '+id+']';
        }
        public var id:Number = 0;
        public var idSecao:Number = 0;
        public var idMarca:Number = 0;
        public var secao:String = '';
        public var grupo:String = '';
        public var subgrupo:String = '';
        public var marca:String = '';
        public var modelo:String = '';
        public var rfUnica:String = '';
        public var rfAuxiliar:String = '';
        public var nome:String = '';
        public var nomeEtiqueta:String = '';
        public var complAplic:String = '';
        public var obs:String = '';
        public var classificacaoFiscal:String = '';
        public var locacao1:String = '';
        public var locacao2:String = '';
        public var locacao3:String = '';
        public var locacao4:String = '';
        public var locacao5:String = '';
        public var locacao6:String = '';
        public var locacao7:String = '';
        public var locacao8:String = '';
        public var locacao9:String = '';
        public var locacao10:String = '';
        public var desuso:Boolean = false;
        public var rfPeso:Number = 0;
        public var unidMed:String = 'UN';
        public var tipo:String = 'produto';
        public var origem:String = 'nacional';
        public var __ie:ItemEmp = null;
        public var __listaIE:Array = null;
        public var __movimentacoes:Array = null;
        public var __fornecedores:Array = null;
        public var __estoques:Array = null;
        public var tipoIdent:String = 'identificador';
    }
}
