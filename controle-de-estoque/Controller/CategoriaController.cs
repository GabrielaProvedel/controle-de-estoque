using controle_de_estoque.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace controle_de_estoque.Controller
{
    public class CategoriaController
    {
        private readonly Context context;

        public CategoriaController(Context context)
        {
            this.context = context;
        }

        public string cadastrar(string nome)
        {
            try
            {
                if (context.Categorias.Any(c => c.NomeCategoria == nome))
                {
                    return "Categoria já cadastrada";
                }

                Categoria novaCategoria = new Categoria();
                novaCategoria.NomeCategoria = nome;

                context.Categorias.Add(novaCategoria);
                context.SaveChanges();
                return "Categoria cadastrada com sucesso!";
            }
            catch
            {
                return "Erro ao cadastrar categoria";
            }
        }

        public string remover(string nome)
        {
            try
            {
                if (!(context.Categorias.Any(categoria => categoria.NomeCategoria == nome))) { return "Categoria não encontrada"; }

                Categoria categoria = context.Categorias.Single(c => c.NomeCategoria == nome);

                ProdutoController produtoController = new ProdutoController(context);
                List<Produto> lista = produtoController.filtrar(nome);
                foreach (Produto p in lista)
                {
                    context.Remove(p);
                }
                context.Remove(categoria);

                context.SaveChanges();
                return "Categoria  e respectivos produtos removidos do estoque";
            }
            catch
            {
                return "Erro ao remover categoria";
            }
        }

        public List<Categoria> listar()
        {
            return context.Categorias.ToList();
        }
    }
}