
	import Core.Alerta.AlertaSistema;
	import Core.Ev.EvRetornaArray;
	import Core.Sessao;
	
	import SDE.Entidade.Balanco;
	import SDE.Entidade.BalancoItem;
	import SDE.Entidade.Item;
	import SDE.Entidade.ItemEmpEstoque;
	import SDE.FachadaServico.FcdBalanco;
	import SDE.FachadaServico.FcdMov;
	
	private var fcdB:FcdBalanco = new FcdBalanco();
	private var fcdM:FcdMov = new FcdMov();
	[Bindable] private var balanco:Balanco = null;
	
	
	
	
	private var idBalanco:Number=0;
	
	private var arItens:Array;
	private var atualItem:Item = null;
	private var atualIEE:ItemEmpEstoque=null;
	
	
	
	
	private function create():void
	{
		listaBalancos();
	}
	private function gridItensCreate():void
	{
		gridItens.addEventListener('deleteRow', deleteRowHandler);
	}
	
	private function listaBalancos():void
	{
		currentState="todos";
		fcdB.Lista(
			function(retorno:Array):void
			{
				var arBalancos:Array = [];
				retorno.sortOn("id");
				for each(var bal:Balanco in retorno)
				{
					var o:Object = {};
					o.balanco = bal;
					o.nome = "Balanço "+bal.id+" em "+bal.dtInicio;
					o.situacao = (bal.fechado) ? "fechado" : "aberto";
					arBalancos.push(o);
				}
				gridBalancos.dataProvider = arBalancos;
				gridBalancos.setFocus();
			}
		);
	}
	
	private function balancoNovo():void
	{
		idBalanco = 0;
		currentState='atual';
		panel1.title = "Novo Balanço";
		arItens = [];
		gridItens.dataProvider = arItens;
	}
	
	
	
	
	private function balancoAbrir():void
	{
		if (gridBalancos.selectedIndex==-1)
			return;
		
		balanco = gridBalancos.selectedItem.balanco;
		
		fcdB.Load(balanco.id,
			function(retorno:Balanco):void
			{
				idBalanco = retorno.id;
				//balanco = retorno;
				//
				arItens = [];
				retorno.__itens.sortOn("id");
				for each(var bi:BalancoItem in retorno.__itens)
				{
					var o:Object = {};
					//o.item = bal;
					o.idBI = bi.id;
					o.idItem = bi.idItem;
					o.nome = "??";
					o.qtdAntiga = "??";
					o.qtd = bi.qtd;
					o.ident = "??";
					arItens.push(o);
				}
				currentState='atual';
				gridItens.dataProvider = arItens;
				panel1.title = "Balanço "+balanco.id+" em "+balanco.dtInicio;
			}
		);
	}
	private function balancoConcluir():void
	{
		var bal:Balanco = gridBalancos.selectedItem.balanco;
		if (bal.fechado)
			return;
		
		fcdB.Finaliza(bal.id,
			function():void
			{
				//AlertaSistema.mensagem("finalizado\rmas não alterou estoques");
				listaBalancos();
			}
		);
	}
	
	
	
	
	
	private function KDown(ev:KeyboardEvent):void
	{
		if (ev.keyCode!=Keyboard.ENTER)
			return;
		
		if (ev.currentTarget==txtBarras)
			_defineProduto();
		else if (ev.currentTarget==nsQtd)
			_registraItemBalanco();
	}
	
	private function _defineProduto():void
	{
		if (txtBarras.text.length<3)
			return;
		
		FcdMov.unica.LoadItemEstoque(
			Sessao.unica.idEmp,
			txtBarras.text,
			function(retorno:Array):void
			{
				if (retorno==null)
				{
					AlertaSistema.mensagem( "Não encontrado" );
					txtBarras.setFocus();
					txtBarras.setSelection(0, txtBarras.length);
				}
				else
				{
					atualItem = retorno[0];
					atualIEE = retorno[1];
					lblAtualItem.text = atualItem.nome+", "+atualIEE.identificador;
					
					nsQtd.value=1;
					txtBarras.text='';
					nsQtd.setFocus();
				}
				//AlertaSistema.mensagem( "Resultado:\ritem: "+atualItem.nome );
				
			}
		);
	}
	
	private function retornaEstoque(ev:EvRetornaArray):void
	{
		if (ev.retorno==null)
		{
			AlertaSistema.mensagem( "Não encontrado" );
			cpEstoque.seleciona();
		}
		else
		{
			atualItem	= ev.retorno[0];
			atualIEE	= ev.retorno[1];
			lblAtualItem.text = atualItem.nome+", "+atualIEE.identificador;
			
			nsQtd.value=1;
			
			txtBarras.text='';
			nsQtd.setFocus();
		}
	}
	
	
	
	
	
	private function _registraItemBalanco():void
	{
		if (atualItem==null)
		{
			AlertaSistema.mensagem( "Você precisa escolher um produto!" );
			return;
		}
		
		
		var bi:BalancoItem = new BalancoItem();
		bi.idBalanco = idBalanco;
		bi.idIEE = atualIEE.id;
		bi.idItem = atualIEE.idItem;
		bi.qtd = nsQtd.value;
		
		//vamos registrar no servidor, isso irá retornar idB + idBI
		fcdB.Registra(bi,
			function(retorno:BalancoItem):void
			{
				if (retorno==null)
				{
					AlertaSistema.mensagem("Este balanço já foi finalizado");
					return;
				}
				//se esse Balanço era "Novo", entao agora ele foi registrado
				idBalanco = retorno.idBalanco;
				if (balanco==null)
				{
					FcdBalanco.unica.Load(idBalanco,
						function(retorno2:Balanco):void
						{
							balanco = retorno2;
						}
					);
				}
				
				//já que esse BalançoItem era "Novo", entao agora ele foi registrado
				var idBI:Number = retorno.id;
				//populamos os dados do item (que já haviam sido trazidos antes)
				
				var o:Object = {};
				//não reutilizaremos os dados comentados, mas nesse momento, essas instancias poderiam ser assinadalas ao item do array
				//o.bi = ****;
				//o.item
				//o.iee
				o.idBI = idBI;
				o.idItem = atualItem.id;
				o.nome = atualItem.nome;
				o.qtdAntiga = atualIEE.qtd;
				o.qtd = bi.qtd;
				o.ident = atualIEE.identificador;
				arItens.push(o);
				//atualizamos a grid
				gridItens.dataProvider = arItens;
				AlertaSistema.mensagem( bi.qtd+" "+atualItem.unidMed+" lançados" );
				//limpamos a tela
				atualItem=null;
				lblAtualItem.text = "";
				nsQtd.value = 0;
				txtBarras.setFocus();
			}
		);
		
	}
    private function deleteRowHandler(event:Event):void
    {
    	if (balanco.fechado)
    	{
    		AlertaSistema.mensagem("Este Balanço já foi finalizado");
    		return;
    	}
    	AlertaSistema.mensagem("Removido");
    	
    	var obj:Object = event.target.data;
    	var idBI:Number = obj.idBI;
    	//remove da grid
    	var pos:int = arItens.indexOf(obj);
    	arItens.splice(pos, 1);
    	//remove do servidor
    	if (idBI>0)
    		fcdB.Remove(idBI);
    	//atualiza o dataprovider
    	gridItens.dataProvider = arItens;
    }
	
	/**/