using college_management.Constantes;
using college_management.Dados;
using college_management.Dados.Modelos;


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
		                           [PermissoesAcesso.AcessoLeitura]));

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
		                                 cargoAdmin.Modelo!.Id));

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
		                           cargoAluno.Modelo!.Id,
		                           matriculaTeste.Id);

		await baseDeDados.Usuarios.Adicionar(alunoTeste);

		matriculaTeste.AlunoId = alunoTeste.Id;
		matriculaTeste.CursoId = cursoTeste.Id;
		await baseDeDados.Matriculas.Adicionar(matriculaTeste);
	}

	private static (string, string, CredenciaisUsuario) ObterCredenciais(string login,
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

        return (loginDefault, nomeDefault, new(senhaDefault));
	}
	public static bool ValidaDadosIniciais(BaseDeDados baseDeDados)
	{

		var _cargoAdms = baseDeDados
			.Cargos
			.ObterPorNome(CargosPadrao.CargoAdministradores) is { } _;
		
		var _cargoAlunos = baseDeDados
			.Cargos
			.ObterPorNome(CargosPadrao.CargoAlunos) is { } _;
		
		_ = UtilitarioAmbiente
			.Variaveis
			.TryGetValue(VariaveisAmbiente.MasterAdminNome, out var nomeDefault);
		var _cargoMaster = baseDeDados
			.Usuarios
			.ObterPorNome(nomeDefault) is { } _;
		
		var _materiaTeste = baseDeDados
			.Materias
			.ObterPorNome("Matéria Teste") is { } _;
		
		var _cursoTeste = baseDeDados
			.Cursos
			.ObterPorNome("Curso Teste") is { } _;
		
		_ = UtilitarioAmbiente
			.Variaveis
			.TryGetValue(VariaveisAmbiente.UsuarioTesteLogin, out var loginAluno);
		var _usuarioTeste = baseDeDados
			.Usuarios
			.ObterPorLogin(loginAluno) is { } _;
		
		return _cargoMaster & _cargoAdms & _cargoAlunos & _cursoTeste & _usuarioTeste & _materiaTeste;
	}
}
