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
    /// Lógica interna para RemoverProduto.xaml
    /// </summary>
    public partial class RemoverProduto : Window
    {
        ProdutoController produtoController;

        public RemoverProduto(Context context)
        {
            produtoController = new ProdutoController(context);
            InitializeComponent();
        }

        private void Button_Remover(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(codigo.Text, out int c) || c <= 0) { MessageBox.Show("Digite um código válido"); }
            else
            {
                string mensagem = produtoController.remover(c);
                MessageBox.Show(mensagem);
                if (mensagem == "Produto removido do estoque") { this.Close(); }
            }
        }
    }
}
