package SDE.Enumerador
{
    public final class EMovTipo
    {
        public static function getCampos():Array{return['entrada_cancel','entrada_devolucao','entrada_compra','saida_cancel','saida_devolucao','saida_venda','saida_condi','outros_cancel','outros_reserva','outros_orcamento','outros_pedido','outros_servicos','ambos_cancel','ambos_balan','ambos_aj_es','ambos_aj_gr','nfs_prefeitura','ambos_ajuste_estoque']};
        
        public static const entrada_cancel:String = 'entrada_cancel';
        public static const entrada_devolucao:String = 'entrada_devolucao';
        public static const entrada_compra:String = 'entrada_compra';
        public static const saida_cancel:String = 'saida_cancel';
        public static const saida_devolucao:String = 'saida_devolucao';
        public static const saida_venda:String = 'saida_venda';
        public static const saida_condi:String = 'saida_condi';
        public static const outros_cancel:String = 'outros_cancel';
        public static const outros_reserva:String = 'outros_reserva';
        public static const outros_orcamento:String = 'outros_orcamento';
        public static const outros_pedido:String = 'outros_pedido';
        public static const outros_servicos:String = 'outros_servicos';
        public static const ambos_cancel:String = 'ambos_cancel';
        public static const ambos_balan:String = 'ambos_balan';
        public static const ambos_aj_es:String = 'ambos_aj_es';
        public static const ambos_aj_gr:String = 'ambos_aj_gr';
        public static const nfs_prefeitura:String = 'nfs_prefeitura';
        public static const ambos_ajuste_estoque:String = 'ambos_ajuste_estoque';
        
        public static function valida(value:String):Boolean
        {
            return (getCampos().indexOf(value)>-1);
        }
    }
}
