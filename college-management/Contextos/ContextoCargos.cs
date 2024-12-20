using college_management.Constantes;
using college_management.Contextos.Interfaces;
using college_management.Dados;
using college_management.Dados.Modelos;
using college_management.Dados.Repositorios;
using college_management.Views;


namespace college_management.Contextos;


public class ContextoCargos : Contexto<Cargo>
{
	public ContextoCargos(BaseDeDados baseDeDados,
	                      Usuario     usuarioContexto) :
		base(baseDeDados,
		     usuarioContexto) { }

	public override async Task Cadastrar() 
    {

    }

	public override async Task Editar() { throw new NotImplementedException(); }

	public override async Task Excluir() { throw new NotImplementedException(); }

	public override void Visualizar()  
	{
        var temPermissao = CargoContexto.TemPermissao(PermissoesAcesso.AcessoAdministradores) ||
                                           CargoContexto.TemPermissao(PermissoesAcesso.AcessoEscrita);

        RelatorioView<Cargo> relatorioView;

        if (temPermissao)
        {
            relatorioView = new RelatorioView<Cargo>("Visualizar Usuários",
                                                       BaseDeDados.Cargos.ObterTodos());

        }

        else
            relatorioView = new RelatorioView<Cargo>("Visualizar Usuário", new List<Cargo>() 
            { 
                BaseDeDados.Cargos.ObterPorId(UsuarioContexto.CargoId) 
            });


        relatorioView.ConstruirLayout();
        relatorioView.Exibir();

        InputView inputRelatorio = new(relatorioView.Titulo);
        inputRelatorio.LerEntrada("Sair", relatorioView.Layout.ToString());
    }


	public override void VerDetalhes() 
    {
        var permissaoAdmin = CargoContexto.TemPermissao(PermissoesAcesso.AcessoAdministradores);


        if (permissaoAdmin)
        {
            OpcoesDeVisualizacao();

            Console.ReadKey();
        }
    }




    #region Metodos privados uteis

	void OpcoesDeVisualizacao()
	{
        MenuView menuPesquisa = new("Cargos",
                                    "Selecione um dos campos:",
                                    ["Nome do Cargo", "Id", "Exibir Todos"]);

        List<Cargo> cargos = new();
        RelatorioView<Cargo> relatorioView = null!;


        menuPesquisa.ConstruirLayout();
        menuPesquisa.LerEntrada();

        KeyValuePair<string, string>? campoPesquisa = menuPesquisa.OpcaoEscolhida switch
        {
            1 => new KeyValuePair<string, string>("Nome do Cargo",
                                                  "Insira o Nome do Cargo: "),
            2 => new KeyValuePair<string, string>("Id",
                                                  "Insira o Id do Cargo: "),

            _ => null
        };

        InputView inputPesquisa = new("Ver Detalhes: Pesquisar Cargo");

        if (campoPesquisa is null)
        {
            inputPesquisa.LerEntrada("Campo",
                                     "Campo inválido. Tente novamente.");

            return;
        }

        inputPesquisa.LerEntrada(campoPesquisa?.Key,
                                 campoPesquisa?.Value);

        

        if (menuPesquisa.OpcaoEscolhida is 1)
        {
            var nomeDoCargo = inputPesquisa.ObterEntrada("Nome do Cargo");
            cargos.Add(BaseDeDados.Cargos.ObterPorNome(nomeDoCargo));
        }

        else if (menuPesquisa.OpcaoEscolhida is 2)
        {
            var id = inputPesquisa.ObterEntrada("Id");
            cargos.Add(BaseDeDados.Cargos.ObterPorId(id));
        }

        if (!cargos.Any())
        {
            inputPesquisa.LerEntrada("Cargo",
                                     "Cargo não encontrado.");
            return;
        }

        relatorioView = new RelatorioView<Cargo>("Cargos", cargos);
        

        relatorioView.ConstruirLayout();
        relatorioView.Exibir();
    }

    #endregion
}