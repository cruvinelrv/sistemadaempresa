package SDE.Enumerador
{
    public final class ECxLancamentoTipo
    {
        public static function getCampos():Array{return['venda','recebimento','entrada','retirada','pagamento']};
        
        public static const venda:String = 'venda';
        public static const recebimento:String = 'recebimento';
        public static const entrada:String = 'entrada';
        public static const retirada:String = 'retirada';
        public static const pagamento:String = 'pagamento';
        
        public static function valida(value:String):Boolean
        {
            return (getCampos().indexOf(value)>-1);
        }
    }
}
