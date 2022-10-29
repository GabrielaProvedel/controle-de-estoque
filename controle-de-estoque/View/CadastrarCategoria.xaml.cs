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
    /// Lógica interna para CadastrarCategoria.xaml
    /// </summary>
    public partial class CadastrarCategoria : Window
    {

        CategoriaController categoriaController;

        public CadastrarCategoria(Context context)
        {
            categoriaController = new CategoriaController(context);
            InitializeComponent();
        }

        private void Button_Cadastrar(object sender, RoutedEventArgs e)
        {
            if (nome.Text == "") { MessageBox.Show("Digite um nome válido"); }
            else
            {
                string mensagem = categoriaController.cadastrar(nome.Text);
                MessageBox.Show(mensagem);
                if (mensagem != "Categoria já cadastrada") { this.Close(); }
            }
        }
    }
}
