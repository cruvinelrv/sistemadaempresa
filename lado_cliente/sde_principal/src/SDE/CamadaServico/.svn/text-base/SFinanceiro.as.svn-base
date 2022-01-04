package SDE.CamadaServico
{
    import mx.rpc.events.ResultEvent;
    import Core.Sessao;
    import Core.ConexaoServidor.MyRemoteObject;
    
    public final class SFinanceiro
    {
        private static var _:SFinanceiro;
        public static function get unica():SFinanceiro
        {
            if (_==null) _=new SFinanceiro();
                return _;
        }
        private var ro:MyRemoteObject = new MyRemoteObject('SDE.CamadaServico.SFinanceiro');
    }
}
