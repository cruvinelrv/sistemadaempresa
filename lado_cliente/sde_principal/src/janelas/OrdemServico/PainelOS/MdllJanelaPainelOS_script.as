// ActionScript file
import Core.Alerta.AlertaSistema;
import Core.App;

import SDE.Entidade.Cliente;
import SDE.Entidade.OrdemServico;

import mx.collections.ArrayCollection;
import mx.formatters.Formatter;

import org.alivepdf.layout.Format;


	[Bindable] private var arraycOrdemServico:ArrayCollection;
	
	private function init():void
	{
		arraycOrdemServico = App.single.cache.arraycOrdemServico;
		var f:Formatter = new Formatter();
	}
	
	private function filtraBuscaOrdemServico():void
	{
		arraycOrdemServico = App.single.cache.arraycOrdemServico;
		var listaOrdemServicoFiltrada:ArrayCollection = new ArrayCollection(arraycOrdemServico.source);
		var listaTemporaria:Array = [];
		if (cpClientePesquisa.selectedItem != null)
		{
			var cli:Cliente = cpClientePesquisa.selectedItem;
			listaTemporaria = [];
			for each (var os_porCliente:OrdemServico in listaOrdemServicoFiltrada)
			{
				if (os_porCliente.cliente_nome == cli.nome)
					listaTemporaria.push(os_porCliente);
			}
			listaOrdemServicoFiltrada = new ArrayCollection(listaTemporaria);
		}
		if (dfDataPesquisa.text != "")
		{
			listaTemporaria = [];
			for each (var os_porData:OrdemServico in listaOrdemServicoFiltrada)
			{
				var dataPesquisa:String = os_porData.dthrLancamento.substr(0,10);
				AlertaSistema.mensagem("Data: " + dataPesquisa, true);
				if (dataPesquisa == dfDataPesquisa.text)
					listaTemporaria.push(os_porData);
			}
			listaOrdemServicoFiltrada = new ArrayCollection(listaTemporaria);
		}
		if (txtNumOSPesquisa.text != "")
		{
			listaTemporaria = [];
			for each (var os_porNumero:OrdemServico in listaOrdemServicoFiltrada)
			{
				if (os_porNumero.numOS == txtNumOSPesquisa.text)
					listaTemporaria.push(os_porNumero);
			}
			listaOrdemServicoFiltrada = new ArrayCollection(listaTemporaria);
		}
		arraycOrdemServico = listaOrdemServicoFiltrada;
	}
	private function limpaBuscaOrdemServico():void
	{
		cpClientePesquisa.selectedItems.removeAll();
		dfDataPesquisa.text = "";
		txtNumOSPesquisa.text = "";
		filtraBuscaOrdemServico();
	}