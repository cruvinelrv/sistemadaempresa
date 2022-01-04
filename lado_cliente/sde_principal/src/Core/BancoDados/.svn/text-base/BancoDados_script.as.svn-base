// ActionScript file
import Core.App;

import SDE.FachadaServico.FcdBancoDados;
import SDE.FachadaServico.FcdItem;
import SDE.Outros.Objeto;

import flash.events.Event;

import mx.controls.AdvancedDataGrid;
import mx.controls.Alert;
import mx.core.ScrollPolicy;
import mx.events.ListEvent;


	private var fcd:FcdBancoDados = new FcdBancoDados();
	private var fcdI:FcdItem = new FcdItem();
	private var idBase:Number = 0;
	private var historico:Array = [];
	
	[Bindable] private var instanciaEditando:Objeto = null;
	
	private function create():void
	{
		lista();
		gridInstancia.addEventListener('editar', acoesEditar);
		gridInstancia.addEventListener('setnull', acoesSetNull);
		limpa();
	}
	private function limpa():void
	{
		cbTranca.selected = false;
		txtExcecao.text="";
	}
	
	private function lista():void
	{
		fcd.ListaClasses(
			function(retorno:Array):void
			{
				cmbClasses.dataProvider = retorno;
			}
		);
		
	}
	
	private function buscaInstancias():void
	{
		idBase = nsIdBase.value;
		var classe:String = cmbClasses.selectedLabel;
		
		var gridInstancias:AdvancedDataGrid = new AdvancedDataGrid();
		gridInstancias.addEventListener(ListEvent.CHANGE, 
			function(ev:ListEvent):void
			{
				var uuID:String = ev.itemRenderer.data["uuID"];
				detalhaInstancia(classe, uuID);
			}
		);
		
		vbPlaceHolderInstancias.removeAllChildren();
		vbPlaceHolderProfundidade.removeAllChildren();
		vbPlaceHolderInstancias.addChild(gridInstancias);
		
		fcd.Lista(idBase, classe,
		
			function(retorno:Array):void
			{
				gridInstancias.dataProvider = retorno;
				
				lblQtdInstancias.text = "INSTÂNCIAS ("+retorno.length+")";
				
				var arInstancias:Array = [];
				//apenas encurtar
				var g:AdvancedDataGrid = gridInstancias;
				//grid.width=990;
				g.percentWidth = 100;
				g.horizontalScrollPolicy = ScrollPolicy.ON;
				g.percentHeight=100;
				g.rowCount=10;
				//g.styleName="w2dtgrVerde";
				
				//vbPlaceHolderInstancias.removeAllChildren();
				historico[classe] = [];
				
				for each (var o:Objeto in retorno)
				{
					var a:Array = [];
					historico[classe][o.uuID] = o.valor;
					a["uuID"] = o.uuID;
					for each (var o2:Objeto in o.valor.campos)
					{
						if (o2.campo.indexOf("__")!=0 && o2.valor != null)
							a[o2.campo] = o2.valor;
					}
					arInstancias.push(a);
				}
				g.dataProvider = arInstancias;
				
				//vbPlaceHolderInstancias.addChild(grid);
				limpa();
			}
			
		);
	}
	
	private function detalhaInstancia(classe:String, uuID:String):void
	{
		instanciaEditando = historico[classe][uuID];
		gridInstancia.dataProvider = instanciaEditando.campos;
	}
	
	
	
	
	
	
	/** -  - **/
	
	private function deletaInstancia():void
	{
		fcd.Deleta(idBase, instanciaEditando.uuID,
			function():void
			{
				for each (var o:Objeto in instanciaEditando.campos)
				{
					o.valor = "REMOVIDO";
					o.classe = "REMOVIDO";
					o.campo = "REMOVIDO";
				}
			}
		);
		limpa();
	}
	
	private function removeTodasInstanciasClasse():void
	{
		var classe:String = cmbClasses.selectedLabel;
		fcd.removeTodasInstanciasClasse(idBase, classe,
			function():void
			{
				buscaInstancias();
			}
		);
		limpa();
	}
	
	
	/** -  - **/
	
	private function acoesEditar(ev:Event):void
	{
		var oCampo:Objeto = ev.target.data;
		
		//não edita campo primitivo
		if (oCampo.uuID>0)
		{
			Alert.show('editar apenas campos primitivos');
			return;
		}
		fcd.EditaCampo(idBase, instanciaEditando.uuID, oCampo.campo, oCampo.valor);
		
		/*
		instanciaEditando.uuID;
		oCampo.campo;
		oCampo.uuID;
		oCampo.valor;
		/**/
		limpa();
	}
	
	private function acoesSetNull(ev:Event):void
	{
		var oCampo:Objeto = ev.target.data;
		
		if ( new String("Int32_Int16_Double").indexOf(oCampo.classe) > -1 )
		{
			Alert.show( "numeros não podem ser nulos" );
			return;
		}
		else if (oCampo.classe == "Boolean")
			oCampo.valor = false;
		else
			oCampo.valor = null;
		
		fcd.EditaCampo(idBase, instanciaEditando.uuID, oCampo.campo, oCampo.valor);
		limpa();
	}
	
	private function metodoCorretivo():void
	{
		fcd.MetodoCorretivo( function():void{ Alert.show('ok'); } );
		limpa();
		//btnMetodoCorretivo.visible = false;
	}
	
	private function geraExcecao():void
	{
		var texto:String = txtExcecao.text;
		fcd.GeraExcecao(
			idBase, texto,
			function():void{ Alert.show('ok'); }
		);
		limpa();
	}

	
	
	
	
	
	
	
	
	
	
	
	
	