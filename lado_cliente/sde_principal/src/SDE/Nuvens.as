package SDE
{
    import SDE.CamadaNuvem.*;
    
    public final class Nuvens
    {
        private static var _:Nuvens = new Nuvens();
        public static function get instancia():Nuvens{return _;}
        
        public var cache:NuvemCache;
        public var listagem:NuvemListagem;
        public var modificacoes:NuvemModificacoes;
        public var notificacoes:NuvemNotificacoes;
        
        public function Inicializa():void
        {
            cache = new NuvemCache();
            listagem = new NuvemListagem();
            listagem.Inicializa();
            modificacoes = new NuvemModificacoes();
            notificacoes = new NuvemNotificacoes();
        }
        public function Fecha():void
        {
            notificacoes.Fecha();
        }
    }
}
