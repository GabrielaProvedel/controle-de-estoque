using controle_de_estoque.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace controle_de_estoque.Controller
{
    public class ConfiguracaoController
    {
        private readonly Context context;

        public ConfiguracaoController(Context context)
        {
            this.context = context;
        }

        public Configuracao obterConfiguracao()
        {
            if (!context.Configuracoes.Any<Configuracao>()) { return new Configuracao(); }
            else { return context.Configuracoes.First<Configuracao>(); }
        }

        public string alterarQuantidadeBaixa(Configuracao c, int q)
        {
            try
            {
                c.quantidadeBaixa = q;
                context.Configuracoes.Update(c);
                context.SaveChanges();
                return "Configurações salvas com sucesso!";
            }
            catch
            {
                return "Não foi possível salvar as novas configurações";
            }
        }

        public int obterQuantidade(Configuracao c)
        {
            return c.quantidadeBaixa;
        }
    }
}
