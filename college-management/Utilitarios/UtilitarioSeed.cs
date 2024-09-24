using college_management.Constantes;
using college_management.Dados;
using college_management.Dados.Modelos;
using college_management.Dados.Repositorios;


namespace college_management.Utilitarios;


public static class UtilitarioSeed
{
	public static async Task IniciarBaseDeDados(BaseDeDados baseDeDados)
	{
		await baseDeDados
		      .Cargos
		      .Adicionar(new Cargo(CargosPadrao.CargoAdministradores,
		                           [PermissoesAcesso.AcessoAdministradores]));

		await baseDeDados
		      .Cargos
		      .Adicionar(new Cargo(CargosPadrao.CargoAlunos,
		                           [PermissoesAcesso.PermissaoAcessoLeitura]));

		await baseDeDados
		      .Cargos
		      .Adicionar(new Cargo(CargosPadrao.CargoAlunos,
		                           [PermissoesAcesso.PermissaoAcessoLeitura]));

		var (loginMestre, nomeMestre, senhaMestre)
			= ObterCredenciais(VariaveisAmbiente.MasterAdminLogin,
			                   VariaveisAmbiente.MasterAdminNome,
			                   VariaveisAmbiente.MasterAdminSenha);

		var cargoAdmin = baseDeDados
		                 .Cargos
		                 .ObterPorNome(CargosPadrao.CargoAdministradores);

		await baseDeDados
		      .Usuarios
		      .Adicionar(new Funcionario(loginMestre,
		                                 nomeMestre,
		                                 senhaMestre,
		                                 cargoAdmin.Id));

		var (loginTeste, nomeTeste, senhaTeste)
			= ObterCredenciais(VariaveisAmbiente.UsuarioTesteLogin,
			                   VariaveisAmbiente.UsuarioTesteNome,
			                   VariaveisAmbiente.UsuarioTesteSenha);

		Materia materiaTeste = new("Matéria Teste", Turno.Integral, 60);
		await baseDeDados.Materias.Adicionar(materiaTeste);

		Curso cursoTeste = new("Curso Teste", [materiaTeste]);
		await baseDeDados.Cursos.Adicionar(cursoTeste);

		Matricula matriculaTeste = new(1, Modalidade.Presencial);

		var cargoAluno = baseDeDados
		                 .Cargos
		                 .ObterPorNome(CargosPadrao.CargoAlunos);

		var alunoTeste = new Aluno(loginTeste,
		                           nomeTeste,
		                           senhaTeste,
		                           cargoAluno.Id,
		                           matriculaTeste.Id);

		await baseDeDados.Usuarios.Adicionar(alunoTeste);

		matriculaTeste.AlunoId = alunoTeste.Id;
		matriculaTeste.CursoId = cursoTeste.Id;
		await baseDeDados.Matriculas.Adicionar(matriculaTeste);
	}

	private static (string, string, string) ObterCredenciais(string login,
	                                                         string nome,
	                                                         string senha)
	{
		_ = UtilitarioAmbiente
		    .Variaveis
		    .TryGetValue(login, out var loginDefault);

		_ = UtilitarioAmbiente
		    .Variaveis
		    .TryGetValue(nome, out var nomeDefault);

		_ = UtilitarioAmbiente
		    .Variaveis
		    .TryGetValue(senha, out var senhaDefault);

		return (loginDefault, nomeDefault, senhaDefault);
	}
}
