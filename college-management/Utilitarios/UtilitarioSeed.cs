using college_management.Constantes;
using college_management.Dados;
using college_management.Dados.Contexto;
using college_management.Dados.Modelos;
using Microsoft.EntityFrameworkCore;


namespace college_management.Utilitarios;


public static class UtilitarioSeed
{
	public static async Task IniciarBaseDeDados(BancoDeDados context)
	{
		// Certifique-se de que o banco de dados está criado
		await context.Database.EnsureCreatedAsync();

		// 1. Obter credenciais do gestor mestre
		var (loginMestre, nomeMestre, senhaMestre) = ObterCredenciais(
			VariaveisAmbiente.MasterAdminLogin,
			VariaveisAmbiente.MasterAdminNome,
			VariaveisAmbiente.MasterAdminSenha);

		// 2. Verificar se o gestor mestre já existe
		var gestorExistente = await context.Usuarios.OfType<Gestor>()
		                                   .FirstOrDefaultAsync(
			                                   u => u.Login == loginMestre);

		Gestor? gestorMestre;

		if (gestorExistente == null)
		{
			// Desativar temporariamente o rastreamento de chaves estrangeiras para o SQLite
			await context.Database.ExecuteSqlRawAsync(
				"PRAGMA foreign_keys = OFF;");

			// Criar o gestor mestre
			gestorMestre = new Gestor(loginMestre, nomeMestre)
			{
				Cargo = Cargo.Administrador
			};

			gestorMestre.GerarCredenciais(senhaMestre);
			context.Usuarios.Add(gestorMestre);

			try
			{
				await context.SaveChangesAsync();

				// Após salvar, atualizamos o GestorId com o próprio Id
				gestorMestre.GestorId = gestorMestre.Id;
				await context.SaveChangesAsync();

				// Reativar chaves estrangeiras
				await context.Database.ExecuteSqlRawAsync(
					"PRAGMA foreign_keys = ON;");
			}
			catch (DbUpdateException ex)
			{
				Console.WriteLine(
					$"Erro ao salvar o gestor mestre: {ex.InnerException?.Message}");

				throw;
			}
		}
		else
		{
			// Se o gestor já existe, usar o existente
			gestorExistente.GestorId = gestorExistente.Id;
			await context.SaveChangesAsync();
		}

		// 3. Recarregar o gestor mestre para usar como referência
		gestorMestre = await context.Usuarios.OfType<Gestor>()
		                            .FirstAsync(u => u.Login == loginMestre);

		// 4. Adicionar as demais entidades em transações separadas
		try
		{
			// Adicionar o docente teste
			if (!await context.Usuarios.AnyAsync(
				    u => u.Login == "docente.teste"))
			{
				var docenteTeste = new Docente("docente.teste", "Docente Teste")
				{
					GestorId = gestorMestre.Id
				};

				docenteTeste.GerarCredenciais("senhaTeste");
				context.Usuarios.Add(docenteTeste);
				await context.SaveChangesAsync();
			}

			// Adicionar o aluno teste
			var (loginTeste, nomeTeste, senhaTeste) = ObterCredenciais(
				VariaveisAmbiente.UsuarioTesteLogin,
				VariaveisAmbiente.UsuarioTesteNome,
				VariaveisAmbiente.UsuarioTesteSenha);

			if (!await context.Usuarios.AnyAsync(u => u.Login == loginTeste))
			{
				var alunoTeste = new Aluno(loginTeste, nomeTeste)
				{
					GestorId = gestorMestre.Id
				};

				alunoTeste.GerarCredenciais(senhaTeste);
				context.Usuarios.Add(alunoTeste);
				await context.SaveChangesAsync();
			}

			// Adicionar curso teste
			if (!await context.Cursos.AnyAsync(c => c.Nome == "Curso Teste"))
			{
				var cursoTeste = new Curso("Curso Teste")
				{
					GestorId = gestorMestre.Id
				};
				context.Cursos.Add(cursoTeste);
				await context.SaveChangesAsync();
			}

			// Adicionar matéria teste
			if (!await context.Materias.AnyAsync(
				    m => m.Nome == "Matéria Teste"))
			{
				var materiaTeste = new Materia("Matéria Teste")
				{
					CargaHoraria = 40,
					GestorId     = gestorMestre.Id
				};
				context.Materias.Add(materiaTeste);
				await context.SaveChangesAsync();
			}
		}
		catch (DbUpdateException ex)
		{
			Console.WriteLine(
				$"Erro ao salvar entidades: {ex.InnerException?.Message}");

			throw;
		}
	}

	private static (string? loginDefault, string? nomeDefault, string?
		senhaDefault) ObterCredenciais(string login, string nome, string senha)
	{
		_ = UtilitarioAmbiente.Variaveis.TryGetValue(
			login,
			out var loginDefault);
		_ = UtilitarioAmbiente.Variaveis.TryGetValue(nome, out var nomeDefault);
		_ = UtilitarioAmbiente.Variaveis.TryGetValue(
			senha,
			out var senhaDefault);

		return (loginDefault, nomeDefault, senhaDefault);
	}

	public static async Task<bool> ValidarDadosIniciais(BancoDeDados context)
	{
		var existeAdmin = await context.Usuarios.OfType<Gestor>()
		                               .AnyAsync(
			                               g => g.Cargo == Cargo.Administrador);
		var existeAluno = await context.Usuarios.OfType<Aluno>().AnyAsync();

		var loginMestre
			= UtilitarioAmbiente.Variaveis[VariaveisAmbiente.MasterAdminLogin];
		var existeMestre
			= await context.Usuarios.AnyAsync(u => u.Login == loginMestre);

		var existeMateria
			= await context.Materias.AnyAsync(m => m.Nome == "Matéria Teste");
		var existeCurso
			= await context.Cursos.AnyAsync(c => c.Nome == "Curso Teste");

		var loginTeste
			= UtilitarioAmbiente.Variaveis[VariaveisAmbiente.UsuarioTesteLogin];
		var existeUsuario
			= await context.Usuarios.AnyAsync(
				u => u.Login == loginTeste);

		return existeAdmin
		       && existeAluno
		       && existeMestre
		       && existeMateria
		       && existeCurso
		       && existeUsuario;
	}
}
