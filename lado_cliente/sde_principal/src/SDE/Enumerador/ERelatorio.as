package SDE.Enumerador
{
    public final class ERelatorio
    {
        public static function getCampos():Array{return['lista_telefone','ficha_cliente','estoque','lista_preco','espelho_mov','caixa','pis_cofins','extrato_conta_corrente_caixa','titulos_receber_pagar','cheque','comissionamento','produtos_vendidos_periodo','agrodefesa','inventario','listagem_balanco','lista_prod_tributo','verificacao_balanco']};
        
        public static const lista_telefone:String = 'lista_telefone';
        public static const ficha_cliente:String = 'ficha_cliente';
        public static const estoque:String = 'estoque';
        public static const lista_preco:String = 'lista_preco';
        public static const espelho_mov:String = 'espelho_mov';
        public static const caixa:String = 'caixa';
        public static const pis_cofins:String = 'pis_cofins';
        public static const extrato_conta_corrente_caixa:String = 'extrato_conta_corrente_caixa';
        public static const titulos_receber_pagar:String = 'titulos_receber_pagar';
        public static const cheque:String = 'cheque';
        public static const comissionamento:String = 'comissionamento';
        public static const produtos_vendidos_periodo:String = 'produtos_vendidos_periodo';
        public static const agrodefesa:String = 'agrodefesa';
        public static const inventario:String = 'inventario';
        public static const listagem_balanco:String = 'listagem_balanco';
        public static const lista_prod_tributo:String = 'lista_prod_tributo';
        public static const verificacao_balanco:String = 'verificacao_balanco';
        
        public static function valida(value:String):Boolean
        {
            return (getCampos().indexOf(value)>-1);
        }
    }
}
