// ActionScript file//
import Core.Alerta.AlertaSistema;

import SDE.Entidade.Cliente;
import SDE.Entidade.Cx_Lancamento;

import mx.core.Application;
import mx.managers.PopUpManager;


	private function change_cpCliente():void
	{
		if (mov==null)
			return;
		
		var c:Cliente = cliente_selecionado;
		if (c==null)
			c=clienteConsumidor;
		mov.idCliente = c.id;
		mov.cliente_nome = c.nome;
		mov.cliente_cpf = c.cpf_cnpj;
		
		cmbClienteEndereco_Faturamento.idCliente = c.id;
		cmbClienteEndereco_Cobranca.idCliente = c.id;
		cmbClienteEndereco_Entrega.idCliente = c.id;
		cmbEndereco_change();
		
		if (arraycLancamentosCaixa && arraycLancamentosCaixa.length > 0)
		{
			for each (var cxL:Cx_Lancamento in arraycLancamentosCaixa)
			{
				cxL.idClientePagador = cliente_selecionado.id;
			}
		}
	}
	
	private function change_cpVendedor():void
	{
		if (mov==null)
			return;
		var c:Cliente = cpVendedor.selectedItem as Cliente;
		mov.idClienteFuncionarioVendedor = c.id;
		vendedor_selecionado = c;
	}
	
	private function cmbClienteContato_change():void
	{
		mov.cliente_contato = cmbClienteContato.selectedLabel;
	}
	
	private function cmbEndereco_change():void
	{
		mov.cliente_endereco_faturamento = cmbClienteEndereco_Faturamento.selectedLabel;
		mov.cliente_endereco_cobranca = cmbClienteEndereco_Cobranca.selectedLabel;
		mov.cliente_endereco_entrega = cmbClienteEndereco_Entrega.selectedLabel;
	}
	
	private function sistema_joga_dados_cliente_no_popup():void
	{
		popupDadosCliente_nome.text = mov.cliente_nome;
		popupDadosCliente_cpf.text = mov.cliente_cpf;
		popupDadosCliente_contato.text = mov.cliente_contato;
		popupDadosCliente_endereco_faturamento.text = mov.cliente_endereco_faturamento;
		popupDadosCliente_endereco_cobranca.text = mov.cliente_endereco_cobranca;
		popupDadosCliente_endereco_entrega.text = mov.cliente_endereco_entrega;
	}
	
	private function popupDadosCliente_mostra():void
	{
		PopUpManager.addPopUp(popupDadosCliente, Application.application.gerenteJanelas, true);
		PopUpManager.centerPopUp(popupDadosCliente);
		sistema_joga_dados_cliente_no_popup();
	}
	
	private function popupDadosCliente_cancela():void
	{
		sistema_joga_dados_cliente_no_popup();
		PopUpManager.removePopUp(popupDadosCliente);
	}
	
	private function popupDadosCliente_salva():void
	{
		mov.cliente_nome = popupDadosCliente_nome.text;
		mov.cliente_cpf = popupDadosCliente_cpf.text;
		mov.cliente_contato = popupDadosCliente_contato.text;
		mov.cliente_endereco_faturamento = popupDadosCliente_endereco_faturamento.text;
		mov.cliente_endereco_cobranca = popupDadosCliente_endereco_cobranca.text;
		mov.cliente_endereco_entrega = popupDadosCliente_endereco_entrega.text;
				
		PopUpManager.removePopUp(popupDadosCliente);
	}