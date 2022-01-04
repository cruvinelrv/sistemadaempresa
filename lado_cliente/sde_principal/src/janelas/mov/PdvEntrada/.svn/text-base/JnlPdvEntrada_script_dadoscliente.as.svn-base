// ActionScript file
import SDE.Entidade.Mov;

import mx.core.Application;
import mx.managers.PopUpManager;

	
	private function cmbClienteContato_change():void
	{
		mov.cliente_contato = cmbClienteContato.selectedLabel;
	}
	
	private function cmbEndereco_change():void
	{
		mov.cliente_nome = clienteEmpresa.apelido_razsoc;
		mov.cliente_cpf = clienteEmpresa.cpf_cnpj;
		mov.cliente_contato = cmbClienteContato.selectedLabel;
		mov.cliente_endereco_faturamento = cmbClienteEndereco_Faturamento.selectedLabel;
		mov.cliente_endereco_cobranca = cmbClienteEndereco_Cobranca.selectedLabel;
		mov.cliente_endereco_entrega = cmbClienteEndereco_Entrega.selectedLabel;
	}
	
	private function sistema_joga_dados_cliente_no_popup():void
	{
		cmbEndereco_change();
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