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
    /// Lógica interna para EditarProduto.xaml
    /// </summary>
    public partial class EditarProduto : Window
    {
        ProdutoController produtoController;
        CategoriaController categoriaController;

        public EditarProduto(Context context)
        {
            produtoController = new ProdutoController(context);
            categoriaController = new CategoriaController(context);

            InitializeComponent();

            List<Categoria> lista = categoriaController.listar();
            foreach (Categoria i in lista)
            {
                categoria.Items.Add(i.NomeCategoria);
            }
        }

        private void Button_EditarProduto(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(codigo.Text, out int cod) || cod <= 0) { MessageBox.Show("Digite um código válido"); }
            else if (nome.Text == "") { MessageBox.Show("Digite um nome válido"); }
            else if (categoria.SelectedItem == null) { MessageBox.Show("Selecione uma categoria"); }
            else if (!double.TryParse(preco.Text, out double pre) || pre < 0) { MessageBox.Show("Digite um preço válido"); }
            else if (!int.TryParse(quantidade.Text, out int qua) || qua < 0) { MessageBox.Show("Digite uma quantidade válida"); }
            else
            {
                string nom = nome.Text;
                string cat = categoria.SelectedItem.ToString();
                MessageBox.Show(produtoController.editar(cod, nom, cat, pre, qua));
                this.Close();
            }
        }

        private void codigo_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (int.TryParse(codigo.Text, out int cod))
            {
                if (produtoController.buscarProduto(cod) != null)
                {
                    nome.Text = produtoController.buscarProduto(cod).Nome;
                    categoria.SelectedItem = produtoController.buscarProduto(cod).Categoria.NomeCategoria;
                    preco.Text = produtoController.buscarProduto(cod).Preco.ToString();
                    quantidade.Text = produtoController.buscarProduto(cod).Quantidade.ToString();
                }
                else
                {
                    nome.Text = "";
                    categoria.SelectedItem = null;
                    preco.Text = "";
                    quantidade.Text = "";
                }
            }

        }
    }
}
