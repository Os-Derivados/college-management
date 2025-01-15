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

		var obterCargoAdmin = baseDeDados
		                 .Cargos
		                 .ObterPorNome(CargosPadrao.CargoAdministradores);

		if (obterCargoAdmin.Status is StatusResposta.ErroNaoEncontrado) return;

		await baseDeDados
		      .Usuarios
		      .Adicionar(new Funcionario(loginMestre,
		                                 nomeMestre,
		                                 senhaMestre,
		                                 obterCargoAdmin.Modelo!.Id!));

		var (loginTeste, nomeTeste, senhaTeste)
			= ObterCredenciais(VariaveisAmbiente.UsuarioTesteLogin,
			                   VariaveisAmbiente.UsuarioTesteNome,
			                   VariaveisAmbiente.UsuarioTesteSenha);

		Materia materiaTeste = new("Matéria Teste", Turno.Integral, 60);
		await baseDeDados.Materias.Adicionar(materiaTeste);

		Matricula matriculaTeste = new(1, Modalidade.Presencial);

		Curso cursoTeste = new("Curso Teste", [materiaTeste]);
		(cursoTeste.MatriculasIds = []).Add(matriculaTeste.Id!);
		await baseDeDados.Cursos.Adicionar(cursoTeste);


		var obterCargoAluno = baseDeDados
		                 .Cargos
		                 .ObterPorNome(CargosPadrao.CargoAlunos);
		
		if (obterCargoAluno.Status is StatusResposta.ErroNaoEncontrado) return;

		var alunoTeste = new Aluno(loginTeste,
		                           nomeTeste,
		                           senhaTeste,
		                           obterCargoAluno.Modelo!.Id!,
		                           matriculaTeste.Id!);

		var alunoCriado = await baseDeDados.Usuarios.Adicionar(alunoTeste);
		
		if (alunoCriado.Status is not StatusResposta.Sucesso) return;

		matriculaTeste.AlunoId = alunoTeste.Id;
		matriculaTeste.CursoId = cursoTeste.Id;
		
		await baseDeDados.Matriculas.Adicionar(matriculaTeste);
	}

	private static (string, string, CredenciaisUsuario) ObterCredenciais(
		string login,
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

		return (loginDefault, nomeDefault,
			new CredenciaisUsuario(senhaDefault));
	}

	public static bool ValidarDadosIniciais(BaseDeDados baseDeDados)
	{
		var cargoAdms = baseDeDados
			.Cargos
			.ObterPorNome(CargosPadrao.CargoAdministradores) is not
			null;

		var cargoAlunos = baseDeDados
		                  .Cargos
		                  .ObterPorNome(CargosPadrao.CargoAlunos) is not null;

		_ = UtilitarioAmbiente
		    .Variaveis
		    .TryGetValue(VariaveisAmbiente.MasterAdminNome,
		                 out var nomeDefault);
		var usuarioMestre = baseDeDados
		                    .Usuarios
		                    .ObterPorNome(nomeDefault) is not null;

		var materiaTeste = baseDeDados
		                   .Materias
		                   .ObterPorNome("Matéria Teste") is not null;

		var cursoTeste = baseDeDados
		                 .Cursos
		                 .ObterPorNome("Curso Teste") is not null;

		_ = UtilitarioAmbiente
		    .Variaveis
		    .TryGetValue(VariaveisAmbiente.UsuarioTesteLogin,
		                 out var loginAluno);
		var usuarioTeste = baseDeDados
		                   .Usuarios
		                   .ObterPorLogin(loginAluno) is not null;

		return usuarioMestre & cargoAdms & cargoAlunos & cursoTeste &
		       usuarioTeste & materiaTeste;
	}
}
