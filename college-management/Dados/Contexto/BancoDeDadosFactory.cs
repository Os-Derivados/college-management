using college_management.Utilitarios;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;


namespace college_management.Dados.Contexto;


public class BancoDeDadosFactory : IDesignTimeDbContextFactory<BancoDeDados>
{
	public BancoDeDados CreateDbContext(string[] args)
	{
		DbContextOptionsBuilder<BancoDeDados> optionsBuilder = new();
		optionsBuilder.UseSqlite(
			$"Data Source={Path.Combine(UtilitarioArquivos.DiretorioDados, "college_management.db")}");

		return new BancoDeDados(optionsBuilder.Options);
	}
}
