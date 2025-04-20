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
		var (loginMestre, nomeMestre, senhaMestre) = ObterCredenciais(
			VariaveisAmbiente.MasterAdminLogin,
			VariaveisAmbiente.MasterAdminNome,
			VariaveisAmbiente.MasterAdminSenha
		);

		var (loginTeste, nomeTeste, senhaTeste) = ObterCredenciais(
			VariaveisAmbiente.UsuarioTesteLogin,
			VariaveisAmbiente.UsuarioTesteNome,
			VariaveisAmbiente.UsuarioTesteSenha
		);

		if (!await context.Usuarios.AnyAsync(u => u.Login == loginMestre))
		{
			var gestorMestre = new Gestor(loginMestre, nomeMestre)
			{
				Cargo = Cargo.Administrador
			};

			gestorMestre.GerarCredenciais(senhaMestre);
			context.Usuarios.Add(gestorMestre);
		}

		if (!await context.Usuarios.AnyAsync(u => u.Login == "docente.teste"))
		{
			var docenteTeste = new Docente(
				"docente.teste",
				"Docente Teste"
			);
			
			docenteTeste.GerarCredenciais("senhaTeste");
			context.Usuarios.Add(docenteTeste);
		}

		if (!await context.Usuarios.AnyAsync(u => u.Login == loginTeste))
		{
			var alunoTeste = new Aluno(loginTeste, nomeTeste);
			
			alunoTeste.GerarCredenciais(senhaTeste);
			context.Usuarios.Add(alunoTeste);
		}

		if (!await context.Cursos.AnyAsync(c => c.Nome == "Curso Teste"))
		{
			var cursoTeste = new Curso("Curso Teste");
			context.Cursos.Add(cursoTeste);
		}

		if (!await context.Materias.AnyAsync(m => m.Nome == "Matéria Teste"))
		{
			var materiaTeste = new Materia("Matéria Teste")
					{ CargaHoraria = 40 };
			context.Materias.Add(materiaTeste);
		}

		await context.SaveChangesAsync();

		// RELACIONAMENTOS N:N
		var curso
				= await context.Cursos.FirstOrDefaultAsync(
					c => c.Nome == "Curso Teste"
				);
		var materia
				= await context.Materias.FirstOrDefaultAsync(
					m => m.Nome == "Matéria Teste"
				);
		var aluno = await context.Usuarios.OfType<Aluno>()
		                         .FirstOrDefaultAsync(
			                         a => a.Login == loginTeste
		                         );
		var docente = await context.Usuarios.OfType<Docente>()
		                           .FirstOrDefaultAsync(
			                           d => d.Login == "docente.teste"
		                           );

		// Adicionar Materia Teste ao Curso Teste
		if (curso != null && materia != null)
		{
			if (!curso.Materias.Contains(materia))
			{
				curso.Materias.Add(materia);
			}

			if (!materia.Cursos.Contains(curso)) { materia.Cursos.Add(curso); }

			await context.SaveChangesAsync();
		}

		// Adicionar Aluno Teste ao Curso Teste
		if (curso != null && aluno != null)
		{
			if (!curso.Alunos.Contains(aluno)) { curso.Alunos.Add(aluno); }

			if (!aluno.Cursos.Contains(curso)) { aluno.Cursos.Add(curso); }

			await context.SaveChangesAsync();
		}

		// Adicionar Aluno Teste à Matéria Teste
		if (materia != null && aluno != null)
		{
			if (!materia.Alunos.Contains(aluno)) { materia.Alunos.Add(aluno); }

			if (!aluno.Materias.Contains(materia))
			{
				aluno.Materias.Add(materia);
			}

			await context.SaveChangesAsync();
		}

		// Adicionar Docente Teste na Materia Teste
		if (materia != null && docente != null)
		{
			if (!materia.Docentes.Contains(docente))
			{
				materia.Docentes.Add(docente);
			}

			if (!docente.Materias.Contains(materia))
			{
				docente.Materias.Add(materia);
			}

			await context.SaveChangesAsync();
		}

		// Adicionar Aluno Teste à Matricula do Curso Teste
		if (curso != null && aluno != null)
		{
			var matricula = new Matricula(1, Modalidade.Presencial)
			{
				CursoId = curso.Id,
				AlunoId = aluno.Id
			};

			context.Matriculas.Add(matricula);
			await context.SaveChangesAsync();
		}

		// Adicionar Docente Teste como professor da Materia Teste
		if (materia != null && docente != null)
		{
			var corpoDocente = new CorpoDocente
			{
				MateriaId = materia.Id,
				DocenteId = docente.Id
			};


			context.CorpoDocente.Add(corpoDocente);
			await context.SaveChangesAsync();
		}

		// Adicionar Avaliacao Teste a Materia Teste
		if (materia != null && aluno != null)
		{
			var avaliacao = new Avaliacao
			{
				AlunoId   = aluno.Id,
				MateriaId = materia.Id
			};

			context.Avaliacoes.Add(avaliacao);
			await context.SaveChangesAsync();
		}

		// Adicionar Materia Teste à grade curricular do Curso Teste
		if (curso != null && materia != null)
		{
			var gradeCurricular = new GradeCurricular
			{
				CursoId   = curso.Id,
				MateriaId = materia.Id
			};

			context.GradeCurricular.Add(gradeCurricular);
			await context.SaveChangesAsync();
		}

		// Registar o Aluno Teste numa Turma de teste
		if (materia != null && aluno != null && docente != null)
		{
			var turma = new Turma
			{
				MateriaId = materia.Id,
				AlunoId   = aluno.Id,
				DocenteId = docente.Id,
				Turno     = Turno.Matutino
			};

			context.Turmas.Add(turma);
			docente.Turmas.Add(turma);

			await context.SaveChangesAsync();
		}
	}

	private static (string? loginDefault, string? nomeDefault, string? senhaDefault) ObterCredenciais(
		string login,
		string nome,
		string senha
	)
	{
		_ = UtilitarioAmbiente.Variaveis.TryGetValue(
			login,
			out var loginDefault
		);
		_ = UtilitarioAmbiente.Variaveis.TryGetValue(nome, out var nomeDefault);
		_ = UtilitarioAmbiente.Variaveis.TryGetValue(
			senha,
			out var senhaDefault
		);

		return (loginDefault, nomeDefault, senhaDefault);
	}

	public static async Task<bool> ValidarDadosIniciais(BancoDeDados context)
	{
		var cargoAdms = await context.Usuarios.OfType<Gestor>()
		                             .AnyAsync(
			                             g => g.Cargo == Cargo.Administrador
		                             );
		var cargoAlunos = await context.Usuarios.OfType<Aluno>().AnyAsync();
		var usuarioMestre
				= await context.Usuarios.AnyAsync(
					u => u.Nome == VariaveisAmbiente.MasterAdminNome
				);
		var materiaTeste
				= await context.Materias.AnyAsync(
					m => m.Nome == "Matéria Teste"
				);
		var cursoTeste
				= await context.Cursos.AnyAsync(c => c.Nome == "Curso Teste");
		var usuarioTeste
				= await context.Usuarios.AnyAsync(
					u => u.Login == VariaveisAmbiente.UsuarioTesteLogin
				);

		return cargoAdms
		       && cargoAlunos
		       && usuarioMestre
		       && cursoTeste
		       && usuarioTeste
		       && materiaTeste;
	}
}
