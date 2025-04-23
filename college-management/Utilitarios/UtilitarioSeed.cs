using college_management.Constantes;
using college_management.Dados;
using college_management.Dados.Contexto;
using college_management.Dados.Modelos;
using college_management.Views;
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
		var mestreExiste = await context.Gestores.AsNoTracking()
		                                .AnyAsync(g => g.Login == loginMestre);

		if (!mestreExiste)
		{
			// Desativar temporariamente o rastreamento de chaves estrangeiras para o SQLite
			await context.Database.ExecuteSqlRawAsync(
				"PRAGMA foreign_keys = OFF;");

			// Criar o gestor mestre
			var novoMestre = new Gestor(loginMestre, nomeMestre)
			{
				Cargo = Cargo.Administrador
			};

			novoMestre.GerarCredenciais(senhaMestre);
			context.Gestores.Add(novoMestre);

			context.SaveChanges();

			context.Entry(novoMestre).State = EntityState.Detached;

			try
			{
				// Após salvar, atualizamos o GestorId com o próprio Id
				Gestor mestreComFk = new(novoMestre.Login, novoMestre.Nome)
				{
					Cargo = novoMestre.Cargo
				};

				mestreComFk.GerarCredenciais(senhaMestre);
				context.Gestores.Update(mestreComFk);

				context.Salvar(novoMestre.Login);

				// Reativar chaves estrangeiras
				await context.Database.ExecuteSqlRawAsync(
					"PRAGMA foreign_keys = ON;");

				context.Entry(mestreComFk).State = EntityState.Detached;
			}
			catch (DbUpdateException ex)
			{
				Console.WriteLine(
					$"Erro ao salvar o gestor mestre: {ex.InnerException?.Message}");

				throw;
			}
		}

		// 3. Recarregar o gestor mestre para usar como referência
		var mestre = await context.Gestores.AsNoTracking()
		                          .FirstAsync(u => u.Login == loginMestre);

		// 4. Adicionar as demais entidades em transações separadas
		try
		{
			// Adicionar o docente teste
			if (!await context.Docentes.AnyAsync(
				    u => u.Login == "docente.teste"))
			{
				var docenteTeste
					= new Docente("docente.teste", "Docente Teste");

				docenteTeste.GerarCredenciais("senhaTeste");
				context.Docentes.Add(docenteTeste);

				context.Salvar(mestre.Login!);

				context.Entry(docenteTeste).State = EntityState.Detached;
			}

			// Adicionar o aluno teste
			var (loginTeste, nomeTeste, senhaTeste) = ObterCredenciais(
				VariaveisAmbiente.UsuarioTesteLogin,
				VariaveisAmbiente.UsuarioTesteNome,
				VariaveisAmbiente.UsuarioTesteSenha);

			if (!await context.Alunos.AnyAsync(u => u.Login == loginTeste))
			{
				var alunoTeste = new Aluno(loginTeste, nomeTeste);

				alunoTeste.GerarCredenciais(senhaTeste);
				context.Alunos.Add(alunoTeste);

				context.Salvar(mestre.Login!);

				context.Entry(alunoTeste).State = EntityState.Detached;
			}

			// Adicionar curso teste
			if (!await context.Cursos.AnyAsync(c => c.Nome == "Curso Teste"))
			{
				var cursoTeste = new Curso("Curso Teste");
				context.Cursos.Add(cursoTeste);
				context.Salvar(mestre.Login!);

				context.Entry(cursoTeste).State = EntityState.Detached;
			}

			// Adicionar matéria teste
			if (!await context.Materias.AnyAsync(
				    m => m.Nome == "Matéria Teste"))
			{
				var materiaTeste = new Materia("Matéria Teste")
				{
					CargaHoraria = 40,
				};

				context.Materias.Add(materiaTeste);
				context.Salvar(mestre.Login!);

				context.Entry(materiaTeste).State = EntityState.Detached;
			}
		}
		catch (DbUpdateException ex)
		{
			Console.WriteLine(
				$"Erro ao salvar entidades: {ex.InnerException?.Message}");

			throw;
		}

		View.Aviso("Banco de dados inicializado com sucesso!");
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
		var existeAdmin = await context.Gestores.OfType<Gestor>()
		                               .AnyAsync(
			                               g => g.Cargo == Cargo.Administrador);
		var existeAluno = await context.Alunos.OfType<Aluno>().AnyAsync();

		var loginMestre
			= UtilitarioAmbiente.Variaveis[VariaveisAmbiente.MasterAdminLogin];
		var existeMestre
			= await context.Gestores.AnyAsync(u => u.Login == loginMestre);

		var existeMateria
			= await context.Materias.AnyAsync(m => m.Nome == "Matéria Teste");
		var existeCurso
			= await context.Cursos.AnyAsync(c => c.Nome == "Curso Teste");

		var loginTeste
			= UtilitarioAmbiente.Variaveis[VariaveisAmbiente.UsuarioTesteLogin];
		var existeUsuario
			= await context.Alunos.AnyAsync(u => u.Login == loginTeste);

		return existeAdmin
		       && existeAluno
		       && existeMestre
		       && existeMateria
		       && existeCurso
		       && existeUsuario;
	}
}
