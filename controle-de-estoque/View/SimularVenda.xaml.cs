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
    /// Lógica interna para SimularVenda.xaml
    /// </summary>
    public partial class SimularVenda : Window
    {
        VendaController vendaController;
        Venda venda;

        public SimularVenda(Context context)
        {
            vendaController = new VendaController(context);
            venda = vendaController.simularVenda();

            InitializeComponent();

            CarrinhoDataGrid.Items.Clear();
            CarrinhoDataGrid.ItemsSource = vendaController.listar(venda);
        }

        private void AtualizarCarrinho()
        {
            CarrinhoDataGrid.ItemsSource = null;
            CarrinhoDataGrid.ItemsSource = vendaController.listar(venda);
            total.Text = "Valor total: " + vendaController.valorTotal(venda).ToString("C");
        }

        private void Button_Adicionar(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(codigo.Text, out int c) || c <= 0) { MessageBox.Show("Digite um código válido"); }
            if (!int.TryParse(quantidade.Text, out int q) || q <= 0) { MessageBox.Show("Digite uma quantidade válida"); }
            else
            {
                string mensagem = vendaController.adicionarProduto(venda, c, q);
                if (mensagem != "") { MessageBox.Show(mensagem); }
            }
            AtualizarCarrinho();
        }

        private void Button_Remover(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(codigo.Text, out int c) || c <= 0) { MessageBox.Show("Digite um código válido"); }
            if (!int.TryParse(quantidade.Text, out int q) || q <= 0) { MessageBox.Show("Digite uma quantidade válida"); }
            else
            {
                string mensagem = vendaController.removerProduto(venda, c, q);
                if(mensagem != "") { MessageBox.Show(mensagem); }
            }
            AtualizarCarrinho();
        }

        private void Button_Finalizar(object sender, RoutedEventArgs e)
        {
            string mensagem = vendaController.finalizarVenda(venda);
            MessageBox.Show(mensagem);
            if(mensagem == "Venda finalizada") { this.Close(); }
        }
    }
}
