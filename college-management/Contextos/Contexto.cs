using college_management.Constantes;
using college_management.Contextos.Interfaces;
using college_management.Dados;
using college_management.Dados.Modelos;
using college_management.Servicos;
using college_management.Views;


namespace college_management.Contextos;


public abstract class Contexto<T> : IContexto<T> where T : Modelo
{
	protected readonly BaseDeDados BaseDeDados;
	protected readonly Usuario     UsuarioContexto;

	protected Contexto(BaseDeDados baseDeDados, Usuario usuarioContexto)
	{
		BaseDeDados = baseDeDados;

		UsuarioContexto = usuarioContexto;
	}

	protected bool TemAcessoRestrito => UsuarioContexto is Gestor;

	public bool ValidarPermissoes()
	{
		if (TemAcessoRestrito) return true;

		View.Aviso("Você não tem permissão para acessar este recurso.");

		return false;
	}

	public void AcessarRecurso(string nomeRecurso)
	{
		var interfacesContexto = GetType().GetInterfaces();

		var recurso = interfacesContexto.Select(t => t.GetMethod(nomeRecurso))
		                                .FirstOrDefault(t => t is not null);

		if (recurso is null)
			throw new InvalidOperationException("Recurso inexistente");

		var task = (Task)recurso.Invoke(this, []);

		task?.Wait();
	}

	public abstract Task Cadastrar();

	public abstract Task Editar();

	public abstract Task Excluir();

	public abstract void Visualizar();

	public abstract void VerDetalhes();

	public async Task GerarRelatorio()
	{
		var modelos = (List<T>)GetModelos();

		ServicoRelatorios<T> servicoRelatorios = new(modelos);

		var relatorio = servicoRelatorios.GerarRelatorio();

		var caminhoRelatorio
			= await servicoRelatorios.ExportarRelatorio(relatorio);

		var resultado = caminhoRelatorio != string.Empty
			? $"""
			   Relatório gerado com sucesso.
			   Caminho do relatório: {caminhoRelatorio}
			   """
			: "Não foi possível gerar o relatório";

		View.Aviso(resultado);

		return;

		object GetModelos()
		{
			return typeof(T) switch
			{
				{ } tipoUsuario when tipoUsuario == typeof(Usuario) =>
					BaseDeDados.Usuarios.ObterTodos().Modelo!,
				{ } tipoCurso when tipoCurso == typeof(Curso) => BaseDeDados
					.Cursos.ObterTodos()
					.Modelo!,
				{ } tipoMateria when tipoMateria == typeof(Materia) =>
					BaseDeDados.Materias.ObterTodos().Modelo!,
				_ => throw new InvalidOperationException(
					"Tipo de modelo não suportado.")
			};
		}
	}


	public MenuView ObterMenuView()
	{
		var opcoes = ObterOpcoes();

		MenuView menuRecursos = new("Menu Recursos",
		                            $"Bem-vindo(a) ao recurso de {typeof(T).Name}.",
		                            opcoes);

		menuRecursos.ConstruirLayout();
		return menuRecursos;
	}

	public string[] ObterOpcoes()
	{
		string[] recursosDisponiveis;

		if (TemAcessoRestrito)
		{
			recursosDisponiveis = typeof(T).Name switch
			{
				nameof(Usuario) => OperacoesRecursos.RecursosEscritaUsuarios,
				nameof(Curso)   => OperacoesRecursos.RecursosEscritaCursos,
				_ =>
				[
					..OperacoesRecursos.RecursosLeitura,
					..OperacoesRecursos.RecursosEscrita
				]
			};

			return recursosDisponiveis;
		}

		recursosDisponiveis = typeof(T).Name switch
		{
			nameof(Usuario) => OperacoesRecursos.RecursosLeituraUsuarios,
			nameof(Curso)   => OperacoesRecursos.RecursosLeituraCursos,
			_               => OperacoesRecursos.RecursosLeitura
		};

		return recursosDisponiveis;
	}
}
