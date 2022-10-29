using controle_de_estoque.Controller;
using controle_de_estoque.Model;
using controle_de_estoque.View;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace controle_de_estoque
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly Context context;

        ProdutoController produtoController;
        CategoriaController categoriaController;

        CadastrarProduto cadastrarProduto;
        EditarProduto editarProduto;
        RemoverProduto removerProduto;
        CadastrarCategoria cadastrarCategoria;
        RemoverCategoria removerCategoria;
        SimularVenda simularVenda;
        AlterarConfiguracoes alterarConfiguracoes;
        Notificacoes notificacoes;

        public MainWindow(Context context)
        {
            this.context = context;
            produtoController = new ProdutoController(context);
            categoriaController = new CategoriaController(context);

            InitializeComponent();

            EstoqueDataGrid.Items.Clear();
            EstoqueDataGrid.ItemsSource = produtoController.listar();

            filtro.Items.Clear();
            List<Categoria> lista = categoriaController.listar();
            foreach (Categoria i in lista)
            {
                filtro.Items.Add(i.NomeCategoria);
            }
            filtro.Items.Add("Todas as categorias");

            notificacoes = new Notificacoes(context);
            notificacoes.Show();
            notificacoes.Focus();
        }

        private void Atualizar()
        {
            busca.Text = "";
            filtro.SelectedItem = null;

            EstoqueDataGrid.ItemsSource = null;
            EstoqueDataGrid.ItemsSource = produtoController.listar();

            filtro.Items.Clear();
            List<Categoria> lista = categoriaController.listar();
            foreach (Categoria i in lista)
            {
                filtro.Items.Add(i.NomeCategoria);
            }
            filtro.Items.Add("Todas as categorias");
        }

        private void Button_Atualizar(object sender, RoutedEventArgs e)
        {
            Atualizar();
        }

        private void Button_Busca(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(busca.Text, out int codigo) || codigo <= 0) { MessageBox.Show("Digite um código válido"); }
            else 
            {
                EstoqueDataGrid.ItemsSource = produtoController.buscar(codigo); 
            }

            filtro.SelectedItem = null;
        }

        private void Button_Filtro(object sender, RoutedEventArgs e)
        {
            if (filtro.SelectedItem == null) { MessageBox.Show("Selecione uma categoria"); }

            else
            {
                string categoria = filtro.SelectedItem.ToString();
                EstoqueDataGrid.ItemsSource = produtoController.filtrar(categoria);
            }

            busca.Text = "";
        }

        private void Button_CadastrarProduto(object sender, RoutedEventArgs e)
        {
            if (JanelaAberta(cadastrarProduto)) { cadastrarProduto.Focus(); }
            else
            {
                cadastrarProduto = new CadastrarProduto(context);
                cadastrarProduto.Show();
            }
        }

        private void Button_EditarProduto(object sender, RoutedEventArgs e)
        {
            if (JanelaAberta(editarProduto)) { editarProduto.Focus(); }
            else
            {
                editarProduto = new EditarProduto(context);
                editarProduto.Show();
            }
        }

        private void Button_RemoverProduto(object sender, RoutedEventArgs e)
        {
            if (JanelaAberta(removerProduto)) { removerProduto.Focus(); }
            else
            {
                removerProduto = new RemoverProduto(context);
                removerProduto.Show();
            }
        }

        private void Button_CadastrarCategoria(object sender, RoutedEventArgs e)
        {
            if (JanelaAberta(cadastrarCategoria)) { cadastrarCategoria.Focus(); }
            else
            {
                cadastrarCategoria = new CadastrarCategoria(context);
                cadastrarCategoria.Show();
            }
        }

        private void Button_RemoverCategoria(object sender, RoutedEventArgs e)
        {
            if (JanelaAberta(removerCategoria)) { removerCategoria.Focus(); }
            else
            {
                removerCategoria = new RemoverCategoria(context);
                removerCategoria.Show();
            }
        }

        private void Button_Venda(object sender, RoutedEventArgs e)
        {
            if (JanelaAberta(simularVenda)) { simularVenda.Focus(); }
            else
            {
                simularVenda = new SimularVenda(context);
                simularVenda.Show();
            }
        }

        private void Button_Configuracoes(object sender, RoutedEventArgs e)
        {
            if (JanelaAberta(alterarConfiguracoes)) { alterarConfiguracoes.Focus(); }
            else
            {
                alterarConfiguracoes = new AlterarConfiguracoes(context);
                alterarConfiguracoes.Show();
            }
        }

        private void Button_Notificacoes(object sender, RoutedEventArgs e)
        {
            if (JanelaAberta(notificacoes)) { notificacoes.Focus(); }
            else
            {
                notificacoes = new Notificacoes(context);
                notificacoes.Show();
            }
        }

        private void Fechar(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (cadastrarProduto != null) { cadastrarProduto.Close(); }
            if (editarProduto != null) { editarProduto.Close(); }
            if (removerProduto != null) { removerProduto.Close(); }
            if (cadastrarCategoria != null) { cadastrarCategoria.Close(); }
            if (removerCategoria != null) { removerCategoria.Close(); }
            if (simularVenda != null) { simularVenda.Close(); }
            if (alterarConfiguracoes != null) { alterarConfiguracoes.Close(); }
            if (notificacoes != null) { notificacoes.Close(); }
        }

        private bool JanelaAberta(Window window)
        {
            if (window == null) { return false; }
            if (Application.Current.Windows.OfType<Window>().Any(w => w == window)) { return true; }
            return false;
        }
    }
}