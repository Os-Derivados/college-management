using college_management.Constantes;
using college_management.Contextos.Interfaces;
using college_management.Dados;
using college_management.Dados.Modelos;
using college_management.Dados.Repositorios;
using college_management.Views;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace college_management.Contextos;


public class ContextoMaterias : Contexto<Materia>
{
	public ContextoMaterias(BaseDeDados baseDeDados,
	                        Usuario     usuarioContexto) :
		base(baseDeDados,
		     usuarioContexto) { }

	public override async Task Cadastrar() 
	{
        var temPermissao =
            CargoContexto.TemPermissao(PermissoesAcesso.AcessoEscrita)
            || CargoContexto.TemPermissao(PermissoesAcesso.AcessoAdministradores);

        InputView inputUsuario = new("Cadastrar Matéria");
        inputUsuario.ConstruirLayout();

        if (!temPermissao)
        {
            inputUsuario.LerEntrada("Erro",
                                    "Você não tem permissão "
                                    + "para acessar esse recurso. ");

            return;
        }

        Dictionary<string, string> cadastroMateria
            = ObterCadastroMateria(inputUsuario);

        if (cadastroMateria["Confirma"] is not "S") return;

        if (!Enum.TryParse(cadastroMateria["Turno"], out Turno turnoEscolhido)) 
        {
            inputUsuario.LerEntrada("Erro",
                                    "O Turno inserido não foi "
                                    + "encontrado."
                                    + "Pressione Enter para continuar.");

            return;
        }

        if (!int.TryParse(cadastroMateria["CargaHoraria"], out int cargaHoraria))
        {
            inputUsuario.LerEntrada("Erro",
                                    "A carga horária inserido não é válida."
                                    + "Pressione Enter para continuar.");

            return;
        }

        Materia? novaMateria = new(cadastroMateria["Nome"], turnoEscolhido, cargaHoraria);

        if (novaMateria is null)
        {
            inputUsuario
                .LerEntrada("Erro",
                            $"Não foi possível criar uma nova {nameof(Materia)}.");

            return;
        }

        var foiAdicionado
            = await BaseDeDados.Materias.Adicionar(novaMateria);

        var mensagemOperacao = foiAdicionado
                                   ? $"{nameof(Materia)} cadastrado com sucesso."
                                   : $"Não foi possível cadastrar uma nova {nameof(Materia)}.";

        inputUsuario.LerEntrada("Sair", mensagemOperacao);
    }

    private Dictionary<string, string> ObterCadastroMateria(InputView inputUsuario)
    {
        KeyValuePair<string, string?>[] mensagensUsuario =
        [
            new("Nome", "Insira o Nome: "),
            new("Turno", "Insira o Turno: "),
            new("CargaHoraria", "Insira a Carga Horária: ")
        ];

        foreach (KeyValuePair<string, string?> mensagem
                 in mensagensUsuario)
            inputUsuario.LerEntrada(mensagem.Key,
                                    mensagem.Value);

        DetalhesView detalhesView = new("Confirmar Cadastro",
                                        inputUsuario
                                            .EntradasUsuario);

        detalhesView.ConstruirLayout();

        StringBuilder mensagemConfirmacao = new();
        mensagemConfirmacao.AppendLine(detalhesView.Layout
                                                   .ToString());

        mensagemConfirmacao.AppendLine("Confirma o Cadastro?\n");
        mensagemConfirmacao.Append("[S]im\t[N]ão: ");

        inputUsuario.LerEntrada("Confirma",
                                mensagemConfirmacao.ToString());

        return inputUsuario.EntradasUsuario;
    }

    public override async Task Editar() { throw new NotImplementedException(); }

	public override async Task Excluir() { throw new NotImplementedException(); }

	public override void Visualizar()  
	{
        RelatorioView<Materia> relatorioView;

        relatorioView = new RelatorioView<Materia>("Visualizar Matérias",
                                                       BaseDeDados.Materias.ObterTodos());

        relatorioView.ConstruirLayout();

        InputView inputRelatorio = new(relatorioView.Titulo);
        inputRelatorio.LerEntrada("Sair", relatorioView.Layout.ToString());
	}

	public override void VerDetalhes() { throw new NotImplementedException(); }
}
