namespace SDE.Enumerador
{
    public enum EModo
    {
        desenvolvimento, suporte, teste, producao
    }

    public enum EBalancoSituacao
    {
        em_andamento, cancelado, efetuado
    }
    public enum EContatoTipo
    {
        fone_fixo, celular, email, msn, skype, fax, ddg, homepage
    }

    public enum EPesTipo
    {
        nao_informado, Fisica, Juridica
    }
    public enum EPesEstCivil
    {
        nao_informado, solteiro, casado, divorciado, viuvo, outros
    }
    public enum EPesSexo
    {
        nao_informado, masculino, feminino
    }
    public enum EItemUnidMed
    {
        UN, KG, M, M2, M3, L, DS, SRV, MX, PC, KIT, JG, CJ, CX, BB, RL, LT, SC, GL
    }
    public enum EItemAliq
    {
        entDentroUF, entForaUF, saiDentroUF, saiForaUF,
        ipi, pis, cofins
    }
    public enum EItemTipo
    {
        produto, servico
    }

    public enum ECxLancamentoSituacao
    {
        aberto, lancado
    }

    public enum ECxLancamentoTipo
    {
        venda, recebimento, entrada, retirada, pagamento
    }

    public enum ECxDiarioSituacao
    {
        aberto_pelo_sistema, aberto, fechado
    }

    public enum ETituloSituacao
    {
        em_aberto, lancado, duplicata_impressa, cheque_repassado
    }

    public enum EMovResumo
    {
        entrada, saida, outros, ambos
    }
    public enum EMovTipo
    {
        entrada_cancel, entrada_devolucao, entrada_compra,
        saida_cancel, saida_devolucao, saida_venda, saida_condi,
        outros_cancel, outros_reserva, outros_orcamento, outros_pedido, outros_servicos,
        ambos_cancel, ambos_balan, ambos_aj_es, ambos_aj_gr, nfs_prefeitura, ambos_ajuste_estoque
    }
    public enum EMovImpressao
    {
        sem_impressao, cupom_fiscal, nfe_produto, orcamento, reserva, nfs_prefeitura, nf_formulario, pedido
    }
    public enum EValorEspecie
    {
        nao_informado, dinheiro, cheque_a_vista, cheque_pre, cartao_credito, prazo,
        cartao_debito, convenio, financeira, reserva, transferencia, haver
    }
    public enum ESituacaoNota
    {
        normal, cancel,
        normal_extemporaneo, cancel_extemporaneo
    }

    public enum EVeiculoTipo
    {
        automovel, caminhao, motocicleta, reboque, semi_reboque, maquina_agricola
    }
    public enum EVeiculoCombustivel
    {
        alcool, gasolina, diesel
    }
    public enum EVeiculoEspecie
    {
        carga, corrida, especial, misto, passsageiro, tracao
    }
    public enum EVeiculoCondicao
    {
        acabado, inacabado, semi_acabado
    }
    public enum EVeiculoCondicaoVIN
    {
        importado, nacional
    }
    public enum EVeiculoPintura
    {
        metalica, solida
    }

    public enum EEnderecoTipo
    {
        residencial_comercial, rural
    }
    public enum EOrdemServicoTipo
    {
        servico, producao, equipamento, veiculo, maq_agricola, maq_pesada
    }

    public enum EOrdemServicoStatus
    {
        nao_iniciada, em_andamento, finalizada, cancelada, reaberta
    }
    public enum EItemTipoIdent
    {
        identificador, grade, lote
    }
    public enum EItemOrigem
    {
        nacional, internacional, internacional_mi
    }
    public enum ECalculoIpiTipo
    {
        percentual, valor_fixo
    }
    public enum ENfeFinalidade
    {
        normal, ajuste, complementar
    }
    public enum ENfeAmbiente
    {
        producao, homologacao
    }
    public enum ENfeFormaPgto
    {
        vista, prazo, outros
    }
    public enum ENfeTipoTransporte
    {
        emitente, destinatario
    }

    public enum ETipoTitulo
    {
        cheque_a_receber, titulo_a_receber, duplicata_a_receber, titulo_a_pagar
    }
    public enum EDmsTipoServico
    {
        prestado, tomado
    }
    public enum EDmsTipoDeclaracao
    {
        com_movimento, sem_movimento
    }
    public enum EDmsSituacao
    {
        normal, retificada
    }

	//vinicius 08out

    public enum ERelatorio
    {
        lista_telefone, ficha_cliente, estoque, lista_preco, espelho_mov, caixa, pis_cofins, extrato_conta_corrente_caixa,

        titulos_receber_pagar, cheque, comissionamento, produtos_vendidos_periodo, agrodefesa, inventario, listagem_balanco, lista_prod_tributo, verificacao_balanco

    }
}