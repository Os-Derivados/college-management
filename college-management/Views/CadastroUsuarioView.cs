using college_management.Constantes;
using college_management.Views.Interfaces;


namespace college_management.Views;


public class CadastroUsuarioView : View, ICadastroUsuarioView
{
	public CadastroUsuarioView(string titulo) : base(titulo)
	{
		_inputCadastro = new InputView(Titulo);
	}

	private readonly InputView                  _inputCadastro;

	public Dictionary<string, string> DadosCadastro => _inputCadastro.EntradasUsuario;

	public void Cadastrar()
	{
		KeyValuePair<string, string?>[] mensagensUsuario =
		[
			new("Nome", "Insira o Nome: "),
			new("Login", "Insira o Login: "),
			new("Senha", "Insira a Senha: "),
			new("Cargo", "Insira o Cargo: ")
		];

		foreach (var mensagem
		         in mensagensUsuario)
			_inputCadastro.LerEntrada(mensagem.Key,
			                          mensagem.Value);

		KeyValuePair<string, string?>[] mensagensAluno =
		[
			new("Periodo", "Insira o Período: "),
			new("Curso", "Insira o nome do Curso: "),
			new("Modalidade", "Insira a Modalidade: ")
		];

		if (_inputCadastro.ObterEntrada("Cargo") is CargosPadrao.CargoAlunos)
		{
			foreach (var mensagem in mensagensAluno)
			{
				_inputCadastro.LerEntrada(mensagem.Key, mensagem.Value);
			}
		}
	}

	public (string nome, string login, string cargo, string senha)
		ObterDadosBase()
	{
		var nome = _inputCadastro.ObterEntrada("Nome");
		var login = _inputCadastro.ObterEntrada("Login");
		var cargo = _inputCadastro.ObterEntrada("Cargo");
		var senha = _inputCadastro.ObterEntrada("Senha");

		return (nome, login, cargo, senha);
	}
}
