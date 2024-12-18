using college_management.Dados.Modelos;
using college_management.Views.Interfaces;


namespace college_management.Views;


public class ValidacaoView<T> : View, IValidacaoView
	where T : Modelo
{
	public ValidacaoView(string titulo, T modelo) : base(titulo)
	{
		_modelo = modelo;
	}

	private readonly T? _modelo;
	
	public bool ValidarModelo()
	{
		if (_modelo is not null) return true;

		InputView erroInput = new("Nulo");

		erroInput.LerEntrada($"{typeof(T).Name} Inexistente",
		                     "O Cargo inserido não foi "
		                     + "encontrado na base de dados."
		                     + "Pressione [Enter] para continuar.");

		return false;
	}
}
