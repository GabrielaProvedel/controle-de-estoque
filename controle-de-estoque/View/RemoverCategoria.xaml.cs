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
    /// Lógica interna para RemoverCategoria.xaml
    /// </summary>
    public partial class RemoverCategoria : Window
    {
        CategoriaController categoriaController;

        public RemoverCategoria(Context context)
        {
            categoriaController = new CategoriaController(context);
            InitializeComponent();

            List<Categoria> lista = categoriaController.listar();
            foreach (Categoria i in lista)
            {
                categoria.Items.Add(i.NomeCategoria);
            }
        }

        private void Button_Remover(object sender, RoutedEventArgs e)
        {
            if (categoria.SelectedItem == null) { MessageBox.Show("Selecione uma categoria"); }
            else
            {
                string mensagem = categoriaController.remover(categoria.SelectedItem.ToString());
                MessageBox.Show(mensagem);
                if(mensagem == "Categoria  e respectivos produtos removidos do estoque") { this.Close(); }
            }
        }
    }
}
