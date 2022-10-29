using controle_de_estoque.Controller;
using controle_de_estoque.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace controle_de_estoque.View
{
    /// <summary>
    /// Lógica interna para AlterarConfiguracoes.xaml
    /// </summary>
    public partial class AlterarConfiguracoes : Window
    {
        ConfiguracaoController configuracaoController;
        Configuracao configuracao;

        public AlterarConfiguracoes(Context context)
        {
            configuracaoController = new ConfiguracaoController(context);
            configuracao = configuracaoController.obterConfiguracao();

            InitializeComponent();
            quantidade.Text = configuracaoController.obterQuantidade(configuracao).ToString();
        }

        private void Button_Salvar(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(quantidade.Text, out int q) || q <= 0) { MessageBox.Show("A quantidade baixa digitada não é válida"); }
            string mensagem = configuracaoController.alterarQuantidadeBaixa(configuracao, q);
            this.Close();
        }
    }
}
