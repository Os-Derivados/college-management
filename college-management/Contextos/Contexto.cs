using college_management.Constantes;
using college_management.Contextos.Interfaces;
using college_management.Dados;
using college_management.Dados.Modelos;
using college_management.Views;


namespace college_management.Contextos;


public abstract class Contexto<T> : IContexto<T> where T : Modelo
{
	protected Contexto(BaseDeDados baseDeDados,
	                   Usuario     usuarioContexto)
	{
		BaseDeDados     = baseDeDados;
		UsuarioContexto = usuarioContexto;
	}

	protected readonly BaseDeDados BaseDeDados;
	protected readonly Usuario     UsuarioContexto;

	public void ListarOpcoes()
	{
		var opcoes = ObterOpcoes();

		MenuView menuRecursos = new("Menu Recursos",
		                            $"Bem vindo ao recuso de {typeof(T).Name}.",
		                            opcoes);

		menuRecursos.ConstruirLayout();
		menuRecursos.Exibir();
	}

	private string[] ObterOpcoes()
	{
		return typeof(T).Name switch
		{
			nameof(Usuario) => OperacoesRecursos.RecursoUsuarios,
			nameof(Curso)   => OperacoesRecursos.RecursoCursos,
			nameof(Materia) => OperacoesRecursos.RecursoMaterias,
			nameof(Cargo)   => OperacoesRecursos.RecursoCargos,
			_ => throw new
				     InvalidOperationException("Não há contexto definido para este tipo")
		};
	}

	public void AcessarRecurso(string nomeRecurso)
	{
		Type[] interfacesContexto = GetType().GetInterfaces();

		var recurso =
			interfacesContexto
				.Select(t => t.GetMethod(nomeRecurso))
				.FirstOrDefault(t => t is not null);

		if (recurso is null)
			throw new
				InvalidOperationException("Recurso inexistente");

		var task = (Task) recurso.Invoke(this, []);
		task.Wait();
	}

	public abstract Task Cadastrar();

	public abstract Task Editar();

	public abstract Task Excluir();

	public abstract void Visualizar();
}
