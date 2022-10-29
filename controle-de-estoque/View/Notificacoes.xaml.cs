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
    /// Lógica interna para Notificacoes.xaml
    /// </summary>
    public partial class Notificacoes : Window
    {
        Context context;
        ConfiguracaoController configuracaoController;
        ProdutoController produtoController;

        public Notificacoes(Context context)
        {
            this.context = context;

            configuracaoController = new ConfiguracaoController(context);
            produtoController = new ProdutoController(context);

            InitializeComponent();

            notificacoes.Items.Clear();
            notificacoes.ItemsSource = produtoController.emBaixa(configuracaoController.obterConfiguracao());

        }

        private void Button_Atualizar(object sender, RoutedEventArgs e)
        {
            notificacoes.ItemsSource = produtoController.emBaixa(configuracaoController.obterConfiguracao());
        }
    }
}
