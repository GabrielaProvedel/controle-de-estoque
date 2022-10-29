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
    /// Lógica interna para CadastrarProduto.xaml
    /// </summary>
    public partial class CadastrarProduto : Window
    {
        ProdutoController produtoController;
        CategoriaController categoriaController;

        public CadastrarProduto(Context context)
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

        private void Button_Cadastrar(object sender, RoutedEventArgs e)
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
                string mensagem = produtoController.cadastrar(cod, nom, cat, pre, qua);
                MessageBox.Show(mensagem);
                if (mensagem != "Código já cadastrado") { this.Close(); }
            }
        }
    }
}
