using college_management.Constantes;
using college_management.Dados;
using college_management.Dados.Modelos;
using college_management.Dados.Repositorios;


namespace college_management.Utilitarios;


public static class UtilitarioSeed
{
	public static async Task IniciarBaseDeDados(
		BaseDeDados baseDeDados)
	{
		await CadastrarCargoPadrao(new Cargo(CargosPadrao
			                                     .CargoAdministradores,
		                                     [
			                                     PermissoesAcesso
				                                     .PermissaoAcessoEscrita,
			                                     PermissoesAcesso
				                                     .PermissaoAcessoAdministradores
		                                     ]),
		                           baseDeDados.Cargos);

		await CadastrarCargoPadrao(new Cargo(CargosPadrao
			                                     .CargoGestores,
		                                     [
			                                     PermissoesAcesso
				                                     .PermissaoAcessoEscrita
		                                     ]),
		                           baseDeDados.Cargos);

		await CadastrarCargoPadrao(new Cargo(CargosPadrao
			                                     .CargoAlunos,
		                                     [
			                                     PermissoesAcesso
				                                     .PermissaoAcessoLeitura
		                                     ]),
		                           baseDeDados.Cargos);

		var (loginMestre, nomeMestre, senhaMestre)
			= ObterCredenciais(VariaveisAmbiente
				                   .MasterAdminLogin,
			                   VariaveisAmbiente.MasterAdminNome,
			                   VariaveisAmbiente
				                   .MasterAdminSenha);

		var cargoAdmin = baseDeDados.Cargos
		                            .ObterTodos()
		                            .FirstOrDefault(c
			                                            => c.Nome
				                                               is
				                                               CargosPadrao
					                                               .CargoAdministradores);

		await CadastrarUsuarioPadrao(new Funcionario(loginMestre,
		                                             nomeMestre,
		                                             cargoAdmin,
		                                             senhaMestre),
		                             baseDeDados.Usuarios);

		var (loginTeste, nomeTeste, senhaTeste)
			= ObterCredenciais(VariaveisAmbiente
				                   .UsuarioTesteLogin,
			                   VariaveisAmbiente
				                   .UsuarioTesteNome,
			                   VariaveisAmbiente
				                   .UsuarioTesteSenha);

		Materia materiaTeste = new("Matéria Teste",
		                           Turno.Integral,
		                           60);

		await CadastrarMateriaPadrao(materiaTeste,
		                             baseDeDados.Materias);

		Curso cursoTeste = new("Curso Teste", [materiaTeste]);
		await CadastrarCursoPadrao(cursoTeste,
		                           baseDeDados.Cursos);

		Matricula matriculaTeste = new(1,
		                               1,
		                               cursoTeste,
		                               Modalidade.Presencial);

		var cargoAluno =
			baseDeDados.Cargos
			           .ObterTodos()
			           .FirstOrDefault(c => c.Nome is
				                                CargosPadrao
					                                .CargoAlunos);

		await CadastrarUsuarioPadrao(new Aluno(loginTeste,
		                                       nomeTeste,
		                                       cargoAluno,
		                                       senhaTeste,
		                                       matriculaTeste),
		                             baseDeDados.Usuarios);
	}

	private static async Task CadastrarCargoPadrao(Cargo cargo,
	                                               RepositorioCargos
		                                               repositorio)
	{
		await repositorio.Adicionar(cargo);
	}

	private static async Task CadastrarMateriaPadrao(
		Materia materia,
		RepositorioMaterias
			repositorio)
	{
		await repositorio.Adicionar(materia);
	}

	private static async Task CadastrarCursoPadrao(Curso curso,
	                                               RepositorioCursos
		                                               repositorio)
	{
		await repositorio.Adicionar(curso);
	}

	private static async Task CadastrarUsuarioPadrao(
		Usuario usuario,
		RepositorioUsuarios
			repositorio)
	{
		await repositorio.Adicionar(usuario);
	}

	private static (string, string, string) ObterCredenciais(
		string login,
		string nome,
		string senha)
	{
		UtilitarioAmbiente.Variaveis
		                  .TryGetValue(login,
		                               out var loginDefault);

		UtilitarioAmbiente.Variaveis
		                  .TryGetValue(nome,
		                               out var nomeDefault);

		UtilitarioAmbiente.Variaveis
		                  .TryGetValue(senha,
		                               out var senhaDefault);

		return (loginDefault, nomeDefault, senhaDefault);
	}
}
