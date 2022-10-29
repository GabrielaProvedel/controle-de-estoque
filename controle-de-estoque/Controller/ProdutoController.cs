using controle_de_estoque.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace controle_de_estoque.Controller
{
    public class ProdutoController
    {
        private readonly Context context;
        CategoriaController categoriaController;
        ConfiguracaoController configuracaoController;

        public ProdutoController(Context context)
        {
            categoriaController = new CategoriaController(context);
            configuracaoController = new ConfiguracaoController(context);
            this.context = context;
        }

        public string cadastrar(int codigo, string nome, string cat, double preco, int quantidade)
        {
            try
            {
                if (context.Produtos.Any(produto => produto.Codigo == codigo))
                {
                    return "Código já cadastrado";
                }

                if(!context.Categorias.Any(categoria => categoria.NomeCategoria == cat)) { return "Categoria não encontrada"; }

                Categoria categoria = context.Categorias.Single(c => c.NomeCategoria == cat);

                Produto novoProduto = new Produto();
                novoProduto.Codigo = codigo;
                novoProduto.Nome = nome;
                novoProduto.Categoria = categoria;
                novoProduto.Preco = preco;
                novoProduto.Quantidade = quantidade;

                context.Produtos.Add(novoProduto);
                context.SaveChanges();

                return "Produto " + novoProduto.Nome + " cadastrado com sucesso";
            }
            catch
            {
                return "Erro ao cadastrar produto";
            }
        }

        public string remover(int codigo)
        {
            try
            {
                if(!(context.Produtos.Any(produto => produto.Codigo == codigo))) { return "Produto não encontrado no estoque"; }

                Produto produto = context.Produtos.Single(p => p.Codigo == codigo);
                context.Remove(produto);
                context.SaveChanges();
                return "Produto removido do estoque";
            }
            catch
            {
                return "Erro ao remover produto";
            }
        }

        public string editar(int codigo, string nome, string c, double preco, int quantidade)
        {
            try
            {
                if (!(context.Produtos.Any(produto => produto.Codigo == codigo))) { return "Produto não encontrado no estoque"; }
                if (!context.Categorias.Any(categoria => categoria.NomeCategoria == c)) { return "Categoria não encontrada"; }

                Produto produto = context.Produtos.Single(p => p.Codigo == codigo);
                Categoria categoria = context.Categorias.Single(cat => cat.NomeCategoria == c);

                produto.Nome = nome;
                produto.Categoria = categoria;
                produto.Preco = preco;
                produto.Quantidade = quantidade;

                context.Produtos.Update(produto);
                context.SaveChanges();
                return "Produto '" + codigo + "' editado com sucesso!";
            }
            catch
            {
                return "Erro ao editar produto";
            }
        }

        public List<Produto> listar()
        {
            return context.Produtos.ToList();
        }

        public List<Produto> buscar(int codigo)
        {
            try
            {
                Produto produto = context.Produtos.Single(p => p.Codigo == codigo);
                List<Produto> lista = new List<Produto>();
                lista.Add(produto);
                return lista;
            }
            catch
            {
                return null;
            }
        }

        public Produto buscarProduto(int codigo)
        {
            try
            {
                Produto produto = context.Produtos.Single(p => p.Codigo == codigo);
                return produto;
            }
            catch
            {
                return null;
            }
        }

        public List<Produto> filtrar(string nome)
        {
            if (nome == "Todas as categorias") { return context.Produtos.ToList(); }

            List<Produto> geral = context.Produtos.ToList();
            List<Produto> lista = new List<Produto>();

            foreach (Produto produto in geral)
            {
                if (produto.Categoria.NomeCategoria == nome)
                {
                    lista.Add(produto);
                }
            }
            return lista;
        }

        public List<String> emBaixa(Configuracao configuracao)
        {
            List<String> lista = new List<String>();

            foreach (Produto produto in context.Produtos.ToList())
            {
                if (produto.Quantidade <= configuracaoController.obterQuantidade(configuracao))
                {
                    lista.Add("Estoque baixo de " + produto.Nome + Environment.NewLine + "Código: " + produto.Codigo + Environment.NewLine + "Quantidade: " + produto.Quantidade + Environment.NewLine);
                }
            }
            return lista;
        }
    }
}