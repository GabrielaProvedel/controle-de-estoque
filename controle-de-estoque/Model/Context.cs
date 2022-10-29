using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace controle_de_estoque.Model
{
	public class Context : DbContext
	{
		public Context(DbContextOptions<Context> options) : base(options)
		{
			Database.EnsureCreated();
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
		}

		public DbSet<Categoria> Categorias { get; set; }
		public DbSet<Produto> Produtos { get; set; }
		public DbSet<Venda> Vendas { get; set; }
		public DbSet<Configuracao> Configuracoes { get; set; }
	}
}
