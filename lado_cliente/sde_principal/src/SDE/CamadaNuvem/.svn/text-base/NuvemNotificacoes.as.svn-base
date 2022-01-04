package SDE.CamadaNuvem
{
    import mx.rpc.events.ResultEvent;
    import Core.App;
    import Core.Sessao;
    import Core.Alerta.AlertaSistema;
    import Core.ConexaoServidor.MyRemoteObject;
    import SDE.Entidade.*;
    import SDE.Outros.*;
    import Core.Utils.MeuFiltroWhere;
    import flash.utils.setInterval;
    import flash.utils.clearInterval;
    import mx.controls.Alert;
    
    public final class NuvemNotificacoes
    {
        private var ro:MyRemoteObject = new MyRemoteObject('SDE.CamadaNuvem.NuvemNotificacoes');
        private var ultima:Number = 0;
        private var TEMPO_INTERVALO_ATUALIZA:Number = 5000;
        private var idInterval:uint = 0;
        
        public function NuvemNotificacoes()
        {
            idInterval = setInterval(Lista_Notificacoes, TEMPO_INTERVALO_ATUALIZA);
        }
        public function Fecha():void
        {
            clearInterval(idInterval);
        }
        public function Lista_Notificacoes(fRetorno:Function=null):void
        {
            if (!Sessao.unica.isLogado)
                return;
            ro.Invoca('Lista_Notificacoes', [Sessao.unica.idCorp, ultima],
                function(retorno:Array):void
                {
                    for each(var at:Atualizacao in retorno)
                    {
                        var todos:Array = App.single.cache['array'+at.classe];
                        ultima = at.idAtualizacao;
                        var filtro:MeuFiltroWhere = new MeuFiltroWhere(todos).andEquals(at.idObj);
                        var filtrados:Array = filtro.getResultadoArraySimples();
                        //queremos que o resultado seja 1
                        if (filtrados.length==0)
                        {
                            if (at.ehInsercao)
                            {
                                todos.push(at.obj);
                                ultima = at.idAtualizacao;
                                AlertaSistema.mensagem('INS ultima agora é '+ultima+' de um filtro de '+filtrados.length +' idObj: '+at.idObj, true);
                            }
                            else
                            {
                                AlertaSistema.mensagem('DEL ZERO ultima agora é '+ultima+' de um filtro de '+filtrados.length +' idObj: '+at.idObj, true);
                            }
                        }
                        else if (filtrados.length==1)
                        {
                            var objAtualizar:Object = filtrados[0];
                            for(var i:int=0; i<todos.length; i++)
                                if (todos[i].id == at.idObj)
                                {
                                    if (at.ehInsercao)
                                        todos[i] = at.obj;
                                    else
                                    {
                                        todos.splice(i,1);
                                        AlertaSistema.mensagem('DEL UM ultima agora é '+ultima+' de um filtro de '+filtrados.length +' idObj: '+at.idObj, true);
                                    }
                                    break;
                                }
                            ultima = at.idAtualizacao;
                            AlertaSistema.mensagem('UPD ultima agora é '+ultima+' de um filtro de '+filtrados.length, true);
                        }
                        else
                            Alert.show( ultima+' | foram encontradas '+filtrados.length+' instancias no caché local\n'+at.classe+' id: '+at.idObj, 'erro!');
                    }
                    if (fRetorno != null)
                        fRetorno();
                }
            );
        }
        
    }
}
