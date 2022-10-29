using controle_de_estoque.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace controle_de_estoque.Controller
{
    public class VendaController
    {
        private readonly Context context;

        public VendaController(Context context)
        {
            this.context = context;
        }

        public Venda simularVenda()
        {
            Venda venda = new Venda();
            if (context.Vendas.ToList<Venda>().Any<Venda>())
            {
                venda.IdVenda = context.Vendas.ToList<Venda>().Last<Venda>().IdVenda + 1;
            }
            else
            {
                venda.IdVenda = 1;
            }

            venda.carrinho = new List<Item>();

            return venda;
        }

        public List<Item> listar(Venda venda)
        {
            return venda.carrinho;
        }

        public string adicionarProduto(Venda venda, int codigo, int quantidade)
        {
            try
            {
                if(!context.Produtos.Any(p => p.Codigo == codigo))
                {
                    return "Produto não encontrado";
                }

                Produto produto = context.Produtos.Single(p => p.Codigo == codigo);

                if (!venda.carrinho.Any(i => i.produto == produto))
                {
                    if (quantidade > produto.Quantidade)
                    {
                        return "A quantidade é maior do que a disponível no estoque";
                    }
                    else
                    {
                        Item item = new();
                        
                        item.produto = produto;
                        item.quantidade = quantidade;
                        item.preco = produto.Preco * item.quantidade;

                        venda.carrinho.Add(item);
                        return "";
                    }
                }
                else
                {
                    Item item = venda.carrinho.Single(i => i.produto == produto);
                    if(item.quantidade + quantidade > produto.Quantidade)
                    {
                        return "A quantidade é maior do que a disponível no estoque";
                    }
                    else
                    {
                        item.quantidade += quantidade;
                        item.preco = item.produto.Preco * item.quantidade;

                        return "";
                    }
                }
            }
            catch
            {
                return "Não foi possível adicionar item ao carrinho";
            }
        }

        public string removerProduto(Venda venda, int codigo, int quantidade)
        {
            try
            {
                if (!venda.carrinho.Any(i => i.produto.Codigo == codigo))
                {
                    return "O carrinho não contém este produto";
                }

                Item item = venda.carrinho.Single(i => i.produto.Codigo == codigo);

                if (quantidade == item.quantidade)
                {
                    venda.carrinho.Remove(item);
                    return "";
                }
                else if (quantidade < item.quantidade)
                {
                    item.quantidade -= quantidade;
                    item.preco = item.produto.Preco * item.quantidade;
                    return "";
                }
                else //(quantidade > item.quantidade)
                {
                    return "A quantidade a ser removida é maior do que a contida no carrinho";
                }
            }
            catch
            {
                return "Não foi possível remover item do carrinho";
            }
        }

        public string finalizarVenda(Venda venda)
        {
            try
            {
                if (!venda.carrinho.Any<Item>()) { return "O carrinho está vazio"; }

                venda.DataVenda = DateTime.Now;

                foreach (var item in venda.carrinho)
                {
                    if(item.quantidade > item.produto.Quantidade) { return "A quantidade do produto '" + item.produto.Codigo + "' é maior do que a disponível no estoque" + Environment.NewLine + "Quantidade disponível: " + item.produto.Quantidade + Environment.NewLine + "Quantidade no carrinho: " + item.quantidade; }
                }

                foreach (var item in venda.carrinho)
                {
                    Produto produto = item.produto;

                    produto.Quantidade -= item.quantidade;
                    context.Produtos.Update(produto);

                    venda.Total += item.preco;
                }

                context.Vendas.Add(venda);
                context.SaveChanges();
                return "Venda finalizada";
            }
            catch
            {
                return "Erro ao finalizar venda";
            }

        }

        public double valorTotal(Venda venda)
        {
            double total = 0;
            foreach (Item i in venda.carrinho)
            {
                total += i.preco;
            }
            return total;
        }
    }
}